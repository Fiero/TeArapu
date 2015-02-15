using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GAF.Core;

public class Game : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------------------------------
	enum EGameState
	{

		Setup,
		PreGame,
		Play,
		Pause,
		Anim,
		Result,
		Gameover,
		
	};

	//-------------------------------------------------------------------------------------------------------------------------
	public		GameObject					AppPrefab;
	private 	EGameState					StateId;

	//-------------------------------------------------------------------------------------------------------------------------
	//private		EGameButtonId				ButtonId;
	private		Game_UIManager				Script_UIManager;
	private		Game_MinigameManager		Script_MinigameManager; 
	//-------------------------------------------------------------------------------------------------------------------------
	
	//Score
	private int currentScore = 0;
	private int highScore = 0;

	//Lives
	private int currentLives = 3;

	//Game timer
	public float maxGameCountdown = 10; //The longest/starting time that a minigame can last.
	public float minGameCountdown = 3; //The minimum time that a minigame can last.
	private float currentMaxGameCountdown; //The current max length of a minigame.
	private float currentGameCountdown; // The current time of the minigame.

	//Result screen timer
	public float maxResultCountdown = 3;
	private float currentResultCountdown;

	//Pregame Image timer
	public float maxPregameCountdown = 3;
	private float currentPregameCountdown;

	private int currentMinigameNumber;
	private GAFMovieClip movieClip;



	//-------------------------------------------------------------------------------------------------------------------------


	void Awake()
	{
		// Is the controlling app already in existance?
		if ( GameObject.Find("App") == null )
		{
			Debug.Log("Creating temporary app");

			Instantiate( AppPrefab );
		}

		StateId = EGameState.Setup;

		// Add all manager scripts here
		Script_UIManager = this.GetComponent<Game_UIManager>();
		Script_MinigameManager = this.GetComponent<Game_MinigameManager>();

		// Setup the UI elements properly
		Script_UIManager.GameplayScreenHide ();
		Script_UIManager.PauseScreenHide();
		Script_UIManager.ResultScreenHide();
		Script_UIManager.GameoverScreenHide();

		highScore = App.Instance.LoadHighScore();

	//	ButtonId = EGameButtonId.None;
	}

	//-------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		Debug.Log("WHATS HIGH:" + highScore); 
		BeginGame();
	}

	//-------------------------------------------------------------------------------------------------------------------------
	void Update()
	{

		switch ( StateId )
		{
			case EGameState.PreGame:
				PregameCountdownTimer();
			break;

			case EGameState.Play:
				GameCountdownTimer();
			break;

			case EGameState.Anim:
				//CountdownTimer();
			break;

			case EGameState.Result:
				ResultCountdownTimer();
			break;

			case EGameState.Gameover:

			break;

			default:
			break;
		}

		/*
		if (ButtonId == EGameButtonId.Leave)
		{
			Debug.Log("Game: Leaving.");

			App.Instance.LoadMainMenu();
		}

		ButtonId = EGameButtonId.None;
		*/
	}

	//-------------------------------------------------------------------------------------------------------------------------
	
	public void BeginGame(){

		currentMinigameNumber = Script_MinigameManager.GetNextMinigameNumber ();
		

		currentGameCountdown = maxGameCountdown;
		currentResultCountdown = maxResultCountdown;
		currentPregameCountdown = maxPregameCountdown;

		Script_UIManager.PauseScreenHide();
		Script_UIManager.ResultScreenHide();

		//Script_MinigameManager.StartMinigame (); //Activates game.

		//StateId = EGameState.Play;

		//instead of starting here, this should move you to pregame and start a pregame countdown

		ShowPreGameImage();
	}

	void ShowPreGameImage(){
		print ("SHOW PREGAME IMAGE: " + currentMinigameNumber);

		Script_UIManager.ActivatePregameImage ( (currentMinigameNumber - 1) );

		StateId = EGameState.PreGame;

	}

	void ShowAnimation(bool isWin){
		GameObject tempAnim;

		Script_UIManager.GameplayScreenHide ();
		StateId = EGameState.Anim;
		//Script_UIManager.ActivateAnimation (isWin, (currentMinigameNumber - 1));
		//tempAnim = Script_UIManager.GetCurrentAnimation(isWin, (currentMinigameNumber - 1));

		Script_UIManager.ActivateAnimation (isWin, (0));
		tempAnim = Script_UIManager.GetCurrentAnimation(isWin, (0));

		movieClip = tempAnim.GetComponent<GAFMovieClip> ();
		movieClip.on_stop_play += AnimCallback;
		}

	private void AnimCallback(IGAFMovieClip _Clip)
	{
		print ("FINISHED PLAYING");
		movieClip.on_stop_play -= AnimCallback;
		Script_UIManager.DisableAllAnims ();
		StateId = EGameState.Result;
		Script_UIManager.ResultScreenShow ();
	}



	public void QuitToMainMenu(){
		Debug.Log("Game: Leaving.");
		App.Instance.LoadMainMenu();
	}

	public void RestartGame(){
		ResetLives();
		ResetScore();

		Script_UIManager.GameoverScreenHide();
		Script_MinigameManager.SetupMinigameRoundList();

		BeginGame(); 


		
	}

	public void Gameover(){

		Script_UIManager.SetGameoverScoreLbl(currentScore);
		CheckHighScore();

		StateId = EGameState.Gameover;
		Script_UIManager.ResultScreenHide();
		Script_UIManager.GameoverScreenShow();



		
	}

	void CheckHighScore(){
		if(currentScore>highScore){
			//NEW HIGH SCORE
			highScore = currentScore;
			App.Instance.SaveHighScore(highScore);
		}
		Script_UIManager.SetGameoverHighScoreLbl(highScore);


	}

	public void MinigameWin(){
		//AddScore(Random.Range(10,100));

		int calcScoreAdd = (int) Mathf.Round((100+(currentGameCountdown*10)));

		AddScore(calcScoreAdd);

		//print("ADD ME:" + Mathf.Round((100+(currentGameCountdown*10))));

		//
	//	Script_UIManager.ResultScreenShow();
	//	StateId = EGameState.Result;
		//Go to win animation instead, THEN result screen.
		ShowAnimation (true);

		NextMiniGameSetup();
	} 

	public void MinigameLose(){
		//AddScore(120);

		//AddScore(Random.Range(10,100));


		LoseLife();

		//
	//	StateId = EGameState.Result;
	//	Script_UIManager.ResultScreenShow();
		//Go to lose animation instead, THEN result screen.
		ShowAnimation (false);

		if(currentLives>0){
			NextMiniGameSetup();
		}

		else{
			Script_MinigameManager.ResetMinigame();
			Script_MinigameManager.DisableAllMinigames();
		}		
	}

	void NextMiniGameSetup(){
		Script_MinigameManager.ResetMinigame();
		//Script_MinigameManager.NextGame();

		Script_MinigameManager.DisableAllMinigames ();
	}

	public void TogglePause(){
		switch ( StateId )
		{
			case EGameState.Play:
				Debug.Log("Game: Toggle to Pause");
				Script_UIManager.PauseScreenShow();
				StateId = EGameState.Pause;
				Time.timeScale = 0f;
			break;

			case EGameState.Pause:
				Debug.Log("Game: Toggle from Pause");
				Script_UIManager.PauseScreenHide();
				StateId = EGameState.Play;
				Time.timeScale = 1f;
			break;

			default:
			break;
		}
	}

	//Score stuff//
	public int GetScore(){
		return currentScore;
	}

	void AddScore(int amountToAdd){
		currentScore+= amountToAdd;
		Script_UIManager.SetResultScoreLbl((currentScore-amountToAdd), currentScore);
	}

	void ResetScore(){
		currentScore = 0;
		Script_UIManager.SetResultScoreLblInstant(currentScore);
	}
	//Score stuff//

	void LoseLife(){
		currentLives--;
		Script_UIManager.SetLivesSprites(currentLives);
	}

	void ResetLives(){
		currentLives = 3;
		Script_UIManager.ResetLivesSprites();
	}

	//Game Timer stuff//
	void GameCountdownTimer(){
		currentGameCountdown -= Time.deltaTime;

		if(currentGameCountdown<0){
			//Script_MinigameManager.TimeUpLose();
			MinigameLose();
		}

		SetTimerUI();

	}

	//Game Timer stuff - Reset//
	void ResetTimer(){
		currentGameCountdown = maxGameCountdown;
		SetTimerUI();
		
	}

	//Result Timer //
	void ResultCountdownTimer(){
		currentResultCountdown -= Time.deltaTime;
		
		Script_UIManager.SetResultCountdownLbl(currentResultCountdown);
		
		if(currentResultCountdown<0){
			if(currentLives>0){
				currentResultCountdown = maxResultCountdown;
				
				Script_MinigameManager.NextGame(); //Chooses next game.
				BeginGame();
			}
			
			else{
				Gameover();
			}
		}
	}

	//Pregame image Timer stuff//
	void PregameCountdownTimer(){
		currentPregameCountdown -= Time.deltaTime;
		
		if(currentPregameCountdown<0){
			print ("GO TO NEW GAME");
			currentPregameCountdown = maxPregameCountdown;

			//Move on to the actual game.

			Script_UIManager.DisableAllPregameImages();
			Script_UIManager.GameplayScreenShow ();

			Script_MinigameManager.StartMinigame (); //Activates game.
			
			StateId = EGameState.Play;

		}
		
	}


	void SetTimerUI(){

		float normalizedValue = Mathf.InverseLerp(0, maxGameCountdown, currentGameCountdown);
		//Script_UIManager.game_countdownBar.value = normalizedValue;
		Script_UIManager.SetGameCountdownBar(normalizedValue);
	}
	//Game Timer stuff//

}

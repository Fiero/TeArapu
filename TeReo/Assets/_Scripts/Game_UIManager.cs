using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_UIManager : MonoBehaviour {

	public GameObject Screen_Pause;
	public GameObject Screen_Result;
	public GameObject Screen_Gameover;

	public UISlider game_countdownBar;
	public UILabel result_countdownLbl;
	public UILabel result_scoreLbl;

	public UILabel gameover_scoreLbl;
	public UILabel gameover_highscoreLbl;

	public UITweener[] result_livesSprites;

	public Transform pregameImagesParent;
	public List<GameObject> pregameImagesArray;

	//-------------------------------------------------------------------------------------------------------------------------
	private bool updateScore = false;
	public float scoreUpdateDuration = 1.0f; // in sec
	private int passes = 100;
	private int currentPass = 0;
	private float increment = 0.0f;

	private float tempUpdateScore = 0;
	private float finalUpdateScore = 0;

	
	void Awake(){
		GetAllPregameImages ();
	}

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		if(updateScore){

			if(currentPass<passes){
				//tempUpdateScore = Mathf.Lerp(tempUpdateScore,finalUpdateScore,0.1f);

				tempUpdateScore += increment;

				result_scoreLbl.text = Mathf.CeilToInt(tempUpdateScore).ToString();

				
				currentPass++;
			}

			else{
				result_scoreLbl.text = Mathf.CeilToInt(finalUpdateScore).ToString();
			}

		}
	
	}

//-------------------------------------------------------------------------------------------------------------------------

	void GetAllPregameImages()
	{
		foreach (Transform child in pregameImagesParent){
			print("Foreach loop: " + child);
			pregameImagesArray.Add(child.gameObject);
		}
		
	}

	public void DisableAllPregameImages(){
		foreach(GameObject pregame in pregameImagesArray)
		{
			pregame.SetActive(false);
		}
	}
	
	public void EnableAllPregameImages(){
		foreach(GameObject pregame in pregameImagesArray)
		{
			pregame.SetActive(true);
		}
	}
	
	public void ActivatePregameImage(int pregameNumber){
		DisableAllPregameImages();
		pregameImagesArray[pregameNumber].SetActive(true);
	}

//-------------------------------------------------------------------------------------------------------------------------
	public void PauseScreenShow(){
		Screen_Pause.SetActive(true);
	}

	public void PauseScreenHide(){
		Screen_Pause.SetActive(false);
	}

	public void ResultScreenShow(){
		Screen_Result.SetActive(true);
	}

	public void ResultScreenHide(){
		Screen_Result.SetActive(false);
	}

	public void GameoverScreenShow(){
		Screen_Gameover.SetActive(true);
	}

	public void GameoverScreenHide(){
		Screen_Gameover.SetActive(false);
	}

	public void SetGameCountdownBar(float amount){
		game_countdownBar.value = amount;
	}

	public void SetResultCountdownLbl(float amount){
		int roundedAmount = Mathf.CeilToInt(amount);

		result_countdownLbl.text = roundedAmount.ToString();
	}
//-------------------------------------------------------------------------------------------------------------------------
	public void SetResultScoreLbl(float oldAmount, float amount){

		tempUpdateScore = oldAmount;
		result_scoreLbl.text = Mathf.CeilToInt(tempUpdateScore).ToString();


		//tempUpdateScore = float.Parse( result_scoreLbl.text);
		finalUpdateScore = amount;

		passes = Mathf.CeilToInt(scoreUpdateDuration * (1.0f / Time.deltaTime));
		currentPass = 0;


		float scoreDifference = finalUpdateScore - tempUpdateScore;

		increment = Mathf.Lerp(0,scoreDifference,(1.0f/passes));


		if(tempUpdateScore!=finalUpdateScore){
			updateScore = true;
		}
	}
//-------------------------------------------------------------------------------------------------------------------------

	public void SetResultScoreLblInstant(float amount){
		result_scoreLbl.text = Mathf.CeilToInt(amount).ToString();
	}
//-------------------------------------------------------------------------------------------------------------------------
	public void SetGameoverScoreLbl(int score){
		gameover_scoreLbl.text = score.ToString();
	}

	public void SetGameoverHighScoreLbl(int score){
		gameover_highscoreLbl.text = score.ToString();
	}

//-------------------------------------------------------------------------------------------------------------------------
	public void SetLivesSprites(int numberOfLives){
		switch(numberOfLives){
			case 0:
			result_livesSprites[2].PlayForward();
			break;

			case 1:
			result_livesSprites[1].PlayForward();
			break;

			case 2:
			result_livesSprites[0].PlayForward();
			break;

			case 3:
			break;

			default:
			break;
		}

	}
//-------------------------------------------------------------------------------------------------------------------------

	public void ResetLivesSprites(){
		result_livesSprites[0].ResetToBeginning();
		result_livesSprites[1].ResetToBeginning();
		result_livesSprites[2].ResetToBeginning();
		
	}
}

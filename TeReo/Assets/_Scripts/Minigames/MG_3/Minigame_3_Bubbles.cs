using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Minigame_3_Bubbles : MonoBehaviour {

	public TextAsset wordlist;

	private Game g; // g.MinigameWin(); g.MinigameLose();
	private string[] alphaWordList = new string[] {"ara","aroha","hui","kata","kei","kutu","puku","tangi","taniwha","tapu","whare"};

	private List<string> fullWordList = new List<string>();
	public List<string> chosenWords = new List<string>();

	public int difficulty = 5;

	public UILabel[] UILabelArray;
	public Transform LabelParent;

	private int numCorrect = 0;

	private string currentCorrectWord;

	public MG_3_ButtonEvent[] buttons;


	void Awake(){
		GetWordList();

		g = GameObject.Find("Game").GetComponent<Game>();

		GetAllButtons();
		GetAllUILabels();
	}

	// Use this for initialization
	void Start () {
		SetupMinigame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetWordList(){
		//alphaWordList = File.ReadAllLines(wordlist.text);
		alphaWordList = wordlist.text.Split('\n'); //C#
	}

	void GetAllButtons(){

		buttons = LabelParent.GetComponentsInChildren<MG_3_ButtonEvent>();

	}

	void GetAllUILabels(){
		UILabelArray =  LabelParent.GetComponentsInChildren<UILabel>();
	}

	void SetupMinigame(){

		int randomWord;

		for(int i = 0; i < alphaWordList.Length; i++){
			fullWordList.Add(alphaWordList[i]);
		}

		for(int j = 0; j<difficulty; j++){
			randomWord = Random.Range(0,fullWordList.Count);

			chosenWords.Add(fullWordList[randomWord]);
			fullWordList.RemoveAt(randomWord);
		}

		SetUILabels();

		chosenWords.Sort();

		currentCorrectWord = chosenWords[numCorrect];

	}

	void SetUILabels(){
	       for(int i = 0; i<chosenWords.Count; i++){
	       		UILabelArray[i].text = chosenWords[i];
	       }
	}

	public void Answer(bool correct){
		if(correct){
			numCorrect++;

			if(numCorrect<difficulty){ //Next right word
				currentCorrectWord = chosenWords[numCorrect];
			}

			else{ //YOU WIN!

				 Reset();
				g.MinigameWin();
			}	
		}

		else{

			 Reset();
			g.MinigameLose();
		}
		print("CLCIKED: " + correct);

	}

	public string GetCorrectWord(){
		return currentCorrectWord;
	}

	public void Reset(){
		//NEED TO RESET ALL THE BUTTONS

		numCorrect=0;

		fullWordList.Clear();
		chosenWords.Clear();

		for(int i = 0; i<buttons.Length; i++){
	       		buttons[i].ResetButton();
	       }

		SetupMinigame();
	}
}

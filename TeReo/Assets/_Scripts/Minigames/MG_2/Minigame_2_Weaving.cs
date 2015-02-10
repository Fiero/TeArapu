using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Minigame_2_Weaving : MonoBehaviour {

	private Game g; // g.MinigameWin(); g.MinigameLose();
	private string[] alphabet = new string[] {"a","e","h","i","k","m","n","ng","o","p","r","t","u","w","wh"};

	private List<string> fullAlphabet = new List<string>();
	private List<string> chosenSequence = new List<string>();

	//public List<UILabel> UILabelArray;

	public UILabel[] UILabelArray;

	private int incorrectLetters = 1; //how many incorrect letters are in the sequence;
	private int replacementLetter; //The letter that has been replaced 0-4

	//public List<MG_2_CustomSlider> sliders;

	public MG_2_CustomSlider[] sliders;

	public Transform SliderParent;

	void Awake(){
		g = GameObject.Find("Game").GetComponent<Game>();

		GetAllSliders();
		GetAllUILabels();


	}

	// Use this for initialization
	void Start () {
		SetupMinigame();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetAllSliders(){

		sliders = SliderParent.GetComponentsInChildren<MG_2_CustomSlider>();

			/*foreach (MG_2_CustomSlider child in SliderParent){
	             print("Foreach loop: " + child);
	             sliders.Add(child);
	        	}
        */
	}

	void GetAllUILabels(){

		UILabelArray =  SliderParent.GetComponentsInChildren<UILabel>();

	}

	void SetupMinigame(){
		//Add all letters into the fullAlphabet list
		for(int i = 0; i < alphabet.Length; i++){
			fullAlphabet.Add(alphabet[i]);
		}

		int tempStartingLetter = Random.Range(0,alphabet.Length-4);

		//int tempStartingLetter = alphabet.Length-5;

		//Add 5 consecutive letters into a new List, removing from the old;
		for(int j = 0; j<5; j++){
			

			chosenSequence.Add(fullAlphabet[tempStartingLetter]);

			//print("ADD:"  + chosenSequence[j]  +  "  REMOVE: " + fullAlphabet[tempStartingLetter]);

			fullAlphabet.RemoveAt(tempStartingLetter);
		}

		//Pick the letter to be replaced. Never Replace the 'open end' of the sequence.
		if(tempStartingLetter == 0){
			print("Dont replace K");
			replacementLetter = Random.Range(0,4);
		}

		else if( tempStartingLetter == (alphabet.Length-5) ){
			print("Dont replace R");
			replacementLetter = Random.Range(1,5);
		}

		else{
			replacementLetter = Random.Range(0,5);
		}

		chosenSequence[replacementLetter] = fullAlphabet[Random.Range(0,fullAlphabet.Count)];

		print("Replased letter: " + chosenSequence[replacementLetter]);

		SetUILabels();

	}

	void SetUILabels(){
		 /*foreach(UILabel label in UILabelArray)
	        {
	            label.text = chosenSequence[UILabelArray.IndexOf(label)];
	        }
	        */

	       for(int i = 0; i<UILabelArray.Length; i++){
	       		UILabelArray[i].text = chosenSequence[i];
	       }
	}

	void ChooseAnswer(int answer){
		print("Answer " + answer);

		if(answer == replacementLetter){

				print("SUCCESS: Finished");
				Reset();
				
				g.MinigameWin();
				
		}

		else{
			print("FAIL");
			Reset();

			g.MinigameLose();
			
		}
	}

	public void ClickButton1(){
		ChooseAnswer(0);
	}

	public void ClickButton2(){
		ChooseAnswer(1);
	}

	public void ClickButton3(){
		ChooseAnswer(2);
	}

	public void ClickButton4(){
		ChooseAnswer(3);
	}

	public void ClickButton5(){
		ChooseAnswer(4);
	}

	void Reset(){
		fullAlphabet.Clear();
		chosenSequence.Clear();

		for(int i = 0; i<sliders.Length; i++){
	       		sliders[i].ResetSlider();
	       }

		SetupMinigame();
	}
}

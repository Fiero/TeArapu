using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Minigame_4_Twitch : MonoBehaviour {

	private Game g; // g.MinigameWin(); g.MinigameLose();
	private string[] alphabet = new string[] {"a","e","h","i","k","m","n","ng","o","p","r","t","u","w","wh"};

	public Transform UIParent;
	public UILabel[] UILabelArray;

	private bool isLeftCorrect = false;

	void Awake(){
		g = GameObject.Find("Game").GetComponent<Game>();

		GetAllUILabels();
	}

	// Use this for initialization
	void Start () {
		SetupMinigame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetAllUILabels(){
		UILabelArray =  UIParent.GetComponentsInChildren<UILabel>();
	}

	public void Answer_Left(){
		if(isLeftCorrect){
			Answer(true);
		}
		else{
			Answer(false);
		}
		
	}

	public void Answer_Right(){
		if(isLeftCorrect){
			Answer(false);
		}
		else{
			Answer(true);
		}
	}

	void Answer(bool correct){

		if(correct){
			Reset();
			g.MinigameWin();
		}

		else{
			Reset();
			g.MinigameLose();
		}
	}

	void SetupMinigame(){
		int randomLetter1 = Random.Range(0,alphabet.Length);
		int randomLetter2 = Random.Range(0,alphabet.Length);

		if(randomLetter1==randomLetter2){
			//If the final letter, minus something
			if(randomLetter2==alphabet.Length-1){
				randomLetter2 -= Random.Range(1,alphabet.Length-1);
			}

			//Add something
			else{
				randomLetter2 += Random.Range(1,alphabet.Length-randomLetter2-1);
			}
		}

		if(randomLetter1<randomLetter2){
			isLeftCorrect = true;
		}

		else{
			isLeftCorrect = false;
		}

		print( randomLetter1 + " : " + randomLetter2);
		SetUILabels(alphabet[randomLetter1], alphabet[randomLetter2]);

	}

	void SetUILabels(string left, string right){
	       UILabelArray[0].text =  left;
	       UILabelArray[1].text =  right;
	}

	void Reset(){
		SetupMinigame();
	}
}

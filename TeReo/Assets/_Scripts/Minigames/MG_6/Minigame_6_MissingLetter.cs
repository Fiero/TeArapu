using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Minigame_6_MissingLetter : MonoBehaviour {

	private Game g; // g.MinigameWin(); g.MinigameLose();
	private string[] alphabet = new string[] {"a","e","h","i","k","m","n","ng","o","p","r","t","u","w","wh"};

	public List<int> fullAlphaList = new List<int>();
	public List<int> chosenLettersIndex = new List<int>();
	public List<string> chosenLetters = new List<string>();

	public Transform letterTransformParent;
	public Transform[] letterTransformArray;

	public Transform[] dropTargetAreaArray;

	public Transform dragContainerParent;
	public GameObject[] dragAreaArray;

	public UIDragDropItem_Custom_MG6[] tilesArray;

	private int answersCorrect = 0;



	void Awake(){
		g = GameObject.Find("Game").GetComponent<Game>();
				letterTransformParent  = GameObject.Find("FullAlphabet").GetComponent<Transform>();;
				dragContainerParent = GameObject.Find("DragContainers").GetComponent<Transform>();;

		GetAllTiles();
		GetAllLetterTransforms();
	}

	// Use this for initialization
	void Start () {
//GetAllTiles();
		//GetAllLetterTransforms();

		SetupMinigame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetAllTiles(){
		tilesArray = dragContainerParent.GetComponentsInChildren<UIDragDropItem_Custom_MG6>();
	}

	void GetAllLetterTransforms(){

		letterTransformArray =  letterTransformParent.GetComponentsInChildren<Transform>();
	}

	void SetupMinigame(){

		int randomLetter;

		for(int i = 0; i < alphabet.Length; i++){
			//fullAlphaList.Add(alphabet[i]);

			fullAlphaList.Add(i);
		}
		
		for(int j = 0; j<3; j++){
			randomLetter = Random.Range(0,fullAlphaList.Count);

			chosenLettersIndex.Add(fullAlphaList[randomLetter]);
			chosenLetters.Add(alphabet[fullAlphaList[randomLetter]]);
			fullAlphaList.RemoveAt(randomLetter);

			//Remove the following letter as well.
			if(randomLetter<fullAlphaList.Count){
				fullAlphaList.RemoveAt(randomLetter);
			}

			//Remove the preceding letter as well.
			if(randomLetter > 0){
				fullAlphaList.RemoveAt(randomLetter-1);
			}
		}

		PositionDropAreas();
		SetAllTileLabels();
		
	}

	void PositionDropAreas(){

		Transform tempTrans;
		UILabel tempLabel;
		MG_6_DropArea tempDropArea;

		for(int i = 0; i < chosenLettersIndex.Count; i++){

			//Set the position of the drop area to the correct;
			tempTrans = dropTargetAreaArray[i];
			tempTrans.position = letterTransformArray[chosenLettersIndex[i]+1].position;

			//Turn the labels of the 3 chosen shapes off.
			tempLabel = letterTransformArray[chosenLettersIndex[i]+1].gameObject.GetComponent<UILabel>();
			tempLabel.enabled = false;

			tempDropArea = tempTrans.gameObject.GetComponent<MG_6_DropArea>();
			tempDropArea.SetCorrectLetter( chosenLettersIndex[i] );
		}
	}

	void SetAllTileLabels(){
		for(int i = 0; i<tilesArray.Length ; i++){

			tilesArray[i].SetLetter(chosenLettersIndex[i], chosenLetters[i] );
		}
	}

	public void Answer(bool correct){

		if(correct){
			answersCorrect++;

			if(answersCorrect>=3){
				Reset();
				g.MinigameWin();
			}
			
		}

		else{
			Reset();
			g.MinigameLose();
		}
	}

	public void Reset(){
		answersCorrect = 0;

		ResetTiles();
		ResetLetterLabels();

		fullAlphaList.Clear();
		chosenLettersIndex.Clear();
		chosenLetters.Clear();

		SetupMinigame();
	}

	void ResetTiles(){
		for(int i = 0; i<tilesArray.Length ; i++){

			tilesArray[i].ForceParent( dragAreaArray[i] );
		}
	}

	void ResetLetterLabels(){

		for(int i  = 1; i<letterTransformArray.Length ; i++){
			letterTransformArray[i].gameObject.GetComponent<UILabel>().enabled = true;
		}
	}
}

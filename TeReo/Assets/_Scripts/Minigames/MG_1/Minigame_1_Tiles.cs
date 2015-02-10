using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Minigame_1_Tiles : MonoBehaviour {

	private Game g;

	private string[] alphabet = new string[] {"a","e","h","i","k","m","n","ng","o","p","r","t","u","w","wh"};

	private List<string> tempSequence;
	private List<int> correctOrder;

	private List<int> wrongChoices;

	public List<UILabel> UILabelArray;
	public Transform[] UILabelParents;

	public GameObject AnswerSliderParent;


	private int currentProgress = 0;

		int tempCorrectNumber;
		int tempIncorrectNumber1;
		int tempIncorrectNumber2;
		int correctPosition;
		int wrongPosChoice;

	void Awake(){
		g = GameObject.Find("Game").GetComponent<Game>();
	}

	// Use this for initialization
	void Start () {
		tempSequence = new List<string>();
		correctOrder = new List<int>();

		wrongChoices  = new List<int>();

		UILabelArray = new List<UILabel>();

		GetAllUILabels();

		SetupMinigame();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetAllUILabels(){
		for(int i = 0; i<UILabelParents.Length; i++){
			foreach (Transform child in UILabelParents[i]){
	             print("Foreach loop: " + child);
	             UILabelArray.Add(child.gameObject.GetComponent<UILabel>());
	        	}
        }
	}

	void SetUILabels(){
		 foreach(UILabel label in UILabelArray)
	        {
	            label.text = tempSequence[UILabelArray.IndexOf(label)];
	        }
	}



	void SetupMinigame(){

		int wrongIndex;

		

		for(int i = 0; i<3; i++){

			//Can be anything from a-u
			tempCorrectNumber = Random.Range(0,alphabet.Length-2); //0-12

			//Add all possible wrong letters into list
			for(int j = tempCorrectNumber+1; j<alphabet.Length; j++){
				wrongChoices.Add(j);
			}

			//Choose first wrong letter from list
			wrongIndex = Random.Range(0, wrongChoices.Count);
     		tempIncorrectNumber1 = wrongChoices[wrongIndex];
     		wrongChoices.RemoveAt(wrongIndex);

     		//Choose second wrong letter from list
     		wrongIndex = Random.Range(0, wrongChoices.Count);
     		tempIncorrectNumber2 = wrongChoices[wrongIndex];

     		//Clear wrong list.
     		wrongChoices.Clear();


			correctPosition = Random.Range(0,3); //0,1,2
			wrongPosChoice = Random.Range(0,2);

			correctOrder.Add(correctPosition);


			if(correctPosition==0){
				if(wrongPosChoice==0){
					tempSequence.Add(alphabet[tempCorrectNumber]);
					tempSequence.Add(alphabet[tempIncorrectNumber1]);
					tempSequence.Add(alphabet[tempIncorrectNumber2]);
				}

				else{
					tempSequence.Add(alphabet[tempCorrectNumber]);
					tempSequence.Add(alphabet[tempIncorrectNumber2]);
					tempSequence.Add(alphabet[tempIncorrectNumber1]);
				}
			}

			else if(correctPosition==1){
				if(wrongPosChoice==0){
					tempSequence.Add(alphabet[tempIncorrectNumber1]);
					tempSequence.Add(alphabet[tempCorrectNumber]);
					tempSequence.Add(alphabet[tempIncorrectNumber2]);
				}

				else{
					tempSequence.Add(alphabet[tempIncorrectNumber2]);
					tempSequence.Add(alphabet[tempCorrectNumber]);
					tempSequence.Add(alphabet[tempIncorrectNumber1]);
				}
			}

			else{
				if(wrongPosChoice==2){
					tempSequence.Add(alphabet[tempIncorrectNumber1]);
					tempSequence.Add(alphabet[tempIncorrectNumber2]);
					tempSequence.Add(alphabet[tempCorrectNumber]);

				}

				else{
					tempSequence.Add(alphabet[tempIncorrectNumber2]);
					tempSequence.Add(alphabet[tempIncorrectNumber1]);
					tempSequence.Add(alphabet[tempCorrectNumber]);
				}
			}



		}

		string tempSequenceList = "Sequence: ";
		foreach(string sequence in tempSequence)
        	{
        		tempSequenceList += sequence+", ";
        	}
        	print(tempSequenceList);

        string tempOrderList = "Order: ";
		foreach(int order in correctOrder)
        	{
        		tempOrderList += order+", ";
        	}
        	print(tempOrderList);

        	SetUILabels();

	}

	void ChooseAnswer(int answer){
		print("Answer " + answer);

		if(answer == correctOrder[currentProgress]){
			

			if(currentProgress<2){
				print("SUCCESS" + currentProgress);
				currentProgress++;
				TweenPosition.Begin(AnswerSliderParent, 0.15f, (AnswerSliderParent.transform.localPosition+new Vector3(-100f,0,0)));
			}

			else{
				print("SUCCESS: Finished");
				Reset();
				
				g.MinigameWin();
				
			}
			
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

	public void Reset(){
		tempSequence.Clear();
		correctOrder.Clear();

		//tempSequence = new List<string>();
		//correctOrder = new List<int>();

		currentProgress=0;
		TweenPosition.Begin(AnswerSliderParent, 0.0f, new Vector3(0,0,0));

		SetupMinigame();
	}
}

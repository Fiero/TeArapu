using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Minigame_8_Slingshot : MonoBehaviour {

	private Game g; // g.MinigameWin(); g.MinigameLose();
	private string[] alphabet = new string[] {"a","e","h","i","k","m","n","ng","o","p","r","t","u","w","wh"};

	public Transform letterTransformParent;
	public Transform[] letterTransformArray;

	private int correctLetterIndex;
	private int selectedLetterIndex;
	private int realSelectedLetterIndex;

	public Transform slingshotDragPoint;
	public LineRenderer line_L;
	public LineRenderer line_R;

	private bool tweeningSlingshot = false;
	public SpringPosition springPos; 

	public TweenPosition UITP;

	void Awake(){
		g = GameObject.Find("Game").GetComponent<Game>();

		letterTransformParent  = GameObject.Find("MG8_FullAlphabet").GetComponent<Transform>();;

		slingshotDragPoint = GameObject.Find("Slingshot_Midpoint").GetComponent<Transform>();
		springPos = slingshotDragPoint.gameObject.GetComponent<SpringPosition>();

		line_L = GameObject.Find("Line_L").GetComponent<LineRenderer>();
		line_R = GameObject.Find("Line_R").GetComponent<LineRenderer>();

		GetAllLetterTransforms();

	}

	// Use this for initialization
	void Start () {
		SetupMinigame();
	}
	
	// Update is called once per frame
	void Update () {
		if(tweeningSlingshot){
			SetLines();
		}
	
	}

	void GetAllLetterTransforms(){

		letterTransformArray =  letterTransformParent.GetComponentsInChildren<Transform>();
	}

	void SetupMinigame(){
		ResetLetterLabels();

		int randomLetter;
		int replacementLetter;

		randomLetter = Random.Range(0,alphabet.Length);
		replacementLetter = Random.Range(0,alphabet.Length);

		//If random letters are the same.
		if(randomLetter == replacementLetter){

			
			int upOrDown = Random.Range(0,2);

			//If A, need to pick up
			if(randomLetter==0){
				replacementLetter = Random.Range(1,alphabet.Length);
			}

			//If Wh, need to pick down
			else if(randomLetter == (alphabet.Length-1)){
				replacementLetter = Random.Range(0, (alphabet.Length-1) );
			}

			//50% pick up
			else if(upOrDown==0){
				replacementLetter = Random.Range( (randomLetter+1) ,alphabet.Length);
			}

			//50% pick down
			else{
				replacementLetter = Random.Range( 0 ,randomLetter);

			}

		}

		print("CHOSEN: " + randomLetter + "-->" + replacementLetter);
		SetIncorrectLetterLabel(randomLetter,replacementLetter);

		correctLetterIndex = randomLetter;

		//SetLines();


	}

	void ResetLetterLabels(){
		UILabel tempLabel;

		for(int i  = 1; i < letterTransformArray.Length ; i++){
			tempLabel = letterTransformArray[i].gameObject.GetComponent<UILabel>();

			tempLabel.text = alphabet[i-1];
		}

	}

	void SetIncorrectLetterLabel(int incorrectLetter, int replacementLetter){
		UILabel tempLabel;
		tempLabel = letterTransformArray[incorrectLetter+1].gameObject.GetComponent<UILabel>();
		tempLabel.text = alphabet[replacementLetter];

	}

	public void SetSlingshotMidpoint(Vector3 pos, int letterIndex){
		selectedLetterIndex = letterIndex;
		realSelectedLetterIndex = selectedLetterIndex-1;

		slingshotDragPoint.localPosition = pos;

		SetLines();

		UITP = letterTransformArray[selectedLetterIndex].gameObject.GetComponent<TweenPosition>();
	}

	public void MoveSlingshotMidpoint (Vector3 delta)
	{
		Vector3 tempPos = slingshotDragPoint.localPosition;
		tempPos.y += delta.y;

		if(tempPos.y < 0.0f){
			slingshotDragPoint.localPosition = tempPos;
		}

		SetLines();
	}

	

	public void SpringMidpoint(){
		tweeningSlingshot = true;

		Vector3 temp = slingshotDragPoint.localPosition;
		temp.y = 0;

		springPos.target = temp;

		print("Y:" + slingshotDragPoint.localPosition.y);

		if(slingshotDragPoint.localPosition.y < -150.0f){
			springPos.onFinished += SpringDone;
		}
		

		springPos.enabled = true;
	}

	void SpringDone(){
		springPos.onFinished -= SpringDone;
		tweeningSlingshot = false;
		print("SELECTED:" + realSelectedLetterIndex + "   CORRECT:" + correctLetterIndex);



		if(correctLetterIndex == realSelectedLetterIndex){
			print("CORRECT");

			Reset();
			g.MinigameWin();

			//UITP.enabled = true;
		}

		else{
			print("FAIL");

			Reset();
			g.MinigameLose();

		}

			

		//letterTransformArray[selectedLetterIndex]
	}


	void SetLines(){


		line_L.SetPosition(1, slingshotDragPoint.localPosition);
		line_R.SetPosition(1, slingshotDragPoint.localPosition);

		SetLetterLabelPositions();

	}

	void SetLetterLabelPositions(){
		for(int i  = 1; i < selectedLetterIndex ; i++){

			letterTransformArray[i].localPosition = Vector3.Lerp(new Vector3(-450f,0f,0f),slingshotDragPoint.localPosition , ((i+1.0f)/(selectedLetterIndex+1.0f) ) );
			//letterTransformArray[i].gameObject.GetComponent<UILabel>().enabled = true;
		}

		letterTransformArray[selectedLetterIndex].localPosition = slingshotDragPoint.localPosition;

		for(int i  = 1; i < (letterTransformArray.Length-selectedLetterIndex) ; i++){
			letterTransformArray[selectedLetterIndex+i].localPosition = Vector3.Lerp(slingshotDragPoint.localPosition ,new Vector3(450f,0f,0f) , ((i)/((letterTransformArray.Length-selectedLetterIndex)+1.0f) ) );
		}
		
	}

	public void Reset(){

		MG_8_CustomTouchTest tempCTT;

		print("UICamera" + UICamera.selectedObject);
		if(UICamera.selectedObject != null){
			tempCTT = UICamera.selectedObject.GetComponent<MG_8_CustomTouchTest>();
			tempCTT.SetTimeUp();
			//UICamera.selectedObject = null;

			UICamera.selectedObject.SendMessage("OnDragEnd");
		}
		//UICamera.selectedObject.SendMessage("OnPress", false);

	/*	if(UICamera.currentTouch.dragged != null){
			print("RELSE");
			
		}
		*/

		
		
		slingshotDragPoint.localPosition = new Vector3(0f,0f,0f);
		selectedLetterIndex = 8;
		SetLines();

		SetupMinigame();
	}
}

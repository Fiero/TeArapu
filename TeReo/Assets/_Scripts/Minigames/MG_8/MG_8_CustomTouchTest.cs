using UnityEngine;
using System.Collections;

public class MG_8_CustomTouchTest : MonoBehaviour {

	private Minigame_8_Slingshot MG_8;
	public int letterIndex = 0;

	private UIRoot mRoot;

	private bool timeup = false;

	void Awake () {
		MG_8 = GameObject.Find("Minigame_8").GetComponent<Minigame_8_Slingshot>();
		mRoot = NGUITools.FindInParents<UIRoot>(this.transform.parent);

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnPress(bool isDown){
		/*if(timeup){
				UICamera.currentTouch.dragged = null;
	        	UICamera.currentTouch.pressed = null;
	    	}*/
	}


	void OnDragStart(){

		timeup = false;

	//	print("UICamera.currentTouch:" + UICamera.currentTouch.dragged.name);

		MG_8.SetSlingshotMidpoint(this.transform.localPosition, letterIndex);

	}

	void OnDrag (Vector2 delta)
	{
		if(!timeup){
			MG_8.MoveSlingshotMidpoint((Vector3)delta * mRoot.pixelSizeAdjustment);
		}

	}

	void OnDragEnd ()
	{
		//RELEASE
		if(!timeup){
			print("END");
			MG_8.SpringMidpoint();	
		}    
	}

	public void SetTimeUp(){
		timeup = true;
	}
}

using UnityEngine;
using System.Collections;

public class MG_3_ButtonEvent : MonoBehaviour {

	private Minigame_3_Bubbles minigame;

	public UILabel label;

	private string correctWord;

	void Awake(){
		minigame = GameObject.Find("Minigame_3").GetComponent<Minigame_3_Bubbles>();
		label = this.transform.GetChild(0).GetComponent<UILabel>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SendAnswer(){
		correctWord = minigame.GetCorrectWord();

		if(label.text == correctWord){
			//Close bubble
			this.transform.gameObject.SetActive(false);
			
			minigame.Answer(true);

			
		}

		else{
			minigame.Answer(false);
		}
	}

	public void ResetButton(){
		this.transform.gameObject.SetActive(true);
	}


}

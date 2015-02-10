using UnityEngine;
using System.Collections;

public class MG_6_DropArea : MonoBehaviour {

	public int correctLetterIndex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCorrectLetter(int letterIndex){
		correctLetterIndex = letterIndex;
	}

	public int GetCorrectLetter(){
		return correctLetterIndex;
	}
}

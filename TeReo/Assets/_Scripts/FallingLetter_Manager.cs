using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FallingLetter_Manager : MonoBehaviour {

	public List<GameObject> fallingLettersArray;

	public Color[] possibleColors;

	void Awake () {
		fallingLettersArray = new List<GameObject>();
		GetAllLetters ();
		SetColours ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetAllLetters()
	{
		foreach (Transform child in this.transform){
			print("Foreach loop: " + child);
			fallingLettersArray.Add(child.gameObject);
		}
		
	}

	void SetColours(){

		UILabel tempLabel;
		int currentColour = possibleColors.Length;

		foreach(GameObject letter in fallingLettersArray)
		{
			tempLabel = letter.GetComponent<UILabel>();
			tempLabel.color = possibleColors [currentColour%possibleColors.Length];

			currentColour++;
		}
	}
}

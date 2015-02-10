using UnityEngine;
using System.Collections;

public class Minigame_Template : MonoBehaviour {

	private Game g; // g.MinigameWin(); g.MinigameLose();
	private string[] alphabet = new string[] {"a","e","h","k","m","n","ng","o","p","r","t","u","w","wh"};

	void Awake(){
		g = GameObject.Find("Game").GetComponent<Game>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetupMinigame(){
	}

	void Reset(){
		SetupMinigame();
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Selects the group of minigames to play for each round.
//Once round is over should pick a new group.

public class Game_MinigameManager : MonoBehaviour {

	public int totalMinigames = 2;
	public int roundLength = 1;
	public List<int> minigameRoundList;

	public Transform minigameParent;
	public List<GameObject> minigamesArray;

	private int currentMinigameCount = 1;
	private int currentMinigame;

	public Minigame_1_Tiles MG_1;
	public Minigame_2_Weaving MG_2;
	public Minigame_3_Bubbles MG_3;
	public Minigame_4_Twitch MG_4;
	public Minigame_5_Rearrange MG_5;
	public Minigame_6_MissingLetter MG_6;
	public Minigame_7_Fishing MG_7;
	public Minigame_8_Slingshot MG_8;

	void Awake(){
		minigameRoundList = new List<int>();
		minigamesArray = new List<GameObject>();

		GetAllMinigames();

		EnableAllMinigames();

		GetMiniGameScripts();
		
		DisableAllMinigames();

		SetupMinigameRoundList();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetMiniGameScripts(){

		MG_1 = GameObject.Find("Minigame_1").GetComponent<Minigame_1_Tiles>();
		MG_2 = GameObject.Find("Minigame_2").GetComponent<Minigame_2_Weaving>();
		MG_3 = GameObject.Find("Minigame_3").GetComponent<Minigame_3_Bubbles>();
		MG_4 = GameObject.Find("Minigame_4").GetComponent<Minigame_4_Twitch>();
		MG_5 = GameObject.Find("Minigame_5").GetComponent<Minigame_5_Rearrange>();
		MG_6 = GameObject.Find("Minigame_6").GetComponent<Minigame_6_MissingLetter>();
		MG_7 = GameObject.Find("Minigame_7").GetComponent<Minigame_7_Fishing>();
		MG_8 = GameObject.Find("Minigame_8").GetComponent<Minigame_8_Slingshot>();
	}

	void GetAllMinigames()
    {
         foreach (Transform child in minigameParent){
             print("Foreach loop: " + child);
             minigamesArray.Add(child.gameObject);
        	}

    }

    public void DisableAllMinigames(){
    	foreach(GameObject minigame in minigamesArray)
        {
           	minigame.SetActive(false);
        }
    }

    public void EnableAllMinigames(){
    	foreach(GameObject minigame in minigamesArray)
        {
           	minigame.SetActive(true);
        }
    }

    void ActivateMinigame(int minigameNumber){
    	DisableAllMinigames();
    	minigamesArray[minigameNumber].SetActive(true);
    }



	//Clears the round list and adds the amount of minigames specified by roundLength.
	public void SetupMinigameRoundList(){
		currentMinigameCount=1;
		minigameRoundList.Clear();

		for(int i=0; i<roundLength; i++){
			AddMinigameToRoundList();

			//minigameRoundList.Add(5);
		}

		PrintRoundList();

		LoadMinigame();
		//StartMinigame ();

	}

	//Adds a unique minigame number to the roundlist.
	void AddMinigameToRoundList(){
		int tempGameNum = Random.Range(1,totalMinigames+1);

		Debug.Log("List contains: " + minigameRoundList.Contains(tempGameNum));
		if(!minigameRoundList.Contains(tempGameNum)){
			minigameRoundList.Add(tempGameNum);
		}

		else{
			AddMinigameToRoundList();
		}
	}

	//Prints what minigames will be played this round;
	void PrintRoundList(){
		string tempRoundList = "Game in Round: ";
		foreach(int minigameNum in minigameRoundList)
        	{
        		tempRoundList += minigameNum+", ";
        	}
        	print(tempRoundList);
	}

	public void NextGame(){
		//Reset the last Minigame;
		//ResetMinigame();

		currentMinigameCount++;

		if(currentMinigameCount>roundLength){
			SetupMinigameRoundList();
			
		}

		else{
			LoadMinigame();
		}
		
	}

	void LoadMinigame(){
		currentMinigame = minigameRoundList[currentMinigameCount-1];
		print("Game " + currentMinigameCount + "/" + roundLength + "    Load Minigame:" + currentMinigame);

		//ActivateMinigame(currentMinigame-1);
	}

	public int GetNextMinigameNumber(){
		return currentMinigame;
		}

	public void StartMinigame(){
		ActivateMinigame(currentMinigame-1);
		}

	public void ResetMinigame(){
		switch(currentMinigame)
		{
        case 1:
        	MG_1.Reset();
            break;
        case 2:
            break;
        case 3:
        	MG_3.Reset();
            break;
        case 4:
            break;
        case 5:
        	MG_5.Reset();
            break;
        case 6:
        	MG_6.Reset();
            break;
        case 7:
       		MG_7.Reset();
            break;
        case 8:
        	MG_8.Reset();
            break;
        default:
            break;
        }

	}


}

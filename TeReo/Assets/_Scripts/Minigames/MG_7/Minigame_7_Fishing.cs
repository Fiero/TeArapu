using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Minigame_7_Fishing : MonoBehaviour {

	public TextAsset wordlist;

	private Game g; // g.MinigameWin(); g.MinigameLose();
	private string[] alphaWordList = new string[] {"ara","aroha","hui","kata","kei","kutu","puku","tangi","taniwha","tapu","whare"};

	private List<string> fullWordList = new List<string>();
	public List<string> orderedWords = new List<string>();

	public GameObject[] hookArray;

	public UILabel[] UILabelArray;
	public MG_7_Target[] targetArray;

	public Transform fishParent;


	private int correctCount = 0;

	void Awake(){
		GetWordList();

		g = GameObject.Find("Game").GetComponent<Game>();
		fishParent = GameObject.Find("Container_Fish").GetComponent<Transform>();

	}

	// Use this for initialization
	void Start () {
		GetAllTargets();
		GetAllHooks();

		SetupMinigame();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetWordList(){
		//alphaWordList = File.ReadAllLines(wordlist.text);
		alphaWordList = wordlist.text.Split('\n'); //C#
	}

	void GetAllHooks(){
		hookArray = GameObject.FindGameObjectsWithTag("Hook");
	}

	void GetAllTargets(){
		targetArray =  fishParent.GetComponentsInChildren<MG_7_Target>();
	}

	void SetupMinigame(){
		int randomWord;

		//Add all of the words.
		for(int i = 0; i < alphaWordList.Length; i++){
			fullWordList.Add(alphaWordList[i]);
		}

		//Add a certain amount of chosen words, the order them alphabetically
		for(int j = 0; j< targetArray.Length; j++){
			randomWord = Random.Range(0,fullWordList.Count);

			orderedWords.Add(fullWordList[randomWord]);
			fullWordList.RemoveAt(randomWord);
		}

		orderedWords.Sort();

		SetUILabels();
	}

	void SetUILabels(){

		for(int i = 0; i<orderedWords.Count; i++){

	       		targetArray[i].SetWord( orderedWords[i] );
	       }
	}

	public int CheckAnswer(string word){

		int isCorrect;

		if(word == orderedWords[correctCount]){
			correctCount++;
			isCorrect = correctCount;
			print("MG7: CORRECT");

			if(correctCount >= 3){
				Reset();
				g.MinigameWin();
			}
		}

		else{
			isCorrect = 0;

			Reset();
			g.MinigameLose();
			print("MG7: FAIL");
		}



		return isCorrect;
	}

	public void Reset(){
		correctCount = 0;

		fullWordList.Clear();
		orderedWords.Clear();

		ResetHookPositions();
		ResetFishPositions();

		SetupMinigame();
	}

	void ResetHookPositions(){
		for(int i = 0; i<hookArray.Length ; i++){

				hookArray[i].SetActive(true);
	       		hookArray[i].transform.localPosition = new Vector3(-100f + (i*100) ,250f,0f);
	       }
	}

	void ResetFishPositions(){
		for(int i = 0; i<targetArray.Length ; i++){

	       		targetArray[i].ResetPosition();
	       }
	}
}

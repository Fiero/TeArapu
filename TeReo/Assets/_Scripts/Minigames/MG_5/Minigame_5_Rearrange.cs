using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Minigame_5_Rearrange : MonoBehaviour {

	public TextAsset wordlist;

	private Game g; // g.MinigameWin(); g.MinigameLose();
	private string[] alphaWordList = new string[] {"ara","aroha","hui","kata","kei","kutu","puku","tangi","taniwha","tapu","whare"};

	private List<string> fullWordList = new List<string>();
	public List<string> chosenWords = new List<string>();
	public List<string> orderedWords = new List<string>();
	private List<string> currentWords = new List<string>();


	public Transform UIParent;
	public UILabel[] UILabelArray;
	public MG_5_CustomDragDrop[] DragDropArray;
	public UIGrid grid;

	private int correctCount = 0;

	void Awake(){
		GetWordList();


		g = GameObject.Find("Game").GetComponent<Game>();

		grid = GameObject.Find("Grid").GetComponent<UIGrid>();


		GetAllUILabels();
		GetAllDragDropScripts();
	}

	// Use this for initialization
	void Start () {
		SetupMinigame();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetWordList(){
		//alphaWordList = File.ReadAllLines(wordlist.text);
		alphaWordList = wordlist.text.Split('\n'); //C#
	}

	void GetAllUILabels(){
		UILabelArray =  UIParent.GetComponentsInChildren<UILabel>();
	}

	void GetAllDragDropScripts(){
		DragDropArray = UIParent.GetComponentsInChildren<MG_5_CustomDragDrop>();
	}

	public void WaitThenCheckOrder(){
		//print("CHECK ORDER");
		StartCoroutine(WaitCheckOrder());
	}

	IEnumerator WaitCheckOrder() {
		//print(Time.time + "   COUNT:" + grid.GetChildList().Count);

		if(grid.GetChildList().Count==4){

			CheckOrder();
		}

		else{
			 yield return new WaitForSeconds(0.1f);
			 WaitThenCheckOrder();
		}
    } 

	public void CheckOrder(){
		UILabel label;

		foreach(Transform gridChild in grid.GetChildList())
        {

        	label = gridChild.GetChild(0).GetComponent<UILabel>();
            print (label.text);

            if(label.text == orderedWords[correctCount]){
            	//print("CORRECT");
            	correctCount++;

            	if(correctCount>=4){
            		//print("DONE");

            		Reset();
            		g.MinigameWin();
            	}
            }

            else{
            	correctCount = 0;
            	break;
            }
        }


	}

	void SetupMinigame(){

		int randomWord;

		for(int i = 0; i < alphaWordList.Length; i++){
			fullWordList.Add(alphaWordList[i]);
		}

		for(int j = 0; j<4; j++){
			randomWord = Random.Range(0,fullWordList.Count);


			chosenWords.Add(fullWordList[randomWord]);
			orderedWords.Add(fullWordList[randomWord]);
			fullWordList.RemoveAt(randomWord);
		}

		orderedWords.Sort();


		//shuffledcards = cards.OrderBy(a => Guid.NewGuid());
		bool equal = chosenWords.SequenceEqual(orderedWords);

		//Was the list already in order
		if(equal){
			chosenWords.Add(chosenWords[0]);
			chosenWords.RemoveAt(0);
		}

		SetUILabels();



	}

	void GetUILabelArrayOrder(){
		UILabel label;

		for(int i = 0; i<UILabelArray.Length; i++){
			label = grid.GetChild(i).GetChild(0).GetComponent<UILabel>();

			UILabelArray[i] = label;
		}
	}

	void SetUILabels(){
		for(int i = 0; i<chosenWords.Count; i++){
	       		UILabelArray[i].text = chosenWords[i];
	       }
	}

	void ResetDragDrops(){
		for(int i = 0; i<DragDropArray.Length; i++){
	       		DragDropArray[i].EndNow();
	       }
	}

	public void Reset(){

		GetUILabelArrayOrder();

		ResetDragDrops();

		correctCount = 0;

		fullWordList.Clear();
		chosenWords.Clear();
		orderedWords.Clear();

		SetupMinigame();
	}
}

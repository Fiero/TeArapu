using UnityEngine;
using System.Collections;

public class MG_5_CustomDragDrop : MonoBehaviour {

	//public UIGrid grid;
	private Minigame_5_Rearrange minigame;

	public UIDragDropItem_Custom_MG5 dragdrop_Script;


	void Awake () {
		minigame = GameObject.Find("Minigame_5").GetComponent<Minigame_5_Rearrange>();

		dragdrop_Script = this.GetComponent<UIDragDropItem_Custom_MG5>();

	//	grid = this.transform.parent.GetComponent<UIGrid>();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space")){
			print("PUSHED");
			EndNow();
		}
	
	}

	void OnDragEnd ()
	{
		print("ENDED");

		//minigame.CheckOrder();
		minigame.WaitThenCheckOrder();

	/*	print("GRID: " +  grid.GetChildList());

		foreach(Transform guy in grid.GetChildList())
        {
            print (guy.name);
        }

    */

	}

	public void EndNow(){
		dragdrop_Script.ForceDrop();
	}
}

using UnityEngine;
using System.Collections;

public class FallingLetter_Script : MonoBehaviour {

	public TweenPosition TweenP;
	public TweenRotation TweenR;
	public UILabel label;

	public float posXStartMin;
	public float posXStartMax;

	public float rotStartMin;
	public float rotStartMax;

	public float rotAmountMin;
	public float rotAmountMax;

	public float durationMin;
	public float durationMax;

	public float delayMin;
	public float delayMax;

	private float posXStart;
	private float rotStart;
	private float rotAmount;
	private float duration;
	private float delay;
	
	void Awake (){
		TweenP = this.GetComponent<TweenPosition>();
		TweenR = this.GetComponent<TweenRotation>();

		label = this.GetComponent<UILabel>();
	}

	// Use this for initialization
	void Start () {
		SetupTweens ();

		//SetupColour ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetupTweens () {

		posXStart = Random.Range (posXStartMin, posXStartMax);
		rotStart = Random.Range (rotStartMin, rotStartMax);
		rotAmount = Random.Range (rotAmountMin, rotAmountMax);
		duration = Random.Range (durationMin, durationMax);
		delay = Random.Range (delayMin, delayMax);

		TweenP.from = new Vector3 (this.transform.localPosition.x, 500, 0);
		TweenP.to = new Vector3 (this.transform.localPosition.x, -500, 0);

	//	TweenP.from = new Vector3 (posXStart, 500, 0);
	//	TweenP.to = new Vector3 (posXStart, -500, 0);

		TweenP.duration = duration;
		TweenP.delay = delay;

		TweenR.from = new Vector3 (0, 0, rotStart);
		TweenR.to = new Vector3 (0, 0, (rotStart+rotAmount));

		TweenR.duration = duration;
		TweenR.delay = delay;

		TweenP.Play ();
		TweenR.Play ();
	}

}

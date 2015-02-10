using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MG_2_CustomSlider : MonoBehaviour {

	public List<EventDelegate> onRelease = new List<EventDelegate>();

	private UISlider slider;
	private float targetValue;

	public float sliderReleaseArea = 0.0f;
	private bool tweenSlider = false;

	// Use this for initialization
	void Start () {
		slider = this.transform.parent.gameObject.GetComponent<UISlider>();

		targetValue = slider.value;
	}
	
	// Update is called once per frame
	void Update () {

		if(tweenSlider){
			slider.sliderValue = Mathf.Lerp(slider.sliderValue, targetValue, Time.deltaTime * 8f);

			if( Mathf.Abs(slider.sliderValue - targetValue) <= 0.01){
				slider.sliderValue = targetValue;
				tweenSlider = false;
				print("TSF");
			}
		}
	}

	private void OnPress(bool isDown)
 	{
	     if(isDown)
	     {
	     	tweenSlider = false;
	     	//print("Pushed");
	     }
	     
	     if(!isDown)
	     {
	     	print("TFT");
	     	

	     	tweenSlider = true;

	     	if(slider.sliderValue > sliderReleaseArea){
	     		targetValue = 1.0f;
	     		EventDelegate.Execute(onRelease);
	     	}

	     	else{
	     		targetValue = 0.0f;
	     	}
	     	
	     	//print("Released");
	     }
 	}

 	public void ResetSlider(){
 		slider.sliderValue = 0.0f;
 		tweenSlider = false;
 	} 
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class App : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------------------------------
	public	String						GameTitle = "A Cool Name";
	public	bool						DisplayedLogo = false;

	public bool MusicOn = true;
	public bool SFXOn = true;
	public SaveLoad_Script SLS;


	//-------------------------------------------------------------------------------------------------------------------------
	private string						CurrentScene;

	//-------------------------------------------------------------------------------------------------------------------------
	public static string				Scene_MainMenu	= "MainMenu";
	public static string				Scene_Gameplay	= "Gameplay";

	//-------------------------------------------------------------------------------------------------------------------------
	static		App						Singleton = null;

	//-------------------------------------------------------------------------------------------------------------------------
	public	static App Instance
	{
		get	{return Singleton;}
	}

	//-------------------------------------------------------------------------------------------------------------------------
	void Awake()
	{
		Debug.Log("App: Awake.");

		if ( Singleton != null )
		{
			Debug.LogError("Multiple App Singletons exist!");
		}

		Singleton = this;

		SLS = this.GetComponent<SaveLoad_Script>();

		// Are we in the Application Scene?
		if ( Application.loadedLevelName == "Application" )
		{
			// Make sure this object persists between scene loads.
			DontDestroyOnLoad(gameObject);

			LoadMainMenu();
		}
	}

	//-------------------------------------------------------------------------------------------------------------------------
	void Start ()
	{
	}
	
	//-------------------------------------------------------------------------------------------------------------------------
	void Update ()
	{
		if ( Input.GetKeyDown(KeyCode.Escape) )
		{
			Quit();
		}
	}

	//-------------------------------------------------------------------------------------------------------------------------
	public void Quit()
	{
		Debug.Log("App: Terminating.");

		Application.Quit();
	}

	//-------------------------------------------------------------------------------------------------------------------------
	public void LoadMainMenu()
	{
		Debug.Log("App: Loading MainMenu.");
		LoadLevel( Scene_MainMenu );
	}

	//-------------------------------------------------------------------------------------------------------------------------
	public void LoadGameplay()
	{
		Debug.Log("App: Loading Gameplay.");
		LoadLevel( Scene_Gameplay );
	}

	//-------------------------------------------------------------------------------------------------------------------------
	public void LoadLevel( string level )
	{
		Debug.Log("App: Loading Level, " + level);

		CurrentScene = level;

		Application.LoadLevel( CurrentScene );
	}

	//-------------------------------------------------------------------------------------------------------------------------
	public void ResetLevel()
	{
		Debug.Log("App: Restarting Level, " + CurrentScene );

		Application.LoadLevel( CurrentScene );
	}

	public void ToggleMusic(bool on){
		MusicOn = on;
	}

	public void ToggleSFX(bool on){
		SFXOn = on;
	}

	public void SaveHighScore(int highScore){
		SLS.SaveInt("highscore", highScore);
	}

	public int LoadHighScore(){
		Debug.Log("HIGHSCORE - LOAD");
		return SLS.LoadInt("highscore");
	}

	public void ClearHighScore(){
		Debug.Log("HIGHSCORE - CLEAR");
		SLS.SaveInt("highscore", 0);
	}

}


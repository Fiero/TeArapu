using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//[RequireComponent(typeof(ScreenFade))]

public class MainMenu : MonoBehaviour
{
	//-------------------------------------------------------------------------------------------------------------------------
	enum EMainMenuState
	{
		Startup,
		DisplayLogo,
		Menu,
		Settings,
	};

	//-------------------------------------------------------------------------------------------------------------------------
	public GameObject			AppPrefab;

	//-------------------------------------------------------------------------------------------------------------------------
	public GameObject			Asset_Logo;

	//-------------------------------------------------------------------------------------------------------------------------
	private EMainMenuState		StateId;
	private float				LogoDisplayTimer;

	public GameObject Screen_Settings;
	public UIToggle UI_MusicToggle;
	public UIToggle UI_SFXToggle;

	//-------------------------------------------------------------------------------------------------------------------------
	void Start ()
	{
		Debug.Log("MainMenu: Starting.");
		SettingsScreenHide();

		// Is the controlling App already in existence?
		if ( GameObject.Find("App") == null )
		{
			Debug.Log("Creating temporary App");

			Instantiate( AppPrefab );
		}

		StateId = EMainMenuState.Startup;
	}

	//-------------------------------------------------------------------------------------------------------------------------
	void Update ()
	{
		switch ( StateId )
		{
			case EMainMenuState.Startup:
				Debug.Log("MainMenu: State: Startup.");

				// Here we would do any menu preparation work.

				// Has the logo been displayed already?
				if (App.Instance.DisplayedLogo)
					// Move to the next state.
					StateId = EMainMenuState.Menu;
				else
				{
					App.Instance.DisplayedLogo = true;
					StateId = EMainMenuState.DisplayLogo;
					LogoDisplayTimer = 1.0f;
					ToggleLogo(true);
				}
				break;

			case EMainMenuState.DisplayLogo:
				LogoDisplayTimer-= Time.deltaTime;

				if ( LogoDisplayTimer < 0.0f ){
					StateId = EMainMenuState.Menu;
					ToggleLogo(false);
					}
				break;

			case EMainMenuState.Menu:
				break;

			case EMainMenuState.Settings:
				break;

			default:
				Debug.LogError("Really shouldn't be here... illegal state id set.");

				// Auto recover.
				StateId = EMainMenuState.Startup;
				break;
		}
	}

	//-------------------------------------------------------------------------------------------------------------------------

	void ToggleLogo( bool show){
		Asset_Logo.SetActive(show);
	}

	void SettingsScreenShow(){
		Screen_Settings.SetActive(true);
	}

	void SettingsScreenHide(){
		Screen_Settings.SetActive(false);
	}

	public void MusicToggle(){
		Debug.Log("MUSIC IS:" + UI_MusicToggle.isChecked);
	}

	public void SFXToggle(){
		Debug.Log("SFX IS:" + UI_SFXToggle.isChecked);
	}

	public void ResetProgress(){
		Debug.Log("CLEAR PLAYER PROGRESS");
		App.Instance.ClearHighScore();
	}


	public void StartGame(){
		Debug.Log("MainMenu: Start Game Selected.");
		App.Instance.LoadLevel( App.Scene_Gameplay );
		App.Instance.LoadGameplay();

	}

	public void ToggleSettings(){
		
		switch ( StateId )
		{
			case EMainMenuState.Menu:
				Debug.Log("MainMenu: Toggle to Settings");
				SettingsScreenShow();
				StateId = EMainMenuState.Settings;
			break;

			case EMainMenuState.Settings:
				Debug.Log("MainMenu: Toggle from Settings");
				SettingsScreenHide();
				StateId = EMainMenuState.Menu;
			break;
		}

	}


}

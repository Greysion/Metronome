﻿/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			AudioManager.cs
   Version:			0.1.0
   Description: 	For managing all audio components within the game. All audio should be called from this script.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[SerializeField] private EventInstance mainTheme;
	[SerializeField] private ParameterInstance mainThemeHealth;

	private FMOD.DSP fft;

	[SerializeField] private float startingSpeed = 0.8f;
	[SerializeField] private float currentSpeed = 0f;
	[SerializeField] private float speedStep = 0.1f;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {

		AwakeMusic();

	}

	private void AwakeMusic() {

		mainTheme = RuntimeManager.CreateInstance("event:/Soundtrack");
		mainTheme.getParameter("health", out mainThemeHealth);

	}

	private void Start() {

		BeginMusic();
		SpeedReset();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Analyser
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Call function from beats.
	private void Update() {
		
		

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Music Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Initialise the music.
	public void BeginMusic() => mainTheme.start();
	public void StopMusic() => mainTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

	// Parameteres controlling the music.
	public void HealthParam(float health) => mainThemeHealth.setValue(health);
	public void SpeedReset() => mainTheme.setPitch(startingSpeed);
	public void SpeedIncrease(bool increase) => mainTheme.setPitch(increase ? currentSpeed += speedStep : currentSpeed -= speedStep);
	
	/* ----------------------------------------------------------------------------- */

}

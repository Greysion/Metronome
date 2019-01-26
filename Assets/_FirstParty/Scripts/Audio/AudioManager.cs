/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
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

	[SerializeField] EventInstance mainTheme;
	[SerializeField] ParameterInstance mainThemeHealth;

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

	/* ----------------------------------------------------------------------------- */

}

/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			AudioManager.cs
   Version:			0.1.0
   Description: 	For managing all audio components within the game. All audio should be called from this script.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	EventInstance mainTheme;
	ParameterInstance mainThemeHealth;

	[SerializeField] public float startingSpeed = 0.8f;
	[SerializeField] public float speedStep = 0.05f;

	[SerializeField] private float currentSpeed = 0f;

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

	/* ----------------------------------------------------------------------------- */

	private void Start() {

		SpeedReset();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Music Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Initialise the music.
	public void BeginMusic() => mainTheme.start();
	public void StopMusic() => mainTheme.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

	// Parameteres controlling the music.
	public void SpeedReset() => mainTheme.setPitch(startingSpeed);
	public void SpeedIncrease(bool increase) => mainTheme.setPitch(increase ? currentSpeed += speedStep : currentSpeed -= speedStep);

	public void DamagedFilter(float health) => mainThemeHealth.setValue(health);

	/* ----------------------------------------------------------------------------- */

	public void HitSFX() => RuntimeManager.PlayOneShot("event:/Hit");

	/* ----------------------------------------------------------------------------- */

}

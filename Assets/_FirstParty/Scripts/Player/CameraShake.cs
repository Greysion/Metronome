/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CameraShake.cs
   Version:			0.1.0
   Description: 	Simple Camera Shake
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class CameraShake : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[SerializeField] private Transform cameraAnchor;

	[Space]
	public bool isRumbling;

	[SerializeField] private float currentRumble;
	[SerializeField] private float maxRumble;

	[Space]
	[SerializeField] private float rumbleLerpSpeed;
	[SerializeField] private float cameraSpeed;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {



	}

	/* ----------------------------------------------------------------------------- */

	private void Start() {



	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Camera Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void LateUpdate() {

		Vector3 desiredPosition = cameraAnchor.position;

		// Lerp into, and out of, the rumble effect for added smoothness.
		if (isRumbling)
			currentRumble = Mathf.Lerp(currentRumble, maxRumble, Time.deltaTime * rumbleLerpSpeed * 3f);

		else if (currentRumble > 0)
			currentRumble = Mathf.Lerp(currentRumble, 0f, Time.deltaTime * rumbleLerpSpeed);

		// If we're at a non-zero rumble, apply the shake to the camera's trajectory.
		if (currentRumble != 0)
			desiredPosition += new Vector3(0.35f - Mathf.PerlinNoise(Random.insideUnitCircle.x, Random.insideUnitCircle.y), 0.35f - Mathf.PerlinNoise(Random.insideUnitCircle.x, Random.insideUnitCircle.y), 0f) * currentRumble;

		// Lerp to the final position.
		transform.position = Vector3.Lerp(transform.position, new Vector3(desiredPosition.x, desiredPosition.y, transform.position.z), Time.deltaTime * cameraSpeed);

	}

	/* ----------------------------------------------------------------------------- */

}
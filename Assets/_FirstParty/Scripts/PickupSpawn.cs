using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour
{

	Camera mainCamera;

	[SerializeField]
	GameObject music;

	[SerializeField]
	float timeToSpawn = 20f;

	float time;

	// Start is called before the first frame update
	void Start()
	{
		mainCamera = Camera.main;
		Invoke("SpawnMusic", timeToSpawn);
		Player.LevelGained += GainLevel;
		Player.LevelLost += LoseLevel;
	}

	Vector3 SpawnLocation()
	{
		float height = mainCamera.orthographicSize - 1;
		float width = mainCamera.aspect * height;
		Vector3 spawnPoint = new Vector3
		{
			x = Random.Range(-width, width),
			y = Random.Range(-height, height),
			z = 0
		};

		return spawnPoint;
	}

	void SpawnMusic()
	{
		Instantiate(music, SpawnLocation(), Quaternion.identity, transform);
		time = Time.time;
	}

	void GainLevel()
	{
		Invoke("SpawnMusic", timeToSpawn);
	}

	void LoseLevel()
	{
		if (IsInvoking("SpawnMusic"))
		{
			CancelInvoke("SpawnMusic");
			Invoke("SpawnMusic", (timeToSpawn - (Time.time - time)) / 2);
		}
	}
    
}

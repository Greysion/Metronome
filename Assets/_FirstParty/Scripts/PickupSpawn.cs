using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour
{

	Camera mainCamera;

	[SerializeField]
	GameObject music;

	// Start is called before the first frame update
	void Start()
	{
		mainCamera = Camera.main;
		InvokeRepeating("SpawnMusic", 20, 20);
	}

	Vector3 SpawnLocation()
	{
		float maxX = mainCamera.rect.xMax;
		float minX = mainCamera.rect.xMin;
		float maxY = mainCamera.rect.yMax;
		float minY = mainCamera.rect.yMin;
		Vector3 spawnPoint = new Vector3
		{
			x = Random.Range(minX, maxX),
			y = Random.Range(minY, maxY),
			z = 0
		};

		return spawnPoint;
	}

	void SpawnMusic()
	{
		Instantiate(music, SpawnLocation(), Quaternion.identity, transform);
	}

    
}

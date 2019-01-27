using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	float speed = 20;

	Camera mainCamera;

	int health = 5;

	public int pickupLevel;

	public bool invulnerable;

	public static Action LevelGained, LevelLost;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	// Update is called once per frame
	void Update()
    {
		MovePlayer();
    }

	void MovePlayer()
	{
		Vector3 mouseMovement = Input.mousePosition;
		mouseMovement.z = 10;
		mouseMovement = mainCamera.ScreenToWorldPoint(mouseMovement);

		float height = mainCamera.orthographicSize;
		float width = mainCamera.aspect * height;

		mouseMovement.x = Mathf.Clamp(mouseMovement.x, -width, width);
		mouseMovement.y = Mathf.Clamp(mouseMovement.y, -height, height);

		transform.position = Vector3.LerpUnclamped(transform.position, mouseMovement, Time.deltaTime * speed);
	}

	private void OnCollisionEnter(Collision collision)
	{
		string tag = collision.transform.tag;
		if(tag == "Bullet")
		{
			TakeDamage();
			Destroy(collision.gameObject);
		}
		else if (tag == "Pickup")
		{
			GainLevel();
			Destroy(collision.gameObject);
		}
	}

	void TakeDamage()
	{
		if (invulnerable)
			return;
		health--;
		if(health <= 0)
		{
			LoseLevel();
		}
		invulnerable = true;
		Invoke("InvulnerabilityOver", 0.8f);
	}

	void InvulnerabilityOver()
	{
		invulnerable = false;
	}

	void LoseLevel()
	{

		print("Level Lost");
		LevelLost();
		pickupLevel--;
		if(pickupLevel <= 0)
		{
			LoseGame();
		}
		health = 5;
	}

	void GainLevel()
	{
		print("Level Gained");
		LevelGained();
		pickupLevel++;
		health = 5;
		if(pickupLevel >= 7)
		{
			WinGame();
		}
	}

	void LoseGame()
	{
		print("Game Lost");
	}

	void WinGame()
	{
		print("Game Won");
	}

}

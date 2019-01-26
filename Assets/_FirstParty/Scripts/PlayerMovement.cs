using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	float speed = 20;

	Camera mainCamera;

	int health = 5;

	public int pickupLevel;

	public bool invulnerable;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	// Update is called once per frame
	void Update()
    {
		Vector3 mouseMovement = Input.mousePosition;
		mouseMovement.z = 10;
		mouseMovement = mainCamera.ScreenToWorldPoint(mouseMovement);
		transform.position = Vector3.MoveTowards(transform.position, mouseMovement, Time.deltaTime * speed);
		//transform.position = mouseMovement;
    }

	private void OnCollisionEnter(Collision collision)
	{
		string tag = collision.transform.tag;
		if(tag == "Bullet")
		{
			TakeDamage();
			Destroy(collision.gameObject);
		}
		else if (tag == "PickUp")
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
		pickupLevel--;
		if(pickupLevel <= 0)
		{
			LoseGame();
		}
		health = 5;
	}

	void GainLevel()
	{
		pickupLevel++;
		health = 5;
	}

	void LoseGame()
	{
		print("Game Lost");
	}

}

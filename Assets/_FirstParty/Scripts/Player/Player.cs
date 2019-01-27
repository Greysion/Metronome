using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Player : MonoBehaviour
{
	[SerializeField]
	float speed = 20;

	Camera mainCamera;

	[SerializeField]
	int maxHealth = 5;

	int health;

	public int pickupLevel;

	public bool invulnerable;

	public static Action<int> LevelGained, LevelLost;

	float hitWeight = 0, levelWeight = 0;

	PostProcessVolume ppHit, ppLevel;

	[SerializeField]
	float[] intensityLevels;

	private void Start()
	{
		mainCamera = Camera.main;
		health = maxHealth;
		ppHit = mainCamera.transform.Find("ppDamage").GetComponent<PostProcessVolume>();
		ppLevel = mainCamera.transform.Find("pplevelMod").GetComponent<PostProcessVolume>();
	}

	// Update is called once per frame
	void Update()
    {
		MovePlayer();
		InvulnerableVisuals();
		LevelVisuals();
    }

	void InvulnerableVisuals()
	{
		if (invulnerable)
		{
			if (!(hitWeight >= 1))
			{
				hitWeight += Time.deltaTime * 5;
				ppHit.weight = Mathf.Lerp(0, 1, hitWeight);
			}

		}
		else
		{
			if (!(hitWeight <= 0))
			{
				hitWeight -= Time.deltaTime * 5;
				ppHit.weight = Mathf.Lerp(1, 0, hitWeight);
			}
		}
	}

	void LevelVisuals()
	{
		float difference = levelWeight - intensityLevels[pickupLevel];
		if (difference >= 0.005f)
		{
			levelWeight = Mathf.Lerp(levelWeight, intensityLevels[pickupLevel], Time.deltaTime * 10);
		}
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
		Invoke("InvulnerabilityOver", 1);
	}

	void InvulnerabilityOver()
	{
		invulnerable = false;
	}

	void LoseLevel()
	{

		print("Level Lost");
		LevelLost(pickupLevel);
		pickupLevel--;
		if(pickupLevel <= 0)
		{
			LoseGame();
		}
		health = maxHealth;
	}

	void GainLevel()
	{
		print("Level Gained");
		LevelGained(pickupLevel);
		pickupLevel++;
		health = maxHealth;
		if(pickupLevel >= 4)
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

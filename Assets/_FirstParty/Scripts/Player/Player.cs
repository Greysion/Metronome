using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Player : MonoBehaviour
{
	[SerializeField]
	float speed = 20;

	Camera mainCamera;

	CameraShake shake;

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

	AudioManager audioManager;

	private void Start()
	{
		audioManager = FindObjectOfType<AudioManager>();
		mainCamera = Camera.main;
		shake = mainCamera.GetComponent<CameraShake>();
		health = maxHealth;
		ppHit = mainCamera.transform.Find("ppDamage").GetComponent<PostProcessVolume>();
		ppLevel = mainCamera.transform.Find("ppLevelMod").GetComponent<PostProcessVolume>();
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
			shake.isRumbling = true;
			if ((hitWeight != 1))
			{
				hitWeight = Mathf.Lerp(hitWeight, 1, Time.deltaTime * 10);
				ppHit.weight = hitWeight;
			}

		}
		else
		{
			shake.isRumbling = false;
			if ((hitWeight != 0))
			{
				hitWeight = Mathf.Lerp(hitWeight, 0, Time.deltaTime * 10);
				ppHit.weight = hitWeight;
			}
		}
	}

	void LevelVisuals()
	{
		float difference = levelWeight - intensityLevels[pickupLevel];
		if (Mathf.Abs(difference) >= 0.005f)
		{
			levelWeight = Mathf.Lerp(levelWeight, intensityLevels[pickupLevel], Time.deltaTime * 10);
			ppLevel.weight = levelWeight;
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
			if (invulnerable)
				return;
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
		audioManager.HitSFX();
		audioManager.DamagedFilter(10f);
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
		audioManager.DamagedFilter(100f);
		invulnerable = false;
	}

	void LoseLevel()
	{

		print("Level Lost");
		LevelLost(pickupLevel);
		pickupLevel--;
		if(pickupLevel < 0)
		{
			LoseGame();
			pickupLevel = 0;
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
			pickupLevel = 4;
		}
	}

	void LoseGame()
	{
		FindObjectOfType<GameManager>().GameOver();
		audioManager.StopMusic();
	}

	void WinGame()
	{
		FindObjectOfType<GameManager>().GameOver();
		audioManager.StopMusic();
	}

}

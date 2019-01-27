using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{

	[SerializeField]
	GameObject bullet;

	[SerializeField]
	Transform bulletContainer;

	float maxAngle = 90;

	int maxPositions = 16;

	float bpm = 75, lowestBeatTime = 16;

	[SerializeField]
	float bulletSpeed = 5f;

	float secondPerBeat;

	int currentWavePosition;

	float anglePerPosition;

	Transform player;

	enum SpreadType { Pulse, Wave, Random, Directed, NumberOfTypes }

	SpreadType currentSpread = SpreadType.Wave, secondSpread = SpreadType.Pulse;

	[SerializeField]
	int patternLimit = 16;

	SpreadType[] attackPattern, secondaryPattern;

	string attackSpreadString = "01230103", easyAttackMode = "11021301", mediumAttackMode = "20303202", hardAttackMode = "01230123";

	float timer;

	int attacks = 0, currentAttack;

	int waveDirection = 1;

	AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
		SetSpreadType(0);
		anglePerPosition = (maxAngle * 2) / maxPositions;
		SetupBPM();
		player = FindObjectOfType<Player>().transform;
		audioManager = FindObjectOfType<AudioManager>();
		Player.LevelGained += GainLevel;
		Player.LevelLost += LoseLevel;
    }

	void SetupBPM()
	{
		secondPerBeat = 60 / bpm;
	}

	public void StartBeat()
	{
		Beat();
		audioManager.BeginMusic();
	}

	void Beat()
	{
		if (!(secondaryPattern.Length > 0))
		{
			Attack(currentSpread);
		}
		else
		{
			Attack(currentSpread, secondSpread);
		}

		if(attacks >= patternLimit)
		{
			attacks = 0;
			currentAttack = (currentAttack + 1) % attackPattern.Length;
			currentSpread = attackPattern[currentAttack];
			if (secondaryPattern.Length != 0) secondSpread = secondaryPattern[currentAttack]; 
		}

		Invoke("Beat", secondPerBeat / lowestBeatTime);
	}

	void SetSpreadType(int currentMusicLevel)
	{
		switch(currentMusicLevel)
		{
			case 0:
				attackPattern = ConvertSpreadString(easyAttackMode);
				secondaryPattern = new SpreadType[0];
				break;
			case 1:
				attackPattern = ConvertSpreadString(mediumAttackMode);
				secondaryPattern = new SpreadType[0];
				break;
			case 2:
				attackPattern = ConvertSpreadString(hardAttackMode);
				secondaryPattern = new SpreadType[0];
				break;
			case 3:
				attackPattern = ConvertSpreadString(easyAttackMode);
				secondaryPattern = ConvertSpreadString(hardAttackMode);
				break;
			default:
				break;

		}
	}

	void Attack(SpreadType spread)
	{
		attacks++;
		switch(spread)
		{
			case SpreadType.Pulse:
				if ((attacks / 2) % 2 == 0)
					break;
				for (int i = 0; i <= maxPositions; i++)
				{
					Fire(SelectAngle(i));
				}
				break;

			case SpreadType.Wave:
				Fire(SelectAngle(currentWavePosition + 1));
				currentWavePosition = (currentWavePosition + (1 * waveDirection));
				if (currentWavePosition >= maxPositions || currentWavePosition <= 0)
					waveDirection *= -1;
				break;

			case SpreadType.Random:
				Fire(SelectAngle(Random.Range(0, maxPositions + 1)));
				break;
			case SpreadType.Directed:
				if (attacks % 4 != 0)
					break;
				Fire(DirectionToTarget(player));
				break;

		}
	}

	void Attack(SpreadType spread1, SpreadType spread2)
	{
		Attack(spread1);
		Attack(spread2);
	}

	void Fire(float angle)
	{
		Quaternion newAngle = Quaternion.Euler(0, 0, angle);
		GameObject spawned = Instantiate(bullet, transform.position, newAngle, bulletContainer);
		spawned.GetComponent<Bullet>().SetSpeed(bulletSpeed);
	}

	void Fire(Vector3 direction)
	{
		GameObject spawned = Instantiate(bullet, transform.position, Quaternion.identity, bulletContainer);
		spawned.transform.up = direction;
		spawned.GetComponent<Bullet>().SetSpeed(bulletSpeed);
	}

	Vector3 DirectionToTarget(Transform target)
	{
		Vector3 direction = target.position - transform.position;
		return direction.normalized;
	}

	float SelectAngle(int position)
	{
		float angle;
		if (position >= maxPositions)
		{
			angle = maxAngle;
		}
		else
		{
			angle = -maxAngle + (position * anglePerPosition);
		}
		return angle;
	}

	SpreadType[] ConvertSpreadString(string spread)
	{
		SpreadType[] spreadList = new SpreadType[spread.Length];
		int i = 0;
		foreach(char letter in spread)
		{
			
			if(char.IsNumber(letter))
			{
				int num = letter - '0';
				if(num >= (int)SpreadType.NumberOfTypes)
				{
					spreadList[i] = SpreadType.Random;
				}
				else spreadList[i] = (SpreadType)num;
			} else
			{
				spreadList[i] = SpreadType.Random;
			}
			i++;
		}
		return spreadList;
	}

	SpreadType[] ConvertSpreadStringInverse(string spread)
	{
		char[] letters = spread.ToCharArray();
		string newSpread = "";
		for(int i = letters.Length; i >=0; i--)
		{
			newSpread += letters[i];
		}
		return ConvertSpreadString(newSpread);
	}

	void GainLevel(int currentLevel)
	{
		audioManager.SpeedIncrease(true);
		SetSpreadType(currentLevel);
		float speedIncrease = audioManager.speedStep;
		bpm *= 1 + speedIncrease;
		bulletSpeed *= 1.3f;
		SetupBPM();
	}

	void LoseLevel(int currentLevel)
	{
		audioManager.SpeedIncrease(false);
		SetSpreadType(currentLevel);
		float speedIncrease = audioManager.speedStep;
		bpm /= 1 + speedIncrease;
		bulletSpeed /= 1.5f;
		SetupBPM();
	}

}

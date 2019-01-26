using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{

	[SerializeField]
	GameObject bullet;

	[SerializeField]
	float maxAngle = 45, distance;

	[SerializeField]
	int maxPositions = 10;

	[SerializeField]
	float bpm = 120, lowestBeatTime = 16;

	float secondPerBeat;

	int currentWavePosition;

	float anglePerPosition;

	Transform player;

	enum SpreadType { Pulse, Wave, Random, Directed }

	[SerializeField]
	SpreadType currentSpread = SpreadType.Wave;

	[SerializeField]
	int patternLimit = 16;

	[SerializeField]
	SpreadType[] attackPattern;

	float timer;

	int beats = 0, attacks = 0, currentAttack;

    // Start is called before the first frame update
    void Start()
    {
		anglePerPosition = (maxAngle * 2) / maxPositions;
		secondPerBeat = 60 / bpm;
		InvokeRepeating("Beat", 0, secondPerBeat / lowestBeatTime);
		player = FindObjectOfType<PlayerMovement>().transform;
    }

	void Beat()
	{
		Attack(currentSpread);
		beats++;
		if(attacks >= patternLimit)
		{
			attacks = 0;
			currentAttack = (currentAttack + 1) % attackPattern.Length;
			currentSpread = attackPattern[currentAttack];
		}
	}

	void Attack(SpreadType spread)
	{
		attacks++;
		switch(spread)
		{
			case SpreadType.Pulse:
				if (beats % 16 != 0)
					break;
				for (int i = 0; i <= maxPositions; i++)
				{
					Fire(SelectAngle(i));
				}
				break;

			case SpreadType.Wave:
				Fire(SelectAngle(currentWavePosition));
				currentWavePosition = (currentWavePosition + 1) % (maxPositions + 1);
				break;

			case SpreadType.Random:
				Fire(SelectAngle(Random.Range(0, maxPositions + 1)));
				break;
			case SpreadType.Directed:
				Fire(DirectionToTarget(player));
				break;

		}
	}

	void Fire(float angle)
	{
		Quaternion newAngle = Quaternion.Euler(0, 0, angle);
		Instantiate(bullet, transform.position, newAngle);
	}

	void Fire(Vector3 direction)
	{
		GameObject spawned = Instantiate(bullet, transform.position, Quaternion.identity);
		spawned.transform.up = direction;
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
}

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

	int currentWavePosition;

	float anglePerPosition;

	enum SpreadType { Pulse, Wave, Random }

	[SerializeField]
	SpreadType currentSpread = SpreadType.Wave;

	Timer timer;

    // Start is called before the first frame update
    void Start()
    {
		anglePerPosition = (maxAngle * 2) / maxPositions;
		timer = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.quarterBeat)
		{
			Attack(currentSpread);
		}
    }

	

	void Attack(SpreadType spread)
	{
		switch(spread)
		{
			case SpreadType.Pulse:
				if (timer.beat)
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

		}
	}

	void Fire(float angle)
	{
		Quaternion newAngle = Quaternion.Euler(0, 0, angle);
		Instantiate(bullet, transform.position, newAngle);
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

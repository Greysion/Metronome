using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

	[SerializeField]
	float bpm = 60;
	float spb;
	float noteTime;

	int numQuarterBeat;

	public float currentTimer;

	public bool beat, quarterBeat;

    // Start is called before the first frame update
    void Start()
    {
		currentTimer = 0;
		spb = 30 / bpm;
		//noteTime = bps * 4;
    }

    // Update is called once per frame
    void Update()
    {
		currentTimer += Time.deltaTime;

		if (currentTimer >= spb / 4)
		{
			currentTimer -= spb;
			quarterBeat = true;
			numQuarterBeat++;
			if(numQuarterBeat >= 3)
			{
				numQuarterBeat = 0;
				beat = true;
			}
			else
			{
				beat = false;
			}
		}
		else
		{
			quarterBeat = false;
		}
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

	Transform player;

	[SerializeField]
	float maxDistance = 5;

	[SerializeField]
	float speed = 0.2f;

	private void Start()
	{
		player = FindObjectOfType<Player>().transform;
	}

	// Update is called once per frame
	void Update()
    {
		Vector3 pos = transform.position;
		float xPosition = Mathf.Lerp(pos.x, player.position.x, Time.deltaTime * speed);
		pos.x = Mathf.Clamp(xPosition, -maxDistance, maxDistance);
		transform.position = pos;
    }
}

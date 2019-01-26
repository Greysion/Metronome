using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

	[SerializeField]
	Transform player;

	[SerializeField]
	float maxDistance;

	[SerializeField]
	float speed;

    // Update is called once per frame
    void Update()
    {
		Vector3 pos = transform.position;
		float xPosition = Mathf.Lerp(pos.x, player.position.x, Time.deltaTime * speed);
		pos.x = Mathf.Clamp(xPosition, -maxDistance, maxDistance);
		transform.position = pos;
    }
}

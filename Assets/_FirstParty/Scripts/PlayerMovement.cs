using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	float sizeX, sizeY;

	[SerializeField]
	float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

		Vector3 mouseMovement = Input.mousePosition;
		mouseMovement.z = 10;
		mouseMovement = Camera.main.ScreenToWorldPoint(mouseMovement);
		transform.position = Vector3.Lerp(transform.position, mouseMovement, Time.deltaTime * speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	float speed;

	Camera mainCamera;

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
		transform.position = Vector3.Lerp(transform.position, mouseMovement, Time.deltaTime * speed);
    }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "Bullet")
		{
			TakeDamage();
			Destroy(collision.gameObject);
		}
	}

	void TakeDamage()
	{
		print("Ouch");
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
		Destroy(gameObject, 5f);
    }

	public void SetSpeed(float bulletSpeed)
	{
		speed = bulletSpeed;
	}

    // Update is called once per frame
    void Update()
    {
		Vector3 movement = transform.up * speed * Time.deltaTime;
		//transform.Translate(movement);
		transform.position += movement;
    }
}

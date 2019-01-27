using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	[SerializeField]
	float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
		Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 movement = transform.up * speed * Time.deltaTime;
		//transform.Translate(movement);
		transform.position += movement;
    }
}

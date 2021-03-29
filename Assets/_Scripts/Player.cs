using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private Transform facingTransform;

	private Rigidbody2D rigidBody;

	private float speed = 3.0f;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			facingTransform.localPosition = new Vector2(0, 1);
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			facingTransform.localPosition = new Vector2(-1, 0);
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			facingTransform.localPosition = new Vector2(0, -1);
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			facingTransform.localPosition = new Vector2(1, 0);
		}
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		rigidBody.MovePosition(transform.position + facingTransform.localPosition * Time.deltaTime * speed);
	}
}

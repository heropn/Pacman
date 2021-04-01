using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float speed = 1.0f;

	private Rigidbody2D rigidBody;

	public Vector2 currentDirection;

	private BoxCollider2D boxCollider;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		boxCollider.GetComponent<BoxCollider2D>();
		currentDirection = new Vector2(-1, 0);
		rigidBody.freezeRotation = true;
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var turningPoint = collision.gameObject.GetComponent<TurningPoint>();

		if (turningPoint != null)
		{
			int random = UnityEngine.Random.Range(0, turningPoint.directions.Count);
			currentDirection = turningPoint.directions[random];

			if (currentDirection.x == 0)
			{
				boxCollider.size = new Vector2(0.05f, 1);
			}
			else
			{
				boxCollider.size = new Vector2(1, 0.05f);
			}
		}
	}

	private void Move()
	{
		rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + currentDirection * Time.deltaTime * speed);
	}
}

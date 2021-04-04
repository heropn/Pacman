using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FinalObjective : MonoBehaviour
{
	public event Action OnPlayerEnter;

	[SerializeField]
	private float speed = 1.5f;

	private Rigidbody2D rigidBody;

	private Vector2 direction;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		direction = new Vector2(1, 0);
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PolygonCollider2D>() != null)
		{
			direction = new Vector2(direction.x * -1, 0);
			transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
		}
		else if (collision.GetComponent<Player>() != null)
		{
			OnPlayerEnter?.Invoke();
		}
	}

	private void Move()
	{
		rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + direction * Time.deltaTime * speed);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float normalSpeed = 1.5f;

	[SerializeField]
	private float speedWhenVulnarable = 0.5f;

	private float speed;

	private Rigidbody2D rigidBody;

	private Vector2 currentDirection;

	private BoxCollider2D boxCollider;

	public State currentState { get; private set; }

	public enum State
	{
		Vulnarable,
		Normal
	}

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
		currentDirection = new Vector2(-1, 0);
		rigidBody.freezeRotation = true;
		currentState = State.Normal;
		speed = normalSpeed;
	}

	private void FixedUpdate()
	{
		Move();
	}

	public void ChangeState(State state)
	{
		currentState = state;

		if (currentState == State.Vulnarable)
		{
			speed = speedWhenVulnarable;
			GetComponent<SpriteRenderer>().color = Color.blue;
		}
		else
		{
			speed = normalSpeed;
			GetComponent<SpriteRenderer>().color = Color.white;
		}
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
				boxCollider.size = new Vector2(1, 0.05f);
			}
			else
			{
				boxCollider.size = new Vector2(0.05f, 1);
			}
		}
	}

	private void Move()
	{
		rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + currentDirection * Time.deltaTime * speed);
	}

	public void Destroy()
	{
		Destroy(gameObject);
		//Do some other animation and stuff
	}
}

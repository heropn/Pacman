using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public event Action<Enemy> onPlayerCollidedWithEnemy;

	[SerializeField]
	private Transform facingTransform;

	[SerializeField]
	private float speed = 2.0f;

	private Animator animator;
	//Side parameter 0 - right, 1 - down, 2 - left, 3 - up

	private Rigidbody2D rigidBody;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		rigidBody.freezeRotation = true;
		animator.SetInteger("Side", 0);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			facingTransform.localPosition = new Vector2(0, 1);
			animator.SetInteger("Side", 3);
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			facingTransform.localPosition = new Vector2(-1, 0);
			animator.SetInteger("Side", 2);
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			facingTransform.localPosition = new Vector2(0, -1);
			animator.SetInteger("Side", 1);
		}
		else if (Input.GetKeyDown(KeyCode.D))
		{
			facingTransform.localPosition = new Vector2(1, 0);
			animator.SetInteger("Side", 0);
		}
	}

	private void FixedUpdate()
	{
		Move();
	}

	public void TeleportToLocation(Vector2 location)
	{
		transform.position = location;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var enemy = collision.GetComponent<Enemy>();

		if (enemy != null)
		{
			onPlayerCollidedWithEnemy?.Invoke(enemy);
		}
	}

	private void Move()
	{
		rigidBody.MovePosition(transform.position + facingTransform.localPosition * Time.deltaTime * speed);
	}
}

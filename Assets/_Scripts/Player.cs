using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField]
	private Transform facingTransform;

	[SerializeField]
	private List<Sprite> sprites;

	private Animator animator;
	//Side parameter 0 - right, 1 - down, 2 - left, 3 - up

	private Rigidbody2D rigidBody;

	private float speed = 3.0f;

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

	private void Move()
	{
		rigidBody.MovePosition(transform.position + facingTransform.localPosition * Time.deltaTime * speed);
	}
}

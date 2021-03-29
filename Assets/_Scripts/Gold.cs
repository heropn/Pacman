using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
	public event Action<Gold> onGoldCollected;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>() != null)
		{
			Debug.Log("Collided");
			onGoldCollected.Invoke(this);
		}
	}
}

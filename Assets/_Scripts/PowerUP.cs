using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
	public event Action<PowerUP> onPowerUpPicked;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>() != null)
		{
			onPowerUpPicked?.Invoke(this);
		}
	}
}

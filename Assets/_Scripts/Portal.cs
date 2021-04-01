using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	public event Action<Portal> onPortalEnter;

	[SerializeField]
	private Portal secondPortal;

	private ParticleSystem particles;

	private void Start()
	{
		particles = GetComponent<ParticleSystem>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<Player>() != null)
		{
			particles.Play();
			onPortalEnter?.Invoke(secondPortal);
		}
	}

	public IEnumerator DisableCollider(float timeInSeconds)
	{
		particles.Play();
		GetComponent<BoxCollider2D>().enabled = false;
		yield return new WaitForSeconds(timeInSeconds);
		GetComponent<BoxCollider2D>().enabled = true;
	}
}

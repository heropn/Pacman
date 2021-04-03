using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	public static Level Instance;

	[SerializeField]
	private BoxCollider2D finalGate;

	private void Awake()
	{
		Instance = this;
	}

	public void SetActiveFinalGate(bool isActive)
	{
		finalGate.gameObject.SetActive(false);
	}
}

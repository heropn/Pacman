using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
	private RectTransform blackEye;
	private RectTransform whiteEye;

	private Vector2 blackEyePosition;
	private Vector2 whiteEyePosition;

	private Vector2 distanceSquaredFromEyes;
	private Vector2 distanceSquaredFromMouse;

	private void Start()
	{
		blackEye = GetComponent<RectTransform>();
		whiteEye = GetComponentInChildren<RectTransform>();

		blackEyePosition = Camera.main.ScreenToWorldPoint(blackEye.position);
		whiteEyePosition = Camera.main.ScreenToWorldPoint(whiteEye.position);

		distanceSquaredFromEyes = new Vector2(Mathf.Pow((blackEyePosition.x - whiteEyePosition.x), 2), Mathf.Pow(blackEyePosition.y - whiteEyePosition.y, 2));
	}

	private void Update()
	{
		Vector2 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

		distanceSquaredFromMouse = new Vector2(Mathf.Pow((blackEyePosition.x - mousePosition.x), 2), Mathf.Pow(blackEyePosition.y - mousePosition.y, 2));
		
		if (mousePosition.x < blackEyePosition.x)
		{
			distanceSquaredFromMouse = new Vector2(-distanceSquaredFromMouse.x, distanceSquaredFromMouse.y);
		}

		if (mousePosition.y < blackEyePosition.y)
		{
			distanceSquaredFromMouse = new Vector2(distanceSquaredFromMouse.x, -distanceSquaredFromMouse.y);
		}



		Debug.Log(distanceSquaredFromMouse.normalized);
	}
}

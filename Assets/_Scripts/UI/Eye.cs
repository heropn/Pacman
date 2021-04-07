using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
	private RectTransform blackEye;

	[SerializeField]
	private RectTransform whiteEye;

	private Vector2 blackEyePosition;
	private Vector2 whiteEyePosition;

	private Vector2 vecFromEyes;

	private float distanceFromEyes;

	private void Start()
	{
		blackEye = GetComponent<RectTransform>();

		blackEyePosition = Camera.main.ScreenToWorldPoint(blackEye.position);
		whiteEyePosition = Camera.main.ScreenToWorldPoint(whiteEye.position);

		vecFromEyes = new Vector2(whiteEyePosition.x - blackEyePosition.x, whiteEyePosition.y - blackEyePosition.y);

		distanceFromEyes = vecFromEyes.magnitude;
	}

	private void Update()
	{
		Vector2 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

		Vector2 vectorFromMouse = new Vector2(mousePosition.x - blackEyePosition.x, mousePosition.y - blackEyePosition.y);

		vecFromEyes = new Vector2(vectorFromMouse.x * (distanceFromEyes / vectorFromMouse.magnitude), vectorFromMouse.y * (distanceFromEyes / vectorFromMouse.magnitude));

		Vector2 vecPos = Camera.main.WorldToScreenPoint(blackEyePosition + vecFromEyes);
		whiteEye.position = vecPos;
	}
}

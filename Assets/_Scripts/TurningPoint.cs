using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningPoint : MonoBehaviour
{
	public List<Vector2> directions { private set; get; }

	[SerializeField]
	private List<TurningPoint> turningPoints = new List<TurningPoint>();

	private void Start()
	{
		directions = new List<Vector2>();

		foreach (var tur in turningPoints)
		{
			var vec = new Vector2(tur.transform.position.x - transform.position.x, tur.transform.position.y - transform.position.y);
			vec.Normalize();
			directions.Add(new Vector2(Mathf.Round(vec.x), Mathf.Round(vec.y)));
		}
	}
}

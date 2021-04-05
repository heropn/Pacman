using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreTemplate : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI tmpPosition;

	[SerializeField]
	private TextMeshProUGUI tmpScore;

	[SerializeField]
	private TextMeshProUGUI tmpName;

	public void SetValues(int pos, int score, string nameText)
	{
		tmpPosition.text = pos.ToString() + '.';
		tmpScore.text = score.ToString();
		tmpName.text = nameText;
	}
}

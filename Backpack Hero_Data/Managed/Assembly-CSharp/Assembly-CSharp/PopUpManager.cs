using System;
using TMPro;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class PopUpManager : MonoBehaviour
{
	// Token: 0x06000312 RID: 786 RVA: 0x00012183 File Offset: 0x00010383
	private void Awake()
	{
		if (!PopUpManager.main)
		{
			PopUpManager.main = this;
			return;
		}
		Object.Destroy(this);
	}

	// Token: 0x06000313 RID: 787 RVA: 0x0001219E File Offset: 0x0001039E
	private void OnDestroy()
	{
		if (PopUpManager.main == this)
		{
			PopUpManager.main = null;
		}
	}

	// Token: 0x06000314 RID: 788 RVA: 0x000121B3 File Offset: 0x000103B3
	private void MakeReference()
	{
		if (!this.canvas)
		{
			this.canvas = GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>();
		}
	}

	// Token: 0x06000315 RID: 789 RVA: 0x000121D7 File Offset: 0x000103D7
	public void CreatePopUp(string text)
	{
		this.CreatePopUp(text, DigitalCursor.main.currentPosition);
	}

	// Token: 0x06000316 RID: 790 RVA: 0x000121EC File Offset: 0x000103EC
	public void CreatePopUp(string text, Vector2 worldPosition)
	{
		this.MakeReference();
		GameObject gameObject = Object.Instantiate<GameObject>(this.popUpPrefab, worldPosition, Quaternion.identity, this.canvas.transform);
		gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
		LangaugeManager.main.SetFont(gameObject.transform);
	}

	// Token: 0x04000218 RID: 536
	public static PopUpManager main;

	// Token: 0x04000219 RID: 537
	[SerializeField]
	private GameObject popUpPrefab;

	// Token: 0x0400021A RID: 538
	private Canvas canvas;
}

using System;
using TMPro;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class MessageManager : MonoBehaviour
{
	// Token: 0x06000B05 RID: 2821 RVA: 0x0006FD76 File Offset: 0x0006DF76
	private void Awake()
	{
		MessageManager.main = this;
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x0006FD7E File Offset: 0x0006DF7E
	private void OnDestroy()
	{
		MessageManager.main = null;
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x0006FD88 File Offset: 0x0006DF88
	private void Start()
	{
		if (!Singleton.Instance.isTestingMode)
		{
			foreach (Overworld_Structure overworld_Structure in Object.FindObjectsOfType<Overworld_Structure>())
			{
				if (!overworld_Structure.transform.parent || !overworld_Structure.transform.parent.CompareTag("Buildings"))
				{
					Object.Destroy(overworld_Structure.gameObject);
				}
			}
		}
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x0006FDED File Offset: 0x0006DFED
	private void Update()
	{
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x0006FDF0 File Offset: 0x0006DFF0
	public void CreatePopUp(string text)
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("UI Canvas");
		Canvas canvas;
		if (gameObject)
		{
			canvas = gameObject.GetComponent<Canvas>();
		}
		else
		{
			canvas = Object.FindObjectOfType<Canvas>();
		}
		Vector2 vector;
		if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), canvas.worldCamera, out vector);
		}
		else
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(DigitalCursor.main.transform.position), null, out vector);
		}
		this.CreatePopUp(text, vector);
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x0006FE93 File Offset: 0x0006E093
	public void CreatePopUp(string text, Vector2 localPoint)
	{
		this.CreatePopUp(text, localPoint, 1f);
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x0006FEA4 File Offset: 0x0006E0A4
	public void CreatePopUp(string text, Vector2 localPoint, float speed)
	{
		Canvas component = GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>();
		GameObject gameObject = Object.Instantiate<GameObject>(this.popUpPrefab, Vector3.zero, Quaternion.identity, component.transform);
		gameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
		gameObject.GetComponent<RectTransform>().anchoredPosition = localPoint;
		gameObject.GetComponentInChildren<Animator>().speed = speed;
		LangaugeManager.main.SetFont(gameObject.transform);
	}

	// Token: 0x040008F3 RID: 2291
	public static MessageManager main;

	// Token: 0x040008F4 RID: 2292
	[SerializeField]
	private GameObject popUpPrefab;
}

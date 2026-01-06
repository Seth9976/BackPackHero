using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200009A RID: 154
public class RunTallyer : MonoBehaviour
{
	// Token: 0x06000361 RID: 865 RVA: 0x00013BD5 File Offset: 0x00011DD5
	private void Start()
	{
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00013BD8 File Offset: 0x00011DD8
	private void Update()
	{
		if (Input.GetKeyDown("5"))
		{
			this.AddNewText("+5 Treasure");
			float num = (float)(this.textParent.childCount * 30) - 405f;
			if (num > 0f)
			{
				if (this.changeHeight != null)
				{
					base.StopCoroutine(this.changeHeight);
				}
				this.changeHeight = base.StartCoroutine(this.ChangeHeightOverTime(num * -1f, 0.6f));
			}
		}
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00013C4C File Offset: 0x00011E4C
	private void AddNewText(string textToDisplay)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.textPrefab, this.textParent);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		TextMeshProUGUI component2 = gameObject.GetComponent<TextMeshProUGUI>();
		component2.text = textToDisplay;
		component.sizeDelta = new Vector2(300f, 0f);
		base.StartCoroutine(this.ExpandHeightOverTime(component2, component, 30f, 0.6f));
	}

	// Token: 0x06000364 RID: 868 RVA: 0x00013CAC File Offset: 0x00011EAC
	private IEnumerator ChangeHeightOverTime(float targetHeight, float time)
	{
		VerticalLayoutGroup vlg = this.textParent.GetComponent<VerticalLayoutGroup>();
		float elapsedTime = 0f;
		float startHeight = (float)vlg.padding.top;
		while (elapsedTime < time)
		{
			vlg.padding.top = (int)Mathf.Lerp(startHeight, targetHeight, elapsedTime / time);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		vlg.padding.top = (int)targetHeight;
		yield break;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00013CC9 File Offset: 0x00011EC9
	private IEnumerator ExpandHeightOverTime(TextMeshProUGUI text, RectTransform rectTransform, float targetHeight, float time)
	{
		float elapsedTime = 0f;
		float startHeight = rectTransform.sizeDelta.y;
		while (elapsedTime < time)
		{
			float num = Mathf.Lerp(startHeight, targetHeight, elapsedTime / time);
			text.alpha = Mathf.Lerp(0f, 1f, elapsedTime / time);
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, num);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		text.alpha = 1f;
		rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, targetHeight);
		yield break;
	}

	// Token: 0x04000274 RID: 628
	[SerializeField]
	private Transform textParent;

	// Token: 0x04000275 RID: 629
	[SerializeField]
	private GameObject textPrefab;

	// Token: 0x04000276 RID: 630
	private Coroutine changeHeight;
}

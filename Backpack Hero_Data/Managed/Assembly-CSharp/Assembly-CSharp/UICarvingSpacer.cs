using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000B0 RID: 176
public class UICarvingSpacer : MonoBehaviour
{
	// Token: 0x0600047D RID: 1149 RVA: 0x0002B9E4 File Offset: 0x00029BE4
	private void Start()
	{
		this.tote = Object.FindObjectOfType<Tote>();
		RectTransform component = base.GetComponent<RectTransform>();
		if (this.startingWidth == -1f)
		{
			this.startingWidth = component.sizeDelta.x;
		}
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0002BA21 File Offset: 0x00029C21
	private void Update()
	{
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0002BA24 File Offset: 0x00029C24
	public void SetWidth(float width)
	{
		RectTransform component = base.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(width, component.sizeDelta.y);
		this.startingWidth = width;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0002BA58 File Offset: 0x00029C58
	public void CreateWidth(float width)
	{
		this.startingWidth = width;
		RectTransform component = base.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(width, component.sizeDelta.y);
		base.StartCoroutine(this.CreateOverTime(width));
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x0002BA98 File Offset: 0x00029C98
	private IEnumerator CreateOverTime(float intendedWidth)
	{
		LayoutGroup v = base.GetComponentInParent<LayoutGroup>();
		RectTransform rt = base.GetComponent<RectTransform>();
		float startingWidthLocal = 0f;
		float timer = 0f;
		while (timer < this.speed)
		{
			Vector2 sizeDelta = rt.sizeDelta;
			timer += Time.deltaTime;
			rt.sizeDelta = new Vector2(Mathf.Lerp(startingWidthLocal, intendedWidth, timer / this.speed), rt.sizeDelta.y);
			Vector2 sizeDelta2 = rt.sizeDelta;
			v.enabled = false;
			v.enabled = true;
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x0002BAB0 File Offset: 0x00029CB0
	public void Remove()
	{
		if (!base.gameObject || this.isDeleting)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		this.isDeleting = true;
		base.StopAllCoroutines();
		base.StartCoroutine(this.RemoveOverTime());
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x0002BB06 File Offset: 0x00029D06
	private IEnumerator RemoveOverTime()
	{
		LayoutGroup v = base.GetComponentInParent<LayoutGroup>();
		RectTransform rt = base.GetComponent<RectTransform>();
		float timer = 0f;
		while (timer < this.speed)
		{
			Vector2 sizeDelta = rt.sizeDelta;
			timer += Time.deltaTime;
			rt.sizeDelta = new Vector2(Mathf.Lerp(this.startingWidth, 0f, timer / this.speed), rt.sizeDelta.y);
			Vector2 sizeDelta2 = rt.sizeDelta;
			v.enabled = false;
			v.enabled = true;
			yield return null;
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x04000352 RID: 850
	[SerializeField]
	private Image carvingImage;

	// Token: 0x04000353 RID: 851
	[SerializeField]
	private float speed;

	// Token: 0x04000354 RID: 852
	private float startingWidth = -1f;

	// Token: 0x04000355 RID: 853
	private bool isDeleting;

	// Token: 0x04000356 RID: 854
	private Tote tote;
}

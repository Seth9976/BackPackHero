using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class Counter : MonoBehaviour
{
	// Token: 0x06000054 RID: 84 RVA: 0x00003ABE File Offset: 0x00001CBE
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.scale = base.GetComponentInParent<Canvas>().transform.localScale.y;
		this.InstantChangeTo(this.value);
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00003AF4 File Offset: 0x00001CF4
	public void InstantChangeTo(int newValue)
	{
		this.value = newValue;
		Object.Instantiate<GameObject>(this.numberTextPrefab, this.numbersParent.position, Quaternion.identity, this.numbersParent).GetComponent<TextMeshProUGUI>().text = this.value.ToString() ?? "";
		this.EndScroll();
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00003B50 File Offset: 0x00001D50
	public void ChangeTo(int newValue)
	{
		if (this.value == newValue)
		{
			return;
		}
		if (this.isRunning)
		{
			base.StopCoroutine(this.scroll);
		}
		int num = this.value;
		int num2 = 0;
		Vector3 position = this.numbersParent.GetChild(this.numbersParent.childCount - 1).position;
		while (num != newValue)
		{
			if (newValue > this.value)
			{
				num++;
				num2++;
			}
			else
			{
				num--;
				num2--;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.numberTextPrefab, position, Quaternion.identity, this.numbersParent);
			gameObject.GetComponent<RectTransform>().anchoredPosition3D += Vector3.up * this.rectTransform.sizeDelta.y * (float)num2 * this.spacingDifference;
			gameObject.GetComponent<TextMeshProUGUI>().text = num.ToString() ?? "";
		}
		this.value = newValue;
		this.scroll = base.StartCoroutine(this.ScrollToNumber(Mathf.Abs(num2)));
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00003C5B File Offset: 0x00001E5B
	private IEnumerator ScrollToNumber(int totalChangeInValue)
	{
		this.isRunning = true;
		bool isGoingUp = true;
		Transform lastChild = this.numbersParent.GetChild(this.numbersParent.childCount - 1);
		float speedX = this.speed * 1f;
		if (lastChild.transform.position.y > base.transform.position.y)
		{
			isGoingUp = false;
		}
		while ((lastChild.transform.position.y < base.transform.position.y && isGoingUp) || (lastChild.transform.position.y > base.transform.position.y && !isGoingUp))
		{
			if (lastChild.transform.position.y < base.transform.position.y)
			{
				this.numbersParent.transform.position += Vector3.up * Time.deltaTime * speedX * this.scale;
				yield return null;
			}
			else
			{
				this.numbersParent.transform.position += Vector3.down * Time.deltaTime * speedX * this.scale;
				yield return null;
			}
			if (!lastChild)
			{
				break;
			}
		}
		this.EndScroll();
		this.isRunning = false;
		yield break;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00003C6C File Offset: 0x00001E6C
	private void EndScroll()
	{
		Transform child = this.numbersParent.GetChild(this.numbersParent.childCount - 1);
		foreach (object obj in this.numbersParent)
		{
			Transform transform = (Transform)obj;
			if (transform != child)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		this.numbersParent.transform.position = base.transform.position;
		child.transform.position = base.transform.position;
	}

	// Token: 0x04000030 RID: 48
	private RectTransform rectTransform;

	// Token: 0x04000031 RID: 49
	[SerializeField]
	private GameObject numberTextPrefab;

	// Token: 0x04000032 RID: 50
	[SerializeField]
	private Transform numbersParent;

	// Token: 0x04000033 RID: 51
	[SerializeField]
	private float speed = 3f;

	// Token: 0x04000034 RID: 52
	[SerializeField]
	private float scale = 1f;

	// Token: 0x04000035 RID: 53
	[SerializeField]
	private float spacingDifference = 0.7f;

	// Token: 0x04000036 RID: 54
	private int value;

	// Token: 0x04000037 RID: 55
	private bool isRunning;

	// Token: 0x04000038 RID: 56
	private Coroutine scroll;
}

using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class ArrowTutorialManager : MonoBehaviour
{
	// Token: 0x0600000E RID: 14 RVA: 0x00002727 File Offset: 0x00000927
	private void Awake()
	{
		if (ArrowTutorialManager.instance == null)
		{
			ArrowTutorialManager.instance = this;
			return;
		}
		Object.Destroy(this);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002743 File Offset: 0x00000943
	private void OnDestroy()
	{
		if (ArrowTutorialManager.instance == this)
		{
			ArrowTutorialManager.instance = null;
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002758 File Offset: 0x00000958
	private void Start()
	{
		this.arrow.SetActive(false);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002768 File Offset: 0x00000968
	private void Update()
	{
		if (this.target != null)
		{
			RectTransform rectTransform = this.target;
			Vector3[] array = new Vector3[4];
			rectTransform.GetWorldCorners(array);
			if (Camera.main.WorldToScreenPoint(rectTransform.transform.position).x < (float)(Screen.width / 2))
			{
				this.arrow.transform.position = (array[2] + array[3]) / 2f;
				this.arrow.transform.position += new Vector3(0.73f, 0f, 0f);
				this.arrow.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
				return;
			}
			this.arrow.transform.position = (array[0] + array[1]) / 2f;
			this.arrow.transform.position -= new Vector3(0.73f, 0f, 0f);
			this.arrow.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000028C3 File Offset: 0x00000AC3
	public void PointArrow(RectTransform t)
	{
		this.target = t;
		this.arrow.SetActive(true);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000028D8 File Offset: 0x00000AD8
	public void HideArrow()
	{
		this.arrow.SetActive(false);
	}

	// Token: 0x04000008 RID: 8
	public static ArrowTutorialManager instance;

	// Token: 0x04000009 RID: 9
	[SerializeField]
	private GameObject arrow;

	// Token: 0x0400000A RID: 10
	private RectTransform target;
}

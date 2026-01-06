using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class CardPositionLayoutElement : MonoBehaviour
{
	// Token: 0x060000B5 RID: 181 RVA: 0x00005441 File Offset: 0x00003641
	private void Start()
	{
		this.cardSize = this.layoutElementRect.sizeDelta;
		this.layoutElementRect.sizeDelta = new Vector2(0f, this.layoutElementRect.sizeDelta.y);
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x0000547C File Offset: 0x0000367C
	private void Update()
	{
		if (this.isDestroying)
		{
			base.transform.SetAsLastSibling();
			this.layoutElementRect.sizeDelta = Vector2.Lerp(this.layoutElementRect.sizeDelta, new Vector2(0f, this.layoutElementRect.sizeDelta.y), Time.deltaTime * 10f);
			if (this.layoutElementRect.sizeDelta.x < 1f)
			{
				Object.Destroy(base.gameObject);
			}
			return;
		}
		this.layoutElementRect.sizeDelta = Vector2.Lerp(this.layoutElementRect.sizeDelta, this.cardSize, Time.deltaTime * 10f);
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x0000552B File Offset: 0x0000372B
	public void StartDestroying()
	{
		this.isDestroying = true;
	}

	// Token: 0x04000089 RID: 137
	[SerializeField]
	private Vector2 cardSize;

	// Token: 0x0400008A RID: 138
	[SerializeField]
	private RectTransform layoutElementRect;

	// Token: 0x0400008B RID: 139
	private bool isDestroying;
}

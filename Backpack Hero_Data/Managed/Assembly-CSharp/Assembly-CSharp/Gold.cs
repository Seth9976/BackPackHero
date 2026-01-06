using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class Gold : MonoBehaviour
{
	// Token: 0x0600062E RID: 1582 RVA: 0x0003CBA7 File Offset: 0x0003ADA7
	private void Start()
	{
		this.itemMovement = base.GetComponent<ItemMovement>();
		base.StartCoroutine(this.Combine());
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0003CBC2 File Offset: 0x0003ADC2
	private IEnumerator Combine()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		Gold[] array = Object.FindObjectsOfType<Gold>();
		bool flag = false;
		foreach (Gold otherGold in array)
		{
			if (otherGold && otherGold != this && !otherGold.combining)
			{
				this.combining = true;
				base.GetComponent<BoxCollider2D>().enabled = false;
				Vector3 position = base.transform.position;
				base.transform.position = otherGold.transform.position;
				this.itemMovement.mousePreview.position = position;
				yield return this.itemMovement.MoveOverTime(12f);
				otherGold.amount += this.amount;
				flag = true;
				break;
			}
			otherGold = null;
		}
		if (flag)
		{
			Object.Destroy(base.gameObject);
		}
		yield break;
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0003CBD4 File Offset: 0x0003ADD4
	private void Update()
	{
		this.goldAmountText.transform.rotation = Quaternion.identity;
		if (this.itemMovement.isDragging || this.combining || this.itemMovement.isTransiting)
		{
			this.goldAmountText.enabled = false;
			return;
		}
		this.goldAmountText.enabled = true;
		this.goldAmountText.text = Mathf.Max(this.amount, 0).ToString() ?? "";
	}

	// Token: 0x040004FB RID: 1275
	[SerializeField]
	private TextMeshPro goldAmountText;

	// Token: 0x040004FC RID: 1276
	private ItemMovement itemMovement;

	// Token: 0x040004FD RID: 1277
	public int amount = 1;

	// Token: 0x040004FE RID: 1278
	public bool combining;
}

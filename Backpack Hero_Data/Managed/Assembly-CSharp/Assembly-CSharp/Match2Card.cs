using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class Match2Card : MonoBehaviour
{
	// Token: 0x060005DF RID: 1503 RVA: 0x0003A02E File Offset: 0x0003822E
	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
		this.gameManager = GameManager.main;
		this.match2CardMaster = base.GetComponentInParent<Match2CardMaster>();
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x0003A053 File Offset: 0x00038253
	private void Update()
	{
		if (this.item)
		{
			this.item.transform.localPosition = new Vector3(0f, 0f, -3f);
		}
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0003A086 File Offset: 0x00038286
	public void OnFlip()
	{
		this.match2CardMaster.Flip(this);
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0003A094 File Offset: 0x00038294
	public void FlipMe()
	{
		if (!this.flipped)
		{
			this.animator.Play("Match2CardFlipUp", 0, 0f);
			this.flipped = true;
			return;
		}
		this.animator.Play("Match2CardFlipDown", 0, 0f);
		this.flipped = false;
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x0003A0E4 File Offset: 0x000382E4
	public void HideItem()
	{
		this.item.transform.localPosition = new Vector3(0f, 0f, -3f);
		this.item.SetActive(false);
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0003A116 File Offset: 0x00038316
	public void ShowItem()
	{
		this.item.transform.localPosition = new Vector3(0f, 0f, -3f);
		this.item.SetActive(true);
	}

	// Token: 0x040004AC RID: 1196
	[HideInInspector]
	public GameObject item;

	// Token: 0x040004AD RID: 1197
	private GameManager gameManager;

	// Token: 0x040004AE RID: 1198
	private Match2CardMaster match2CardMaster;

	// Token: 0x040004AF RID: 1199
	private Animator animator;

	// Token: 0x040004B0 RID: 1200
	private bool flipped;
}

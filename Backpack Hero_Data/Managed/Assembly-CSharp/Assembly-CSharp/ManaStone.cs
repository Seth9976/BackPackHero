using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class ManaStone : SpecialItem
{
	// Token: 0x0600092B RID: 2347 RVA: 0x0005F129 File Offset: 0x0005D329
	private void OnEnable()
	{
		if (!ManaStone.manastones.Contains(this))
		{
			ManaStone.manastones.Add(this);
		}
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0005F143 File Offset: 0x0005D343
	private void OnDisable()
	{
		ManaStone.manastones.Remove(this);
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x0005F154 File Offset: 0x0005D354
	public static void ResetAllStones()
	{
		foreach (ManaStone manaStone in ManaStone.manastones)
		{
			manaStone.ResetStone();
		}
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x0005F1A4 File Offset: 0x0005D3A4
	private void ResetStone()
	{
		this.currentPower = this.maxPower;
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x0005F1B4 File Offset: 0x0005D3B4
	protected override void Start()
	{
		base.Start();
		this.gameManager = GameManager.main;
		if (this.startingPower == -1)
		{
			this.currentPower = this.maxPower;
		}
		else
		{
			this.currentPower = this.startingPower;
		}
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x0005F204 File Offset: 0x0005D404
	private void SetupAmountText()
	{
		Vector3 localPosition = new Vector3(0f, 0f, 0f);
		if (this.amountTextParent)
		{
			localPosition = this.amountTextParent.transform.localPosition;
			Object.Destroy(this.amountTextParent);
		}
		this.amountTextParent = Object.Instantiate<GameObject>(this.gameManager.textAmountPrefab, Vector3.zero, Quaternion.identity, base.transform);
		this.amountTextParent.transform.localPosition = new Vector3(localPosition.x, localPosition.y, -1f);
		this.amountTextParent.SetActive(true);
		this.amountText = this.amountTextParent.GetComponentInChildren<TextMeshPro>();
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x0005F2BC File Offset: 0x0005D4BC
	private void Update()
	{
		if (!this.amountText)
		{
			this.SetupAmountText();
		}
		if (!this.spriteRenderer.enabled)
		{
			this.amountText.sortingOrder = this.spriteRenderer.sortingOrder;
			this.amountText.gameObject.SetActive(false);
			return;
		}
		this.amountText.sortingOrder = this.spriteRenderer.sortingOrder;
		this.amountText.gameObject.SetActive(true);
		this.amountText.text = this.currentPower.ToString() + "/" + this.maxPower.ToString();
		this.amountText.transform.rotation = Quaternion.identity;
		this.amountText.color = new Color(1f, 1f, 1f, this.spriteRenderer.color.a);
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x0005F3A7 File Offset: 0x0005D5A7
	public void ChangeMana(int num)
	{
		this.currentPower += num;
		this.currentPower = Mathf.Clamp(this.currentPower, 0, this.maxPower);
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x0005F3CF File Offset: 0x0005D5CF
	public override void UseSpecialEffect(Status stat)
	{
	}

	// Token: 0x04000742 RID: 1858
	[SerializeField]
	public int maxPower = 3;

	// Token: 0x04000743 RID: 1859
	[HideInInspector]
	public int currentPower;

	// Token: 0x04000744 RID: 1860
	public int startingPower = -1;

	// Token: 0x04000745 RID: 1861
	[SerializeField]
	private TextMeshPro amountText;

	// Token: 0x04000746 RID: 1862
	[SerializeField]
	private GameObject amountTextParent;

	// Token: 0x04000747 RID: 1863
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000748 RID: 1864
	private static List<ManaStone> manastones = new List<ManaStone>();
}

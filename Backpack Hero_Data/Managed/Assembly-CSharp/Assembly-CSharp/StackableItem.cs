using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public class StackableItem : MonoBehaviour
{
	// Token: 0x06000871 RID: 2161 RVA: 0x000584EC File Offset: 0x000566EC
	private void Start()
	{
		base.StartCoroutine(this.NewGold());
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x000584FB File Offset: 0x000566FB
	private IEnumerator NewGold()
	{
		yield return new WaitForSeconds(1f);
		if (this.amountText)
		{
			this.amountText.text = "";
		}
		GameManager.main.ChangeGold(this.amount);
		ItemMovement component = base.GetComponent<ItemMovement>();
		if (component)
		{
			component.DelayDestroy();
		}
		yield break;
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0005850C File Offset: 0x0005670C
	private void SetupAmountText()
	{
		if (this.amountTextParent)
		{
			Object.Destroy(this.amountTextParent);
		}
		this.amountTextParent = Object.Instantiate<GameObject>(this.gameManager.textAmountPrefab, base.transform.position + Vector3.back, Quaternion.identity, base.transform);
		this.amountTextParent.SetActive(true);
		this.amountText = this.amountTextParent.GetComponentInChildren<TextMeshPro>();
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x00058584 File Offset: 0x00056784
	public IEnumerator Fake()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForFixedUpdate();
		foreach (StackableItem stackableItem in Object.FindObjectsOfType<StackableItem>())
		{
			if (stackableItem && !stackableItem.combining && stackableItem.real && stackableItem != this && Item2.GetDisplayName(this.item.displayName) == Item2.GetDisplayName(stackableItem.GetComponent<Item2>().displayName) && !stackableItem.combining)
			{
				this.combining = true;
				GameObject gameObject = Object.Instantiate<GameObject>(this.fakePrefab, base.transform.position, Quaternion.identity);
				gameObject.GetComponent<SpriteRenderer>().sprite = base.GetComponent<SpriteRenderer>().sprite;
				gameObject.GetComponent<SpriteRenderer>().sortingOrder = base.GetComponent<SpriteRenderer>().sortingOrder;
				gameObject.GetComponent<FakeGold>().dest = stackableItem.transform;
				stackableItem.ChangeAmount(this.amount);
				Object.Destroy(base.gameObject);
				yield break;
			}
		}
		this.real = true;
		yield break;
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x00058593 File Offset: 0x00056793
	private void Update()
	{
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x00058598 File Offset: 0x00056798
	public void ChangeAmount(int value)
	{
		if (!this.amountText)
		{
			this.SetupAmountText();
		}
		this.amount += value;
		this.amountText.text = Mathf.Max(this.amount, 0).ToString() ?? "";
	}

	// Token: 0x04000691 RID: 1681
	[SerializeField]
	private TextMeshPro amountText;

	// Token: 0x04000692 RID: 1682
	[SerializeField]
	private GameObject amountTextParent;

	// Token: 0x04000693 RID: 1683
	[SerializeField]
	private GameObject fakePrefab;

	// Token: 0x04000694 RID: 1684
	private ItemMovement itemMovement;

	// Token: 0x04000695 RID: 1685
	private Item2 item;

	// Token: 0x04000696 RID: 1686
	public int amount = 1;

	// Token: 0x04000697 RID: 1687
	public bool combining;

	// Token: 0x04000698 RID: 1688
	public bool real;

	// Token: 0x04000699 RID: 1689
	private GameManager gameManager;
}

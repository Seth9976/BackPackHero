using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D4 RID: 212
public class WheelSlice : MonoBehaviour
{
	// Token: 0x0600062A RID: 1578 RVA: 0x0003CB64 File Offset: 0x0003AD64
	private void Start()
	{
		this.gameManager = GameManager.main;
		this.spriteRenderer = base.GetComponent<Image>();
		this.myColor = this.spriteRenderer.color;
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0003CB8E File Offset: 0x0003AD8E
	private void Update()
	{
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0003CB90 File Offset: 0x0003AD90
	public IEnumerator Flash()
	{
		UICarvingIndicator componentInChildren = base.GetComponentInChildren<UICarvingIndicator>();
		if (componentInChildren == null)
		{
			yield break;
		}
		GameObject x = ItemSpawner.InstantiateItemFree(componentInChildren.GetItem());
		x.transform.position = base.transform.position;
		x.transform.rotation = Quaternion.identity;
		ItemMovement component = x.GetComponent<ItemMovement>();
		component.moveToItemTransform = true;
		component.transform.localScale = Vector3.one;
		Item2 item2 = x.GetComponent<Item2>();
		if (Item2.ShareItemTypes(item2.itemType, new List<Item2.ItemType> { Item2.ItemType.Curse }))
		{
			SoundManager.main.PlaySFX("miniGameBad");
		}
		else
		{
			SoundManager.main.PlaySFX("miniGameGood");
		}
		component.StartCoroutine(component.Move(new Vector2(0f, 2.5f), 0));
		Color flashColor = new Color(this.myColor.r * 2f, this.myColor.g * 2f, this.myColor.b * 2f, 1f);
		float time = 0f;
		while (time < 0.8f)
		{
			time += Time.deltaTime;
			this.spriteRenderer.color = Color.Lerp(flashColor, this.myColor, time / 0.8f);
			yield return null;
		}
		if (!Item2.ShareItemTypes(item2.itemType, new List<Item2.ItemType> { Item2.ItemType.Curse }))
		{
			x.GetComponent<SpriteRenderer>().sortingOrder = 0;
		}
		while (this.gameManager.inSpecialReorg)
		{
			yield return null;
		}
		Object.FindObjectOfType<WheelMaster>().SetAllWheeltemsToLayer(2);
		yield break;
	}

	// Token: 0x040004F8 RID: 1272
	private Image spriteRenderer;

	// Token: 0x040004F9 RID: 1273
	private Color myColor;

	// Token: 0x040004FA RID: 1274
	private GameManager gameManager;
}

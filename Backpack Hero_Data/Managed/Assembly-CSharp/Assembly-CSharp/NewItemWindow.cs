using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000074 RID: 116
public class NewItemWindow : MonoBehaviour
{
	// Token: 0x06000247 RID: 583 RVA: 0x0000E268 File Offset: 0x0000C468
	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
		if (this.particlesSprite)
		{
			Overworld_ParticleManager.main.ShowParticles(this.particlesSprite, 1f);
		}
		SoundManager.main.PlaySFX("newUnlock");
		DigitalCursor.main.Show();
		LangaugeManager.main.SetFont(base.transform);
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000E2CC File Offset: 0x0000C4CC
	private void Update()
	{
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
	public void ClearContent()
	{
		foreach (object obj in this.contentTransform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000E32C File Offset: 0x0000C52C
	public void SetSimpleImageCard(Sprite s, string unlockName, string cardName, string text)
	{
		this.itemNameText.text = LangaugeManager.main.GetTextByKey(unlockName);
		this.ClearContent();
		GameObject gameObject = Object.Instantiate<GameObject>(this.simpleImageCardPrefab);
		gameObject.transform.SetParent(this.contentTransform);
		Card component = gameObject.GetComponent<Card>();
		component.GetDesciptionsSimpleImage(s, LangaugeManager.main.GetTextByKey(cardName), new List<string> { text }, base.gameObject);
		component.stuck = true;
		component.deleteOnDeactivate = false;
		component.SetParent(base.gameObject);
		component.transform.localScale = Vector3.one;
		component.GetComponentInChildren<CanvasGroup>().alpha = 1f;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
	public void SetLore(string lore)
	{
		this.itemNameText.text = LangaugeManager.main.GetTextByKey("gm72b");
		this.ClearContent();
		GameObject gameObject = Object.Instantiate<GameObject>(this.loreCardPrefab);
		gameObject.transform.SetParent(this.contentTransform);
		Card component = gameObject.GetComponent<Card>();
		component.GetDescriptionsLore(lore, base.gameObject);
		component.stuck = true;
		component.deleteOnDeactivate = false;
		component.SetParent(base.gameObject);
		component.transform.localScale = Vector3.one;
		component.GetComponentInChildren<CanvasGroup>().alpha = 1f;
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000E468 File Offset: 0x0000C668
	public void SetBuilding(SellingTile tile)
	{
		this.itemNameText.text = LangaugeManager.main.GetTextByKey("gm71");
		this.ClearContent();
		GameObject gameObject = Overworld_CardManager.main.DisplayCard(tile);
		gameObject.transform.SetParent(this.contentTransform);
		Card component = gameObject.GetComponent<Card>();
		component.GetDescription(tile);
		component.stuck = true;
		component.deleteOnDeactivate = false;
		component.SetParent(base.gameObject);
		component.transform.localScale = Vector3.one;
		component.GetComponentInChildren<CanvasGroup>().alpha = 1f;
		Overworld_ParticleManager.main.ShowParticles(this.constructionSprite, 1f);
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000E50C File Offset: 0x0000C70C
	public void SetBuilding(Overworld_Structure overworld_Structure)
	{
		this.itemNameText.text = LangaugeManager.main.GetTextByKey("gm71");
		this.ClearContent();
		GameObject gameObject = Overworld_CardManager.main.DisplayCard(overworld_Structure, base.gameObject);
		Overworld_CardManager.main.MakeIndependent();
		gameObject.transform.SetParent(this.contentTransform);
		Card component = gameObject.GetComponent<Card>();
		component.MakeStuck();
		component.deleteOnDeactivate = false;
		component.SetParent(base.gameObject);
		component.transform.localScale = Vector3.one;
		component.GetComponentInChildren<CanvasGroup>().alpha = 1f;
		if (Overworld_ParticleManager.main)
		{
			Overworld_ParticleManager.main.ShowParticles(this.constructionSprite, 1f);
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
	public void SetCostume(RuntimeAnimatorController controller)
	{
		this.itemNameText.text = LangaugeManager.main.GetTextByKey("gm72c");
		this.ClearContent();
		Object.Instantiate<GameObject>(this.simpleCardPrefab, Vector3.zero, Quaternion.identity, this.contentTransform);
		this.animatorForCostume.runtimeAnimatorController = controller;
		this.animatorForCostume.Play("Player_Win");
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000E62C File Offset: 0x0000C82C
	public void SetCharacter(Character character)
	{
		this.itemNameText.text = LangaugeManager.main.GetTextByKey("gm66");
		this.ClearContent();
		Card component = Object.Instantiate<GameObject>(this.newCharacterCardPrefab, Vector3.zero, Quaternion.identity, this.contentTransform).GetComponent<Card>();
		component.SetParent(base.gameObject);
		component.GetDescription(character);
		component.stuck = true;
		component.deleteOnDeactivate = false;
		Overworld_ParticleManager.main.ShowParticles(character.mapCharacterSprite[0], 1f);
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
	public void SetMission(Missions m)
	{
		this.ClearContent();
		this.itemNameText.text = LangaugeManager.main.GetTextByKey("gm72");
		GameObject gameObject = Object.Instantiate<GameObject>(this.newMissionPrefab, Vector3.zero, Quaternion.identity, this.contentTransform);
		RunTypeSelector.SetText(m);
		LangaugeManager.main.SetFont(gameObject.transform);
		Card component = gameObject.GetComponent<Card>();
		component.GetDescriptionMission(m, base.gameObject);
		component.stuck = true;
		component.deleteOnDeactivate = false;
		component.SetParent(base.gameObject);
		Selectable[] componentsInChildren = gameObject.GetComponentsInChildren<Selectable>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].navigation = new Navigation
			{
				mode = Navigation.Mode.None
			};
		}
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000E770 File Offset: 0x0000C970
	public void SetItems(List<Item2> items)
	{
		this.ClearContent();
		Debug.Log("setting items" + items.Count.ToString());
		if (items.Count == 0)
		{
			return;
		}
		if (items.Count == 1)
		{
			this.itemNameText.text = LangaugeManager.main.GetTextByKey("gm65");
		}
		else
		{
			this.itemNameText.text = LangaugeManager.main.GetTextByKey("gm65b");
		}
		foreach (Item2 item in items)
		{
			this.SetItem(item);
		}
		if (Overworld_ParticleManager.main)
		{
			Overworld_ParticleManager.main.ShowParticles(items);
		}
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0000E840 File Offset: 0x0000CA40
	public void SetItem(Item2 item2)
	{
		ItemMovement component = item2.GetComponent<ItemMovement>();
		if (!component)
		{
			return;
		}
		GameObject gameObject = component.ShowCard(null);
		gameObject.transform.SetParent(this.contentTransform);
		gameObject.transform.localScale = Vector3.one;
		Card component2 = gameObject.GetComponent<Card>();
		component2.stuck = true;
		component2.deleteOnDeactivate = false;
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000E897 File Offset: 0x0000CA97
	public void ToggleInventory()
	{
		if (this.animator.gameObject.activeSelf)
		{
			this.HideInventory();
			return;
		}
		this.ShowInventory();
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
	private void ShowInventory()
	{
		this.animator.gameObject.SetActive(true);
		this.animator.Play("inventoryIn");
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0000E8DB File Offset: 0x0000CADB
	private void HideInventory()
	{
		this.animator.Play("inventoryOut");
		DigitalCursor.main.Hide();
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0000E8F7 File Offset: 0x0000CAF7
	private void OnDisable()
	{
		if (Overworld_ParticleManager.main)
		{
			Overworld_ParticleManager.main.HideParticles();
		}
	}

	// Token: 0x0400018A RID: 394
	[SerializeField]
	private Animator animatorForCostume;

	// Token: 0x0400018B RID: 395
	[SerializeField]
	private Transform contentTransform;

	// Token: 0x0400018C RID: 396
	[SerializeField]
	private Item2 item;

	// Token: 0x0400018D RID: 397
	[SerializeField]
	private TextMeshProUGUI itemNameText;

	// Token: 0x0400018E RID: 398
	[SerializeField]
	private Character character;

	// Token: 0x0400018F RID: 399
	[SerializeField]
	private GameObject newCharacterCardPrefab;

	// Token: 0x04000190 RID: 400
	[SerializeField]
	private GameObject simpleCardPrefab;

	// Token: 0x04000191 RID: 401
	[SerializeField]
	private GameObject newMissionPrefab;

	// Token: 0x04000192 RID: 402
	[SerializeField]
	private GameObject simpleImageCardPrefab;

	// Token: 0x04000193 RID: 403
	[SerializeField]
	private GameObject loreCardPrefab;

	// Token: 0x04000194 RID: 404
	[SerializeField]
	private Sprite particlesSprite;

	// Token: 0x04000195 RID: 405
	[SerializeField]
	private Sprite constructionSprite;

	// Token: 0x04000196 RID: 406
	private Animator animator;
}

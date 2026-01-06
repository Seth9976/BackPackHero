using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200013E RID: 318
public class Overworld_CardManager : MonoBehaviour
{
	// Token: 0x06000C0C RID: 3084 RVA: 0x0007D2FD File Offset: 0x0007B4FD
	private void Awake()
	{
		Overworld_CardManager.main = this;
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x0007D305 File Offset: 0x0007B505
	private void OnDestroy()
	{
		if (Overworld_CardManager.main == this)
		{
			Overworld_CardManager.main = null;
		}
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x0007D31A File Offset: 0x0007B51A
	private void Start()
	{
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x0007D31C File Offset: 0x0007B51C
	private void Update()
	{
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x0007D31E File Offset: 0x0007B51E
	public void RemoveCard()
	{
		if (this.currentCard)
		{
			Object.Destroy(this.currentCard.gameObject);
		}
	}

	// Token: 0x06000C11 RID: 3089 RVA: 0x0007D33D File Offset: 0x0007B53D
	public void RemoveCardIfParent(GameObject t)
	{
		if (!this.currentCard)
		{
			return;
		}
		if (this.currentCard.GetParent() == t)
		{
			this.RemoveCard();
		}
	}

	// Token: 0x06000C12 RID: 3090 RVA: 0x0007D368 File Offset: 0x0007B568
	public GameObject DisplayCard(Overworld_NPC npc)
	{
		this.RemoveCard();
		GameObject gameObject = Object.Instantiate<GameObject>(this.overWorldCardPrefab, this.cardCanvas.transform);
		this.currentCard = gameObject.GetComponent<Card>();
		this.currentCard.GetDescription(npc);
		return gameObject;
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x0007D3AC File Offset: 0x0007B5AC
	public GameObject DisplayCard(Overworld_Structure building, GameObject parent = null)
	{
		if (parent == null)
		{
			parent = base.gameObject;
		}
		this.RemoveCard();
		GameObject gameObject = Object.Instantiate<GameObject>(this.overWorldCardPrefab, this.cardCanvas.transform);
		this.currentCard = gameObject.GetComponent<Card>();
		this.currentCard.GetDescription(building);
		this.currentCard.deleteOnDeactivate = false;
		return gameObject;
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x0007D40C File Offset: 0x0007B60C
	public void MakeIndependent()
	{
		this.currentCard = null;
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x0007D418 File Offset: 0x0007B618
	public GameObject DisplayCard(SellingTile building)
	{
		this.RemoveCard();
		GameObject gameObject = Object.Instantiate<GameObject>(this.overWorldCardPrefab, this.cardCanvas.transform);
		this.currentCard = gameObject.GetComponent<Card>();
		this.currentCard.GetDescription(building);
		this.currentCard.deleteOnDeactivate = false;
		return gameObject;
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x0007D468 File Offset: 0x0007B668
	public void DisplayCardSimple(string x, GameObject parent, float width = 550f)
	{
		this.RemoveCard();
		GameObject gameObject = Object.Instantiate<GameObject>(this.simpleCardPrefab, this.cardCanvas.transform);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(width, component.sizeDelta.y);
		this.currentCard = gameObject.GetComponent<Card>();
		this.currentCard.GetDescriptionsSimple(new List<string> { x }, parent);
	}

	// Token: 0x040009C0 RID: 2496
	public static Overworld_CardManager main;

	// Token: 0x040009C1 RID: 2497
	private Card currentCard;

	// Token: 0x040009C2 RID: 2498
	[SerializeField]
	private GameObject overWorldCardPrefab;

	// Token: 0x040009C3 RID: 2499
	[SerializeField]
	private GameObject simpleCardPrefab;

	// Token: 0x040009C4 RID: 2500
	[SerializeField]
	private Canvas cardCanvas;
}

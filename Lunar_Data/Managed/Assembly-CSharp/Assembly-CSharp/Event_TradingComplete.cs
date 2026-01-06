using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003D RID: 61
public class Event_TradingComplete : MonoBehaviour
{
	// Token: 0x060001C7 RID: 455 RVA: 0x00009BC4 File Offset: 0x00007DC4
	private void Start()
	{
		this.Remove();
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00009BCC File Offset: 0x00007DCC
	private void Remove()
	{
		GameObject selectedCardFromEvent = EventManager.instance.selectedCardFromEvent;
		Image componentInChildren = selectedCardFromEvent.GetComponentInChildren<Image>();
		this.cardImage.sprite = componentInChildren.sprite;
		Object.Destroy(selectedCardFromEvent);
		Object.Instantiate<GameObject>(Event_TradingSetup.selectedCardFromEvent, new Vector3(0f, 0f, 0f), Quaternion.identity, CardManager.instance.cardsParent).GetComponentInChildren<CardPlacement>().ReturnToDeck();
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00009C38 File Offset: 0x00007E38
	public void SwapSprite()
	{
		Image componentInChildren = Event_TradingSetup.selectedCardFromEvent.GetComponentInChildren<Image>();
		this.cardImage.sprite = componentInChildren.sprite;
	}

	// Token: 0x04000161 RID: 353
	[SerializeField]
	private Image cardImage;
}

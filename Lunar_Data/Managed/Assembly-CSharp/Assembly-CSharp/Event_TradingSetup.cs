using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003E RID: 62
public class Event_TradingSetup : MonoBehaviour
{
	// Token: 0x060001CB RID: 459 RVA: 0x00009C6C File Offset: 0x00007E6C
	private void Start()
	{
		GameObject gameObject = (Event_TradingSetup.selectedCardFromEvent = CardSpawner.instance.GetRandomCards(1)[0]);
		CardDescription component = gameObject.GetComponent<CardDescription>();
		this.cardDescriptor.SetCardTexts(component.cardName, component.cardDescription);
		this.cardDescriptor.SetEnergyRequirement(component.cardEffect.GetNecessaryEnergy());
		this.cardDescriptor.SetCardUseAndClassTypes(component.cardEffect.useType, component.cardEffect.lengthOfEffect, component.cardEffect.classType);
		Image componentInChildren = gameObject.GetComponentInChildren<Image>();
		this.cardImage.sprite = componentInChildren.sprite;
	}

	// Token: 0x04000162 RID: 354
	[SerializeField]
	private Image cardImage;

	// Token: 0x04000163 RID: 355
	[SerializeField]
	private CardDescriptor cardDescriptor;

	// Token: 0x04000164 RID: 356
	public static GameObject selectedCardFromEvent;
}

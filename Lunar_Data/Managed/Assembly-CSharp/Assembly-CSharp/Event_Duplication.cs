using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000039 RID: 57
public class Event_Duplication : MonoBehaviour
{
	// Token: 0x060001BB RID: 443 RVA: 0x00009A14 File Offset: 0x00007C14
	private void Start()
	{
		this.Duplicate();
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00009A1C File Offset: 0x00007C1C
	private void Update()
	{
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00009A20 File Offset: 0x00007C20
	private void Duplicate()
	{
		GameObject selectedCardFromEvent = EventManager.instance.selectedCardFromEvent;
		Image componentInChildren = selectedCardFromEvent.GetComponentInChildren<Image>();
		this.cardImage.sprite = componentInChildren.sprite;
		this.duplicationImage.sprite = componentInChildren.sprite;
		Object.Instantiate<GameObject>(selectedCardFromEvent, CardManager.instance.deck).gameObject.SetActive(false);
	}

	// Token: 0x04000158 RID: 344
	[SerializeField]
	private Image cardImage;

	// Token: 0x04000159 RID: 345
	[SerializeField]
	private Image duplicationImage;
}

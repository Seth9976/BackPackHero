using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003C RID: 60
public class Event_Removal : MonoBehaviour
{
	// Token: 0x060001C4 RID: 452 RVA: 0x00009B80 File Offset: 0x00007D80
	private void Start()
	{
		this.Remove();
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00009B88 File Offset: 0x00007D88
	private void Remove()
	{
		GameObject selectedCardFromEvent = EventManager.instance.selectedCardFromEvent;
		Image componentInChildren = selectedCardFromEvent.GetComponentInChildren<Image>();
		this.cardImage.sprite = componentInChildren.sprite;
		Object.Destroy(selectedCardFromEvent);
	}

	// Token: 0x04000160 RID: 352
	[SerializeField]
	private Image cardImage;
}

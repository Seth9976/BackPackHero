using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003B RID: 59
public class Event_RelicGamble : MonoBehaviour
{
	// Token: 0x060001C1 RID: 449 RVA: 0x00009A9B File Offset: 0x00007C9B
	private void Start()
	{
		this.Gamble();
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x00009AA4 File Offset: 0x00007CA4
	private void Gamble()
	{
		this.addParent.SetActive(false);
		this.removeParent.SetActive(false);
		if (Random.Range(0, 100) < 50)
		{
			this.replacementText.SetKey("eventGambleO3");
			GameObject selectedCardFromEvent = EventManager.instance.selectedCardFromEvent;
			this.removeParent.SetActive(true);
			Image componentInChildren = selectedCardFromEvent.GetComponentInChildren<Image>();
			this.removeImage.sprite = componentInChildren.sprite;
			Object.Destroy(selectedCardFromEvent);
			return;
		}
		this.replacementText.SetKey("eventGambleO2");
		GameObject selectedCardFromEvent2 = EventManager.instance.selectedCardFromEvent;
		this.addParent.SetActive(true);
		Image componentInChildren2 = selectedCardFromEvent2.GetComponentInChildren<Image>();
		this.addImage.GetComponent<Image>().sprite = componentInChildren2.sprite;
		this.addImage2.GetComponent<Image>().sprite = componentInChildren2.sprite;
		Object.Instantiate<GameObject>(selectedCardFromEvent2);
	}

	// Token: 0x0400015A RID: 346
	[SerializeField]
	private ReplacementText replacementText;

	// Token: 0x0400015B RID: 347
	[SerializeField]
	private GameObject removeParent;

	// Token: 0x0400015C RID: 348
	[SerializeField]
	private Image removeImage;

	// Token: 0x0400015D RID: 349
	[SerializeField]
	private GameObject addParent;

	// Token: 0x0400015E RID: 350
	[SerializeField]
	private Image addImage;

	// Token: 0x0400015F RID: 351
	[SerializeField]
	private Image addImage2;
}

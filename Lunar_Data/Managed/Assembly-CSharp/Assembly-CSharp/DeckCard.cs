using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000025 RID: 37
public class DeckCard : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06000119 RID: 281 RVA: 0x00006CA4 File Offset: 0x00004EA4
	private void Start()
	{
		this.cardImage.sprite = this.cardReference.GetComponent<Image>().sprite;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00006CC1 File Offset: 0x00004EC1
	public void OnPointerClick(PointerEventData eventData)
	{
		DeckPanel.instance.ConfirmSelection();
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00006CCD File Offset: 0x00004ECD
	public void OnPointerEnter(PointerEventData eventData)
	{
		DeckSelector.instance.SetSelectedObject(base.gameObject);
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00006CDF File Offset: 0x00004EDF
	public void OnPointerExit(PointerEventData eventData)
	{
		if (DeckSelector.instance.selectedObject == base.gameObject)
		{
			DeckSelector.instance.SetSelectedObject(null);
		}
	}

	// Token: 0x040000D8 RID: 216
	[SerializeField]
	public GameObject cardReference;

	// Token: 0x040000D9 RID: 217
	[SerializeField]
	private Image cardImage;
}

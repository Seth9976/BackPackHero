using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200001C RID: 28
public class ChooseCard : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerClickHandler
{
	// Token: 0x060000C7 RID: 199 RVA: 0x00005998 File Offset: 0x00003B98
	private void OnEnable()
	{
		ChooseCard.chooseCards.Add(this);
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x000059A5 File Offset: 0x00003BA5
	private void OnDisable()
	{
		ChooseCard.chooseCards.Remove(this);
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x000059B3 File Offset: 0x00003BB3
	private void Start()
	{
		this.choosePanel = base.GetComponentInParent<ChoosePanel>();
	}

	// Token: 0x060000CA RID: 202 RVA: 0x000059C4 File Offset: 0x00003BC4
	private void Update()
	{
		if (!this.choosePanel)
		{
			Debug.LogError("ChooseCard has no ChoosePanel parent");
		}
		if (this.choosePanel.selectedCard == base.transform)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.one, Time.deltaTime * 10f);
			base.transform.SetAsLastSibling();
			return;
		}
		base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.one * 0.9f, Time.deltaTime * 10f);
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00005A6C File Offset: 0x00003C6C
	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		this.choosePanel.selectedCard = base.transform;
		SoundManager.instance.PlaySFX("cardClick", -1.0);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00005A97 File Offset: 0x00003C97
	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		this.SelectThisCard();
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00005AA0 File Offset: 0x00003CA0
	public void SelectThisCard()
	{
		if (!this.choosePanel)
		{
			return;
		}
		if (this.choosePanel.HasChoseAlready())
		{
			return;
		}
		if (this.choosePanel.timeOpen <= 0.6f)
		{
			return;
		}
		Relic component = this.cardPrefab.GetComponent<Relic>();
		if (this.cardPrefab.GetComponent<CardPlacement>())
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.cardPrefab);
			CardPlacement component2 = gameObject.GetComponent<CardPlacement>();
			gameObject.transform.position = CanvasManager.instance.transform.position;
			component2.ReturnToDeck();
		}
		else if (component)
		{
			Object.Instantiate<GameObject>(this.cardPrefab, RelicManager.instance.transform);
		}
		this.choosePanel.SetHasChosen(true);
		this.choosePanel.GetComponent<SingleUI>().CloseAndDestroy();
		SoundManager.instance.PlaySFX("cardAdd", double.PositiveInfinity);
	}

	// Token: 0x04000097 RID: 151
	public static List<ChooseCard> chooseCards = new List<ChooseCard>();

	// Token: 0x04000098 RID: 152
	private ChoosePanel choosePanel;

	// Token: 0x04000099 RID: 153
	public GameObject cardPrefab;
}

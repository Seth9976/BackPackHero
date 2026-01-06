using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class EventButton : MonoBehaviour
{
	// Token: 0x060001AF RID: 431 RVA: 0x000098F1 File Offset: 0x00007AF1
	private void Start()
	{
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x000098F3 File Offset: 0x00007AF3
	private void Update()
	{
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x000098F8 File Offset: 0x00007AF8
	public void AddEvents(GameObject resultPanelPrefab)
	{
		Object.Instantiate<GameObject>(this.deckViewerPrefab, CanvasManager.instance.masterContentScaler).GetComponentInChildren<DeckPanel>().ShowAllCards();
		EventManager.instance.originalEventPanel = base.GetComponentInParent<SingleUI>().gameObject;
		EventManager.instance.eventResultPanel = resultPanelPrefab;
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00009944 File Offset: 0x00007B44
	public void AddEventsWithRelicSelection(GameObject resultPanelPrefab)
	{
		Object.Instantiate<GameObject>(this.deckViewerPrefab, CanvasManager.instance.masterContentScaler).GetComponentInChildren<DeckPanel>().ShowRelics();
		EventManager.instance.originalEventPanel = base.GetComponentInParent<SingleUI>().gameObject;
		EventManager.instance.eventResultPanel = resultPanelPrefab;
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x00009990 File Offset: 0x00007B90
	public void AddEventWithoutCards(GameObject resultPanelPrefab)
	{
		Object.Instantiate<GameObject>(resultPanelPrefab, CanvasManager.instance.masterContentScaler);
		EventManager.instance.eventResultPanel = null;
		base.GetComponentInParent<SingleUI>().CloseAndDestroyViaFade();
	}

	// Token: 0x04000153 RID: 339
	[SerializeField]
	private GameObject deckViewerPrefab;
}

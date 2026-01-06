using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000024 RID: 36
public class DeckButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x06000115 RID: 277 RVA: 0x00006C86 File Offset: 0x00004E86
	private void Start()
	{
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00006C88 File Offset: 0x00004E88
	private void Update()
	{
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00006C8A File Offset: 0x00004E8A
	public void OnPointerClick(PointerEventData eventData)
	{
		GameManager.instance.ShowDeck(this.transformToShow);
	}

	// Token: 0x040000D7 RID: 215
	public Transform transformToShow;
}

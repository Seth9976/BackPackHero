using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200009D RID: 157
public class TooltipCreator : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x06000438 RID: 1080 RVA: 0x00015100 File Offset: 0x00013300
	public void OnPointerEnter(PointerEventData eventData)
	{
		TooltipCreator.instance = this;
		TooltipCreator.tooltipReference = Object.Instantiate<GameObject>(this.tooltipPrefab, this.tooltipPosition.position, Quaternion.identity, CanvasManager.instance.masterContentScaler);
		TooltipCreator.tooltipReference.GetComponent<Tooltip>().SetKey(this.textKey);
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x00015152 File Offset: 0x00013352
	public void OnPointerExit(PointerEventData eventData)
	{
		if (!TooltipCreator.tooltipReference)
		{
			return;
		}
		if (TooltipCreator.instance != this)
		{
			return;
		}
		TooltipCreator.instance = null;
		Object.Destroy(TooltipCreator.tooltipReference);
	}

	// Token: 0x0400033B RID: 827
	[SerializeField]
	private GameObject tooltipPrefab;

	// Token: 0x0400033C RID: 828
	[SerializeField]
	private string textKey;

	// Token: 0x0400033D RID: 829
	[SerializeField]
	private Transform tooltipPosition;

	// Token: 0x0400033E RID: 830
	public static GameObject tooltipReference;

	// Token: 0x0400033F RID: 831
	public static TooltipCreator instance;
}

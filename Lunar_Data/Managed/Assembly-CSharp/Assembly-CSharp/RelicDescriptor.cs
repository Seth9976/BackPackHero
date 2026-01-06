using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200007C RID: 124
public class RelicDescriptor : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x0600036B RID: 875 RVA: 0x00011204 File Offset: 0x0000F404
	public void OnPointerEnter(PointerEventData eventData)
	{
		RelicDescriptor.cardDescriptorReference = Object.Instantiate<GameObject>(this.cardDescriptorPrefab, base.transform.position + Vector3.down * 150f, Quaternion.identity, CanvasManager.instance.transform);
		CardDescriptor component = RelicDescriptor.cardDescriptorReference.GetComponent<CardDescriptor>();
		CardDescription component2 = base.GetComponent<CardDescription>();
		component.SetCardTexts(component2.cardName, component2.cardDescription);
		component.DisableUseAndClassTypes();
		component.DisableEnergyRequirement();
	}

	// Token: 0x0600036C RID: 876 RVA: 0x0001127D File Offset: 0x0000F47D
	public void OnPointerExit(PointerEventData eventData)
	{
		if (!RelicDescriptor.cardDescriptorReference)
		{
			return;
		}
		Object.Destroy(RelicDescriptor.cardDescriptorReference);
	}

	// Token: 0x040002A0 RID: 672
	[SerializeField]
	private GameObject cardDescriptorPrefab;

	// Token: 0x040002A1 RID: 673
	public static GameObject cardDescriptorReference;
}

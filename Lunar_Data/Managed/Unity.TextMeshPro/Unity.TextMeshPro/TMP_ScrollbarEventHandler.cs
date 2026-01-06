using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TMPro
{
	// Token: 0x0200004D RID: 77
	public class TMP_ScrollbarEventHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x0600035D RID: 861 RVA: 0x00024AA6 File Offset: 0x00022CA6
		public void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log("Scrollbar click...");
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00024AB2 File Offset: 0x00022CB2
		public void OnSelect(BaseEventData eventData)
		{
			Debug.Log("Scrollbar selected");
			this.isSelected = true;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00024AC5 File Offset: 0x00022CC5
		public void OnDeselect(BaseEventData eventData)
		{
			Debug.Log("Scrollbar De-Selected");
			this.isSelected = false;
		}

		// Token: 0x0400032E RID: 814
		public bool isSelected;
	}
}

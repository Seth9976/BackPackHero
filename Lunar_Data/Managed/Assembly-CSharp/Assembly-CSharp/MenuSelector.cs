using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200005B RID: 91
public class MenuSelector : MonoBehaviour
{
	// Token: 0x060002A3 RID: 675 RVA: 0x0000D88C File Offset: 0x0000BA8C
	private void OnEnable()
	{
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0000D890 File Offset: 0x0000BA90
	private void Update()
	{
		if (!EventSystem.current.currentSelectedGameObject || !EventSystem.current.currentSelectedGameObject.transform.IsChildOf(base.transform))
		{
			this.Select();
		}
		if (EventSystem.current.currentSelectedGameObject && EventSystem.current.currentSelectedGameObject.transform.IsChildOf(base.transform) && this.previousSelected != EventSystem.current.currentSelectedGameObject)
		{
			SoundManager.instance.PlaySFX("lilBlip", double.PositiveInfinity);
			this.previousSelected = EventSystem.current.currentSelectedGameObject;
		}
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0000D93C File Offset: 0x0000BB3C
	private void Select()
	{
		if (this.previousSelected)
		{
			EventSystem.current.SetSelectedGameObject(this.previousSelected);
			return;
		}
		if (this.defaultSelected)
		{
			EventSystem.current.SetSelectedGameObject(this.defaultSelected);
			this.previousSelected = this.defaultSelected;
			return;
		}
	}

	// Token: 0x040001F3 RID: 499
	[SerializeField]
	private GameObject defaultSelected;

	// Token: 0x040001F4 RID: 500
	[SerializeField]
	private GameObject previousSelected;

	// Token: 0x040001F5 RID: 501
	private Selectable[] selectables;
}

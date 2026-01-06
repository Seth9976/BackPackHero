using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class PetPlacementIcon : MonoBehaviour
{
	// Token: 0x060004EE RID: 1262 RVA: 0x0002FCEE File Offset: 0x0002DEEE
	private void Start()
	{
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0002FCF0 File Offset: 0x0002DEF0
	private void Update()
	{
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0002FCF4 File Offset: 0x0002DEF4
	private void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		int siblingIndex = base.GetComponent<Follow>().follow.transform.GetSiblingIndex();
		GameManager.main.SetPetPlacement(siblingIndex);
	}
}

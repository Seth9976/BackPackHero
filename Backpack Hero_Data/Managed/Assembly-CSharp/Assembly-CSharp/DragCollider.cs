using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class DragCollider : MonoBehaviour
{
	// Token: 0x06000160 RID: 352 RVA: 0x0000988B File Offset: 0x00007A8B
	private void Start()
	{
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000988D File Offset: 0x00007A8D
	private void Update()
	{
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00009890 File Offset: 0x00007A90
	private void OnMouseDown()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		if (DigitalCursor.main.OverUI())
		{
			return;
		}
		Overworld_Structure componentInParent = base.GetComponentInParent<Overworld_Structure>();
		if (componentInParent)
		{
			componentInParent.StartDrag(false);
		}
	}
}

using System;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class ChestPouch : CustomInputHandler
{
	// Token: 0x0600002E RID: 46 RVA: 0x00002CF4 File Offset: 0x00000EF4
	private void Start()
	{
		this.pouch = base.GetComponent<ItemPouch>();
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002D02 File Offset: 0x00000F02
	private void Update()
	{
		if (this.pouch.open)
		{
			this.inputHandler.enabled = false;
			return;
		}
		this.inputHandler.enabled = true;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00002D2A File Offset: 0x00000F2A
	public void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.OpenEvent();
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002D3F File Offset: 0x00000F3F
	public override void OnPressStart(string keyName, bool overrideKeyName = false)
	{
		if (keyName != "confirm" && !overrideKeyName)
		{
			return;
		}
		this.OpenEvent();
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002D58 File Offset: 0x00000F58
	public void GoodBye()
	{
		this.isLeaving = true;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002D61 File Offset: 0x00000F61
	private void OpenEvent()
	{
		if (this.isLeaving || GameManager.main.viewingEvent)
		{
			return;
		}
		SoundManager.main.PlaySFX("moveHere");
		this.TogglePouch();
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002D90 File Offset: 0x00000F90
	public void TogglePouch()
	{
		Parcel component = base.GetComponent<Parcel>();
		if (component)
		{
			if (this.pouch.open)
			{
				component.HideText(true);
			}
			else
			{
				component.HideText(false);
			}
		}
		this.pouch.Toggle();
		this.pouch.OpenedViaClick();
	}

	// Token: 0x04000014 RID: 20
	[SerializeField]
	private InputHandler inputHandler;

	// Token: 0x04000015 RID: 21
	private ItemPouch pouch;

	// Token: 0x04000016 RID: 22
	private bool isLeaving;
}

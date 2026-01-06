using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200013D RID: 317
public class Overworld_ButtonGroup : CustomInputHandler
{
	// Token: 0x06000BFF RID: 3071 RVA: 0x0007D059 File Offset: 0x0007B259
	private void Awake()
	{
		Overworld_ButtonGroup.allButtonGroups.Add(this);
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x0007D066 File Offset: 0x0007B266
	private void OnDestroy()
	{
		Overworld_ButtonGroup.allButtonGroups.Remove(this);
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x0007D074 File Offset: 0x0007B274
	private void Start()
	{
		this.digitalCursorInterface = base.GetComponentInParent<DigitalCursorInterface>();
		this.layoutGroup = base.GetComponent<HorizontalLayoutGroup>();
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x0007D090 File Offset: 0x0007B290
	public void Setup()
	{
		this.overworldButtons = new List<Overworld_Button>(base.GetComponentsInChildren<Overworld_Button>());
		List<Transform> list = new List<Transform>();
		foreach (Overworld_Button overworld_Button in this.overworldButtons)
		{
			list.Add(overworld_Button.transform);
		}
		list.Sort((Transform x, Transform y) => x.GetComponent<Overworld_Button>().GetName().CompareTo(y.GetComponent<Overworld_Button>().GetName()));
		foreach (Transform transform in list)
		{
			transform.SetAsFirstSibling();
		}
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x0007D160 File Offset: 0x0007B360
	public void Select(Transform transform)
	{
		this.currentSelection = transform;
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x0007D16C File Offset: 0x0007B36C
	private void Update()
	{
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.controller)
		{
			if (this.digitalCursorInterface.enabled && EventSystem.current && EventSystem.current.currentSelectedGameObject && EventSystem.current.currentSelectedGameObject.transform.IsChildOf(base.transform))
			{
				this.position = Overworld_ButtonGroup.Position.open;
			}
			else
			{
				this.position = Overworld_ButtonGroup.Position.closed;
			}
		}
		if (this.position == Overworld_ButtonGroup.Position.open)
		{
			this.layoutGroup.spacing = Mathf.MoveTowards(this.layoutGroup.spacing, 10f, 2000f * Time.deltaTime);
			return;
		}
		if (this.position == Overworld_ButtonGroup.Position.closed)
		{
			this.layoutGroup.spacing = Mathf.MoveTowards(this.layoutGroup.spacing, -200f, 2000f * Time.deltaTime);
		}
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x0007D244 File Offset: 0x0007B444
	private void SelectThisGroup()
	{
		if (this.position == Overworld_ButtonGroup.Position.open)
		{
			return;
		}
		this.position = Overworld_ButtonGroup.Position.open;
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x0007D258 File Offset: 0x0007B458
	public static void DeselectAllGroups()
	{
		foreach (Overworld_ButtonGroup overworld_ButtonGroup in Overworld_ButtonGroup.allButtonGroups)
		{
			overworld_ButtonGroup.DeselectThisGroup();
		}
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x0007D2A8 File Offset: 0x0007B4A8
	public void DeselectThisGroup()
	{
		if (this.position == Overworld_ButtonGroup.Position.closed)
		{
			return;
		}
		this.position = Overworld_ButtonGroup.Position.closed;
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x0007D2BB File Offset: 0x0007B4BB
	public override void OnCursorHold()
	{
		if (DigitalCursor.main.controlStyle == DigitalCursor.ControlStyle.cursor)
		{
			this.SelectThisGroup();
		}
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x0007D2CF File Offset: 0x0007B4CF
	public override void OnCursorEnd()
	{
		this.DeselectThisGroup();
	}

	// Token: 0x040009BA RID: 2490
	private static List<Overworld_ButtonGroup> allButtonGroups = new List<Overworld_ButtonGroup>();

	// Token: 0x040009BB RID: 2491
	private Transform currentSelection;

	// Token: 0x040009BC RID: 2492
	public Overworld_ButtonGroup.Position position = Overworld_ButtonGroup.Position.closed;

	// Token: 0x040009BD RID: 2493
	private HorizontalLayoutGroup layoutGroup;

	// Token: 0x040009BE RID: 2494
	private List<Overworld_Button> overworldButtons = new List<Overworld_Button>();

	// Token: 0x040009BF RID: 2495
	private DigitalCursorInterface digitalCursorInterface;

	// Token: 0x020003ED RID: 1005
	public enum Position
	{
		// Token: 0x04001740 RID: 5952
		open,
		// Token: 0x04001741 RID: 5953
		closed
	}
}

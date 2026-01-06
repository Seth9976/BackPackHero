using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000DC RID: 220
public abstract class CustomInputHandler : MonoBehaviour
{
	// Token: 0x06000687 RID: 1671 RVA: 0x0003FAAC File Offset: 0x0003DCAC
	public static List<CustomInputHandler> GetAllEnabledInterfaces()
	{
		List<CustomInputHandler> list = Object.FindObjectsOfType<CustomInputHandler>().ToList<CustomInputHandler>();
		for (int i = 0; i < list.Count; i++)
		{
			if (!list[i])
			{
				list.RemoveAt(i);
				i--;
			}
		}
		return list;
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000688 RID: 1672 RVA: 0x0003FAEF File Offset: 0x0003DCEF
	// (set) Token: 0x06000689 RID: 1673 RVA: 0x0003FAF7 File Offset: 0x0003DCF7
	[SerializeField]
	public bool enabledInterface { get; private set; } = true;

	// Token: 0x0600068A RID: 1674 RVA: 0x0003FB00 File Offset: 0x0003DD00
	public void DisableInterface()
	{
		this.enabledInterface = false;
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0003FB09 File Offset: 0x0003DD09
	public void EnableInterface()
	{
		this.enabledInterface = true;
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0003FB12 File Offset: 0x0003DD12
	public virtual void OnPressStart(string keyName, bool overrideKeyName)
	{
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0003FB14 File Offset: 0x0003DD14
	public virtual void OnPressHold(string keyName)
	{
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0003FB16 File Offset: 0x0003DD16
	public virtual void OnPressEnd(string keyName)
	{
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0003FB18 File Offset: 0x0003DD18
	public virtual void OnCursorStart()
	{
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0003FB1A File Offset: 0x0003DD1A
	public virtual void OnCursorHold()
	{
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0003FB1C File Offset: 0x0003DD1C
	public virtual void OnCursorEnd()
	{
	}
}

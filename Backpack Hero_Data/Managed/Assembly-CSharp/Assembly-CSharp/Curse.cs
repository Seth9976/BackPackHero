using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class Curse : MonoBehaviour
{
	// Token: 0x06000700 RID: 1792 RVA: 0x00043CF5 File Offset: 0x00041EF5
	private void Start()
	{
		this.Setup();
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x00043D00 File Offset: 0x00041F00
	public void Setup()
	{
		this.myItem = base.GetComponent<Item2>();
		if (this.myItem)
		{
			if (this.type == Curse.Type.Curse)
			{
				base.name = "Curse";
				this.myItem.displayName = "Curse";
				using (List<Item2.Modifier>.Enumerator enumerator = this.myItem.modifiers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Item2.Modifier modifier = enumerator.Current;
						modifier.name = "Curse";
					}
					return;
				}
			}
			if (this.type == Curse.Type.Blessing)
			{
				base.name = "Blessing";
				this.myItem.displayName = "Blessing";
				foreach (Item2.Modifier modifier2 in this.myItem.modifiers)
				{
					modifier2.name = "Blessing";
				}
			}
		}
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x00043E08 File Offset: 0x00042008
	public void Play()
	{
		if (this.myItem)
		{
			bool destroyed = this.myItem.destroyed;
		}
	}

	// Token: 0x040005A2 RID: 1442
	public Curse.Type type;

	// Token: 0x040005A3 RID: 1443
	[NonSerialized]
	public List<Item2.Modifier> modifiers = new List<Item2.Modifier>();

	// Token: 0x040005A4 RID: 1444
	public int size;

	// Token: 0x040005A5 RID: 1445
	public Item2 myItem;

	// Token: 0x040005A6 RID: 1446
	public int difficulty;

	// Token: 0x02000321 RID: 801
	public enum Type
	{
		// Token: 0x04001277 RID: 4727
		Curse,
		// Token: 0x04001278 RID: 4728
		Blessing
	}
}

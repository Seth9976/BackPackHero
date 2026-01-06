using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EB RID: 235
public class SpecialItemCram : SpecialItem
{
	// Token: 0x06000857 RID: 2135 RVA: 0x00057128 File Offset: 0x00055328
	public override void UseSpecialEffect(Status stat)
	{
		if (!this.satchel)
		{
			this.satchel = Object.FindObjectOfType<Satchel>();
		}
		if (this.type == SpecialItemCram.Type.piston)
		{
			using (List<Item2.Area>.Enumerator enumerator = this.areas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Item2.Area area = enumerator.Current;
					if (area == Item2.Area.self || this.overrideDirection)
					{
						this.satchel.Cram(this.item, area, this.areaDistance, this.directionalOverride);
					}
					else
					{
						this.satchel.Cram(this.item, area, this.areaDistance, Item2.Area.self);
					}
				}
				return;
			}
		}
		if (this.type == SpecialItemCram.Type.bouncer)
		{
			this.satchel.Cram(this.item, Item2.Area.top, Item2.AreaDistance.adjacent, Item2.Area.self);
			this.satchel.Cram(this.item, Item2.Area.left, Item2.AreaDistance.adjacent, Item2.Area.self);
			this.satchel.Cram(this.item, Item2.Area.right, Item2.AreaDistance.adjacent, Item2.Area.self);
			this.satchel.Cram(this.item, Item2.Area.bottom, Item2.AreaDistance.adjacent, Item2.Area.self);
		}
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x00057234 File Offset: 0x00055434
	public override void ShowHighlights()
	{
		if (!this.satchel)
		{
			this.satchel = Object.FindObjectOfType<Satchel>();
		}
		if (this.type == SpecialItemCram.Type.piston)
		{
			using (List<Item2.Area>.Enumerator enumerator = this.areas.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Item2.Area area = enumerator.Current;
					if (area == Item2.Area.self || this.overrideDirection)
					{
						this.satchel.CramHightlight(this.item, area, this.areaDistance, this.directionalOverride);
					}
					else
					{
						this.satchel.CramHightlight(this.item, area, this.areaDistance, Item2.Area.self);
					}
				}
				return;
			}
		}
		if (this.type == SpecialItemCram.Type.bouncer)
		{
			this.satchel.CramHightlight(this.item, Item2.Area.top, Item2.AreaDistance.adjacent, Item2.Area.self);
			this.satchel.CramHightlight(this.item, Item2.Area.left, Item2.AreaDistance.adjacent, Item2.Area.self);
			this.satchel.CramHightlight(this.item, Item2.Area.right, Item2.AreaDistance.adjacent, Item2.Area.self);
			this.satchel.CramHightlight(this.item, Item2.Area.bottom, Item2.AreaDistance.adjacent, Item2.Area.self);
		}
	}

	// Token: 0x04000678 RID: 1656
	[SerializeField]
	private SpecialItemCram.Type type;

	// Token: 0x04000679 RID: 1657
	[SerializeField]
	private List<Item2.Area> areas;

	// Token: 0x0400067A RID: 1658
	[SerializeField]
	private Item2.AreaDistance areaDistance;

	// Token: 0x0400067B RID: 1659
	[SerializeField]
	private Item2.Area directionalOverride;

	// Token: 0x0400067C RID: 1660
	public bool overrideDirection;

	// Token: 0x0400067D RID: 1661
	private Satchel satchel;

	// Token: 0x02000368 RID: 872
	public enum Type
	{
		// Token: 0x0400145C RID: 5212
		piston,
		// Token: 0x0400145D RID: 5213
		bouncer
	}
}

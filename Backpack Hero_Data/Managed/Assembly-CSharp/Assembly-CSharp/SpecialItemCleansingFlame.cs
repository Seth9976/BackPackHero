using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000198 RID: 408
public class SpecialItemCleansingFlame : SpecialItem
{
	// Token: 0x06001067 RID: 4199 RVA: 0x0009D024 File Offset: 0x0009B224
	public override void UseSpecialEffect(Status stat)
	{
		this.tilesUsed.AddRange(this.item.FindGridSpaces());
	}

	// Token: 0x06001068 RID: 4200 RVA: 0x0009D03C File Offset: 0x0009B23C
	public override void ShowHighlights()
	{
		this.itemMovement.CreateHighlight(Color.red, this.tilesUsed.ConvertAll<Vector2>((GameObject x) => base.transform.InverseTransformPoint(x.transform.position)).ToList<Vector2>());
	}

	// Token: 0x06001069 RID: 4201 RVA: 0x0009D06C File Offset: 0x0009B26C
	public override bool ConsiderPlacement(Vector2 endPosition)
	{
		List<GameObject> list = new List<GameObject>();
		List<GameObject> list2 = new List<GameObject>();
		this.itemMovement.TestAtPosition(endPosition, out list2, out list, 1f, false, true, false);
		foreach (GameObject gameObject in list2)
		{
			if (this.tilesUsed.Contains(gameObject))
			{
				PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey("Meditation Idol1"));
				return false;
			}
		}
		return true;
	}

	// Token: 0x04000D5F RID: 3423
	public List<GameObject> tilesUsed = new List<GameObject>();
}

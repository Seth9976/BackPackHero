using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000149 RID: 329
[CreateAssetMenu(fileName = "New Item List", menuName = "Overworld/Items")]
public class Overworld_ItemsList : ScriptableObject
{
	// Token: 0x04000A45 RID: 2629
	public List<Overworld_Item> items = new List<Overworld_Item>();
}

using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200013A RID: 314
[CreateAssetMenu(fileName = "New Building", menuName = "Overworld/Building")]
public class Overworld_BuildingList : ScriptableObject
{
	// Token: 0x04000993 RID: 2451
	public List<Overworld_BuildingManager.Building> buildings = new List<Overworld_BuildingManager.Building>();
}

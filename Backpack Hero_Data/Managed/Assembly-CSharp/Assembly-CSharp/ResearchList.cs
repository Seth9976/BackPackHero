using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200015F RID: 351
[CreateAssetMenu(fileName = "ResearchList", menuName = "ScriptableObjects/Research List")]
public class ResearchList : ScriptableObject
{
	// Token: 0x04000B6E RID: 2926
	[SerializeField]
	public List<MetaProgressSaveManager.Research> researchList;
}

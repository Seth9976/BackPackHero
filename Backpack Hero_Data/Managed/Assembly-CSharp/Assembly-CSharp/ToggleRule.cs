using System;
using UnityEngine;

// Token: 0x0200008A RID: 138
[Serializable]
public class ToggleRule
{
	// Token: 0x04000209 RID: 521
	[Tooltip("which platforms to enable the gameobjects on")]
	public Platform[] AllowedOn = new Platform[0];

	// Token: 0x0400020A RID: 522
	[Tooltip("which gameobjects it should enable")]
	public GameObject[] TargetGameObjects = new GameObject[0];
}

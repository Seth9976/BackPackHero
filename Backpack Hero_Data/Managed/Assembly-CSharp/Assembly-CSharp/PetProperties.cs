using System;
using UnityEngine;

// Token: 0x02000087 RID: 135
[CreateAssetMenu(fileName = "PetProperties", menuName = "ScriptableObjects/PetProperties")]
public class PetProperties : ScriptableObject
{
	// Token: 0x040001FD RID: 509
	[SerializeField]
	public GameObject statusPrefab;

	// Token: 0x040001FE RID: 510
	[SerializeField]
	public GameObject petProxyPrefab;

	// Token: 0x040001FF RID: 511
	[SerializeField]
	private Transform enemyIntentParent;

	// Token: 0x04000200 RID: 512
	[SerializeField]
	private GameObject enemyIntentPrefab;
}

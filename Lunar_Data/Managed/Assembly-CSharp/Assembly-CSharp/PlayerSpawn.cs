using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class PlayerSpawn : MonoBehaviour
{
	// Token: 0x06000340 RID: 832 RVA: 0x000109EE File Offset: 0x0000EBEE
	public void CreatePlayer()
	{
		Object.Instantiate<GameObject>(Singleton.instance.selectedCharacter.characterPrefab, this.playerSpawnPoint.position, Quaternion.identity);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400027C RID: 636
	[SerializeField]
	private Transform playerSpawnPoint;
}

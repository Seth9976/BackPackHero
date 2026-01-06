using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class BootHelper : MonoBehaviour
{
	// Token: 0x06000017 RID: 23 RVA: 0x00002900 File Offset: 0x00000B00
	public void KillBootSceneGOs()
	{
		foreach (GameObject gameObject in this._gameObjectsToKill)
		{
			Object.Destroy(gameObject);
		}
	}

	// Token: 0x0400000B RID: 11
	[SerializeField]
	private List<GameObject> _gameObjectsToKill = new List<GameObject>();
}

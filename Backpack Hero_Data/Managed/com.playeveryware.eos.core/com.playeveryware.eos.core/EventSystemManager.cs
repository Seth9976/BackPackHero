using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class EventSystemManager : MonoBehaviour
{
	// Token: 0x06000009 RID: 9 RVA: 0x00002129 File Offset: 0x00000329
	private void Awake()
	{
		Object.Instantiate<GameObject>(this.inputSystemPrefab, base.transform);
	}

	// Token: 0x04000004 RID: 4
	public GameObject inputSystemPrefab;

	// Token: 0x04000005 RID: 5
	public GameObject inputManagerPrefab;
}

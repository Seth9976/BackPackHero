using System;
using UnityEngine;

// Token: 0x020000FF RID: 255
public class DestroyIfGameObjectDisabled : MonoBehaviour
{
	// Token: 0x060008D5 RID: 2261 RVA: 0x0005CA7D File Offset: 0x0005AC7D
	private void Start()
	{
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0005CA7F File Offset: 0x0005AC7F
	private void Update()
	{
		if (!this.gameObjectToCheck || !this.gameObjectToCheck.activeInHierarchy)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040006FA RID: 1786
	[SerializeField]
	private GameObject gameObjectToCheck;
}

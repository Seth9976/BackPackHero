using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class PropertyText : MonoBehaviour
{
	// Token: 0x06000971 RID: 2417 RVA: 0x00060F5F File Offset: 0x0005F15F
	private void Start()
	{
		base.StartCoroutine(this.ReplaceTextWithSymbols());
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x00060F6E File Offset: 0x0005F16E
	private void Update()
	{
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00060F70 File Offset: 0x0005F170
	private IEnumerator ReplaceTextWithSymbols()
	{
		yield return new WaitForSeconds(0.01f);
		yield break;
	}

	// Token: 0x04000781 RID: 1921
	[SerializeField]
	private GameObject prefabToSpawn;
}

using System;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class KeepAsLastSibling : MonoBehaviour
{
	// Token: 0x06000215 RID: 533 RVA: 0x0000D0F6 File Offset: 0x0000B2F6
	private void Start()
	{
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000D0F8 File Offset: 0x0000B2F8
	private void Update()
	{
		base.transform.SetAsLastSibling();
	}
}

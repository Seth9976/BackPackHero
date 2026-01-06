using System;
using UnityEngine;

// Token: 0x02000138 RID: 312
[DefaultExecutionOrder(-10)]
public class OverworldTarget : MonoBehaviour
{
	// Token: 0x06000BBA RID: 3002 RVA: 0x0007B268 File Offset: 0x00079468
	private void Start()
	{
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x0007B26A File Offset: 0x0007946A
	private void Update()
	{
		base.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	// Token: 0x0400098F RID: 2447
	[SerializeField]
	private Camera myCamera;
}

using System;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class KeepYPosition : MonoBehaviour
{
	// Token: 0x06000218 RID: 536 RVA: 0x0000D10D File Offset: 0x0000B30D
	private void Start()
	{
		this.yPosition = base.transform.position.y;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x0000D125 File Offset: 0x0000B325
	private void LateUpdate()
	{
		base.transform.position = new Vector3(base.transform.position.x, this.yPosition, base.transform.position.z);
	}

	// Token: 0x04000161 RID: 353
	private float yPosition;
}

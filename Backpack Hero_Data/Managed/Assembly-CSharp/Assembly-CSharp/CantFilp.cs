using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class CantFilp : MonoBehaviour
{
	// Token: 0x06000023 RID: 35 RVA: 0x00002B64 File Offset: 0x00000D64
	private void Start()
	{
		this.localPos = base.transform.localPosition;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002B78 File Offset: 0x00000D78
	private void Update()
	{
		if (base.transform.parent.lossyScale.x < 0f)
		{
			base.transform.localPosition = new Vector3(-this.localPos.x, this.localPos.y, this.localPos.z);
			return;
		}
		base.transform.localPosition = this.localPos;
	}

	// Token: 0x0400000F RID: 15
	private Vector3 localPos;
}

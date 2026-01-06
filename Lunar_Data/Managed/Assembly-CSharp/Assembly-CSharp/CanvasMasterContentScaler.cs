using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
[ExecuteInEditMode]
public class CanvasMasterContentScaler : MonoBehaviour
{
	// Token: 0x06000062 RID: 98 RVA: 0x00003748 File Offset: 0x00001948
	private void Start()
	{
	}

	// Token: 0x06000063 RID: 99 RVA: 0x0000374C File Offset: 0x0000194C
	private void Update()
	{
		this.width = (float)Screen.width * Camera.main.rect.width;
		this.height = (float)Screen.height * Camera.main.rect.height;
		float num = this.width / 1280f;
		float num2 = this.height / 720f;
		base.transform.localScale = new Vector3(num, num2, 1f);
	}

	// Token: 0x04000042 RID: 66
	[SerializeField]
	private float width;

	// Token: 0x04000043 RID: 67
	[SerializeField]
	private float height;
}

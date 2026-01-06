using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class CameraMover : MonoBehaviour
{
	// Token: 0x06000054 RID: 84 RVA: 0x00003633 File Offset: 0x00001833
	private void Update()
	{
		if (!this.hasResized && Input.GetMouseButtonDown(0))
		{
			Screen.fullScreen = true;
			Screen.fullScreen = false;
			this.hasResized = true;
		}
	}

	// Token: 0x04000039 RID: 57
	private bool hasResized;
}

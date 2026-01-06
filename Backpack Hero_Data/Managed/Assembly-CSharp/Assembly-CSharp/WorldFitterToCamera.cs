using System;
using UnityEngine;

// Token: 0x02000120 RID: 288
public class WorldFitterToCamera : MonoBehaviour
{
	// Token: 0x060009DE RID: 2526 RVA: 0x00064C6B File Offset: 0x00062E6B
	private void Start()
	{
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x00064C70 File Offset: 0x00062E70
	private void Update()
	{
		float num = (float)Screen.width / (float)Screen.height;
		num /= 1.777778f;
		base.transform.localScale = new Vector3(num, 1f, 1f);
	}
}

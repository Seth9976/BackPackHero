using System;
using UnityEngine;

// Token: 0x02000128 RID: 296
public class MenuDragger : MonoBehaviour
{
	// Token: 0x06000B1E RID: 2846 RVA: 0x00070A4C File Offset: 0x0006EC4C
	private void Start()
	{
		this.rigidbody2D = base.GetComponent<Rigidbody2D>();
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x00070A5A File Offset: 0x0006EC5A
	private void Update()
	{
		this.rigidbody2D.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
	}

	// Token: 0x04000926 RID: 2342
	private Rigidbody2D rigidbody2D;
}

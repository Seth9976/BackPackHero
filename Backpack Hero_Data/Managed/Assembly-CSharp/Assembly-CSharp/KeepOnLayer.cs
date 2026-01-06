using System;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class KeepOnLayer : MonoBehaviour
{
	// Token: 0x0600089A RID: 2202 RVA: 0x0005B503 File Offset: 0x00059703
	private void Start()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x0005B511 File Offset: 0x00059711
	private void Update()
	{
		this.spriteRenderer.sortingOrder = this.layer;
	}

	// Token: 0x040006C8 RID: 1736
	[SerializeField]
	private int layer;

	// Token: 0x040006C9 RID: 1737
	private SpriteRenderer spriteRenderer;
}

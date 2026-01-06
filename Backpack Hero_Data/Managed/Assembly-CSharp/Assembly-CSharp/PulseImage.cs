using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000092 RID: 146
public class PulseImage : MonoBehaviour
{
	// Token: 0x06000324 RID: 804 RVA: 0x00012513 File Offset: 0x00010713
	private void Start()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000325 RID: 805 RVA: 0x00012524 File Offset: 0x00010724
	private void Update()
	{
		base.transform.localScale = new Vector3(base.transform.localScale.x, Mathf.PingPong(Time.time * this.speed, 1f) + 0.5f, 1f);
	}

	// Token: 0x06000326 RID: 806 RVA: 0x00012572 File Offset: 0x00010772
	private void OnDestroy()
	{
		base.transform.localScale = Vector3.one;
	}

	// Token: 0x0400022A RID: 554
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400022B RID: 555
	private Image image;

	// Token: 0x0400022C RID: 556
	private float speed = 2f;
}

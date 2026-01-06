using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class FlowerWobble : MonoBehaviour
{
	// Token: 0x0600018A RID: 394 RVA: 0x0000A078 File Offset: 0x00008278
	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0000A086 File Offset: 0x00008286
	private void Update()
	{
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000A088 File Offset: 0x00008288
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer != 8)
		{
			return;
		}
		if (!this.animator)
		{
			return;
		}
		if (!this.animator.enabled)
		{
			this.animator.enabled = true;
		}
		this.animator.Play("wobble", 0, 0f);
	}

	// Token: 0x04000101 RID: 257
	private Animator animator;
}

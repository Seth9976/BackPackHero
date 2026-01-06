using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class HideOnBuildMode : MonoBehaviour
{
	// Token: 0x0600019B RID: 411 RVA: 0x0000A5F1 File Offset: 0x000087F1
	private void Start()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		base.StartCoroutine(this.Hide());
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000A60C File Offset: 0x0000880C
	private IEnumerator Hide()
	{
		for (;;)
		{
			yield return new WaitForSeconds(0.25f);
			if (!this.spriteRenderer)
			{
				break;
			}
			if (Overworld_Manager.main.IsState(Overworld_Manager.State.MOVING))
			{
				this.spriteRenderer.enabled = true;
			}
			else
			{
				this.spriteRenderer.enabled = false;
			}
		}
		yield break;
		yield break;
	}

	// Token: 0x04000107 RID: 263
	private SpriteRenderer spriteRenderer;
}

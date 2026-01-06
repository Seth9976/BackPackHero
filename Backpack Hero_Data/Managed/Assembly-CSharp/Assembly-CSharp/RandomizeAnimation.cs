using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class RandomizeAnimation : MonoBehaviour
{
	// Token: 0x0600032D RID: 813 RVA: 0x00012844 File Offset: 0x00010A44
	private void Start()
	{
		this.animator.enabled = false;
		base.StartCoroutine(this.StartAnimation());
	}

	// Token: 0x0600032E RID: 814 RVA: 0x0001285F File Offset: 0x00010A5F
	private IEnumerator StartAnimation()
	{
		yield return new WaitForSeconds((float)Random.Range(0, 1));
		this.animator.enabled = true;
		Object.Destroy(this);
		yield break;
	}

	// Token: 0x04000237 RID: 567
	[SerializeField]
	private Animator animator;
}

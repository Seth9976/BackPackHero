using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class EnableAnimatorAfterDelay : MonoBehaviour
{
	// Token: 0x0600016E RID: 366 RVA: 0x00009ABF File Offset: 0x00007CBF
	public void EnableAnimatorAfterDelayFunc()
	{
		base.StartCoroutine(this.EnableAnimatorAfterDelayCoroutine());
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00009ACE File Offset: 0x00007CCE
	private IEnumerator EnableAnimatorAfterDelayCoroutine()
	{
		if (EnableAnimatorAfterDelay.enableAnimatorAfterDelays.Contains(this))
		{
			yield break;
		}
		EnableAnimatorAfterDelay.enableAnimatorAfterDelays.Add(this);
		this.text.enabled = false;
		yield return new WaitForSeconds(1.1f);
		while (EnableAnimatorAfterDelay.enableAnimatorAfterDelays.Count > 0 && EnableAnimatorAfterDelay.enableAnimatorAfterDelays[0] != this)
		{
			yield return new WaitForSeconds(0.3f);
		}
		SoundManager.main.PlaySFX("lucky1");
		this.text.enabled = true;
		this.animator.enabled = true;
		yield return new WaitForSeconds(0.1f);
		EnableAnimatorAfterDelay.enableAnimatorAfterDelays.Remove(this);
		yield break;
	}

	// Token: 0x040000EE RID: 238
	private static List<EnableAnimatorAfterDelay> enableAnimatorAfterDelays = new List<EnableAnimatorAfterDelay>();

	// Token: 0x040000EF RID: 239
	[SerializeField]
	private Animator animator;

	// Token: 0x040000F0 RID: 240
	[SerializeField]
	private TextMeshProUGUI text;
}

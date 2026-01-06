using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class LevelChallengeText : MonoBehaviour
{
	// Token: 0x0600025E RID: 606 RVA: 0x0000CA31 File Offset: 0x0000AC31
	private void Start()
	{
		this.textRoutine = base.StartCoroutine(this.ShowText());
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000CA45 File Offset: 0x0000AC45
	private IEnumerator ShowText()
	{
		while (SingleUI.IsViewingPopUp())
		{
			this.animator.enabled = false;
			this.text.enabled = false;
			yield return null;
		}
		this.animator.enabled = true;
		this.text.enabled = true;
		yield break;
	}

	// Token: 0x040001BE RID: 446
	[SerializeField]
	private Animator animator;

	// Token: 0x040001BF RID: 447
	[SerializeField]
	private TextMeshProUGUI text;

	// Token: 0x040001C0 RID: 448
	private Coroutine textRoutine;
}

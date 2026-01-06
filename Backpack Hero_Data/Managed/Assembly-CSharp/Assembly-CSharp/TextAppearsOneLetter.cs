using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x0200019E RID: 414
public class TextAppearsOneLetter : MonoBehaviour
{
	// Token: 0x0600108E RID: 4238 RVA: 0x0009DAC1 File Offset: 0x0009BCC1
	private void Start()
	{
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x0009DAC4 File Offset: 0x0009BCC4
	private void OnEnable()
	{
		if (!this.text)
		{
			this.text = base.GetComponent<TextMeshProUGUI>();
		}
		if (!this.invert)
		{
			this.text.maxVisibleCharacters = 1;
			if (this.coroutine != null)
			{
				base.StopCoroutine(this.coroutine);
			}
			this.coroutine = base.StartCoroutine(this.ShowOverTime());
			return;
		}
		this.text.maxVisibleCharacters = this.text.text.Length;
		if (this.coroutine != null)
		{
			base.StopCoroutine(this.coroutine);
		}
		this.coroutine = base.StartCoroutine(this.RemoveOverTimeCoroutine());
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0009DB66 File Offset: 0x0009BD66
	private IEnumerator ShowOverTime()
	{
		int num;
		for (int i = 0; i < this.text.text.Length + 1; i = num + 1)
		{
			this.text.maxVisibleCharacters = i;
			yield return new WaitForSeconds(this.timeBetweenLetters);
			num = i;
		}
		yield break;
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x0009DB75 File Offset: 0x0009BD75
	private IEnumerator RemoveOverTimeCoroutine()
	{
		int num;
		for (int i = this.text.text.Length; i >= 0; i = num - 1)
		{
			this.text.maxVisibleCharacters = i;
			yield return new WaitForSeconds(this.timeBetweenLetters);
			num = i;
		}
		yield break;
	}

	// Token: 0x04000D7C RID: 3452
	[SerializeField]
	private bool invert;

	// Token: 0x04000D7D RID: 3453
	[SerializeField]
	private TextMeshProUGUI text;

	// Token: 0x04000D7E RID: 3454
	[SerializeField]
	private float timeBetweenLetters = 0.1f;

	// Token: 0x04000D7F RID: 3455
	private Coroutine coroutine;
}

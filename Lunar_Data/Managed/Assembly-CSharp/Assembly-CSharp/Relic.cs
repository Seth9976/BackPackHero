using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200007B RID: 123
public class Relic : MonoBehaviour
{
	// Token: 0x06000366 RID: 870 RVA: 0x000110BC File Offset: 0x0000F2BC
	private void Start()
	{
		if (this.spriteRenderer)
		{
			this.relicImage.sprite = this.spriteRenderer.sprite;
			Object.Destroy(this.spriteRenderer);
		}
		if (this.popInCoroutine != null)
		{
			base.StopCoroutine(this.popInCoroutine);
		}
		this.popInCoroutine = base.StartCoroutine(this.PopIn(base.transform));
		RelicManager.instance.AddRelic(this);
		UnityEvent unityEvent = this.onStart;
		if (unityEvent != null)
		{
			unityEvent.Invoke();
		}
		this.SetNumber(1);
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00011148 File Offset: 0x0000F348
	public void SetNumber(int num)
	{
		this.relicNumber = num;
		if (num == 1)
		{
			this.relicNumberText.text = "";
			return;
		}
		this.relicNumberText.text = "x" + num.ToString();
		if (this.popInCoroutine != null)
		{
			base.StopCoroutine(this.popInCoroutine);
		}
		this.popInCoroutine = base.StartCoroutine(this.PopIn(this.relicNumberText.transform));
	}

	// Token: 0x06000368 RID: 872 RVA: 0x000111BE File Offset: 0x0000F3BE
	private IEnumerator PopIn(Transform trans)
	{
		float t = 0f;
		float duration = 0.25f;
		Vector2 destinationSize = Vector3.one * 1.2f;
		Vector2 startSize = Vector3.zero;
		while (t < duration)
		{
			t += Time.deltaTime;
			trans.localScale = Vector2.Lerp(startSize, destinationSize, t / duration);
			yield return null;
		}
		t = 0f;
		while (t < duration)
		{
			t += Time.deltaTime;
			trans.localScale = Vector2.Lerp(destinationSize, Vector3.one, t / duration);
			yield return null;
		}
		this.popInCoroutine = null;
		yield break;
	}

	// Token: 0x06000369 RID: 873 RVA: 0x000111D4 File Offset: 0x0000F3D4
	public void AddEnergy()
	{
		EnergyBarMaster.instance.CreateEnergyCapsule();
	}

	// Token: 0x04000298 RID: 664
	[Header("------------------Relic Info--------------------")]
	[SerializeField]
	public int numberAllowed = 1;

	// Token: 0x04000299 RID: 665
	[SerializeField]
	public List<PlayableCharacter.CharacterName> validForCharacters = new List<PlayableCharacter.CharacterName>();

	// Token: 0x0400029A RID: 666
	[Header("------------------References--------------------")]
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Token: 0x0400029B RID: 667
	[SerializeField]
	private Image relicImage;

	// Token: 0x0400029C RID: 668
	[SerializeField]
	private TextMeshProUGUI relicNumberText;

	// Token: 0x0400029D RID: 669
	[NonSerialized]
	public int relicNumber = 1;

	// Token: 0x0400029E RID: 670
	[SerializeField]
	private UnityEvent onStart;

	// Token: 0x0400029F RID: 671
	private Coroutine popInCoroutine;
}

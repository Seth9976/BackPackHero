using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000035 RID: 53
public class EnergyCapsule : MonoBehaviour
{
	// Token: 0x060001A5 RID: 421 RVA: 0x000094FE File Offset: 0x000076FE
	private void Start()
	{
		this.startSize = this.energyCapsuleImage.rectTransform.sizeDelta;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00009516 File Offset: 0x00007716
	public bool IsFull()
	{
		return this.energyValue >= 100f;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00009528 File Offset: 0x00007728
	public void SetEnergy(float amount)
	{
		this.energyValue = amount;
		this.energyValue = Mathf.Clamp(this.energyValue, 0f, 100f);
		this.energyCapsuleImage.sprite = this.notFullEnergy;
		this.energyCapsuleFill.fillAmount = this.energyValue / 100f;
		if (this.energyValue >= 100f)
		{
			this.energyCapsuleImage.sprite = this.fullEnergy;
			if (this.pulseRoutine != null)
			{
				base.StopCoroutine(this.pulseRoutine);
			}
			this.pulseRoutine = base.StartCoroutine(this.Pulse(new Vector2(this.energyCapsuleImage.rectTransform.sizeDelta.x, this.energyCapsuleImage.rectTransform.sizeDelta.y * 1.5f), 0.1f));
		}
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00009600 File Offset: 0x00007800
	public void FillEnergy(float amount)
	{
		this.energyValue += amount;
		this.energyValue = Mathf.Clamp(this.energyValue, 0f, 100f);
		this.energyCapsuleImage.sprite = this.notFullEnergy;
		this.energyCapsuleFill.fillAmount = this.energyValue / 100f;
		if (this.energyValue >= 100f)
		{
			SoundManager.instance.PlaySFX("fillEnergy", double.PositiveInfinity);
			this.energyCapsuleImage.sprite = this.fullEnergy;
			if (this.pulseRoutine != null)
			{
				base.StopCoroutine(this.pulseRoutine);
			}
			this.pulseRoutine = base.StartCoroutine(this.Pulse(new Vector2(this.energyCapsuleImage.rectTransform.sizeDelta.x, this.energyCapsuleImage.rectTransform.sizeDelta.y * 1.5f), 0.1f));
		}
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x000096F8 File Offset: 0x000078F8
	public void PulseNotEnough()
	{
		if (this.pulseRoutine != null)
		{
			base.StopCoroutine(this.pulseRoutine);
		}
		this.pulseRoutine = base.StartCoroutine(this.Pulse(new Vector2(this.energyCapsuleImage.rectTransform.sizeDelta.x, this.energyCapsuleImage.rectTransform.sizeDelta.y * 0.75f), 0.1f));
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00009765 File Offset: 0x00007965
	public IEnumerator Pulse(Vector2 pulseToSize, float duration)
	{
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			this.energyCapsuleImage.rectTransform.sizeDelta = Vector2.Lerp(this.startSize, pulseToSize, elapsedTime / duration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		this.energyCapsuleImage.rectTransform.sizeDelta = pulseToSize;
		elapsedTime = 0f;
		while (elapsedTime < duration * 2f)
		{
			this.energyCapsuleImage.rectTransform.sizeDelta = Vector2.Lerp(pulseToSize, this.startSize, elapsedTime / (duration * 2f));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		this.energyCapsuleImage.rectTransform.sizeDelta = this.startSize;
		yield break;
	}

	// Token: 0x04000147 RID: 327
	[SerializeField]
	private Sprite fullEnergy;

	// Token: 0x04000148 RID: 328
	[SerializeField]
	private Sprite notFullEnergy;

	// Token: 0x04000149 RID: 329
	[SerializeField]
	private Image energyCapsuleFill;

	// Token: 0x0400014A RID: 330
	[SerializeField]
	private Image energyCapsuleImage;

	// Token: 0x0400014B RID: 331
	public float energyValue = 100f;

	// Token: 0x0400014C RID: 332
	private Vector2 startSize;

	// Token: 0x0400014D RID: 333
	private Coroutine pulseRoutine;
}

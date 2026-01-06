using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000063 RID: 99
public class PassiveEffect : MonoBehaviour
{
	// Token: 0x060002D3 RID: 723 RVA: 0x0000EA37 File Offset: 0x0000CC37
	private void Start()
	{
		this.defaultSize = this.rect.localScale;
		this.rect.localScale = new Vector2(0f, 0f);
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x0000EA70 File Offset: 0x0000CC70
	private void Update()
	{
		if (this.shrinkAndDestroyCoroutine != null)
		{
			return;
		}
		this.rect.localScale = Vector2.Lerp(this.rect.localScale, this.defaultSize, Time.deltaTime * 5f);
		this.HandleTimer();
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
	private void HandleTimer()
	{
		if (!this.isTimed)
		{
			return;
		}
		if (this.timeType == PassiveEffect.TimeType.TimeManagerScaled)
		{
			this.timeRemaining -= Time.deltaTime * TimeManager.instance.currentTimeScale;
		}
		else if (this.timeType == PassiveEffect.TimeType.RealTime)
		{
			this.timeRemaining -= Time.deltaTime * TimeManager.instance.currentUnscaledTimeScale;
		}
		this.passiveSlider.value = this.timeRemaining / this.totalTime;
		if (this.timeRemaining <= 0f)
		{
			PassiveEffect.PassiveEffectDeactivateDelegate passiveEffectDeactivateDelegate = this.passiveEffectDeactivateDelegate;
			if (passiveEffectDeactivateDelegate != null)
			{
				passiveEffectDeactivateDelegate();
			}
			if (this.objectsEffected != null)
			{
				foreach (GameObject gameObject in this.objectsEffected)
				{
					if (!(gameObject == null))
					{
						Object.Destroy(gameObject);
					}
				}
			}
			this.ShrinkAndDestroy();
		}
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x0000EBBC File Offset: 0x0000CDBC
	public void ActivatePassiveEffect(Sprite icon, float duration, PassiveEffect.PassiveEffectDelegate passiveEffectDelegate = null, PassiveEffect.PassiveEffectDeactivateDelegate passiveEffectDeactivateDelegate = null)
	{
		this.passiveEffectDelegate = passiveEffectDelegate;
		this.passiveEffectDeactivateDelegate = passiveEffectDeactivateDelegate;
		if (icon != null)
		{
			this.passiveIconImage.sprite = icon;
		}
		this.timeRemaining = duration;
		this.totalTime = duration;
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
	private void ShrinkAndDestroy()
	{
		if (this.shrinkAndDestroyCoroutine != null)
		{
			return;
		}
		this.shrinkAndDestroyCoroutine = base.StartCoroutine(this.ShrinkAndDestroyRoutine());
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0000EC0D File Offset: 0x0000CE0D
	private IEnumerator ShrinkAndDestroyRoutine()
	{
		while (this.rect.localScale.x > 0.1f)
		{
			this.rect.localScale = Vector2.Lerp(this.rect.localScale, new Vector2(0f, 0f), Time.deltaTime * 5f);
			yield return null;
		}
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x0400021F RID: 543
	[SerializeField]
	private PassiveEffect.TimeType timeType;

	// Token: 0x04000220 RID: 544
	[SerializeField]
	private RectTransform rect;

	// Token: 0x04000221 RID: 545
	private Vector2 defaultSize;

	// Token: 0x04000222 RID: 546
	[SerializeField]
	private Image passiveIconImage;

	// Token: 0x04000223 RID: 547
	[SerializeField]
	private Slider passiveSlider;

	// Token: 0x04000224 RID: 548
	[SerializeField]
	private bool isTimed = true;

	// Token: 0x04000225 RID: 549
	private float timeRemaining = 10f;

	// Token: 0x04000226 RID: 550
	private float totalTime = 10f;

	// Token: 0x04000227 RID: 551
	public List<GameObject> objectsEffected = new List<GameObject>();

	// Token: 0x04000228 RID: 552
	public PassiveEffect.PassiveEffectDelegate passiveEffectDelegate;

	// Token: 0x04000229 RID: 553
	public PassiveEffect.PassiveEffectDeactivateDelegate passiveEffectDeactivateDelegate;

	// Token: 0x0400022A RID: 554
	private Coroutine shrinkAndDestroyCoroutine;

	// Token: 0x020000EF RID: 239
	[SerializeField]
	public enum TimeType
	{
		// Token: 0x0400047A RID: 1146
		TimeManagerScaled,
		// Token: 0x0400047B RID: 1147
		RealTime
	}

	// Token: 0x020000F0 RID: 240
	// (Invoke) Token: 0x06000576 RID: 1398
	public delegate void PassiveEffectDelegate();

	// Token: 0x020000F1 RID: 241
	// (Invoke) Token: 0x0600057A RID: 1402
	public delegate void PassiveEffectDeactivateDelegate();
}

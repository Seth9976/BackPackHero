using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class GroundDetector : MonoBehaviour
{
	// Token: 0x0600020D RID: 525 RVA: 0x0000ADB2 File Offset: 0x00008FB2
	private void OnEnable()
	{
		if (GroundDetector.instance == null)
		{
			GroundDetector.instance = this;
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000ADC7 File Offset: 0x00008FC7
	private void OnDisable()
	{
		if (GroundDetector.instance == this)
		{
			GroundDetector.instance = null;
		}
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0000ADDC File Offset: 0x00008FDC
	private void OnTriggerEnter2D(Collider2D other)
	{
		EffectOnGround component = other.GetComponent<EffectOnGround>();
		if (component != null)
		{
			this.effectsOnGround.Add(component);
		}
	}

	// Token: 0x06000210 RID: 528 RVA: 0x0000AE08 File Offset: 0x00009008
	private void OnTriggerExit2D(Collider2D other)
	{
		EffectOnGround component = other.GetComponent<EffectOnGround>();
		if (component != null)
		{
			this.effectsOnGround.Remove(component);
		}
	}

	// Token: 0x06000211 RID: 529 RVA: 0x0000AE32 File Offset: 0x00009032
	public bool OnFire()
	{
		return this.effectsOnGround.Any((EffectOnGround x) => x.groundEffectType == EffectOnGround.GroundEffectType.Fire);
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0000AE5E File Offset: 0x0000905E
	public bool OnIce()
	{
		return this.effectsOnGround.Any((EffectOnGround x) => x.groundEffectType == EffectOnGround.GroundEffectType.Ice);
	}

	// Token: 0x06000213 RID: 531 RVA: 0x0000AE8A File Offset: 0x0000908A
	public bool OnPoison()
	{
		return this.effectsOnGround.Any((EffectOnGround x) => x.groundEffectType == EffectOnGround.GroundEffectType.Poison);
	}

	// Token: 0x04000195 RID: 405
	public static GroundDetector instance;

	// Token: 0x04000196 RID: 406
	[SerializeField]
	private List<EffectOnGround> effectsOnGround = new List<EffectOnGround>();
}

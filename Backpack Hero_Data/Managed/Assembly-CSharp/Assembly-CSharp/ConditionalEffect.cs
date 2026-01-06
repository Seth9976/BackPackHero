using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000014 RID: 20
public class ConditionalEffect : MonoBehaviour
{
	// Token: 0x06000073 RID: 115 RVA: 0x00005251 File Offset: 0x00003451
	private void OnEnable()
	{
		if (!ConditionalEffect.conditions.Contains(this))
		{
			ConditionalEffect.conditions.Add(this);
		}
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000526B File Offset: 0x0000346B
	private void OnDisable()
	{
		ConditionalEffect.conditions.Remove(this);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00005279 File Offset: 0x00003479
	private void Start()
	{
		this.ConsiderInvoke();
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00005281 File Offset: 0x00003481
	public void ConsiderInvoke()
	{
		if (this.storyModeOnly && !Singleton.Instance.storyMode)
		{
			return;
		}
		if (this.CheckMetaProgressMarkers())
		{
			this.effect.Invoke();
			return;
		}
		this.effectIfNotMet.Invoke();
	}

	// Token: 0x06000077 RID: 119 RVA: 0x000052B8 File Offset: 0x000034B8
	public static void AllConditionsConsiderInvoke()
	{
		foreach (ConditionalEffect conditionalEffect in ConditionalEffect.conditions)
		{
			conditionalEffect.ConsiderInvoke();
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00005308 File Offset: 0x00003508
	public bool CheckMetaProgressMarkers()
	{
		foreach (MetaProgressSaveManager.MetaProgressMarker metaProgressMarker in this.markers)
		{
			if (!MetaProgressSaveManager.main.HasMetaProgressMarker(metaProgressMarker))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04000043 RID: 67
	public static List<ConditionalEffect> conditions = new List<ConditionalEffect>();

	// Token: 0x04000044 RID: 68
	[SerializeField]
	private List<MetaProgressSaveManager.MetaProgressMarker> markers = new List<MetaProgressSaveManager.MetaProgressMarker>();

	// Token: 0x04000045 RID: 69
	[SerializeField]
	private UnityEvent effect;

	// Token: 0x04000046 RID: 70
	[SerializeField]
	private UnityEvent effectIfNotMet;

	// Token: 0x04000047 RID: 71
	[SerializeField]
	private bool storyModeOnly;
}

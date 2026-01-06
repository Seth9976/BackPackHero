using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008A RID: 138
public class RunTypeManager : MonoBehaviour
{
	// Token: 0x060003A3 RID: 931 RVA: 0x00012180 File Offset: 0x00010380
	private void OnEnable()
	{
		if (RunTypeManager.instance == null)
		{
			RunTypeManager.instance = this;
		}
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00012195 File Offset: 0x00010395
	private void OnDisable()
	{
		if (RunTypeManager.instance == this)
		{
			RunTypeManager.instance = null;
		}
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x000121AC File Offset: 0x000103AC
	private void Start()
	{
		RunType selectedRun = Singleton.instance.selectedRun;
		if (!selectedRun)
		{
			return;
		}
		foreach (RunType.RunProperty runProperty in selectedRun.runProperties)
		{
			if (runProperty.runPropertyType == RunType.RunProperty.RunPropertyType.StartingDeck)
			{
				CardManager.instance.DeleteAllCards();
				foreach (GameObject gameObject in runProperty.objs)
				{
					Object.Instantiate<GameObject>(gameObject, CardManager.instance.deck).gameObject.SetActive(false);
				}
				CardManager.instance.ShuffleDeck();
			}
		}
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x0001227C File Offset: 0x0001047C
	public bool GetRunTypeModifierExists(RunType.RunProperty.RunPropertyType runPropertyType)
	{
		RunType selectedRun = Singleton.instance.selectedRun;
		if (!selectedRun)
		{
			return false;
		}
		using (List<RunType.RunProperty>.Enumerator enumerator = selectedRun.runProperties.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.runPropertyType == runPropertyType)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000122EC File Offset: 0x000104EC
	public float GetRunTypeModifierPercentage(RunType.RunProperty.RunPropertyType runPropertyType)
	{
		float num = 1f;
		RunType selectedRun = Singleton.instance.selectedRun;
		if (!selectedRun)
		{
			return num;
		}
		foreach (RunType.RunProperty runProperty in selectedRun.runProperties)
		{
			if (runProperty.runPropertyType == runPropertyType)
			{
				num += runProperty.value;
			}
		}
		return num;
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00012368 File Offset: 0x00010568
	public float GetRunTypeModifierValue(RunType.RunProperty.RunPropertyType runPropertyType)
	{
		float num = 0f;
		RunType selectedRun = Singleton.instance.selectedRun;
		if (!selectedRun)
		{
			return num;
		}
		foreach (RunType.RunProperty runProperty in selectedRun.runProperties)
		{
			if (runProperty.runPropertyType == runPropertyType)
			{
				num += runProperty.value;
			}
		}
		return num;
	}

	// Token: 0x040002CC RID: 716
	public static RunTypeManager instance;
}

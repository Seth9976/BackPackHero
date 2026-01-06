using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
public class RelicManager : MonoBehaviour
{
	// Token: 0x0600036E RID: 878 RVA: 0x0001129E File Offset: 0x0000F49E
	private void OnEnable()
	{
		if (RelicManager.instance == null)
		{
			RelicManager.instance = this;
		}
	}

	// Token: 0x0600036F RID: 879 RVA: 0x000112B3 File Offset: 0x0000F4B3
	private void OnDisable()
	{
		if (RelicManager.instance == this)
		{
			RelicManager.instance = null;
		}
	}

	// Token: 0x06000370 RID: 880 RVA: 0x000112C8 File Offset: 0x0000F4C8
	public void AddRelic(Relic relic)
	{
		relic.transform.SetParent(base.transform);
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Relic component = base.transform.GetChild(i).GetComponent<Relic>();
			if (component && !(component == relic) && Utils.CompareStrings(component.name, relic.name))
			{
				component.SetNumber(component.relicNumber + 1);
				relic.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
				return;
			}
		}
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00011354 File Offset: 0x0000F554
	public int GetNumOfRelics(string relicName)
	{
		int num = 0;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			Relic component = base.transform.GetChild(i).GetComponent<Relic>();
			if (component && Utils.CompareStrings(component.name, relicName))
			{
				num += component.relicNumber;
			}
		}
		return num;
	}

	// Token: 0x040002A2 RID: 674
	public static RelicManager instance;
}

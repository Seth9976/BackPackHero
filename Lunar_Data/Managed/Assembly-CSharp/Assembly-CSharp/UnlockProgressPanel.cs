using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class UnlockProgressPanel : MonoBehaviour
{
	// Token: 0x06000479 RID: 1145 RVA: 0x000160E1 File Offset: 0x000142E1
	private void Start()
	{
		UnlockManager instance = UnlockManager.instance;
		this.FindMostCompletedUnlocks(4);
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x000160F0 File Offset: 0x000142F0
	private void FindMostCompletedUnlocks(int numToFind)
	{
		List<Unlock> list = new List<Unlock>();
		foreach (Unlock unlock3 in UnlockManager.instance.unlocks)
		{
			list.Add(unlock3);
		}
		list.RemoveAll((Unlock unlock) => !unlock.isVisible);
		list.RemoveAll((Unlock unlock) => unlock.Unlocked());
		list = list.OrderByDescending((Unlock unlock) => unlock.ProgressMade()).ToList<Unlock>();
		list = list.GetRange(0, Mathf.Min(numToFind, list.Count));
		foreach (Unlock unlock2 in list)
		{
			Object.Instantiate<GameObject>(this.unlockProgressBarPrefab, this.layoutGroupParent).GetComponent<UnlockProgressBar>().SetUnlock(unlock2);
		}
	}

	// Token: 0x04000379 RID: 889
	[SerializeField]
	private Transform layoutGroupParent;

	// Token: 0x0400037A RID: 890
	[SerializeField]
	private GameObject unlockProgressBarPrefab;
}

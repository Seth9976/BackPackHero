using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
public class AllUnlocksProgressPanel : MonoBehaviour
{
	// Token: 0x06000010 RID: 16 RVA: 0x00002427 File Offset: 0x00000627
	private void Start()
	{
		this.DisplayAllUnlocks();
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002430 File Offset: 0x00000630
	private void DisplayAllUnlocks()
	{
		foreach (Unlock unlock in UnlockManager.instance.unlocks)
		{
			if (unlock.isVisible)
			{
				Object.Instantiate<GameObject>(this.unlockProgressBarPrefab, this.layoutGroupParent).GetComponent<UnlockProgressBar>().SetUnlock(unlock);
			}
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000024A4 File Offset: 0x000006A4
	public void ShowUnlockDetails(Unlock unlock)
	{
		this.unlockCompletePanel.gameObject.SetActive(true);
		this.unlockCompletePanel.GetComponent<UnlockCompletePanel>().UnlockComplete(unlock);
	}

	// Token: 0x04000006 RID: 6
	[SerializeField]
	private Transform layoutGroupParent;

	// Token: 0x04000007 RID: 7
	[SerializeField]
	private GameObject unlockProgressBarPrefab;

	// Token: 0x04000008 RID: 8
	[SerializeField]
	private UnlockCompletePanel unlockCompletePanel;
}

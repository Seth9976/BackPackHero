using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000A4 RID: 164
public class UnlockManager : MonoBehaviour
{
	// Token: 0x06000466 RID: 1126 RVA: 0x00015B9B File Offset: 0x00013D9B
	private void OnEnable()
	{
		UnlockManager.instance = this;
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00015BA3 File Offset: 0x00013DA3
	private void OnDisable()
	{
		UnlockManager.instance = null;
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00015BAB File Offset: 0x00013DAB
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			this.ShowUnlocks();
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x00015BBC File Offset: 0x00013DBC
	public bool IsUnlocked(GameObject gameObject)
	{
		Unlock associatedLock = this.GetAssociatedLock(gameObject);
		return associatedLock == null || associatedLock.AlreadyUnlocked();
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00015BE4 File Offset: 0x00013DE4
	public Unlock GetAssociatedLock(GameObject gameObject)
	{
		foreach (Unlock unlock in this.unlocks)
		{
			if (unlock.unlockedItem == gameObject)
			{
				return unlock;
			}
		}
		return null;
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00015C48 File Offset: 0x00013E48
	public bool IsUnlocked(RunType runType)
	{
		Unlock unlock = this.HasAssociatedLock(runType);
		return unlock == null || unlock.AlreadyUnlocked();
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x00015C70 File Offset: 0x00013E70
	public Unlock HasAssociatedLock(RunType runType)
	{
		foreach (Unlock unlock in this.unlocks)
		{
			if (unlock.runType == runType)
			{
				return unlock;
			}
		}
		return null;
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x00015CD4 File Offset: 0x00013ED4
	public bool IsUnlocked(PlayableCharacter character)
	{
		Unlock unlock = this.HasAssociatedLock(character);
		return unlock == null || unlock.AlreadyUnlocked();
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00015CFC File Offset: 0x00013EFC
	public Unlock HasAssociatedLock(PlayableCharacter character)
	{
		foreach (Unlock unlock in this.unlocks)
		{
			if (unlock.character == character)
			{
				return unlock;
			}
		}
		return null;
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x00015D60 File Offset: 0x00013F60
	public void ShowUnlocks()
	{
		if (this.showUnlocksCoroutine != null)
		{
			base.StopCoroutine(this.showUnlocksCoroutine);
		}
		this.showUnlocksCoroutine = base.StartCoroutine(this.ShowUnlocksR());
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x00015D88 File Offset: 0x00013F88
	private IEnumerator ShowUnlocksR()
	{
		foreach (Unlock unlock in this.unlocks)
		{
			if (!unlock.AlreadyUnlocked() && unlock.UnlockedThisRun())
			{
				Object.Instantiate<GameObject>(this.unlockCompletePanelPrefab, CanvasManager.instance.masterContentScaler).GetComponent<UnlockCompletePanel>().unlockProgressBar.SetUnlock(unlock);
				while (SingleUI.IsViewingPopUp())
				{
					yield return null;
				}
			}
		}
		List<Unlock>.Enumerator enumerator = default(List<Unlock>.Enumerator);
		Object.Instantiate<GameObject>(this.unlockProgressPanelPrefab, CanvasManager.instance.masterContentScaler);
		while (SingleUI.IsViewingPopUp())
		{
			yield return null;
		}
		Singleton.instance.UpdateStartingRunAccomplishments();
		yield break;
		yield break;
	}

	// Token: 0x04000365 RID: 869
	public static UnlockManager instance;

	// Token: 0x04000366 RID: 870
	[SerializeField]
	public List<Unlock> unlocks = new List<Unlock>();

	// Token: 0x04000367 RID: 871
	[SerializeField]
	private GameObject unlockProgressPanelPrefab;

	// Token: 0x04000368 RID: 872
	[SerializeField]
	private GameObject unlockCompletePanelPrefab;

	// Token: 0x04000369 RID: 873
	[SerializeField]
	private bool unlockAll;

	// Token: 0x0400036A RID: 874
	private Coroutine showUnlocksCoroutine;
}

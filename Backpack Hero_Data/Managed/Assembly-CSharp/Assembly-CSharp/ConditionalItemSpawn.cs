using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class ConditionalItemSpawn : MonoBehaviour
{
	// Token: 0x0600007B RID: 123 RVA: 0x00005387 File Offset: 0x00003587
	private IEnumerator SpawnSpecialItems()
	{
		yield return new WaitForSeconds(0.5f);
		if (TutorialManager.main && TutorialManager.main.tutorialSequence != TutorialManager.TutorialSequence.trulyDone)
		{
			yield break;
		}
		using (List<ConditionalItemSpawn.ItemSpawn>.Enumerator enumerator = this.itemSpawns.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ConditionalItemSpawn.ItemSpawn itemSpawn = enumerator.Current;
				if (MetaProgressSaveManager.ConditionsMet(itemSpawn.conditions))
				{
					Object.Instantiate<GameObject>(itemSpawn.item, base.transform.position, Quaternion.identity);
					MetaProgressSaveManager.main.AddMetaProgressMarker(itemSpawn.markersToAdd);
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00005396 File Offset: 0x00003596
	private void Start()
	{
		if (!Singleton.Instance.storyMode)
		{
			return;
		}
		base.StartCoroutine(this.SpawnSpecialItems());
	}

	// Token: 0x04000048 RID: 72
	[SerializeField]
	private List<ConditionalItemSpawn.ItemSpawn> itemSpawns;

	// Token: 0x02000245 RID: 581
	[Serializable]
	private class ItemSpawn
	{
		// Token: 0x04000EB9 RID: 3769
		public GameObject item;

		// Token: 0x04000EBA RID: 3770
		public List<MetaProgressSaveManager.MetaProgressCondition> conditions = new List<MetaProgressSaveManager.MetaProgressCondition>();

		// Token: 0x04000EBB RID: 3771
		public List<MetaProgressSaveManager.MetaProgressMarker> markersToAdd = new List<MetaProgressSaveManager.MetaProgressMarker>();
	}
}

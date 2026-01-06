using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class CardSpawner : MonoBehaviour
{
	// Token: 0x060000B9 RID: 185 RVA: 0x0000553C File Offset: 0x0000373C
	private void OnEnable()
	{
		if (CardSpawner.instance == null)
		{
			CardSpawner.instance = this;
			return;
		}
		Object.Destroy(this);
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00005558 File Offset: 0x00003758
	private void OnDisable()
	{
		if (CardSpawner.instance == this)
		{
			CardSpawner.instance = null;
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000556D File Offset: 0x0000376D
	private void Start()
	{
	}

	// Token: 0x060000BC RID: 188 RVA: 0x0000556F File Offset: 0x0000376F
	private void Update()
	{
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00005574 File Offset: 0x00003774
	public List<GameObject> GetRandomRelics(int numToGet)
	{
		List<GameObject> list = (from x in (from x in this.relics.Where((Relic x) => x.numberAllowed > RelicManager.instance.GetNumOfRelics(x.name)).ToList<Relic>()
				where UnlockManager.instance.IsUnlocked(x.gameObject)
				select x).ToList<Relic>()
			where x.validForCharacters.Count == 0 || x.validForCharacters.Contains(Singleton.instance.selectedCharacter.characterName)
			select x).ToList<Relic>().ConvertAll<GameObject>((Relic x) => x.gameObject);
		List<GameObject> list2 = new List<GameObject>();
		for (int i = 0; i < numToGet; i++)
		{
			int num = Random.Range(0, list.Count);
			list2.Add(list[num]);
			list.RemoveAt(num);
		}
		return list2;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x0000565C File Offset: 0x0000385C
	public List<GameObject> GetRandomCards(int numToGet)
	{
		List<GameObject> list = (from x in (from x in new List<CardEffect>(this.cardEffect).Where((CardEffect x) => x.canBeFoundInNormalDrops).ToList<CardEffect>()
				where x.validForCharacters.Count == 0 || x.validForCharacters.Contains(Singleton.instance.selectedCharacter.characterName)
				select x).ToList<CardEffect>()
			where UnlockManager.instance.IsUnlocked(x.gameObject)
			select x).ToList<CardEffect>().ConvertAll<GameObject>((CardEffect x) => x.gameObject);
		List<GameObject> list2 = new List<GameObject>();
		for (int i = 0; i < numToGet; i++)
		{
			int num = Random.Range(0, list.Count);
			list2.Add(list[num]);
			list.RemoveAt(num);
		}
		return list2;
	}

	// Token: 0x0400008C RID: 140
	public static CardSpawner instance;

	// Token: 0x0400008D RID: 141
	[SerializeField]
	private List<CardEffect> cardEffect = new List<CardEffect>();

	// Token: 0x0400008E RID: 142
	[SerializeField]
	private List<Relic> relics = new List<Relic>();
}

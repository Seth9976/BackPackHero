using System;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class VisionSprite : MonoBehaviour
{
	// Token: 0x06001026 RID: 4134 RVA: 0x0009BBB0 File Offset: 0x00099DB0
	private void Start()
	{
		GameManager main = GameManager.main;
		DungeonSpawner dungeonSpawner = Object.FindObjectOfType<DungeonSpawner>();
		foreach (DungeonLevel.EnemyEncounter2 enemyEncounter in main.dungeonLevel.enemyEncounters)
		{
			if (enemyEncounter.enemiesInGroup[0].name == dungeonSpawner.GetDungeonPropertyString(DungeonSpawner.DungeonProperty.Type.chosenBoss))
			{
				SpriteRenderer componentInChildren = enemyEncounter.enemiesInGroup[0].GetComponentInChildren<SpriteRenderer>();
				SpriteRenderer componentInChildren2 = base.GetComponentInChildren<SpriteRenderer>();
				componentInChildren2.sprite = componentInChildren.sprite;
				componentInChildren2.flipX = componentInChildren.flipX;
			}
		}
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x0009BC5C File Offset: 0x00099E5C
	private void Update()
	{
	}
}

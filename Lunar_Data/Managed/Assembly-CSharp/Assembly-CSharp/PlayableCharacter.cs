using System;
using System.Collections.Generic;
using SaveSystem.States;
using UnityEngine;

// Token: 0x0200006D RID: 109
[CreateAssetMenu(fileName = "PlayableCharacter", menuName = "ScriptableObjects/PlayableCharacter", order = 2)]
public class PlayableCharacter : ScriptableObject
{
	// Token: 0x04000251 RID: 593
	[SerializeField]
	public PlayableCharacter.CharacterName characterName;

	// Token: 0x04000252 RID: 594
	[SerializeField]
	public ProgressState.Unlock unlockCondition = ProgressState.Unlock.PriestCharacter;

	// Token: 0x04000253 RID: 595
	[SerializeField]
	public GameObject characterPrefab;

	// Token: 0x04000254 RID: 596
	[SerializeField]
	public Sprite mapIcon;

	// Token: 0x04000255 RID: 597
	[SerializeField]
	public RuntimeAnimatorController entranceAnimator;

	// Token: 0x04000256 RID: 598
	[SerializeField]
	public List<GameObject> startingDeck = new List<GameObject>();

	// Token: 0x020000F8 RID: 248
	public enum CharacterName
	{
		// Token: 0x0400049A RID: 1178
		BattleNun,
		// Token: 0x0400049B RID: 1179
		Dagger
	}
}

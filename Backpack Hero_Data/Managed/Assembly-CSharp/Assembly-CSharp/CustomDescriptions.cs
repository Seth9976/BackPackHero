using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class CustomDescriptions : MonoBehaviour
{
	// Token: 0x040005A7 RID: 1447
	[SerializeField]
	public string flavorTextKey;

	// Token: 0x040005A8 RID: 1448
	public List<CustomDescriptions.Description> descriptions;

	// Token: 0x02000322 RID: 802
	[Serializable]
	public class Description
	{
		// Token: 0x060015EE RID: 5614 RVA: 0x000BCDDC File Offset: 0x000BAFDC
		public int GetValue()
		{
			CustomDescriptions.Description.ValueType valueType = this.valueType;
			if (valueType == CustomDescriptions.Description.ValueType.integer)
			{
				return this.value;
			}
			if (valueType == CustomDescriptions.Description.ValueType.numberOfInvincbleHitsTaken)
			{
				return Mathf.Max(0, GameFlowManager.main.GetCombatStat(GameFlowManager.CombatStat.Type.invincibleHitsTaken, GameFlowManager.CombatStat.Length.combat)) + 1;
			}
			return 0;
		}

		// Token: 0x04001279 RID: 4729
		public Item2.Trigger trigger;

		// Token: 0x0400127A RID: 4730
		public List<Character.CharacterName> validForCharacters;

		// Token: 0x0400127B RID: 4731
		public string triggerToOverrideName;

		// Token: 0x0400127C RID: 4732
		public string triggerName;

		// Token: 0x0400127D RID: 4733
		public string description;

		// Token: 0x0400127E RID: 4734
		public CustomDescriptions.Description.ValueType valueType;

		// Token: 0x0400127F RID: 4735
		public int value;

		// Token: 0x04001280 RID: 4736
		public bool requireCertainRotation;

		// Token: 0x04001281 RID: 4737
		public int zRotation;

		// Token: 0x0200049D RID: 1181
		public enum ValueType
		{
			// Token: 0x04001B0A RID: 6922
			integer,
			// Token: 0x04001B0B RID: 6923
			numberOfInvincbleHitsTaken
		}
	}
}

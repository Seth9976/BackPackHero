using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000B RID: 11
	[MovedFrom(true, "UnityEngine", null, null)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/RuleOverrideTile.html")]
	[Serializable]
	public class AdvancedRuleOverrideTile : RuleOverrideTile
	{
		// Token: 0x1700000A RID: 10
		public RuleTile.TilingRuleOutput this[RuleTile.TilingRule originalRule]
		{
			get
			{
				foreach (RuleTile.TilingRuleOutput tilingRuleOutput in this.m_OverrideTilingRules)
				{
					if (tilingRuleOutput.m_Id == originalRule.m_Id)
					{
						return tilingRuleOutput;
					}
				}
				return null;
			}
			set
			{
				for (int i = this.m_OverrideTilingRules.Count - 1; i >= 0; i--)
				{
					if (this.m_OverrideTilingRules[i].m_Id == originalRule.m_Id)
					{
						this.m_OverrideTilingRules.RemoveAt(i);
						break;
					}
				}
				if (value != null)
				{
					RuleTile.TilingRuleOutput tilingRuleOutput = JsonUtility.FromJson<RuleTile.TilingRuleOutput>(JsonUtility.ToJson(value));
					this.m_OverrideTilingRules.Add(tilingRuleOutput);
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003D58 File Offset: 0x00001F58
		public void ApplyOverrides(IList<KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRuleOutput>> overrides)
		{
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			for (int i = 0; i < overrides.Count; i++)
			{
				this[overrides[i].Key] = overrides[i].Value;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public void GetOverrides(List<KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRuleOutput>> overrides, ref int validCount)
		{
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			overrides.Clear();
			if (this.m_Tile)
			{
				foreach (RuleTile.TilingRule tilingRule in this.m_Tile.m_TilingRules)
				{
					RuleTile.TilingRuleOutput tilingRuleOutput = this[tilingRule];
					overrides.Add(new KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRuleOutput>(tilingRule, tilingRuleOutput));
				}
			}
			validCount = overrides.Count;
			using (List<RuleTile.TilingRuleOutput>.Enumerator enumerator2 = this.m_OverrideTilingRules.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					RuleTile.TilingRuleOutput overrideRule = enumerator2.Current;
					if (!overrides.Exists((KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRuleOutput> o) => o.Key.m_Id == overrideRule.m_Id))
					{
						RuleTile.TilingRule tilingRule2 = new RuleTile.TilingRule
						{
							m_Id = overrideRule.m_Id
						};
						overrides.Add(new KeyValuePair<RuleTile.TilingRule, RuleTile.TilingRuleOutput>(tilingRule2, overrideRule));
					}
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003EC4 File Offset: 0x000020C4
		public override void Override()
		{
			if (!this.m_Tile || !this.m_InstanceTile)
			{
				return;
			}
			base.PrepareOverride();
			RuleTile instanceTile = this.m_InstanceTile;
			instanceTile.m_DefaultSprite = this.m_DefaultSprite;
			instanceTile.m_DefaultGameObject = this.m_DefaultGameObject;
			instanceTile.m_DefaultColliderType = this.m_DefaultColliderType;
			foreach (RuleTile.TilingRule tilingRule in instanceTile.m_TilingRules)
			{
				RuleTile.TilingRuleOutput tilingRuleOutput = this[tilingRule];
				if (tilingRuleOutput != null)
				{
					JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(tilingRuleOutput), tilingRule);
				}
			}
		}

		// Token: 0x04000028 RID: 40
		public Sprite m_DefaultSprite;

		// Token: 0x04000029 RID: 41
		public GameObject m_DefaultGameObject;

		// Token: 0x0400002A RID: 42
		public Tile.ColliderType m_DefaultColliderType = Tile.ColliderType.Sprite;

		// Token: 0x0400002B RID: 43
		public List<RuleTile.TilingRuleOutput> m_OverrideTilingRules = new List<RuleTile.TilingRuleOutput>();
	}
}

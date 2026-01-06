using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x02000043 RID: 67
[CreateMenu("Replace Text", 0)]
public class ReplaceTextAction : ActionDataBase
{
	// Token: 0x06000130 RID: 304 RVA: 0x00008250 File Offset: 0x00006450
	public static string Replace(string s, List<ReplaceTextAction.Rule> rules)
	{
		using (List<ReplaceTextAction.Rule>.Enumerator enumerator = rules.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ReplaceTextAction.Rule rule = enumerator.Current;
				switch (rule.typeToGet)
				{
				case ReplaceTextAction.Rule.Type.OverworldResourceName:
				{
					string text = "";
					switch (rule.overworldResource)
					{
					case Overworld_ResourceManager.Resource.Type.Food:
						text = "Food";
						break;
					case Overworld_ResourceManager.Resource.Type.BuildingMaterial:
						text = "Material";
						break;
					case Overworld_ResourceManager.Resource.Type.Treasure:
						text = "Treasure";
						break;
					case Overworld_ResourceManager.Resource.Type.Population:
						text = "Population";
						break;
					}
					s = s.Replace(rule.stringToReplace, LangaugeManager.main.GetTextByKey(text));
					break;
				}
				case ReplaceTextAction.Rule.Type.OverworldResourceVal:
					s = s.Replace(rule.stringToReplace, Overworld_ResourceManager.main.resources.Find((Overworld_ResourceManager.Resource x) => x.type == rule.overworldResource).amount.ToString());
					break;
				case ReplaceTextAction.Rule.Type.ItemName:
					s = s.Replace(rule.stringToReplace, LangaugeManager.main.GetTextByKey(Item2.GetDisplayName(rule.item.name)));
					break;
				}
			}
		}
		return s;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x000083B0 File Offset: 0x000065B0
	public override void OnStart()
	{
		Overworld_ConversationManager.main.currentReplaceRules.Add(this.rule);
	}

	// Token: 0x040000C6 RID: 198
	[SerializeField]
	public ReplaceTextAction.Rule rule;

	// Token: 0x02000262 RID: 610
	[Serializable]
	public class Rule
	{
		// Token: 0x04000EFE RID: 3838
		public string stringToReplace = "/x";

		// Token: 0x04000EFF RID: 3839
		public ReplaceTextAction.Rule.Type typeToGet;

		// Token: 0x04000F00 RID: 3840
		public Overworld_ResourceManager.Resource.Type overworldResource;

		// Token: 0x04000F01 RID: 3841
		public Item2 item;

		// Token: 0x02000489 RID: 1161
		public enum Type
		{
			// Token: 0x04001A81 RID: 6785
			OverworldResourceName,
			// Token: 0x04001A82 RID: 6786
			OverworldResourceVal,
			// Token: 0x04001A83 RID: 6787
			ItemName
		}
	}
}

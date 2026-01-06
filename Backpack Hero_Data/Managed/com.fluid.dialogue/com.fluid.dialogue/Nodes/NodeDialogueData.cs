using System;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000016 RID: 22
	[CreateMenu("Dialogue", 1)]
	public class NodeDialogueData : NodeDataChoiceBase
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002B53 File Offset: 0x00000D53
		protected override string DefaultName
		{
			get
			{
				return "Dialogue";
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002B5A File Offset: 0x00000D5A
		public override string Text
		{
			get
			{
				return this.dialogue;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002B62 File Offset: 0x00000D62
		public new void Setup()
		{
			this.key = "n" + this.index.ToString();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002B80 File Offset: 0x00000D80
		public override INode GetRuntime(IGraph graphRuntime, IDialogueController controller)
		{
			return new NodeDialogue(graphRuntime, base.UniqueId, this.actor, this.dialogue, this.key, this.keyOverride, this.externalKey, this.prefix, this.audio, this.children.ToList<INodeData>(), this.choices.Select((ChoiceData c) => c.GetRuntime(graphRuntime, controller)).ToList<IChoice>(), this.conditions.Select((ConditionDataBase c) => c.GetRuntime(graphRuntime, controller)).ToList<ICondition>(), this.enterActions.Select((ActionDataBase a) => a.GetRuntime(graphRuntime, controller)).ToList<IAction>(), this.exitActions.Select((ActionDataBase a) => a.GetRuntime(graphRuntime, controller)).ToList<IAction>());
		}

		// Token: 0x04000029 RID: 41
		public ActorDefinition actor;

		// Token: 0x0400002A RID: 42
		public AudioClip audio;

		// Token: 0x0400002B RID: 43
		[TextArea]
		public string dialogue;

		// Token: 0x0400002C RID: 44
		public string key;

		// Token: 0x0400002D RID: 45
		public bool keyOverride;

		// Token: 0x0400002E RID: 46
		public bool externalKey;

		// Token: 0x0400002F RID: 47
		[HideInInspector]
		public string prefix;
	}
}

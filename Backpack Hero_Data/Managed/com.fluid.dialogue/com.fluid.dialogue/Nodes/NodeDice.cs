using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Conditions;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000017 RID: 23
	public class NodeDice : INode, IUniqueId
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002C5F File Offset: 0x00000E5F
		public string UniqueId { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002C67 File Offset: 0x00000E67
		public List<IAction> EnterActions { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002C6F File Offset: 0x00000E6F
		public List<IAction> ExitActions { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002C77 File Offset: 0x00000E77
		public virtual bool IsValid
		{
			get
			{
				return this._conditions.Find((ICondition c) => !c.GetIsValid(this)) == null;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002C93 File Offset: 0x00000E93
		public List<IChoice> HubChoices
		{
			get
			{
				return this._choiceList.Where((IChoice c) => c.IsValid).ToList<IChoice>();
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public NodeDice(string uniqueId, List<IChoice> choiceList, List<ICondition> conditions, List<IAction> enter, List<IAction> exit)
		{
			this.UniqueId = uniqueId;
			this._choiceList = choiceList;
			this._conditions = conditions;
			this.EnterActions = enter;
			this.ExitActions = exit;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002CF1 File Offset: 0x00000EF1
		public INode Next()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public void Play(IDialoguePlayback playback)
		{
			int num = Random.Range(0, this.HubChoices.Count);
			playback.SelectChoice(num);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002D1E File Offset: 0x00000F1E
		public IChoice GetChoice(int index)
		{
			return this.HubChoices[index];
		}

		// Token: 0x04000030 RID: 48
		private readonly List<IChoice> _choiceList;

		// Token: 0x04000031 RID: 49
		private List<ICondition> _conditions;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Conditions;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000013 RID: 19
	public class NodeChoiceHub : INode, IUniqueId
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002811 File Offset: 0x00000A11
		public string UniqueId { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002819 File Offset: 0x00000A19
		public List<IAction> EnterActions { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002821 File Offset: 0x00000A21
		public List<IAction> ExitActions { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002829 File Offset: 0x00000A29
		public virtual bool IsValid
		{
			get
			{
				return this._conditions.Find((ICondition c) => !c.GetIsValid(this)) == null;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002845 File Offset: 0x00000A45
		public List<IChoice> HubChoices
		{
			get
			{
				return this._choiceList.Where((IChoice c) => c.IsValid).ToList<IChoice>();
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002876 File Offset: 0x00000A76
		public NodeChoiceHub(string uniqueId, List<IChoice> choiceList, List<ICondition> conditions)
		{
			this.UniqueId = uniqueId;
			this._choiceList = choiceList;
			this._conditions = conditions;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002893 File Offset: 0x00000A93
		public INode Next()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000289A File Offset: 0x00000A9A
		public void Play(IDialoguePlayback playback)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000028A1 File Offset: 0x00000AA1
		public IChoice GetChoice(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400001B RID: 27
		private readonly List<IChoice> _choiceList;

		// Token: 0x0400001C RID: 28
		private List<ICondition> _conditions;
	}
}

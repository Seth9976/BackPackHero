using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x0200001E RID: 30
	public abstract class NodeBase : INode, IUniqueId
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002F7B File Offset: 0x0000117B
		public List<IAction> EnterActions { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002F83 File Offset: 0x00001183
		public List<IAction> ExitActions { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002F8B File Offset: 0x0000118B
		public virtual bool IsValid
		{
			get
			{
				return this._conditions.Find((ICondition c) => !c.GetIsValid(this)) == null;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002FA7 File Offset: 0x000011A7
		public List<IChoice> HubChoices { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002FAF File Offset: 0x000011AF
		public string UniqueId { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002FB8 File Offset: 0x000011B8
		protected List<INode> Children
		{
			get
			{
				List<INode> list;
				if ((list = this._childrenRuntimeCache) == null)
				{
					list = (this._childrenRuntimeCache = this._children.Select(new Func<INodeData, INode>(this._runtime.GetCopy)).ToList<INode>());
				}
				return list;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002FFA File Offset: 0x000011FA
		protected NodeBase(IGraph runtime, string uniqueId, List<INodeData> children, List<ICondition> conditions, List<IAction> enterActions, List<IAction> exitActions)
		{
			this._runtime = runtime;
			this.UniqueId = uniqueId;
			this._children = children;
			this._conditions = conditions;
			this.EnterActions = enterActions;
			this.ExitActions = exitActions;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000302F File Offset: 0x0000122F
		public INode Next()
		{
			return this.Children.Find((INode n) => n.IsValid);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000305B File Offset: 0x0000125B
		public void Play(IDialoguePlayback playback)
		{
			playback.Events.NodeEnter.Invoke(this);
			this.OnPlay(playback);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003075 File Offset: 0x00001275
		protected virtual void OnPlay(IDialoguePlayback playback)
		{
			playback.Next();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000307D File Offset: 0x0000127D
		public virtual IChoice GetChoice(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000035 RID: 53
		private readonly List<INodeData> _children;

		// Token: 0x04000036 RID: 54
		private readonly List<ICondition> _conditions;

		// Token: 0x04000037 RID: 55
		private readonly IGraph _runtime;

		// Token: 0x04000038 RID: 56
		private List<INode> _childrenRuntimeCache;
	}
}

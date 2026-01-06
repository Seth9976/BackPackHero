using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000021 RID: 33
	public abstract class NodeDataBase : ScriptableObject, INodeData, IGetRuntime<INode>, ISetup, IUniqueId, IConnectionChildCollection
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003090 File Offset: 0x00001290
		public string UniqueId
		{
			get
			{
				return this._uniqueId;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003098 File Offset: 0x00001298
		protected virtual string DefaultName { get; } = "Untitled";

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000030A0 File Offset: 0x000012A0
		public IReadOnlyList<NodeDataBase> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000030A8 File Offset: 0x000012A8
		public virtual bool HideInspectorActions
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000030AB File Offset: 0x000012AB
		public virtual string Text
		{
			get
			{
				return "";
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000030B2 File Offset: 0x000012B2
		public virtual List<ChoiceData> Choices { get; } = new List<ChoiceData>();

		// Token: 0x060000A5 RID: 165 RVA: 0x000030BC File Offset: 0x000012BC
		public void Setup()
		{
			this.index = this.index;
			this._uniqueId = Guid.NewGuid().ToString();
			base.name = this.DefaultName;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000030FA File Offset: 0x000012FA
		public void SetIndex(int index)
		{
			this.index = index;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003103 File Offset: 0x00001303
		public void AddConnectionChild(NodeDataBase child)
		{
			this.children.Add(child);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003111 File Offset: 0x00001311
		public void RemoveConnectionChild(NodeDataBase child)
		{
			this.children.Remove(child);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003120 File Offset: 0x00001320
		public virtual void SortConnectionsByPosition()
		{
			this.children = this.children.OrderBy((NodeDataBase i) => i.rect.yMin).ToList<NodeDataBase>();
		}

		// Token: 0x060000AA RID: 170
		public abstract INode GetRuntime(IGraph graphRuntime, IDialogueController dialogue);

		// Token: 0x060000AB RID: 171 RVA: 0x00003157 File Offset: 0x00001357
		public virtual void ClearConnectionChildren()
		{
			this.children.Clear();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003164 File Offset: 0x00001364
		public virtual NodeDataBase GetDataCopy()
		{
			NodeDataBase nodeDataBase = Object.Instantiate<NodeDataBase>(this);
			nodeDataBase.conditions = this.conditions.Select(new Func<ConditionDataBase, ConditionDataBase>(Object.Instantiate<ConditionDataBase>)).ToList<ConditionDataBase>();
			nodeDataBase.enterActions = this.enterActions.Select(new Func<ActionDataBase, ActionDataBase>(Object.Instantiate<ActionDataBase>)).ToList<ActionDataBase>();
			nodeDataBase.exitActions = this.exitActions.Select(new Func<ActionDataBase, ActionDataBase>(Object.Instantiate<ActionDataBase>)).ToList<ActionDataBase>();
			return nodeDataBase;
		}

		// Token: 0x0400003D RID: 61
		[HideInInspector]
		[SerializeField]
		private string _uniqueId;

		// Token: 0x0400003E RID: 62
		[HideInInspector]
		public Rect rect;

		// Token: 0x0400003F RID: 63
		public int index;

		// Token: 0x04000040 RID: 64
		public string nodeTitle;

		// Token: 0x04000041 RID: 65
		[HideInInspector]
		public List<NodeDataBase> children = new List<NodeDataBase>();

		// Token: 0x04000042 RID: 66
		[HideInInspector]
		public List<ConditionDataBase> conditions = new List<ConditionDataBase>();

		// Token: 0x04000043 RID: 67
		[HideInInspector]
		public List<ActionDataBase> enterActions = new List<ActionDataBase>();

		// Token: 0x04000044 RID: 68
		[HideInInspector]
		public List<ActionDataBase> exitActions = new List<ActionDataBase>();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Graphs;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Choices
{
	// Token: 0x0200002D RID: 45
	public class ChoiceData : ScriptableObject, IGetRuntime<IChoice>, ISetup, IUniqueId, IConnectionChildCollection
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003F73 File Offset: 0x00002173
		public string UniqueId
		{
			get
			{
				return this._uniqueId;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003F7B File Offset: 0x0000217B
		public IReadOnlyList<NodeDataBase> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003F84 File Offset: 0x00002184
		public void Setup()
		{
			base.name = "Choice";
			this._uniqueId = Guid.NewGuid().ToString();
			this.key = "n" + this.parentIndex.ToString() + "c" + this.index.ToString();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003FE0 File Offset: 0x000021E0
		public void SetIndex(int index, int parentIndex)
		{
			this.index = index;
			this.parentIndex = parentIndex;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003FF0 File Offset: 0x000021F0
		public IChoice GetRuntime(IGraph graphRuntime, IDialogueController dialogue)
		{
			return new ChoiceRuntime(graphRuntime, this.text, this.key, this.keyOverride, this.externalKey, this.prefix, this._uniqueId, this.children.ToList<INodeData>());
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004027 File Offset: 0x00002227
		public void AddConnectionChild(NodeDataBase child)
		{
			this.children.Add(child);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004035 File Offset: 0x00002235
		public void RemoveConnectionChild(NodeDataBase child)
		{
			this.children.Remove(child);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004044 File Offset: 0x00002244
		public void SortConnectionsByPosition()
		{
			this.children = this.children.OrderBy((NodeDataBase i) => i.rect.yMin).ToList<NodeDataBase>();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000407B File Offset: 0x0000227B
		public void ClearConnectionChildren()
		{
			this.children.Clear();
		}

		// Token: 0x04000051 RID: 81
		public string text;

		// Token: 0x04000052 RID: 82
		public string key;

		// Token: 0x04000053 RID: 83
		public bool keyOverride;

		// Token: 0x04000054 RID: 84
		public bool externalKey;

		// Token: 0x04000055 RID: 85
		public string prefix;

		// Token: 0x04000056 RID: 86
		public int index;

		// Token: 0x04000057 RID: 87
		public int parentIndex;

		// Token: 0x04000058 RID: 88
		[HideInInspector]
		public List<NodeDataBase> children = new List<NodeDataBase>();

		// Token: 0x04000059 RID: 89
		[HideInInspector]
		[SerializeField]
		private string _uniqueId;
	}
}

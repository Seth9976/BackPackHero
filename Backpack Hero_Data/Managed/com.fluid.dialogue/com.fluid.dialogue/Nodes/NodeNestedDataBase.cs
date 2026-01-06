using System;
using CleverCrow.Fluid.Dialogues.Graphs;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000023 RID: 35
	public abstract class NodeNestedDataBase<T> : ScriptableObject, IGetRuntime<T>, ISetup, IUniqueId
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000033A3 File Offset: 0x000015A3
		public string UniqueId
		{
			get
			{
				return this._uniqueId;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000033AC File Offset: 0x000015AC
		public void Setup()
		{
			if (string.IsNullOrEmpty(this._title))
			{
				this._title = base.GetType().Name;
			}
			base.name = base.GetType().Name;
			this._uniqueId = Guid.NewGuid().ToString();
		}

		// Token: 0x060000B5 RID: 181
		public abstract T GetRuntime(IGraph graphRuntime, IDialogueController dialogue);

		// Token: 0x04000048 RID: 72
		[SerializeField]
		public string _title;

		// Token: 0x04000049 RID: 73
		[SerializeField]
		protected string _uniqueId;
	}
}

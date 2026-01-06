using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200015C RID: 348
	public abstract class MultiInputUnit<T> : Unit, IMultiInputUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00010478 File Offset: 0x0000E678
		[DoNotSerialize]
		protected virtual int minInputCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0001047B File Offset: 0x0000E67B
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x00010483 File Offset: 0x0000E683
		[DoNotSerialize]
		[Inspectable]
		[UnitHeaderInspectable("Inputs")]
		public virtual int inputCount
		{
			get
			{
				return this._inputCount;
			}
			set
			{
				this._inputCount = Mathf.Clamp(value, this.minInputCount, 10);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00010499 File Offset: 0x0000E699
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x000104A1 File Offset: 0x0000E6A1
		[DoNotSerialize]
		public ReadOnlyCollection<ValueInput> multiInputs { get; protected set; }

		// Token: 0x0600090A RID: 2314 RVA: 0x000104AC File Offset: 0x0000E6AC
		protected override void Definition()
		{
			List<ValueInput> list = new List<ValueInput>();
			this.multiInputs = list.AsReadOnly();
			for (int i = 0; i < this.inputCount; i++)
			{
				list.Add(base.ValueInput<T>(i.ToString()));
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000104F0 File Offset: 0x0000E6F0
		protected void InputsAllowNull()
		{
			foreach (ValueInput valueInput in this.multiInputs)
			{
				valueInput.AllowsNull();
			}
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0001054B File Offset: 0x0000E74B
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}

		// Token: 0x040001FC RID: 508
		[SerializeAs("inputCount")]
		private int _inputCount = 2;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000043 RID: 67
	[UnitCategory("Control")]
	[UnitOrder(13)]
	public sealed class Sequence : Unit
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00007677 File Offset: 0x00005877
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000767F File Offset: 0x0000587F
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00007688 File Offset: 0x00005888
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00007690 File Offset: 0x00005890
		[DoNotSerialize]
		[Inspectable]
		[InspectorLabel("Steps")]
		[UnitHeaderInspectable("Steps")]
		public int outputCount
		{
			get
			{
				return this._outputCount;
			}
			set
			{
				this._outputCount = Mathf.Clamp(value, 1, 10);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000076A1 File Offset: 0x000058A1
		// (set) Token: 0x0600029A RID: 666 RVA: 0x000076A9 File Offset: 0x000058A9
		[DoNotSerialize]
		public ReadOnlyCollection<ControlOutput> multiOutputs { get; private set; }

		// Token: 0x0600029B RID: 667 RVA: 0x000076B4 File Offset: 0x000058B4
		protected override void Definition()
		{
			this.enter = base.ControlInputCoroutine("enter", new Func<Flow, ControlOutput>(this.Enter), new Func<Flow, IEnumerator>(this.EnterCoroutine));
			List<ControlOutput> list = new List<ControlOutput>();
			this.multiOutputs = list.AsReadOnly();
			for (int i = 0; i < this.outputCount; i++)
			{
				ControlOutput controlOutput = base.ControlOutput(i.ToString());
				base.Succession(this.enter, controlOutput);
				list.Add(controlOutput);
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00007730 File Offset: 0x00005930
		private ControlOutput Enter(Flow flow)
		{
			GraphStack graphStack = flow.PreserveStack();
			foreach (ControlOutput controlOutput in this.multiOutputs)
			{
				flow.Invoke(controlOutput);
				flow.RestoreStack(graphStack);
			}
			flow.DisposePreservedStack(graphStack);
			return null;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00007794 File Offset: 0x00005994
		private IEnumerator EnterCoroutine(Flow flow)
		{
			GraphStack stack = flow.PreserveStack();
			foreach (ControlOutput controlOutput in this.multiOutputs)
			{
				yield return controlOutput;
				flow.RestoreStack(stack);
			}
			IEnumerator<ControlOutput> enumerator = null;
			flow.DisposePreservedStack(stack);
			yield break;
			yield break;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x000077AA File Offset: 0x000059AA
		public void CopyFrom(Sequence source)
		{
			base.CopyFrom(source);
			this.outputCount = source.outputCount;
		}

		// Token: 0x040000C5 RID: 197
		[SerializeAs("outputCount")]
		private int _outputCount = 2;
	}
}

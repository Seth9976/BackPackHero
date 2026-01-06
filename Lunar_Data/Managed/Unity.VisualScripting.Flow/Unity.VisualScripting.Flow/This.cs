using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200012D RID: 301
	[SpecialUnit]
	[RenamedFrom("Bolt.Self")]
	[RenamedFrom("Unity.VisualScripting.Self")]
	public sealed class This : Unit
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0000E86B File Offset: 0x0000CA6B
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x0000E873 File Offset: 0x0000CA73
		[DoNotSerialize]
		[PortLabelHidden]
		[PortLabel("This")]
		public ValueOutput self { get; private set; }

		// Token: 0x060007D3 RID: 2003 RVA: 0x0000E87C File Offset: 0x0000CA7C
		protected override void Definition()
		{
			this.self = base.ValueOutput<GameObject>("self", new Func<Flow, GameObject>(this.Result)).PredictableIf(new Func<Flow, bool>(this.IsPredictable));
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0000E8AC File Offset: 0x0000CAAC
		private GameObject Result(Flow flow)
		{
			return flow.stack.self;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000E8B9 File Offset: 0x0000CAB9
		private bool IsPredictable(Flow flow)
		{
			return flow.stack.self != null;
		}
	}
}

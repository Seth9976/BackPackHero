using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000B1 RID: 177
	[UnitCategory("Graphs/Graph Nodes")]
	public abstract class SetGraph<TGraph, TMacro, TMachine> : Unit where TGraph : class, IGraph, new() where TMacro : Macro<TGraph> where TMachine : Machine<TGraph, TMacro>
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000AC98 File Offset: 0x00008E98
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; protected set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000ACA9 File Offset: 0x00008EA9
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0000ACB1 File Offset: 0x00008EB1
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput target { get; protected set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000ACBA File Offset: 0x00008EBA
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x0000ACC2 File Offset: 0x00008EC2
		[DoNotSerialize]
		[PortLabel("Graph")]
		[PortLabelHidden]
		public ValueInput graphInput { get; protected set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000ACCB File Offset: 0x00008ECB
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0000ACD3 File Offset: 0x00008ED3
		[DoNotSerialize]
		[PortLabel("Graph")]
		[PortLabelHidden]
		public ValueOutput graphOutput { get; protected set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000ACDC File Offset: 0x00008EDC
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0000ACE4 File Offset: 0x00008EE4
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; protected set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000526 RID: 1318
		protected abstract bool isGameObject { get; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000ACED File Offset: 0x00008EED
		private Type targetType
		{
			get
			{
				if (!this.isGameObject)
				{
					return typeof(TMachine);
				}
				return typeof(GameObject);
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000AD0C File Offset: 0x00008F0C
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.SetMacro));
			this.target = base.ValueInput(this.targetType, "target").NullMeansSelf();
			this.target.SetDefaultValue(this.targetType.PseudoDefault());
			this.graphInput = base.ValueInput<TMacro>("graphInput", default(TMacro));
			this.graphOutput = base.ValueOutput<TMacro>("graphOutput");
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.graphInput, this.enter);
			base.Assignment(this.enter, this.graphOutput);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000ADDC File Offset: 0x00008FDC
		private ControlOutput SetMacro(Flow flow)
		{
			TMacro value = flow.GetValue<TMacro>(this.graphInput);
			object value2 = flow.GetValue(this.target, this.targetType);
			GameObject gameObject = value2 as GameObject;
			if (gameObject != null)
			{
				gameObject.GetComponent<TMachine>().nest.SwitchToMacro(value);
			}
			else
			{
				((TMachine)((object)value2)).nest.SwitchToMacro(value);
			}
			flow.SetValue(this.graphOutput, value);
			return this.exit;
		}
	}
}

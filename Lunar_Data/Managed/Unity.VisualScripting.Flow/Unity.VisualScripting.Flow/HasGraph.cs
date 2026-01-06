using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000AE RID: 174
	[UnitCategory("Graphs/Graph Nodes")]
	public abstract class HasGraph<TGraph, TMacro, TMachine> : Unit where TGraph : class, IGraph, new() where TMacro : Macro<TGraph> where TMachine : Machine<TGraph, TMacro>
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000AA12 File Offset: 0x00008C12
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x0000AA1A File Offset: 0x00008C1A
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000AA23 File Offset: 0x00008C23
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0000AA2B File Offset: 0x00008C2B
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput target { get; private set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000AA34 File Offset: 0x00008C34
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0000AA3C File Offset: 0x00008C3C
		[DoNotSerialize]
		[PortLabel("Graph")]
		[PortLabelHidden]
		public ValueInput graphInput { get; private set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000AA45 File Offset: 0x00008C45
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x0000AA4D File Offset: 0x00008C4D
		[DoNotSerialize]
		[PortLabel("Has Graph")]
		[PortLabelHidden]
		public ValueOutput hasGraphOutput { get; private set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000AA56 File Offset: 0x00008C56
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0000AA5E File Offset: 0x00008C5E
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000512 RID: 1298
		protected abstract bool isGameObject { get; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000AA67 File Offset: 0x00008C67
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

		// Token: 0x06000514 RID: 1300 RVA: 0x0000AA88 File Offset: 0x00008C88
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.TriggerHasGraph));
			this.target = base.ValueInput(this.targetType, "target").NullMeansSelf();
			this.target.SetDefaultValue(this.targetType.PseudoDefault());
			this.graphInput = base.ValueInput<TMacro>("graphInput", default(TMacro));
			this.hasGraphOutput = base.ValueOutput<bool>("hasGraphOutput", new Func<Flow, bool>(this.OutputHasGraph));
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.graphInput, this.enter);
			base.Assignment(this.enter, this.hasGraphOutput);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000AB62 File Offset: 0x00008D62
		private ControlOutput TriggerHasGraph(Flow flow)
		{
			flow.SetValue(this.hasGraphOutput, this.OutputHasGraph(flow));
			return this.exit;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000AB84 File Offset: 0x00008D84
		private bool OutputHasGraph(Flow flow)
		{
			TMacro macro = flow.GetValue<TMacro>(this.graphInput);
			GameObject gameObject = flow.GetValue(this.target, this.targetType) as GameObject;
			if (gameObject != null)
			{
				if (gameObject != null)
				{
					IEnumerable<TMachine> components = gameObject.GetComponents<TMachine>();
					macro = flow.GetValue<TMacro>(this.graphInput);
					return components.Where((TMachine currentMachine) => currentMachine != null).Any((TMachine currentMachine) => currentMachine.graph != null && currentMachine.graph.Equals(macro.graph));
				}
			}
			else
			{
				TMachine value = flow.GetValue<TMachine>(this.target);
				if (value.graph != null && value.graph.Equals(macro.graph))
				{
					return true;
				}
			}
			return false;
		}
	}
}

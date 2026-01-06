using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000A8 RID: 168
	[UnitSurtitle("Custom Event")]
	[UnitShortTitle("Trigger")]
	[TypeIcon(typeof(CustomEvent))]
	[UnitCategory("Events")]
	[UnitOrder(1)]
	public sealed class TriggerCustomEvent : Unit
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000A175 File Offset: 0x00008375
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x0000A17D File Offset: 0x0000837D
		[DoNotSerialize]
		public List<ValueInput> arguments { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000A186 File Offset: 0x00008386
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0000A18E File Offset: 0x0000838E
		[DoNotSerialize]
		[Inspectable]
		[UnitHeaderInspectable("Arguments")]
		public int argumentCount
		{
			get
			{
				return this._argumentCount;
			}
			set
			{
				this._argumentCount = Mathf.Clamp(value, 0, 10);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000A19F File Offset: 0x0000839F
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0000A1A7 File Offset: 0x000083A7
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000A1B0 File Offset: 0x000083B0
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0000A1B8 File Offset: 0x000083B8
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput name { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000A1C1 File Offset: 0x000083C1
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0000A1C9 File Offset: 0x000083C9
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput target { get; private set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000A1D2 File Offset: 0x000083D2
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0000A1DA File Offset: 0x000083DA
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000A1E4 File Offset: 0x000083E4
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Trigger));
			this.exit = base.ControlOutput("exit");
			this.name = base.ValueInput<string>("name", string.Empty);
			this.target = base.ValueInput<GameObject>("target", null).NullMeansSelf();
			this.arguments = new List<ValueInput>();
			for (int i = 0; i < this.argumentCount; i++)
			{
				ValueInput valueInput = base.ValueInput<object>("argument_" + i.ToString());
				this.arguments.Add(valueInput);
				base.Requirement(valueInput, this.enter);
			}
			base.Requirement(this.name, this.enter);
			base.Requirement(this.target, this.enter);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000A2D0 File Offset: 0x000084D0
		private ControlOutput Trigger(Flow flow)
		{
			GameObject value = flow.GetValue<GameObject>(this.target);
			string value2 = flow.GetValue<string>(this.name);
			object[] array = this.arguments.Select(new Func<ValueInput, object>(flow.GetConvertedValue)).ToArray<object>();
			CustomEvent.Trigger(value, value2, array);
			return this.exit;
		}

		// Token: 0x04000134 RID: 308
		[SerializeAs("argumentCount")]
		private int _argumentCount;
	}
}

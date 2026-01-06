using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000096 RID: 150
	[UnitCategory("Events/Physics")]
	public sealed class OnJointBreak : GameObjectEventUnit<float>
	{
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00009B0A File Offset: 0x00007D0A
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnJointBreakMessageListener);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00009B16 File Offset: 0x00007D16
		protected override string hookName
		{
			get
			{
				return "OnJointBreak";
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00009B1D File Offset: 0x00007D1D
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00009B25 File Offset: 0x00007D25
		[DoNotSerialize]
		public ValueOutput breakForce { get; private set; }

		// Token: 0x0600047A RID: 1146 RVA: 0x00009B2E File Offset: 0x00007D2E
		protected override void Definition()
		{
			base.Definition();
			this.breakForce = base.ValueOutput<float>("breakForce");
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00009B47 File Offset: 0x00007D47
		protected override void AssignArguments(Flow flow, float breakForce)
		{
			flow.SetValue(this.breakForce, breakForce);
		}
	}
}

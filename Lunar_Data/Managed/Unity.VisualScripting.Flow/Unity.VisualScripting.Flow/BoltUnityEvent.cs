using System;
using System.ComponentModel;

namespace Unity.VisualScripting
{
	// Token: 0x02000056 RID: 86
	[UnitCategory("Events")]
	[UnitTitle("UnityEvent")]
	[UnitOrder(2)]
	[DisplayName("Visual Scripting Unity Event")]
	public sealed class BoltUnityEvent : MachineEventUnit<string>
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00008850 File Offset: 0x00006A50
		protected override string hookName
		{
			get
			{
				return "UnityEvent";
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00008857 File Offset: 0x00006A57
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000885F File Offset: 0x00006A5F
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput name { get; private set; }

		// Token: 0x0600034E RID: 846 RVA: 0x00008868 File Offset: 0x00006A68
		protected override void Definition()
		{
			base.Definition();
			this.name = base.ValueInput<string>("name", string.Empty);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00008886 File Offset: 0x00006A86
		protected override bool ShouldTrigger(Flow flow, string name)
		{
			return EventUnit<string>.CompareNames(flow, this.name, name);
		}
	}
}

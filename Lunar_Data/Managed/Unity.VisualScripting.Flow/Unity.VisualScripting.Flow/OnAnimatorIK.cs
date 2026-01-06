using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200004F RID: 79
	[UnitCategory("Events/Animation")]
	public sealed class OnAnimatorIK : GameObjectEventUnit<int>
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00008791 File Offset: 0x00006991
		public override Type MessageListenerType
		{
			get
			{
				return typeof(AnimatorMessageListener);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000879D File Offset: 0x0000699D
		protected override string hookName
		{
			get
			{
				return "OnAnimatorIK";
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000087A4 File Offset: 0x000069A4
		// (set) Token: 0x0600033A RID: 826 RVA: 0x000087AC File Offset: 0x000069AC
		[DoNotSerialize]
		public ValueOutput layerIndex { get; private set; }

		// Token: 0x0600033B RID: 827 RVA: 0x000087B5 File Offset: 0x000069B5
		protected override void Definition()
		{
			base.Definition();
			this.layerIndex = base.ValueOutput<int>("layerIndex");
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000087CE File Offset: 0x000069CE
		protected override void AssignArguments(Flow flow, int layerIndex)
		{
			flow.SetValue(this.layerIndex, layerIndex);
		}
	}
}

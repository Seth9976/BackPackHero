using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200004B RID: 75
	[UnitCategory("Control")]
	[UnitOrder(17)]
	[UnitFooterPorts(ControlOutputs = true)]
	public sealed class TryCatch : Unit
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000082C1 File Offset: 0x000064C1
		// (set) Token: 0x06000307 RID: 775 RVA: 0x000082C9 File Offset: 0x000064C9
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000308 RID: 776 RVA: 0x000082D2 File Offset: 0x000064D2
		// (set) Token: 0x06000309 RID: 777 RVA: 0x000082DA File Offset: 0x000064DA
		[DoNotSerialize]
		public ControlOutput @try { get; private set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600030A RID: 778 RVA: 0x000082E3 File Offset: 0x000064E3
		// (set) Token: 0x0600030B RID: 779 RVA: 0x000082EB File Offset: 0x000064EB
		[DoNotSerialize]
		public ControlOutput @catch { get; private set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600030C RID: 780 RVA: 0x000082F4 File Offset: 0x000064F4
		// (set) Token: 0x0600030D RID: 781 RVA: 0x000082FC File Offset: 0x000064FC
		[DoNotSerialize]
		public ControlOutput @finally { get; private set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00008305 File Offset: 0x00006505
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000830D File Offset: 0x0000650D
		[DoNotSerialize]
		public ValueOutput exception { get; private set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00008316 File Offset: 0x00006516
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000831E File Offset: 0x0000651E
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable]
		[TypeFilter(new Type[] { typeof(Exception) }, Matching = TypesMatching.AssignableToAll)]
		[TypeSet(TypeSet.SettingsAssembliesTypes)]
		public Type exceptionType { get; set; } = typeof(Exception);

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00008327 File Offset: 0x00006527
		public override bool canDefine
		{
			get
			{
				return this.exceptionType != null && typeof(Exception).IsAssignableFrom(this.exceptionType);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00008350 File Offset: 0x00006550
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Enter));
			this.@try = base.ControlOutput("try");
			this.@catch = base.ControlOutput("catch");
			this.@finally = base.ControlOutput("finally");
			this.exception = base.ValueOutput(this.exceptionType, "exception");
			base.Assignment(this.enter, this.exception);
			base.Succession(this.enter, this.@try);
			base.Succession(this.enter, this.@catch);
			base.Succession(this.enter, this.@finally);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000840C File Offset: 0x0000660C
		public ControlOutput Enter(Flow flow)
		{
			if (flow.isCoroutine)
			{
				throw new NotSupportedException("Coroutines cannot catch exceptions.");
			}
			try
			{
				flow.Invoke(this.@try);
			}
			catch (Exception ex)
			{
				if (!this.exceptionType.IsInstanceOfType(ex))
				{
					throw;
				}
				flow.SetValue(this.exception, ex);
				flow.Invoke(this.@catch);
			}
			finally
			{
				flow.Invoke(this.@finally);
			}
			return null;
		}
	}
}

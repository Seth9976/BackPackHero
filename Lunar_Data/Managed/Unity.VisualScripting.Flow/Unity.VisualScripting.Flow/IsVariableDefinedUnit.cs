using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200014A RID: 330
	[UnitShortTitle("Is Variable Defined")]
	public abstract class IsVariableDefinedUnit : VariableUnit
	{
		// Token: 0x06000890 RID: 2192 RVA: 0x0000FD59 File Offset: 0x0000DF59
		protected IsVariableDefinedUnit()
		{
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0000FD61 File Offset: 0x0000DF61
		protected IsVariableDefinedUnit(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0000FD6A File Offset: 0x0000DF6A
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0000FD72 File Offset: 0x0000DF72
		[DoNotSerialize]
		[PortLabel("Defined")]
		[PortLabelHidden]
		public new ValueOutput isDefined { get; private set; }

		// Token: 0x06000894 RID: 2196 RVA: 0x0000FD7B File Offset: 0x0000DF7B
		protected override void Definition()
		{
			base.Definition();
			this.isDefined = base.ValueOutput<bool>("isDefined", new Func<Flow, bool>(this.IsDefined));
			base.Requirement(base.name, this.isDefined);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		protected virtual bool IsDefined(Flow flow)
		{
			string value = flow.GetValue<string>(base.name);
			return this.GetDeclarations(flow).IsDefined(value);
		}
	}
}

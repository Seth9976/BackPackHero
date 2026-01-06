using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200013F RID: 319
	[UnitShortTitle("Get Variable")]
	public abstract class GetVariableUnit : VariableUnit
	{
		// Token: 0x06000872 RID: 2162 RVA: 0x0000FB65 File Offset: 0x0000DD65
		protected GetVariableUnit()
		{
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0000FB6D File Offset: 0x0000DD6D
		protected GetVariableUnit(string defaultName)
			: base(defaultName)
		{
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0000FB76 File Offset: 0x0000DD76
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x0000FB7E File Offset: 0x0000DD7E
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput value { get; private set; }

		// Token: 0x06000876 RID: 2166 RVA: 0x0000FB88 File Offset: 0x0000DD88
		protected override void Definition()
		{
			base.Definition();
			this.value = base.ValueOutput<object>("value", new Func<Flow, object>(this.Get)).PredictableIf(new Func<Flow, bool>(this.IsDefined));
			base.Requirement(base.name, this.value);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
		protected virtual bool IsDefined(Flow flow)
		{
			string value = flow.GetValue<string>(base.name);
			VariableDeclarations declarations = this.GetDeclarations(flow);
			return declarations != null && declarations.IsDefined(value);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0000FC10 File Offset: 0x0000DE10
		protected virtual object Get(Flow flow)
		{
			string value = flow.GetValue<string>(base.name);
			return this.GetDeclarations(flow).Get(value);
		}
	}
}

using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000179 RID: 377
	public abstract class ValuePortDefinition : UnitPortDefinition, IUnitValuePortDefinition, IUnitPortDefinition
	{
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x000119E9 File Offset: 0x0000FBE9
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x000119F1 File Offset: 0x0000FBF1
		[SerializeAs("_type")]
		private Type _type { get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x000119FA File Offset: 0x0000FBFA
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x00011A02 File Offset: 0x0000FC02
		[Inspectable]
		[DoNotSerialize]
		public virtual Type type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00011A0B File Offset: 0x0000FC0B
		public override bool isValid
		{
			get
			{
				return base.isValid && this.type != null;
			}
		}
	}
}

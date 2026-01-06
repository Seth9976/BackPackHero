using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000176 RID: 374
	public sealed class ValueInputDefinition : ValuePortDefinition, IUnitInputPortDefinition, IUnitPortDefinition
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000116C1 File Offset: 0x0000F8C1
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x000116CC File Offset: 0x0000F8CC
		[Inspectable]
		[DoNotSerialize]
		public override Type type
		{
			get
			{
				return base.type;
			}
			set
			{
				base.type = value;
				if (!this.type.IsAssignableFrom(this.defaultValue))
				{
					if (ValueInput.SupportsDefaultValue(this.type))
					{
						this._defaultvalue = this.type.PseudoDefault();
						return;
					}
					this.hasDefaultValue = false;
					this._defaultvalue = null;
				}
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00011720 File Offset: 0x0000F920
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00011728 File Offset: 0x0000F928
		[Serialize]
		[Inspectable]
		public bool hasDefaultValue { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00011731 File Offset: 0x0000F931
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x0001173C File Offset: 0x0000F93C
		[DoNotSerialize]
		[Inspectable]
		public object defaultValue
		{
			get
			{
				return this._defaultvalue;
			}
			set
			{
				if (this.type == null)
				{
					throw new InvalidOperationException("A type must be defined before setting the default value.");
				}
				if (!ValueInput.SupportsDefaultValue(this.type))
				{
					throw new InvalidOperationException("The selected type does not support default values.");
				}
				Ensure.That("value").IsOfType<object>(value, this.type);
				this._defaultvalue = value;
			}
		}

		// Token: 0x0400020C RID: 524
		[SerializeAs("defaultValue")]
		private object _defaultvalue;
	}
}

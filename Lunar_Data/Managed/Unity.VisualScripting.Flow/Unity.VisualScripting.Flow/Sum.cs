using System;
using System.Collections.Generic;
using System.Linq;

namespace Unity.VisualScripting
{
	// Token: 0x020000EE RID: 238
	[UnitOrder(303)]
	[TypeIcon(typeof(Add<>))]
	public abstract class Sum<T> : MultiInputUnit<T>
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0000D5D5 File Offset: 0x0000B7D5
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0000D5DD File Offset: 0x0000B7DD
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput sum { get; private set; }

		// Token: 0x060006FB RID: 1787 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		protected override void Definition()
		{
			IDefaultValue<T> defaultValue = this as IDefaultValue<T>;
			if (defaultValue != null)
			{
				List<ValueInput> list = new List<ValueInput>();
				base.multiInputs = list.AsReadOnly();
				for (int i = 0; i < this.inputCount; i++)
				{
					if (i == 0)
					{
						list.Add(base.ValueInput<T>(i.ToString()));
					}
					else
					{
						list.Add(base.ValueInput<T>(i.ToString(), defaultValue.defaultValue));
					}
				}
			}
			else
			{
				base.Definition();
			}
			this.sum = base.ValueOutput<T>("sum", new Func<Flow, T>(this.Operation)).Predictable();
			foreach (ValueInput valueInput in base.multiInputs)
			{
				base.Requirement(valueInput, this.sum);
			}
		}

		// Token: 0x060006FC RID: 1788
		public abstract T Operation(T a, T b);

		// Token: 0x060006FD RID: 1789
		public abstract T Operation(IEnumerable<T> values);

		// Token: 0x060006FE RID: 1790 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		public T Operation(Flow flow)
		{
			if (this.inputCount == 2)
			{
				return this.Operation(flow.GetValue<T>(base.multiInputs[0]), flow.GetValue<T>(base.multiInputs[1]));
			}
			return this.Operation(base.multiInputs.Select(new Func<ValueInput, T>(flow.GetValue<T>)));
		}
	}
}

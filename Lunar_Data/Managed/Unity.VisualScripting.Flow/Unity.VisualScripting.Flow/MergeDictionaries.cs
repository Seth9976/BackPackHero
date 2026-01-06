using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000025 RID: 37
	[UnitCategory("Collections/Dictionaries")]
	[UnitOrder(5)]
	public sealed class MergeDictionaries : MultiInputUnit<IDictionary>
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000551C File Offset: 0x0000371C
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00005524 File Offset: 0x00003724
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput dictionary { get; private set; }

		// Token: 0x0600016E RID: 366 RVA: 0x00005530 File Offset: 0x00003730
		protected override void Definition()
		{
			this.dictionary = base.ValueOutput<IDictionary>("dictionary", new Func<Flow, IDictionary>(this.Merge));
			base.Definition();
			foreach (ValueInput valueInput in base.multiInputs)
			{
				base.Requirement(valueInput, this.dictionary);
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000055A8 File Offset: 0x000037A8
		public IDictionary Merge(Flow flow)
		{
			AotDictionary aotDictionary = new AotDictionary();
			for (int i = 0; i < this.inputCount; i++)
			{
				IDictionaryEnumerator enumerator = flow.GetValue<IDictionary>(base.multiInputs[i]).GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (!aotDictionary.Contains(enumerator.Key))
					{
						aotDictionary.Add(enumerator.Key, enumerator.Value);
					}
				}
			}
			return aotDictionary;
		}
	}
}

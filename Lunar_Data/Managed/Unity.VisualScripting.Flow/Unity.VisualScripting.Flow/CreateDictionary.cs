using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000022 RID: 34
	[UnitCategory("Collections/Dictionaries")]
	[UnitOrder(-1)]
	[TypeIcon(typeof(IDictionary))]
	[RenamedFrom("Bolt.CreateDitionary")]
	public sealed class CreateDictionary : Unit
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000532B File Offset: 0x0000352B
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00005333 File Offset: 0x00003533
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput dictionary { get; private set; }

		// Token: 0x06000157 RID: 343 RVA: 0x0000533C File Offset: 0x0000353C
		protected override void Definition()
		{
			this.dictionary = base.ValueOutput<IDictionary>("dictionary", new Func<Flow, IDictionary>(this.Create));
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000535B File Offset: 0x0000355B
		public IDictionary Create(Flow flow)
		{
			return new AotDictionary();
		}
	}
}

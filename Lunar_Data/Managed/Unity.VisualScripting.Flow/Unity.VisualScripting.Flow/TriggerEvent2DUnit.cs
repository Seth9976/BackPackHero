using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000A4 RID: 164
	[UnitCategory("Events/Physics 2D")]
	public abstract class TriggerEvent2DUnit : GameObjectEventUnit<Collider2D>
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0000A005 File Offset: 0x00008205
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x0000A00D File Offset: 0x0000820D
		[DoNotSerialize]
		public ValueOutput collider { get; private set; }

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000A016 File Offset: 0x00008216
		protected override void Definition()
		{
			base.Definition();
			this.collider = base.ValueOutput<Collider2D>("collider");
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000A02F File Offset: 0x0000822F
		protected override void AssignArguments(Flow flow, Collider2D other)
		{
			flow.SetValue(this.collider, other);
		}
	}
}

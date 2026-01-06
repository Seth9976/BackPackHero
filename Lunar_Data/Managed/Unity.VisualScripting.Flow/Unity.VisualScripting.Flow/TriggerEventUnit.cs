using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200009B RID: 155
	[UnitCategory("Events/Physics")]
	public abstract class TriggerEventUnit : GameObjectEventUnit<Collider>
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00009C68 File Offset: 0x00007E68
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x00009C70 File Offset: 0x00007E70
		[DoNotSerialize]
		public ValueOutput collider { get; private set; }

		// Token: 0x06000491 RID: 1169 RVA: 0x00009C79 File Offset: 0x00007E79
		protected override void Definition()
		{
			base.Definition();
			this.collider = base.ValueOutput<Collider>("collider");
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00009C92 File Offset: 0x00007E92
		protected override void AssignArguments(Flow flow, Collider other)
		{
			flow.SetValue(this.collider, other);
		}
	}
}

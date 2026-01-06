using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200009C RID: 156
	[UnitCategory("Events/Physics 2D")]
	public abstract class CollisionEvent2DUnit : GameObjectEventUnit<Collision2D>
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00009CA9 File Offset: 0x00007EA9
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x00009CB1 File Offset: 0x00007EB1
		[DoNotSerialize]
		public ValueOutput collider { get; private set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00009CBA File Offset: 0x00007EBA
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x00009CC2 File Offset: 0x00007EC2
		[DoNotSerialize]
		public ValueOutput contacts { get; private set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x00009CCB File Offset: 0x00007ECB
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x00009CD3 File Offset: 0x00007ED3
		[DoNotSerialize]
		public ValueOutput relativeVelocity { get; private set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x00009CDC File Offset: 0x00007EDC
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x00009CE4 File Offset: 0x00007EE4
		[DoNotSerialize]
		public ValueOutput enabled { get; private set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x00009CED File Offset: 0x00007EED
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x00009CF5 File Offset: 0x00007EF5
		[DoNotSerialize]
		public ValueOutput data { get; private set; }

		// Token: 0x0600049E RID: 1182 RVA: 0x00009D00 File Offset: 0x00007F00
		protected override void Definition()
		{
			base.Definition();
			this.collider = base.ValueOutput<Collider2D>("collider");
			this.contacts = base.ValueOutput<ContactPoint2D[]>("contacts");
			this.relativeVelocity = base.ValueOutput<Vector2>("relativeVelocity");
			this.enabled = base.ValueOutput<bool>("enabled");
			this.data = base.ValueOutput<Collision2D>("data");
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00009D68 File Offset: 0x00007F68
		protected override void AssignArguments(Flow flow, Collision2D collisionData)
		{
			flow.SetValue(this.collider, collisionData.collider);
			flow.SetValue(this.contacts, collisionData.contacts);
			flow.SetValue(this.relativeVelocity, collisionData.relativeVelocity);
			flow.SetValue(this.enabled, collisionData.enabled);
			flow.SetValue(this.data, collisionData);
		}
	}
}

using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000091 RID: 145
	[UnitCategory("Events/Physics")]
	public abstract class CollisionEventUnit : GameObjectEventUnit<Collision>
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000097CE File Offset: 0x000079CE
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x000097D6 File Offset: 0x000079D6
		[DoNotSerialize]
		public ValueOutput collider { get; private set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000097DF File Offset: 0x000079DF
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x000097E7 File Offset: 0x000079E7
		[DoNotSerialize]
		public ValueOutput contacts { get; private set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x000097F0 File Offset: 0x000079F0
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x000097F8 File Offset: 0x000079F8
		[DoNotSerialize]
		public ValueOutput impulse { get; private set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00009801 File Offset: 0x00007A01
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00009809 File Offset: 0x00007A09
		[DoNotSerialize]
		public ValueOutput relativeVelocity { get; private set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00009812 File Offset: 0x00007A12
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000981A File Offset: 0x00007A1A
		[DoNotSerialize]
		public ValueOutput data { get; private set; }

		// Token: 0x06000457 RID: 1111 RVA: 0x00009824 File Offset: 0x00007A24
		protected override void Definition()
		{
			base.Definition();
			this.collider = base.ValueOutput<Collider>("collider");
			this.contacts = base.ValueOutput<ContactPoint[]>("contacts");
			this.impulse = base.ValueOutput<Vector3>("impulse");
			this.relativeVelocity = base.ValueOutput<Vector3>("relativeVelocity");
			this.data = base.ValueOutput<Collision>("data");
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000988C File Offset: 0x00007A8C
		protected override void AssignArguments(Flow flow, Collision collision)
		{
			flow.SetValue(this.collider, collision.collider);
			flow.SetValue(this.contacts, collision.contacts);
			flow.SetValue(this.impulse, collision.impulse);
			flow.SetValue(this.relativeVelocity, collision.relativeVelocity);
			flow.SetValue(this.data, collision);
		}
	}
}

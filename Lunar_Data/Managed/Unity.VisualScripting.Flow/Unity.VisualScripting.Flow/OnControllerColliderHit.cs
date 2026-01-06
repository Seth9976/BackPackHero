using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000095 RID: 149
	[UnitCategory("Events/Physics")]
	[TypeIcon(typeof(CharacterController))]
	public sealed class OnControllerColliderHit : GameObjectEventUnit<ControllerColliderHit>
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00009951 File Offset: 0x00007B51
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnControllerColliderHitMessageListener);
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000995D File Offset: 0x00007B5D
		protected override string hookName
		{
			get
			{
				return "OnControllerColliderHit";
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00009964 File Offset: 0x00007B64
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000996C File Offset: 0x00007B6C
		[DoNotSerialize]
		public ValueOutput collider { get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00009975 File Offset: 0x00007B75
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000997D File Offset: 0x00007B7D
		[DoNotSerialize]
		public ValueOutput controller { get; private set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00009986 File Offset: 0x00007B86
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000998E File Offset: 0x00007B8E
		[DoNotSerialize]
		public ValueOutput moveDirection { get; private set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00009997 File Offset: 0x00007B97
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000999F File Offset: 0x00007B9F
		[DoNotSerialize]
		public ValueOutput moveLength { get; private set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000099A8 File Offset: 0x00007BA8
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x000099B0 File Offset: 0x00007BB0
		[DoNotSerialize]
		public ValueOutput normal { get; private set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000099B9 File Offset: 0x00007BB9
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x000099C1 File Offset: 0x00007BC1
		[DoNotSerialize]
		public ValueOutput point { get; private set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x000099CA File Offset: 0x00007BCA
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x000099D2 File Offset: 0x00007BD2
		[DoNotSerialize]
		public ValueOutput data { get; private set; }

		// Token: 0x06000473 RID: 1139 RVA: 0x000099DC File Offset: 0x00007BDC
		protected override void Definition()
		{
			base.Definition();
			this.collider = base.ValueOutput<Collider>("collider");
			this.controller = base.ValueOutput<CharacterController>("controller");
			this.moveDirection = base.ValueOutput<Vector3>("moveDirection");
			this.moveLength = base.ValueOutput<float>("moveLength");
			this.normal = base.ValueOutput<Vector3>("normal");
			this.point = base.ValueOutput<Vector3>("point");
			this.data = base.ValueOutput<ControllerColliderHit>("data");
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00009A68 File Offset: 0x00007C68
		protected override void AssignArguments(Flow flow, ControllerColliderHit hitData)
		{
			flow.SetValue(this.collider, hitData.collider);
			flow.SetValue(this.controller, hitData.controller);
			flow.SetValue(this.moveDirection, hitData.moveDirection);
			flow.SetValue(this.moveLength, hitData.moveLength);
			flow.SetValue(this.normal, hitData.normal);
			flow.SetValue(this.point, hitData.point);
			flow.SetValue(this.data, hitData);
		}
	}
}

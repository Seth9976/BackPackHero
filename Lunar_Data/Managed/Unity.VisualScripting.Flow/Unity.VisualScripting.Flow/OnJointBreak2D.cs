using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000A0 RID: 160
	[UnitCategory("Events/Physics 2D")]
	public sealed class OnJointBreak2D : GameObjectEventUnit<Joint2D>
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x00009E2D File Offset: 0x0000802D
		public override Type MessageListenerType
		{
			get
			{
				return typeof(UnityOnJointBreak2DMessageListener);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x00009E39 File Offset: 0x00008039
		protected override string hookName
		{
			get
			{
				return "OnJointBreak2D";
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00009E40 File Offset: 0x00008040
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x00009E48 File Offset: 0x00008048
		[DoNotSerialize]
		public ValueOutput breakForce { get; private set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00009E51 File Offset: 0x00008051
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00009E59 File Offset: 0x00008059
		[DoNotSerialize]
		public ValueOutput breakTorque { get; private set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00009E62 File Offset: 0x00008062
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00009E6A File Offset: 0x0000806A
		[DoNotSerialize]
		public ValueOutput connectedBody { get; private set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00009E73 File Offset: 0x00008073
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00009E7B File Offset: 0x0000807B
		[DoNotSerialize]
		public ValueOutput reactionForce { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00009E84 File Offset: 0x00008084
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x00009E8C File Offset: 0x0000808C
		[DoNotSerialize]
		public ValueOutput reactionTorque { get; private set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00009E95 File Offset: 0x00008095
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00009E9D File Offset: 0x0000809D
		[DoNotSerialize]
		public ValueOutput joint { get; private set; }

		// Token: 0x060004B8 RID: 1208 RVA: 0x00009EA8 File Offset: 0x000080A8
		protected override void Definition()
		{
			base.Definition();
			this.breakForce = base.ValueOutput<float>("breakForce");
			this.breakTorque = base.ValueOutput<float>("breakTorque");
			this.connectedBody = base.ValueOutput<Rigidbody2D>("connectedBody");
			this.reactionForce = base.ValueOutput<Vector2>("reactionForce");
			this.reactionTorque = base.ValueOutput<float>("reactionTorque");
			this.joint = base.ValueOutput<Joint2D>("joint");
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00009F24 File Offset: 0x00008124
		protected override void AssignArguments(Flow flow, Joint2D joint)
		{
			flow.SetValue(this.breakForce, joint.breakForce);
			flow.SetValue(this.breakTorque, joint.breakTorque);
			flow.SetValue(this.connectedBody, joint.connectedBody);
			flow.SetValue(this.reactionForce, joint.reactionForce);
			flow.SetValue(this.reactionTorque, joint.reactionTorque);
			flow.SetValue(this.joint, joint);
		}
	}
}

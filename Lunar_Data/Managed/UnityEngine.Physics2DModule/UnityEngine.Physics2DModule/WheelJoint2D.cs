using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	[NativeHeader("Modules/Physics2D/WheelJoint2D.h")]
	public sealed class WheelJoint2D : AnchoredJoint2D
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00008654 File Offset: 0x00006854
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0000866A File Offset: 0x0000686A
		public JointSuspension2D suspension
		{
			get
			{
				JointSuspension2D jointSuspension2D;
				this.get_suspension_Injected(out jointSuspension2D);
				return jointSuspension2D;
			}
			set
			{
				this.set_suspension_Injected(ref value);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000440 RID: 1088
		// (set) Token: 0x06000441 RID: 1089
		public extern bool useMotor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00008674 File Offset: 0x00006874
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000868A File Offset: 0x0000688A
		public JointMotor2D motor
		{
			get
			{
				JointMotor2D jointMotor2D;
				this.get_motor_Injected(out jointMotor2D);
				return jointMotor2D;
			}
			set
			{
				this.set_motor_Injected(ref value);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000444 RID: 1092
		public extern float jointTranslation
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000445 RID: 1093
		public extern float jointLinearSpeed
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000446 RID: 1094
		public extern float jointSpeed
		{
			[NativeMethod("GetJointAngularSpeed")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000447 RID: 1095
		public extern float jointAngle
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000448 RID: 1096
		[MethodImpl(4096)]
		public extern float GetMotorTorque(float timeStep);

		// Token: 0x0600044A RID: 1098
		[MethodImpl(4096)]
		private extern void get_suspension_Injected(out JointSuspension2D ret);

		// Token: 0x0600044B RID: 1099
		[MethodImpl(4096)]
		private extern void set_suspension_Injected(ref JointSuspension2D value);

		// Token: 0x0600044C RID: 1100
		[MethodImpl(4096)]
		private extern void get_motor_Injected(out JointMotor2D ret);

		// Token: 0x0600044D RID: 1101
		[MethodImpl(4096)]
		private extern void set_motor_Injected(ref JointMotor2D value);
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200006A RID: 106
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Modules/Animation/MuscleHandle.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public struct MuscleHandle
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00008199 File Offset: 0x00006399
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x000081A1 File Offset: 0x000063A1
		public HumanPartDof humanPartDof { readonly get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x000081AA File Offset: 0x000063AA
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x000081B2 File Offset: 0x000063B2
		public int dof { readonly get; private set; }

		// Token: 0x06000616 RID: 1558 RVA: 0x000081BB File Offset: 0x000063BB
		public MuscleHandle(BodyDof bodyDof)
		{
			this.humanPartDof = HumanPartDof.Body;
			this.dof = (int)bodyDof;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000081CE File Offset: 0x000063CE
		public MuscleHandle(HeadDof headDof)
		{
			this.humanPartDof = HumanPartDof.Head;
			this.dof = (int)headDof;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000081E4 File Offset: 0x000063E4
		public MuscleHandle(HumanPartDof partDof, LegDof legDof)
		{
			bool flag = partDof != HumanPartDof.LeftLeg && partDof != HumanPartDof.RightLeg;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a leg, please use either HumanPartDof.LeftLeg or HumanPartDof.RightLeg.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)legDof;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00008220 File Offset: 0x00006420
		public MuscleHandle(HumanPartDof partDof, ArmDof armDof)
		{
			bool flag = partDof != HumanPartDof.LeftArm && partDof != HumanPartDof.RightArm;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for an arm, please use either HumanPartDof.LeftArm or HumanPartDof.RightArm.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)armDof;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0000825C File Offset: 0x0000645C
		public MuscleHandle(HumanPartDof partDof, FingerDof fingerDof)
		{
			bool flag = partDof < HumanPartDof.LeftThumb || partDof > HumanPartDof.RightLittle;
			if (flag)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a finger.");
			}
			this.humanPartDof = partDof;
			this.dof = (int)fingerDof;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00008298 File Offset: 0x00006498
		public string name
		{
			get
			{
				return this.GetName();
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x000082B0 File Offset: 0x000064B0
		public static int muscleHandleCount
		{
			get
			{
				return MuscleHandle.GetMuscleHandleCount();
			}
		}

		// Token: 0x0600061D RID: 1565
		[MethodImpl(4096)]
		public static extern void GetMuscleHandles([NotNull("ArgumentNullException")] [Out] MuscleHandle[] muscleHandles);

		// Token: 0x0600061E RID: 1566 RVA: 0x000082C7 File Offset: 0x000064C7
		private string GetName()
		{
			return MuscleHandle.GetName_Injected(ref this);
		}

		// Token: 0x0600061F RID: 1567
		[MethodImpl(4096)]
		private static extern int GetMuscleHandleCount();

		// Token: 0x06000620 RID: 1568
		[MethodImpl(4096)]
		private static extern string GetName_Injected(ref MuscleHandle _unity_self);
	}
}

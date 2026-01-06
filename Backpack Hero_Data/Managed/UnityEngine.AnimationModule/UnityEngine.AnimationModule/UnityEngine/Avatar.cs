using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002F RID: 47
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/Avatar.h")]
	public class Avatar : Object
	{
		// Token: 0x06000207 RID: 519 RVA: 0x000039FB File Offset: 0x00001BFB
		private Avatar()
		{
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000208 RID: 520
		public extern bool isValid
		{
			[NativeMethod("IsValid")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000209 RID: 521
		public extern bool isHuman
		{
			[NativeMethod("IsHuman")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00003A08 File Offset: 0x00001C08
		public HumanDescription humanDescription
		{
			get
			{
				HumanDescription humanDescription;
				this.get_humanDescription_Injected(out humanDescription);
				return humanDescription;
			}
		}

		// Token: 0x0600020B RID: 523
		[MethodImpl(4096)]
		internal extern void SetMuscleMinMax(int muscleId, float min, float max);

		// Token: 0x0600020C RID: 524
		[MethodImpl(4096)]
		internal extern void SetParameter(int parameterId, float value);

		// Token: 0x0600020D RID: 525 RVA: 0x00003A20 File Offset: 0x00001C20
		internal float GetAxisLength(int humanId)
		{
			return this.Internal_GetAxisLength(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00003A40 File Offset: 0x00001C40
		internal Quaternion GetPreRotation(int humanId)
		{
			return this.Internal_GetPreRotation(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00003A60 File Offset: 0x00001C60
		internal Quaternion GetPostRotation(int humanId)
		{
			return this.Internal_GetPostRotation(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00003A80 File Offset: 0x00001C80
		internal Quaternion GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			return this.Internal_GetZYPostQ(HumanTrait.GetBoneIndexFromMono(humanId), parentQ, q);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00003AA0 File Offset: 0x00001CA0
		internal Quaternion GetZYRoll(int humanId, Vector3 uvw)
		{
			return this.Internal_GetZYRoll(HumanTrait.GetBoneIndexFromMono(humanId), uvw);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00003AC0 File Offset: 0x00001CC0
		internal Vector3 GetLimitSign(int humanId)
		{
			return this.Internal_GetLimitSign(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x06000213 RID: 531
		[NativeMethod("GetAxisLength")]
		[MethodImpl(4096)]
		internal extern float Internal_GetAxisLength(int humanId);

		// Token: 0x06000214 RID: 532 RVA: 0x00003AE0 File Offset: 0x00001CE0
		[NativeMethod("GetPreRotation")]
		internal Quaternion Internal_GetPreRotation(int humanId)
		{
			Quaternion quaternion;
			this.Internal_GetPreRotation_Injected(humanId, out quaternion);
			return quaternion;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00003AF8 File Offset: 0x00001CF8
		[NativeMethod("GetPostRotation")]
		internal Quaternion Internal_GetPostRotation(int humanId)
		{
			Quaternion quaternion;
			this.Internal_GetPostRotation_Injected(humanId, out quaternion);
			return quaternion;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00003B10 File Offset: 0x00001D10
		[NativeMethod("GetZYPostQ")]
		internal Quaternion Internal_GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			Quaternion quaternion;
			this.Internal_GetZYPostQ_Injected(humanId, ref parentQ, ref q, out quaternion);
			return quaternion;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00003B2C File Offset: 0x00001D2C
		[NativeMethod("GetZYRoll")]
		internal Quaternion Internal_GetZYRoll(int humanId, Vector3 uvw)
		{
			Quaternion quaternion;
			this.Internal_GetZYRoll_Injected(humanId, ref uvw, out quaternion);
			return quaternion;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00003B48 File Offset: 0x00001D48
		[NativeMethod("GetLimitSign")]
		internal Vector3 Internal_GetLimitSign(int humanId)
		{
			Vector3 vector;
			this.Internal_GetLimitSign_Injected(humanId, out vector);
			return vector;
		}

		// Token: 0x06000219 RID: 537
		[MethodImpl(4096)]
		private extern void get_humanDescription_Injected(out HumanDescription ret);

		// Token: 0x0600021A RID: 538
		[MethodImpl(4096)]
		private extern void Internal_GetPreRotation_Injected(int humanId, out Quaternion ret);

		// Token: 0x0600021B RID: 539
		[MethodImpl(4096)]
		private extern void Internal_GetPostRotation_Injected(int humanId, out Quaternion ret);

		// Token: 0x0600021C RID: 540
		[MethodImpl(4096)]
		private extern void Internal_GetZYPostQ_Injected(int humanId, ref Quaternion parentQ, ref Quaternion q, out Quaternion ret);

		// Token: 0x0600021D RID: 541
		[MethodImpl(4096)]
		private extern void Internal_GetZYRoll_Injected(int humanId, ref Vector3 uvw, out Quaternion ret);

		// Token: 0x0600021E RID: 542
		[MethodImpl(4096)]
		private extern void Internal_GetLimitSign_Injected(int humanId, out Vector3 ret);
	}
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine
{
	// Token: 0x02000036 RID: 54
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[NativeHeader("Modules/Animation/AvatarMask.h")]
	[MovedFrom(true, "UnityEditor.Animations", "UnityEditor", null)]
	public sealed class AvatarMask : Object
	{
		// Token: 0x06000244 RID: 580 RVA: 0x00003DCC File Offset: 0x00001FCC
		public AvatarMask()
		{
			AvatarMask.Internal_Create(this);
		}

		// Token: 0x06000245 RID: 581
		[FreeFunction("AnimationBindings::CreateAvatarMask")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] AvatarMask self);

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00003DE0 File Offset: 0x00001FE0
		[Obsolete("AvatarMask.humanoidBodyPartCount is deprecated, use AvatarMaskBodyPart.LastBodyPart instead.")]
		public int humanoidBodyPartCount
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x06000247 RID: 583
		[NativeMethod("GetBodyPart")]
		[MethodImpl(4096)]
		public extern bool GetHumanoidBodyPartActive(AvatarMaskBodyPart index);

		// Token: 0x06000248 RID: 584
		[NativeMethod("SetBodyPart")]
		[MethodImpl(4096)]
		public extern void SetHumanoidBodyPartActive(AvatarMaskBodyPart index, bool value);

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000249 RID: 585
		// (set) Token: 0x0600024A RID: 586
		public extern int transformCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public void AddTransformPath(Transform transform)
		{
			this.AddTransformPath(transform, true);
		}

		// Token: 0x0600024C RID: 588
		[MethodImpl(4096)]
		public extern void AddTransformPath([NotNull("ArgumentNullException")] Transform transform, [DefaultValue("true")] bool recursive);

		// Token: 0x0600024D RID: 589 RVA: 0x00003E00 File Offset: 0x00002000
		public void RemoveTransformPath(Transform transform)
		{
			this.RemoveTransformPath(transform, true);
		}

		// Token: 0x0600024E RID: 590
		[MethodImpl(4096)]
		public extern void RemoveTransformPath([NotNull("ArgumentNullException")] Transform transform, [DefaultValue("true")] bool recursive);

		// Token: 0x0600024F RID: 591
		[MethodImpl(4096)]
		public extern string GetTransformPath(int index);

		// Token: 0x06000250 RID: 592
		[MethodImpl(4096)]
		public extern void SetTransformPath(int index, string path);

		// Token: 0x06000251 RID: 593
		[MethodImpl(4096)]
		private extern float GetTransformWeight(int index);

		// Token: 0x06000252 RID: 594
		[MethodImpl(4096)]
		private extern void SetTransformWeight(int index, float weight);

		// Token: 0x06000253 RID: 595 RVA: 0x00003E0C File Offset: 0x0000200C
		public bool GetTransformActive(int index)
		{
			return this.GetTransformWeight(index) > 0.5f;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00003E2C File Offset: 0x0000202C
		public void SetTransformActive(int index, bool value)
		{
			this.SetTransformWeight(index, value ? 1f : 0f);
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000255 RID: 597
		internal extern bool hasFeetIK
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00003E48 File Offset: 0x00002048
		internal void Copy(AvatarMask other)
		{
			for (AvatarMaskBodyPart avatarMaskBodyPart = AvatarMaskBodyPart.Root; avatarMaskBodyPart < AvatarMaskBodyPart.LastBodyPart; avatarMaskBodyPart++)
			{
				this.SetHumanoidBodyPartActive(avatarMaskBodyPart, other.GetHumanoidBodyPartActive(avatarMaskBodyPart));
			}
			this.transformCount = other.transformCount;
			for (int i = 0; i < other.transformCount; i++)
			{
				this.SetTransformPath(i, other.GetTransformPath(i));
				this.SetTransformActive(i, other.GetTransformActive(i));
			}
		}
	}
}

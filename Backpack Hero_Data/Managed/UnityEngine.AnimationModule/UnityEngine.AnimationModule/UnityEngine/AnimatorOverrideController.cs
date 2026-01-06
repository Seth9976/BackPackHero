using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/AnimatorOverrideController.h")]
	public class AnimatorOverrideController : RuntimeAnimatorController
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x000037D2 File Offset: 0x000019D2
		public AnimatorOverrideController()
		{
			AnimatorOverrideController.Internal_Create(this, null);
			this.OnOverrideControllerDirty = null;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000037EB File Offset: 0x000019EB
		public AnimatorOverrideController(RuntimeAnimatorController controller)
		{
			AnimatorOverrideController.Internal_Create(this, controller);
			this.OnOverrideControllerDirty = null;
		}

		// Token: 0x060001EB RID: 491
		[FreeFunction("AnimationBindings::CreateAnimatorOverrideController")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] AnimatorOverrideController self, RuntimeAnimatorController controller);

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001EC RID: 492
		// (set) Token: 0x060001ED RID: 493
		public extern RuntimeAnimatorController runtimeAnimatorController
		{
			[NativeMethod("GetAnimatorController")]
			[MethodImpl(4096)]
			get;
			[NativeMethod("SetAnimatorController")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000085 RID: 133
		public AnimationClip this[string name]
		{
			get
			{
				return this.Internal_GetClipByName(name, true);
			}
			set
			{
				this.Internal_SetClipByName(name, value);
			}
		}

		// Token: 0x060001F0 RID: 496
		[NativeMethod("GetClip")]
		[MethodImpl(4096)]
		private extern AnimationClip Internal_GetClipByName(string name, bool returnEffectiveClip);

		// Token: 0x060001F1 RID: 497
		[NativeMethod("SetClip")]
		[MethodImpl(4096)]
		private extern void Internal_SetClipByName(string name, AnimationClip clip);

		// Token: 0x17000086 RID: 134
		public AnimationClip this[AnimationClip clip]
		{
			get
			{
				return this.GetClip(clip, true);
			}
			set
			{
				this.SetClip(clip, value, true);
			}
		}

		// Token: 0x060001F4 RID: 500
		[MethodImpl(4096)]
		private extern AnimationClip GetClip(AnimationClip originalClip, bool returnEffectiveClip);

		// Token: 0x060001F5 RID: 501
		[MethodImpl(4096)]
		private extern void SetClip(AnimationClip originalClip, AnimationClip overrideClip, bool notify);

		// Token: 0x060001F6 RID: 502
		[MethodImpl(4096)]
		private extern void SendNotification();

		// Token: 0x060001F7 RID: 503
		[MethodImpl(4096)]
		private extern AnimationClip GetOriginalClip(int index);

		// Token: 0x060001F8 RID: 504
		[MethodImpl(4096)]
		private extern AnimationClip GetOverrideClip(AnimationClip originalClip);

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F9 RID: 505
		public extern int overridesCount
		{
			[NativeMethod("GetOriginalClipsCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00003854 File Offset: 0x00001A54
		public void GetOverrides(List<KeyValuePair<AnimationClip, AnimationClip>> overrides)
		{
			bool flag = overrides == null;
			if (flag)
			{
				throw new ArgumentNullException("overrides");
			}
			int overridesCount = this.overridesCount;
			bool flag2 = overrides.Capacity < overridesCount;
			if (flag2)
			{
				overrides.Capacity = overridesCount;
			}
			overrides.Clear();
			for (int i = 0; i < overridesCount; i++)
			{
				AnimationClip originalClip = this.GetOriginalClip(i);
				overrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(originalClip, this.GetOverrideClip(originalClip)));
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000038CC File Offset: 0x00001ACC
		public void ApplyOverrides(IList<KeyValuePair<AnimationClip, AnimationClip>> overrides)
		{
			bool flag = overrides == null;
			if (flag)
			{
				throw new ArgumentNullException("overrides");
			}
			for (int i = 0; i < overrides.Count; i++)
			{
				this.SetClip(overrides[i].Key, overrides[i].Value, false);
			}
			this.SendNotification();
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00003930 File Offset: 0x00001B30
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00003994 File Offset: 0x00001B94
		[Obsolete("AnimatorOverrideController.clips property is deprecated. Use AnimatorOverrideController.GetOverrides and AnimatorOverrideController.ApplyOverrides instead.")]
		public AnimationClipPair[] clips
		{
			get
			{
				int overridesCount = this.overridesCount;
				AnimationClipPair[] array = new AnimationClipPair[overridesCount];
				for (int i = 0; i < overridesCount; i++)
				{
					array[i] = new AnimationClipPair();
					array[i].originalClip = this.GetOriginalClip(i);
					array[i].overrideClip = this.GetOverrideClip(array[i].originalClip);
				}
				return array;
			}
			set
			{
				for (int i = 0; i < value.Length; i++)
				{
					this.SetClip(value[i].originalClip, value[i].overrideClip, false);
				}
				this.SendNotification();
			}
		}

		// Token: 0x060001FE RID: 510
		[NativeConditional("UNITY_EDITOR")]
		[MethodImpl(4096)]
		internal extern void PerformOverrideClipListCleanup();

		// Token: 0x060001FF RID: 511 RVA: 0x000039D4 File Offset: 0x00001BD4
		[RequiredByNativeCode]
		[NativeConditional("UNITY_EDITOR")]
		internal static void OnInvalidateOverrideController(AnimatorOverrideController controller)
		{
			bool flag = controller.OnOverrideControllerDirty != null;
			if (flag)
			{
				controller.OnOverrideControllerDirty();
			}
		}

		// Token: 0x0400006E RID: 110
		internal AnimatorOverrideController.OnOverrideControllerDirtyCallback OnOverrideControllerDirty;

		// Token: 0x02000024 RID: 36
		// (Invoke) Token: 0x06000201 RID: 513
		internal delegate void OnOverrideControllerDirtyCallback();
	}
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	public class AvatarBuilder
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00003D98 File Offset: 0x00001F98
		public static Avatar BuildHumanAvatar(GameObject go, HumanDescription humanDescription)
		{
			bool flag = go == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return AvatarBuilder.BuildHumanAvatarInternal(go, humanDescription);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00003DC2 File Offset: 0x00001FC2
		[FreeFunction("AvatarBuilderBindings::BuildHumanAvatar")]
		private static Avatar BuildHumanAvatarInternal(GameObject go, HumanDescription humanDescription)
		{
			return AvatarBuilder.BuildHumanAvatarInternal_Injected(go, ref humanDescription);
		}

		// Token: 0x06000241 RID: 577
		[FreeFunction("AvatarBuilderBindings::BuildGenericAvatar")]
		[MethodImpl(4096)]
		public static extern Avatar BuildGenericAvatar([NotNull("ArgumentNullException")] GameObject go, [NotNull("ArgumentNullException")] string rootMotionTransformName);

		// Token: 0x06000243 RID: 579
		[MethodImpl(4096)]
		private static extern Avatar BuildHumanAvatarInternal_Injected(GameObject go, ref HumanDescription humanDescription);
	}
}

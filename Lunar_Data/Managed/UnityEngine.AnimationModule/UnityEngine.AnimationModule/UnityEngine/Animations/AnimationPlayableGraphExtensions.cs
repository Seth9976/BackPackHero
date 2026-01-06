using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x02000051 RID: 81
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableGraphExtensions.bindings.h")]
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("AnimationPlayableGraphExtensionsBindings", StaticAccessorType.DoubleColon)]
	internal static class AnimationPlayableGraphExtensions
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x000058E9 File Offset: 0x00003AE9
		internal static void SyncUpdateAndTimeMode(this PlayableGraph graph, Animator animator)
		{
			AnimationPlayableGraphExtensions.InternalSyncUpdateAndTimeMode(ref graph, animator);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000058F5 File Offset: 0x00003AF5
		internal static void DestroyOutput(this PlayableGraph graph, PlayableOutputHandle handle)
		{
			AnimationPlayableGraphExtensions.InternalDestroyOutput(ref graph, ref handle);
		}

		// Token: 0x060003C4 RID: 964
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern bool InternalCreateAnimationOutput(ref PlayableGraph graph, string name, out PlayableOutputHandle handle);

		// Token: 0x060003C5 RID: 965
		[NativeThrows]
		[MethodImpl(4096)]
		internal static extern void InternalSyncUpdateAndTimeMode(ref PlayableGraph graph, [NotNull("ArgumentNullException")] Animator animator);

		// Token: 0x060003C6 RID: 966
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern void InternalDestroyOutput(ref PlayableGraph graph, ref PlayableOutputHandle handle);

		// Token: 0x060003C7 RID: 967
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern int InternalAnimationOutputCount(ref PlayableGraph graph);

		// Token: 0x060003C8 RID: 968
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern bool InternalGetAnimationOutput(ref PlayableGraph graph, int index, out PlayableOutputHandle handle);
	}
}

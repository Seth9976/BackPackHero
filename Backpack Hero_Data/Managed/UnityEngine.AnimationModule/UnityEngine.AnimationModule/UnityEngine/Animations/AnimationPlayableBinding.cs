using System;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x0200003F RID: 63
	public static class AnimationPlayableBinding
	{
		// Token: 0x0600029C RID: 668 RVA: 0x000045A4 File Offset: 0x000027A4
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(Animator), new PlayableBinding.CreateOutputMethod(AnimationPlayableBinding.CreateAnimationOutput));
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000045D4 File Offset: 0x000027D4
		private static PlayableOutput CreateAnimationOutput(PlayableGraph graph, string name)
		{
			return AnimationPlayableOutput.Create(graph, name, null);
		}
	}
}

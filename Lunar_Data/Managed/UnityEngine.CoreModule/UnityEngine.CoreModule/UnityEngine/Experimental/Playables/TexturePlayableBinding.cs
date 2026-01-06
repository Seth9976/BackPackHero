using System;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x02000469 RID: 1129
	public static class TexturePlayableBinding
	{
		// Token: 0x06002803 RID: 10243 RVA: 0x0004291C File Offset: 0x00040B1C
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(RenderTexture), new PlayableBinding.CreateOutputMethod(TexturePlayableBinding.CreateTextureOutput));
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0004294C File Offset: 0x00040B4C
		private static PlayableOutput CreateTextureOutput(PlayableGraph graph, string name)
		{
			return TexturePlayableOutput.Create(graph, name, null);
		}
	}
}

using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000449 RID: 1097
	public static class ScriptPlayableBinding
	{
		// Token: 0x060026D2 RID: 9938 RVA: 0x00040BC0 File Offset: 0x0003EDC0
		public static PlayableBinding Create(string name, Object key, Type type)
		{
			return PlayableBinding.CreateInternal(name, key, type, new PlayableBinding.CreateOutputMethod(ScriptPlayableBinding.CreateScriptOutput));
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x00040BE8 File Offset: 0x0003EDE8
		private static PlayableOutput CreateScriptOutput(PlayableGraph graph, string name)
		{
			return ScriptPlayableOutput.Create(graph, name);
		}
	}
}

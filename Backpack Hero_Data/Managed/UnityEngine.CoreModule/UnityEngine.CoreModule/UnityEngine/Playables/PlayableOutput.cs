using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000445 RID: 1093
	[RequiredByNativeCode]
	public struct PlayableOutput : IPlayableOutput, IEquatable<PlayableOutput>
	{
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x00040348 File Offset: 0x0003E548
		public static PlayableOutput Null
		{
			get
			{
				return PlayableOutput.m_NullPlayableOutput;
			}
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x0004035F File Offset: 0x0003E55F
		[VisibleToOtherModules]
		internal PlayableOutput(PlayableOutputHandle handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x0004036C File Offset: 0x0003E56C
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x00040384 File Offset: 0x0003E584
		public bool IsPlayableOutputOfType<T>() where T : struct, IPlayableOutput
		{
			return this.GetHandle().IsPlayableOutputOfType<T>();
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x000403A4 File Offset: 0x0003E5A4
		public Type GetPlayableOutputType()
		{
			return this.GetHandle().GetPlayableOutputType();
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000403C4 File Offset: 0x0003E5C4
		public bool Equals(PlayableOutput other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000E25 RID: 3621
		private PlayableOutputHandle m_Handle;

		// Token: 0x04000E26 RID: 3622
		private static readonly PlayableOutput m_NullPlayableOutput = new PlayableOutput(PlayableOutputHandle.Null);
	}
}

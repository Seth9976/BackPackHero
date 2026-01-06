using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000438 RID: 1080
	[RequiredByNativeCode]
	public struct Playable : IPlayable, IEquatable<Playable>
	{
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x0003F254 File Offset: 0x0003D454
		public static Playable Null
		{
			get
			{
				return Playable.m_NullPlayable;
			}
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x0003F26C File Offset: 0x0003D46C
		public static Playable Create(PlayableGraph graph, int inputCount = 0)
		{
			Playable playable = new Playable(graph.CreatePlayableHandle());
			playable.SetInputCount(inputCount);
			return playable;
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x0003F295 File Offset: 0x0003D495
		[VisibleToOtherModules]
		internal Playable(PlayableHandle handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x0003F2A0 File Offset: 0x0003D4A0
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x0003F2B8 File Offset: 0x0003D4B8
		public bool IsPlayableOfType<T>() where T : struct, IPlayable
		{
			return this.GetHandle().IsPlayableOfType<T>();
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x0003F2D8 File Offset: 0x0003D4D8
		public Type GetPlayableType()
		{
			return this.GetHandle().GetPlayableType();
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x0003F2F8 File Offset: 0x0003D4F8
		public bool Equals(Playable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000E07 RID: 3591
		private PlayableHandle m_Handle;

		// Token: 0x04000E08 RID: 3592
		private static readonly Playable m_NullPlayable = new Playable(PlayableHandle.Null);
	}
}

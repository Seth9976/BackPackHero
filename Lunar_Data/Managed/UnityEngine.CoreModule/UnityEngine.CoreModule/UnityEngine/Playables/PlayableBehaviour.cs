using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200043B RID: 1083
	[RequiredByNativeCode]
	[Serializable]
	public abstract class PlayableBehaviour : IPlayableBehaviour, ICloneable
	{
		// Token: 0x06002588 RID: 9608 RVA: 0x00008C2F File Offset: 0x00006E2F
		public PlayableBehaviour()
		{
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00004557 File Offset: 0x00002757
		public virtual void OnGraphStart(Playable playable)
		{
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x00004557 File Offset: 0x00002757
		public virtual void OnGraphStop(Playable playable)
		{
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x00004557 File Offset: 0x00002757
		public virtual void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x00004557 File Offset: 0x00002757
		public virtual void OnPlayableDestroy(Playable playable)
		{
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x00004557 File Offset: 0x00002757
		[Obsolete("OnBehaviourDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public virtual void OnBehaviourDelay(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x00004557 File Offset: 0x00002757
		public virtual void OnBehaviourPlay(Playable playable, FrameData info)
		{
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x00004557 File Offset: 0x00002757
		public virtual void OnBehaviourPause(Playable playable, FrameData info)
		{
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x00004557 File Offset: 0x00002757
		public virtual void PrepareData(Playable playable, FrameData info)
		{
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x00004557 File Offset: 0x00002757
		public virtual void PrepareFrame(Playable playable, FrameData info)
		{
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x00004557 File Offset: 0x00002757
		public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0003F3C0 File Offset: 0x0003D5C0
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}

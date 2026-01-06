using System;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000007 RID: 7
	internal class AnimationPreviewUpdateCallback : ITimelineEvaluateCallback
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002774 File Offset: 0x00000974
		public AnimationPreviewUpdateCallback(AnimationPlayableOutput output)
		{
			this.m_Output = output;
			Playable sourcePlayable = this.m_Output.GetSourcePlayable<AnimationPlayableOutput>();
			if (sourcePlayable.IsValid<Playable>())
			{
				this.m_Graph = sourcePlayable.GetGraph<Playable>();
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027B0 File Offset: 0x000009B0
		public void Evaluate()
		{
			if (!this.m_Graph.IsValid())
			{
				return;
			}
			if (this.m_PreviewComponents == null)
			{
				this.FetchPreviewComponents();
			}
			foreach (IAnimationWindowPreview animationWindowPreview in this.m_PreviewComponents)
			{
				if (animationWindowPreview != null)
				{
					animationWindowPreview.UpdatePreviewGraph(this.m_Graph);
				}
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002828 File Offset: 0x00000A28
		private void FetchPreviewComponents()
		{
			this.m_PreviewComponents = new List<IAnimationWindowPreview>();
			Animator target = this.m_Output.GetTarget();
			if (target == null)
			{
				return;
			}
			GameObject gameObject = target.gameObject;
			this.m_PreviewComponents.AddRange(gameObject.GetComponents<IAnimationWindowPreview>());
		}

		// Token: 0x04000015 RID: 21
		private AnimationPlayableOutput m_Output;

		// Token: 0x04000016 RID: 22
		private PlayableGraph m_Graph;

		// Token: 0x04000017 RID: 23
		private List<IAnimationWindowPreview> m_PreviewComponents;
	}
}

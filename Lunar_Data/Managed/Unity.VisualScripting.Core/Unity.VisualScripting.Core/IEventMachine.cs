using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000055 RID: 85
	public interface IEventMachine : IMachine, IGraphRoot, IGraphParent, IGraphNester, IAotStubbable
	{
		// Token: 0x0600027E RID: 638
		void TriggerAnimationEvent(AnimationEvent animationEvent);

		// Token: 0x0600027F RID: 639
		void TriggerUnityEvent(string name);
	}
}

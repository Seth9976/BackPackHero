using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200007C RID: 124
	[AddComponentMenu("Visual Scripting/Listeners/Animator Message Listener")]
	public sealed class AnimatorMessageListener : MonoBehaviour
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x000092B7 File Offset: 0x000074B7
		private void OnAnimatorMove()
		{
			EventBus.Trigger("OnAnimatorMove", base.gameObject);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000092C9 File Offset: 0x000074C9
		private void OnAnimatorIK(int layerIndex)
		{
			EventBus.Trigger<int>("OnAnimatorIK", base.gameObject, layerIndex);
		}
	}
}

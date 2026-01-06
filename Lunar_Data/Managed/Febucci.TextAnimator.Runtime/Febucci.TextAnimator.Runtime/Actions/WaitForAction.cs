using System;
using System.Collections;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using UnityEngine;

namespace Febucci.UI.Actions
{
	// Token: 0x02000034 RID: 52
	[CreateAssetMenu(fileName = "WaitFor Action", menuName = "Text Animator/Actions/Wait For", order = 1)]
	[TagInfo("waitfor")]
	[Serializable]
	public sealed class WaitForAction : ActionScriptableBase
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00004C2D File Offset: 0x00002E2D
		public override IEnumerator DoAction(ActionMarker action, TypewriterCore typewriter, TypingInfo typingInfo)
		{
			float targetTime = this.defaultTime;
			if (action.parameters.Length != 0)
			{
				FormatUtils.TryGetFloat(action.parameters[0], this.defaultTime, out targetTime);
			}
			float t = 0f;
			while (t <= targetTime)
			{
				t += typewriter.TextAnimator.time.deltaTime;
				yield return null;
			}
			yield break;
		}

		// Token: 0x040000A5 RID: 165
		[Tooltip("Time used in case the action does not have the first parameter")]
		public float defaultTime = 1f;
	}
}

using System;
using System.Collections;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using UnityEngine;

namespace Febucci.UI.Actions
{
	// Token: 0x02000032 RID: 50
	[CreateAssetMenu(fileName = "Speed Action", menuName = "Text Animator/Actions/Speed", order = 1)]
	[TagInfo("speed")]
	[Serializable]
	public sealed class SpeedAction : ActionScriptableBase
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00004BED File Offset: 0x00002DED
		public override IEnumerator DoAction(ActionMarker action, TypewriterCore typewriter, TypingInfo typingInfo)
		{
			float num = this.defaultSpeed;
			if (action.parameters.Length != 0)
			{
				FormatUtils.TryGetFloat(action.parameters[0], this.defaultSpeed, out num);
			}
			typingInfo.speed = num;
			yield break;
		}

		// Token: 0x040000A4 RID: 164
		[Tooltip("Speed used in case the action does not have the first parameter")]
		public float defaultSpeed = 2f;
	}
}

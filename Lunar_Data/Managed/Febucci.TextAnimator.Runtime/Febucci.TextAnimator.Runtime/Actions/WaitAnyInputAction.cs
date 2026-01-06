using System;
using System.Collections;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using UnityEngine;

namespace Febucci.UI.Actions
{
	// Token: 0x02000033 RID: 51
	[CreateAssetMenu(fileName = "WaitAnyInput Action", menuName = "Text Animator/Actions/Wait Any Input", order = 1)]
	[TagInfo("waitinput")]
	[Serializable]
	public sealed class WaitAnyInputAction : ActionScriptableBase
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004C1D File Offset: 0x00002E1D
		public override IEnumerator DoAction(ActionMarker action, TypewriterCore typewriter, TypingInfo typingInfo)
		{
			while (!Input.anyKeyDown)
			{
				yield return null;
			}
			yield break;
		}
	}
}

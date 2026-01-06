using System;
using Febucci.UI.Core;
using Febucci.UI.Effects;
using UnityEngine;

namespace Febucci.UI.Examples
{
	// Token: 0x02000002 RID: 2
	[AddComponentMenu("")]
	public class DefaultEffectsExample : MonoBehaviour
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private void Awake()
		{
			this.settings = TextAnimatorSettings.Instance;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		private string AddEffect<T>(TextAnimatorSettings.Category<T> category, string tag) where T : ScriptableObject
		{
			return string.Format("{0}{1}{2}{3}{4}/{5}, ", new object[] { category.openingSymbol, tag, category.closingSymbol, tag, category.openingSymbol, category.closingSymbol });
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		private void Start()
		{
			string text = "<b>You can add effects by using <color=red>rich text tags</color>.</b>" + string.Format("\nExample: writing {0}<noparse><shake>I'm cold</shake></noparse>{1} will result in {2}<shake>I'm cold</shake>{3}.", new object[] { '"', '"', '"', '"' }) + string.Format("\n\n Effects that animate through time are called {0}<color=red>Behaviors</color>{1}, and the default tags are: ", '"', '"');
			foreach (AnimationScriptableBase animationScriptableBase in this.typewriter.TextAnimator.DatabaseBehaviors.Data)
			{
				if (animationScriptableBase)
				{
					text += this.AddEffect<AnimationsDatabase>(this.settings.behaviors, animationScriptableBase.TagID);
				}
			}
			text += string.Format("\n\n<b>Effects that animate letters while they appear on screen are called {0}<color=red>Appearances</color>{1} and the default tags are</b>: ", '"', '"');
			foreach (AnimationScriptableBase animationScriptableBase2 in this.typewriter.TextAnimator.DatabaseAppearances.Data)
			{
				if (animationScriptableBase2)
				{
					text += this.AddEffect<AnimationsDatabase>(this.settings.appearances, animationScriptableBase2.TagID);
				}
			}
			this.typewriter.ShowText(text);
		}

		// Token: 0x04000001 RID: 1
		public TypewriterCore typewriter;

		// Token: 0x04000002 RID: 2
		private TextAnimatorSettings settings;
	}
}

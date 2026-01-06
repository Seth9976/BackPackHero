using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000018 RID: 24
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Rainbow", fileName = "Rainbow Behavior")]
	[EffectInfo("rainb", EffectCategory.Behaviors)]
	public sealed class RainbowBehavior : BehaviorScriptableBase
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000033D4 File Offset: 0x000015D4
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "f")
			{
				this.frequency = this.baseFrequency * modifier.value;
				return;
			}
			if (!(name == "s"))
			{
				return;
			}
			this.waveSize = this.baseWaveSize * modifier.value;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000342A File Offset: 0x0000162A
		public override void ResetContext(TAnimCore animator)
		{
			this.frequency = this.baseFrequency;
			this.waveSize = this.baseWaveSize;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003444 File Offset: 0x00001644
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			for (byte b = 0; b < 4; b += 1)
			{
				this.temp = Color.HSVToRGB(Mathf.PingPong(animator.time.timeSinceStart * this.frequency + (float)character.index * this.waveSize, 1f), 1f, 1f);
				this.temp.a = character.current.colors[(int)b].a;
				character.current.colors[(int)b] = this.temp;
			}
		}

		// Token: 0x0400004C RID: 76
		public float baseFrequency = 0.5f;

		// Token: 0x0400004D RID: 77
		public float baseWaveSize = 0.08f;

		// Token: 0x0400004E RID: 78
		private float frequency;

		// Token: 0x0400004F RID: 79
		private float waveSize;

		// Token: 0x04000050 RID: 80
		private Color32 temp;
	}
}

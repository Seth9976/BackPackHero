using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000024 RID: 36
	[Preserve]
	[CreateAssetMenu(fileName = "Uniform Curve Animation", menuName = "Text Animator/Animations/Special/Uniform Curve")]
	[EffectInfo("", EffectCategory.All)]
	public sealed class UniformCurveAnimation : AnimationScriptableBase
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00004073 File Offset: 0x00002273
		public override void ResetContext(TAnimCore animator)
		{
			this.weightMult = 1f;
			this.timeSpeed = 1f;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000408C File Offset: 0x0000228C
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "f")
			{
				this.timeSpeed = modifier.value;
				return;
			}
			if (!(name == "a"))
			{
				return;
			}
			this.weightMult = modifier.value;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000040D4 File Offset: 0x000022D4
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.timePassed = this.timeMode.GetTime(animator.time.timeSinceStart * this.timeSpeed, character.passedTime * this.timeSpeed, character.index);
			if (this.timePassed < 0f)
			{
				return;
			}
			float num = this.weightMult * this.emissionCurve.Evaluate(this.timePassed);
			Matrix4x4 matrix4x;
			Vector3 vector;
			if (this.animationData.TryCalculatingMatrix(character, this.timePassed, num, out matrix4x, out vector))
			{
				for (byte b = 0; b < 4; b += 1)
				{
					character.current.positions[(int)b] = matrix4x.MultiplyPoint3x4(character.current.positions[(int)b] - vector) + vector;
				}
			}
			Color32 color;
			if (this.animationData.TryCalculatingColor(character, this.timePassed, num, out color))
			{
				character.current.colors.LerpUnclamped(color, Mathf.Clamp01(num));
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000041D7 File Offset: 0x000023D7
		public override float GetMaxDuration()
		{
			return this.emissionCurve.GetMaxDuration();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000041E4 File Offset: 0x000023E4
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return true;
		}

		// Token: 0x04000072 RID: 114
		public TimeMode timeMode = new TimeMode(true);

		// Token: 0x04000073 RID: 115
		[EmissionCurveProperty]
		public EmissionCurve emissionCurve = new EmissionCurve();

		// Token: 0x04000074 RID: 116
		public AnimationData animationData = new AnimationData();

		// Token: 0x04000075 RID: 117
		private float weightMult;

		// Token: 0x04000076 RID: 118
		private float timeSpeed;

		// Token: 0x04000077 RID: 119
		private bool hasTransformEffects;

		// Token: 0x04000078 RID: 120
		private float timePassed;
	}
}

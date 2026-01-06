using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000019 RID: 25
	[Preserve]
	[CreateAssetMenu(menuName = "Text Animator/Animations/Behaviors/Rotation", fileName = "Rotation Behavior")]
	[EffectInfo("rot", EffectCategory.Behaviors)]
	public sealed class RotationBehavior : BehaviorScriptableBase
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00003500 File Offset: 0x00001700
		public override void SetModifier(ModifierInfo modifier)
		{
			string name = modifier.name;
			if (name == "f")
			{
				this.angleSpeed = this.baseRotSpeed * modifier.value;
				return;
			}
			if (!(name == "w"))
			{
				return;
			}
			this.angleDiffBetweenChars = this.baseDiffBetweenChars * modifier.value;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003556 File Offset: 0x00001756
		public override void ResetContext(TAnimCore animator)
		{
			this.angleSpeed = this.baseRotSpeed;
			this.angleDiffBetweenChars = this.baseDiffBetweenChars;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003570 File Offset: 0x00001770
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			character.current.positions.RotateChar(-animator.time.timeSinceStart * this.angleSpeed + this.angleDiffBetweenChars * (float)character.index);
		}

		// Token: 0x04000051 RID: 81
		public float baseRotSpeed = 180f;

		// Token: 0x04000052 RID: 82
		public float baseDiffBetweenChars = 10f;

		// Token: 0x04000053 RID: 83
		private float angleSpeed;

		// Token: 0x04000054 RID: 84
		private float angleDiffBetweenChars;
	}
}

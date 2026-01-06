using System;
using System.Collections.Generic;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000023 RID: 35
	[Preserve]
	[CreateAssetMenu(fileName = "Composite With Emission", menuName = "Text Animator/Animations/Special/Composite With Emission")]
	[EffectInfo("", EffectCategory.All)]
	public sealed class CompositeWithEmission : AnimationScriptableBase
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003DC0 File Offset: 0x00001FC0
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.ValidateArray();
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].InitializeOnce();
			}
			this.prev = default(MeshData);
			this.prev.colors = new Color32[4];
			this.prev.positions = new Vector3[4];
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003E24 File Offset: 0x00002024
		public override void ResetContext(TAnimCore animator)
		{
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ResetContext(animator);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003E50 File Offset: 0x00002050
		public override void SetModifier(ModifierInfo modifier)
		{
			base.SetModifier(modifier);
			AnimationScriptableBase[] array = this.animations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetModifier(modifier);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003E84 File Offset: 0x00002084
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			float time = this.timeMode.GetTime(animator.time.timeSinceStart, character.passedTime, character.index);
			if (time < 0f)
			{
				return;
			}
			for (int i = 0; i < 4; i++)
			{
				this.prev.positions[i] = character.current.positions[i];
				this.prev.colors[i] = character.current.colors[i];
			}
			float num = this.emissionCurve.Evaluate(time);
			foreach (AnimationScriptableBase animationScriptableBase in this.animations)
			{
				if (animationScriptableBase.CanApplyEffectTo(character, animator))
				{
					animationScriptableBase.ApplyEffectTo(ref character, animator);
				}
			}
			for (int k = 0; k < 4; k++)
			{
				character.current.positions[k] = Vector3.LerpUnclamped(this.prev.positions[k], character.current.positions[k], num);
				character.current.colors[k] = Color32.LerpUnclamped(this.prev.colors[k], character.current.colors[k], num);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003FDD File Offset: 0x000021DD
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return true;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003FE0 File Offset: 0x000021E0
		public override float GetMaxDuration()
		{
			return this.emissionCurve.GetMaxDuration();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003FF0 File Offset: 0x000021F0
		private void ValidateArray()
		{
			List<AnimationScriptableBase> list = new List<AnimationScriptableBase>();
			for (int i = 0; i < this.animations.Length; i++)
			{
				if (this.animations[i] != this)
				{
					list.Add(this.animations[i]);
				}
			}
			this.animations = list.ToArray();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004040 File Offset: 0x00002240
		private void OnValidate()
		{
			this.ValidateArray();
		}

		// Token: 0x0400006E RID: 110
		public TimeMode timeMode = new TimeMode(true);

		// Token: 0x0400006F RID: 111
		[EmissionCurveProperty]
		public EmissionCurve emissionCurve = new EmissionCurve();

		// Token: 0x04000070 RID: 112
		public AnimationScriptableBase[] animations = new AnimationScriptableBase[0];

		// Token: 0x04000071 RID: 113
		private MeshData prev;
	}
}

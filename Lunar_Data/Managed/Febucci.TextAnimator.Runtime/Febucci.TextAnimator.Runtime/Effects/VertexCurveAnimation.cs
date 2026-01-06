using System;
using Febucci.UI.Core;
using UnityEngine;
using UnityEngine.Scripting;

namespace Febucci.UI.Effects
{
	// Token: 0x02000025 RID: 37
	[Preserve]
	[CreateAssetMenu(fileName = "Vertex Curve Animation", menuName = "Text Animator/Animations/Special/Vertex Curve Animation")]
	[EffectInfo("", EffectCategory.All)]
	public sealed class VertexCurveAnimation : AnimationScriptableBase
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004211 File Offset: 0x00002411
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00004219 File Offset: 0x00002419
		public AnimationData[] VertexData
		{
			get
			{
				return this.animationPerVertexData;
			}
			set
			{
				this.animationPerVertexData = value;
				this.ClampVertexDataArray();
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004228 File Offset: 0x00002428
		public override void ResetContext(TAnimCore animator)
		{
			this.weightMult = 1f;
			this.timeSpeed = 1f;
			this.ClampVertexDataArray();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004248 File Offset: 0x00002448
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

		// Token: 0x06000093 RID: 147 RVA: 0x00004290 File Offset: 0x00002490
		public override void ApplyEffectTo(ref CharacterData character, TAnimCore animator)
		{
			this.timePassed = this.timeMode.GetTime(animator.time.timeSinceStart * this.timeSpeed, character.passedTime * this.timeSpeed, character.index);
			if (this.timePassed < 0f)
			{
				return;
			}
			float num = this.weightMult * this.emissionCurve.Evaluate(this.timePassed);
			for (byte b = 0; b < 4; b += 1)
			{
				if (this.animationPerVertexData[(int)b].TryCalculatingMatrix(character, this.timePassed, num, out this.matrix, out this.offset))
				{
					character.current.positions[(int)b] = this.matrix.MultiplyPoint3x4(character.current.positions[(int)b] - this.offset) + this.offset;
				}
				if (this.animationPerVertexData[(int)b].TryCalculatingColor(character, this.timePassed, num, out this.color))
				{
					character.current.colors[(int)b] = Color32.LerpUnclamped(character.current.colors[(int)b], this.color, Mathf.Clamp01(num));
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000043CD File Offset: 0x000025CD
		public override float GetMaxDuration()
		{
			return this.emissionCurve.GetMaxDuration();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000043DA File Offset: 0x000025DA
		public override bool CanApplyEffectTo(CharacterData character, TAnimCore animator)
		{
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000043E0 File Offset: 0x000025E0
		private void ClampVertexDataArray()
		{
			for (int i = 0; i < this.animationPerVertexData.Length; i++)
			{
				if (this.animationPerVertexData[i] == null)
				{
					this.animationPerVertexData[i] = new AnimationData();
				}
			}
			if (this.animationPerVertexData.Length != 4)
			{
				Debug.LogError("Vertex data array must have four vertices. Clamping/Resizing to four.");
				AnimationData[] array = new AnimationData[4];
				for (int j = 0; j < array.Length; j++)
				{
					if (j < this.animationPerVertexData.Length)
					{
						array[j] = this.animationPerVertexData[j];
					}
					else
					{
						array[j] = new AnimationData();
					}
				}
				this.animationPerVertexData = array;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004468 File Offset: 0x00002668
		private void OnValidate()
		{
			this.ClampVertexDataArray();
		}

		// Token: 0x04000079 RID: 121
		public TimeMode timeMode = new TimeMode(true);

		// Token: 0x0400007A RID: 122
		[EmissionCurveProperty]
		public EmissionCurve emissionCurve = new EmissionCurve();

		// Token: 0x0400007B RID: 123
		[SerializeField]
		private AnimationData[] animationPerVertexData = new AnimationData[4];

		// Token: 0x0400007C RID: 124
		private float timeSpeed;

		// Token: 0x0400007D RID: 125
		private float weightMult;

		// Token: 0x0400007E RID: 126
		private Matrix4x4 matrix;

		// Token: 0x0400007F RID: 127
		private Vector3 offset;

		// Token: 0x04000080 RID: 128
		private Vector3 movement;

		// Token: 0x04000081 RID: 129
		private Vector2 scale;

		// Token: 0x04000082 RID: 130
		private Quaternion rot;

		// Token: 0x04000083 RID: 131
		private Color32 color;

		// Token: 0x04000084 RID: 132
		private float timePassed;
	}
}

using System;
using Febucci.UI.Core;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class AnimationData
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000449C File Offset: 0x0000269C
		public bool TryCalculatingMatrix(CharacterData character, float timePassed, float weight, out Matrix4x4 matrix, out Vector3 offset)
		{
			matrix = default(Matrix4x4);
			if (!this.movementX.enabled && !this.movementY.enabled && !this.movementZ.enabled && !this.rotX.enabled && !this.rotY.enabled && !this.rotZ.enabled && !this.scaleX.enabled && !this.scaleY.enabled)
			{
				offset = Vector2.zero;
				return false;
			}
			offset = (character.current.positions[0] + character.current.positions[2]) / 2f;
			this.rot = Quaternion.Euler(Mathf.LerpUnclamped(0f, this.rotX.Evaluate(timePassed, character.index), weight), Mathf.LerpUnclamped(0f, this.rotY.Evaluate(timePassed, character.index), weight), Mathf.LerpUnclamped(0f, this.rotZ.Evaluate(timePassed, character.index), weight));
			this.movement = new Vector3(Mathf.LerpUnclamped(0f, this.movementX.Evaluate(timePassed, character.index), weight), Mathf.LerpUnclamped(0f, this.movementY.Evaluate(timePassed, character.index), weight), Mathf.LerpUnclamped(0f, this.movementZ.Evaluate(timePassed, character.index), weight));
			this.scale = new Vector2(Mathf.LerpUnclamped(1f, this.scaleX.Evaluate(timePassed, character.index), weight), Mathf.LerpUnclamped(1f, this.scaleY.Evaluate(timePassed, character.index), weight));
			matrix.SetTRS(this.movement, this.rot, this.scale);
			return true;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000468C File Offset: 0x0000288C
		public bool TryCalculatingColor(CharacterData character, float timePassed, float weight, out Color32 color)
		{
			if (!this.colorCurve.enabled)
			{
				color = Color.white;
				return false;
			}
			color = this.colorCurve.Evaluate(timePassed, character.index);
			return true;
		}

		// Token: 0x04000085 RID: 133
		[FloatCurveProperty]
		public FloatCurve movementX = new FloatCurve(1f, 0f, 0f);

		// Token: 0x04000086 RID: 134
		[FloatCurveProperty]
		public FloatCurve movementY = new FloatCurve(1f, 0f, 0f);

		// Token: 0x04000087 RID: 135
		[FloatCurveProperty]
		public FloatCurve movementZ = new FloatCurve(1f, 0f, 0f);

		// Token: 0x04000088 RID: 136
		[FloatCurveProperty]
		public FloatCurve scaleX = new FloatCurve(2f, 0f, 1f);

		// Token: 0x04000089 RID: 137
		[FloatCurveProperty]
		public FloatCurve scaleY = new FloatCurve(2f, 0f, 1f);

		// Token: 0x0400008A RID: 138
		[FloatCurveProperty]
		public FloatCurve rotX = new FloatCurve(45f, 0f, 0f);

		// Token: 0x0400008B RID: 139
		[FloatCurveProperty]
		public FloatCurve rotY = new FloatCurve(45f, 0f, 0f);

		// Token: 0x0400008C RID: 140
		[FloatCurveProperty]
		public FloatCurve rotZ = new FloatCurve(45f, 0f, 0f);

		// Token: 0x0400008D RID: 141
		[ColorCurveProperty]
		public ColorCurve colorCurve = new ColorCurve(0f);

		// Token: 0x0400008E RID: 142
		private Vector3 movement;

		// Token: 0x0400008F RID: 143
		private Vector2 scale;

		// Token: 0x04000090 RID: 144
		private Quaternion rot;
	}
}

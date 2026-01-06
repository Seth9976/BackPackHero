using System;
using Pathfinding.Drawing;
using Pathfinding.Util;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class AutoRepathPolicy
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00011F64 File Offset: 0x00010164
		public virtual bool ShouldRecalculatePath(Vector3 position, float radius, Vector3 destination, float time)
		{
			if (this.mode == AutoRepathPolicy.Mode.Never || float.IsPositiveInfinity(destination.x))
			{
				return false;
			}
			float num = time - this.lastRepathTime;
			if (this.mode == AutoRepathPolicy.Mode.EveryNSeconds)
			{
				return num >= this.period;
			}
			float num2 = (destination - this.lastDestination).sqrMagnitude / Mathf.Max((position - this.lastDestination).sqrMagnitude, radius * radius) * (this.sensitivity * this.sensitivity);
			if (float.IsNaN(num2))
			{
				num2 = 0f;
			}
			return num >= this.maximumPeriod * (1f - Mathf.Sqrt(num2));
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001200F File Offset: 0x0001020F
		public virtual void Reset()
		{
			this.lastRepathTime = float.NegativeInfinity;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001201C File Offset: 0x0001021C
		public virtual void DidRecalculatePath(Vector3 destination, float time)
		{
			this.lastRepathTime = time;
			this.lastDestination = destination;
			this.lastRepathTime -= (global::UnityEngine.Random.value - 0.5f) * 0.3f * ((this.mode == AutoRepathPolicy.Mode.Dynamic) ? this.maximumPeriod : this.period);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00012070 File Offset: 0x00010270
		public void DrawGizmos(CommandBuilder draw, Vector3 position, float radius, NativeMovementPlane movementPlane)
		{
			if (this.visualizeSensitivity && !float.IsPositiveInfinity(this.lastDestination.x))
			{
				float num = Mathf.Sqrt(Mathf.Max((position - this.lastDestination).sqrMagnitude, radius * radius) / (this.sensitivity * this.sensitivity));
				draw.Circle(this.lastDestination, movementPlane.ToWorld(float2.zero, 1f), num, Color.magenta);
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000120F0 File Offset: 0x000102F0
		public AutoRepathPolicy Clone()
		{
			return base.MemberwiseClone() as AutoRepathPolicy;
		}

		// Token: 0x04000254 RID: 596
		public AutoRepathPolicy.Mode mode = AutoRepathPolicy.Mode.Dynamic;

		// Token: 0x04000255 RID: 597
		[FormerlySerializedAs("interval")]
		public float period = 0.5f;

		// Token: 0x04000256 RID: 598
		public float sensitivity = 10f;

		// Token: 0x04000257 RID: 599
		[FormerlySerializedAs("maximumInterval")]
		public float maximumPeriod = 2f;

		// Token: 0x04000258 RID: 600
		public bool visualizeSensitivity;

		// Token: 0x04000259 RID: 601
		private Vector3 lastDestination = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x0400025A RID: 602
		private float lastRepathTime = float.NegativeInfinity;

		// Token: 0x02000067 RID: 103
		public enum Mode
		{
			// Token: 0x0400025C RID: 604
			Never,
			// Token: 0x0400025D RID: 605
			EveryNSeconds,
			// Token: 0x0400025E RID: 606
			Dynamic
		}
	}
}

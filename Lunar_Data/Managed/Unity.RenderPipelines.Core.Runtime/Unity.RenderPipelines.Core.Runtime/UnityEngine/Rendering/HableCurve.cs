using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000AA RID: 170
	public class HableCurve
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001B8D8 File Offset: 0x00019AD8
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0001B8E0 File Offset: 0x00019AE0
		public float whitePoint { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001B8E9 File Offset: 0x00019AE9
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0001B8F1 File Offset: 0x00019AF1
		public float inverseWhitePoint { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0001B8FA File Offset: 0x00019AFA
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0001B902 File Offset: 0x00019B02
		public float x0 { get; private set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001B90B File Offset: 0x00019B0B
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0001B913 File Offset: 0x00019B13
		public float x1 { get; private set; }

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001B91C File Offset: 0x00019B1C
		public HableCurve()
		{
			for (int i = 0; i < 3; i++)
			{
				this.segments[i] = new HableCurve.Segment();
			}
			this.uniforms = new HableCurve.Uniforms(this);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001B960 File Offset: 0x00019B60
		public float Eval(float x)
		{
			float num = x * this.inverseWhitePoint;
			int num2 = ((num < this.x0) ? 0 : ((num < this.x1) ? 1 : 2));
			return this.segments[num2].Eval(num);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001B9A0 File Offset: 0x00019BA0
		public void Init(float toeStrength, float toeLength, float shoulderStrength, float shoulderLength, float shoulderAngle, float gamma)
		{
			HableCurve.DirectParams directParams = default(HableCurve.DirectParams);
			toeLength = Mathf.Pow(Mathf.Clamp01(toeLength), 2.2f);
			toeStrength = Mathf.Clamp01(toeStrength);
			shoulderAngle = Mathf.Clamp01(shoulderAngle);
			shoulderStrength = Mathf.Clamp(shoulderStrength, 1E-05f, 0.99999f);
			shoulderLength = Mathf.Max(0f, shoulderLength);
			gamma = Mathf.Max(1E-05f, gamma);
			float num = toeLength * 0.5f;
			float num2 = (1f - toeStrength) * num;
			float num3 = 1f - num2;
			float num4 = num + num3;
			float num5 = (1f - shoulderStrength) * num3;
			float num6 = num + num5;
			float num7 = num2 + num5;
			float num8 = Mathf.Pow(2f, shoulderLength) - 1f;
			float num9 = num4 + num8;
			directParams.x0 = num;
			directParams.y0 = num2;
			directParams.x1 = num6;
			directParams.y1 = num7;
			directParams.W = num9;
			directParams.gamma = gamma;
			directParams.overshootX = directParams.W * 2f * shoulderAngle * shoulderLength;
			directParams.overshootY = 0.5f * shoulderAngle * shoulderLength;
			this.InitSegments(directParams);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001BABC File Offset: 0x00019CBC
		private void InitSegments(HableCurve.DirectParams srcParams)
		{
			HableCurve.DirectParams directParams = srcParams;
			this.whitePoint = srcParams.W;
			this.inverseWhitePoint = 1f / srcParams.W;
			directParams.W = 1f;
			directParams.x0 /= srcParams.W;
			directParams.x1 /= srcParams.W;
			directParams.overshootX = srcParams.overshootX / srcParams.W;
			float num;
			float num2;
			this.AsSlopeIntercept(out num, out num2, directParams.x0, directParams.x1, directParams.y0, directParams.y1);
			float gamma = srcParams.gamma;
			HableCurve.Segment segment = this.segments[1];
			segment.offsetX = -(num2 / num);
			segment.offsetY = 0f;
			segment.scaleX = 1f;
			segment.scaleY = 1f;
			segment.lnA = gamma * Mathf.Log(num);
			segment.B = gamma;
			float num3 = this.EvalDerivativeLinearGamma(num, num2, gamma, directParams.x0);
			float num4 = this.EvalDerivativeLinearGamma(num, num2, gamma, directParams.x1);
			directParams.y0 = Mathf.Max(1E-05f, Mathf.Pow(directParams.y0, directParams.gamma));
			directParams.y1 = Mathf.Max(1E-05f, Mathf.Pow(directParams.y1, directParams.gamma));
			directParams.overshootY = Mathf.Pow(1f + directParams.overshootY, directParams.gamma) - 1f;
			this.x0 = directParams.x0;
			this.x1 = directParams.x1;
			HableCurve.Segment segment2 = this.segments[0];
			segment2.offsetX = 0f;
			segment2.offsetY = 0f;
			segment2.scaleX = 1f;
			segment2.scaleY = 1f;
			float num5;
			float num6;
			this.SolveAB(out num5, out num6, directParams.x0, directParams.y0, num3);
			segment2.lnA = num5;
			segment2.B = num6;
			HableCurve.Segment segment3 = this.segments[2];
			float num7 = 1f + directParams.overshootX - directParams.x1;
			float num8 = 1f + directParams.overshootY - directParams.y1;
			float num9;
			float num10;
			this.SolveAB(out num9, out num10, num7, num8, num4);
			segment3.offsetX = 1f + directParams.overshootX;
			segment3.offsetY = 1f + directParams.overshootY;
			segment3.scaleX = -1f;
			segment3.scaleY = -1f;
			segment3.lnA = num9;
			segment3.B = num10;
			float num11 = this.segments[2].Eval(1f);
			float num12 = 1f / num11;
			this.segments[0].offsetY *= num12;
			this.segments[0].scaleY *= num12;
			this.segments[1].offsetY *= num12;
			this.segments[1].scaleY *= num12;
			this.segments[2].offsetY *= num12;
			this.segments[2].scaleY *= num12;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001BDD5 File Offset: 0x00019FD5
		private void SolveAB(out float lnA, out float B, float x0, float y0, float m)
		{
			B = m * x0 / y0;
			lnA = Mathf.Log(y0) - B * Mathf.Log(x0);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		private void AsSlopeIntercept(out float m, out float b, float x0, float x1, float y0, float y1)
		{
			float num = y1 - y0;
			float num2 = x1 - x0;
			if (num2 == 0f)
			{
				m = 1f;
			}
			else
			{
				m = num / num2;
			}
			b = y0 - x0 * m;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001BE2B File Offset: 0x0001A02B
		private float EvalDerivativeLinearGamma(float m, float b, float g, float x)
		{
			return g * m * Mathf.Pow(m * x + b, g - 1f);
		}

		// Token: 0x0400036D RID: 877
		public readonly HableCurve.Segment[] segments = new HableCurve.Segment[3];

		// Token: 0x0400036E RID: 878
		public readonly HableCurve.Uniforms uniforms;

		// Token: 0x02000177 RID: 375
		public class Segment
		{
			// Token: 0x06000901 RID: 2305 RVA: 0x00024B74 File Offset: 0x00022D74
			public float Eval(float x)
			{
				float num = (x - this.offsetX) * this.scaleX;
				float num2 = 0f;
				if (num > 0f)
				{
					num2 = Mathf.Exp(this.lnA + this.B * Mathf.Log(num));
				}
				return num2 * this.scaleY + this.offsetY;
			}

			// Token: 0x040005A5 RID: 1445
			public float offsetX;

			// Token: 0x040005A6 RID: 1446
			public float offsetY;

			// Token: 0x040005A7 RID: 1447
			public float scaleX;

			// Token: 0x040005A8 RID: 1448
			public float scaleY;

			// Token: 0x040005A9 RID: 1449
			public float lnA;

			// Token: 0x040005AA RID: 1450
			public float B;
		}

		// Token: 0x02000178 RID: 376
		private struct DirectParams
		{
			// Token: 0x040005AB RID: 1451
			internal float x0;

			// Token: 0x040005AC RID: 1452
			internal float y0;

			// Token: 0x040005AD RID: 1453
			internal float x1;

			// Token: 0x040005AE RID: 1454
			internal float y1;

			// Token: 0x040005AF RID: 1455
			internal float W;

			// Token: 0x040005B0 RID: 1456
			internal float overshootX;

			// Token: 0x040005B1 RID: 1457
			internal float overshootY;

			// Token: 0x040005B2 RID: 1458
			internal float gamma;
		}

		// Token: 0x02000179 RID: 377
		public class Uniforms
		{
			// Token: 0x06000903 RID: 2307 RVA: 0x00024BD0 File Offset: 0x00022DD0
			internal Uniforms(HableCurve parent)
			{
				this.parent = parent;
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000904 RID: 2308 RVA: 0x00024BDF File Offset: 0x00022DDF
			public Vector4 curve
			{
				get
				{
					return new Vector4(this.parent.inverseWhitePoint, this.parent.x0, this.parent.x1, 0f);
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000905 RID: 2309 RVA: 0x00024C0C File Offset: 0x00022E0C
			public Vector4 toeSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[0].offsetX, this.parent.segments[0].offsetY, this.parent.segments[0].scaleX, this.parent.segments[0].scaleY);
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06000906 RID: 2310 RVA: 0x00024C66 File Offset: 0x00022E66
			public Vector4 toeSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[0].lnA, this.parent.segments[0].B, 0f, 0f);
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x06000907 RID: 2311 RVA: 0x00024C9C File Offset: 0x00022E9C
			public Vector4 midSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[1].offsetX, this.parent.segments[1].offsetY, this.parent.segments[1].scaleX, this.parent.segments[1].scaleY);
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000908 RID: 2312 RVA: 0x00024CF6 File Offset: 0x00022EF6
			public Vector4 midSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[1].lnA, this.parent.segments[1].B, 0f, 0f);
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06000909 RID: 2313 RVA: 0x00024D2C File Offset: 0x00022F2C
			public Vector4 shoSegmentA
			{
				get
				{
					return new Vector4(this.parent.segments[2].offsetX, this.parent.segments[2].offsetY, this.parent.segments[2].scaleX, this.parent.segments[2].scaleY);
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x0600090A RID: 2314 RVA: 0x00024D86 File Offset: 0x00022F86
			public Vector4 shoSegmentB
			{
				get
				{
					return new Vector4(this.parent.segments[2].lnA, this.parent.segments[2].B, 0f, 0f);
				}
			}

			// Token: 0x040005B3 RID: 1459
			private HableCurve parent;
		}
	}
}

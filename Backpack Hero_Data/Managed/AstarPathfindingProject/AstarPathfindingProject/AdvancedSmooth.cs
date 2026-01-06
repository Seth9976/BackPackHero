using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000073 RID: 115
	[AddComponentMenu("Pathfinding/Modifiers/Advanced Smooth")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_advanced_smooth.php")]
	[Serializable]
	public class AdvancedSmooth : MonoModifier
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00024367 File Offset: 0x00022567
		public override int Order
		{
			get
			{
				return 40;
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0002436C File Offset: 0x0002256C
		public override void Apply(Path p)
		{
			Vector3[] array = p.vectorPath.ToArray();
			if (array == null || array.Length <= 2)
			{
				return;
			}
			List<Vector3> list = new List<Vector3>();
			list.Add(array[0]);
			AdvancedSmooth.TurnConstructor.turningRadius = this.turningRadius;
			for (int i = 1; i < array.Length - 1; i++)
			{
				List<AdvancedSmooth.Turn> list2 = new List<AdvancedSmooth.Turn>();
				AdvancedSmooth.TurnConstructor.Setup(i, array);
				this.turnConstruct1.Prepare(i, array);
				this.turnConstruct2.Prepare(i, array);
				AdvancedSmooth.TurnConstructor.PostPrepare();
				if (i == 1)
				{
					this.turnConstruct1.PointToTangent(list2);
					this.turnConstruct2.PointToTangent(list2);
				}
				else
				{
					this.turnConstruct1.TangentToTangent(list2);
					this.turnConstruct2.TangentToTangent(list2);
				}
				this.EvaluatePaths(list2, list);
				if (i == array.Length - 2)
				{
					this.turnConstruct1.TangentToPoint(list2);
					this.turnConstruct2.TangentToPoint(list2);
				}
				this.EvaluatePaths(list2, list);
			}
			list.Add(array[array.Length - 1]);
			p.vectorPath = list;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00024470 File Offset: 0x00022670
		private void EvaluatePaths(List<AdvancedSmooth.Turn> turnList, List<Vector3> output)
		{
			turnList.Sort();
			for (int i = 0; i < turnList.Count; i++)
			{
				if (i == 0)
				{
					turnList[i].GetPath(output);
				}
			}
			turnList.Clear();
			if (AdvancedSmooth.TurnConstructor.changedPreviousTangent)
			{
				this.turnConstruct1.OnTangentUpdate();
				this.turnConstruct2.OnTangentUpdate();
			}
		}

		// Token: 0x04000377 RID: 887
		public float turningRadius = 1f;

		// Token: 0x04000378 RID: 888
		public AdvancedSmooth.MaxTurn turnConstruct1 = new AdvancedSmooth.MaxTurn();

		// Token: 0x04000379 RID: 889
		public AdvancedSmooth.ConstantTurn turnConstruct2 = new AdvancedSmooth.ConstantTurn();

		// Token: 0x02000133 RID: 307
		[Serializable]
		public class MaxTurn : AdvancedSmooth.TurnConstructor
		{
			// Token: 0x06000AC1 RID: 2753 RVA: 0x000435E8 File Offset: 0x000417E8
			public override void OnTangentUpdate()
			{
				this.rightCircleCenter = AdvancedSmooth.TurnConstructor.current + AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.leftCircleCenter = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.vaRight = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.rightCircleCenter);
				this.vaLeft = this.vaRight + 3.141592653589793;
			}

			// Token: 0x06000AC2 RID: 2754 RVA: 0x00043668 File Offset: 0x00041868
			public override void Prepare(int i, Vector3[] vectorPath)
			{
				this.preRightCircleCenter = this.rightCircleCenter;
				this.preLeftCircleCenter = this.leftCircleCenter;
				this.rightCircleCenter = AdvancedSmooth.TurnConstructor.current + AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.leftCircleCenter = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.normal * AdvancedSmooth.TurnConstructor.turningRadius;
				this.preVaRight = this.vaRight;
				this.preVaLeft = this.vaLeft;
				this.vaRight = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.rightCircleCenter);
				this.vaLeft = this.vaRight + 3.141592653589793;
			}

			// Token: 0x06000AC3 RID: 2755 RVA: 0x00043718 File Offset: 0x00041918
			public override void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
			{
				this.alfaRightRight = base.Atan2(this.rightCircleCenter - this.preRightCircleCenter);
				this.alfaLeftLeft = base.Atan2(this.leftCircleCenter - this.preLeftCircleCenter);
				this.alfaRightLeft = base.Atan2(this.leftCircleCenter - this.preRightCircleCenter);
				this.alfaLeftRight = base.Atan2(this.rightCircleCenter - this.preLeftCircleCenter);
				double num = (double)(this.leftCircleCenter - this.preRightCircleCenter).magnitude;
				double num2 = (double)(this.rightCircleCenter - this.preLeftCircleCenter).magnitude;
				bool flag = false;
				bool flag2 = false;
				if (num < (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f))
				{
					num = (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f);
					flag = true;
				}
				if (num2 < (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f))
				{
					num2 = (double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f);
					flag2 = true;
				}
				this.deltaRightLeft = (flag ? 0.0 : (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f) / num)));
				this.deltaLeftRight = (flag2 ? 0.0 : (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius * 2f) / num2)));
				this.betaRightRight = base.ClockwiseAngle(this.preVaRight, this.alfaRightRight - 1.5707963267948966);
				this.betaRightLeft = base.ClockwiseAngle(this.preVaRight, this.alfaRightLeft - this.deltaRightLeft);
				this.betaLeftRight = base.CounterClockwiseAngle(this.preVaLeft, this.alfaLeftRight + this.deltaLeftRight);
				this.betaLeftLeft = base.CounterClockwiseAngle(this.preVaLeft, this.alfaLeftLeft + 1.5707963267948966);
				this.betaRightRight += base.ClockwiseAngle(this.alfaRightRight - 1.5707963267948966, this.vaRight);
				this.betaRightLeft += base.CounterClockwiseAngle(this.alfaRightLeft + this.deltaRightLeft, this.vaLeft);
				this.betaLeftRight += base.ClockwiseAngle(this.alfaLeftRight - this.deltaLeftRight, this.vaRight);
				this.betaLeftLeft += base.CounterClockwiseAngle(this.alfaLeftLeft + 1.5707963267948966, this.vaLeft);
				this.betaRightRight = base.GetLengthFromAngle(this.betaRightRight, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				this.betaRightLeft = base.GetLengthFromAngle(this.betaRightLeft, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				this.betaLeftRight = base.GetLengthFromAngle(this.betaLeftRight, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				this.betaLeftLeft = base.GetLengthFromAngle(this.betaLeftLeft, (double)AdvancedSmooth.TurnConstructor.turningRadius);
				Vector3 vector = base.AngleToVector(this.alfaRightRight - 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.preRightCircleCenter;
				Vector3 vector2 = base.AngleToVector(this.alfaRightLeft - this.deltaRightLeft) * AdvancedSmooth.TurnConstructor.turningRadius + this.preRightCircleCenter;
				Vector3 vector3 = base.AngleToVector(this.alfaLeftRight + this.deltaLeftRight) * AdvancedSmooth.TurnConstructor.turningRadius + this.preLeftCircleCenter;
				Vector3 vector4 = base.AngleToVector(this.alfaLeftLeft + 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.preLeftCircleCenter;
				Vector3 vector5 = base.AngleToVector(this.alfaRightRight - 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.rightCircleCenter;
				Vector3 vector6 = base.AngleToVector(this.alfaRightLeft - this.deltaRightLeft + 3.141592653589793) * AdvancedSmooth.TurnConstructor.turningRadius + this.leftCircleCenter;
				Vector3 vector7 = base.AngleToVector(this.alfaLeftRight + this.deltaLeftRight + 3.141592653589793) * AdvancedSmooth.TurnConstructor.turningRadius + this.rightCircleCenter;
				Vector3 vector8 = base.AngleToVector(this.alfaLeftLeft + 1.5707963267948966) * AdvancedSmooth.TurnConstructor.turningRadius + this.leftCircleCenter;
				this.betaRightRight += (double)(vector - vector5).magnitude;
				this.betaRightLeft += (double)(vector2 - vector6).magnitude;
				this.betaLeftRight += (double)(vector3 - vector7).magnitude;
				this.betaLeftLeft += (double)(vector4 - vector8).magnitude;
				if (flag)
				{
					this.betaRightLeft += 10000000.0;
				}
				if (flag2)
				{
					this.betaLeftRight += 10000000.0;
				}
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaRightRight, this, 2));
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaRightLeft, this, 3));
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaLeftRight, this, 4));
				turnList.Add(new AdvancedSmooth.Turn((float)this.betaLeftLeft, this, 5));
			}

			// Token: 0x06000AC4 RID: 2756 RVA: 0x00043C68 File Offset: 0x00041E68
			public override void PointToTangent(List<AdvancedSmooth.Turn> turnList)
			{
				bool flag = false;
				bool flag2 = false;
				float magnitude = (AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter).magnitude;
				float magnitude2 = (AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter).magnitude;
				if (magnitude < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag = true;
				}
				if (magnitude2 < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag2 = true;
				}
				double num = (flag ? 0.0 : base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter));
				double num2 = (flag ? 0.0 : (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / (AdvancedSmooth.TurnConstructor.prev - this.rightCircleCenter).magnitude))));
				this.gammaRight = num + num2;
				double num3 = (flag ? 0.0 : base.ClockwiseAngle(this.gammaRight, this.vaRight));
				double num4 = (flag2 ? 0.0 : base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter));
				double num5 = (flag2 ? 0.0 : (1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / (AdvancedSmooth.TurnConstructor.prev - this.leftCircleCenter).magnitude))));
				this.gammaLeft = num4 - num5;
				double num6 = (flag2 ? 0.0 : base.CounterClockwiseAngle(this.gammaLeft, this.vaLeft));
				if (!flag)
				{
					turnList.Add(new AdvancedSmooth.Turn((float)num3, this, 0));
				}
				if (!flag2)
				{
					turnList.Add(new AdvancedSmooth.Turn((float)num6, this, 1));
				}
			}

			// Token: 0x06000AC5 RID: 2757 RVA: 0x00043E08 File Offset: 0x00042008
			public override void TangentToPoint(List<AdvancedSmooth.Turn> turnList)
			{
				bool flag = false;
				bool flag2 = false;
				float magnitude = (AdvancedSmooth.TurnConstructor.next - this.rightCircleCenter).magnitude;
				float magnitude2 = (AdvancedSmooth.TurnConstructor.next - this.leftCircleCenter).magnitude;
				if (magnitude < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag = true;
				}
				if (magnitude2 < AdvancedSmooth.TurnConstructor.turningRadius)
				{
					flag2 = true;
				}
				if (!flag)
				{
					double num = base.Atan2(AdvancedSmooth.TurnConstructor.next - this.rightCircleCenter);
					double num2 = 1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / magnitude));
					this.gammaRight = num - num2;
					double num3 = base.ClockwiseAngle(this.vaRight, this.gammaRight);
					turnList.Add(new AdvancedSmooth.Turn((float)num3, this, 6));
				}
				if (!flag2)
				{
					double num4 = base.Atan2(AdvancedSmooth.TurnConstructor.next - this.leftCircleCenter);
					double num5 = 1.5707963267948966 - Math.Asin((double)(AdvancedSmooth.TurnConstructor.turningRadius / magnitude2));
					this.gammaLeft = num4 + num5;
					double num6 = base.CounterClockwiseAngle(this.vaLeft, this.gammaLeft);
					turnList.Add(new AdvancedSmooth.Turn((float)num6, this, 7));
				}
			}

			// Token: 0x06000AC6 RID: 2758 RVA: 0x00043F28 File Offset: 0x00042128
			public override void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output)
			{
				switch (turn.id)
				{
				case 0:
					base.AddCircleSegment(this.gammaRight, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 1:
					base.AddCircleSegment(this.gammaLeft, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 2:
					base.AddCircleSegment(this.preVaRight, this.alfaRightRight - 1.5707963267948966, true, this.preRightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaRightRight - 1.5707963267948966, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 3:
					base.AddCircleSegment(this.preVaRight, this.alfaRightLeft - this.deltaRightLeft, true, this.preRightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaRightLeft - this.deltaRightLeft + 3.141592653589793, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 4:
					base.AddCircleSegment(this.preVaLeft, this.alfaLeftRight + this.deltaLeftRight, false, this.preLeftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaLeftRight + this.deltaLeftRight + 3.141592653589793, this.vaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 5:
					base.AddCircleSegment(this.preVaLeft, this.alfaLeftLeft + 1.5707963267948966, false, this.preLeftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					base.AddCircleSegment(this.alfaLeftLeft + 1.5707963267948966, this.vaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 6:
					base.AddCircleSegment(this.vaRight, this.gammaRight, true, this.rightCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				case 7:
					base.AddCircleSegment(this.vaLeft, this.gammaLeft, false, this.leftCircleCenter, output, AdvancedSmooth.TurnConstructor.turningRadius);
					return;
				default:
					return;
				}
			}

			// Token: 0x04000703 RID: 1795
			private Vector3 preRightCircleCenter = Vector3.zero;

			// Token: 0x04000704 RID: 1796
			private Vector3 preLeftCircleCenter = Vector3.zero;

			// Token: 0x04000705 RID: 1797
			private Vector3 rightCircleCenter;

			// Token: 0x04000706 RID: 1798
			private Vector3 leftCircleCenter;

			// Token: 0x04000707 RID: 1799
			private double vaRight;

			// Token: 0x04000708 RID: 1800
			private double vaLeft;

			// Token: 0x04000709 RID: 1801
			private double preVaLeft;

			// Token: 0x0400070A RID: 1802
			private double preVaRight;

			// Token: 0x0400070B RID: 1803
			private double gammaLeft;

			// Token: 0x0400070C RID: 1804
			private double gammaRight;

			// Token: 0x0400070D RID: 1805
			private double betaRightRight;

			// Token: 0x0400070E RID: 1806
			private double betaRightLeft;

			// Token: 0x0400070F RID: 1807
			private double betaLeftRight;

			// Token: 0x04000710 RID: 1808
			private double betaLeftLeft;

			// Token: 0x04000711 RID: 1809
			private double deltaRightLeft;

			// Token: 0x04000712 RID: 1810
			private double deltaLeftRight;

			// Token: 0x04000713 RID: 1811
			private double alfaRightRight;

			// Token: 0x04000714 RID: 1812
			private double alfaLeftLeft;

			// Token: 0x04000715 RID: 1813
			private double alfaRightLeft;

			// Token: 0x04000716 RID: 1814
			private double alfaLeftRight;
		}

		// Token: 0x02000134 RID: 308
		[Serializable]
		public class ConstantTurn : AdvancedSmooth.TurnConstructor
		{
			// Token: 0x06000AC8 RID: 2760 RVA: 0x00044154 File Offset: 0x00042354
			public override void Prepare(int i, Vector3[] vectorPath)
			{
			}

			// Token: 0x06000AC9 RID: 2761 RVA: 0x00044158 File Offset: 0x00042358
			public override void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
			{
				Vector3 vector = Vector3.Cross(AdvancedSmooth.TurnConstructor.t1, Vector3.up);
				Vector3 vector2 = AdvancedSmooth.TurnConstructor.current - AdvancedSmooth.TurnConstructor.prev;
				Vector3 vector3 = vector2 * 0.5f + AdvancedSmooth.TurnConstructor.prev;
				vector2 = Vector3.Cross(vector2, Vector3.up);
				bool flag;
				this.circleCenter = VectorMath.LineDirIntersectionPointXZ(AdvancedSmooth.TurnConstructor.prev, vector, vector3, vector2, out flag);
				if (!flag)
				{
					return;
				}
				this.gamma1 = base.Atan2(AdvancedSmooth.TurnConstructor.prev - this.circleCenter);
				this.gamma2 = base.Atan2(AdvancedSmooth.TurnConstructor.current - this.circleCenter);
				this.clockwise = !VectorMath.RightOrColinearXZ(this.circleCenter, AdvancedSmooth.TurnConstructor.prev, AdvancedSmooth.TurnConstructor.prev + AdvancedSmooth.TurnConstructor.t1);
				double num = (this.clockwise ? base.ClockwiseAngle(this.gamma1, this.gamma2) : base.CounterClockwiseAngle(this.gamma1, this.gamma2));
				num = base.GetLengthFromAngle(num, (double)(this.circleCenter - AdvancedSmooth.TurnConstructor.current).magnitude);
				turnList.Add(new AdvancedSmooth.Turn((float)num, this, 0));
			}

			// Token: 0x06000ACA RID: 2762 RVA: 0x00044284 File Offset: 0x00042484
			public override void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output)
			{
				base.AddCircleSegment(this.gamma1, this.gamma2, this.clockwise, this.circleCenter, output, (this.circleCenter - AdvancedSmooth.TurnConstructor.current).magnitude);
				AdvancedSmooth.TurnConstructor.normal = (AdvancedSmooth.TurnConstructor.current - this.circleCenter).normalized;
				AdvancedSmooth.TurnConstructor.t2 = Vector3.Cross(AdvancedSmooth.TurnConstructor.normal, Vector3.up).normalized;
				AdvancedSmooth.TurnConstructor.normal = -AdvancedSmooth.TurnConstructor.normal;
				if (!this.clockwise)
				{
					AdvancedSmooth.TurnConstructor.t2 = -AdvancedSmooth.TurnConstructor.t2;
					AdvancedSmooth.TurnConstructor.normal = -AdvancedSmooth.TurnConstructor.normal;
				}
				AdvancedSmooth.TurnConstructor.changedPreviousTangent = true;
			}

			// Token: 0x04000717 RID: 1815
			private Vector3 circleCenter;

			// Token: 0x04000718 RID: 1816
			private double gamma1;

			// Token: 0x04000719 RID: 1817
			private double gamma2;

			// Token: 0x0400071A RID: 1818
			private bool clockwise;
		}

		// Token: 0x02000135 RID: 309
		public abstract class TurnConstructor
		{
			// Token: 0x06000ACC RID: 2764
			public abstract void Prepare(int i, Vector3[] vectorPath);

			// Token: 0x06000ACD RID: 2765 RVA: 0x00044344 File Offset: 0x00042544
			public virtual void OnTangentUpdate()
			{
			}

			// Token: 0x06000ACE RID: 2766 RVA: 0x00044346 File Offset: 0x00042546
			public virtual void PointToTangent(List<AdvancedSmooth.Turn> turnList)
			{
			}

			// Token: 0x06000ACF RID: 2767 RVA: 0x00044348 File Offset: 0x00042548
			public virtual void TangentToPoint(List<AdvancedSmooth.Turn> turnList)
			{
			}

			// Token: 0x06000AD0 RID: 2768 RVA: 0x0004434A File Offset: 0x0004254A
			public virtual void TangentToTangent(List<AdvancedSmooth.Turn> turnList)
			{
			}

			// Token: 0x06000AD1 RID: 2769
			public abstract void GetPath(AdvancedSmooth.Turn turn, List<Vector3> output);

			// Token: 0x06000AD2 RID: 2770 RVA: 0x0004434C File Offset: 0x0004254C
			public static void Setup(int i, Vector3[] vectorPath)
			{
				AdvancedSmooth.TurnConstructor.current = vectorPath[i];
				AdvancedSmooth.TurnConstructor.prev = vectorPath[i - 1];
				AdvancedSmooth.TurnConstructor.next = vectorPath[i + 1];
				AdvancedSmooth.TurnConstructor.prev.y = AdvancedSmooth.TurnConstructor.current.y;
				AdvancedSmooth.TurnConstructor.next.y = AdvancedSmooth.TurnConstructor.current.y;
				AdvancedSmooth.TurnConstructor.t1 = AdvancedSmooth.TurnConstructor.t2;
				AdvancedSmooth.TurnConstructor.t2 = (AdvancedSmooth.TurnConstructor.next - AdvancedSmooth.TurnConstructor.current).normalized - (AdvancedSmooth.TurnConstructor.prev - AdvancedSmooth.TurnConstructor.current).normalized;
				AdvancedSmooth.TurnConstructor.t2 = AdvancedSmooth.TurnConstructor.t2.normalized;
				AdvancedSmooth.TurnConstructor.prevNormal = AdvancedSmooth.TurnConstructor.normal;
				AdvancedSmooth.TurnConstructor.normal = Vector3.Cross(AdvancedSmooth.TurnConstructor.t2, Vector3.up);
				AdvancedSmooth.TurnConstructor.normal = AdvancedSmooth.TurnConstructor.normal.normalized;
			}

			// Token: 0x06000AD3 RID: 2771 RVA: 0x00044427 File Offset: 0x00042627
			public static void PostPrepare()
			{
				AdvancedSmooth.TurnConstructor.changedPreviousTangent = false;
			}

			// Token: 0x06000AD4 RID: 2772 RVA: 0x00044430 File Offset: 0x00042630
			public void AddCircleSegment(double startAngle, double endAngle, bool clockwise, Vector3 center, List<Vector3> output, float radius)
			{
				double num = 0.06283185307179587;
				if (clockwise)
				{
					while (endAngle > startAngle + 6.283185307179586)
					{
						endAngle -= 6.283185307179586;
					}
					while (endAngle < startAngle)
					{
						endAngle += 6.283185307179586;
					}
				}
				else
				{
					while (endAngle < startAngle - 6.283185307179586)
					{
						endAngle += 6.283185307179586;
					}
					while (endAngle > startAngle)
					{
						endAngle -= 6.283185307179586;
					}
				}
				if (clockwise)
				{
					for (double num2 = startAngle; num2 < endAngle; num2 += num)
					{
						output.Add(this.AngleToVector(num2) * radius + center);
					}
				}
				else
				{
					for (double num3 = startAngle; num3 > endAngle; num3 -= num)
					{
						output.Add(this.AngleToVector(num3) * radius + center);
					}
				}
				output.Add(this.AngleToVector(endAngle) * radius + center);
			}

			// Token: 0x06000AD5 RID: 2773 RVA: 0x0004451C File Offset: 0x0004271C
			public void DebugCircleSegment(Vector3 center, double startAngle, double endAngle, double radius, Color color)
			{
				double num = 0.06283185307179587;
				while (endAngle < startAngle)
				{
					endAngle += 6.283185307179586;
				}
				Vector3 vector = this.AngleToVector(startAngle) * (float)radius + center;
				for (double num2 = startAngle + num; num2 < endAngle; num2 += num)
				{
					Debug.DrawLine(vector, this.AngleToVector(num2) * (float)radius + center);
				}
				Debug.DrawLine(vector, this.AngleToVector(endAngle) * (float)radius + center);
			}

			// Token: 0x06000AD6 RID: 2774 RVA: 0x000445A0 File Offset: 0x000427A0
			public void DebugCircle(Vector3 center, double radius, Color color)
			{
				double num = 0.06283185307179587;
				Vector3 vector = this.AngleToVector(-num) * (float)radius + center;
				for (double num2 = 0.0; num2 < 6.283185307179586; num2 += num)
				{
					Vector3 vector2 = this.AngleToVector(num2) * (float)radius + center;
					Debug.DrawLine(vector, vector2, color);
					vector = vector2;
				}
			}

			// Token: 0x06000AD7 RID: 2775 RVA: 0x00044608 File Offset: 0x00042808
			public double GetLengthFromAngle(double angle, double radius)
			{
				return radius * angle;
			}

			// Token: 0x06000AD8 RID: 2776 RVA: 0x0004460D File Offset: 0x0004280D
			public double ClockwiseAngle(double from, double to)
			{
				return this.ClampAngle(to - from);
			}

			// Token: 0x06000AD9 RID: 2777 RVA: 0x00044618 File Offset: 0x00042818
			public double CounterClockwiseAngle(double from, double to)
			{
				return this.ClampAngle(from - to);
			}

			// Token: 0x06000ADA RID: 2778 RVA: 0x00044623 File Offset: 0x00042823
			public Vector3 AngleToVector(double a)
			{
				return new Vector3((float)Math.Cos(a), 0f, (float)Math.Sin(a));
			}

			// Token: 0x06000ADB RID: 2779 RVA: 0x0004463D File Offset: 0x0004283D
			public double ToDegrees(double rad)
			{
				return rad * 57.295780181884766;
			}

			// Token: 0x06000ADC RID: 2780 RVA: 0x0004464A File Offset: 0x0004284A
			public double ClampAngle(double a)
			{
				while (a < 0.0)
				{
					a += 6.283185307179586;
				}
				while (a > 6.283185307179586)
				{
					a -= 6.283185307179586;
				}
				return a;
			}

			// Token: 0x06000ADD RID: 2781 RVA: 0x00044683 File Offset: 0x00042883
			public double Atan2(Vector3 v)
			{
				return Math.Atan2((double)v.z, (double)v.x);
			}

			// Token: 0x0400071B RID: 1819
			public float constantBias;

			// Token: 0x0400071C RID: 1820
			public float factorBias = 1f;

			// Token: 0x0400071D RID: 1821
			public static float turningRadius = 1f;

			// Token: 0x0400071E RID: 1822
			public const double ThreeSixtyRadians = 6.283185307179586;

			// Token: 0x0400071F RID: 1823
			public static Vector3 prev;

			// Token: 0x04000720 RID: 1824
			public static Vector3 current;

			// Token: 0x04000721 RID: 1825
			public static Vector3 next;

			// Token: 0x04000722 RID: 1826
			public static Vector3 t1;

			// Token: 0x04000723 RID: 1827
			public static Vector3 t2;

			// Token: 0x04000724 RID: 1828
			public static Vector3 normal;

			// Token: 0x04000725 RID: 1829
			public static Vector3 prevNormal;

			// Token: 0x04000726 RID: 1830
			public static bool changedPreviousTangent = false;
		}

		// Token: 0x02000136 RID: 310
		public struct Turn : IComparable<AdvancedSmooth.Turn>
		{
			// Token: 0x1700018B RID: 395
			// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x000446BD File Offset: 0x000428BD
			public float score
			{
				get
				{
					return this.length * this.constructor.factorBias + this.constructor.constantBias;
				}
			}

			// Token: 0x06000AE1 RID: 2785 RVA: 0x000446DD File Offset: 0x000428DD
			public Turn(float length, AdvancedSmooth.TurnConstructor constructor, int id = 0)
			{
				this.length = length;
				this.id = id;
				this.constructor = constructor;
			}

			// Token: 0x06000AE2 RID: 2786 RVA: 0x000446F4 File Offset: 0x000428F4
			public void GetPath(List<Vector3> output)
			{
				this.constructor.GetPath(this, output);
			}

			// Token: 0x06000AE3 RID: 2787 RVA: 0x00044708 File Offset: 0x00042908
			public int CompareTo(AdvancedSmooth.Turn t)
			{
				if (t.score > this.score)
				{
					return -1;
				}
				if (t.score >= this.score)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06000AE4 RID: 2788 RVA: 0x0004472D File Offset: 0x0004292D
			public static bool operator <(AdvancedSmooth.Turn lhs, AdvancedSmooth.Turn rhs)
			{
				return lhs.score < rhs.score;
			}

			// Token: 0x06000AE5 RID: 2789 RVA: 0x0004473F File Offset: 0x0004293F
			public static bool operator >(AdvancedSmooth.Turn lhs, AdvancedSmooth.Turn rhs)
			{
				return lhs.score > rhs.score;
			}

			// Token: 0x04000727 RID: 1831
			public float length;

			// Token: 0x04000728 RID: 1832
			public int id;

			// Token: 0x04000729 RID: 1833
			public AdvancedSmooth.TurnConstructor constructor;
		}
	}
}

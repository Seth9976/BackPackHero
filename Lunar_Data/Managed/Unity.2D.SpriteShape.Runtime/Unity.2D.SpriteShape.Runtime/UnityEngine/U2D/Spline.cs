using System;
using System.Collections.Generic;

namespace UnityEngine.U2D
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class Spline
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00006B85 File Offset: 0x00004D85
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00006B98 File Offset: 0x00004D98
		public bool isOpenEnded
		{
			get
			{
				return this.GetPointCount() < 3 || this.m_IsOpenEnded;
			}
			set
			{
				this.m_IsOpenEnded = value;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00006BA4 File Offset: 0x00004DA4
		private bool IsPositionValid(int index, int next, Vector3 point)
		{
			int pointCount = this.GetPointCount();
			if (this.isOpenEnded && (index == 0 || index == pointCount))
			{
				return true;
			}
			int num = ((index == 0) ? (pointCount - 1) : (index - 1));
			if (num >= 0 && (this.m_ControlPoints[num].position - point).magnitude < Spline.KEpsilon)
			{
				return false;
			}
			next = ((next >= pointCount) ? 0 : next);
			return next >= pointCount || (this.m_ControlPoints[next].position - point).magnitude >= Spline.KEpsilon;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00006C38 File Offset: 0x00004E38
		public void Clear()
		{
			this.m_ControlPoints.Clear();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00006C45 File Offset: 0x00004E45
		public int GetPointCount()
		{
			return this.m_ControlPoints.Count;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006C54 File Offset: 0x00004E54
		public void InsertPointAt(int index, Vector3 point)
		{
			if (!this.IsPositionValid(index, index, point))
			{
				throw new ArgumentException(Spline.KErrorMessage);
			}
			this.m_ControlPoints.Insert(index, new SplineControlPoint
			{
				position = point,
				height = 1f,
				cornerMode = Corner.Automatic
			});
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006CA1 File Offset: 0x00004EA1
		public void RemovePointAt(int index)
		{
			if (this.m_ControlPoints.Count > 2)
			{
				this.m_ControlPoints.RemoveAt(index);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006CBD File Offset: 0x00004EBD
		public Vector3 GetPosition(int index)
		{
			return this.m_ControlPoints[index].position;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006CD0 File Offset: 0x00004ED0
		public void SetPosition(int index, Vector3 point)
		{
			if (!this.IsPositionValid(index, index + 1, point))
			{
				throw new ArgumentException(Spline.KErrorMessage);
			}
			SplineControlPoint splineControlPoint = this.m_ControlPoints[index];
			splineControlPoint.position = point;
			this.m_ControlPoints[index] = splineControlPoint;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00006D16 File Offset: 0x00004F16
		public Vector3 GetLeftTangent(int index)
		{
			if (this.GetTangentMode(index) == ShapeTangentMode.Linear)
			{
				return Vector3.zero;
			}
			return this.m_ControlPoints[index].leftTangent;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006D38 File Offset: 0x00004F38
		public void SetLeftTangent(int index, Vector3 tangent)
		{
			if (this.GetTangentMode(index) == ShapeTangentMode.Linear)
			{
				return;
			}
			SplineControlPoint splineControlPoint = this.m_ControlPoints[index];
			splineControlPoint.leftTangent = tangent;
			this.m_ControlPoints[index] = splineControlPoint;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006D70 File Offset: 0x00004F70
		public Vector3 GetRightTangent(int index)
		{
			if (this.GetTangentMode(index) == ShapeTangentMode.Linear)
			{
				return Vector3.zero;
			}
			return this.m_ControlPoints[index].rightTangent;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006D94 File Offset: 0x00004F94
		public void SetRightTangent(int index, Vector3 tangent)
		{
			if (this.GetTangentMode(index) == ShapeTangentMode.Linear)
			{
				return;
			}
			SplineControlPoint splineControlPoint = this.m_ControlPoints[index];
			splineControlPoint.rightTangent = tangent;
			this.m_ControlPoints[index] = splineControlPoint;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006DCC File Offset: 0x00004FCC
		public ShapeTangentMode GetTangentMode(int index)
		{
			return this.m_ControlPoints[index].mode;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006DE0 File Offset: 0x00004FE0
		public void SetTangentMode(int index, ShapeTangentMode mode)
		{
			SplineControlPoint splineControlPoint = this.m_ControlPoints[index];
			splineControlPoint.mode = mode;
			this.m_ControlPoints[index] = splineControlPoint;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006E0E File Offset: 0x0000500E
		public float GetHeight(int index)
		{
			return this.m_ControlPoints[index].height;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006E21 File Offset: 0x00005021
		public void SetHeight(int index, float value)
		{
			this.m_ControlPoints[index].height = value;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006E35 File Offset: 0x00005035
		public int GetSpriteIndex(int index)
		{
			return this.m_ControlPoints[index].spriteIndex;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006E48 File Offset: 0x00005048
		public void SetSpriteIndex(int index, int value)
		{
			this.m_ControlPoints[index].spriteIndex = value;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006E5C File Offset: 0x0000505C
		public bool GetCorner(int index)
		{
			return this.GetCornerMode(index) > Corner.Disable;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006E68 File Offset: 0x00005068
		public void SetCorner(int index, bool value)
		{
			this.m_ControlPoints[index].corner = value;
			this.m_ControlPoints[index].cornerMode = (value ? Corner.Automatic : Corner.Disable);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006E94 File Offset: 0x00005094
		public override int GetHashCode()
		{
			int num = -2128831035;
			for (int i = 0; i < this.GetPointCount(); i++)
			{
				num = (num * 16777619) ^ this.m_ControlPoints[i].GetHashCode();
			}
			return (num * 16777619) ^ this.m_IsOpenEnded.GetHashCode();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006EE7 File Offset: 0x000050E7
		internal void SetCornerMode(int index, Corner value)
		{
			this.m_ControlPoints[index].corner = value > Corner.Disable;
			this.m_ControlPoints[index].cornerMode = value;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006F10 File Offset: 0x00005110
		internal Corner GetCornerMode(int index)
		{
			if (this.m_ControlPoints[index].cornerMode == Corner.Disable && this.m_ControlPoints[index].corner)
			{
				this.m_ControlPoints[index].cornerMode = Corner.Automatic;
				return Corner.Automatic;
			}
			return this.m_ControlPoints[index].cornerMode;
		}

		// Token: 0x04000057 RID: 87
		private static readonly string KErrorMessage = "Internal error: Point too close to neighbor";

		// Token: 0x04000058 RID: 88
		private static readonly float KEpsilon = 0.01f;

		// Token: 0x04000059 RID: 89
		[SerializeField]
		private bool m_IsOpenEnded;

		// Token: 0x0400005A RID: 90
		[SerializeField]
		private List<SplineControlPoint> m_ControlPoints = new List<SplineControlPoint>();
	}
}

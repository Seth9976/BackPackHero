using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020000BC RID: 188
	public class Draw
	{
		// Token: 0x0600082D RID: 2093 RVA: 0x000368EC File Offset: 0x00034AEC
		private void SetColor(Color color)
		{
			if (this.gizmos && global::UnityEngine.Gizmos.color != color)
			{
				global::UnityEngine.Gizmos.color = color;
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0003690C File Offset: 0x00034B0C
		public void Polyline(List<Vector3> points, Color color, bool cycle = false)
		{
			for (int i = 0; i < points.Count - 1; i++)
			{
				this.Line(points[i], points[i + 1], color);
			}
			if (cycle && points.Count > 1)
			{
				this.Line(points[points.Count - 1], points[0], color);
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0003696C File Offset: 0x00034B6C
		public void Line(Vector3 a, Vector3 b, Color color)
		{
			this.SetColor(color);
			if (this.gizmos)
			{
				global::UnityEngine.Gizmos.DrawLine(this.matrix.MultiplyPoint3x4(a), this.matrix.MultiplyPoint3x4(b));
				return;
			}
			global::UnityEngine.Debug.DrawLine(this.matrix.MultiplyPoint3x4(a), this.matrix.MultiplyPoint3x4(b), color);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000369C4 File Offset: 0x00034BC4
		public void CircleXZ(Vector3 center, float radius, Color color, float startAngle = 0f, float endAngle = 6.2831855f)
		{
			int num = 40;
			while (startAngle > endAngle)
			{
				startAngle -= 6.2831855f;
			}
			Vector3 vector = new Vector3(Mathf.Cos(startAngle) * radius, 0f, Mathf.Sin(startAngle) * radius);
			for (int i = 0; i <= num; i++)
			{
				Vector3 vector2 = new Vector3(Mathf.Cos(Mathf.Lerp(startAngle, endAngle, (float)i / (float)num)) * radius, 0f, Mathf.Sin(Mathf.Lerp(startAngle, endAngle, (float)i / (float)num)) * radius);
				this.Line(center + vector, center + vector2, color);
				vector = vector2;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00036A60 File Offset: 0x00034C60
		public void Cylinder(Vector3 position, Vector3 up, float height, float radius, Color color)
		{
			Vector3 normalized = Vector3.Cross(up, Vector3.one).normalized;
			this.matrix = Matrix4x4.TRS(position, Quaternion.LookRotation(normalized, up), new Vector3(radius, height, radius));
			this.CircleXZ(Vector3.zero, 1f, color, 0f, 6.2831855f);
			if (height > 0f)
			{
				this.CircleXZ(Vector3.up, 1f, color, 0f, 6.2831855f);
				this.Line(new Vector3(1f, 0f, 0f), new Vector3(1f, 1f, 0f), color);
				this.Line(new Vector3(-1f, 0f, 0f), new Vector3(-1f, 1f, 0f), color);
				this.Line(new Vector3(0f, 0f, 1f), new Vector3(0f, 1f, 1f), color);
				this.Line(new Vector3(0f, 0f, -1f), new Vector3(0f, 1f, -1f), color);
			}
			this.matrix = Matrix4x4.identity;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00036BAC File Offset: 0x00034DAC
		public void CrossXZ(Vector3 position, Color color, float size = 1f)
		{
			size *= 0.5f;
			this.Line(position - Vector3.right * size, position + Vector3.right * size, color);
			this.Line(position - Vector3.forward * size, position + Vector3.forward * size, color);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00036C14 File Offset: 0x00034E14
		public void Bezier(Vector3 a, Vector3 b, Color color)
		{
			Vector3 vector = b - a;
			if (vector == Vector3.zero)
			{
				return;
			}
			Vector3 vector2 = Vector3.Cross(Vector3.up, vector);
			Vector3 vector3 = Vector3.Cross(vector, vector2).normalized;
			vector3 *= vector.magnitude * 0.1f;
			Vector3 vector4 = a + vector3;
			Vector3 vector5 = b + vector3;
			Vector3 vector6 = a;
			for (int i = 1; i <= 20; i++)
			{
				float num = (float)i / 20f;
				Vector3 vector7 = AstarSplines.CubicBezier(a, vector4, vector5, b, num);
				this.Line(vector6, vector7, color);
				vector6 = vector7;
			}
		}

		// Token: 0x040004CB RID: 1227
		public static readonly Draw Debug = new Draw
		{
			gizmos = false
		};

		// Token: 0x040004CC RID: 1228
		public static readonly Draw Gizmos = new Draw
		{
			gizmos = true
		};

		// Token: 0x040004CD RID: 1229
		private bool gizmos;

		// Token: 0x040004CE RID: 1230
		private Matrix4x4 matrix = Matrix4x4.identity;
	}
}

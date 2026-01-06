using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Drawing
{
	// Token: 0x02000024 RID: 36
	public struct CommandBuilder2D
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000646B File Offset: 0x0000466B
		public CommandBuilder2D(CommandBuilder draw, bool xy)
		{
			this.draw = draw;
			this.xy = xy;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000647C File Offset: 0x0000467C
		public unsafe void Line(float2 a, float2 b)
		{
			this.draw.Reserve<CommandBuilder.LineData>();
			UnsafeAppendBuffer* buffer = this.draw.buffer;
			int length = buffer->Length;
			int num = length + 4 + 24;
			byte* ptr = buffer->Ptr + length;
			*(int*)ptr = 5;
			CommandBuilder.LineData* ptr2 = (CommandBuilder.LineData*)(ptr + 4);
			if (this.xy)
			{
				ptr2->a = new float3(a, 0f);
				ptr2->b = new float3(b, 0f);
			}
			else
			{
				ptr2->a = new float3(a.x, 0f, a.y);
				ptr2->b = new float3(b.x, 0f, b.y);
			}
			buffer->Length = num;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006528 File Offset: 0x00004728
		public unsafe void Line(float2 a, float2 b, Color color)
		{
			this.draw.Reserve<Color32, CommandBuilder.LineData>();
			UnsafeAppendBuffer* buffer = this.draw.buffer;
			int length = buffer->Length;
			int num = length + 4 + 24 + 4;
			byte* ptr = buffer->Ptr + length;
			*(int*)ptr = 261;
			*(int*)(ptr + 4) = (int)CommandBuilder.ConvertColor(color);
			CommandBuilder.LineData* ptr2 = (CommandBuilder.LineData*)(ptr + 8);
			if (this.xy)
			{
				ptr2->a = new float3(a, 0f);
				ptr2->b = new float3(b, 0f);
			}
			else
			{
				ptr2->a = new float3(a.x, 0f, a.y);
				ptr2->b = new float3(b.x, 0f, b.y);
			}
			buffer->Length = num;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000065E1 File Offset: 0x000047E1
		public void Line(float3 a, float3 b)
		{
			this.draw.Line(a, b);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000065F0 File Offset: 0x000047F0
		public void Circle(float2 center, float radius, float startAngle = 0f, float endAngle = 6.2831855f)
		{
			this.Circle(this.xy ? new float3(center, 0f) : new float3(center.x, 0f, center.y), radius, startAngle, endAngle);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006628 File Offset: 0x00004828
		public void Circle(float3 center, float radius, float startAngle = 0f, float endAngle = 6.2831855f)
		{
			if (this.xy)
			{
				this.draw.PushMatrix(CommandBuilder2D.XZ_TO_XY_MATRIX);
				this.draw.CircleXZInternal(new float3(center.x, center.z, center.y), radius, startAngle, endAngle);
				this.draw.PopMatrix();
				return;
			}
			this.draw.CircleXZInternal(center, radius, startAngle, endAngle);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000668F File Offset: 0x0000488F
		public void SolidCircle(float2 center, float radius, float startAngle = 0f, float endAngle = 6.2831855f)
		{
			this.SolidCircle(this.xy ? new float3(center, 0f) : new float3(center.x, 0f, center.y), radius, startAngle, endAngle);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000066C8 File Offset: 0x000048C8
		public void SolidCircle(float3 center, float radius, float startAngle = 0f, float endAngle = 6.2831855f)
		{
			if (this.xy)
			{
				this.draw.PushMatrix(CommandBuilder2D.XZ_TO_XY_MATRIX);
			}
			this.draw.SolidCircleXZInternal(new float3(center.x, -center.z, center.y), radius, startAngle, endAngle);
			if (this.xy)
			{
				this.draw.PopMatrix();
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006727 File Offset: 0x00004927
		public void WirePill(float2 a, float2 b, float radius)
		{
			this.WirePill(a, b - a, math.length(b - a), radius);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006744 File Offset: 0x00004944
		public void WirePill(float2 position, float2 direction, float length, float radius)
		{
			direction = math.normalizesafe(direction, default(float2));
			if (radius <= 0f)
			{
				this.Line(position, position + direction * length);
				return;
			}
			if (length <= 0f || math.all(direction == 0f))
			{
				this.Circle(position, radius, 0f, 6.2831855f);
				return;
			}
			float4x4 float4x;
			if (this.xy)
			{
				float4x = new float4x4(new float4(direction, 0f, 0f), new float4(math.cross(new float3(direction, 0f), CommandBuilder2D.XY_UP), 0f), new float4(0f, 0f, 1f, 0f), new float4(position, 0f, 1f));
			}
			else
			{
				float4x = new float4x4(new float4(direction.x, 0f, direction.y, 0f), new float4(0f, 1f, 0f, 0f), new float4(math.cross(new float3(direction.x, 0f, direction.y), CommandBuilder2D.XZ_UP), 0f), new float4(position.x, 0f, position.y, 1f));
			}
			this.draw.PushMatrix(float4x);
			this.Circle(new float2(0f, 0f), radius, 1.5707964f, 4.712389f);
			this.Line(new float2(0f, -radius), new float2(length, -radius));
			this.Circle(new float2(length, 0f), radius, -1.5707964f, 1.5707964f);
			this.Line(new float2(0f, radius), new float2(length, radius));
			this.draw.PopMatrix();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006928 File Offset: 0x00004B28
		[BurstDiscard]
		public void Polyline(List<Vector2> points, bool cycle = false)
		{
			for (int i = 0; i < points.Count - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Count > 1)
			{
				this.Line(points[points.Count - 1], points[0]);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006988 File Offset: 0x00004B88
		[BurstDiscard]
		public void Polyline(Vector2[] points, bool cycle = false)
		{
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000069DC File Offset: 0x00004BDC
		[BurstDiscard]
		public void Polyline(float2[] points, bool cycle = false)
		{
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006A30 File Offset: 0x00004C30
		public void Polyline(NativeArray<float2> points, bool cycle = false)
		{
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006A94 File Offset: 0x00004C94
		public void Cross(float2 position, float size = 1f)
		{
			size *= 0.5f;
			this.Line(position - new float2(size, 0f), position + new float2(size, 0f));
			this.Line(position - new float2(0f, size), position + new float2(0f, size));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006AFA File Offset: 0x00004CFA
		public void WireRectangle(float3 center, float2 size)
		{
			this.draw.WirePlane(center, this.xy ? CommandBuilder2D.XY_TO_XZ_ROTATION : CommandBuilder2D.XZ_TO_XZ_ROTATION, size);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006B20 File Offset: 0x00004D20
		public void WireRectangle(Rect rect)
		{
			float2 @float = rect.min;
			float2 float2 = rect.max;
			this.Line(new float2(@float.x, @float.y), new float2(float2.x, @float.y));
			this.Line(new float2(float2.x, @float.y), new float2(float2.x, float2.y));
			this.Line(new float2(float2.x, float2.y), new float2(@float.x, float2.y));
			this.Line(new float2(@float.x, float2.y), new float2(@float.x, @float.y));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public void SolidRectangle(Rect rect)
		{
			this.draw.SolidPlane(new float3(rect.center.x, rect.center.y, 0f), this.xy ? CommandBuilder2D.XY_TO_XZ_ROTATION : CommandBuilder2D.XZ_TO_XZ_ROTATION, new float2(rect.width, rect.height));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006C4C File Offset: 0x00004E4C
		public void WireGrid(float2 center, int2 cells, float2 totalSize)
		{
			this.draw.WireGrid(this.xy ? new float3(center, 0f) : new float3(center.x, 0f, center.y), this.xy ? CommandBuilder2D.XY_TO_XZ_ROTATION : CommandBuilder2D.XZ_TO_XZ_ROTATION, cells, totalSize);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006CA5 File Offset: 0x00004EA5
		public void WireGrid(float3 center, int2 cells, float2 totalSize)
		{
			this.draw.WireGrid(center, this.xy ? CommandBuilder2D.XY_TO_XZ_ROTATION : CommandBuilder2D.XZ_TO_XZ_ROTATION, cells, totalSize);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006CC9 File Offset: 0x00004EC9
		[BurstDiscard]
		public CommandBuilder.ScopeMatrix WithMatrix(Matrix4x4 matrix)
		{
			return this.draw.WithMatrix(matrix);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006CD7 File Offset: 0x00004ED7
		[BurstDiscard]
		public CommandBuilder.ScopeMatrix WithMatrix(float3x3 matrix)
		{
			return this.draw.WithMatrix(matrix);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006CE5 File Offset: 0x00004EE5
		[BurstDiscard]
		public CommandBuilder.ScopeColor WithColor(Color color)
		{
			return this.draw.WithColor(color);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006CF3 File Offset: 0x00004EF3
		[BurstDiscard]
		public CommandBuilder.ScopeLineWidth WithLineWidth(float pixels, bool automaticJoins = true)
		{
			return this.draw.WithLineWidth(pixels, automaticJoins);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006D02 File Offset: 0x00004F02
		public void PushMatrix(Matrix4x4 matrix)
		{
			this.draw.PushMatrix(matrix);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006D10 File Offset: 0x00004F10
		public void PushMatrix(float4x4 matrix)
		{
			this.draw.PushMatrix(matrix);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006D1E File Offset: 0x00004F1E
		public void PopMatrix()
		{
			this.draw.PopMatrix();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006D2B File Offset: 0x00004F2B
		public void Line(Vector3 a, Vector3 b)
		{
			this.draw.Line(a, b);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006D3C File Offset: 0x00004F3C
		public void Line(Vector2 a, Vector2 b)
		{
			this.Line(this.xy ? new Vector3(a.x, a.y, 0f) : new Vector3(a.x, 0f, a.y), this.xy ? new Vector3(b.x, b.y, 0f) : new Vector3(b.x, 0f, b.y));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006DBB File Offset: 0x00004FBB
		public void Line(Vector3 a, Vector3 b, Color color)
		{
			this.draw.Line(a, b, color);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006DCC File Offset: 0x00004FCC
		public void Line(Vector2 a, Vector2 b, Color color)
		{
			this.Line(this.xy ? new Vector3(a.x, a.y, 0f) : new Vector3(a.x, 0f, a.y), this.xy ? new Vector3(b.x, b.y, 0f) : new Vector3(b.x, 0f, b.y), color);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006E4C File Offset: 0x0000504C
		public void Ray(float3 origin, float3 direction)
		{
			this.draw.Ray(origin, direction);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006E5C File Offset: 0x0000505C
		public void Ray(float2 origin, float2 direction)
		{
			this.Ray(this.xy ? new float3(origin, 0f) : new float3(origin.x, 0f, origin.y), this.xy ? new float3(direction, 0f) : new float3(direction.x, 0f, direction.y));
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006EC5 File Offset: 0x000050C5
		public void Ray(Ray ray, float length)
		{
			this.draw.Ray(ray, length);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006ED4 File Offset: 0x000050D4
		public void Arc(float3 center, float3 start, float3 end)
		{
			this.draw.Arc(center, start, end);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006EE4 File Offset: 0x000050E4
		public void Arc(float2 center, float2 start, float2 end)
		{
			this.Arc(this.xy ? new float3(center, 0f) : new float3(center.x, 0f, center.y), this.xy ? new float3(start, 0f) : new float3(start.x, 0f, start.y), this.xy ? new float3(end, 0f) : new float3(end.x, 0f, end.y));
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006F78 File Offset: 0x00005178
		[BurstDiscard]
		public void Polyline(List<Vector3> points, bool cycle = false)
		{
			this.draw.Polyline(points, cycle);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006F87 File Offset: 0x00005187
		[BurstDiscard]
		public void Polyline(Vector3[] points, bool cycle = false)
		{
			this.draw.Polyline(points, cycle);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006F96 File Offset: 0x00005196
		[BurstDiscard]
		public void Polyline(float3[] points, bool cycle = false)
		{
			this.draw.Polyline(points, cycle);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006FA5 File Offset: 0x000051A5
		public void Polyline(NativeArray<float3> points, bool cycle = false)
		{
			this.draw.Polyline(points, cycle);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006FB4 File Offset: 0x000051B4
		public void Cross(float3 position, float size = 1f)
		{
			this.draw.Cross(position, size);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006FC3 File Offset: 0x000051C3
		public void Bezier(float3 p0, float3 p1, float3 p2, float3 p3)
		{
			this.draw.Bezier(p0, p1, p2, p3);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006FD8 File Offset: 0x000051D8
		public void Bezier(float2 p0, float2 p1, float2 p2, float2 p3)
		{
			this.Bezier(this.xy ? new float3(p0, 0f) : new float3(p0.x, 0f, p0.y), this.xy ? new float3(p1, 0f) : new float3(p1.x, 0f, p1.y), this.xy ? new float3(p2, 0f) : new float3(p2.x, 0f, p2.y), this.xy ? new float3(p3, 0f) : new float3(p3.x, 0f, p3.y));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000709A File Offset: 0x0000529A
		public void Arrow(float3 from, float3 to)
		{
			this.ArrowRelativeSizeHead(from, to, this.xy ? CommandBuilder2D.XY_UP : CommandBuilder2D.XZ_UP, 0.2f);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000070C0 File Offset: 0x000052C0
		public void Arrow(float2 from, float2 to)
		{
			this.Arrow(this.xy ? new float3(from, 0f) : new float3(from.x, 0f, from.y), this.xy ? new float3(to, 0f) : new float3(to.x, 0f, to.y));
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007129 File Offset: 0x00005329
		public void Arrow(float3 from, float3 to, float3 up, float headSize)
		{
			this.draw.Arrow(from, to, up, headSize);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000713C File Offset: 0x0000533C
		public void Arrow(float2 from, float2 to, float2 up, float headSize)
		{
			this.Arrow(this.xy ? new float3(from, 0f) : new float3(from.x, 0f, from.y), this.xy ? new float3(to, 0f) : new float3(to.x, 0f, to.y), this.xy ? new float3(up, 0f) : new float3(up.x, 0f, up.y), headSize);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000071D2 File Offset: 0x000053D2
		public void ArrowRelativeSizeHead(float3 from, float3 to, float3 up, float headFraction)
		{
			this.draw.ArrowRelativeSizeHead(from, to, up, headFraction);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000071E4 File Offset: 0x000053E4
		public void ArrowRelativeSizeHead(float2 from, float2 to, float2 up, float headFraction)
		{
			this.ArrowRelativeSizeHead(this.xy ? new float3(from, 0f) : new float3(from.x, 0f, from.y), this.xy ? new float3(to, 0f) : new float3(to.x, 0f, to.y), this.xy ? new float3(up, 0f) : new float3(up.x, 0f, up.y), headFraction);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000727C File Offset: 0x0000547C
		public void ArrowheadArc(float3 origin, float3 direction, float offset, float width = 60f)
		{
			if (!math.any(direction))
			{
				return;
			}
			if (offset < 0f)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset == 0f)
			{
				return;
			}
			Quaternion quaternion = Quaternion.LookRotation(direction, this.xy ? CommandBuilder2D.XY_UP : CommandBuilder2D.XZ_UP);
			this.PushMatrix(Matrix4x4.TRS(origin, quaternion, Vector3.one));
			float num = 1.5707964f - width * 0.008726646f;
			float num2 = 1.5707964f + width * 0.008726646f;
			this.draw.CircleXZInternal(float3.zero, offset, num, num2);
			float3 @float = new float3(math.cos(num), 0f, math.sin(num)) * offset;
			float3 float2 = new float3(math.cos(num2), 0f, math.sin(num2)) * offset;
			float3 float3 = new float3(0f, 0f, 1.4142f * offset);
			this.Line(@float, float3);
			this.Line(float3, float2);
			this.PopMatrix();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007388 File Offset: 0x00005588
		public void ArrowheadArc(float2 origin, float2 direction, float offset, float width = 60f)
		{
			this.ArrowheadArc(this.xy ? new float3(origin, 0f) : new float3(origin.x, 0f, origin.y), this.xy ? new float3(direction, 0f) : new float3(direction.x, 0f, direction.y), offset, width);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000073F4 File Offset: 0x000055F4
		public void WireRectangle(float3 center, quaternion rotation, float2 size)
		{
			this.draw.WireRectangle(center, rotation, size);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00007404 File Offset: 0x00005604
		public void WireRectangle(float2 center, quaternion rotation, float2 size)
		{
			this.WireRectangle(this.xy ? new float3(center, 0f) : new float3(center.x, 0f, center.y), rotation, size);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00007439 File Offset: 0x00005639
		public void Ray(float3 origin, float3 direction, Color color)
		{
			this.draw.Ray(origin, direction, color);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000744C File Offset: 0x0000564C
		public void Ray(float2 origin, float2 direction, Color color)
		{
			this.Ray(this.xy ? new float3(origin, 0f) : new float3(origin.x, 0f, origin.y), this.xy ? new float3(direction, 0f) : new float3(direction.x, 0f, direction.y), color);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000074B6 File Offset: 0x000056B6
		public void Ray(Ray ray, float length, Color color)
		{
			this.draw.Ray(ray, length, color);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000074C6 File Offset: 0x000056C6
		public void Arc(float3 center, float3 start, float3 end, Color color)
		{
			this.draw.Arc(center, start, end, color);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000074D8 File Offset: 0x000056D8
		public void Arc(float2 center, float2 start, float2 end, Color color)
		{
			this.Arc(this.xy ? new float3(center, 0f) : new float3(center.x, 0f, center.y), this.xy ? new float3(start, 0f) : new float3(start.x, 0f, start.y), this.xy ? new float3(end, 0f) : new float3(end.x, 0f, end.y), color);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000756E File Offset: 0x0000576E
		[BurstDiscard]
		public void Polyline(List<Vector3> points, bool cycle, Color color)
		{
			this.draw.Polyline(points, cycle, color);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000757E File Offset: 0x0000577E
		[BurstDiscard]
		public void Polyline(List<Vector3> points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007589 File Offset: 0x00005789
		[BurstDiscard]
		public void Polyline(Vector3[] points, bool cycle, Color color)
		{
			this.draw.Polyline(points, cycle, color);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007599 File Offset: 0x00005799
		[BurstDiscard]
		public void Polyline(Vector3[] points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000075A4 File Offset: 0x000057A4
		[BurstDiscard]
		public void Polyline(float3[] points, bool cycle, Color color)
		{
			this.draw.Polyline(points, cycle, color);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000075B4 File Offset: 0x000057B4
		[BurstDiscard]
		public void Polyline(float3[] points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000075BF File Offset: 0x000057BF
		public void Polyline(NativeArray<float3> points, bool cycle, Color color)
		{
			this.draw.Polyline(points, cycle, color);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000075CF File Offset: 0x000057CF
		public void Polyline(NativeArray<float3> points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000075DA File Offset: 0x000057DA
		public void Cross(float3 position, float size, Color color)
		{
			this.draw.Cross(position, size, color);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000075EA File Offset: 0x000057EA
		public void Cross(float3 position, Color color)
		{
			this.Cross(position, 1f, color);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000075F9 File Offset: 0x000057F9
		public void Bezier(float3 p0, float3 p1, float3 p2, float3 p3, Color color)
		{
			this.draw.Bezier(p0, p1, p2, p3, color);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007610 File Offset: 0x00005810
		public void Bezier(float2 p0, float2 p1, float2 p2, float2 p3, Color color)
		{
			this.Bezier(this.xy ? new float3(p0, 0f) : new float3(p0.x, 0f, p0.y), this.xy ? new float3(p1, 0f) : new float3(p1.x, 0f, p1.y), this.xy ? new float3(p2, 0f) : new float3(p2.x, 0f, p2.y), this.xy ? new float3(p3, 0f) : new float3(p3.x, 0f, p3.y), color);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x000076D4 File Offset: 0x000058D4
		public void Arrow(float3 from, float3 to, Color color)
		{
			this.ArrowRelativeSizeHead(from, to, this.xy ? CommandBuilder2D.XY_UP : CommandBuilder2D.XZ_UP, 0.2f, color);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000076F8 File Offset: 0x000058F8
		public void Arrow(float2 from, float2 to, Color color)
		{
			this.Arrow(this.xy ? new float3(from, 0f) : new float3(from.x, 0f, from.y), this.xy ? new float3(to, 0f) : new float3(to.x, 0f, to.y), color);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007762 File Offset: 0x00005962
		public void Arrow(float3 from, float3 to, float3 up, float headSize, Color color)
		{
			this.draw.Arrow(from, to, up, headSize, color);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007778 File Offset: 0x00005978
		public void Arrow(float2 from, float2 to, float2 up, float headSize, Color color)
		{
			this.Arrow(this.xy ? new float3(from, 0f) : new float3(from.x, 0f, from.y), this.xy ? new float3(to, 0f) : new float3(to.x, 0f, to.y), this.xy ? new float3(up, 0f) : new float3(up.x, 0f, up.y), headSize, color);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007810 File Offset: 0x00005A10
		public void ArrowRelativeSizeHead(float3 from, float3 to, float3 up, float headFraction, Color color)
		{
			this.draw.ArrowRelativeSizeHead(from, to, up, headFraction, color);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007824 File Offset: 0x00005A24
		public void ArrowRelativeSizeHead(float2 from, float2 to, float2 up, float headFraction, Color color)
		{
			this.ArrowRelativeSizeHead(this.xy ? new float3(from, 0f) : new float3(from.x, 0f, from.y), this.xy ? new float3(to, 0f) : new float3(to.x, 0f, to.y), this.xy ? new float3(up, 0f) : new float3(up.x, 0f, up.y), headFraction, color);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000078BC File Offset: 0x00005ABC
		public void ArrowheadArc(float3 origin, float3 direction, float offset, float width, Color color)
		{
			if (!math.any(direction))
			{
				return;
			}
			if (offset < 0f)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset == 0f)
			{
				return;
			}
			this.draw.PushColor(color);
			Quaternion quaternion = Quaternion.LookRotation(direction, this.xy ? CommandBuilder2D.XY_UP : CommandBuilder2D.XZ_UP);
			this.PushMatrix(Matrix4x4.TRS(origin, quaternion, Vector3.one));
			float num = 1.5707964f - width * 0.008726646f;
			float num2 = 1.5707964f + width * 0.008726646f;
			this.draw.CircleXZInternal(float3.zero, offset, num, num2);
			float3 @float = new float3(math.cos(num), 0f, math.sin(num)) * offset;
			float3 float2 = new float3(math.cos(num2), 0f, math.sin(num2)) * offset;
			float3 float3 = new float3(0f, 0f, 1.4142f * offset);
			this.Line(@float, float3);
			this.Line(float3, float2);
			this.PopMatrix();
			this.draw.PopColor();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000079DF File Offset: 0x00005BDF
		public void ArrowheadArc(float3 origin, float3 direction, float offset, Color color)
		{
			this.ArrowheadArc(origin, direction, offset, 60f, color);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000079F4 File Offset: 0x00005BF4
		public void ArrowheadArc(float2 origin, float2 direction, float offset, float width, Color color)
		{
			this.ArrowheadArc(this.xy ? new float3(origin, 0f) : new float3(origin.x, 0f, origin.y), this.xy ? new float3(direction, 0f) : new float3(direction.x, 0f, direction.y), offset, width, color);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007A62 File Offset: 0x00005C62
		public void ArrowheadArc(float2 origin, float2 direction, float offset, Color color)
		{
			this.ArrowheadArc(origin, direction, offset, 60f, color);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007A74 File Offset: 0x00005C74
		public void WireRectangle(float3 center, quaternion rotation, float2 size, Color color)
		{
			this.draw.WireRectangle(center, rotation, size, color);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007A86 File Offset: 0x00005C86
		public void WireRectangle(float2 center, quaternion rotation, float2 size, Color color)
		{
			this.WireRectangle(this.xy ? new float3(center, 0f) : new float3(center.x, 0f, center.y), rotation, size, color);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007ABD File Offset: 0x00005CBD
		public void Line(float3 a, float3 b, Color color)
		{
			this.draw.Line(a, b, color);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007ACD File Offset: 0x00005CCD
		public void Circle(float2 center, float radius, float startAngle, float endAngle, Color color)
		{
			this.Circle(this.xy ? new float3(center, 0f) : new float3(center.x, 0f, center.y), radius, startAngle, endAngle, color);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007B06 File Offset: 0x00005D06
		public void Circle(float2 center, float radius, Color color)
		{
			this.Circle(center, radius, 0f, 6.2831855f, color);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007B1C File Offset: 0x00005D1C
		public void Circle(float3 center, float radius, float startAngle, float endAngle, Color color)
		{
			this.draw.PushColor(color);
			if (this.xy)
			{
				this.draw.PushMatrix(CommandBuilder2D.XZ_TO_XY_MATRIX);
				this.draw.CircleXZInternal(new float3(center.x, center.z, center.y), radius, startAngle, endAngle);
				this.draw.PopMatrix();
			}
			else
			{
				this.draw.CircleXZInternal(center, radius, startAngle, endAngle);
			}
			this.draw.PopColor();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007B9C File Offset: 0x00005D9C
		public void Circle(float3 center, float radius, Color color)
		{
			this.Circle(center, radius, 0f, 6.2831855f, color);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007BB1 File Offset: 0x00005DB1
		public void WirePill(float2 a, float2 b, float radius, Color color)
		{
			this.WirePill(a, b - a, math.length(b - a), radius, color);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007BD0 File Offset: 0x00005DD0
		public void WirePill(float2 position, float2 direction, float length, float radius, Color color)
		{
			this.draw.PushColor(color);
			direction = math.normalizesafe(direction, default(float2));
			if (radius <= 0f)
			{
				this.Line(position, position + direction * length);
			}
			else if (length <= 0f || math.all(direction == 0f))
			{
				this.Circle(position, radius, 0f, 6.2831855f);
			}
			else
			{
				float4x4 float4x;
				if (this.xy)
				{
					float4x = new float4x4(new float4(direction, 0f, 0f), new float4(math.cross(new float3(direction, 0f), CommandBuilder2D.XY_UP), 0f), new float4(0f, 0f, 1f, 0f), new float4(position, 0f, 1f));
				}
				else
				{
					float4x = new float4x4(new float4(direction.x, 0f, direction.y, 0f), new float4(0f, 1f, 0f, 0f), new float4(math.cross(new float3(direction.x, 0f, direction.y), CommandBuilder2D.XZ_UP), 0f), new float4(position.x, 0f, position.y, 1f));
				}
				this.draw.PushMatrix(float4x);
				this.Circle(new float2(0f, 0f), radius, 1.5707964f, 4.712389f);
				this.Line(new float2(0f, -radius), new float2(length, -radius));
				this.Circle(new float2(length, 0f), radius, -1.5707964f, 1.5707964f);
				this.Line(new float2(0f, radius), new float2(length, radius));
				this.draw.PopMatrix();
			}
			this.draw.PopColor();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007DD4 File Offset: 0x00005FD4
		[BurstDiscard]
		public void Polyline(List<Vector2> points, bool cycle, Color color)
		{
			this.draw.PushColor(color);
			for (int i = 0; i < points.Count - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Count > 1)
			{
				this.Line(points[points.Count - 1], points[0]);
			}
			this.draw.PopColor();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007E48 File Offset: 0x00006048
		[BurstDiscard]
		public void Polyline(List<Vector2> points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007E54 File Offset: 0x00006054
		[BurstDiscard]
		public void Polyline(Vector2[] points, bool cycle, Color color)
		{
			this.draw.PushColor(color);
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
			this.draw.PopColor();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007EBF File Offset: 0x000060BF
		[BurstDiscard]
		public void Polyline(Vector2[] points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007ECC File Offset: 0x000060CC
		[BurstDiscard]
		public void Polyline(float2[] points, bool cycle, Color color)
		{
			this.draw.PushColor(color);
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
			this.draw.PopColor();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00007F37 File Offset: 0x00006137
		[BurstDiscard]
		public void Polyline(float2[] points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007F44 File Offset: 0x00006144
		public void Polyline(NativeArray<float2> points, bool cycle, Color color)
		{
			this.draw.PushColor(color);
			for (int i = 0; i < points.Length - 1; i++)
			{
				this.Line(points[i], points[i + 1]);
			}
			if (cycle && points.Length > 1)
			{
				this.Line(points[points.Length - 1], points[0]);
			}
			this.draw.PopColor();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007FBF File Offset: 0x000061BF
		public void Polyline(NativeArray<float2> points, Color color)
		{
			this.Polyline(points, false, color);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007FCC File Offset: 0x000061CC
		public void Cross(float2 position, float size, Color color)
		{
			this.draw.PushColor(color);
			size *= 0.5f;
			this.Line(position - new float2(size, 0f), position + new float2(size, 0f));
			this.Line(position - new float2(0f, size), position + new float2(0f, size));
			this.draw.PopColor();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008049 File Offset: 0x00006249
		public void Cross(float2 position, Color color)
		{
			this.Cross(position, 1f, color);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008058 File Offset: 0x00006258
		public void WireRectangle(float3 center, float2 size, Color color)
		{
			this.draw.WirePlane(center, this.xy ? CommandBuilder2D.XY_TO_XZ_ROTATION : CommandBuilder2D.XZ_TO_XZ_ROTATION, size, color);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000807C File Offset: 0x0000627C
		public void WireRectangle(Rect rect, Color color)
		{
			this.draw.PushColor(color);
			float2 @float = rect.min;
			float2 float2 = rect.max;
			this.Line(new float2(@float.x, @float.y), new float2(float2.x, @float.y));
			this.Line(new float2(float2.x, @float.y), new float2(float2.x, float2.y));
			this.Line(new float2(float2.x, float2.y), new float2(@float.x, float2.y));
			this.Line(new float2(@float.x, float2.y), new float2(@float.x, @float.y));
			this.draw.PopColor();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000815C File Offset: 0x0000635C
		public void WireGrid(float2 center, int2 cells, float2 totalSize, Color color)
		{
			this.draw.WireGrid(this.xy ? new float3(center, 0f) : new float3(center.x, 0f, center.y), this.xy ? CommandBuilder2D.XY_TO_XZ_ROTATION : CommandBuilder2D.XZ_TO_XZ_ROTATION, cells, totalSize, color);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000081B7 File Offset: 0x000063B7
		public void WireGrid(float3 center, int2 cells, float2 totalSize, Color color)
		{
			this.draw.WireGrid(center, this.xy ? CommandBuilder2D.XY_TO_XZ_ROTATION : CommandBuilder2D.XZ_TO_XZ_ROTATION, cells, totalSize, color);
		}

		// Token: 0x0400006E RID: 110
		private CommandBuilder draw;

		// Token: 0x0400006F RID: 111
		private bool xy;

		// Token: 0x04000070 RID: 112
		private static readonly float3 XY_UP = new float3(0f, 0f, 1f);

		// Token: 0x04000071 RID: 113
		private static readonly float3 XZ_UP = new float3(0f, 1f, 0f);

		// Token: 0x04000072 RID: 114
		private static readonly quaternion XY_TO_XZ_ROTATION = quaternion.RotateX(-1.5707964f);

		// Token: 0x04000073 RID: 115
		private static readonly quaternion XZ_TO_XZ_ROTATION = quaternion.identity;

		// Token: 0x04000074 RID: 116
		private static readonly float4x4 XZ_TO_XY_MATRIX = new float4x4(new float4(1f, 0f, 0f, 0f), new float4(0f, 0f, 1f, 0f), new float4(0f, 1f, 0f, 0f), new float4(0f, 0f, 0f, 1f));
	}
}

using System;
using Unity.Collections;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200031B RID: 795
	internal static class Tessellation
	{
		// Token: 0x060019BB RID: 6587 RVA: 0x0006AB34 File Offset: 0x00068D34
		public static void TessellateRect(MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshBuilder.AllocMeshData meshAlloc, bool computeUVs)
		{
			bool flag = rectParams.rect.width < Tessellation.kEpsilon || rectParams.rect.height < Tessellation.kEpsilon;
			if (!flag)
			{
				Tessellation.s_MarkerTessellateRect.Begin();
				Vector2 vector = new Vector2(rectParams.rect.width * 0.5f, rectParams.rect.height * 0.5f);
				rectParams.topLeftRadius = Vector2.Min(rectParams.topLeftRadius, vector);
				rectParams.topRightRadius = Vector2.Min(rectParams.topRightRadius, vector);
				rectParams.bottomRightRadius = Vector2.Min(rectParams.bottomRightRadius, vector);
				rectParams.bottomLeftRadius = Vector2.Min(rectParams.bottomLeftRadius, vector);
				ushort num = 0;
				ushort num2 = 0;
				Tessellation.TessellateRoundedCorners(ref rectParams, 0f, null, rectParams.colorPage, ref num, ref num2, true);
				MeshWriteData meshWriteData = meshAlloc.Allocate((uint)num, (uint)num2);
				num = 0;
				num2 = 0;
				Tessellation.TessellateRoundedCorners(ref rectParams, posZ, meshWriteData, rectParams.colorPage, ref num, ref num2, false);
				if (computeUVs)
				{
					Tessellation.ComputeUVs(rectParams.rect, rectParams.uv, meshWriteData.uvRegion, meshWriteData.m_Vertices);
				}
				Debug.Assert((int)num == meshWriteData.vertexCount);
				Debug.Assert((int)num2 == meshWriteData.indexCount);
				Tessellation.s_MarkerTessellateRect.End();
			}
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0006AC88 File Offset: 0x00068E88
		public static void TessellateQuad(MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshBuilder.AllocMeshData meshAlloc)
		{
			bool flag = rectParams.rect.width < Tessellation.kEpsilon || rectParams.rect.height < Tessellation.kEpsilon;
			if (!flag)
			{
				Tessellation.s_MarkerTessellateRect.Begin();
				ushort num = 0;
				ushort num2 = 0;
				Tessellation.TessellateQuad(rectParams.rect, Tessellation.Edges.All, rectParams.color, posZ, null, rectParams.colorPage, ref num, ref num2, true);
				MeshWriteData meshWriteData = meshAlloc.Allocate((uint)num, (uint)num2);
				num = 0;
				num2 = 0;
				Tessellation.TessellateQuad(rectParams.rect, Tessellation.Edges.All, rectParams.color, posZ, meshWriteData, rectParams.colorPage, ref num, ref num2, false);
				Debug.Assert((int)num == meshWriteData.vertexCount);
				Debug.Assert((int)num2 == meshWriteData.indexCount);
				Tessellation.s_MarkerTessellateRect.End();
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0006AD5C File Offset: 0x00068F5C
		public static void TessellateBorder(MeshGenerationContextUtils.BorderParams borderParams, float posZ, MeshBuilder.AllocMeshData meshAlloc)
		{
			bool flag = borderParams.rect.width < Tessellation.kEpsilon || borderParams.rect.height < Tessellation.kEpsilon;
			if (!flag)
			{
				Tessellation.s_MarkerTessellateBorder.Begin();
				Vector2 vector = new Vector2(borderParams.rect.width * 0.5f, borderParams.rect.height * 0.5f);
				borderParams.topLeftRadius = Vector2.Min(borderParams.topLeftRadius, vector);
				borderParams.topRightRadius = Vector2.Min(borderParams.topRightRadius, vector);
				borderParams.bottomRightRadius = Vector2.Min(borderParams.bottomRightRadius, vector);
				borderParams.bottomLeftRadius = Vector2.Min(borderParams.bottomLeftRadius, vector);
				borderParams.leftWidth = Mathf.Min(borderParams.leftWidth, vector.x);
				borderParams.topWidth = Mathf.Min(borderParams.topWidth, vector.y);
				borderParams.rightWidth = Mathf.Min(borderParams.rightWidth, vector.x);
				borderParams.bottomWidth = Mathf.Min(borderParams.bottomWidth, vector.y);
				ushort num = 0;
				ushort num2 = 0;
				Tessellation.TessellateRoundedBorders(ref borderParams, 0f, null, ref num, ref num2, true);
				MeshWriteData meshWriteData = meshAlloc.Allocate((uint)num, (uint)num2);
				num = 0;
				num2 = 0;
				Tessellation.TessellateRoundedBorders(ref borderParams, posZ, meshWriteData, ref num, ref num2, false);
				Debug.Assert((int)num == meshWriteData.vertexCount);
				Debug.Assert((int)num2 == meshWriteData.indexCount);
				Tessellation.s_MarkerTessellateBorder.End();
			}
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0006AEE0 File Offset: 0x000690E0
		private static void TessellateRoundedCorners(ref MeshGenerationContextUtils.RectangleParams rectParams, float posZ, MeshWriteData mesh, ColorPage colorPage, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			Vector2 vector = new Vector2(rectParams.rect.width * 0.5f, rectParams.rect.height * 0.5f);
			Rect rect = new Rect(rectParams.rect.x, rectParams.rect.y, vector.x, vector.y);
			Tessellation.TessellateRoundedCorner(rect, rectParams.color, posZ, rectParams.topLeftRadius, mesh, colorPage, ref vertexCount, ref indexCount, countOnly);
			ushort num = vertexCount;
			ushort num2 = indexCount;
			Tessellation.TessellateRoundedCorner(rect, rectParams.color, posZ, rectParams.topRightRadius, mesh, colorPage, ref vertexCount, ref indexCount, countOnly);
			bool flag = !countOnly;
			if (flag)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), true);
				Tessellation.FlipWinding(mesh.m_Indices, (int)num2, (int)(indexCount - num2));
			}
			num = vertexCount;
			num2 = indexCount;
			Tessellation.TessellateRoundedCorner(rect, rectParams.color, posZ, rectParams.bottomRightRadius, mesh, colorPage, ref vertexCount, ref indexCount, countOnly);
			bool flag2 = !countOnly;
			if (flag2)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), true);
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), false);
			}
			num = vertexCount;
			num2 = indexCount;
			Tessellation.TessellateRoundedCorner(rect, rectParams.color, posZ, rectParams.bottomLeftRadius, mesh, colorPage, ref vertexCount, ref indexCount, countOnly);
			bool flag3 = !countOnly;
			if (flag3)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), false);
				Tessellation.FlipWinding(mesh.m_Indices, (int)num2, (int)(indexCount - num2));
			}
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0006B07C File Offset: 0x0006927C
		private static void TessellateRoundedBorders(ref MeshGenerationContextUtils.BorderParams border, float posZ, MeshWriteData mesh, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			Vector2 vector = new Vector2(border.rect.width * 0.5f, border.rect.height * 0.5f);
			Rect rect = new Rect(border.rect.x, border.rect.y, vector.x, vector.y);
			Color32 color = border.leftColor;
			Color32 color2 = border.topColor;
			Color32 color3 = border.bottomColor;
			Color32 color4 = border.rightColor;
			Tessellation.TessellateRoundedBorder(rect, color, color2, posZ, border.topLeftRadius, border.leftWidth, border.topWidth, mesh, border.leftColorPage, border.topColorPage, ref vertexCount, ref indexCount, countOnly);
			ushort num = vertexCount;
			ushort num2 = indexCount;
			Tessellation.TessellateRoundedBorder(rect, color4, color2, posZ, border.topRightRadius, border.rightWidth, border.topWidth, mesh, border.rightColorPage, border.topColorPage, ref vertexCount, ref indexCount, countOnly);
			bool flag = !countOnly;
			if (flag)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), true);
				Tessellation.FlipWinding(mesh.m_Indices, (int)num2, (int)(indexCount - num2));
			}
			num = vertexCount;
			num2 = indexCount;
			Tessellation.TessellateRoundedBorder(rect, color4, color3, posZ, border.bottomRightRadius, border.rightWidth, border.bottomWidth, mesh, border.rightColorPage, border.bottomColorPage, ref vertexCount, ref indexCount, countOnly);
			bool flag2 = !countOnly;
			if (flag2)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), true);
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), false);
			}
			num = vertexCount;
			num2 = indexCount;
			Tessellation.TessellateRoundedBorder(rect, color, color3, posZ, border.bottomLeftRadius, border.leftWidth, border.bottomWidth, mesh, border.leftColorPage, border.bottomColorPage, ref vertexCount, ref indexCount, countOnly);
			bool flag3 = !countOnly;
			if (flag3)
			{
				Tessellation.MirrorVertices(rect, mesh.m_Vertices, (int)num, (int)(vertexCount - num), false);
				Tessellation.FlipWinding(mesh.m_Indices, (int)num2, (int)(indexCount - num2));
			}
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0006B280 File Offset: 0x00069480
		private static void TessellateRoundedCorner(Rect rect, Color32 color, float posZ, Vector2 radius, MeshWriteData mesh, ColorPage colorPage, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			Vector2 vector = rect.position + radius;
			Rect zero = Rect.zero;
			bool flag = radius == Vector2.zero;
			if (flag)
			{
				Tessellation.TessellateQuad(rect, (Tessellation.Edges)3, color, posZ, mesh, default(ColorPage), ref vertexCount, ref indexCount, countOnly);
			}
			else
			{
				Tessellation.TessellateFilledFan(vector, radius, Vector2.zero, 0f, 0f, color, color, posZ, mesh, colorPage, colorPage, ref vertexCount, ref indexCount, countOnly);
				bool flag2 = radius.x < rect.width;
				if (flag2)
				{
					zero = new Rect(rect.x + radius.x, rect.y, rect.width - radius.x, rect.height);
					Tessellation.TessellateQuad(zero, Tessellation.Edges.Top, color, posZ, mesh, colorPage, ref vertexCount, ref indexCount, countOnly);
				}
				bool flag3 = radius.y < rect.height;
				if (flag3)
				{
					zero = new Rect(rect.x, rect.y + radius.y, (radius.x < rect.width) ? radius.x : rect.width, rect.height - radius.y);
					Tessellation.TessellateQuad(zero, Tessellation.Edges.Left, color, posZ, mesh, colorPage, ref vertexCount, ref indexCount, countOnly);
				}
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0006B3C8 File Offset: 0x000695C8
		private static void TessellateRoundedBorder(Rect rect, Color32 leftColor, Color32 topColor, float posZ, Vector2 radius, float leftWidth, float topWidth, MeshWriteData mesh, ColorPage leftColorPage, ColorPage topColorPage, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			bool flag = leftWidth < Tessellation.kEpsilon && topWidth < Tessellation.kEpsilon;
			if (!flag)
			{
				leftWidth = Mathf.Max(0f, leftWidth);
				topWidth = Mathf.Max(0f, topWidth);
				radius.x = Mathf.Clamp(radius.x, 0f, rect.width);
				radius.y = Mathf.Clamp(radius.y, 0f, rect.height);
				Vector2 vector = rect.position + radius;
				Rect zero = Rect.zero;
				bool flag2 = radius.x < Tessellation.kEpsilon || radius.y < Tessellation.kEpsilon;
				if (flag2)
				{
					bool flag3 = leftWidth > Tessellation.kEpsilon;
					if (flag3)
					{
						zero = new Rect(rect.x, rect.y, leftWidth, rect.height);
						Tessellation.TessellateStraightBorder(zero, Tessellation.Edges.Left, topWidth, leftColor, posZ, mesh, leftColorPage, ref vertexCount, ref indexCount, countOnly);
					}
					bool flag4 = topWidth > Tessellation.kEpsilon;
					if (flag4)
					{
						zero = new Rect(rect.x, rect.y, rect.width, topWidth);
						Tessellation.TessellateStraightBorder(zero, Tessellation.Edges.Top, leftWidth, topColor, posZ, mesh, topColorPage, ref vertexCount, ref indexCount, countOnly);
					}
				}
				else
				{
					bool flag5 = Tessellation.LooseCompare(radius.x, leftWidth) == 0 && Tessellation.LooseCompare(radius.y, topWidth) == 0;
					if (flag5)
					{
						Tessellation.TessellateFilledFan(vector, radius, Vector2.zero, leftWidth, topWidth, leftColor, topColor, posZ, mesh, leftColorPage, topColorPage, ref vertexCount, ref indexCount, countOnly);
					}
					else
					{
						bool flag6 = Tessellation.LooseCompare(radius.x, leftWidth) > 0 && Tessellation.LooseCompare(radius.y, topWidth) > 0;
						if (flag6)
						{
							Tessellation.TessellateBorderedFan(vector, radius, leftWidth, topWidth, leftColor, topColor, posZ, mesh, leftColorPage, topColorPage, ref vertexCount, ref indexCount, countOnly);
						}
						else
						{
							zero = new Rect(rect.x, rect.y, Mathf.Max(radius.x, leftWidth), Mathf.Max(radius.y, topWidth));
							Tessellation.TessellateComplexBorderCorner(zero, radius, leftWidth, topWidth, leftColor, topColor, posZ, mesh, leftColorPage, topColorPage, ref vertexCount, ref indexCount, countOnly);
						}
					}
					float num = Mathf.Max(radius.y, topWidth);
					zero = new Rect(rect.x, rect.y + num, leftWidth, rect.height - num);
					Tessellation.TessellateStraightBorder(zero, Tessellation.Edges.Left, 0f, leftColor, posZ, mesh, leftColorPage, ref vertexCount, ref indexCount, countOnly);
					num = Mathf.Max(radius.x, leftWidth);
					zero = new Rect(rect.x + num, rect.y, rect.width - num, topWidth);
					Tessellation.TessellateStraightBorder(zero, Tessellation.Edges.Top, 0f, topColor, posZ, mesh, topColorPage, ref vertexCount, ref indexCount, countOnly);
				}
			}
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0006B6B0 File Offset: 0x000698B0
		private static Vector2 IntersectLines(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
		{
			Vector2 vector = p3 - p2;
			Vector2 vector2 = p2 - p0;
			Vector2 vector3 = p1 - p0;
			float num = vector.x * vector3.y - vector3.x * vector.y;
			bool flag = Mathf.Approximately(num, 0f);
			Vector2 vector4;
			if (flag)
			{
				vector4 = new Vector2(float.NaN, float.NaN);
			}
			else
			{
				float num2 = vector.x * vector2.y - vector2.x * vector.y;
				float num3 = num2 / num;
				Vector2 vector5 = p0 + vector3 * num3;
				vector4 = vector5;
			}
			return vector4;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0006B754 File Offset: 0x00069954
		private static int LooseCompare(float a, float b)
		{
			bool flag = a < b - Tessellation.kEpsilon;
			int num;
			if (flag)
			{
				num = -1;
			}
			else
			{
				bool flag2 = a > b + Tessellation.kEpsilon;
				if (flag2)
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0006B78C File Offset: 0x0006998C
		private unsafe static void TessellateComplexBorderCorner(Rect rect, Vector2 radius, float leftWidth, float topWidth, Color32 leftColor, Color32 topColor, float posZ, MeshWriteData mesh, ColorPage leftColorPage, ColorPage topColorPage, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			bool flag = rect.width < Tessellation.kEpsilon || rect.height < Tessellation.kEpsilon;
			if (!flag)
			{
				Vector2 vector = rect.position + radius;
				Vector2 zero = Vector2.zero;
				Vector2 vector2 = vector - radius;
				Vector2 vector3 = new Vector2(leftWidth, topWidth);
				Vector2 vector4 = Tessellation.IntersectLines(vector2, vector3, new Vector2(0f, radius.y), radius);
				bool flag2 = vector4.x >= 0f && Tessellation.LooseCompare(vector4.x, leftWidth) <= 0;
				if (flag2)
				{
					zero.x = Mathf.Min(0f, vector4.x - vector.x);
				}
				vector4 = Tessellation.IntersectLines(vector2, vector3, new Vector2(radius.x, 0f), radius);
				bool flag3 = vector4.y >= 0f && Tessellation.LooseCompare(vector4.y, topWidth) <= 0;
				if (flag3)
				{
					zero.y = Mathf.Min(0f, vector4.y - vector.y);
				}
				Tessellation.TessellateFilledFan(vector, radius, zero, leftWidth, topWidth, leftColor, topColor, posZ, mesh, leftColorPage, topColorPage, ref vertexCount, ref indexCount, countOnly);
				bool flag4 = leftWidth < radius.x && topWidth < radius.y;
				bool flag5 = Tessellation.LooseCompare(rect.height, radius.y) > 0;
				if (flag5)
				{
					Rect rect2 = new Rect(rect.x, rect.y + radius.y, leftWidth, rect.height - radius.y);
					Vector2* ptr;
					checked
					{
						ptr = stackalloc Vector2[unchecked((UIntPtr)4) * (UIntPtr)sizeof(Vector2)];
					}
					ptr[2] = new Vector2(radius.x - leftWidth + zero.x, zero.y);
					Tessellation.TessellateQuad(rect2, Tessellation.Edges.Left | (flag4 ? Tessellation.Edges.Right : Tessellation.Edges.None), ptr, leftColor, posZ, mesh, leftColorPage, ref vertexCount, ref indexCount, countOnly);
				}
				else
				{
					bool flag6 = zero.y < -Tessellation.kEpsilon;
					if (flag6)
					{
						Rect rect3 = new Rect(rect.x, rect.y + radius.y + zero.y, leftWidth, -zero.y);
						Vector2* ptr2;
						checked
						{
							ptr2 = stackalloc Vector2[unchecked((UIntPtr)4) * (UIntPtr)sizeof(Vector2)];
						}
						ptr2[1] = new Vector2(radius.x + zero.x, 0f);
						Tessellation.TessellateQuad(rect3, Tessellation.Edges.Right, ptr2, leftColor, posZ, mesh, leftColorPage, ref vertexCount, ref indexCount, countOnly);
					}
				}
				bool flag7 = Tessellation.LooseCompare(rect.width, radius.x) > 0;
				if (flag7)
				{
					Rect rect4 = new Rect(rect.x + radius.x, rect.y, rect.width - radius.x, topWidth);
					Vector2* ptr3;
					checked
					{
						ptr3 = stackalloc Vector2[unchecked((UIntPtr)4) * (UIntPtr)sizeof(Vector2)];
					}
					*ptr3 = new Vector2(zero.x, radius.y - topWidth + zero.y);
					Tessellation.TessellateQuad(rect4, Tessellation.Edges.Top | (flag4 ? Tessellation.Edges.Bottom : Tessellation.Edges.None), ptr3, topColor, posZ, mesh, topColorPage, ref vertexCount, ref indexCount, countOnly);
				}
				else
				{
					bool flag8 = zero.x < -Tessellation.kEpsilon;
					if (flag8)
					{
						Rect rect5 = new Rect(rect.x + radius.x + zero.x, rect.y, -zero.x, topWidth);
						Vector2* ptr4;
						checked
						{
							ptr4 = stackalloc Vector2[unchecked((UIntPtr)4) * (UIntPtr)sizeof(Vector2)];
						}
						*ptr4 = new Vector2(leftWidth - (radius.x + zero.x), 0f);
						ptr4[1] = new Vector2(0f, radius.y);
						Tessellation.TessellateQuad(rect5, Tessellation.Edges.Bottom, ptr4, topColor, posZ, mesh, topColorPage, ref vertexCount, ref indexCount, countOnly);
					}
				}
			}
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0006BB64 File Offset: 0x00069D64
		private static void TessellateQuad(Rect rect, Color32 color, float posZ, MeshWriteData mesh, ColorPage colorPage, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			bool flag = rect.width < Tessellation.kEpsilon || rect.height < Tessellation.kEpsilon;
			if (!flag)
			{
				if (countOnly)
				{
					vertexCount += 4;
					indexCount += 6;
				}
				else
				{
					Color32 color2 = new Color32(0, 0, 0, colorPage.isValid ? 1 : 0);
					Color32 color3 = new Color32(0, 0, colorPage.pageAndID.r, colorPage.pageAndID.g);
					Color32 color4 = new Color32(0, 0, 0, colorPage.pageAndID.b);
					Vector3 vector = new Vector3(rect.x, rect.y, posZ);
					Vector3 vector2 = new Vector3(rect.xMax, rect.y, posZ);
					Vector3 vector3 = new Vector3(rect.x, rect.yMax, posZ);
					Vector3 vector4 = new Vector3(rect.xMax, rect.yMax, posZ);
					mesh.SetNextVertex(new Vertex
					{
						position = vector,
						tint = color,
						flags = color2,
						opacityColorPages = color3,
						ids = color4
					});
					mesh.SetNextVertex(new Vertex
					{
						position = vector2,
						tint = color,
						flags = color2,
						opacityColorPages = color3,
						ids = color4
					});
					mesh.SetNextVertex(new Vertex
					{
						position = vector3,
						tint = color,
						flags = color2,
						opacityColorPages = color3,
						ids = color4
					});
					mesh.SetNextVertex(new Vertex
					{
						position = vector4,
						tint = color,
						flags = color2,
						opacityColorPages = color3,
						ids = color4
					});
					mesh.SetNextIndex(vertexCount);
					mesh.SetNextIndex(vertexCount + 1);
					mesh.SetNextIndex(vertexCount + 2);
					mesh.SetNextIndex(vertexCount + 3);
					mesh.SetNextIndex(vertexCount + 2);
					mesh.SetNextIndex(vertexCount + 1);
					vertexCount += 4;
					indexCount += 6;
				}
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0006BDA8 File Offset: 0x00069FA8
		private static void TessellateQuad(Rect rect, Tessellation.Edges smoothedEdges, Color32 color, float posZ, MeshWriteData mesh, ColorPage colorPage, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			Tessellation.TessellateQuad(rect, smoothedEdges, null, color, posZ, mesh, colorPage, ref vertexCount, ref indexCount, countOnly);
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0006BDCC File Offset: 0x00069FCC
		private static int EdgesCount(Tessellation.Edges edges)
		{
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				bool flag = (edges & (Tessellation.Edges)(1 << i)) > Tessellation.Edges.None;
				if (flag)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0006BE08 File Offset: 0x0006A008
		private unsafe static void TessellateQuad(Rect rect, Tessellation.Edges smoothedEdges, Vector2* offsets, Color32 color, float posZ, MeshWriteData mesh, ColorPage colorPage, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			bool flag = rect.width < Tessellation.kEpsilon || rect.height < Tessellation.kEpsilon;
			if (!flag)
			{
				bool flag2 = smoothedEdges == Tessellation.Edges.None && offsets == null;
				if (flag2)
				{
					Tessellation.TessellateQuad(rect, color, posZ, mesh, colorPage, ref vertexCount, ref indexCount, countOnly);
				}
				else
				{
					bool flag3 = Tessellation.EdgesCount(smoothedEdges) == 1 && offsets == null;
					if (flag3)
					{
						Tessellation.TessellateQuadSingleEdge(rect, smoothedEdges, color, posZ, mesh, colorPage, ref vertexCount, ref indexCount, countOnly);
					}
					else if (countOnly)
					{
						vertexCount += 12;
						indexCount += 12;
					}
					else
					{
						Vector3* ptr;
						checked
						{
							ptr = stackalloc Vector3[unchecked((UIntPtr)4) * (UIntPtr)sizeof(Vector3)];
							*ptr = new Vector3(rect.xMin, rect.yMax, posZ);
						}
						ptr[1] = new Vector3(rect.xMin, rect.yMin, posZ);
						ptr[2] = new Vector3(rect.xMax, rect.yMin, posZ);
						ptr[3] = new Vector3(rect.xMax, rect.yMax, posZ);
						Vector3 vector = Vector3.zero;
						bool flag4 = offsets != null;
						if (flag4)
						{
							*ptr += *offsets;
							ptr[1] += offsets[1];
							ptr[2] += offsets[2];
							ptr[3] += offsets[3];
							vector += *ptr;
							vector += ptr[1];
							vector += ptr[2];
							vector += ptr[3];
							vector /= 4f;
							vector.z = posZ;
						}
						else
						{
							vector = new Vector3(rect.xMin + rect.width / 2f, rect.yMin + rect.height / 2f, posZ);
						}
						Color32 color2 = new Color32(0, 0, 0, colorPage.isValid ? 1 : 0);
						Color32 color3 = new Color32(0, 0, colorPage.pageAndID.r, colorPage.pageAndID.g);
						Color32 color4 = new Color32(0, 0, 0, colorPage.pageAndID.b);
						ushort num = vertexCount;
						for (int i = 0; i < Tessellation.s_AllEdges.Length; i++)
						{
							Tessellation.Edges edges = Tessellation.s_AllEdges[i];
							Vector3 vector2 = ptr[i];
							Vector3 vector3 = ptr[(i + 1) % 4];
							float magnitude = ((vector2 + vector3) / 2f - vector).magnitude;
							Vertex vertex = new Vertex
							{
								position = vector2,
								tint = color,
								flags = color2,
								opacityColorPages = color3,
								ids = color4
							};
							Vertex vertex2 = new Vertex
							{
								position = vector3,
								tint = color,
								flags = color2,
								opacityColorPages = color3,
								ids = color4
							};
							Vertex vertex3 = new Vertex
							{
								position = vector,
								tint = color,
								flags = color2,
								opacityColorPages = color3,
								ids = color4
							};
							bool flag5 = (smoothedEdges & edges) == edges;
							if (flag5)
							{
								Tessellation.EncodeStraightArc(ref vertex, ref vertex2, ref vertex3, magnitude);
							}
							mesh.SetNextVertex(vertex);
							mesh.SetNextVertex(vertex2);
							mesh.SetNextVertex(vertex3);
							ushort num2 = num;
							num = num2 + 1;
							mesh.SetNextIndex(num2);
							ushort num3 = num;
							num = num3 + 1;
							mesh.SetNextIndex(num3);
							ushort num4 = num;
							num = num4 + 1;
							mesh.SetNextIndex(num4);
						}
						vertexCount += 12;
						indexCount += 12;
					}
				}
			}
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0006C298 File Offset: 0x0006A498
		private static void EncodeStraightArc(ref Vertex v0, ref Vertex v1, ref Vertex center, float radius)
		{
			Tessellation.ExpandTriangle(ref v0.position, ref v1.position, center.position, 2f);
			Vector3 vector = (v0.position + v1.position) / 2f;
			float magnitude = (center.position - vector).magnitude;
			float num = magnitude / radius;
			center.circle = new Vector4(0f, 0f, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
			v0.circle = new Vector4(num, 0f, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
			v1.circle = new Vector4(num, 0f, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
			v0.flags.b = 1;
			v1.flags.b = 1;
			center.flags.b = 1;
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0006C374 File Offset: 0x0006A574
		private static void ExpandTriangle(ref Vector3 v0, ref Vector3 v1, Vector3 center, float factor)
		{
			v0 += (v0 - center).normalized * factor;
			v1 += (v1 - center).normalized * factor;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0006C3D8 File Offset: 0x0006A5D8
		private static void TessellateQuadSingleEdge(Rect rect, Tessellation.Edges smoothedEdge, Color32 color, float posZ, MeshWriteData mesh, ColorPage colorPage, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			if (countOnly)
			{
				vertexCount += 4;
				indexCount += 6;
			}
			else
			{
				Vector3 vector = new Vector3(rect.x, rect.y, posZ);
				Vector3 vector2 = new Vector3(rect.x + rect.width, rect.y, posZ);
				Vector3 vector3 = new Vector3(rect.x + rect.width, rect.y + rect.height, posZ);
				Vector3 vector4 = new Vector3(rect.x, rect.y + rect.height, posZ);
				Vector2 vector5 = new Vector2(Mathf.Abs(vector2.x - vector.x), Mathf.Abs(vector3.y - vector2.y));
				Vector2 vector6 = new Vector2((vector5.x + 2f) / vector5.x, (vector5.y + 2f) / vector5.y);
				Vector4 vector7 = Vector4.zero;
				Vector4 vector8 = Vector4.zero;
				Vector4 vector9 = Vector4.zero;
				Vector4 zero = Vector4.zero;
				switch (smoothedEdge)
				{
				case Tessellation.Edges.Left:
					vector.x -= 2f;
					vector4.x -= 2f;
					zero = new Vector4(vector6.x, 0f, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
					vector7 = zero;
					vector9 = new Vector4(0f, 0f, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
					vector8 = vector9;
					break;
				case Tessellation.Edges.Top:
					vector.y -= 2f;
					vector2.y -= 2f;
					vector8 = new Vector4(0f, vector6.y, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
					vector7 = vector8;
					zero = new Vector4(0f, 0f, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
					vector9 = zero;
					break;
				case (Tessellation.Edges)3:
					break;
				case Tessellation.Edges.Right:
					vector2.x += 2f;
					vector3.x += 2f;
					vector9 = new Vector4(vector6.x, 0f, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
					vector8 = vector9;
					zero = new Vector4(0f, 0f, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
					vector7 = zero;
					break;
				default:
					if (smoothedEdge == Tessellation.Edges.Bottom)
					{
						vector3.y += 2f;
						vector4.y += 2f;
						zero = new Vector4(0f, vector6.y, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
						vector9 = zero;
						vector8 = new Vector4(0f, 0f, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
						vector7 = vector8;
					}
					break;
				}
				Color32 color2 = new Color32(0, 0, 1, colorPage.isValid ? 1 : 0);
				Color32 color3 = new Color32(0, 0, colorPage.pageAndID.r, colorPage.pageAndID.g);
				Color32 color4 = new Color32(0, 0, 0, colorPage.pageAndID.b);
				ushort num = vertexCount;
				mesh.SetNextVertex(new Vertex
				{
					position = vector,
					tint = color,
					flags = color2,
					circle = vector7,
					opacityColorPages = color3,
					ids = color4
				});
				mesh.SetNextVertex(new Vertex
				{
					position = vector2,
					tint = color,
					flags = color2,
					circle = vector8,
					opacityColorPages = color3,
					ids = color4
				});
				mesh.SetNextVertex(new Vertex
				{
					position = vector3,
					tint = color,
					flags = color2,
					circle = vector9,
					opacityColorPages = color3,
					ids = color4
				});
				mesh.SetNextVertex(new Vertex
				{
					position = vector4,
					tint = color,
					flags = color2,
					circle = zero,
					opacityColorPages = color3,
					ids = color4
				});
				mesh.SetNextIndex(num);
				mesh.SetNextIndex(num + 1);
				mesh.SetNextIndex(num + 2);
				mesh.SetNextIndex(num);
				mesh.SetNextIndex(num + 2);
				mesh.SetNextIndex(num + 3);
				vertexCount += 4;
				indexCount += 6;
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0006C87C File Offset: 0x0006AA7C
		private static void TessellateStraightBorder(Rect rect, Tessellation.Edges smoothedEdge, float miterOffset, Color color, float posZ, MeshWriteData mesh, ColorPage colorPage, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			Debug.Assert(smoothedEdge == Tessellation.Edges.Left || smoothedEdge == Tessellation.Edges.Top);
			bool flag = rect.width < Tessellation.kEpsilon || rect.height < Tessellation.kEpsilon;
			if (!flag)
			{
				if (countOnly)
				{
					vertexCount += 4;
					indexCount += 6;
				}
				else
				{
					Vector3 vector = new Vector3(rect.xMin, rect.yMin, posZ);
					Vector3 vector2 = new Vector3(rect.xMax, rect.yMin, posZ);
					Vector3 vector3 = new Vector3(rect.xMax, rect.yMax, posZ);
					Vector3 vector4 = new Vector3(rect.xMin, rect.yMax, posZ);
					Color32 color2 = new Color32(0, 0, 1, colorPage.isValid ? 1 : 0);
					Color32 color3 = new Color32(0, 0, colorPage.pageAndID.r, colorPage.pageAndID.g);
					Color32 color4 = new Color32(0, 0, 0, colorPage.pageAndID.b);
					bool flag2 = smoothedEdge == Tessellation.Edges.Left;
					if (flag2)
					{
						Vector3 vector5 = vector;
						Vector3 vector6 = vector2;
						vector.x -= 2f;
						vector2.x += 2f;
						vector3.x += 2f;
						vector4.x -= 2f;
						float num = vector2.x - vector.x;
						Vector4 vector7 = new Vector4(num / (rect.width + 2f), 0f, num / 2f, 0f);
						Vector4 zero = Vector4.zero;
						Vertex vertex = new Vertex
						{
							position = vector,
							tint = color,
							flags = color2,
							circle = vector7,
							opacityColorPages = color3,
							ids = color4
						};
						Vertex vertex2 = new Vertex
						{
							position = vector2,
							tint = color,
							flags = color2,
							circle = zero,
							opacityColorPages = color3,
							ids = color4
						};
						Vertex vertex3 = new Vertex
						{
							position = vector3,
							tint = color,
							flags = color2,
							circle = zero,
							opacityColorPages = color3,
							ids = color4
						};
						Vertex vertex4 = new Vertex
						{
							position = vector4,
							tint = color,
							flags = color2,
							circle = vector7,
							opacityColorPages = color3,
							ids = color4
						};
						vector = vector5;
						vector2 = vector6;
						vector2.y += miterOffset;
						Vector3 vector8 = (vector2 - vector).normalized * 1.4142f * 2f;
						vector -= vector8;
						vector2 += vector8;
						vertex.circle = Tessellation.GetInterpolatedCircle(vector, ref vertex, ref vertex2, ref vertex3);
						vertex.position = vector;
						vertex2.circle = Tessellation.GetInterpolatedCircle(vector2, ref vertex, ref vertex2, ref vertex3);
						vertex2.position = vector2;
						mesh.SetNextVertex(vertex);
						mesh.SetNextVertex(vertex2);
						mesh.SetNextVertex(vertex3);
						mesh.SetNextVertex(vertex4);
					}
					else
					{
						Vector3 vector9 = vector;
						Vector3 vector10 = vector4;
						vector.y -= 2f;
						vector2.y -= 2f;
						vector3.y += 2f;
						vector4.y += 2f;
						float num2 = vector4.y - vector.y;
						Vector4 vector11 = new Vector4(0f, num2 / (rect.height + 2f), 0f, num2 / 2f);
						Vector4 zero2 = Vector4.zero;
						Vertex vertex5 = new Vertex
						{
							position = vector,
							tint = color,
							flags = color2,
							circle = vector11,
							opacityColorPages = color3,
							ids = color4
						};
						Vertex vertex6 = new Vertex
						{
							position = vector2,
							tint = color,
							flags = color2,
							circle = vector11,
							opacityColorPages = color3,
							ids = color4
						};
						Vertex vertex7 = new Vertex
						{
							position = vector3,
							tint = color,
							flags = color2,
							circle = zero2,
							opacityColorPages = color3,
							ids = color4
						};
						Vertex vertex8 = new Vertex
						{
							position = vector4,
							tint = color,
							flags = color2,
							circle = zero2,
							opacityColorPages = color3,
							ids = color4
						};
						vector = vector9;
						vector4 = vector10;
						vector4.x += miterOffset;
						Vector3 vector12 = (vector4 - vector).normalized * 1.4142f * 2f;
						vector -= vector12;
						vector4 += vector12;
						vertex5.circle = Tessellation.GetInterpolatedCircle(vector, ref vertex5, ref vertex6, ref vertex7);
						vertex5.position = vector;
						vertex8.circle = Tessellation.GetInterpolatedCircle(vector4, ref vertex5, ref vertex6, ref vertex7);
						vertex8.position = vector4;
						mesh.SetNextVertex(vertex5);
						mesh.SetNextVertex(vertex6);
						mesh.SetNextVertex(vertex7);
						mesh.SetNextVertex(vertex8);
					}
					ushort num3 = vertexCount;
					mesh.SetNextIndex(num3);
					mesh.SetNextIndex(num3 + 1);
					mesh.SetNextIndex(num3 + 2);
					mesh.SetNextIndex(num3 + 2);
					mesh.SetNextIndex(num3 + 3);
					mesh.SetNextIndex(num3);
					vertexCount += 4;
					indexCount += 6;
				}
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0006CE9C File Offset: 0x0006B09C
		private static Vector4 GetInterpolatedCircle(Vector2 p, ref Vertex v0, ref Vertex v1, ref Vertex v2)
		{
			float num;
			float num2;
			float num3;
			Tessellation.ComputeBarycentricCoordinates(p, v0.position, v1.position, v2.position, out num, out num2, out num3);
			return v0.circle * num + v1.circle * num2 + v2.circle * num3;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0006CF0C File Offset: 0x0006B10C
		private static void ComputeBarycentricCoordinates(Vector2 p, Vector2 a, Vector2 b, Vector2 c, out float u, out float v, out float w)
		{
			Vector2 vector = b - a;
			Vector2 vector2 = c - a;
			Vector2 vector3 = p - a;
			float num = Vector2.Dot(vector, vector);
			float num2 = Vector2.Dot(vector, vector2);
			float num3 = Vector2.Dot(vector2, vector2);
			float num4 = Vector2.Dot(vector3, vector);
			float num5 = Vector2.Dot(vector3, vector2);
			float num6 = num * num3 - num2 * num2;
			v = (num3 * num4 - num2 * num5) / num6;
			w = (num * num5 - num2 * num4) / num6;
			u = 1f - v - w;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0006CF9C File Offset: 0x0006B19C
		private static void TessellateFilledFan(Vector2 center, Vector2 radius, Vector2 miterOffset, float leftWidth, float topWidth, Color32 leftColor, Color32 topColor, float posZ, MeshWriteData mesh, ColorPage leftColorPage, ColorPage topColorPage, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			if (countOnly)
			{
				vertexCount += 6;
				indexCount += 6;
			}
			else
			{
				Color32 color = new Color32(0, 0, 1, leftColorPage.isValid ? 1 : 0);
				Color32 color2 = new Color32(0, 0, leftColorPage.pageAndID.r, leftColorPage.pageAndID.g);
				Color32 color3 = new Color32(0, 0, 0, leftColorPage.pageAndID.b);
				Color32 color4 = new Color32(0, 0, 1, topColorPage.isValid ? 1 : 0);
				Color32 color5 = new Color32(0, 0, topColorPage.pageAndID.r, topColorPage.pageAndID.g);
				Color32 color6 = new Color32(0, 0, 0, topColorPage.pageAndID.b);
				Vertex vertex = default(Vertex);
				Vertex vertex2 = vertex;
				Vertex vertex3 = vertex;
				Vertex vertex4 = vertex;
				Vector2 vector = new Vector2(radius.x + 2f, radius.y + 2f);
				Vector2 vector2 = new Vector2(vector.x / radius.x, vector.y / radius.y);
				vertex.position = new Vector3(center.x, center.y, posZ);
				vertex2.position = new Vector3(center.x - vector.x, center.y, posZ);
				vertex3.position = new Vector3(center.x - vector.x, center.y - vector.y, posZ);
				vertex4.position = new Vector3(center.x, center.y - vector.y, posZ);
				vertex.circle = new Vector4(0f, 0f, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
				vertex2.circle = new Vector4(vector2.x, 0f, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
				vertex3.circle = new Vector4(vector2.x, vector2.y, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
				vertex4.circle = new Vector4(0f, vector2.y, Tessellation.kUnusedArc, Tessellation.kUnusedArc);
				bool flag = miterOffset != Vector2.zero;
				if (flag)
				{
					Vector3 vector3 = vertex.position + miterOffset;
					vertex.circle = Tessellation.GetInterpolatedCircle(vector3, ref vertex, ref vertex2, ref vertex3);
					vertex.position = vector3;
					bool flag2 = miterOffset.y != 0f;
					if (flag2)
					{
						vector3 = vertex2.position - new Vector3(miterOffset.x * 2f / radius.x, miterOffset.y * 2f / radius.y, 0f);
						vertex2.circle = Tessellation.GetInterpolatedCircle(new Vector2(vector3.x, vector3.y), ref vertex, ref vertex2, ref vertex3);
						vertex2.position = vector3;
					}
					else
					{
						bool flag3 = miterOffset.x != 0f;
						if (flag3)
						{
							vector3 = vertex4.position - new Vector3(miterOffset.x * 2f / radius.x, miterOffset.y * 2f / radius.y, 0f);
							vertex4.circle = Tessellation.GetInterpolatedCircle(new Vector2(vector3.x, vector3.y), ref vertex, ref vertex2, ref vertex3);
							vertex4.position = vector3;
						}
					}
				}
				Vertex vertex5 = vertex3;
				Vertex vertex6 = vertex;
				vertex.tint = leftColor;
				vertex2.tint = leftColor;
				vertex5.tint = leftColor;
				vertex3.tint = topColor;
				vertex4.tint = topColor;
				vertex6.tint = topColor;
				vertex.flags = color;
				vertex.opacityColorPages = color2;
				vertex.ids = color3;
				vertex2.flags = color;
				vertex2.opacityColorPages = color2;
				vertex2.ids = color3;
				vertex5.flags = color;
				vertex5.opacityColorPages = color2;
				vertex5.ids = color3;
				vertex3.flags = color4;
				vertex3.opacityColorPages = color5;
				vertex3.ids = color6;
				vertex4.flags = color4;
				vertex4.opacityColorPages = color5;
				vertex4.ids = color6;
				vertex6.flags = color4;
				vertex6.opacityColorPages = color5;
				vertex6.ids = color6;
				mesh.SetNextVertex(vertex);
				mesh.SetNextVertex(vertex2);
				mesh.SetNextVertex(vertex5);
				mesh.SetNextVertex(vertex3);
				mesh.SetNextVertex(vertex4);
				mesh.SetNextVertex(vertex6);
				mesh.SetNextIndex(vertexCount);
				mesh.SetNextIndex(vertexCount + 1);
				mesh.SetNextIndex(vertexCount + 2);
				mesh.SetNextIndex(vertexCount + 3);
				mesh.SetNextIndex(vertexCount + 4);
				mesh.SetNextIndex(vertexCount + 5);
				vertexCount += 6;
				indexCount += 6;
			}
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0006D49C File Offset: 0x0006B69C
		private static void TessellateBorderedFan(Vector2 center, Vector2 outerRadius, float leftWidth, float topWidth, Color32 leftColor, Color32 topColor, float posZ, MeshWriteData mesh, ColorPage leftColorPage, ColorPage topColorPage, ref ushort vertexCount, ref ushort indexCount, bool countOnly)
		{
			if (countOnly)
			{
				vertexCount += 6;
				indexCount += 6;
			}
			else
			{
				Vector2 vector = new Vector2(outerRadius.x - leftWidth, outerRadius.y - topWidth);
				Color32 color = new Color32(0, 0, 1, leftColorPage.isValid ? 1 : 0);
				Color32 color2 = new Color32(0, 0, leftColorPage.pageAndID.r, leftColorPage.pageAndID.g);
				Color32 color3 = new Color32(0, 0, 0, leftColorPage.pageAndID.b);
				Color32 color4 = new Color32(0, 0, 1, topColorPage.isValid ? 1 : 0);
				Color32 color5 = new Color32(0, 0, topColorPage.pageAndID.r, topColorPage.pageAndID.g);
				Color32 color6 = new Color32(0, 0, 0, topColorPage.pageAndID.b);
				Vertex vertex = default(Vertex);
				Vertex vertex2 = vertex;
				Vertex vertex3 = vertex;
				Vertex vertex4 = vertex;
				vertex.position = new Vector3(center.x, center.y, posZ);
				vertex2.position = new Vector3(center.x - outerRadius.x, center.y, posZ);
				vertex3.position = new Vector3(center.x - outerRadius.x, center.y - outerRadius.y, posZ);
				vertex4.position = new Vector3(center.x, center.y - outerRadius.y, posZ);
				Vector2 vector2 = outerRadius / vector;
				vertex.circle = new Vector4(0f, 0f, 0f, 0f);
				vertex2.circle = new Vector4(1f, 0f, vector2.x, 0f);
				vertex3.circle = new Vector4(1f, 1f, vector2.x, vector2.y);
				vertex4.circle = new Vector4(0f, 1f, 0f, vector2.y);
				Vertex vertex5 = vertex3;
				Vertex vertex6 = vertex;
				vertex.tint = leftColor;
				vertex2.tint = leftColor;
				vertex5.tint = leftColor;
				vertex3.tint = topColor;
				vertex4.tint = topColor;
				vertex6.tint = topColor;
				vertex.flags = color;
				vertex.opacityColorPages = color2;
				vertex.ids = color3;
				vertex2.flags = color;
				vertex2.opacityColorPages = color2;
				vertex2.ids = color3;
				vertex5.flags = color;
				vertex5.opacityColorPages = color2;
				vertex5.ids = color3;
				vertex3.flags = color4;
				vertex3.opacityColorPages = color5;
				vertex3.ids = color6;
				vertex4.flags = color4;
				vertex4.opacityColorPages = color5;
				vertex4.ids = color6;
				vertex6.flags = color4;
				vertex6.opacityColorPages = color5;
				vertex6.ids = color6;
				mesh.SetNextVertex(vertex);
				mesh.SetNextVertex(vertex2);
				mesh.SetNextVertex(vertex5);
				mesh.SetNextVertex(vertex3);
				mesh.SetNextVertex(vertex4);
				mesh.SetNextVertex(vertex6);
				mesh.SetNextIndex(vertexCount);
				mesh.SetNextIndex(vertexCount + 1);
				mesh.SetNextIndex(vertexCount + 2);
				mesh.SetNextIndex(vertexCount + 3);
				mesh.SetNextIndex(vertexCount + 4);
				mesh.SetNextIndex(vertexCount + 5);
				vertexCount += 6;
				indexCount += 6;
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0006D820 File Offset: 0x0006BA20
		private static void MirrorVertices(Rect rect, NativeSlice<Vertex> vertices, int vertexStart, int vertexCount, bool flipHorizontal)
		{
			if (flipHorizontal)
			{
				for (int i = 0; i < vertexCount; i++)
				{
					Vertex vertex = vertices[vertexStart + i];
					vertex.position.x = rect.xMax - (vertex.position.x - rect.xMax);
					vertex.uv.x = -vertex.uv.x;
					vertices[vertexStart + i] = vertex;
				}
			}
			else
			{
				for (int j = 0; j < vertexCount; j++)
				{
					Vertex vertex2 = vertices[vertexStart + j];
					vertex2.position.y = rect.yMax - (vertex2.position.y - rect.yMax);
					vertex2.uv.y = -vertex2.uv.y;
					vertices[vertexStart + j] = vertex2;
				}
			}
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0006D918 File Offset: 0x0006BB18
		private static void FlipWinding(NativeSlice<ushort> indices, int indexStart, int indexCount)
		{
			for (int i = 0; i < indexCount; i += 3)
			{
				ushort num = indices[indexStart + i];
				indices[indexStart + i] = indices[indexStart + i + 1];
				indices[indexStart + i + 1] = num;
			}
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0006D968 File Offset: 0x0006BB68
		private static void ComputeUVs(Rect tessellatedRect, Rect textureRect, Rect uvRegion, NativeSlice<Vertex> vertices)
		{
			Vector2 position = tessellatedRect.position;
			Vector2 vector = new Vector2(1f / tessellatedRect.width, 1f / tessellatedRect.height);
			for (int i = 0; i < vertices.Length; i++)
			{
				Vertex vertex = vertices[i];
				Vector2 vector2 = vertex.position;
				vector2 -= position;
				vector2 *= vector;
				vertex.uv.x = (vector2.x * textureRect.width + textureRect.xMin) * uvRegion.width + uvRegion.xMin;
				vertex.uv.y = ((1f - vector2.y) * textureRect.height + textureRect.yMin) * uvRegion.height + uvRegion.yMin;
				vertices[i] = vertex;
			}
		}

		// Token: 0x04000BA5 RID: 2981
		internal static float kEpsilon = 0.001f;

		// Token: 0x04000BA6 RID: 2982
		internal static float kUnusedArc = -9999.9f;

		// Token: 0x04000BA7 RID: 2983
		internal static ushort kSubdivisions = 6;

		// Token: 0x04000BA8 RID: 2984
		private static ProfilerMarker s_MarkerTessellateRect = new ProfilerMarker("TessellateRect");

		// Token: 0x04000BA9 RID: 2985
		private static ProfilerMarker s_MarkerTessellateBorder = new ProfilerMarker("TessellateBorder");

		// Token: 0x04000BAA RID: 2986
		internal const int kMaxEdgeBit = 4;

		// Token: 0x04000BAB RID: 2987
		private static Tessellation.Edges[] s_AllEdges = new Tessellation.Edges[]
		{
			Tessellation.Edges.Left,
			Tessellation.Edges.Top,
			Tessellation.Edges.Right,
			Tessellation.Edges.Bottom
		};

		// Token: 0x0200031C RID: 796
		internal enum Edges
		{
			// Token: 0x04000BAD RID: 2989
			None,
			// Token: 0x04000BAE RID: 2990
			Left,
			// Token: 0x04000BAF RID: 2991
			Top,
			// Token: 0x04000BB0 RID: 2992
			Right = 4,
			// Token: 0x04000BB1 RID: 2993
			Bottom = 8,
			// Token: 0x04000BB2 RID: 2994
			All = 15
		}
	}
}

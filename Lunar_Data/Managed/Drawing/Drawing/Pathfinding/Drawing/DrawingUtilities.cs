using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding.Drawing
{
	// Token: 0x02000049 RID: 73
	public static class DrawingUtilities
	{
		// Token: 0x0600026F RID: 623 RVA: 0x0000B414 File Offset: 0x00009614
		public static Bounds BoundsFrom(GameObject gameObject)
		{
			return DrawingUtilities.BoundsFrom(gameObject.transform);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000B424 File Offset: 0x00009624
		public static Bounds BoundsFrom(Transform transform)
		{
			transform.gameObject.GetComponents<Component>(DrawingUtilities.componentBuffer);
			Bounds bounds = new Bounds(transform.position, Vector3.zero);
			for (int i = 0; i < DrawingUtilities.componentBuffer.Count; i++)
			{
				Component component = DrawingUtilities.componentBuffer[i];
				Collider collider = component as Collider;
				if (collider != null)
				{
					bounds.Encapsulate(collider.bounds);
				}
				else
				{
					Collider2D collider2D = component as Collider2D;
					if (collider2D != null)
					{
						bounds.Encapsulate(collider2D.bounds);
					}
					else
					{
						MeshRenderer meshRenderer = component as MeshRenderer;
						if (meshRenderer != null)
						{
							bounds.Encapsulate(meshRenderer.bounds);
						}
						else
						{
							SpriteRenderer spriteRenderer = component as SpriteRenderer;
							if (spriteRenderer != null)
							{
								bounds.Encapsulate(spriteRenderer.bounds);
							}
						}
					}
				}
			}
			DrawingUtilities.componentBuffer.Clear();
			int childCount = transform.childCount;
			for (int j = 0; j < childCount; j++)
			{
				bounds.Encapsulate(DrawingUtilities.BoundsFrom(transform.GetChild(j)));
			}
			return bounds;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000B51C File Offset: 0x0000971C
		public static Bounds BoundsFrom(List<Vector3> points)
		{
			if (points.Count == 0)
			{
				throw new ArgumentException("At least 1 point is required");
			}
			Vector3 vector = points[0];
			Vector3 vector2 = points[0];
			for (int i = 0; i < points.Count; i++)
			{
				vector = Vector3.Min(vector, points[i]);
				vector2 = Vector3.Max(vector2, points[i]);
			}
			return new Bounds((vector2 + vector) * 0.5f, (vector2 - vector) * 0.5f);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000B5A0 File Offset: 0x000097A0
		public static Bounds BoundsFrom(Vector3[] points)
		{
			if (points.Length == 0)
			{
				throw new ArgumentException("At least 1 point is required");
			}
			Vector3 vector = points[0];
			Vector3 vector2 = points[0];
			for (int i = 0; i < points.Length; i++)
			{
				vector = Vector3.Min(vector, points[i]);
				vector2 = Vector3.Max(vector2, points[i]);
			}
			return new Bounds((vector2 + vector) * 0.5f, (vector2 - vector) * 0.5f);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000B620 File Offset: 0x00009820
		public static Bounds BoundsFrom(NativeArray<float3> points)
		{
			if (points.Length == 0)
			{
				throw new ArgumentException("At least 1 point is required");
			}
			float3 @float = points[0];
			float3 float2 = points[0];
			for (int i = 0; i < points.Length; i++)
			{
				@float = math.min(@float, points[i]);
				float2 = math.max(float2, points[i]);
			}
			return new Bounds((float2 + @float) * 0.5f, (float2 - @float) * 0.5f);
		}

		// Token: 0x04000123 RID: 291
		private static List<Component> componentBuffer = new List<Component>();
	}
}

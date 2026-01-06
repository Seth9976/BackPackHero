using System;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/RuleTile.html")]
	[Serializable]
	public class HexagonalRuleTile : RuleTile
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002064 File Offset: 0x00000264
		public override int m_RotationAngle
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		public static Vector3 TilemapPositionToWorldPosition(Vector3Int tilemapPosition)
		{
			Vector3 vector = new Vector3((float)tilemapPosition.x, (float)tilemapPosition.y);
			if (tilemapPosition.y % 2 != 0)
			{
				vector.x += 0.5f;
			}
			vector.y *= HexagonalRuleTile.m_TilemapToWorldYScale;
			return vector;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020B8 File Offset: 0x000002B8
		public static Vector3Int WorldPositionToTilemapPosition(Vector3 worldPosition)
		{
			worldPosition.y /= HexagonalRuleTile.m_TilemapToWorldYScale;
			Vector3Int vector3Int = default(Vector3Int);
			vector3Int.y = Mathf.RoundToInt(worldPosition.y);
			if (vector3Int.y % 2 != 0)
			{
				vector3Int.x = Mathf.RoundToInt(worldPosition.x - 0.5f);
			}
			else
			{
				vector3Int.x = Mathf.RoundToInt(worldPosition.x);
			}
			return vector3Int;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002128 File Offset: 0x00000328
		public override Vector3Int GetOffsetPosition(Vector3Int position, Vector3Int offset)
		{
			Vector3Int vector3Int = position + offset;
			if (offset.y % 2 != 0 && position.y % 2 != 0)
			{
				vector3Int.x++;
			}
			return vector3Int;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002163 File Offset: 0x00000363
		public override Vector3Int GetOffsetPositionReverse(Vector3Int position, Vector3Int offset)
		{
			return this.GetOffsetPosition(position, this.GetRotatedPosition(offset, 180));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002178 File Offset: 0x00000378
		public override Vector3Int GetRotatedPosition(Vector3Int position, int rotation)
		{
			if (rotation != 0)
			{
				Vector3 vector = HexagonalRuleTile.TilemapPositionToWorldPosition(position);
				int num = rotation / 60;
				if (this.m_FlatTop)
				{
					vector = new Vector3(vector.x * HexagonalRuleTile.m_CosAngleArr2[num] - vector.y * HexagonalRuleTile.m_SinAngleArr2[num], vector.x * HexagonalRuleTile.m_SinAngleArr2[num] + vector.y * HexagonalRuleTile.m_CosAngleArr2[num]);
				}
				else
				{
					vector = new Vector3(vector.x * HexagonalRuleTile.m_CosAngleArr1[num] - vector.y * HexagonalRuleTile.m_SinAngleArr1[num], vector.x * HexagonalRuleTile.m_SinAngleArr1[num] + vector.y * HexagonalRuleTile.m_CosAngleArr1[num]);
				}
				position = HexagonalRuleTile.WorldPositionToTilemapPosition(vector);
			}
			return position;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000222C File Offset: 0x0000042C
		public override Vector3Int GetMirroredPosition(Vector3Int position, bool mirrorX, bool mirrorY)
		{
			if (mirrorX || mirrorY)
			{
				Vector3 vector = HexagonalRuleTile.TilemapPositionToWorldPosition(position);
				if (this.m_FlatTop)
				{
					if (mirrorX)
					{
						vector.y *= -1f;
					}
					if (mirrorY)
					{
						vector.x *= -1f;
					}
				}
				else
				{
					if (mirrorX)
					{
						vector.x *= -1f;
					}
					if (mirrorY)
					{
						vector.y *= -1f;
					}
				}
				position = HexagonalRuleTile.WorldPositionToTilemapPosition(vector);
			}
			return position;
		}

		// Token: 0x04000001 RID: 1
		private static float[] m_CosAngleArr1 = new float[]
		{
			Mathf.Cos(0f),
			Mathf.Cos(-1.0471976f),
			Mathf.Cos(-2.0943952f),
			Mathf.Cos(-3.1415927f),
			Mathf.Cos(-4.1887903f),
			Mathf.Cos(-5.2359877f)
		};

		// Token: 0x04000002 RID: 2
		private static float[] m_SinAngleArr1 = new float[]
		{
			Mathf.Sin(0f),
			Mathf.Sin(-1.0471976f),
			Mathf.Sin(-2.0943952f),
			Mathf.Sin(-3.1415927f),
			Mathf.Sin(-4.1887903f),
			Mathf.Sin(-5.2359877f)
		};

		// Token: 0x04000003 RID: 3
		private static float[] m_CosAngleArr2 = new float[]
		{
			Mathf.Cos(0f),
			Mathf.Cos(1.0471976f),
			Mathf.Cos(2.0943952f),
			Mathf.Cos(3.1415927f),
			Mathf.Cos(4.1887903f),
			Mathf.Cos(5.2359877f)
		};

		// Token: 0x04000004 RID: 4
		private static float[] m_SinAngleArr2 = new float[]
		{
			Mathf.Sin(0f),
			Mathf.Sin(1.0471976f),
			Mathf.Sin(2.0943952f),
			Mathf.Sin(3.1415927f),
			Mathf.Sin(4.1887903f),
			Mathf.Sin(5.2359877f)
		};

		// Token: 0x04000005 RID: 5
		[RuleTile.DontOverride]
		public bool m_FlatTop;

		// Token: 0x04000006 RID: 6
		private static float m_TilemapToWorldYScale = Mathf.Pow(1f - Mathf.Pow(0.5f, 2f), 0.5f);
	}
}

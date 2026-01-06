using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/RuleTile.html")]
	[Serializable]
	public class RuleTile : TileBase
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002471 File Offset: 0x00000671
		public virtual Type m_NeighborType
		{
			get
			{
				return typeof(RuleTile.TilingRuleOutput.Neighbor);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000247D File Offset: 0x0000067D
		public virtual int m_RotationAngle
		{
			get
			{
				return 90;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002481 File Offset: 0x00000681
		public int m_RotationCount
		{
			get
			{
				return 360 / this.m_RotationAngle;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000248F File Offset: 0x0000068F
		public HashSet<Vector3Int> neighborPositions
		{
			get
			{
				if (this.m_NeighborPositions.Count == 0)
				{
					this.UpdateNeighborPositions();
				}
				return this.m_NeighborPositions;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024AC File Offset: 0x000006AC
		public void UpdateNeighborPositions()
		{
			RuleTile.m_CacheTilemapsNeighborPositions.Clear();
			HashSet<Vector3Int> neighborPositions = this.m_NeighborPositions;
			neighborPositions.Clear();
			foreach (RuleTile.TilingRule tilingRule in this.m_TilingRules)
			{
				foreach (KeyValuePair<Vector3Int, int> keyValuePair in tilingRule.GetNeighbors())
				{
					Vector3Int key = keyValuePair.Key;
					neighborPositions.Add(key);
					if (tilingRule.m_RuleTransform == RuleTile.TilingRuleOutput.Transform.Rotated)
					{
						for (int i = this.m_RotationAngle; i < 360; i += this.m_RotationAngle)
						{
							neighborPositions.Add(this.GetRotatedPosition(key, i));
						}
					}
					else if (tilingRule.m_RuleTransform == RuleTile.TilingRuleOutput.Transform.MirrorXY)
					{
						neighborPositions.Add(this.GetMirroredPosition(key, true, true));
						neighborPositions.Add(this.GetMirroredPosition(key, true, false));
						neighborPositions.Add(this.GetMirroredPosition(key, false, true));
					}
					else if (tilingRule.m_RuleTransform == RuleTile.TilingRuleOutput.Transform.MirrorX)
					{
						neighborPositions.Add(this.GetMirroredPosition(key, true, false));
					}
					else if (tilingRule.m_RuleTransform == RuleTile.TilingRuleOutput.Transform.MirrorY)
					{
						neighborPositions.Add(this.GetMirroredPosition(key, false, true));
					}
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002630 File Offset: 0x00000830
		public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject instantiatedGameObject)
		{
			if (instantiatedGameObject != null)
			{
				Tilemap component = tilemap.GetComponent<Tilemap>();
				Matrix4x4 orientationMatrix = component.orientationMatrix;
				Matrix4x4 identity = Matrix4x4.identity;
				Vector3 vector = default(Vector3);
				Quaternion quaternion = default(Quaternion);
				Vector3 vector2 = default(Vector3);
				bool flag = false;
				Matrix4x4 matrix4x = identity;
				foreach (RuleTile.TilingRule tilingRule in this.m_TilingRules)
				{
					if (this.RuleMatches(tilingRule, position, tilemap, ref matrix4x))
					{
						matrix4x = orientationMatrix * matrix4x;
						vector = new Vector3(matrix4x.m03, matrix4x.m13, matrix4x.m23);
						quaternion = Quaternion.LookRotation(new Vector3(matrix4x.m02, matrix4x.m12, matrix4x.m22), new Vector3(matrix4x.m01, matrix4x.m11, matrix4x.m21));
						vector2 = matrix4x.lossyScale;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					vector = new Vector3(orientationMatrix.m03, orientationMatrix.m13, orientationMatrix.m23);
					quaternion = Quaternion.LookRotation(new Vector3(orientationMatrix.m02, orientationMatrix.m12, orientationMatrix.m22), new Vector3(orientationMatrix.m01, orientationMatrix.m11, orientationMatrix.m21));
					vector2 = orientationMatrix.lossyScale;
				}
				instantiatedGameObject.transform.localPosition = vector + component.CellToLocalInterpolated(position + component.tileAnchor);
				instantiatedGameObject.transform.localRotation = quaternion;
				instantiatedGameObject.transform.localScale = vector2;
			}
			return true;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027D8 File Offset: 0x000009D8
		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			Matrix4x4 identity = Matrix4x4.identity;
			tileData.sprite = this.m_DefaultSprite;
			tileData.gameObject = this.m_DefaultGameObject;
			tileData.colliderType = this.m_DefaultColliderType;
			tileData.flags = TileFlags.LockTransform;
			tileData.transform = identity;
			Matrix4x4 matrix4x = identity;
			foreach (RuleTile.TilingRule tilingRule in this.m_TilingRules)
			{
				if (this.RuleMatches(tilingRule, position, tilemap, ref matrix4x))
				{
					switch (tilingRule.m_Output)
					{
					case RuleTile.TilingRuleOutput.OutputSprite.Single:
					case RuleTile.TilingRuleOutput.OutputSprite.Animation:
						tileData.sprite = tilingRule.m_Sprites[0];
						break;
					case RuleTile.TilingRuleOutput.OutputSprite.Random:
					{
						int num = Mathf.Clamp(Mathf.FloorToInt(RuleTile.GetPerlinValue(position, tilingRule.m_PerlinScale, 100000f) * (float)tilingRule.m_Sprites.Length), 0, tilingRule.m_Sprites.Length - 1);
						tileData.sprite = tilingRule.m_Sprites[num];
						if (tilingRule.m_RandomTransform != RuleTile.TilingRuleOutput.Transform.Fixed)
						{
							matrix4x = this.ApplyRandomTransform(tilingRule.m_RandomTransform, matrix4x, tilingRule.m_PerlinScale, position);
						}
						break;
					}
					}
					tileData.transform = matrix4x;
					tileData.gameObject = tilingRule.m_GameObject;
					tileData.colliderType = tilingRule.m_ColliderType;
					break;
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002920 File Offset: 0x00000B20
		public static float GetPerlinValue(Vector3Int position, float scale, float offset)
		{
			return Mathf.PerlinNoise(((float)position.x + offset) * scale, ((float)position.y + offset) * scale);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002940 File Offset: 0x00000B40
		private static bool IsTilemapUsedTilesChange(Tilemap tilemap, out KeyValuePair<HashSet<TileBase>, HashSet<Vector3Int>> hashSet)
		{
			if (!RuleTile.m_CacheTilemapsNeighborPositions.TryGetValue(tilemap, out hashSet))
			{
				return true;
			}
			HashSet<TileBase> key = hashSet.Key;
			int usedTilesCount = tilemap.GetUsedTilesCount();
			if (usedTilesCount != key.Count)
			{
				return true;
			}
			if (RuleTile.m_AllocatedUsedTileArr.Length < usedTilesCount)
			{
				Array.Resize<TileBase>(ref RuleTile.m_AllocatedUsedTileArr, usedTilesCount);
			}
			tilemap.GetUsedTilesNonAlloc(RuleTile.m_AllocatedUsedTileArr);
			for (int i = 0; i < usedTilesCount; i++)
			{
				TileBase tileBase = RuleTile.m_AllocatedUsedTileArr[i];
				if (!key.Contains(tileBase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000029B8 File Offset: 0x00000BB8
		private static KeyValuePair<HashSet<TileBase>, HashSet<Vector3Int>> CachingTilemapNeighborPositions(Tilemap tilemap)
		{
			int usedTilesCount = tilemap.GetUsedTilesCount();
			HashSet<TileBase> hashSet = new HashSet<TileBase>();
			HashSet<Vector3Int> hashSet2 = new HashSet<Vector3Int>();
			if (RuleTile.m_AllocatedUsedTileArr.Length < usedTilesCount)
			{
				Array.Resize<TileBase>(ref RuleTile.m_AllocatedUsedTileArr, usedTilesCount);
			}
			tilemap.GetUsedTilesNonAlloc(RuleTile.m_AllocatedUsedTileArr);
			for (int i = 0; i < usedTilesCount; i++)
			{
				TileBase tileBase = RuleTile.m_AllocatedUsedTileArr[i];
				hashSet.Add(tileBase);
				RuleTile ruleTile = null;
				RuleTile ruleTile2 = tileBase as RuleTile;
				if (ruleTile2 != null)
				{
					ruleTile = ruleTile2;
				}
				else
				{
					RuleOverrideTile ruleOverrideTile = tileBase as RuleOverrideTile;
					if (ruleOverrideTile != null)
					{
						ruleTile = ruleOverrideTile.m_Tile;
					}
				}
				if (ruleTile)
				{
					foreach (Vector3Int vector3Int in ruleTile.neighborPositions)
					{
						hashSet2.Add(vector3Int);
					}
				}
			}
			KeyValuePair<HashSet<TileBase>, HashSet<Vector3Int>> keyValuePair = new KeyValuePair<HashSet<TileBase>, HashSet<Vector3Int>>(hashSet, hashSet2);
			RuleTile.m_CacheTilemapsNeighborPositions[tilemap] = keyValuePair;
			return keyValuePair;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002AB8 File Offset: 0x00000CB8
		private static bool NeedRelease()
		{
			foreach (KeyValuePair<Tilemap, KeyValuePair<HashSet<TileBase>, HashSet<Vector3Int>>> keyValuePair in RuleTile.m_CacheTilemapsNeighborPositions)
			{
				if (keyValuePair.Key == null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002B1C File Offset: 0x00000D1C
		private static void ReleaseDestroyedTilemapCacheData()
		{
			if (!RuleTile.NeedRelease())
			{
				return;
			}
			bool flag = false;
			foreach (Tilemap tilemap in RuleTile.m_CacheTilemapsNeighborPositions.Keys.ToArray<Tilemap>())
			{
				if (tilemap == null && RuleTile.m_CacheTilemapsNeighborPositions.Remove(tilemap))
				{
					flag = true;
				}
			}
			if (flag)
			{
				RuleTile.m_CacheTilemapsNeighborPositions = new Dictionary<Tilemap, KeyValuePair<HashSet<TileBase>, HashSet<Vector3Int>>>(RuleTile.m_CacheTilemapsNeighborPositions);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B80 File Offset: 0x00000D80
		public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
		{
			Matrix4x4 identity = Matrix4x4.identity;
			foreach (RuleTile.TilingRule tilingRule in this.m_TilingRules)
			{
				if (tilingRule.m_Output == RuleTile.TilingRuleOutput.OutputSprite.Animation && this.RuleMatches(tilingRule, position, tilemap, ref identity))
				{
					tileAnimationData.animatedSprites = tilingRule.m_Sprites;
					tileAnimationData.animationSpeed = Random.Range(tilingRule.m_MinAnimationSpeed, tilingRule.m_MaxAnimationSpeed);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002C14 File Offset: 0x00000E14
		public override void RefreshTile(Vector3Int position, ITilemap tilemap)
		{
			base.RefreshTile(position, tilemap);
			Tilemap component = tilemap.GetComponent<Tilemap>();
			RuleTile.ReleaseDestroyedTilemapCacheData();
			KeyValuePair<HashSet<TileBase>, HashSet<Vector3Int>> keyValuePair;
			if (RuleTile.IsTilemapUsedTilesChange(component, out keyValuePair))
			{
				keyValuePair = RuleTile.CachingTilemapNeighborPositions(component);
			}
			foreach (Vector3Int vector3Int in keyValuePair.Value)
			{
				Vector3Int offsetPositionReverse = this.GetOffsetPositionReverse(position, vector3Int);
				TileBase tile = tilemap.GetTile(offsetPositionReverse);
				RuleTile ruleTile = null;
				RuleTile ruleTile2 = tile as RuleTile;
				if (ruleTile2 != null)
				{
					ruleTile = ruleTile2;
				}
				else
				{
					RuleOverrideTile ruleOverrideTile = tile as RuleOverrideTile;
					if (ruleOverrideTile != null)
					{
						ruleTile = ruleOverrideTile.m_Tile;
					}
				}
				if (ruleTile != null && (ruleTile == this || ruleTile.neighborPositions.Contains(vector3Int)))
				{
					base.RefreshTile(offsetPositionReverse, tilemap);
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public virtual bool RuleMatches(RuleTile.TilingRule rule, Vector3Int position, ITilemap tilemap, ref Matrix4x4 transform)
		{
			if (this.RuleMatches(rule, position, tilemap, 0))
			{
				transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 0f), Vector3.one);
				return true;
			}
			if (rule.m_RuleTransform == RuleTile.TilingRuleOutput.Transform.Rotated)
			{
				for (int i = this.m_RotationAngle; i < 360; i += this.m_RotationAngle)
				{
					if (this.RuleMatches(rule, position, tilemap, i))
					{
						transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, (float)(-(float)i)), Vector3.one);
						return true;
					}
				}
			}
			else if (rule.m_RuleTransform == RuleTile.TilingRuleOutput.Transform.MirrorXY)
			{
				if (this.RuleMatches(rule, position, tilemap, true, true))
				{
					transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(-1f, -1f, 1f));
					return true;
				}
				if (this.RuleMatches(rule, position, tilemap, true, false))
				{
					transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(-1f, 1f, 1f));
					return true;
				}
				if (this.RuleMatches(rule, position, tilemap, false, true))
				{
					transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, -1f, 1f));
					return true;
				}
			}
			else if (rule.m_RuleTransform == RuleTile.TilingRuleOutput.Transform.MirrorX)
			{
				if (this.RuleMatches(rule, position, tilemap, true, false))
				{
					transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(-1f, 1f, 1f));
					return true;
				}
			}
			else if (rule.m_RuleTransform == RuleTile.TilingRuleOutput.Transform.MirrorY && this.RuleMatches(rule, position, tilemap, false, true))
			{
				transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, -1f, 1f));
				return true;
			}
			return false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002ED8 File Offset: 0x000010D8
		public virtual Matrix4x4 ApplyRandomTransform(RuleTile.TilingRuleOutput.Transform type, Matrix4x4 original, float perlinScale, Vector3Int position)
		{
			float perlinValue = RuleTile.GetPerlinValue(position, perlinScale, 200000f);
			switch (type)
			{
			case RuleTile.TilingRuleOutput.Transform.Rotated:
			{
				int num = Mathf.Clamp(Mathf.FloorToInt(perlinValue * (float)this.m_RotationCount), 0, this.m_RotationCount - 1) * this.m_RotationAngle;
				return Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, (float)(-(float)num)), Vector3.one);
			}
			case RuleTile.TilingRuleOutput.Transform.MirrorX:
				return original * Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(((double)perlinValue < 0.5) ? 1f : (-1f), 1f, 1f));
			case RuleTile.TilingRuleOutput.Transform.MirrorY:
				return original * Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, ((double)perlinValue < 0.5) ? 1f : (-1f), 1f));
			case RuleTile.TilingRuleOutput.Transform.MirrorXY:
				return original * Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3((Math.Abs((double)perlinValue - 0.5) > 0.25) ? 1f : (-1f), ((double)perlinValue < 0.5) ? 1f : (-1f), 1f));
			default:
				return original;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003034 File Offset: 0x00001234
		public FieldInfo[] GetCustomFields(bool isOverrideInstance)
		{
			return (from field in base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
				where typeof(RuleTile).GetField(field.Name) == null
				where field.IsPublic || field.IsDefined(typeof(SerializeField))
				where !field.IsDefined(typeof(HideInInspector))
				where !isOverrideInstance || !field.IsDefined(typeof(RuleTile.DontOverride))
				select field).ToArray<FieldInfo>();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000030E0 File Offset: 0x000012E0
		public virtual bool RuleMatch(int neighbor, TileBase other)
		{
			RuleOverrideTile ruleOverrideTile = other as RuleOverrideTile;
			if (ruleOverrideTile != null)
			{
				other = ruleOverrideTile.m_InstanceTile;
			}
			if (neighbor != 1)
			{
				return neighbor != 2 || other != this;
			}
			return other == this;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000311C File Offset: 0x0000131C
		public bool RuleMatches(RuleTile.TilingRule rule, Vector3Int position, ITilemap tilemap, int angle)
		{
			int num = Math.Min(rule.m_Neighbors.Count, rule.m_NeighborPositions.Count);
			for (int i = 0; i < num; i++)
			{
				int num2 = rule.m_Neighbors[i];
				Vector3Int rotatedPosition = this.GetRotatedPosition(rule.m_NeighborPositions[i], angle);
				TileBase tile = tilemap.GetTile(this.GetOffsetPosition(position, rotatedPosition));
				if (!this.RuleMatch(num2, tile))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003194 File Offset: 0x00001394
		public bool RuleMatches(RuleTile.TilingRule rule, Vector3Int position, ITilemap tilemap, bool mirrorX, bool mirrorY)
		{
			int num = Math.Min(rule.m_Neighbors.Count, rule.m_NeighborPositions.Count);
			for (int i = 0; i < num; i++)
			{
				int num2 = rule.m_Neighbors[i];
				Vector3Int mirroredPosition = this.GetMirroredPosition(rule.m_NeighborPositions[i], mirrorX, mirrorY);
				TileBase tile = tilemap.GetTile(this.GetOffsetPosition(position, mirroredPosition));
				if (!this.RuleMatch(num2, tile))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000320C File Offset: 0x0000140C
		public virtual Vector3Int GetRotatedPosition(Vector3Int position, int rotation)
		{
			if (rotation <= 90)
			{
				if (rotation == 0)
				{
					return position;
				}
				if (rotation == 90)
				{
					return new Vector3Int(position.y, -position.x, 0);
				}
			}
			else
			{
				if (rotation == 180)
				{
					return new Vector3Int(-position.x, -position.y, 0);
				}
				if (rotation == 270)
				{
					return new Vector3Int(-position.y, position.x, 0);
				}
			}
			return position;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003280 File Offset: 0x00001480
		public virtual Vector3Int GetMirroredPosition(Vector3Int position, bool mirrorX, bool mirrorY)
		{
			if (mirrorX)
			{
				position.x *= -1;
			}
			if (mirrorY)
			{
				position.y *= -1;
			}
			return position;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000032A7 File Offset: 0x000014A7
		public virtual Vector3Int GetOffsetPosition(Vector3Int position, Vector3Int offset)
		{
			return position + offset;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000032B0 File Offset: 0x000014B0
		public virtual Vector3Int GetOffsetPositionReverse(Vector3Int position, Vector3Int offset)
		{
			return position - offset;
		}

		// Token: 0x04000007 RID: 7
		public Sprite m_DefaultSprite;

		// Token: 0x04000008 RID: 8
		public GameObject m_DefaultGameObject;

		// Token: 0x04000009 RID: 9
		public Tile.ColliderType m_DefaultColliderType = Tile.ColliderType.Sprite;

		// Token: 0x0400000A RID: 10
		[HideInInspector]
		public List<RuleTile.TilingRule> m_TilingRules = new List<RuleTile.TilingRule>();

		// Token: 0x0400000B RID: 11
		private HashSet<Vector3Int> m_NeighborPositions = new HashSet<Vector3Int>();

		// Token: 0x0400000C RID: 12
		private static Dictionary<Tilemap, KeyValuePair<HashSet<TileBase>, HashSet<Vector3Int>>> m_CacheTilemapsNeighborPositions = new Dictionary<Tilemap, KeyValuePair<HashSet<TileBase>, HashSet<Vector3Int>>>();

		// Token: 0x0400000D RID: 13
		private static TileBase[] m_AllocatedUsedTileArr = Array.Empty<TileBase>();

		// Token: 0x0200000D RID: 13
		[Serializable]
		public class TilingRuleOutput
		{
			// Token: 0x04000030 RID: 48
			public int m_Id;

			// Token: 0x04000031 RID: 49
			public Sprite[] m_Sprites = new Sprite[1];

			// Token: 0x04000032 RID: 50
			public GameObject m_GameObject;

			// Token: 0x04000033 RID: 51
			[FormerlySerializedAs("m_AnimationSpeed")]
			public float m_MinAnimationSpeed = 1f;

			// Token: 0x04000034 RID: 52
			[FormerlySerializedAs("m_AnimationSpeed")]
			public float m_MaxAnimationSpeed = 1f;

			// Token: 0x04000035 RID: 53
			public float m_PerlinScale = 0.5f;

			// Token: 0x04000036 RID: 54
			public RuleTile.TilingRuleOutput.OutputSprite m_Output;

			// Token: 0x04000037 RID: 55
			public Tile.ColliderType m_ColliderType = Tile.ColliderType.Sprite;

			// Token: 0x04000038 RID: 56
			public RuleTile.TilingRuleOutput.Transform m_RandomTransform;

			// Token: 0x0200001D RID: 29
			public class Neighbor
			{
				// Token: 0x04000052 RID: 82
				public const int This = 1;

				// Token: 0x04000053 RID: 83
				public const int NotThis = 2;
			}

			// Token: 0x0200001E RID: 30
			public enum Transform
			{
				// Token: 0x04000055 RID: 85
				Fixed,
				// Token: 0x04000056 RID: 86
				Rotated,
				// Token: 0x04000057 RID: 87
				MirrorX,
				// Token: 0x04000058 RID: 88
				MirrorY,
				// Token: 0x04000059 RID: 89
				MirrorXY
			}

			// Token: 0x0200001F RID: 31
			public enum OutputSprite
			{
				// Token: 0x0400005B RID: 91
				Single,
				// Token: 0x0400005C RID: 92
				Random,
				// Token: 0x0400005D RID: 93
				Animation
			}
		}

		// Token: 0x0200000E RID: 14
		[Serializable]
		public class TilingRule : RuleTile.TilingRuleOutput
		{
			// Token: 0x0600005B RID: 91 RVA: 0x00004888 File Offset: 0x00002A88
			public RuleTile.TilingRule Clone()
			{
				RuleTile.TilingRule tilingRule = new RuleTile.TilingRule
				{
					m_Neighbors = new List<int>(this.m_Neighbors),
					m_NeighborPositions = new List<Vector3Int>(this.m_NeighborPositions),
					m_RuleTransform = this.m_RuleTransform,
					m_Sprites = new Sprite[this.m_Sprites.Length],
					m_GameObject = this.m_GameObject,
					m_MinAnimationSpeed = this.m_MinAnimationSpeed,
					m_MaxAnimationSpeed = this.m_MaxAnimationSpeed,
					m_PerlinScale = this.m_PerlinScale,
					m_Output = this.m_Output,
					m_ColliderType = this.m_ColliderType,
					m_RandomTransform = this.m_RandomTransform
				};
				Array.Copy(this.m_Sprites, tilingRule.m_Sprites, this.m_Sprites.Length);
				return tilingRule;
			}

			// Token: 0x0600005C RID: 92 RVA: 0x0000494C File Offset: 0x00002B4C
			public Dictionary<Vector3Int, int> GetNeighbors()
			{
				Dictionary<Vector3Int, int> dictionary = new Dictionary<Vector3Int, int>();
				int num = 0;
				while (num < this.m_Neighbors.Count && num < this.m_NeighborPositions.Count)
				{
					dictionary.Add(this.m_NeighborPositions[num], this.m_Neighbors[num]);
					num++;
				}
				return dictionary;
			}

			// Token: 0x0600005D RID: 93 RVA: 0x000049A2 File Offset: 0x00002BA2
			public void ApplyNeighbors(Dictionary<Vector3Int, int> dict)
			{
				this.m_NeighborPositions = dict.Keys.ToList<Vector3Int>();
				this.m_Neighbors = dict.Values.ToList<int>();
			}

			// Token: 0x0600005E RID: 94 RVA: 0x000049C8 File Offset: 0x00002BC8
			public BoundsInt GetBounds()
			{
				BoundsInt boundsInt = new BoundsInt(Vector3Int.zero, Vector3Int.one);
				foreach (KeyValuePair<Vector3Int, int> keyValuePair in this.GetNeighbors())
				{
					boundsInt.xMin = Mathf.Min(boundsInt.xMin, keyValuePair.Key.x);
					boundsInt.yMin = Mathf.Min(boundsInt.yMin, keyValuePair.Key.y);
					boundsInt.xMax = Mathf.Max(boundsInt.xMax, keyValuePair.Key.x + 1);
					boundsInt.yMax = Mathf.Max(boundsInt.yMax, keyValuePair.Key.y + 1);
				}
				return boundsInt;
			}

			// Token: 0x04000039 RID: 57
			public List<int> m_Neighbors = new List<int>();

			// Token: 0x0400003A RID: 58
			public List<Vector3Int> m_NeighborPositions = new List<Vector3Int>
			{
				new Vector3Int(-1, 1, 0),
				new Vector3Int(0, 1, 0),
				new Vector3Int(1, 1, 0),
				new Vector3Int(-1, 0, 0),
				new Vector3Int(1, 0, 0),
				new Vector3Int(-1, -1, 0),
				new Vector3Int(0, -1, 0),
				new Vector3Int(1, -1, 0)
			};

			// Token: 0x0400003B RID: 59
			public RuleTile.TilingRuleOutput.Transform m_RuleTransform;
		}

		// Token: 0x0200000F RID: 15
		public class DontOverride : Attribute
		{
		}
	}
}

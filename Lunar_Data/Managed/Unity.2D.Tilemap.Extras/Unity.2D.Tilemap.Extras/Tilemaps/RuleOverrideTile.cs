using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000C RID: 12
	[MovedFrom(true, "UnityEngine", null, null)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/RuleOverrideTile.html")]
	[Serializable]
	public class RuleOverrideTile : TileBase
	{
		// Token: 0x1700000B RID: 11
		public Sprite this[Sprite originalSprite]
		{
			get
			{
				foreach (RuleOverrideTile.TileSpritePair tileSpritePair in this.m_Sprites)
				{
					if (tileSpritePair.m_OriginalSprite == originalSprite)
					{
						return tileSpritePair.m_OverrideSprite;
					}
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.m_Sprites = this.m_Sprites.Where((RuleOverrideTile.TileSpritePair spritePair) => spritePair.m_OriginalSprite != originalSprite).ToList<RuleOverrideTile.TileSpritePair>();
					return;
				}
				foreach (RuleOverrideTile.TileSpritePair tileSpritePair in this.m_Sprites)
				{
					if (tileSpritePair.m_OriginalSprite == originalSprite)
					{
						tileSpritePair.m_OverrideSprite = value;
						return;
					}
				}
				this.m_Sprites.Add(new RuleOverrideTile.TileSpritePair
				{
					m_OriginalSprite = originalSprite,
					m_OverrideSprite = value
				});
			}
		}

		// Token: 0x1700000C RID: 12
		public GameObject this[GameObject originalGameObject]
		{
			get
			{
				foreach (RuleOverrideTile.TileGameObjectPair tileGameObjectPair in this.m_GameObjects)
				{
					if (tileGameObjectPair.m_OriginalGameObject == originalGameObject)
					{
						return tileGameObjectPair.m_OverrideGameObject;
					}
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.m_GameObjects = this.m_GameObjects.Where((RuleOverrideTile.TileGameObjectPair gameObjectPair) => gameObjectPair.m_OriginalGameObject != originalGameObject).ToList<RuleOverrideTile.TileGameObjectPair>();
					return;
				}
				foreach (RuleOverrideTile.TileGameObjectPair tileGameObjectPair in this.m_GameObjects)
				{
					if (tileGameObjectPair.m_OriginalGameObject == originalGameObject)
					{
						tileGameObjectPair.m_OverrideGameObject = value;
						return;
					}
				}
				this.m_GameObjects.Add(new RuleOverrideTile.TileGameObjectPair
				{
					m_OriginalGameObject = originalGameObject,
					m_OverrideGameObject = value
				});
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000041E8 File Offset: 0x000023E8
		private void CreateInstanceTile()
		{
			RuleTile ruleTile = ScriptableObject.CreateInstance(this.m_Tile.GetType()) as RuleTile;
			ruleTile.hideFlags = HideFlags.NotEditable;
			ruleTile.name = this.m_Tile.name + " (Override)";
			this.m_InstanceTile = ruleTile;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004234 File Offset: 0x00002434
		public void ApplyOverrides(IList<KeyValuePair<Sprite, Sprite>> overrides)
		{
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			for (int i = 0; i < overrides.Count; i++)
			{
				this[overrides[i].Key] = overrides[i].Value;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004284 File Offset: 0x00002484
		public void ApplyOverrides(IList<KeyValuePair<GameObject, GameObject>> overrides)
		{
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			for (int i = 0; i < overrides.Count; i++)
			{
				this[overrides[i].Key] = overrides[i].Value;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000042D4 File Offset: 0x000024D4
		public void GetOverrides(List<KeyValuePair<Sprite, Sprite>> overrides, ref int validCount)
		{
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			overrides.Clear();
			List<Sprite> list = new List<Sprite>();
			if (this.m_Tile)
			{
				if (this.m_Tile.m_DefaultSprite)
				{
					list.Add(this.m_Tile.m_DefaultSprite);
				}
				foreach (RuleTile.TilingRule tilingRule in this.m_Tile.m_TilingRules)
				{
					foreach (Sprite sprite in tilingRule.m_Sprites)
					{
						if (sprite && !list.Contains(sprite))
						{
							list.Add(sprite);
						}
					}
				}
			}
			validCount = list.Count;
			foreach (RuleOverrideTile.TileSpritePair tileSpritePair in this.m_Sprites)
			{
				if (!list.Contains(tileSpritePair.m_OriginalSprite))
				{
					list.Add(tileSpritePair.m_OriginalSprite);
				}
			}
			foreach (Sprite sprite2 in list)
			{
				overrides.Add(new KeyValuePair<Sprite, Sprite>(sprite2, this[sprite2]));
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004454 File Offset: 0x00002654
		public void GetOverrides(List<KeyValuePair<GameObject, GameObject>> overrides, ref int validCount)
		{
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			overrides.Clear();
			List<GameObject> list = new List<GameObject>();
			if (this.m_Tile)
			{
				if (this.m_Tile.m_DefaultGameObject)
				{
					list.Add(this.m_Tile.m_DefaultGameObject);
				}
				foreach (RuleTile.TilingRule tilingRule in this.m_Tile.m_TilingRules)
				{
					if (tilingRule.m_GameObject && !list.Contains(tilingRule.m_GameObject))
					{
						list.Add(tilingRule.m_GameObject);
					}
				}
			}
			validCount = list.Count;
			foreach (RuleOverrideTile.TileGameObjectPair tileGameObjectPair in this.m_GameObjects)
			{
				if (!list.Contains(tileGameObjectPair.m_OriginalGameObject))
				{
					list.Add(tileGameObjectPair.m_OriginalGameObject);
				}
			}
			foreach (GameObject gameObject in list)
			{
				overrides.Add(new KeyValuePair<GameObject, GameObject>(gameObject, this[gameObject]));
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000045C4 File Offset: 0x000027C4
		public virtual void Override()
		{
			if (!this.m_Tile)
			{
				return;
			}
			if (!this.m_InstanceTile)
			{
				this.CreateInstanceTile();
			}
			this.PrepareOverride();
			RuleTile instanceTile = this.m_InstanceTile;
			instanceTile.m_DefaultSprite = this[instanceTile.m_DefaultSprite] ?? instanceTile.m_DefaultSprite;
			instanceTile.m_DefaultGameObject = this[instanceTile.m_DefaultGameObject] ?? instanceTile.m_DefaultGameObject;
			foreach (RuleTile.TilingRule tilingRule in instanceTile.m_TilingRules)
			{
				for (int i = 0; i < tilingRule.m_Sprites.Length; i++)
				{
					Sprite sprite = tilingRule.m_Sprites[i];
					tilingRule.m_Sprites[i] = this[sprite] ?? sprite;
				}
				tilingRule.m_GameObject = this[tilingRule.m_GameObject] ?? tilingRule.m_GameObject;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000046C8 File Offset: 0x000028C8
		public void PrepareOverride()
		{
			RuleTile tempTile = Object.Instantiate<RuleTile>(this.m_InstanceTile);
			Dictionary<FieldInfo, object> dictionary = this.m_InstanceTile.GetCustomFields(true).ToDictionary((FieldInfo field) => field, (FieldInfo field) => field.GetValue(tempTile));
			JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(this.m_Tile), this.m_InstanceTile);
			foreach (KeyValuePair<FieldInfo, object> keyValuePair in dictionary)
			{
				keyValuePair.Key.SetValue(this.m_InstanceTile, keyValuePair.Value);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004790 File Offset: 0x00002990
		public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
		{
			return this.m_InstanceTile && this.m_InstanceTile.GetTileAnimationData(position, tilemap, ref tileAnimationData);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000047AF File Offset: 0x000029AF
		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			if (!this.m_InstanceTile)
			{
				return;
			}
			this.m_InstanceTile.GetTileData(position, tilemap, ref tileData);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000047CD File Offset: 0x000029CD
		public override void RefreshTile(Vector3Int position, ITilemap tilemap)
		{
			if (!this.m_InstanceTile)
			{
				return;
			}
			this.m_InstanceTile.RefreshTile(position, tilemap);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000047EA File Offset: 0x000029EA
		public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
		{
			return !this.m_InstanceTile || this.m_InstanceTile.StartUp(position, tilemap, go);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004809 File Offset: 0x00002A09
		public void OnEnable()
		{
			if (this.m_Tile == null)
			{
				return;
			}
			if (this.m_InstanceTile == null)
			{
				this.Override();
			}
		}

		// Token: 0x0400002C RID: 44
		public RuleTile m_Tile;

		// Token: 0x0400002D RID: 45
		public List<RuleOverrideTile.TileSpritePair> m_Sprites = new List<RuleOverrideTile.TileSpritePair>();

		// Token: 0x0400002E RID: 46
		public List<RuleOverrideTile.TileGameObjectPair> m_GameObjects = new List<RuleOverrideTile.TileGameObjectPair>();

		// Token: 0x0400002F RID: 47
		[HideInInspector]
		public RuleTile m_InstanceTile;

		// Token: 0x02000017 RID: 23
		[Serializable]
		public class TileSpritePair
		{
			// Token: 0x04000049 RID: 73
			public Sprite m_OriginalSprite;

			// Token: 0x0400004A RID: 74
			public Sprite m_OverrideSprite;
		}

		// Token: 0x02000018 RID: 24
		[Serializable]
		public class TileGameObjectPair
		{
			// Token: 0x0400004B RID: 75
			public GameObject m_OriginalGameObject;

			// Token: 0x0400004C RID: 76
			public GameObject m_OverrideGameObject;
		}
	}
}

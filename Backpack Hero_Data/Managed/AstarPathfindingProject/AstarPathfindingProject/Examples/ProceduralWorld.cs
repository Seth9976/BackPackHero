using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Examples
{
	// Token: 0x020000DD RID: 221
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_examples_1_1_procedural_world.php")]
	public class ProceduralWorld : MonoBehaviour
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x0003E902 File Offset: 0x0003CB02
		private void Start()
		{
			this.Update();
			AstarPath.active.Scan(null);
			base.StartCoroutine(this.GenerateTiles());
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0003E924 File Offset: 0x0003CB24
		private void Update()
		{
			Int2 @int = new Int2(Mathf.RoundToInt((this.target.position.x - this.tileSize * 0.5f) / this.tileSize), Mathf.RoundToInt((this.target.position.z - this.tileSize * 0.5f) / this.tileSize));
			this.range = ((this.range < 1) ? 1 : this.range);
			bool flag = true;
			while (flag)
			{
				flag = false;
				foreach (KeyValuePair<Int2, ProceduralWorld.ProceduralTile> keyValuePair in this.tiles)
				{
					if (Mathf.Abs(keyValuePair.Key.x - @int.x) > this.range || Mathf.Abs(keyValuePair.Key.y - @int.y) > this.range)
					{
						keyValuePair.Value.Destroy();
						this.tiles.Remove(keyValuePair.Key);
						flag = true;
						break;
					}
				}
			}
			for (int i = @int.x - this.range; i <= @int.x + this.range; i++)
			{
				for (int j = @int.y - this.range; j <= @int.y + this.range; j++)
				{
					if (!this.tiles.ContainsKey(new Int2(i, j)))
					{
						ProceduralWorld.ProceduralTile proceduralTile = new ProceduralWorld.ProceduralTile(this, i, j);
						IEnumerator enumerator2 = proceduralTile.Generate();
						enumerator2.MoveNext();
						this.tileGenerationQueue.Enqueue(enumerator2);
						this.tiles.Add(new Int2(i, j), proceduralTile);
					}
				}
			}
			for (int k = @int.x - this.disableAsyncLoadWithinRange; k <= @int.x + this.disableAsyncLoadWithinRange; k++)
			{
				for (int l = @int.y - this.disableAsyncLoadWithinRange; l <= @int.y + this.disableAsyncLoadWithinRange; l++)
				{
					this.tiles[new Int2(k, l)].ForceFinish();
				}
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0003EB68 File Offset: 0x0003CD68
		private IEnumerator GenerateTiles()
		{
			for (;;)
			{
				if (this.tileGenerationQueue.Count > 0)
				{
					IEnumerator enumerator = this.tileGenerationQueue.Dequeue();
					yield return base.StartCoroutine(enumerator);
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x040005B4 RID: 1460
		public Transform target;

		// Token: 0x040005B5 RID: 1461
		public ProceduralWorld.ProceduralPrefab[] prefabs;

		// Token: 0x040005B6 RID: 1462
		public int range = 1;

		// Token: 0x040005B7 RID: 1463
		public int disableAsyncLoadWithinRange = 1;

		// Token: 0x040005B8 RID: 1464
		public float tileSize = 100f;

		// Token: 0x040005B9 RID: 1465
		public int subTiles = 20;

		// Token: 0x040005BA RID: 1466
		public bool staticBatching;

		// Token: 0x040005BB RID: 1467
		private Queue<IEnumerator> tileGenerationQueue = new Queue<IEnumerator>();

		// Token: 0x040005BC RID: 1468
		private Dictionary<Int2, ProceduralWorld.ProceduralTile> tiles = new Dictionary<Int2, ProceduralWorld.ProceduralTile>();

		// Token: 0x02000167 RID: 359
		public enum RotationRandomness
		{
			// Token: 0x040007FF RID: 2047
			AllAxes,
			// Token: 0x04000800 RID: 2048
			Y
		}

		// Token: 0x02000168 RID: 360
		[Serializable]
		public class ProceduralPrefab
		{
			// Token: 0x04000801 RID: 2049
			public GameObject prefab;

			// Token: 0x04000802 RID: 2050
			public float density;

			// Token: 0x04000803 RID: 2051
			public float perlin;

			// Token: 0x04000804 RID: 2052
			public float perlinPower = 1f;

			// Token: 0x04000805 RID: 2053
			public Vector2 perlinOffset = Vector2.zero;

			// Token: 0x04000806 RID: 2054
			public float perlinScale = 1f;

			// Token: 0x04000807 RID: 2055
			public float random = 1f;

			// Token: 0x04000808 RID: 2056
			public ProceduralWorld.RotationRandomness randomRotation;

			// Token: 0x04000809 RID: 2057
			public bool singleFixed;
		}

		// Token: 0x02000169 RID: 361
		private class ProceduralTile
		{
			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000B4A RID: 2890 RVA: 0x000475BA File Offset: 0x000457BA
			// (set) Token: 0x06000B4B RID: 2891 RVA: 0x000475C2 File Offset: 0x000457C2
			public bool destroyed { get; private set; }

			// Token: 0x06000B4C RID: 2892 RVA: 0x000475CB File Offset: 0x000457CB
			public ProceduralTile(ProceduralWorld world, int x, int z)
			{
				this.x = x;
				this.z = z;
				this.world = world;
				this.rnd = new Random((x * 10007) ^ (z * 36007));
			}

			// Token: 0x06000B4D RID: 2893 RVA: 0x00047602 File Offset: 0x00045802
			public IEnumerator Generate()
			{
				this.ie = this.InternalGenerate();
				GameObject gameObject = new GameObject("Tile " + this.x.ToString() + " " + this.z.ToString());
				this.root = gameObject.transform;
				while (this.ie != null && this.root != null && this.ie.MoveNext())
				{
					yield return this.ie.Current;
				}
				this.ie = null;
				yield break;
			}

			// Token: 0x06000B4E RID: 2894 RVA: 0x00047611 File Offset: 0x00045811
			public void ForceFinish()
			{
				while (this.ie != null && this.root != null && this.ie.MoveNext())
				{
				}
				this.ie = null;
			}

			// Token: 0x06000B4F RID: 2895 RVA: 0x00047640 File Offset: 0x00045840
			private Vector3 RandomInside()
			{
				return new Vector3
				{
					x = ((float)this.x + (float)this.rnd.NextDouble()) * this.world.tileSize,
					z = ((float)this.z + (float)this.rnd.NextDouble()) * this.world.tileSize
				};
			}

			// Token: 0x06000B50 RID: 2896 RVA: 0x000476A4 File Offset: 0x000458A4
			private Vector3 RandomInside(float px, float pz)
			{
				return new Vector3
				{
					x = (px + (float)this.rnd.NextDouble() / (float)this.world.subTiles) * this.world.tileSize,
					z = (pz + (float)this.rnd.NextDouble() / (float)this.world.subTiles) * this.world.tileSize
				};
			}

			// Token: 0x06000B51 RID: 2897 RVA: 0x00047718 File Offset: 0x00045918
			private Quaternion RandomYRot(ProceduralWorld.ProceduralPrefab prefab)
			{
				if (prefab.randomRotation != ProceduralWorld.RotationRandomness.AllAxes)
				{
					return Quaternion.Euler(0f, 360f * (float)this.rnd.NextDouble(), 0f);
				}
				return Quaternion.Euler(360f * (float)this.rnd.NextDouble(), 360f * (float)this.rnd.NextDouble(), 360f * (float)this.rnd.NextDouble());
			}

			// Token: 0x06000B52 RID: 2898 RVA: 0x0004778A File Offset: 0x0004598A
			private IEnumerator InternalGenerate()
			{
				Debug.Log("Generating tile " + this.x.ToString() + ", " + this.z.ToString());
				int counter = 0;
				float[,] ditherMap = new float[this.world.subTiles + 2, this.world.subTiles + 2];
				int num4;
				for (int i = 0; i < this.world.prefabs.Length; i = num4 + 1)
				{
					ProceduralWorld.ProceduralPrefab pref = this.world.prefabs[i];
					if (pref.singleFixed)
					{
						Vector3 vector = new Vector3(((float)this.x + 0.5f) * this.world.tileSize, 0f, ((float)this.z + 0.5f) * this.world.tileSize);
						Object.Instantiate<GameObject>(pref.prefab, vector, Quaternion.identity).transform.parent = this.root;
					}
					else
					{
						float subSize = this.world.tileSize / (float)this.world.subTiles;
						for (int k = 0; k < this.world.subTiles; k++)
						{
							for (int l = 0; l < this.world.subTiles; l++)
							{
								ditherMap[k + 1, l + 1] = 0f;
							}
						}
						for (int sx = 0; sx < this.world.subTiles; sx = num4 + 1)
						{
							for (int sz = 0; sz < this.world.subTiles; sz = num4 + 1)
							{
								float px = (float)this.x + (float)sx / (float)this.world.subTiles;
								float pz = (float)this.z + (float)sz / (float)this.world.subTiles;
								float num = Mathf.Pow(Mathf.PerlinNoise((px + pref.perlinOffset.x) * pref.perlinScale, (pz + pref.perlinOffset.y) * pref.perlinScale), pref.perlinPower);
								float num2 = pref.density * Mathf.Lerp(1f, num, pref.perlin) * Mathf.Lerp(1f, (float)this.rnd.NextDouble(), pref.random);
								float num3 = subSize * subSize * num2 + ditherMap[sx + 1, sz + 1];
								int count = Mathf.RoundToInt(num3);
								ditherMap[sx + 1 + 1, sz + 1] += 0.4375f * (num3 - (float)count);
								ditherMap[sx + 1 - 1, sz + 1 + 1] += 0.1875f * (num3 - (float)count);
								ditherMap[sx + 1, sz + 1 + 1] += 0.3125f * (num3 - (float)count);
								ditherMap[sx + 1 + 1, sz + 1 + 1] += 0.0625f * (num3 - (float)count);
								for (int j = 0; j < count; j = num4 + 1)
								{
									Vector3 vector2 = this.RandomInside(px, pz);
									Object.Instantiate<GameObject>(pref.prefab, vector2, this.RandomYRot(pref)).transform.parent = this.root;
									num4 = counter;
									counter = num4 + 1;
									if (counter % 2 == 0)
									{
										yield return null;
									}
									num4 = j;
								}
								num4 = sz;
							}
							num4 = sx;
						}
					}
					pref = null;
					num4 = i;
				}
				ditherMap = null;
				yield return null;
				yield return null;
				if (Application.HasProLicense() && this.world.staticBatching)
				{
					StaticBatchingUtility.Combine(this.root.gameObject);
				}
				yield break;
			}

			// Token: 0x06000B53 RID: 2899 RVA: 0x0004779C File Offset: 0x0004599C
			public void Destroy()
			{
				if (this.root != null)
				{
					Debug.Log("Destroying tile " + this.x.ToString() + ", " + this.z.ToString());
					Object.Destroy(this.root.gameObject);
					this.root = null;
				}
				this.ie = null;
			}

			// Token: 0x0400080A RID: 2058
			private int x;

			// Token: 0x0400080B RID: 2059
			private int z;

			// Token: 0x0400080C RID: 2060
			private Random rnd;

			// Token: 0x0400080D RID: 2061
			private ProceduralWorld world;

			// Token: 0x0400080F RID: 2063
			private Transform root;

			// Token: 0x04000810 RID: 2064
			private IEnumerator ie;
		}
	}
}

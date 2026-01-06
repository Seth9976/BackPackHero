using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using AOT;
using Pathfinding.Drawing.Text;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;

namespace Pathfinding.Drawing
{
	// Token: 0x02000029 RID: 41
	public class DrawingData
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000084A9 File Offset: 0x000066A9
		private int adjustedSceneModeVersion
		{
			get
			{
				return this.sceneModeVersion + (Application.isPlaying ? 1000 : 0);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000084C1 File Offset: 0x000066C1
		internal int GetNextDrawOrderIndex()
		{
			this.currentDrawOrderIndex++;
			return this.currentDrawOrderIndex;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000084D7 File Offset: 0x000066D7
		internal void PoolMesh(Mesh mesh)
		{
			this.stagingCachedMeshes.Add(mesh);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000084E5 File Offset: 0x000066E5
		private void SortPooledMeshes()
		{
			this.cachedMeshes.Sort((Mesh a, Mesh b) => b.vertexCount - a.vertexCount);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008514 File Offset: 0x00006714
		internal Mesh GetMesh(int desiredVertexCount)
		{
			if (this.cachedMeshes.Count > 0)
			{
				int num = 0;
				int i = this.cachedMeshes.Count;
				while (i > num + 1)
				{
					int num2 = (num + i) / 2;
					if (this.cachedMeshes[num2].vertexCount < desiredVertexCount)
					{
						i = num2;
					}
					else
					{
						num = num2;
					}
				}
				Mesh mesh = this.cachedMeshes[num];
				if (num == 0)
				{
					this.lastTimeLargestCachedMeshWasUsed = this.version;
				}
				this.cachedMeshes.RemoveAt(num);
				return mesh;
			}
			Mesh mesh2 = new Mesh();
			mesh2.hideFlags = HideFlags.DontSave;
			mesh2.MarkDynamic();
			return mesh2;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000085A0 File Offset: 0x000067A0
		internal void LoadFontDataIfNecessary()
		{
			if (this.fontData.material == null)
			{
				SDFFont sdffont = DefaultFonts.LoadDefaultFont();
				this.fontData.Dispose();
				this.fontData = new SDFLookupData(sdffont);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000085DD File Offset: 0x000067DD
		private static float CurrentTime
		{
			get
			{
				if (!Application.isPlaying)
				{
					return Time.realtimeSinceStartup;
				}
				return Time.time;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000085F1 File Offset: 0x000067F1
		private unsafe static void UpdateTime()
		{
			*SharedDrawingData.BurstTime.Data = DrawingData.CurrentTime;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008604 File Offset: 0x00006804
		public CommandBuilder GetBuilder(bool renderInGame = false)
		{
			DrawingData.UpdateTime();
			return new CommandBuilder(this, DrawingData.Hasher.NotSupplied, this.frameRedrawScope, default(RedrawScope), !renderInGame, false, this.adjustedSceneModeVersion);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000863C File Offset: 0x0000683C
		internal CommandBuilder GetBuiltInBuilder(bool renderInGame = false)
		{
			DrawingData.UpdateTime();
			return new CommandBuilder(this, DrawingData.Hasher.NotSupplied, this.frameRedrawScope, default(RedrawScope), !renderInGame, true, this.adjustedSceneModeVersion);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008673 File Offset: 0x00006873
		public CommandBuilder GetBuilder(RedrawScope redrawScope, bool renderInGame = false)
		{
			DrawingData.UpdateTime();
			return new CommandBuilder(this, DrawingData.Hasher.NotSupplied, this.frameRedrawScope, redrawScope, !renderInGame, false, this.adjustedSceneModeVersion);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008697 File Offset: 0x00006897
		public CommandBuilder GetBuilder(DrawingData.Hasher hasher, RedrawScope redrawScope = default(RedrawScope), bool renderInGame = false)
		{
			if (!hasher.Equals(DrawingData.Hasher.NotSupplied))
			{
				this.DiscardData(hasher);
			}
			DrawingData.UpdateTime();
			return new CommandBuilder(this, hasher, this.frameRedrawScope, redrawScope, !renderInGame, false, this.adjustedSceneModeVersion);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000086CC File Offset: 0x000068CC
		public DrawingSettings.Settings settingsRef
		{
			get
			{
				if (this.settings == null)
				{
					this.settings = DrawingSettings.DefaultSettings;
				}
				return this.settings;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000086E7 File Offset: 0x000068E7
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000086EF File Offset: 0x000068EF
		public int version { get; private set; } = 1;

		// Token: 0x060001CC RID: 460 RVA: 0x000086F8 File Offset: 0x000068F8
		public GameObject GetAssociatedGameObject(RedrawScope scope)
		{
			GameObject gameObject;
			if (this.persistentRedrawScopes.TryGetValue(scope.id, out gameObject))
			{
				return gameObject;
			}
			return null;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000871D File Offset: 0x0000691D
		private void DiscardData(DrawingData.Hasher hasher)
		{
			this.processedData.ReleaseAllWithHash(this, hasher);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000872C File Offset: 0x0000692C
		internal void OnChangingPlayMode()
		{
			this.sceneModeVersion++;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000873C File Offset: 0x0000693C
		public bool Draw(DrawingData.Hasher hasher)
		{
			if (hasher.Equals(DrawingData.Hasher.NotSupplied))
			{
				throw new ArgumentException("Invalid hash value");
			}
			return this.processedData.SetVersion(hasher, this.version);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008769 File Offset: 0x00006969
		public bool Draw(DrawingData.Hasher hasher, RedrawScope scope)
		{
			if (hasher.Equals(DrawingData.Hasher.NotSupplied))
			{
				throw new ArgumentException("Invalid hash value");
			}
			this.processedData.SetCustomScope(hasher, scope);
			return this.processedData.SetVersion(hasher, this.version);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000087A4 File Offset: 0x000069A4
		internal void Draw(RedrawScope scope)
		{
			if (scope.isValid)
			{
				this.processedData.SetVersion(scope, this.version);
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000087C2 File Offset: 0x000069C2
		internal void DrawUntilDisposed(RedrawScope scope, GameObject associatedGameObject)
		{
			if (scope.isValid)
			{
				this.Draw(scope);
				this.persistentRedrawScopes.Add(scope.id, associatedGameObject);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000087E6 File Offset: 0x000069E6
		internal void DisposeRedrawScope(RedrawScope scope)
		{
			if (scope.isValid)
			{
				this.processedData.SetVersion(scope, -1);
				this.persistentRedrawScopes.Remove(scope.id);
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008814 File Offset: 0x00006A14
		private void RefreshRedrawScopes()
		{
			foreach (KeyValuePair<int, GameObject> keyValuePair in this.persistentRedrawScopes)
			{
				this.processedData.SetVersion(new RedrawScope(this, keyValuePair.Key), this.version);
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008880 File Offset: 0x00006A80
		public void TickFramePreRender()
		{
			this.data.DisposeCommandBuildersWithJobDependencies(this);
			this.processedData.FilterOldPersistentCommands(this.version, this.lastTickVersion, DrawingData.CurrentTime, this.adjustedSceneModeVersion);
			this.RefreshRedrawScopes();
			this.processedData.ReleaseDataOlderThan(this, this.lastTickVersion2 + 1);
			this.lastTickVersion2 = this.lastTickVersion;
			this.lastTickVersion = this.version;
			this.currentDrawOrderIndex = 0;
			this.cachedMeshes.AddRange(this.stagingCachedMeshes);
			this.stagingCachedMeshes.Clear();
			this.SortPooledMeshes();
			if (this.version - this.lastTimeLargestCachedMeshWasUsed > 60 && this.cachedMeshes.Count > 0)
			{
				Object.DestroyImmediate(this.cachedMeshes[0]);
				this.cachedMeshes.RemoveAt(0);
				this.lastTimeLargestCachedMeshWasUsed = this.version;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008960 File Offset: 0x00006B60
		public void PostRenderCleanup()
		{
			this.data.ReleaseAllUnused();
			int version = this.version;
			this.version = version + 1;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00008988 File Offset: 0x00006B88
		private int totalMemoryUsage
		{
			get
			{
				return this.data.memoryUsage + this.processedData.memoryUsage;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000089A4 File Offset: 0x00006BA4
		private void LoadMaterials()
		{
			if (this.surfaceMaterial == null)
			{
				this.surfaceMaterial = Resources.Load<Material>("aline_surface");
			}
			if (this.lineMaterial == null)
			{
				this.lineMaterial = Resources.Load<Material>("aline_outline");
			}
			if (this.fontData.material == null)
			{
				SDFFont sdffont = DefaultFonts.LoadDefaultFont();
				this.fontData.Dispose();
				this.fontData = new SDFLookupData(sdffont);
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008A20 File Offset: 0x00006C20
		public DrawingData()
		{
			this.gizmosHandle = GCHandle.Alloc(this, GCHandleType.Weak);
			this.LoadMaterials();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008A9B File Offset: 0x00006C9B
		private static int CeilLog2(int x)
		{
			return (int)math.ceil(math.log2((float)x));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008AAC File Offset: 0x00006CAC
		public void Render(Camera cam, bool allowGizmos, DrawingData.CommandBufferWrapper commandBuffer, bool allowCameraDefault)
		{
			this.LoadMaterials();
			if (this.surfaceMaterial == null || this.lineMaterial == null)
			{
				return;
			}
			Plane[] array = this.frustrumPlanes;
			GeometryUtility.CalculateFrustumPlanes(cam, array);
			DrawingData.Range range;
			if (!this.cameraVersions.TryGetValue(cam, out range))
			{
				range = new DrawingData.Range
				{
					start = int.MinValue,
					end = int.MinValue
				};
			}
			if (range.end > this.lastTickVersion)
			{
				range.end = this.version + 1;
			}
			else
			{
				range = new DrawingData.Range
				{
					start = range.end,
					end = this.version + 1
				};
			}
			range.start = Mathf.Max(range.start, this.lastTickVersion2 + 1);
			DrawingSettings.Settings settingsRef = this.settingsRef;
			if (!GL.wireframe)
			{
				this.processedData.SubmitMeshes(this, cam, range.start, allowGizmos, allowCameraDefault);
				this.meshes.Clear();
				this.processedData.CollectMeshes(range.start, this.meshes, cam, allowGizmos, allowCameraDefault);
				this.processedData.PoolDynamicMeshes(this);
				this.meshes.Sort(DrawingData.meshSorter);
				int num = Shader.PropertyToID("_Color");
				int num2 = Shader.PropertyToID("_FadeColor");
				Color color = new Color(1f, 1f, 1f, settingsRef.solidOpacity);
				Color color2 = new Color(1f, 1f, 1f, settingsRef.solidOpacityBehindObjects);
				Color color3 = new Color(1f, 1f, 1f, settingsRef.lineOpacity);
				Color color4 = new Color(1f, 1f, 1f, settingsRef.lineOpacityBehindObjects);
				Color color5 = new Color(1f, 1f, 1f, settingsRef.textOpacity);
				Color color6 = new Color(1f, 1f, 1f, settingsRef.textOpacityBehindObjects);
				int i = 0;
				while (i < this.meshes.Count)
				{
					int num3 = i + 1;
					DrawingData.MeshType meshType = this.meshes[i].type & DrawingData.MeshType.BaseType;
					while (num3 < this.meshes.Count && (this.meshes[num3].type & DrawingData.MeshType.BaseType) == meshType)
					{
						num3++;
					}
					this.customMaterialProperties.Clear();
					Material material;
					switch (meshType)
					{
					case DrawingData.MeshType.Solid:
						material = this.surfaceMaterial;
						this.customMaterialProperties.SetColor(num, color);
						this.customMaterialProperties.SetColor(num2, color2);
						break;
					case DrawingData.MeshType.Lines:
						material = this.lineMaterial;
						this.customMaterialProperties.SetColor(num, color3);
						this.customMaterialProperties.SetColor(num2, color4);
						break;
					case DrawingData.MeshType.Solid | DrawingData.MeshType.Lines:
						goto IL_02E1;
					case DrawingData.MeshType.Text:
						material = this.fontData.material;
						this.customMaterialProperties.SetColor(num, color5);
						this.customMaterialProperties.SetColor(num2, color6);
						break;
					default:
						goto IL_02E1;
					}
					for (int j = 0; j < material.passCount; j++)
					{
						for (int k = i; k < num3; k++)
						{
							DrawingData.RenderedMeshWithType renderedMeshWithType = this.meshes[k];
							if ((renderedMeshWithType.type & DrawingData.MeshType.Custom) != (DrawingData.MeshType)0)
							{
								if (GeometryUtility.TestPlanesAABB(array, DrawingData.TransformBoundingBox(renderedMeshWithType.matrix, renderedMeshWithType.mesh.bounds)))
								{
									this.customMaterialProperties.SetColor(num, color * renderedMeshWithType.color);
									commandBuffer.DrawMesh(renderedMeshWithType.mesh, renderedMeshWithType.matrix, material, 0, j, this.customMaterialProperties);
									this.customMaterialProperties.SetColor(num, color);
								}
							}
							else if (GeometryUtility.TestPlanesAABB(array, renderedMeshWithType.mesh.bounds))
							{
								commandBuffer.DrawMesh(renderedMeshWithType.mesh, Matrix4x4.identity, material, 0, j, this.customMaterialProperties);
							}
						}
					}
					i = num3;
					continue;
					IL_02E1:
					throw new InvalidOperationException("Invalid mesh type");
				}
				this.meshes.Clear();
			}
			this.cameraVersions[cam] = range;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008EC0 File Offset: 0x000070C0
		private static Bounds TransformBoundingBox(Matrix4x4 matrix, Bounds bounds)
		{
			Vector3 min = bounds.min;
			Vector3 max = bounds.max;
			Bounds bounds2 = new Bounds(matrix.MultiplyPoint(min), Vector3.zero);
			bounds2.Encapsulate(matrix.MultiplyPoint(new Vector3(min.x, min.y, max.z)));
			bounds2.Encapsulate(matrix.MultiplyPoint(new Vector3(min.x, max.y, min.z)));
			bounds2.Encapsulate(matrix.MultiplyPoint(new Vector3(min.x, max.y, max.z)));
			bounds2.Encapsulate(matrix.MultiplyPoint(new Vector3(max.x, min.y, min.z)));
			bounds2.Encapsulate(matrix.MultiplyPoint(new Vector3(max.x, min.y, max.z)));
			bounds2.Encapsulate(matrix.MultiplyPoint(new Vector3(max.x, max.y, min.z)));
			bounds2.Encapsulate(matrix.MultiplyPoint(new Vector3(max.x, max.y, max.z)));
			return bounds2;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008FF8 File Offset: 0x000071F8
		public void ClearData()
		{
			this.gizmosHandle.Free();
			this.data.Dispose();
			this.processedData.Dispose(this);
			for (int i = 0; i < this.cachedMeshes.Count; i++)
			{
				Object.DestroyImmediate(this.cachedMeshes[i]);
			}
			this.cachedMeshes.Clear();
			this.fontData.Dispose();
		}

		// Token: 0x0400007B RID: 123
		internal DrawingData.BuilderDataContainer data;

		// Token: 0x0400007C RID: 124
		internal DrawingData.ProcessedBuilderDataContainer processedData;

		// Token: 0x0400007D RID: 125
		private List<DrawingData.RenderedMeshWithType> meshes = new List<DrawingData.RenderedMeshWithType>();

		// Token: 0x0400007E RID: 126
		private List<Mesh> cachedMeshes = new List<Mesh>();

		// Token: 0x0400007F RID: 127
		private List<Mesh> stagingCachedMeshes = new List<Mesh>();

		// Token: 0x04000080 RID: 128
		private int lastTimeLargestCachedMeshWasUsed;

		// Token: 0x04000081 RID: 129
		internal SDFLookupData fontData;

		// Token: 0x04000082 RID: 130
		private int currentDrawOrderIndex;

		// Token: 0x04000083 RID: 131
		internal int sceneModeVersion;

		// Token: 0x04000084 RID: 132
		public Material surfaceMaterial;

		// Token: 0x04000085 RID: 133
		public Material lineMaterial;

		// Token: 0x04000086 RID: 134
		public Material textMaterial;

		// Token: 0x04000087 RID: 135
		public DrawingSettings.Settings settings;

		// Token: 0x04000089 RID: 137
		private int lastTickVersion;

		// Token: 0x0400008A RID: 138
		private int lastTickVersion2;

		// Token: 0x0400008B RID: 139
		private Dictionary<int, GameObject> persistentRedrawScopes = new Dictionary<int, GameObject>();

		// Token: 0x0400008C RID: 140
		internal GCHandle gizmosHandle;

		// Token: 0x0400008D RID: 141
		public RedrawScope frameRedrawScope;

		// Token: 0x0400008E RID: 142
		private Dictionary<Camera, DrawingData.Range> cameraVersions = new Dictionary<Camera, DrawingData.Range>();

		// Token: 0x0400008F RID: 143
		internal static readonly ProfilerMarker MarkerScheduleJobs = new ProfilerMarker("ScheduleJobs");

		// Token: 0x04000090 RID: 144
		internal static readonly ProfilerMarker MarkerAwaitUserDependencies = new ProfilerMarker("Await user dependencies");

		// Token: 0x04000091 RID: 145
		internal static readonly ProfilerMarker MarkerSchedule = new ProfilerMarker("Schedule");

		// Token: 0x04000092 RID: 146
		internal static readonly ProfilerMarker MarkerBuild = new ProfilerMarker("Build");

		// Token: 0x04000093 RID: 147
		internal static readonly ProfilerMarker MarkerPool = new ProfilerMarker("Pool");

		// Token: 0x04000094 RID: 148
		internal static readonly ProfilerMarker MarkerRelease = new ProfilerMarker("Release");

		// Token: 0x04000095 RID: 149
		internal static readonly ProfilerMarker MarkerBuildMeshes = new ProfilerMarker("Build Meshes");

		// Token: 0x04000096 RID: 150
		internal static readonly ProfilerMarker MarkerCollectMeshes = new ProfilerMarker("Collect Meshes");

		// Token: 0x04000097 RID: 151
		internal static readonly ProfilerMarker MarkerSortMeshes = new ProfilerMarker("Sort Meshes");

		// Token: 0x04000098 RID: 152
		internal static readonly ProfilerMarker LeakTracking = new ProfilerMarker("RedrawScope Leak Tracking");

		// Token: 0x04000099 RID: 153
		private static readonly DrawingData.MeshCompareByDrawingOrder meshSorter = new DrawingData.MeshCompareByDrawingOrder();

		// Token: 0x0400009A RID: 154
		private Plane[] frustrumPlanes = new Plane[6];

		// Token: 0x0400009B RID: 155
		private MaterialPropertyBlock customMaterialProperties = new MaterialPropertyBlock();

		// Token: 0x0200002A RID: 42
		public struct Hasher : IEquatable<DrawingData.Hasher>
		{
			// Token: 0x1700000E RID: 14
			// (get) Token: 0x060001DF RID: 479 RVA: 0x00009114 File Offset: 0x00007314
			public static DrawingData.Hasher NotSupplied
			{
				get
				{
					return new DrawingData.Hasher
					{
						hash = ulong.MaxValue
					};
				}
			}

			// Token: 0x060001E0 RID: 480 RVA: 0x00009134 File Offset: 0x00007334
			public static DrawingData.Hasher Create<T>(T init)
			{
				DrawingData.Hasher hasher = default(DrawingData.Hasher);
				hasher.Add<T>(init);
				return hasher;
			}

			// Token: 0x060001E1 RID: 481 RVA: 0x00009152 File Offset: 0x00007352
			public void Add<T>(T hash)
			{
				this.hash = (1572869UL * this.hash) ^ (ulong)((long)hash.GetHashCode() + 12289L);
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000917D File Offset: 0x0000737D
			public ulong Hash
			{
				get
				{
					return this.hash;
				}
			}

			// Token: 0x060001E3 RID: 483 RVA: 0x00009185 File Offset: 0x00007385
			public override int GetHashCode()
			{
				return (int)this.hash;
			}

			// Token: 0x060001E4 RID: 484 RVA: 0x0000918E File Offset: 0x0000738E
			public bool Equals(DrawingData.Hasher other)
			{
				return this.hash == other.hash;
			}

			// Token: 0x0400009C RID: 156
			private ulong hash;
		}

		// Token: 0x0200002B RID: 43
		internal struct ProcessedBuilderData
		{
			// Token: 0x17000010 RID: 16
			// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000919E File Offset: 0x0000739E
			public bool isValid
			{
				get
				{
					return this.type > DrawingData.ProcessedBuilderData.Type.Invalid;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x060001E6 RID: 486 RVA: 0x000091A9 File Offset: 0x000073A9
			public unsafe UnsafeAppendBuffer* splitterOutputPtr
			{
				get
				{
					return &((DrawingData.ProcessedBuilderData.MeshBuffers*)this.temporaryMeshBuffers.GetUnsafePtr<DrawingData.ProcessedBuilderData.MeshBuffers>())->splitterOutput;
				}
			}

			// Token: 0x060001E7 RID: 487 RVA: 0x000091BC File Offset: 0x000073BC
			public void Init(DrawingData.ProcessedBuilderData.Type type, DrawingData.BuilderData.Meta meta)
			{
				this.submitted = false;
				this.type = type;
				this.meta = meta;
				if (this.meshes == null)
				{
					this.meshes = new List<DrawingData.MeshWithType>();
				}
				if (!this.temporaryMeshBuffers.IsCreated)
				{
					this.temporaryMeshBuffers = new NativeArray<DrawingData.ProcessedBuilderData.MeshBuffers>(1, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
					this.temporaryMeshBuffers[0] = new DrawingData.ProcessedBuilderData.MeshBuffers(Allocator.Persistent);
				}
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x00009220 File Offset: 0x00007420
			public unsafe void SetSplitterJob(DrawingData gizmos, JobHandle splitterJob)
			{
				this.splitterJob = splitterJob;
				if (this.type == DrawingData.ProcessedBuilderData.Type.Static)
				{
					GeometryBuilder.CameraInfo cameraInfo = new GeometryBuilder.CameraInfo(null);
					this.buildJob = GeometryBuilder.Build(gizmos, (DrawingData.ProcessedBuilderData.MeshBuffers*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<DrawingData.ProcessedBuilderData.MeshBuffers>(this.temporaryMeshBuffers), ref cameraInfo, splitterJob);
					DrawingData.ProcessedBuilderData.SubmittedJobs++;
					if (DrawingData.ProcessedBuilderData.SubmittedJobs % 8 == 0)
					{
						JobHandle.ScheduleBatchedJobs();
					}
				}
			}

			// Token: 0x060001E9 RID: 489 RVA: 0x0000927C File Offset: 0x0000747C
			public unsafe void SchedulePersistFilter(int version, int lastTickVersion, float time, int sceneModeVersion)
			{
				if (this.type != DrawingData.ProcessedBuilderData.Type.Persistent)
				{
					throw new InvalidOperationException();
				}
				if (this.meta.sceneModeVersion != sceneModeVersion)
				{
					this.meta.version = -1;
					return;
				}
				if (this.meta.version < lastTickVersion || this.submitted)
				{
					this.splitterJob.Complete();
					this.meta.version = version;
					if (this.temporaryMeshBuffers[0].splitterOutput.Length == 0)
					{
						this.meta.version = -1;
						return;
					}
					this.buildJob.Complete();
					this.splitterJob = new PersistentFilterJob
					{
						buffer = &((DrawingData.ProcessedBuilderData.MeshBuffers*)this.temporaryMeshBuffers.GetUnsafePtr<DrawingData.ProcessedBuilderData.MeshBuffers>())->splitterOutput,
						time = time
					}.Schedule(this.splitterJob);
				}
			}

			// Token: 0x060001EA RID: 490 RVA: 0x0000934E File Offset: 0x0000754E
			public bool IsValidForCamera(Camera camera, bool allowGizmos, bool allowCameraDefault)
			{
				if (!allowGizmos && this.meta.isGizmos)
				{
					return false;
				}
				if (this.meta.cameraTargets != null)
				{
					return this.meta.cameraTargets.Contains(camera);
				}
				return allowCameraDefault;
			}

			// Token: 0x060001EB RID: 491 RVA: 0x00009382 File Offset: 0x00007582
			public unsafe void Schedule(DrawingData gizmos, ref GeometryBuilder.CameraInfo cameraInfo)
			{
				if (this.type != DrawingData.ProcessedBuilderData.Type.Static)
				{
					this.buildJob = GeometryBuilder.Build(gizmos, (DrawingData.ProcessedBuilderData.MeshBuffers*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<DrawingData.ProcessedBuilderData.MeshBuffers>(this.temporaryMeshBuffers), ref cameraInfo, this.splitterJob);
				}
			}

			// Token: 0x060001EC RID: 492 RVA: 0x000093AB File Offset: 0x000075AB
			public unsafe void BuildMeshes(DrawingData gizmos)
			{
				if (this.type == DrawingData.ProcessedBuilderData.Type.Static && this.submitted)
				{
					return;
				}
				this.buildJob.Complete();
				GeometryBuilder.BuildMesh(gizmos, this.meshes, (DrawingData.ProcessedBuilderData.MeshBuffers*)this.temporaryMeshBuffers.GetUnsafePtr<DrawingData.ProcessedBuilderData.MeshBuffers>());
				this.submitted = true;
			}

			// Token: 0x060001ED RID: 493 RVA: 0x000093E8 File Offset: 0x000075E8
			public unsafe void CollectMeshes(List<DrawingData.RenderedMeshWithType> meshes)
			{
				List<DrawingData.MeshWithType> list = this.meshes;
				int num = 0;
				UnsafeAppendBuffer capturedState = this.temporaryMeshBuffers[0].capturedState;
				int num2 = capturedState.Length / UnsafeUtility.SizeOf<DrawingData.ProcessedBuilderData.CapturedState>();
				for (int i = 0; i < list.Count; i++)
				{
					Color color;
					Matrix4x4 matrix4x;
					int num3;
					if ((list[i].type & DrawingData.MeshType.Custom) != (DrawingData.MeshType)0)
					{
						DrawingData.ProcessedBuilderData.CapturedState capturedState2 = *(DrawingData.ProcessedBuilderData.CapturedState*)(capturedState.Ptr + (IntPtr)num * (IntPtr)sizeof(DrawingData.ProcessedBuilderData.CapturedState));
						color = capturedState2.color;
						matrix4x = capturedState2.matrix;
						num++;
						num3 = this.meta.drawOrderIndex + 1;
					}
					else
					{
						color = Color.white;
						matrix4x = Matrix4x4.identity;
						num3 = this.meta.drawOrderIndex;
					}
					meshes.Add(new DrawingData.RenderedMeshWithType
					{
						mesh = list[i].mesh,
						type = list[i].type,
						drawingOrderIndex = num3,
						color = color,
						matrix = matrix4x
					});
				}
			}

			// Token: 0x060001EE RID: 494 RVA: 0x000094EC File Offset: 0x000076EC
			private void PoolMeshes(DrawingData gizmos, bool includeCustom)
			{
				if (!this.isValid)
				{
					throw new InvalidOperationException();
				}
				int num = 0;
				for (int i = 0; i < this.meshes.Count; i++)
				{
					if ((this.meshes[i].type & DrawingData.MeshType.Custom) == (DrawingData.MeshType)0 || (includeCustom && (this.meshes[i].type & DrawingData.MeshType.Pool) != (DrawingData.MeshType)0))
					{
						gizmos.PoolMesh(this.meshes[i].mesh);
					}
					else
					{
						this.meshes[num] = this.meshes[i];
						num++;
					}
				}
				this.meshes.RemoveRange(num, this.meshes.Count - num);
			}

			// Token: 0x060001EF RID: 495 RVA: 0x0000959B File Offset: 0x0000779B
			public void PoolDynamicMeshes(DrawingData gizmos)
			{
				if (this.type == DrawingData.ProcessedBuilderData.Type.Static && this.submitted)
				{
					return;
				}
				this.PoolMeshes(gizmos, false);
			}

			// Token: 0x060001F0 RID: 496 RVA: 0x000095B8 File Offset: 0x000077B8
			public void Release(DrawingData gizmos)
			{
				if (!this.isValid)
				{
					throw new InvalidOperationException();
				}
				this.PoolMeshes(gizmos, true);
				this.meshes.Clear();
				this.type = DrawingData.ProcessedBuilderData.Type.Invalid;
				this.splitterJob.Complete();
				this.buildJob.Complete();
				DrawingData.ProcessedBuilderData.MeshBuffers meshBuffers = this.temporaryMeshBuffers[0];
				meshBuffers.DisposeIfLarge();
				this.temporaryMeshBuffers[0] = meshBuffers;
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x00009624 File Offset: 0x00007824
			public void Dispose()
			{
				if (this.isValid)
				{
					throw new InvalidOperationException();
				}
				this.splitterJob.Complete();
				this.buildJob.Complete();
				if (this.temporaryMeshBuffers.IsCreated)
				{
					this.temporaryMeshBuffers[0].Dispose();
					this.temporaryMeshBuffers.Dispose();
				}
			}

			// Token: 0x0400009D RID: 157
			public DrawingData.ProcessedBuilderData.Type type;

			// Token: 0x0400009E RID: 158
			public DrawingData.BuilderData.Meta meta;

			// Token: 0x0400009F RID: 159
			private bool submitted;

			// Token: 0x040000A0 RID: 160
			public NativeArray<DrawingData.ProcessedBuilderData.MeshBuffers> temporaryMeshBuffers;

			// Token: 0x040000A1 RID: 161
			private JobHandle buildJob;

			// Token: 0x040000A2 RID: 162
			private JobHandle splitterJob;

			// Token: 0x040000A3 RID: 163
			public List<DrawingData.MeshWithType> meshes;

			// Token: 0x040000A4 RID: 164
			private static int SubmittedJobs;

			// Token: 0x0200002C RID: 44
			public enum Type
			{
				// Token: 0x040000A6 RID: 166
				Invalid,
				// Token: 0x040000A7 RID: 167
				Static,
				// Token: 0x040000A8 RID: 168
				Dynamic,
				// Token: 0x040000A9 RID: 169
				Persistent
			}

			// Token: 0x0200002D RID: 45
			public struct CapturedState
			{
				// Token: 0x040000AA RID: 170
				public Matrix4x4 matrix;

				// Token: 0x040000AB RID: 171
				public Color color;
			}

			// Token: 0x0200002E RID: 46
			public struct MeshBuffers
			{
				// Token: 0x060001F2 RID: 498 RVA: 0x00009684 File Offset: 0x00007884
				public MeshBuffers(Allocator allocator)
				{
					this.splitterOutput = new UnsafeAppendBuffer(0, 4, allocator);
					this.vertices = new UnsafeAppendBuffer(0, 4, allocator);
					this.triangles = new UnsafeAppendBuffer(0, 4, allocator);
					this.solidVertices = new UnsafeAppendBuffer(0, 4, allocator);
					this.solidTriangles = new UnsafeAppendBuffer(0, 4, allocator);
					this.textVertices = new UnsafeAppendBuffer(0, 4, allocator);
					this.textTriangles = new UnsafeAppendBuffer(0, 4, allocator);
					this.capturedState = new UnsafeAppendBuffer(0, 4, allocator);
					this.bounds = default(Bounds);
				}

				// Token: 0x060001F3 RID: 499 RVA: 0x00009738 File Offset: 0x00007938
				public void Dispose()
				{
					this.splitterOutput.Dispose();
					this.vertices.Dispose();
					this.triangles.Dispose();
					this.solidVertices.Dispose();
					this.solidTriangles.Dispose();
					this.textVertices.Dispose();
					this.textTriangles.Dispose();
					this.capturedState.Dispose();
				}

				// Token: 0x060001F4 RID: 500 RVA: 0x000097A0 File Offset: 0x000079A0
				private static void DisposeIfLarge(ref UnsafeAppendBuffer ls)
				{
					if (ls.Length * 3 < ls.Capacity && ls.Capacity > 1024)
					{
						AllocatorManager.AllocatorHandle allocator = ls.Allocator;
						ls.Dispose();
						ls = new UnsafeAppendBuffer(0, 4, allocator);
					}
				}

				// Token: 0x060001F5 RID: 501 RVA: 0x000097E8 File Offset: 0x000079E8
				public void DisposeIfLarge()
				{
					DrawingData.ProcessedBuilderData.MeshBuffers.DisposeIfLarge(ref this.splitterOutput);
					DrawingData.ProcessedBuilderData.MeshBuffers.DisposeIfLarge(ref this.vertices);
					DrawingData.ProcessedBuilderData.MeshBuffers.DisposeIfLarge(ref this.triangles);
					DrawingData.ProcessedBuilderData.MeshBuffers.DisposeIfLarge(ref this.solidVertices);
					DrawingData.ProcessedBuilderData.MeshBuffers.DisposeIfLarge(ref this.solidTriangles);
					DrawingData.ProcessedBuilderData.MeshBuffers.DisposeIfLarge(ref this.textVertices);
					DrawingData.ProcessedBuilderData.MeshBuffers.DisposeIfLarge(ref this.textTriangles);
					DrawingData.ProcessedBuilderData.MeshBuffers.DisposeIfLarge(ref this.capturedState);
				}

				// Token: 0x040000AC RID: 172
				public UnsafeAppendBuffer splitterOutput;

				// Token: 0x040000AD RID: 173
				public UnsafeAppendBuffer vertices;

				// Token: 0x040000AE RID: 174
				public UnsafeAppendBuffer triangles;

				// Token: 0x040000AF RID: 175
				public UnsafeAppendBuffer solidVertices;

				// Token: 0x040000B0 RID: 176
				public UnsafeAppendBuffer solidTriangles;

				// Token: 0x040000B1 RID: 177
				public UnsafeAppendBuffer textVertices;

				// Token: 0x040000B2 RID: 178
				public UnsafeAppendBuffer textTriangles;

				// Token: 0x040000B3 RID: 179
				public UnsafeAppendBuffer capturedState;

				// Token: 0x040000B4 RID: 180
				public Bounds bounds;
			}
		}

		// Token: 0x0200002F RID: 47
		internal struct SubmittedMesh
		{
			// Token: 0x040000B5 RID: 181
			public Mesh mesh;

			// Token: 0x040000B6 RID: 182
			public bool temporary;
		}

		// Token: 0x02000030 RID: 48
		[BurstCompile]
		internal struct BuilderData : IDisposable
		{
			// Token: 0x17000012 RID: 18
			// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000984D File Offset: 0x00007A4D
			// (set) Token: 0x060001F7 RID: 503 RVA: 0x00009855 File Offset: 0x00007A55
			public DrawingData.BuilderData.State state { readonly get; private set; }

			// Token: 0x060001F8 RID: 504 RVA: 0x0000985E File Offset: 0x00007A5E
			public void Reserve(int dataIndex, bool isBuiltInCommandBuilder)
			{
				if (this.state != DrawingData.BuilderData.State.Free)
				{
					throw new InvalidOperationException();
				}
				this.state = DrawingData.BuilderData.State.Reserved;
				this.packedMeta = new DrawingData.BuilderData.BitPackedMeta(dataIndex, DrawingData.BuilderData.UniqueIDCounter++ & 32767, isBuiltInCommandBuilder);
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x00009898 File Offset: 0x00007A98
			public void Init(DrawingData.Hasher hasher, RedrawScope frameRedrawScope, RedrawScope customRedrawScope, bool isGizmos, int drawOrderIndex, int sceneModeVersion)
			{
				if (this.state != DrawingData.BuilderData.State.Reserved)
				{
					throw new InvalidOperationException();
				}
				this.meta = new DrawingData.BuilderData.Meta
				{
					hasher = hasher,
					redrawScope1 = frameRedrawScope,
					redrawScope2 = customRedrawScope,
					isGizmos = isGizmos,
					version = 0,
					drawOrderIndex = drawOrderIndex,
					sceneModeVersion = sceneModeVersion,
					cameraTargets = null
				};
				if (this.meshes == null)
				{
					this.meshes = new List<DrawingData.SubmittedMesh>();
				}
				if (!this.commandBuffers.IsCreated)
				{
					this.commandBuffers = new NativeArray<UnsafeAppendBuffer>(128, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
					for (int i = 0; i < this.commandBuffers.Length; i++)
					{
						this.commandBuffers[i] = new UnsafeAppendBuffer(0, 4, Allocator.Persistent);
					}
				}
				this.state = DrawingData.BuilderData.State.Initialized;
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x060001FA RID: 506 RVA: 0x0000996E File Offset: 0x00007B6E
			public unsafe UnsafeAppendBuffer* bufferPtr
			{
				get
				{
					return (UnsafeAppendBuffer*)this.commandBuffers.GetUnsafePtr<UnsafeAppendBuffer>();
				}
			}

			// Token: 0x060001FB RID: 507 RVA: 0x0000997B File Offset: 0x00007B7B
			[BurstCompile]
			[MonoPInvokeCallback(typeof(DrawingData.BuilderData.AnyBuffersWrittenToDelegate))]
			private unsafe static bool AnyBuffersWrittenTo(UnsafeAppendBuffer* buffers, int numBuffers)
			{
				return DrawingData.BuilderData.AnyBuffersWrittenTo_00000237$BurstDirectCall.Invoke(buffers, numBuffers);
			}

			// Token: 0x060001FC RID: 508 RVA: 0x00009984 File Offset: 0x00007B84
			[BurstCompile]
			[MonoPInvokeCallback(typeof(DrawingData.BuilderData.AnyBuffersWrittenToDelegate))]
			private unsafe static void ResetAllBuffers(UnsafeAppendBuffer* buffers, int numBuffers)
			{
				DrawingData.BuilderData.ResetAllBuffers_00000238$BurstDirectCall.Invoke(buffers, numBuffers);
			}

			// Token: 0x060001FD RID: 509 RVA: 0x0000998D File Offset: 0x00007B8D
			public void SubmitWithDependency(GCHandle gcHandle, JobHandle dependency, AllowedDelay allowedDelay)
			{
				this.state = DrawingData.BuilderData.State.WaitingForUserDefinedJob;
				this.disposeDependency = dependency;
				this.disposeDependencyDelay = allowedDelay;
				this.disposeGCHandle = gcHandle;
			}

			// Token: 0x060001FE RID: 510 RVA: 0x000099AC File Offset: 0x00007BAC
			public unsafe void Submit(DrawingData gizmos)
			{
				if (this.state != DrawingData.BuilderData.State.Initialized)
				{
					throw new InvalidOperationException();
				}
				if (this.meshes.Count == 0 && !DrawingData.BuilderData.AnyBuffersWrittenToInvoke((UnsafeAppendBuffer*)this.commandBuffers.GetUnsafeReadOnlyPtr<UnsafeAppendBuffer>(), this.commandBuffers.Length))
				{
					this.Release();
					return;
				}
				this.meta.version = gizmos.version;
				DrawingData.BuilderData.Meta meta = this.meta;
				meta.drawOrderIndex = this.meta.drawOrderIndex * 3;
				int num = gizmos.processedData.Reserve(DrawingData.ProcessedBuilderData.Type.Static, meta);
				meta.drawOrderIndex = this.meta.drawOrderIndex * 3 + 1;
				int num2 = gizmos.processedData.Reserve(DrawingData.ProcessedBuilderData.Type.Dynamic, meta);
				meta.drawOrderIndex = this.meta.drawOrderIndex + 1000000;
				int num3 = gizmos.processedData.Reserve(DrawingData.ProcessedBuilderData.Type.Persistent, meta);
				this.splitterJob = new StreamSplitter
				{
					inputBuffers = this.commandBuffers,
					staticBuffer = gizmos.processedData.Get(num).splitterOutputPtr,
					dynamicBuffer = gizmos.processedData.Get(num2).splitterOutputPtr,
					persistentBuffer = gizmos.processedData.Get(num3).splitterOutputPtr
				}.Schedule(default(JobHandle));
				gizmos.processedData.Get(num).SetSplitterJob(gizmos, this.splitterJob);
				gizmos.processedData.Get(num2).SetSplitterJob(gizmos, this.splitterJob);
				gizmos.processedData.Get(num3).SetSplitterJob(gizmos, this.splitterJob);
				if (this.meshes.Count > 0)
				{
					List<DrawingData.MeshWithType> list = gizmos.processedData.Get(num2).meshes;
					for (int i = 0; i < this.meshes.Count; i++)
					{
						list.Add(new DrawingData.MeshWithType
						{
							mesh = this.meshes[i].mesh,
							type = (DrawingData.MeshType.Solid | DrawingData.MeshType.Custom | (this.meshes[i].temporary ? DrawingData.MeshType.Pool : ((DrawingData.MeshType)0)))
						});
					}
					this.meshes.Clear();
				}
				this.state = DrawingData.BuilderData.State.WaitingForSplitter;
			}

			// Token: 0x060001FF RID: 511 RVA: 0x00009BDC File Offset: 0x00007DDC
			public void CheckJobDependency(DrawingData gizmos, bool allowBlocking)
			{
				if (this.state == DrawingData.BuilderData.State.WaitingForUserDefinedJob && (this.disposeDependency.IsCompleted || (allowBlocking && this.disposeDependencyDelay == AllowedDelay.EndOfFrame)))
				{
					this.disposeDependency.Complete();
					this.disposeDependency = default(JobHandle);
					this.disposeGCHandle.Free();
					this.state = DrawingData.BuilderData.State.Initialized;
					this.Submit(gizmos);
				}
			}

			// Token: 0x06000200 RID: 512 RVA: 0x00009C3A File Offset: 0x00007E3A
			public void Release()
			{
				if (this.state == DrawingData.BuilderData.State.Free)
				{
					throw new InvalidOperationException();
				}
				this.state = DrawingData.BuilderData.State.Free;
				this.ClearData();
			}

			// Token: 0x06000201 RID: 513 RVA: 0x00009C58 File Offset: 0x00007E58
			private unsafe void ClearData()
			{
				this.disposeDependency.Complete();
				this.splitterJob.Complete();
				this.meta = default(DrawingData.BuilderData.Meta);
				this.disposeDependency = default(JobHandle);
				this.preventDispose = false;
				this.meshes.Clear();
				DrawingData.BuilderData.ResetAllBuffers((UnsafeAppendBuffer*)this.commandBuffers.GetUnsafePtr<UnsafeAppendBuffer>(), this.commandBuffers.Length);
			}

			// Token: 0x06000202 RID: 514 RVA: 0x00009CC0 File Offset: 0x00007EC0
			public void Dispose()
			{
				if (this.state == DrawingData.BuilderData.State.WaitingForUserDefinedJob)
				{
					this.disposeDependency.Complete();
					this.disposeGCHandle.Free();
					this.state = DrawingData.BuilderData.State.WaitingForSplitter;
				}
				if (this.state == DrawingData.BuilderData.State.Reserved || this.state == DrawingData.BuilderData.State.Initialized || this.state == DrawingData.BuilderData.State.WaitingForUserDefinedJob)
				{
					Debug.LogError("Drawing data is being destroyed, but a drawing instance is still active. Are you sure you have called Dispose on all drawing instances? This will cause a memory leak!");
					return;
				}
				this.splitterJob.Complete();
				if (this.commandBuffers.IsCreated)
				{
					for (int i = 0; i < this.commandBuffers.Length; i++)
					{
						this.commandBuffers[i].Dispose();
					}
					this.commandBuffers.Dispose();
				}
			}

			// Token: 0x06000204 RID: 516 RVA: 0x00009DB8 File Offset: 0x00007FB8
			[BurstCompile]
			[MonoPInvokeCallback(typeof(DrawingData.BuilderData.AnyBuffersWrittenToDelegate))]
			[MethodImpl(256)]
			public unsafe static bool AnyBuffersWrittenTo$BurstManaged(UnsafeAppendBuffer* buffers, int numBuffers)
			{
				bool flag = false;
				for (int i = 0; i < numBuffers; i++)
				{
					flag |= buffers[i].Length > 0;
				}
				return flag;
			}

			// Token: 0x06000205 RID: 517 RVA: 0x00009DEC File Offset: 0x00007FEC
			[BurstCompile]
			[MonoPInvokeCallback(typeof(DrawingData.BuilderData.AnyBuffersWrittenToDelegate))]
			[MethodImpl(256)]
			public unsafe static void ResetAllBuffers$BurstManaged(UnsafeAppendBuffer* buffers, int numBuffers)
			{
				for (int i = 0; i < numBuffers; i++)
				{
					buffers[i].Reset();
				}
			}

			// Token: 0x040000B7 RID: 183
			public DrawingData.BuilderData.BitPackedMeta packedMeta;

			// Token: 0x040000B8 RID: 184
			public List<DrawingData.SubmittedMesh> meshes;

			// Token: 0x040000B9 RID: 185
			public NativeArray<UnsafeAppendBuffer> commandBuffers;

			// Token: 0x040000BB RID: 187
			public bool preventDispose;

			// Token: 0x040000BC RID: 188
			private JobHandle splitterJob;

			// Token: 0x040000BD RID: 189
			private JobHandle disposeDependency;

			// Token: 0x040000BE RID: 190
			private AllowedDelay disposeDependencyDelay;

			// Token: 0x040000BF RID: 191
			private GCHandle disposeGCHandle;

			// Token: 0x040000C0 RID: 192
			public DrawingData.BuilderData.Meta meta;

			// Token: 0x040000C1 RID: 193
			private static int UniqueIDCounter = 0;

			// Token: 0x040000C2 RID: 194
			private static readonly DrawingData.BuilderData.AnyBuffersWrittenToDelegate AnyBuffersWrittenToInvoke = BurstCompiler.CompileFunctionPointer<DrawingData.BuilderData.AnyBuffersWrittenToDelegate>(new DrawingData.BuilderData.AnyBuffersWrittenToDelegate(DrawingData.BuilderData.AnyBuffersWrittenTo)).Invoke;

			// Token: 0x040000C3 RID: 195
			private static readonly DrawingData.BuilderData.ResetAllBuffersToDelegate ResetAllBuffersToInvoke = BurstCompiler.CompileFunctionPointer<DrawingData.BuilderData.ResetAllBuffersToDelegate>(new DrawingData.BuilderData.ResetAllBuffersToDelegate(DrawingData.BuilderData.ResetAllBuffers)).Invoke;

			// Token: 0x02000031 RID: 49
			public enum State
			{
				// Token: 0x040000C5 RID: 197
				Free,
				// Token: 0x040000C6 RID: 198
				Reserved,
				// Token: 0x040000C7 RID: 199
				Initialized,
				// Token: 0x040000C8 RID: 200
				WaitingForSplitter,
				// Token: 0x040000C9 RID: 201
				WaitingForUserDefinedJob
			}

			// Token: 0x02000032 RID: 50
			public struct Meta
			{
				// Token: 0x040000CA RID: 202
				public DrawingData.Hasher hasher;

				// Token: 0x040000CB RID: 203
				public RedrawScope redrawScope1;

				// Token: 0x040000CC RID: 204
				public RedrawScope redrawScope2;

				// Token: 0x040000CD RID: 205
				public int version;

				// Token: 0x040000CE RID: 206
				public bool isGizmos;

				// Token: 0x040000CF RID: 207
				public int sceneModeVersion;

				// Token: 0x040000D0 RID: 208
				public int drawOrderIndex;

				// Token: 0x040000D1 RID: 209
				public Camera[] cameraTargets;
			}

			// Token: 0x02000033 RID: 51
			public struct BitPackedMeta
			{
				// Token: 0x06000206 RID: 518 RVA: 0x00009E15 File Offset: 0x00008015
				public BitPackedMeta(int dataIndex, int uniqueID, bool isBuiltInCommandBuilder)
				{
					if (dataIndex > 65535)
					{
						throw new Exception("Too many command builders active. Are some command builders not being disposed?");
					}
					this.flags = (uint)(dataIndex | (uniqueID << 17) | (isBuiltInCommandBuilder ? 65536 : 0));
				}

				// Token: 0x17000014 RID: 20
				// (get) Token: 0x06000207 RID: 519 RVA: 0x00009E42 File Offset: 0x00008042
				public int dataIndex
				{
					get
					{
						return (int)(this.flags & 65535U);
					}
				}

				// Token: 0x17000015 RID: 21
				// (get) Token: 0x06000208 RID: 520 RVA: 0x00009E50 File Offset: 0x00008050
				public int uniqueID
				{
					get
					{
						return (int)(this.flags >> 17);
					}
				}

				// Token: 0x17000016 RID: 22
				// (get) Token: 0x06000209 RID: 521 RVA: 0x00009E5B File Offset: 0x0000805B
				public bool isBuiltInCommandBuilder
				{
					get
					{
						return (this.flags & 65536U) > 0U;
					}
				}

				// Token: 0x0600020A RID: 522 RVA: 0x00009E6C File Offset: 0x0000806C
				public static bool operator ==(DrawingData.BuilderData.BitPackedMeta lhs, DrawingData.BuilderData.BitPackedMeta rhs)
				{
					return lhs.flags == rhs.flags;
				}

				// Token: 0x0600020B RID: 523 RVA: 0x00009E7C File Offset: 0x0000807C
				public static bool operator !=(DrawingData.BuilderData.BitPackedMeta lhs, DrawingData.BuilderData.BitPackedMeta rhs)
				{
					return lhs.flags != rhs.flags;
				}

				// Token: 0x0600020C RID: 524 RVA: 0x00009E90 File Offset: 0x00008090
				public override bool Equals(object obj)
				{
					if (obj is DrawingData.BuilderData.BitPackedMeta)
					{
						DrawingData.BuilderData.BitPackedMeta bitPackedMeta = (DrawingData.BuilderData.BitPackedMeta)obj;
						return this.flags == bitPackedMeta.flags;
					}
					return false;
				}

				// Token: 0x0600020D RID: 525 RVA: 0x00009EBC File Offset: 0x000080BC
				public override int GetHashCode()
				{
					return (int)this.flags;
				}

				// Token: 0x040000D2 RID: 210
				private uint flags;

				// Token: 0x040000D3 RID: 211
				private const int UniqueIDBitshift = 17;

				// Token: 0x040000D4 RID: 212
				private const int IsBuiltInFlagIndex = 16;

				// Token: 0x040000D5 RID: 213
				private const int IndexMask = 65535;

				// Token: 0x040000D6 RID: 214
				private const int MaxDataIndex = 65535;

				// Token: 0x040000D7 RID: 215
				public const int UniqueIdMask = 32767;
			}

			// Token: 0x02000034 RID: 52
			// (Invoke) Token: 0x0600020F RID: 527
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			private unsafe delegate bool AnyBuffersWrittenToDelegate(UnsafeAppendBuffer* buffers, int numBuffers);

			// Token: 0x02000035 RID: 53
			// (Invoke) Token: 0x06000213 RID: 531
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			private unsafe delegate void ResetAllBuffersToDelegate(UnsafeAppendBuffer* buffers, int numBuffers);

			// Token: 0x02000036 RID: 54
			// (Invoke) Token: 0x06000217 RID: 535
			public unsafe delegate bool AnyBuffersWrittenTo_00000237$PostfixBurstDelegate(UnsafeAppendBuffer* buffers, int numBuffers);

			// Token: 0x02000037 RID: 55
			internal static class AnyBuffersWrittenTo_00000237$BurstDirectCall
			{
				// Token: 0x0600021A RID: 538 RVA: 0x00009EC4 File Offset: 0x000080C4
				[BurstDiscard]
				private unsafe static void GetFunctionPointerDiscard(ref IntPtr A_0)
				{
					if (DrawingData.BuilderData.AnyBuffersWrittenTo_00000237$BurstDirectCall.Pointer == 0)
					{
						DrawingData.BuilderData.AnyBuffersWrittenTo_00000237$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(DrawingData.BuilderData.AnyBuffersWrittenTo_00000237$BurstDirectCall.DeferredCompilation, methodof(DrawingData.BuilderData.AnyBuffersWrittenTo$BurstManaged(UnsafeAppendBuffer*, int)).MethodHandle, typeof(DrawingData.BuilderData.AnyBuffersWrittenTo_00000237$PostfixBurstDelegate).TypeHandle);
					}
					A_0 = DrawingData.BuilderData.AnyBuffersWrittenTo_00000237$BurstDirectCall.Pointer;
				}

				// Token: 0x0600021B RID: 539 RVA: 0x00009EF0 File Offset: 0x000080F0
				private static IntPtr GetFunctionPointer()
				{
					IntPtr intPtr = (IntPtr)0;
					DrawingData.BuilderData.AnyBuffersWrittenTo_00000237$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
					return intPtr;
				}

				// Token: 0x0600021C RID: 540 RVA: 0x00009F08 File Offset: 0x00008108
				public unsafe static void Constructor()
				{
					DrawingData.BuilderData.AnyBuffersWrittenTo_00000237$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(DrawingData.BuilderData.AnyBuffersWrittenTo(UnsafeAppendBuffer*, int)).MethodHandle);
				}

				// Token: 0x0600021D RID: 541 RVA: 0x00002094 File Offset: 0x00000294
				public static void Initialize()
				{
				}

				// Token: 0x0600021E RID: 542 RVA: 0x00009F19 File Offset: 0x00008119
				// Note: this type is marked as 'beforefieldinit'.
				static AnyBuffersWrittenTo_00000237$BurstDirectCall()
				{
					DrawingData.BuilderData.AnyBuffersWrittenTo_00000237$BurstDirectCall.Constructor();
				}

				// Token: 0x0600021F RID: 543 RVA: 0x00009F20 File Offset: 0x00008120
				public unsafe static bool Invoke(UnsafeAppendBuffer* buffers, int numBuffers)
				{
					if (BurstCompiler.IsEnabled)
					{
						IntPtr functionPointer = DrawingData.BuilderData.AnyBuffersWrittenTo_00000237$BurstDirectCall.GetFunctionPointer();
						if (functionPointer != 0)
						{
							return calli(System.Boolean(Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer*,System.Int32), buffers, numBuffers, functionPointer);
						}
					}
					return DrawingData.BuilderData.AnyBuffersWrittenTo$BurstManaged(buffers, numBuffers);
				}

				// Token: 0x040000D8 RID: 216
				private static IntPtr Pointer;

				// Token: 0x040000D9 RID: 217
				private static IntPtr DeferredCompilation;
			}

			// Token: 0x02000038 RID: 56
			// (Invoke) Token: 0x06000221 RID: 545
			public unsafe delegate void ResetAllBuffers_00000238$PostfixBurstDelegate(UnsafeAppendBuffer* buffers, int numBuffers);

			// Token: 0x02000039 RID: 57
			internal static class ResetAllBuffers_00000238$BurstDirectCall
			{
				// Token: 0x06000224 RID: 548 RVA: 0x00009F53 File Offset: 0x00008153
				[BurstDiscard]
				private unsafe static void GetFunctionPointerDiscard(ref IntPtr A_0)
				{
					if (DrawingData.BuilderData.ResetAllBuffers_00000238$BurstDirectCall.Pointer == 0)
					{
						DrawingData.BuilderData.ResetAllBuffers_00000238$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(DrawingData.BuilderData.ResetAllBuffers_00000238$BurstDirectCall.DeferredCompilation, methodof(DrawingData.BuilderData.ResetAllBuffers$BurstManaged(UnsafeAppendBuffer*, int)).MethodHandle, typeof(DrawingData.BuilderData.ResetAllBuffers_00000238$PostfixBurstDelegate).TypeHandle);
					}
					A_0 = DrawingData.BuilderData.ResetAllBuffers_00000238$BurstDirectCall.Pointer;
				}

				// Token: 0x06000225 RID: 549 RVA: 0x00009F80 File Offset: 0x00008180
				private static IntPtr GetFunctionPointer()
				{
					IntPtr intPtr = (IntPtr)0;
					DrawingData.BuilderData.ResetAllBuffers_00000238$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
					return intPtr;
				}

				// Token: 0x06000226 RID: 550 RVA: 0x00009F98 File Offset: 0x00008198
				public unsafe static void Constructor()
				{
					DrawingData.BuilderData.ResetAllBuffers_00000238$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(DrawingData.BuilderData.ResetAllBuffers(UnsafeAppendBuffer*, int)).MethodHandle);
				}

				// Token: 0x06000227 RID: 551 RVA: 0x00002094 File Offset: 0x00000294
				public static void Initialize()
				{
				}

				// Token: 0x06000228 RID: 552 RVA: 0x00009FA9 File Offset: 0x000081A9
				// Note: this type is marked as 'beforefieldinit'.
				static ResetAllBuffers_00000238$BurstDirectCall()
				{
					DrawingData.BuilderData.ResetAllBuffers_00000238$BurstDirectCall.Constructor();
				}

				// Token: 0x06000229 RID: 553 RVA: 0x00009FB0 File Offset: 0x000081B0
				public unsafe static void Invoke(UnsafeAppendBuffer* buffers, int numBuffers)
				{
					if (BurstCompiler.IsEnabled)
					{
						IntPtr functionPointer = DrawingData.BuilderData.ResetAllBuffers_00000238$BurstDirectCall.GetFunctionPointer();
						if (functionPointer != 0)
						{
							calli(System.Void(Unity.Collections.LowLevel.Unsafe.UnsafeAppendBuffer*,System.Int32), buffers, numBuffers, functionPointer);
							return;
						}
					}
					DrawingData.BuilderData.ResetAllBuffers$BurstManaged(buffers, numBuffers);
				}

				// Token: 0x040000DA RID: 218
				private static IntPtr Pointer;

				// Token: 0x040000DB RID: 219
				private static IntPtr DeferredCompilation;
			}
		}

		// Token: 0x0200003A RID: 58
		internal struct BuilderDataContainer : IDisposable
		{
			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600022A RID: 554 RVA: 0x00009FE4 File Offset: 0x000081E4
			public unsafe int memoryUsage
			{
				get
				{
					int num = 0;
					if (this.data != null)
					{
						for (int i = 0; i < this.data.Length; i++)
						{
							NativeArray<UnsafeAppendBuffer> commandBuffers = this.data[i].commandBuffers;
							for (int j = 0; j < commandBuffers.Length; j++)
							{
								num += commandBuffers[j].Capacity;
							}
							num += this.data[i].commandBuffers.Length * sizeof(UnsafeAppendBuffer);
						}
					}
					return num;
				}
			}

			// Token: 0x0600022B RID: 555 RVA: 0x0000A064 File Offset: 0x00008264
			public DrawingData.BuilderData.BitPackedMeta Reserve(bool isBuiltInCommandBuilder)
			{
				if (this.data == null)
				{
					this.data = new DrawingData.BuilderData[1];
				}
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].state == DrawingData.BuilderData.State.Free)
					{
						this.data[i].Reserve(i, isBuiltInCommandBuilder);
						return this.data[i].packedMeta;
					}
				}
				DrawingData.BuilderData[] array = new DrawingData.BuilderData[this.data.Length * 2];
				this.data.CopyTo(array, 0);
				this.data = array;
				return this.Reserve(isBuiltInCommandBuilder);
			}

			// Token: 0x0600022C RID: 556 RVA: 0x0000A0FB File Offset: 0x000082FB
			public void Release(DrawingData.BuilderData.BitPackedMeta meta)
			{
				this.data[meta.dataIndex].Release();
			}

			// Token: 0x0600022D RID: 557 RVA: 0x0000A114 File Offset: 0x00008314
			public bool StillExists(DrawingData.BuilderData.BitPackedMeta meta)
			{
				int dataIndex = meta.dataIndex;
				return this.data != null && dataIndex < this.data.Length && this.data[dataIndex].packedMeta == meta;
			}

			// Token: 0x0600022E RID: 558 RVA: 0x0000A158 File Offset: 0x00008358
			public ref DrawingData.BuilderData Get(DrawingData.BuilderData.BitPackedMeta meta)
			{
				int dataIndex = meta.dataIndex;
				if (this.data[dataIndex].state == DrawingData.BuilderData.State.Free)
				{
					throw new ArgumentException("Data is not reserved");
				}
				if (this.data[dataIndex].packedMeta != meta)
				{
					throw new ArgumentException("This command builder has already been disposed");
				}
				return ref this.data[dataIndex];
			}

			// Token: 0x0600022F RID: 559 RVA: 0x0000A1BC File Offset: 0x000083BC
			public void DisposeCommandBuildersWithJobDependencies(DrawingData gizmos)
			{
				if (this.data == null)
				{
					return;
				}
				for (int i = 0; i < this.data.Length; i++)
				{
					this.data[i].CheckJobDependency(gizmos, false);
				}
				for (int j = 0; j < this.data.Length; j++)
				{
					this.data[j].CheckJobDependency(gizmos, true);
				}
			}

			// Token: 0x06000230 RID: 560 RVA: 0x0000A220 File Offset: 0x00008420
			public void ReleaseAllUnused()
			{
				if (this.data == null)
				{
					return;
				}
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].state == DrawingData.BuilderData.State.WaitingForSplitter)
					{
						this.data[i].Release();
					}
				}
			}

			// Token: 0x06000231 RID: 561 RVA: 0x0000A270 File Offset: 0x00008470
			public void Dispose()
			{
				if (this.data != null)
				{
					for (int i = 0; i < this.data.Length; i++)
					{
						this.data[i].Dispose();
					}
				}
				this.data = null;
			}

			// Token: 0x040000DC RID: 220
			private DrawingData.BuilderData[] data;
		}

		// Token: 0x0200003B RID: 59
		internal struct ProcessedBuilderDataContainer
		{
			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000232 RID: 562 RVA: 0x0000A2B0 File Offset: 0x000084B0
			public int memoryUsage
			{
				get
				{
					int num = 0;
					if (this.data != null)
					{
						for (int i = 0; i < this.data.Length; i++)
						{
							NativeArray<DrawingData.ProcessedBuilderData.MeshBuffers> temporaryMeshBuffers = this.data[i].temporaryMeshBuffers;
							for (int j = 0; j < temporaryMeshBuffers.Length; j++)
							{
								int num2 = 0;
								num2 += temporaryMeshBuffers[j].textVertices.Capacity;
								num2 += temporaryMeshBuffers[j].textTriangles.Capacity;
								num2 += temporaryMeshBuffers[j].solidVertices.Capacity;
								num2 += temporaryMeshBuffers[j].solidTriangles.Capacity;
								num2 += temporaryMeshBuffers[j].vertices.Capacity;
								num2 += temporaryMeshBuffers[j].triangles.Capacity;
								num2 += temporaryMeshBuffers[j].capturedState.Capacity;
								num2 += temporaryMeshBuffers[j].splitterOutput.Capacity;
								num += num2;
								Debug.Log(string.Concat(new string[]
								{
									i.ToString(),
									":",
									j.ToString(),
									" ",
									num2.ToString()
								}));
							}
						}
					}
					return num;
				}
			}

			// Token: 0x06000233 RID: 563 RVA: 0x0000A40C File Offset: 0x0000860C
			public int Reserve(DrawingData.ProcessedBuilderData.Type type, DrawingData.BuilderData.Meta meta)
			{
				if (this.data == null)
				{
					this.data = new DrawingData.ProcessedBuilderData[0];
					this.freeSlots = new Stack<int>();
					this.freeLists = new Stack<List<int>>();
					this.hash2index = new Dictionary<ulong, List<int>>();
				}
				if (this.freeSlots.Count == 0)
				{
					DrawingData.ProcessedBuilderData[] array = new DrawingData.ProcessedBuilderData[math.max(4, this.data.Length * 2)];
					this.data.CopyTo(array, 0);
					for (int i = this.data.Length; i < array.Length; i++)
					{
						this.freeSlots.Push(i);
					}
					this.data = array;
				}
				int num = this.freeSlots.Pop();
				this.data[num].Init(type, meta);
				if (!meta.hasher.Equals(DrawingData.Hasher.NotSupplied))
				{
					List<int> list;
					if (!this.hash2index.TryGetValue(meta.hasher.Hash, out list))
					{
						if (this.freeLists.Count == 0)
						{
							this.freeLists.Push(new List<int>());
						}
						list = (this.hash2index[meta.hasher.Hash] = this.freeLists.Pop());
					}
					list.Add(num);
				}
				return num;
			}

			// Token: 0x06000234 RID: 564 RVA: 0x0000A540 File Offset: 0x00008740
			public ref DrawingData.ProcessedBuilderData Get(int index)
			{
				if (!this.data[index].isValid)
				{
					throw new ArgumentException();
				}
				return ref this.data[index];
			}

			// Token: 0x06000235 RID: 565 RVA: 0x0000A568 File Offset: 0x00008768
			private void Release(DrawingData gizmos, int i)
			{
				ulong hash = this.data[i].meta.hasher.Hash;
				List<int> list;
				if (!this.data[i].meta.hasher.Equals(DrawingData.Hasher.NotSupplied) && this.hash2index.TryGetValue(hash, out list))
				{
					list.Remove(i);
					if (list.Count == 0)
					{
						this.freeLists.Push(list);
						this.hash2index.Remove(hash);
					}
				}
				this.data[i].Release(gizmos);
				this.freeSlots.Push(i);
			}

			// Token: 0x06000236 RID: 566 RVA: 0x0000A60C File Offset: 0x0000880C
			public void SubmitMeshes(DrawingData gizmos, Camera camera, int versionThreshold, bool allowGizmos, bool allowCameraDefault)
			{
				if (this.data == null)
				{
					return;
				}
				GeometryBuilder.CameraInfo cameraInfo = new GeometryBuilder.CameraInfo(camera);
				int num = 0;
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].isValid && this.data[i].meta.version >= versionThreshold && this.data[i].IsValidForCamera(camera, allowGizmos, allowCameraDefault))
					{
						num++;
						this.data[i].Schedule(gizmos, ref cameraInfo);
					}
				}
				JobHandle.ScheduleBatchedJobs();
				for (int j = 0; j < this.data.Length; j++)
				{
					if (this.data[j].isValid && this.data[j].meta.version >= versionThreshold && this.data[j].IsValidForCamera(camera, allowGizmos, allowCameraDefault))
					{
						this.data[j].BuildMeshes(gizmos);
					}
				}
			}

			// Token: 0x06000237 RID: 567 RVA: 0x0000A70C File Offset: 0x0000890C
			public void PoolDynamicMeshes(DrawingData gizmos)
			{
				if (this.data == null)
				{
					return;
				}
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].isValid)
					{
						this.data[i].PoolDynamicMeshes(gizmos);
					}
				}
			}

			// Token: 0x06000238 RID: 568 RVA: 0x0000A75C File Offset: 0x0000895C
			public void CollectMeshes(int versionThreshold, List<DrawingData.RenderedMeshWithType> meshes, Camera camera, bool allowGizmos, bool allowCameraDefault)
			{
				if (this.data == null)
				{
					return;
				}
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].isValid && this.data[i].meta.version >= versionThreshold && this.data[i].IsValidForCamera(camera, allowGizmos, allowCameraDefault))
					{
						this.data[i].CollectMeshes(meshes);
					}
				}
			}

			// Token: 0x06000239 RID: 569 RVA: 0x0000A7DC File Offset: 0x000089DC
			public void FilterOldPersistentCommands(int version, int lastTickVersion, float time, int sceneModeVersion)
			{
				if (this.data == null)
				{
					return;
				}
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].isValid && this.data[i].type == DrawingData.ProcessedBuilderData.Type.Persistent)
					{
						this.data[i].SchedulePersistFilter(version, lastTickVersion, time, sceneModeVersion);
					}
				}
			}

			// Token: 0x0600023A RID: 570 RVA: 0x0000A844 File Offset: 0x00008A44
			public bool SetVersion(DrawingData.Hasher hasher, int version)
			{
				if (this.data == null)
				{
					return false;
				}
				List<int> list;
				if (this.hash2index.TryGetValue(hasher.Hash, out list))
				{
					for (int i = 0; i < list.Count; i++)
					{
						int num = list[i];
						this.data[num].meta.version = version;
					}
					return true;
				}
				return false;
			}

			// Token: 0x0600023B RID: 571 RVA: 0x0000A8A4 File Offset: 0x00008AA4
			public bool SetVersion(RedrawScope scope, int version)
			{
				if (this.data == null)
				{
					return false;
				}
				bool flag = false;
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].isValid && (this.data[i].meta.redrawScope1.id == scope.id || this.data[i].meta.redrawScope2.id == scope.id))
					{
						this.data[i].meta.version = version;
						flag = true;
					}
				}
				return flag;
			}

			// Token: 0x0600023C RID: 572 RVA: 0x0000A948 File Offset: 0x00008B48
			public bool SetCustomScope(DrawingData.Hasher hasher, RedrawScope scope)
			{
				if (this.data == null)
				{
					return false;
				}
				List<int> list;
				if (this.hash2index.TryGetValue(hasher.Hash, out list))
				{
					for (int i = 0; i < list.Count; i++)
					{
						int num = list[i];
						this.data[num].meta.redrawScope2 = scope;
					}
					return true;
				}
				return false;
			}

			// Token: 0x0600023D RID: 573 RVA: 0x0000A9A8 File Offset: 0x00008BA8
			public void ReleaseDataOlderThan(DrawingData gizmos, int version)
			{
				if (this.data == null)
				{
					return;
				}
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].isValid && this.data[i].meta.version < version)
					{
						this.Release(gizmos, i);
					}
				}
			}

			// Token: 0x0600023E RID: 574 RVA: 0x0000AA08 File Offset: 0x00008C08
			public void ReleaseAllWithHash(DrawingData gizmos, DrawingData.Hasher hasher)
			{
				if (this.data == null)
				{
					return;
				}
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].isValid && this.data[i].meta.hasher.Hash == hasher.Hash)
					{
						this.Release(gizmos, i);
					}
				}
			}

			// Token: 0x0600023F RID: 575 RVA: 0x0000AA70 File Offset: 0x00008C70
			public void Dispose(DrawingData gizmos)
			{
				if (this.data == null)
				{
					return;
				}
				for (int i = 0; i < this.data.Length; i++)
				{
					if (this.data[i].isValid)
					{
						this.Release(gizmos, i);
					}
					this.data[i].Dispose();
				}
				this.data = null;
			}

			// Token: 0x040000DD RID: 221
			private DrawingData.ProcessedBuilderData[] data;

			// Token: 0x040000DE RID: 222
			private Dictionary<ulong, List<int>> hash2index;

			// Token: 0x040000DF RID: 223
			private Stack<int> freeSlots;

			// Token: 0x040000E0 RID: 224
			private Stack<List<int>> freeLists;
		}

		// Token: 0x0200003C RID: 60
		[Flags]
		internal enum MeshType
		{
			// Token: 0x040000E2 RID: 226
			Solid = 1,
			// Token: 0x040000E3 RID: 227
			Lines = 2,
			// Token: 0x040000E4 RID: 228
			Text = 4,
			// Token: 0x040000E5 RID: 229
			Custom = 8,
			// Token: 0x040000E6 RID: 230
			Pool = 16,
			// Token: 0x040000E7 RID: 231
			BaseType = 7
		}

		// Token: 0x0200003D RID: 61
		internal struct MeshWithType
		{
			// Token: 0x040000E8 RID: 232
			public Mesh mesh;

			// Token: 0x040000E9 RID: 233
			public DrawingData.MeshType type;
		}

		// Token: 0x0200003E RID: 62
		internal struct RenderedMeshWithType
		{
			// Token: 0x040000EA RID: 234
			public Mesh mesh;

			// Token: 0x040000EB RID: 235
			public DrawingData.MeshType type;

			// Token: 0x040000EC RID: 236
			public int drawingOrderIndex;

			// Token: 0x040000ED RID: 237
			public Color color;

			// Token: 0x040000EE RID: 238
			public Matrix4x4 matrix;
		}

		// Token: 0x0200003F RID: 63
		private struct Range
		{
			// Token: 0x040000EF RID: 239
			public int start;

			// Token: 0x040000F0 RID: 240
			public int end;
		}

		// Token: 0x02000040 RID: 64
		private class MeshCompareByDrawingOrder : IComparer<DrawingData.RenderedMeshWithType>
		{
			// Token: 0x06000240 RID: 576 RVA: 0x0000AACC File Offset: 0x00008CCC
			public int Compare(DrawingData.RenderedMeshWithType a, DrawingData.RenderedMeshWithType b)
			{
				int num = (int)(a.type & DrawingData.MeshType.BaseType);
				int num2 = (int)(b.type & DrawingData.MeshType.BaseType);
				if (num == num2)
				{
					return a.drawingOrderIndex - b.drawingOrderIndex;
				}
				return num - num2;
			}
		}

		// Token: 0x02000041 RID: 65
		public struct CommandBufferWrapper
		{
			// Token: 0x06000242 RID: 578 RVA: 0x0000AB00 File Offset: 0x00008D00
			public void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int submeshIndex, int shaderPass, MaterialPropertyBlock properties)
			{
				if (this.cmd != null)
				{
					this.cmd.DrawMesh(mesh, matrix, material, submeshIndex, shaderPass, properties);
				}
			}

			// Token: 0x040000F1 RID: 241
			public CommandBuffer cmd;
		}
	}
}

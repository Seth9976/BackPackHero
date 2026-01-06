using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Rendering;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200032F RID: 815
	internal class UIRenderDevice : IDisposable
	{
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x0006FFC9 File Offset: 0x0006E1C9
		internal uint maxVerticesPerPage { get; } = 65535U;

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0006FFD1 File Offset: 0x0006E1D1
		// (set) Token: 0x06001A3C RID: 6716 RVA: 0x0006FFD9 File Offset: 0x0006E1D9
		internal bool breakBatches { get; set; }

		// Token: 0x06001A3D RID: 6717 RVA: 0x0006FFE4 File Offset: 0x0006E1E4
		static UIRenderDevice()
		{
			Utility.EngineUpdate += new Action(UIRenderDevice.OnEngineUpdateGlobal);
			Utility.FlushPendingResources += new Action(UIRenderDevice.OnFlushPendingResources);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000700DA File Offset: 0x0006E2DA
		public UIRenderDevice(uint initialVertexCapacity = 0U, uint initialIndexCapacity = 0U)
			: this(initialVertexCapacity, initialIndexCapacity, false)
		{
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000700E8 File Offset: 0x0006E2E8
		protected UIRenderDevice(uint initialVertexCapacity, uint initialIndexCapacity, bool mockDevice)
		{
			this.m_MockDevice = mockDevice;
			Debug.Assert(!UIRenderDevice.m_SynchronousFree);
			Debug.Assert(true);
			bool flag = UIRenderDevice.m_ActiveDeviceCount++ == 0;
			if (flag)
			{
				bool flag2 = !UIRenderDevice.m_SubscribedToNotifications && !this.m_MockDevice;
				if (flag2)
				{
					Utility.NotifyOfUIREvents(true);
					UIRenderDevice.m_SubscribedToNotifications = true;
				}
			}
			this.m_NextPageVertexCount = Math.Max(initialVertexCapacity, 2048U);
			this.m_LargeMeshVertexCount = this.m_NextPageVertexCount;
			this.m_IndexToVertexCountRatio = initialIndexCapacity / initialVertexCapacity;
			this.m_IndexToVertexCountRatio = Mathf.Max(this.m_IndexToVertexCountRatio, 2f);
			this.m_DeferredFrees = new List<List<UIRenderDevice.AllocToFree>>(4);
			this.m_Updates = new List<List<UIRenderDevice.AllocToUpdate>>(4);
			int num = 0;
			while ((long)num < 4L)
			{
				this.m_DeferredFrees.Add(new List<UIRenderDevice.AllocToFree>());
				this.m_Updates.Add(new List<UIRenderDevice.AllocToUpdate>());
				num++;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x0007025C File Offset: 0x0006E45C
		internal static Texture2D defaultShaderInfoTexFloat
		{
			get
			{
				bool flag = UIRenderDevice.s_DefaultShaderInfoTexFloat == null;
				if (flag)
				{
					UIRenderDevice.s_DefaultShaderInfoTexFloat = new Texture2D(64, 64, TextureFormat.RGBAFloat, false);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.name = "DefaultShaderInfoTexFloat";
					UIRenderDevice.s_DefaultShaderInfoTexFloat.hideFlags = HideFlags.HideAndDontSave;
					UIRenderDevice.s_DefaultShaderInfoTexFloat.filterMode = FilterMode.Point;
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y, UIRVEShaderInfoAllocator.identityTransformRow0Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 1, UIRVEShaderInfoAllocator.identityTransformRow1Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.identityTransformTexel.x, UIRVEShaderInfoAllocator.identityTransformTexel.y + 2, UIRVEShaderInfoAllocator.identityTransformRow2Value);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.infiniteClipRectTexel.x, UIRVEShaderInfoAllocator.infiniteClipRectTexel.y, UIRVEShaderInfoAllocator.infiniteClipRectValue);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.fullOpacityTexel.x, UIRVEShaderInfoAllocator.fullOpacityTexel.y, UIRVEShaderInfoAllocator.fullOpacityValue);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y, Color.white);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 1, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 2, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 3, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexFloat.Apply(false, true);
				}
				return UIRenderDevice.s_DefaultShaderInfoTexFloat;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x0007046C File Offset: 0x0006E66C
		internal static Texture2D defaultShaderInfoTexARGB8
		{
			get
			{
				bool flag = UIRenderDevice.s_DefaultShaderInfoTexARGB8 == null;
				if (flag)
				{
					UIRenderDevice.s_DefaultShaderInfoTexARGB8 = new Texture2D(64, 64, TextureFormat.RGBA32, false);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.name = "DefaultShaderInfoTexARGB8";
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.hideFlags = HideFlags.HideAndDontSave;
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.filterMode = FilterMode.Point;
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.fullOpacityTexel.x, UIRVEShaderInfoAllocator.fullOpacityTexel.y, UIRVEShaderInfoAllocator.fullOpacityValue);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y, Color.white);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 1, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 2, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.SetPixel(UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.x, UIRVEShaderInfoAllocator.defaultTextCoreSettingsTexel.y + 3, Color.clear);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8.Apply(false, true);
				}
				return UIRenderDevice.s_DefaultShaderInfoTexARGB8;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x000705BC File Offset: 0x0006E7BC
		internal static bool vertexTexturingIsAvailable
		{
			get
			{
				bool flag = UIRenderDevice.s_VertexTexturingIsAvailable == null;
				if (flag)
				{
					Shader shader = Shader.Find(UIRUtility.k_DefaultShaderName);
					Material material = new Material(shader);
					material.hideFlags |= HideFlags.DontSaveInEditor;
					string tag = material.GetTag("UIE_VertexTexturingIsAvailable", false);
					UIRUtility.Destroy(material);
					UIRenderDevice.s_VertexTexturingIsAvailable = new bool?(tag == "1");
				}
				return UIRenderDevice.s_VertexTexturingIsAvailable.Value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x00070638 File Offset: 0x0006E838
		internal static bool shaderModelIs35
		{
			get
			{
				bool flag = UIRenderDevice.s_ShaderModelIs35 == null;
				if (flag)
				{
					Shader shader = Shader.Find(UIRUtility.k_DefaultShaderName);
					Material material = new Material(shader);
					material.hideFlags |= HideFlags.DontSaveInEditor;
					string tag = material.GetTag("UIE_ShaderModelIs35", false);
					UIRUtility.Destroy(material);
					UIRenderDevice.s_ShaderModelIs35 = new bool?(tag == "1");
				}
				return UIRenderDevice.s_ShaderModelIs35.Value;
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x000706B4 File Offset: 0x0006E8B4
		private void InitVertexDeclaration()
		{
			VertexAttributeDescriptor[] array = new VertexAttributeDescriptor[]
			{
				new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
				new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord2, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord3, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord4, VertexAttributeFormat.UNorm8, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord5, VertexAttributeFormat.Float32, 4, 0),
				new VertexAttributeDescriptor(VertexAttribute.TexCoord6, VertexAttributeFormat.Float32, 1, 0)
			};
			this.m_VertexDecl = Utility.GetVertexDeclaration(array);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00070768 File Offset: 0x0006E968
		private void CompleteCreation()
		{
			bool flag = this.m_MockDevice || this.fullyCreated;
			if (!flag)
			{
				this.InitVertexDeclaration();
				this.m_Fences = new uint[4];
				this.m_StandardMatProps = new MaterialPropertyBlock();
				this.m_DefaultStencilState = Utility.CreateStencilState(new StencilState
				{
					enabled = true,
					readMask = byte.MaxValue,
					writeMask = byte.MaxValue,
					compareFunctionFront = CompareFunction.Equal,
					passOperationFront = StencilOp.Keep,
					failOperationFront = StencilOp.Keep,
					zFailOperationFront = StencilOp.IncrementSaturate,
					compareFunctionBack = CompareFunction.Less,
					passOperationBack = StencilOp.Keep,
					failOperationBack = StencilOp.Keep,
					zFailOperationBack = StencilOp.DecrementSaturate
				});
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x00070830 File Offset: 0x0006EA30
		private bool fullyCreated
		{
			get
			{
				return this.m_Fences != null;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x0007084B File Offset: 0x0006EA4B
		// (set) Token: 0x06001A48 RID: 6728 RVA: 0x00070853 File Offset: 0x0006EA53
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001A49 RID: 6729 RVA: 0x0007085C File Offset: 0x0006EA5C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0007086E File Offset: 0x0006EA6E
		internal void DisposeImmediate()
		{
			Debug.Assert(!UIRenderDevice.m_SynchronousFree);
			UIRenderDevice.m_SynchronousFree = true;
			this.Dispose();
			UIRenderDevice.m_SynchronousFree = false;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00070894 File Offset: 0x0006EA94
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				UIRenderDevice.m_ActiveDeviceCount--;
				if (disposing)
				{
					UIRenderDevice.DeviceToFree deviceToFree = new UIRenderDevice.DeviceToFree
					{
						handle = (this.m_MockDevice ? 0U : Utility.InsertCPUFence()),
						page = this.m_FirstPage
					};
					bool flag = deviceToFree.handle == 0U;
					if (flag)
					{
						deviceToFree.Dispose();
					}
					else
					{
						UIRenderDevice.m_DeviceFreeQueue.AddLast(deviceToFree);
						bool synchronousFree = UIRenderDevice.m_SynchronousFree;
						if (synchronousFree)
						{
							UIRenderDevice.ProcessDeviceFreeQueue();
						}
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00070934 File Offset: 0x0006EB34
		public MeshHandle Allocate(uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			MeshHandle meshHandle = this.m_MeshHandles.Get();
			meshHandle.triangleCount = indexCount / 3U;
			this.Allocate(meshHandle, vertexCount, indexCount, out vertexData, out indexData, false);
			indexOffset = (ushort)meshHandle.allocVerts.start;
			return meshHandle;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0007097C File Offset: 0x0006EB7C
		public void Update(MeshHandle mesh, uint vertexCount, out NativeSlice<Vertex> vertexData)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, (int)vertexCount);
			}
			else
			{
				uint start = mesh.allocVerts.start;
				NativeSlice<ushort> nativeSlice = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)mesh.allocIndices.size);
				NativeSlice<ushort> nativeSlice2;
				ushort num;
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, mesh.allocIndices.size, out vertexData, out nativeSlice2, out num, out allocToUpdate, false);
				int size = (int)mesh.allocIndices.size;
				int num2 = (int)((uint)num - start);
				for (int i = 0; i < size; i++)
				{
					nativeSlice2[i] = (ushort)((int)nativeSlice[i] + num2);
				}
			}
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00070A78 File Offset: 0x0006EC78
		public void Update(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset)
		{
			Debug.Assert(mesh.allocVerts.size >= vertexCount);
			Debug.Assert(mesh.allocIndices.size >= indexCount);
			bool flag = mesh.allocTime == this.m_FrameIndex;
			if (flag)
			{
				vertexData = mesh.allocPage.vertices.cpuData.Slice((int)mesh.allocVerts.start, (int)vertexCount);
				indexData = mesh.allocPage.indices.cpuData.Slice((int)mesh.allocIndices.start, (int)indexCount);
				indexOffset = (ushort)mesh.allocVerts.start;
				this.UpdateCopyBackIndices(mesh, true);
			}
			else
			{
				UIRenderDevice.AllocToUpdate allocToUpdate;
				this.UpdateAfterGPUUsedData(mesh, vertexCount, indexCount, out vertexData, out indexData, out indexOffset, out allocToUpdate, true);
			}
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00070B44 File Offset: 0x0006ED44
		private void UpdateCopyBackIndices(MeshHandle mesh, bool copyBackIndices)
		{
			bool flag = mesh.updateAllocID == 0U;
			if (!flag)
			{
				int num = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> list = this.ActiveUpdatesForMeshHandle(mesh);
				UIRenderDevice.AllocToUpdate allocToUpdate = list[num];
				allocToUpdate.copyBackIndices = true;
				list[num] = allocToUpdate;
			}
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00070B8C File Offset: 0x0006ED8C
		internal List<UIRenderDevice.AllocToUpdate> ActiveUpdatesForMeshHandle(MeshHandle mesh)
		{
			return this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00070BBC File Offset: 0x0006EDBC
		private bool TryAllocFromPage(Page page, uint vertexCount, uint indexCount, ref Alloc va, ref Alloc ia, bool shortLived)
		{
			va = page.vertices.allocator.Allocate(vertexCount, shortLived);
			bool flag = va.size > 0U;
			if (flag)
			{
				ia = page.indices.allocator.Allocate(indexCount, shortLived);
				bool flag2 = ia.size > 0U;
				if (flag2)
				{
					return true;
				}
				page.vertices.allocator.Free(va);
				va.size = 0U;
			}
			return false;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00070C48 File Offset: 0x0006EE48
		private void Allocate(MeshHandle meshHandle, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, bool shortLived)
		{
			UIRenderDevice.s_MarkerAllocate.Begin();
			Page page = null;
			Alloc alloc = default(Alloc);
			Alloc alloc2 = default(Alloc);
			bool flag = vertexCount <= this.m_LargeMeshVertexCount;
			if (flag)
			{
				bool flag2 = this.m_FirstPage != null;
				if (flag2)
				{
					page = this.m_FirstPage;
					for (;;)
					{
						bool flag3 = this.TryAllocFromPage(page, vertexCount, indexCount, ref alloc, ref alloc2, shortLived) || page.next == null;
						if (flag3)
						{
							break;
						}
						page = page.next;
					}
				}
				else
				{
					this.CompleteCreation();
				}
				bool flag4 = alloc2.size == 0U;
				if (flag4)
				{
					this.m_NextPageVertexCount <<= 1;
					this.m_NextPageVertexCount = Math.Max(this.m_NextPageVertexCount, vertexCount * 2U);
					this.m_NextPageVertexCount = Math.Min(this.m_NextPageVertexCount, this.maxVerticesPerPage);
					uint num = (uint)(this.m_NextPageVertexCount * this.m_IndexToVertexCountRatio + 0.5f);
					num = Math.Max(num, indexCount * 2U);
					Debug.Assert(((page != null) ? page.next : null) == null);
					page = new Page(this.m_NextPageVertexCount, num, 4U, this.m_MockDevice);
					page.next = this.m_FirstPage;
					this.m_FirstPage = page;
					alloc = page.vertices.allocator.Allocate(vertexCount, shortLived);
					alloc2 = page.indices.allocator.Allocate(indexCount, shortLived);
					Debug.Assert(alloc.size > 0U);
					Debug.Assert(alloc2.size > 0U);
				}
			}
			else
			{
				this.CompleteCreation();
				Page page2 = this.m_FirstPage;
				Page page3 = this.m_FirstPage;
				int num2 = int.MaxValue;
				while (page2 != null)
				{
					int num3 = page2.vertices.cpuData.Length - (int)vertexCount;
					int num4 = page2.indices.cpuData.Length - (int)indexCount;
					bool flag5 = page2.isEmpty && num3 >= 0 && num4 >= 0 && num3 < num2;
					if (flag5)
					{
						page = page2;
						num2 = num3;
					}
					page3 = page2;
					page2 = page2.next;
				}
				bool flag6 = page == null;
				if (flag6)
				{
					uint num5 = ((vertexCount > this.maxVerticesPerPage) ? 2U : vertexCount);
					Debug.Assert(vertexCount <= this.maxVerticesPerPage, "Requested Vertex count is above the limit. Alloc will fail.");
					page = new Page(num5, indexCount, 4U, this.m_MockDevice);
					bool flag7 = page3 != null;
					if (flag7)
					{
						page3.next = page;
					}
					else
					{
						this.m_FirstPage = page;
					}
				}
				alloc = page.vertices.allocator.Allocate(vertexCount, shortLived);
				alloc2 = page.indices.allocator.Allocate(indexCount, shortLived);
			}
			Debug.Assert(alloc.size == vertexCount, "Vertices allocated != Vertices requested");
			Debug.Assert(alloc2.size == indexCount, "Indices allocated != Indices requested");
			bool flag8 = alloc.size != vertexCount || alloc2.size != indexCount;
			if (flag8)
			{
				bool flag9 = alloc.handle != null;
				if (flag9)
				{
					page.vertices.allocator.Free(alloc);
				}
				bool flag10 = alloc2.handle != null;
				if (flag10)
				{
					page.vertices.allocator.Free(alloc2);
				}
				alloc2 = default(Alloc);
				alloc = default(Alloc);
			}
			page.vertices.RegisterUpdate(alloc.start, alloc.size);
			page.indices.RegisterUpdate(alloc2.start, alloc2.size);
			vertexData = new NativeSlice<Vertex>(page.vertices.cpuData, (int)alloc.start, (int)alloc.size);
			indexData = new NativeSlice<ushort>(page.indices.cpuData, (int)alloc2.start, (int)alloc2.size);
			meshHandle.allocPage = page;
			meshHandle.allocVerts = alloc;
			meshHandle.allocIndices = alloc2;
			meshHandle.allocTime = this.m_FrameIndex;
			UIRenderDevice.s_MarkerAllocate.End();
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00071028 File Offset: 0x0006F228
		private void UpdateAfterGPUUsedData(MeshHandle mesh, uint vertexCount, uint indexCount, out NativeSlice<Vertex> vertexData, out NativeSlice<ushort> indexData, out ushort indexOffset, out UIRenderDevice.AllocToUpdate allocToUpdate, bool copyBackIndices)
		{
			UIRenderDevice.AllocToUpdate allocToUpdate2 = default(UIRenderDevice.AllocToUpdate);
			uint nextUpdateID = this.m_NextUpdateID;
			this.m_NextUpdateID = nextUpdateID + 1U;
			allocToUpdate2.id = nextUpdateID;
			allocToUpdate2.allocTime = this.m_FrameIndex;
			allocToUpdate2.meshHandle = mesh;
			allocToUpdate2.copyBackIndices = copyBackIndices;
			allocToUpdate = allocToUpdate2;
			Debug.Assert(this.m_NextUpdateID > 0U);
			bool flag = mesh.updateAllocID == 0U;
			if (flag)
			{
				allocToUpdate.permAllocVerts = mesh.allocVerts;
				allocToUpdate.permAllocIndices = mesh.allocIndices;
				allocToUpdate.permPage = mesh.allocPage;
			}
			else
			{
				int num = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> list = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate allocToUpdate3 = list[num];
				Debug.Assert(allocToUpdate3.id == mesh.updateAllocID);
				allocToUpdate.copyBackIndices |= allocToUpdate3.copyBackIndices;
				allocToUpdate.permAllocVerts = allocToUpdate3.permAllocVerts;
				allocToUpdate.permAllocIndices = allocToUpdate3.permAllocIndices;
				allocToUpdate.permPage = allocToUpdate3.permPage;
				allocToUpdate3.allocTime = uint.MaxValue;
				list[num] = allocToUpdate3;
				List<UIRenderDevice.AllocToFree> list2 = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
			}
			bool flag2 = this.TryAllocFromPage(mesh.allocPage, vertexCount, indexCount, ref mesh.allocVerts, ref mesh.allocIndices, true);
			if (flag2)
			{
				mesh.allocPage.vertices.RegisterUpdate(mesh.allocVerts.start, mesh.allocVerts.size);
				mesh.allocPage.indices.RegisterUpdate(mesh.allocIndices.start, mesh.allocIndices.size);
			}
			else
			{
				this.Allocate(mesh, vertexCount, indexCount, out vertexData, out indexData, true);
			}
			mesh.triangleCount = indexCount / 3U;
			mesh.updateAllocID = allocToUpdate.id;
			mesh.allocTime = allocToUpdate.allocTime;
			this.m_Updates[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Updates.Count))].Add(allocToUpdate);
			vertexData = new NativeSlice<Vertex>(mesh.allocPage.vertices.cpuData, (int)mesh.allocVerts.start, (int)vertexCount);
			indexData = new NativeSlice<ushort>(mesh.allocPage.indices.cpuData, (int)mesh.allocIndices.start, (int)indexCount);
			indexOffset = (ushort)mesh.allocVerts.start;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00071318 File Offset: 0x0006F518
		public void Free(MeshHandle mesh)
		{
			bool flag = mesh.updateAllocID > 0U;
			if (flag)
			{
				int num = (int)(mesh.updateAllocID - 1U);
				List<UIRenderDevice.AllocToUpdate> list = this.m_Updates[(int)(mesh.allocTime % (uint)this.m_Updates.Count)];
				UIRenderDevice.AllocToUpdate allocToUpdate = list[num];
				Debug.Assert(allocToUpdate.id == mesh.updateAllocID);
				List<UIRenderDevice.AllocToFree> list2 = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = allocToUpdate.permAllocVerts,
					page = allocToUpdate.permPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = allocToUpdate.permAllocIndices,
					page = allocToUpdate.permPage,
					vertices = false
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocVerts,
					page = mesh.allocPage,
					vertices = true
				});
				list2.Add(new UIRenderDevice.AllocToFree
				{
					alloc = mesh.allocIndices,
					page = mesh.allocPage,
					vertices = false
				});
				allocToUpdate.allocTime = uint.MaxValue;
				list[num] = allocToUpdate;
			}
			else
			{
				bool flag2 = mesh.allocTime != this.m_FrameIndex;
				if (flag2)
				{
					int num2 = (int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count);
					this.m_DeferredFrees[num2].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocVerts,
						page = mesh.allocPage,
						vertices = true
					});
					this.m_DeferredFrees[num2].Add(new UIRenderDevice.AllocToFree
					{
						alloc = mesh.allocIndices,
						page = mesh.allocPage,
						vertices = false
					});
				}
				else
				{
					mesh.allocPage.vertices.allocator.Free(mesh.allocVerts);
					mesh.allocPage.indices.allocator.Free(mesh.allocIndices);
				}
			}
			mesh.allocVerts = default(Alloc);
			mesh.allocIndices = default(Alloc);
			mesh.allocPage = null;
			mesh.updateAllocID = 0U;
			this.m_MeshHandles.Return(mesh);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000715A0 File Offset: 0x0006F7A0
		private static Vector4 GetClipSpaceParams()
		{
			RectInt activeViewport = Utility.GetActiveViewport();
			return new Vector4((float)activeViewport.width * 0.5f, (float)activeViewport.height * 0.5f, 2f / (float)activeViewport.width, 2f / (float)activeViewport.height);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000715F8 File Offset: 0x0006F7F8
		public void OnFrameRenderingBegin()
		{
			this.AdvanceFrame();
			this.m_DrawStats = default(UIRenderDevice.DrawStatistics);
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			UIRenderDevice.s_MarkerBeforeDraw.Begin();
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				page.vertices.SendUpdates();
				page.indices.SendUpdates();
			}
			UIRenderDevice.s_MarkerBeforeDraw.End();
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00071674 File Offset: 0x0006F874
		private unsafe static NativeSlice<T> PtrToSlice<T>(void* p, int count) where T : struct
		{
			return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>(p, UnsafeUtility.SizeOf<T>(), count);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00071694 File Offset: 0x0006F894
		[MethodImpl(256)]
		private void ApplyDrawCommandState(RenderChainCommand cmd, int textureSlot, Material newMat, bool newMatDiffers, bool newFontDiffers, ref UIRenderDevice.EvaluationState st)
		{
			if (newMatDiffers)
			{
				st.curState.material = newMat;
				st.mustApplyMaterial = true;
			}
			st.curPage = cmd.mesh.allocPage;
			bool flag = cmd.state.texture != TextureId.invalid;
			if (flag)
			{
				bool flag2 = textureSlot < 0;
				if (flag2)
				{
					textureSlot = this.m_TextureSlotManager.FindOldestSlot();
					this.m_TextureSlotManager.Bind(cmd.state.texture, textureSlot, st.stateMatProps);
					st.mustApplyStateBlock = true;
				}
				else
				{
					this.m_TextureSlotManager.MarkUsed(textureSlot);
				}
			}
			if (newFontDiffers)
			{
				st.mustApplyStateBlock = true;
				st.curState.font = cmd.state.font;
				st.stateMatProps.SetTexture(UIRenderDevice.s_FontTexPropID, cmd.state.font);
				st.curState.fontTexSDFScale = cmd.state.fontTexSDFScale;
				st.stateMatProps.SetFloat(UIRenderDevice.s_FontTexSDFScaleID, st.curState.fontTexSDFScale);
			}
			bool flag3 = cmd.state.stencilRef != st.curState.stencilRef;
			if (flag3)
			{
				st.curState.stencilRef = cmd.state.stencilRef;
				st.mustApplyStencil = true;
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000717F8 File Offset: 0x0006F9F8
		private void ApplyBatchState(ref UIRenderDevice.EvaluationState st, bool allowMaterialChange)
		{
			bool flag = !this.m_MockDevice;
			if (flag)
			{
				bool mustApplyMaterial = st.mustApplyMaterial;
				if (mustApplyMaterial)
				{
					bool flag2 = !allowMaterialChange;
					if (flag2)
					{
						Debug.LogError("Attempted to change material when it is not allowed to do so.");
						return;
					}
					this.m_DrawStats.materialSetCount = this.m_DrawStats.materialSetCount + 1U;
					st.curState.material.SetPass(0);
					bool flag3 = this.m_StandardMatProps != null;
					if (flag3)
					{
						Utility.SetPropertyBlock(this.m_StandardMatProps);
					}
					st.mustApplyCommonBlock = true;
					st.mustApplyStateBlock = true;
					st.mustApplyStencil = true;
				}
				bool mustApplyStateBlock = st.mustApplyStateBlock;
				if (mustApplyStateBlock)
				{
					Utility.SetPropertyBlock(st.stateMatProps);
				}
				bool mustApplyStencil = st.mustApplyStencil;
				if (mustApplyStencil)
				{
					this.m_DrawStats.stencilRefChanges = this.m_DrawStats.stencilRefChanges + 1U;
					Utility.SetStencilState(this.m_DefaultStencilState, st.curState.stencilRef);
				}
			}
			st.mustApplyMaterial = false;
			st.mustApplyCommonBlock = false;
			st.mustApplyStateBlock = false;
			st.mustApplyStencil = false;
			this.m_TextureSlotManager.StartNewBatch();
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00071904 File Offset: 0x0006FB04
		public unsafe void EvaluateChain(RenderChainCommand head, Material initialMat, Material defaultMat, Texture gradientSettings, Texture shaderInfo, float pixelsPerPoint, NativeSlice<Transform3x4> transforms, NativeSlice<Vector4> clipRects, MaterialPropertyBlock stateMatProps, bool allowMaterialChange, ref Exception immediateException)
		{
			Utility.ProfileDrawChainBegin();
			bool breakBatches = this.breakBatches;
			DrawParams drawParams = this.m_DrawParams;
			drawParams.Reset();
			drawParams.renderTexture.Add(RenderTexture.active);
			stateMatProps.Clear();
			this.m_TextureSlotManager.Reset();
			bool fullyCreated = this.fullyCreated;
			if (fullyCreated)
			{
				bool flag = head != null && head.state.fontTexSDFScale != 0f;
				if (flag)
				{
					this.m_StandardMatProps.SetFloat(UIRenderDevice.s_FontTexSDFScaleID, head.state.fontTexSDFScale);
				}
				bool flag2 = gradientSettings != null;
				if (flag2)
				{
					this.m_StandardMatProps.SetTexture(UIRenderDevice.s_GradientSettingsTexID, gradientSettings);
				}
				bool flag3 = shaderInfo != null;
				if (flag3)
				{
					this.m_StandardMatProps.SetTexture(UIRenderDevice.s_ShaderInfoTexID, shaderInfo);
				}
				bool flag4 = transforms.Length > 0;
				if (flag4)
				{
					Utility.SetVectorArray<Transform3x4>(this.m_StandardMatProps, UIRenderDevice.s_TransformsPropID, transforms);
				}
				bool flag5 = clipRects.Length > 0;
				if (flag5)
				{
					Utility.SetVectorArray<Vector4>(this.m_StandardMatProps, UIRenderDevice.s_ClipRectsPropID, clipRects);
				}
				this.m_StandardMatProps.SetVector(UIRenderDevice.s_ClipSpaceParamsID, UIRenderDevice.GetClipSpaceParams());
				Utility.SetPropertyBlock(this.m_StandardMatProps);
			}
			int num = 1024;
			DrawBufferRange* ptr;
			checked
			{
				ptr = stackalloc DrawBufferRange[unchecked((UIntPtr)num) * (UIntPtr)sizeof(DrawBufferRange)];
			}
			int num2 = num - 1;
			int num3 = 0;
			int num4 = 0;
			DrawBufferRange drawBufferRange = default(DrawBufferRange);
			int num5 = -1;
			UIRenderDevice.EvaluationState evaluationState = new UIRenderDevice.EvaluationState
			{
				stateMatProps = stateMatProps,
				defaultMat = defaultMat,
				curState = new State
				{
					material = initialMat
				},
				mustApplyCommonBlock = true,
				mustApplyStateBlock = true,
				mustApplyStencil = true
			};
			while (head != null)
			{
				this.m_DrawStats.commandCount = this.m_DrawStats.commandCount + 1U;
				this.m_DrawStats.drawCommandCount = this.m_DrawStats.drawCommandCount + ((head.type == CommandType.Draw) ? 1U : 0U);
				bool flag6 = drawBufferRange.indexCount > 0 && num4 == num - 1;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				int num6 = -1;
				Material material = null;
				bool flag10 = false;
				bool flag11 = false;
				bool flag12 = head.type == CommandType.Draw;
				if (flag12)
				{
					material = ((head.state.material != null) ? head.state.material : defaultMat);
					bool flag13 = material != evaluationState.curState.material;
					if (flag13)
					{
						flag9 = true;
						flag10 = true;
						flag7 = true;
						flag8 = true;
					}
					bool flag14 = head.mesh.allocPage != evaluationState.curPage;
					if (flag14)
					{
						flag9 = true;
						flag7 = true;
						flag8 = true;
					}
					else
					{
						bool flag15 = (long)num5 != (long)((ulong)head.mesh.allocIndices.start + (ulong)((long)head.indexOffset));
						if (flag15)
						{
							flag7 = true;
						}
					}
					bool flag16 = head.state.texture != TextureId.invalid;
					if (flag16)
					{
						flag9 = true;
						num6 = this.m_TextureSlotManager.IndexOf(head.state.texture);
						bool flag17 = num6 < 0 && this.m_TextureSlotManager.FreeSlots < 1;
						if (flag17)
						{
							flag7 = true;
							flag8 = true;
						}
					}
					bool flag18 = head.state.font != null && head.state.font != evaluationState.curState.font;
					if (flag18)
					{
						flag9 = true;
						flag11 = true;
						flag7 = true;
						flag8 = true;
					}
					bool flag19 = head.state.stencilRef != evaluationState.curState.stencilRef;
					if (flag19)
					{
						flag9 = true;
						flag7 = true;
						flag8 = true;
					}
					bool flag20 = flag7 && flag6;
					if (flag20)
					{
						flag8 = true;
					}
				}
				else
				{
					flag7 = true;
					flag8 = true;
				}
				bool flag21 = breakBatches;
				if (flag21)
				{
					flag7 = true;
					flag8 = true;
				}
				bool flag22 = flag7;
				if (flag22)
				{
					bool flag23 = drawBufferRange.indexCount > 0;
					if (flag23)
					{
						int num7 = (num3 + num4++) & num2;
						ptr[num7] = drawBufferRange;
						Debug.Assert(num4 < num || flag8);
						drawBufferRange = default(DrawBufferRange);
						this.m_DrawStats.drawRangeCount = this.m_DrawStats.drawRangeCount + 1U;
					}
					bool flag24 = head.type == CommandType.Draw;
					if (flag24)
					{
						drawBufferRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset);
						drawBufferRange.indexCount = head.indexCount;
						drawBufferRange.vertsReferenced = (int)head.mesh.allocVerts.size;
						drawBufferRange.minIndexVal = (int)head.mesh.allocVerts.start;
						num5 = drawBufferRange.firstIndex + head.indexCount;
						this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
					}
					bool flag25 = flag8;
					if (flag25)
					{
						bool flag26 = num4 > 0;
						if (flag26)
						{
							this.ApplyBatchState(ref evaluationState, allowMaterialChange);
							this.KickRanges(ptr, ref num4, ref num3, num, evaluationState.curPage);
						}
						bool flag27 = head.type > CommandType.Draw;
						if (flag27)
						{
							bool flag28 = !this.m_MockDevice;
							if (flag28)
							{
								head.ExecuteNonDrawMesh(drawParams, pixelsPerPoint, ref immediateException);
							}
							bool flag29 = head.type == CommandType.Immediate || head.type == CommandType.ImmediateCull || head.type == CommandType.BlitToPreviousRT || head.type == CommandType.PushRenderTexture || head.type == CommandType.PopDefaultMaterial || head.type == CommandType.PushDefaultMaterial;
							if (flag29)
							{
								evaluationState.curState.material = null;
								evaluationState.mustApplyMaterial = false;
								this.m_DrawStats.immediateDraws = this.m_DrawStats.immediateDraws + 1U;
								bool flag30 = head.type == CommandType.PopDefaultMaterial;
								if (flag30)
								{
									int num8 = drawParams.defaultMaterial.Count - 1;
									defaultMat = drawParams.defaultMaterial[num8];
									drawParams.defaultMaterial.RemoveAt(num8);
								}
								bool flag31 = head.type == CommandType.PushDefaultMaterial;
								if (flag31)
								{
									drawParams.defaultMaterial.Add(defaultMat);
									defaultMat = head.state.material;
								}
							}
						}
					}
					bool flag32 = head.type == CommandType.Draw && flag9;
					if (flag32)
					{
						this.ApplyDrawCommandState(head, num6, material, flag10, flag11, ref evaluationState);
					}
					head = head.next;
				}
				else
				{
					bool flag33 = drawBufferRange.indexCount == 0;
					if (flag33)
					{
						num5 = (drawBufferRange.firstIndex = (int)(head.mesh.allocIndices.start + (uint)head.indexOffset));
					}
					drawBufferRange.indexCount += head.indexCount;
					int minIndexVal = drawBufferRange.minIndexVal;
					int start = (int)head.mesh.allocVerts.start;
					int num9 = drawBufferRange.minIndexVal + drawBufferRange.vertsReferenced;
					int num10 = (int)(head.mesh.allocVerts.start + head.mesh.allocVerts.size);
					drawBufferRange.minIndexVal = Mathf.Min(minIndexVal, start);
					drawBufferRange.vertsReferenced = Mathf.Max(num9, num10) - drawBufferRange.minIndexVal;
					num5 += head.indexCount;
					this.m_DrawStats.totalIndices = this.m_DrawStats.totalIndices + (uint)head.indexCount;
					bool flag34 = flag9;
					if (flag34)
					{
						this.ApplyDrawCommandState(head, num6, material, flag10, flag11, ref evaluationState);
					}
					head = head.next;
				}
			}
			bool flag35 = drawBufferRange.indexCount > 0;
			if (flag35)
			{
				int num11 = (num3 + num4++) & num2;
				ptr[num11] = drawBufferRange;
			}
			bool flag36 = num4 > 0;
			if (flag36)
			{
				this.ApplyBatchState(ref evaluationState, allowMaterialChange);
				this.KickRanges(ptr, ref num4, ref num3, num, evaluationState.curPage);
			}
			this.UpdateFenceValue();
			Utility.ProfileDrawChainEnd();
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000720B0 File Offset: 0x000702B0
		private unsafe void UpdateFenceValue()
		{
			bool flag = this.m_Fences != null;
			if (flag)
			{
				uint num = Utility.InsertCPUFence();
				fixed (uint* ptr = &this.m_Fences[(int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length))])
				{
					uint* ptr2 = ptr;
					bool flag3;
					do
					{
						uint num2 = *ptr2;
						bool flag2 = num - num2 <= 0U;
						if (flag2)
						{
							break;
						}
						int num3 = Interlocked.CompareExchange(ref *(int*)ptr2, (int)num, (int)num2);
						flag3 = (long)num3 == (long)((ulong)num2);
					}
					while (!flag3);
				}
			}
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00072130 File Offset: 0x00070330
		private unsafe void KickRanges(DrawBufferRange* ranges, ref int rangesReady, ref int rangesStart, int rangesCount, Page curPage)
		{
			Debug.Assert(rangesReady > 0);
			bool flag = rangesStart + rangesReady <= rangesCount;
			if (flag)
			{
				bool flag2 = !this.m_MockDevice;
				if (flag2)
				{
					this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), rangesReady));
				}
				this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 1U;
			}
			else
			{
				int num = rangesCount - rangesStart;
				int num2 = rangesReady - num;
				bool flag3 = !this.m_MockDevice;
				if (flag3)
				{
					this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)(ranges + rangesStart), num));
					this.DrawRanges<ushort, Vertex>(curPage.indices.gpuData, curPage.vertices.gpuData, UIRenderDevice.PtrToSlice<DrawBufferRange>((void*)ranges, num2));
				}
				this.m_DrawStats.drawRangeCallCount = this.m_DrawStats.drawRangeCallCount + 2U;
			}
			rangesStart = (rangesStart + rangesReady) & (rangesCount - 1);
			rangesReady = 0;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00072244 File Offset: 0x00070444
		private unsafe void DrawRanges<I, T>(Utility.GPUBuffer<I> ib, Utility.GPUBuffer<T> vb, NativeSlice<DrawBufferRange> ranges) where I : struct where T : struct
		{
			checked
			{
				IntPtr* ptr = stackalloc IntPtr[unchecked((UIntPtr)1) * (UIntPtr)sizeof(IntPtr)];
				*ptr = vb.BufferPointer;
				Utility.DrawRanges(ib.BufferPointer, ptr, 1, new IntPtr(ranges.GetUnsafePtr<DrawBufferRange>()), ranges.Length, this.m_VertexDecl);
			}
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0007228C File Offset: 0x0007048C
		internal void WaitOnAllCpuFences()
		{
			for (int i = 0; i < this.m_Fences.Length; i++)
			{
				this.WaitOnCpuFence(this.m_Fences[i]);
			}
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000722C0 File Offset: 0x000704C0
		private void WaitOnCpuFence(uint fence)
		{
			bool flag = fence != 0U && !Utility.CPUFencePassed(fence);
			if (flag)
			{
				UIRenderDevice.s_MarkerFence.Begin();
				Utility.WaitForCPUFencePassed(fence);
				UIRenderDevice.s_MarkerFence.End();
			}
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00072300 File Offset: 0x00070500
		public void AdvanceFrame()
		{
			UIRenderDevice.s_MarkerAdvanceFrame.Begin();
			this.m_FrameIndex += 1U;
			this.m_DrawStats.currentFrameIndex = (int)this.m_FrameIndex;
			bool flag = this.m_Fences != null;
			if (flag)
			{
				int num = (int)((ulong)this.m_FrameIndex % (ulong)((long)this.m_Fences.Length));
				uint num2 = this.m_Fences[num];
				this.WaitOnCpuFence(num2);
				this.m_Fences[num] = 0U;
			}
			this.m_NextUpdateID = 1U;
			List<UIRenderDevice.AllocToFree> list = this.m_DeferredFrees[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToFree allocToFree in list)
			{
				bool vertices = allocToFree.vertices;
				if (vertices)
				{
					allocToFree.page.vertices.allocator.Free(allocToFree.alloc);
				}
				else
				{
					allocToFree.page.indices.allocator.Free(allocToFree.alloc);
				}
			}
			list.Clear();
			List<UIRenderDevice.AllocToUpdate> list2 = this.m_Updates[(int)(this.m_FrameIndex % (uint)this.m_DeferredFrees.Count)];
			foreach (UIRenderDevice.AllocToUpdate allocToUpdate in list2)
			{
				bool flag2 = allocToUpdate.meshHandle.updateAllocID == allocToUpdate.id && allocToUpdate.meshHandle.allocTime == allocToUpdate.allocTime;
				if (flag2)
				{
					NativeSlice<Vertex> nativeSlice = new NativeSlice<Vertex>(allocToUpdate.meshHandle.allocPage.vertices.cpuData, (int)allocToUpdate.meshHandle.allocVerts.start, (int)allocToUpdate.meshHandle.allocVerts.size);
					NativeSlice<Vertex> nativeSlice2 = new NativeSlice<Vertex>(allocToUpdate.permPage.vertices.cpuData, (int)allocToUpdate.permAllocVerts.start, (int)allocToUpdate.meshHandle.allocVerts.size);
					nativeSlice2.CopyFrom(nativeSlice);
					allocToUpdate.permPage.vertices.RegisterUpdate(allocToUpdate.permAllocVerts.start, allocToUpdate.meshHandle.allocVerts.size);
					bool copyBackIndices = allocToUpdate.copyBackIndices;
					if (copyBackIndices)
					{
						NativeSlice<ushort> nativeSlice3 = new NativeSlice<ushort>(allocToUpdate.meshHandle.allocPage.indices.cpuData, (int)allocToUpdate.meshHandle.allocIndices.start, (int)allocToUpdate.meshHandle.allocIndices.size);
						NativeSlice<ushort> nativeSlice4 = new NativeSlice<ushort>(allocToUpdate.permPage.indices.cpuData, (int)allocToUpdate.permAllocIndices.start, (int)allocToUpdate.meshHandle.allocIndices.size);
						int length = nativeSlice4.Length;
						int num3 = (int)(allocToUpdate.permAllocVerts.start - allocToUpdate.meshHandle.allocVerts.start);
						for (int i = 0; i < length; i++)
						{
							nativeSlice4[i] = (ushort)((int)nativeSlice3[i] + num3);
						}
						allocToUpdate.permPage.indices.RegisterUpdate(allocToUpdate.permAllocIndices.start, allocToUpdate.meshHandle.allocIndices.size);
					}
					list.Add(new UIRenderDevice.AllocToFree
					{
						alloc = allocToUpdate.meshHandle.allocVerts,
						page = allocToUpdate.meshHandle.allocPage,
						vertices = true
					});
					list.Add(new UIRenderDevice.AllocToFree
					{
						alloc = allocToUpdate.meshHandle.allocIndices,
						page = allocToUpdate.meshHandle.allocPage,
						vertices = false
					});
					allocToUpdate.meshHandle.allocVerts = allocToUpdate.permAllocVerts;
					allocToUpdate.meshHandle.allocIndices = allocToUpdate.permAllocIndices;
					allocToUpdate.meshHandle.allocPage = allocToUpdate.permPage;
					allocToUpdate.meshHandle.updateAllocID = 0U;
				}
			}
			list2.Clear();
			this.PruneUnusedPages();
			UIRenderDevice.s_MarkerAdvanceFrame.End();
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00072774 File Offset: 0x00070974
		private void PruneUnusedPages()
		{
			Page page4;
			Page page3;
			Page page2;
			Page page = (page2 = (page3 = (page4 = null)));
			Page next;
			for (Page page5 = this.m_FirstPage; page5 != null; page5 = next)
			{
				bool flag = !page5.isEmpty;
				if (flag)
				{
					page5.framesEmpty = 0;
				}
				else
				{
					page5.framesEmpty++;
				}
				bool flag2 = page5.framesEmpty < 60;
				if (flag2)
				{
					bool flag3 = page2 != null;
					if (flag3)
					{
						page.next = page5;
					}
					else
					{
						page2 = page5;
					}
					page = page5;
				}
				else
				{
					bool flag4 = page3 != null;
					if (flag4)
					{
						page4.next = page5;
					}
					else
					{
						page3 = page5;
					}
					page4 = page5;
				}
				next = page5.next;
				page5.next = null;
			}
			this.m_FirstPage = page2;
			Page next2;
			for (Page page5 = page3; page5 != null; page5 = next2)
			{
				next2 = page5.next;
				page5.next = null;
				page5.Dispose();
			}
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00072854 File Offset: 0x00070A54
		internal static void PrepareForGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount++;
			bool flag = UIRenderDevice.s_DefaultShaderInfoTexFloat != null;
			if (flag)
			{
				UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexFloat);
				UIRenderDevice.s_DefaultShaderInfoTexFloat = null;
			}
			bool flag2 = UIRenderDevice.s_DefaultShaderInfoTexARGB8 != null;
			if (flag2)
			{
				UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexARGB8);
				UIRenderDevice.s_DefaultShaderInfoTexARGB8 = null;
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000728B2 File Offset: 0x00070AB2
		internal static void WrapUpGfxDeviceRecreate()
		{
			UIRenderDevice.m_ActiveDeviceCount--;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000728C1 File Offset: 0x00070AC1
		internal static void FlushAllPendingDeviceDisposes()
		{
			Utility.SyncRenderThread();
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000728D0 File Offset: 0x00070AD0
		internal UIRenderDevice.AllocationStatistics GatherAllocationStatistics()
		{
			UIRenderDevice.AllocationStatistics allocationStatistics = default(UIRenderDevice.AllocationStatistics);
			allocationStatistics.completeInit = this.fullyCreated;
			allocationStatistics.freesDeferred = new int[this.m_DeferredFrees.Count];
			for (int i = 0; i < this.m_DeferredFrees.Count; i++)
			{
				allocationStatistics.freesDeferred[i] = this.m_DeferredFrees[i].Count;
			}
			int num = 0;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				num++;
			}
			allocationStatistics.pages = new UIRenderDevice.AllocationStatistics.PageStatistics[num];
			num = 0;
			for (Page page = this.m_FirstPage; page != null; page = page.next)
			{
				allocationStatistics.pages[num].vertices = page.vertices.allocator.GatherStatistics();
				allocationStatistics.pages[num].indices = page.indices.allocator.GatherStatistics();
				num++;
			}
			return allocationStatistics;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000729DC File Offset: 0x00070BDC
		internal UIRenderDevice.DrawStatistics GatherDrawStatistics()
		{
			return this.m_DrawStats;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x000729F4 File Offset: 0x00070BF4
		private static void ProcessDeviceFreeQueue()
		{
			UIRenderDevice.s_MarkerFree.Begin();
			bool synchronousFree = UIRenderDevice.m_SynchronousFree;
			if (synchronousFree)
			{
				Utility.SyncRenderThread();
			}
			for (LinkedListNode<UIRenderDevice.DeviceToFree> linkedListNode = UIRenderDevice.m_DeviceFreeQueue.First; linkedListNode != null; linkedListNode = UIRenderDevice.m_DeviceFreeQueue.First)
			{
				bool flag = !Utility.CPUFencePassed(linkedListNode.Value.handle);
				if (flag)
				{
					break;
				}
				linkedListNode.Value.Dispose();
				UIRenderDevice.m_DeviceFreeQueue.RemoveFirst();
			}
			Debug.Assert(!UIRenderDevice.m_SynchronousFree || UIRenderDevice.m_DeviceFreeQueue.Count == 0);
			bool flag2 = UIRenderDevice.m_ActiveDeviceCount == 0 && UIRenderDevice.m_SubscribedToNotifications;
			if (flag2)
			{
				bool flag3 = UIRenderDevice.s_DefaultShaderInfoTexFloat != null;
				if (flag3)
				{
					UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexFloat);
					UIRenderDevice.s_DefaultShaderInfoTexFloat = null;
				}
				bool flag4 = UIRenderDevice.s_DefaultShaderInfoTexARGB8 != null;
				if (flag4)
				{
					UIRUtility.Destroy(UIRenderDevice.s_DefaultShaderInfoTexARGB8);
					UIRenderDevice.s_DefaultShaderInfoTexARGB8 = null;
				}
				Utility.NotifyOfUIREvents(false);
				UIRenderDevice.m_SubscribedToNotifications = false;
			}
			UIRenderDevice.s_MarkerFree.End();
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00072B07 File Offset: 0x00070D07
		private static void OnEngineUpdateGlobal()
		{
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00072B10 File Offset: 0x00070D10
		private static void OnFlushPendingResources()
		{
			UIRenderDevice.m_SynchronousFree = true;
			UIRenderDevice.ProcessDeviceFreeQueue();
		}

		// Token: 0x04000C20 RID: 3104
		internal const uint k_MaxQueuedFrameCount = 4U;

		// Token: 0x04000C21 RID: 3105
		internal const int k_PruneEmptyPageFrameCount = 60;

		// Token: 0x04000C22 RID: 3106
		private readonly bool m_MockDevice;

		// Token: 0x04000C23 RID: 3107
		private IntPtr m_DefaultStencilState;

		// Token: 0x04000C24 RID: 3108
		private IntPtr m_VertexDecl;

		// Token: 0x04000C25 RID: 3109
		private Page m_FirstPage;

		// Token: 0x04000C26 RID: 3110
		private uint m_NextPageVertexCount;

		// Token: 0x04000C27 RID: 3111
		private uint m_LargeMeshVertexCount;

		// Token: 0x04000C28 RID: 3112
		private float m_IndexToVertexCountRatio;

		// Token: 0x04000C29 RID: 3113
		private List<List<UIRenderDevice.AllocToFree>> m_DeferredFrees;

		// Token: 0x04000C2A RID: 3114
		private List<List<UIRenderDevice.AllocToUpdate>> m_Updates;

		// Token: 0x04000C2B RID: 3115
		private uint[] m_Fences;

		// Token: 0x04000C2C RID: 3116
		private MaterialPropertyBlock m_StandardMatProps;

		// Token: 0x04000C2D RID: 3117
		private uint m_FrameIndex;

		// Token: 0x04000C2E RID: 3118
		private uint m_NextUpdateID = 1U;

		// Token: 0x04000C2F RID: 3119
		private UIRenderDevice.DrawStatistics m_DrawStats;

		// Token: 0x04000C30 RID: 3120
		private readonly LinkedPool<MeshHandle> m_MeshHandles = new LinkedPool<MeshHandle>(() => new MeshHandle(), delegate(MeshHandle mh)
		{
		}, 10000);

		// Token: 0x04000C31 RID: 3121
		private readonly DrawParams m_DrawParams = new DrawParams();

		// Token: 0x04000C32 RID: 3122
		private readonly TextureSlotManager m_TextureSlotManager = new TextureSlotManager();

		// Token: 0x04000C33 RID: 3123
		private static LinkedList<UIRenderDevice.DeviceToFree> m_DeviceFreeQueue = new LinkedList<UIRenderDevice.DeviceToFree>();

		// Token: 0x04000C34 RID: 3124
		private static int m_ActiveDeviceCount = 0;

		// Token: 0x04000C35 RID: 3125
		private static bool m_SubscribedToNotifications;

		// Token: 0x04000C36 RID: 3126
		private static bool m_SynchronousFree;

		// Token: 0x04000C37 RID: 3127
		private static readonly int s_FontTexPropID = Shader.PropertyToID("_FontTex");

		// Token: 0x04000C38 RID: 3128
		private static readonly int s_FontTexSDFScaleID = Shader.PropertyToID("_FontTexSDFScale");

		// Token: 0x04000C39 RID: 3129
		private static readonly int s_GradientSettingsTexID = Shader.PropertyToID("_GradientSettingsTex");

		// Token: 0x04000C3A RID: 3130
		private static readonly int s_ShaderInfoTexID = Shader.PropertyToID("_ShaderInfoTex");

		// Token: 0x04000C3B RID: 3131
		private static readonly int s_TransformsPropID = Shader.PropertyToID("_Transforms");

		// Token: 0x04000C3C RID: 3132
		private static readonly int s_ClipRectsPropID = Shader.PropertyToID("_ClipRects");

		// Token: 0x04000C3D RID: 3133
		private static readonly int s_ClipSpaceParamsID = Shader.PropertyToID("_ClipSpaceParams");

		// Token: 0x04000C3E RID: 3134
		private static ProfilerMarker s_MarkerAllocate = new ProfilerMarker("UIR.Allocate");

		// Token: 0x04000C3F RID: 3135
		private static ProfilerMarker s_MarkerFree = new ProfilerMarker("UIR.Free");

		// Token: 0x04000C40 RID: 3136
		private static ProfilerMarker s_MarkerAdvanceFrame = new ProfilerMarker("UIR.AdvanceFrame");

		// Token: 0x04000C41 RID: 3137
		private static ProfilerMarker s_MarkerFence = new ProfilerMarker("UIR.WaitOnFence");

		// Token: 0x04000C42 RID: 3138
		private static ProfilerMarker s_MarkerBeforeDraw = new ProfilerMarker("UIR.BeforeDraw");

		// Token: 0x04000C43 RID: 3139
		private static bool? s_VertexTexturingIsAvailable;

		// Token: 0x04000C44 RID: 3140
		private const string k_VertexTexturingIsAvailableTag = "UIE_VertexTexturingIsAvailable";

		// Token: 0x04000C45 RID: 3141
		private const string k_VertexTexturingIsAvailableTrue = "1";

		// Token: 0x04000C46 RID: 3142
		private static bool? s_ShaderModelIs35;

		// Token: 0x04000C47 RID: 3143
		private const string k_ShaderModelIs35Tag = "UIE_ShaderModelIs35";

		// Token: 0x04000C48 RID: 3144
		private const string k_ShaderModelIs35True = "1";

		// Token: 0x04000C4B RID: 3147
		private static Texture2D s_DefaultShaderInfoTexFloat;

		// Token: 0x04000C4C RID: 3148
		private static Texture2D s_DefaultShaderInfoTexARGB8;

		// Token: 0x02000330 RID: 816
		internal struct AllocToUpdate
		{
			// Token: 0x04000C4E RID: 3150
			public uint id;

			// Token: 0x04000C4F RID: 3151
			public uint allocTime;

			// Token: 0x04000C50 RID: 3152
			public MeshHandle meshHandle;

			// Token: 0x04000C51 RID: 3153
			public Alloc permAllocVerts;

			// Token: 0x04000C52 RID: 3154
			public Alloc permAllocIndices;

			// Token: 0x04000C53 RID: 3155
			public Page permPage;

			// Token: 0x04000C54 RID: 3156
			public bool copyBackIndices;
		}

		// Token: 0x02000331 RID: 817
		private struct AllocToFree
		{
			// Token: 0x04000C55 RID: 3157
			public Alloc alloc;

			// Token: 0x04000C56 RID: 3158
			public Page page;

			// Token: 0x04000C57 RID: 3159
			public bool vertices;
		}

		// Token: 0x02000332 RID: 818
		private struct DeviceToFree
		{
			// Token: 0x06001A6A RID: 6762 RVA: 0x00072B20 File Offset: 0x00070D20
			public void Dispose()
			{
				while (this.page != null)
				{
					Page page = this.page;
					this.page = this.page.next;
					page.Dispose();
				}
			}

			// Token: 0x04000C58 RID: 3160
			public uint handle;

			// Token: 0x04000C59 RID: 3161
			public Page page;
		}

		// Token: 0x02000333 RID: 819
		private struct EvaluationState
		{
			// Token: 0x04000C5A RID: 3162
			public MaterialPropertyBlock stateMatProps;

			// Token: 0x04000C5B RID: 3163
			public Material defaultMat;

			// Token: 0x04000C5C RID: 3164
			public State curState;

			// Token: 0x04000C5D RID: 3165
			public Page curPage;

			// Token: 0x04000C5E RID: 3166
			public bool mustApplyMaterial;

			// Token: 0x04000C5F RID: 3167
			public bool mustApplyCommonBlock;

			// Token: 0x04000C60 RID: 3168
			public bool mustApplyStateBlock;

			// Token: 0x04000C61 RID: 3169
			public bool mustApplyStencil;
		}

		// Token: 0x02000334 RID: 820
		internal struct AllocationStatistics
		{
			// Token: 0x04000C62 RID: 3170
			public UIRenderDevice.AllocationStatistics.PageStatistics[] pages;

			// Token: 0x04000C63 RID: 3171
			public int[] freesDeferred;

			// Token: 0x04000C64 RID: 3172
			public bool completeInit;

			// Token: 0x02000335 RID: 821
			public struct PageStatistics
			{
				// Token: 0x04000C65 RID: 3173
				internal HeapStatistics vertices;

				// Token: 0x04000C66 RID: 3174
				internal HeapStatistics indices;
			}
		}

		// Token: 0x02000336 RID: 822
		internal struct DrawStatistics
		{
			// Token: 0x04000C67 RID: 3175
			public int currentFrameIndex;

			// Token: 0x04000C68 RID: 3176
			public uint totalIndices;

			// Token: 0x04000C69 RID: 3177
			public uint commandCount;

			// Token: 0x04000C6A RID: 3178
			public uint drawCommandCount;

			// Token: 0x04000C6B RID: 3179
			public uint materialSetCount;

			// Token: 0x04000C6C RID: 3180
			public uint drawRangeCount;

			// Token: 0x04000C6D RID: 3181
			public uint drawRangeCallCount;

			// Token: 0x04000C6E RID: 3182
			public uint immediateDraws;

			// Token: 0x04000C6F RID: 3183
			public uint stencilRefChanges;
		}
	}
}

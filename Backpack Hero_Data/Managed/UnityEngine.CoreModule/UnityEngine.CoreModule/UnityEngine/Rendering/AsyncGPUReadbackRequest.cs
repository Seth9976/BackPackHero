using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000398 RID: 920
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/AsyncGPUReadbackManaged.h")]
	[NativeHeader("Runtime/Graphics/Texture.h")]
	public struct AsyncGPUReadbackRequest
	{
		// Token: 0x06001EF5 RID: 7925 RVA: 0x00032603 File Offset: 0x00030803
		public void Update()
		{
			AsyncGPUReadbackRequest.Update_Injected(ref this);
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0003260B File Offset: 0x0003080B
		public void WaitForCompletion()
		{
			AsyncGPUReadbackRequest.WaitForCompletion_Injected(ref this);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00032614 File Offset: 0x00030814
		public unsafe NativeArray<T> GetData<T>(int layer = 0) where T : struct
		{
			bool flag = !this.done || this.hasError;
			if (flag)
			{
				throw new InvalidOperationException("Cannot access the data as it is not available");
			}
			bool flag2 = layer < 0 || layer >= this.layerCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Layer index is out of range {0} / {1}", layer, this.layerCount));
			}
			int num = UnsafeUtility.SizeOf<T>();
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)this.GetDataRaw(layer), this.layerDataSize / num, Allocator.None);
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x000326A4 File Offset: 0x000308A4
		public bool done
		{
			get
			{
				return this.IsDone();
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x000326BC File Offset: 0x000308BC
		public bool hasError
		{
			get
			{
				return this.HasError();
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x000326D4 File Offset: 0x000308D4
		public int layerCount
		{
			get
			{
				return this.GetLayerCount();
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x000326EC File Offset: 0x000308EC
		public int layerDataSize
		{
			get
			{
				return this.GetLayerDataSize();
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x00032704 File Offset: 0x00030904
		public int width
		{
			get
			{
				return this.GetWidth();
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x0003271C File Offset: 0x0003091C
		public int height
		{
			get
			{
				return this.GetHeight();
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00032734 File Offset: 0x00030934
		public int depth
		{
			get
			{
				return this.GetDepth();
			}
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0003274C File Offset: 0x0003094C
		private bool IsDone()
		{
			return AsyncGPUReadbackRequest.IsDone_Injected(ref this);
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x00032754 File Offset: 0x00030954
		private bool HasError()
		{
			return AsyncGPUReadbackRequest.HasError_Injected(ref this);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0003275C File Offset: 0x0003095C
		private int GetLayerCount()
		{
			return AsyncGPUReadbackRequest.GetLayerCount_Injected(ref this);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x00032764 File Offset: 0x00030964
		private int GetLayerDataSize()
		{
			return AsyncGPUReadbackRequest.GetLayerDataSize_Injected(ref this);
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0003276C File Offset: 0x0003096C
		private int GetWidth()
		{
			return AsyncGPUReadbackRequest.GetWidth_Injected(ref this);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00032774 File Offset: 0x00030974
		private int GetHeight()
		{
			return AsyncGPUReadbackRequest.GetHeight_Injected(ref this);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0003277C File Offset: 0x0003097C
		private int GetDepth()
		{
			return AsyncGPUReadbackRequest.GetDepth_Injected(ref this);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00032784 File Offset: 0x00030984
		internal void SetScriptingCallback(Action<AsyncGPUReadbackRequest> callback)
		{
			AsyncGPUReadbackRequest.SetScriptingCallback_Injected(ref this, callback);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x0003278D File Offset: 0x0003098D
		private IntPtr GetDataRaw(int layer)
		{
			return AsyncGPUReadbackRequest.GetDataRaw_Injected(ref this, layer);
		}

		// Token: 0x06001F08 RID: 7944
		[MethodImpl(4096)]
		private static extern void Update_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F09 RID: 7945
		[MethodImpl(4096)]
		private static extern void WaitForCompletion_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F0A RID: 7946
		[MethodImpl(4096)]
		private static extern bool IsDone_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F0B RID: 7947
		[MethodImpl(4096)]
		private static extern bool HasError_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F0C RID: 7948
		[MethodImpl(4096)]
		private static extern int GetLayerCount_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F0D RID: 7949
		[MethodImpl(4096)]
		private static extern int GetLayerDataSize_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F0E RID: 7950
		[MethodImpl(4096)]
		private static extern int GetWidth_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F0F RID: 7951
		[MethodImpl(4096)]
		private static extern int GetHeight_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F10 RID: 7952
		[MethodImpl(4096)]
		private static extern int GetDepth_Injected(ref AsyncGPUReadbackRequest _unity_self);

		// Token: 0x06001F11 RID: 7953
		[MethodImpl(4096)]
		private static extern void SetScriptingCallback_Injected(ref AsyncGPUReadbackRequest _unity_self, Action<AsyncGPUReadbackRequest> callback);

		// Token: 0x06001F12 RID: 7954
		[MethodImpl(4096)]
		private static extern IntPtr GetDataRaw_Injected(ref AsyncGPUReadbackRequest _unity_self, int layer);

		// Token: 0x04000A30 RID: 2608
		internal IntPtr m_Ptr;

		// Token: 0x04000A31 RID: 2609
		internal int m_Version;
	}
}

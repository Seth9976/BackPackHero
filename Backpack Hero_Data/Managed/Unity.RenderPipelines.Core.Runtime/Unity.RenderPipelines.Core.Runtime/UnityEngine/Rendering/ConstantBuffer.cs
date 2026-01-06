using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000044 RID: 68
	public class ConstantBuffer
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0000CFAA File Offset: 0x0000B1AA
		public static void PushGlobal<CBType>(CommandBuffer cmd, in CBType data, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, in data);
			instance.SetGlobal(cmd, shaderId);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		public static void PushGlobal<CBType>(in CBType data, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(in data);
			instance.SetGlobal(shaderId);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		public static void Push<CBType>(CommandBuffer cmd, in CBType data, ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, in data);
			instance.Set(cmd, cs, shaderId);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000CFEB File Offset: 0x0000B1EB
		public static void Push<CBType>(in CBType data, ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(in data);
			instance.Set(cs, shaderId);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000D000 File Offset: 0x0000B200
		public static void Push<CBType>(CommandBuffer cmd, in CBType data, Material mat, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(cmd, in data);
			instance.Set(mat, shaderId);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000D016 File Offset: 0x0000B216
		public static void Push<CBType>(in CBType data, Material mat, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType> instance = ConstantBufferSingleton<CBType>.instance;
			instance.UpdateData(in data);
			instance.Set(mat, shaderId);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000D02B File Offset: 0x0000B22B
		public static void UpdateData<CBType>(CommandBuffer cmd, in CBType data) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.UpdateData(cmd, in data);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000D039 File Offset: 0x0000B239
		public static void UpdateData<CBType>(in CBType data) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.UpdateData(in data);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000D046 File Offset: 0x0000B246
		public static void SetGlobal<CBType>(CommandBuffer cmd, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.SetGlobal(cmd, shaderId);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000D054 File Offset: 0x0000B254
		public static void SetGlobal<CBType>(int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.SetGlobal(shaderId);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D061 File Offset: 0x0000B261
		public static void Set<CBType>(CommandBuffer cmd, ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.Set(cmd, cs, shaderId);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D070 File Offset: 0x0000B270
		public static void Set<CBType>(ComputeShader cs, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.Set(cs, shaderId);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D07E File Offset: 0x0000B27E
		public static void Set<CBType>(Material mat, int shaderId) where CBType : struct
		{
			ConstantBufferSingleton<CBType>.instance.Set(mat, shaderId);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D08C File Offset: 0x0000B28C
		public static void ReleaseAll()
		{
			foreach (ConstantBufferBase constantBufferBase in ConstantBuffer.m_RegisteredConstantBuffers)
			{
				constantBufferBase.Release();
			}
			ConstantBuffer.m_RegisteredConstantBuffers.Clear();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D0E8 File Offset: 0x0000B2E8
		internal static void Register(ConstantBufferBase cb)
		{
			ConstantBuffer.m_RegisteredConstantBuffers.Add(cb);
		}

		// Token: 0x040001AA RID: 426
		private static List<ConstantBufferBase> m_RegisteredConstantBuffers = new List<ConstantBufferBase>();
	}
}

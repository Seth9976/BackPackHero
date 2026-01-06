using System;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000BA RID: 186
	internal class ShaderData : IDisposable
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x0001F6E8 File Offset: 0x0001D8E8
		private ShaderData()
		{
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001F6F0 File Offset: 0x0001D8F0
		internal static ShaderData instance
		{
			get
			{
				if (ShaderData.m_Instance == null)
				{
					ShaderData.m_Instance = new ShaderData();
				}
				return ShaderData.m_Instance;
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001F708 File Offset: 0x0001D908
		public void Dispose()
		{
			this.DisposeBuffer(ref this.m_LightDataBuffer);
			this.DisposeBuffer(ref this.m_LightIndicesBuffer);
			this.DisposeBuffer(ref this.m_AdditionalLightShadowParamsStructuredBuffer);
			this.DisposeBuffer(ref this.m_AdditionalLightShadowSliceMatricesStructuredBuffer);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001F73A File Offset: 0x0001D93A
		internal ComputeBuffer GetLightDataBuffer(int size)
		{
			return this.GetOrUpdateBuffer<ShaderInput.LightData>(ref this.m_LightDataBuffer, size);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001F749 File Offset: 0x0001D949
		internal ComputeBuffer GetLightIndicesBuffer(int size)
		{
			return this.GetOrUpdateBuffer<int>(ref this.m_LightIndicesBuffer, size);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001F758 File Offset: 0x0001D958
		internal ComputeBuffer GetAdditionalLightShadowParamsStructuredBuffer(int size)
		{
			return this.GetOrUpdateBuffer<Vector4>(ref this.m_AdditionalLightShadowParamsStructuredBuffer, size);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001F767 File Offset: 0x0001D967
		internal ComputeBuffer GetAdditionalLightShadowSliceMatricesStructuredBuffer(int size)
		{
			return this.GetOrUpdateBuffer<Matrix4x4>(ref this.m_AdditionalLightShadowSliceMatricesStructuredBuffer, size);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001F776 File Offset: 0x0001D976
		private ComputeBuffer GetOrUpdateBuffer<T>(ref ComputeBuffer buffer, int size) where T : struct
		{
			if (buffer == null)
			{
				buffer = new ComputeBuffer(size, Marshal.SizeOf<T>());
			}
			else if (size > buffer.count)
			{
				buffer.Dispose();
				buffer = new ComputeBuffer(size, Marshal.SizeOf<T>());
			}
			return buffer;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001F7AB File Offset: 0x0001D9AB
		private void DisposeBuffer(ref ComputeBuffer buffer)
		{
			if (buffer != null)
			{
				buffer.Dispose();
				buffer = null;
			}
		}

		// Token: 0x04000469 RID: 1129
		private static ShaderData m_Instance;

		// Token: 0x0400046A RID: 1130
		private ComputeBuffer m_LightDataBuffer;

		// Token: 0x0400046B RID: 1131
		private ComputeBuffer m_LightIndicesBuffer;

		// Token: 0x0400046C RID: 1132
		private ComputeBuffer m_AdditionalLightShadowParamsStructuredBuffer;

		// Token: 0x0400046D RID: 1133
		private ComputeBuffer m_AdditionalLightShadowSliceMatricesStructuredBuffer;
	}
}

using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x02000046 RID: 70
	public class ConstantBuffer<CBType> : ConstantBufferBase where CBType : struct
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000D111 File Offset: 0x0000B311
		public ConstantBuffer()
		{
			this.m_GPUConstantBuffer = new ComputeBuffer(1, UnsafeUtility.SizeOf<CBType>(), ComputeBufferType.Constant);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000D142 File Offset: 0x0000B342
		public void UpdateData(CommandBuffer cmd, in CBType data)
		{
			this.m_Data[0] = data;
			cmd.SetBufferData(this.m_GPUConstantBuffer, this.m_Data);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000D168 File Offset: 0x0000B368
		public void UpdateData(in CBType data)
		{
			this.m_Data[0] = data;
			this.m_GPUConstantBuffer.SetData(this.m_Data);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D18D File Offset: 0x0000B38D
		public void SetGlobal(CommandBuffer cmd, int shaderId)
		{
			this.m_GlobalBindings.Add(shaderId);
			cmd.SetGlobalConstantBuffer(this.m_GPUConstantBuffer, shaderId, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D1B5 File Offset: 0x0000B3B5
		public void SetGlobal(int shaderId)
		{
			this.m_GlobalBindings.Add(shaderId);
			Shader.SetGlobalConstantBuffer(shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		public void Set(CommandBuffer cmd, ComputeShader cs, int shaderId)
		{
			cmd.SetComputeConstantBufferParam(cs, shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		public void Set(ComputeShader cs, int shaderId)
		{
			cs.SetConstantBuffer(shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D213 File Offset: 0x0000B413
		public void Set(Material mat, int shaderId)
		{
			mat.SetConstantBuffer(shaderId, this.m_GPUConstantBuffer, 0, this.m_GPUConstantBuffer.stride);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000D22E File Offset: 0x0000B42E
		public void PushGlobal(CommandBuffer cmd, in CBType data, int shaderId)
		{
			this.UpdateData(cmd, in data);
			this.SetGlobal(cmd, shaderId);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000D240 File Offset: 0x0000B440
		public void PushGlobal(in CBType data, int shaderId)
		{
			this.UpdateData(in data);
			this.SetGlobal(shaderId);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D250 File Offset: 0x0000B450
		public override void Release()
		{
			foreach (int num in this.m_GlobalBindings)
			{
				Shader.SetGlobalConstantBuffer(num, null, 0, 0);
			}
			this.m_GlobalBindings.Clear();
			CoreUtils.SafeRelease(this.m_GPUConstantBuffer);
		}

		// Token: 0x040001AB RID: 427
		private HashSet<int> m_GlobalBindings = new HashSet<int>();

		// Token: 0x040001AC RID: 428
		private CBType[] m_Data = new CBType[1];

		// Token: 0x040001AD RID: 429
		private ComputeBuffer m_GPUConstantBuffer;
	}
}

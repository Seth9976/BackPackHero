using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003DE RID: 990
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/GPUFence.h")]
	public struct GraphicsFence
	{
		// Token: 0x06001F8E RID: 8078 RVA: 0x000339CC File Offset: 0x00031BCC
		internal static SynchronisationStageFlags TranslateSynchronizationStageToFlags(SynchronisationStage s)
		{
			return (s == SynchronisationStage.VertexProcessing) ? SynchronisationStageFlags.VertexProcessing : SynchronisationStageFlags.PixelProcessing;
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x000339E8 File Offset: 0x00031BE8
		public bool passed
		{
			get
			{
				this.Validate();
				bool flag = !SystemInfo.supportsGraphicsFence || (this.m_FenceType == GraphicsFenceType.AsyncQueueSynchronisation && !SystemInfo.supportsAsyncCompute);
				if (flag)
				{
					throw new NotSupportedException("Cannot determine if this GraphicsFence has passed as this platform has not implemented GraphicsFences.");
				}
				bool flag2 = !this.IsFencePending();
				return flag2 || GraphicsFence.HasFencePassed_Internal(this.m_Ptr);
			}
		}

		// Token: 0x06001F90 RID: 8080
		[FreeFunction("GPUFenceInternals::HasFencePassed_Internal")]
		[MethodImpl(4096)]
		private static extern bool HasFencePassed_Internal(IntPtr fencePtr);

		// Token: 0x06001F91 RID: 8081 RVA: 0x00033A4C File Offset: 0x00031C4C
		internal void InitPostAllocation()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				bool supportsGraphicsFence = SystemInfo.supportsGraphicsFence;
				if (supportsGraphicsFence)
				{
					throw new NullReferenceException("The internal fence ptr is null, this should not be possible for fences that have been correctly constructed using Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
				}
				this.m_Version = this.GetPlatformNotSupportedVersion();
			}
			else
			{
				this.m_Version = GraphicsFence.GetVersionNumber(this.m_Ptr);
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x00033AA4 File Offset: 0x00031CA4
		internal bool IsFencePending()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			return !flag && this.m_Version == GraphicsFence.GetVersionNumber(this.m_Ptr);
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x00033AE4 File Offset: 0x00031CE4
		internal void Validate()
		{
			bool flag = this.m_Version == 0 || (SystemInfo.supportsGraphicsFence && this.m_Version == this.GetPlatformNotSupportedVersion());
			if (flag)
			{
				throw new InvalidOperationException("This GraphicsFence object has not been correctly constructed see Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
			}
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00033B24 File Offset: 0x00031D24
		private int GetPlatformNotSupportedVersion()
		{
			return -1;
		}

		// Token: 0x06001F95 RID: 8085
		[NativeThrows]
		[FreeFunction("GPUFenceInternals::GetVersionNumber")]
		[MethodImpl(4096)]
		private static extern int GetVersionNumber(IntPtr fencePtr);

		// Token: 0x04000C27 RID: 3111
		internal IntPtr m_Ptr;

		// Token: 0x04000C28 RID: 3112
		internal int m_Version;

		// Token: 0x04000C29 RID: 3113
		internal GraphicsFenceType m_FenceType;
	}
}

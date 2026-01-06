using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200012B RID: 299
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public struct RenderBuffer
	{
		// Token: 0x06000863 RID: 2147 RVA: 0x0000C72D File Offset: 0x0000A92D
		[FreeFunction(Name = "RenderBufferScripting::SetLoadAction", HasExplicitThis = true)]
		internal void SetLoadAction(RenderBufferLoadAction action)
		{
			RenderBuffer.SetLoadAction_Injected(ref this, action);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0000C736 File Offset: 0x0000A936
		[FreeFunction(Name = "RenderBufferScripting::SetStoreAction", HasExplicitThis = true)]
		internal void SetStoreAction(RenderBufferStoreAction action)
		{
			RenderBuffer.SetStoreAction_Injected(ref this, action);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000C73F File Offset: 0x0000A93F
		[FreeFunction(Name = "RenderBufferScripting::GetLoadAction", HasExplicitThis = true)]
		internal RenderBufferLoadAction GetLoadAction()
		{
			return RenderBuffer.GetLoadAction_Injected(ref this);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000C747 File Offset: 0x0000A947
		[FreeFunction(Name = "RenderBufferScripting::GetStoreAction", HasExplicitThis = true)]
		internal RenderBufferStoreAction GetStoreAction()
		{
			return RenderBuffer.GetStoreAction_Injected(ref this);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0000C74F File Offset: 0x0000A94F
		[FreeFunction(Name = "RenderBufferScripting::GetNativeRenderBufferPtr", HasExplicitThis = true)]
		public IntPtr GetNativeRenderBufferPtr()
		{
			return RenderBuffer.GetNativeRenderBufferPtr_Injected(ref this);
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0000C758 File Offset: 0x0000A958
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x0000C770 File Offset: 0x0000A970
		internal RenderBufferLoadAction loadAction
		{
			get
			{
				return this.GetLoadAction();
			}
			set
			{
				this.SetLoadAction(value);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0000C77C File Offset: 0x0000A97C
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x0000C794 File Offset: 0x0000A994
		internal RenderBufferStoreAction storeAction
		{
			get
			{
				return this.GetStoreAction();
			}
			set
			{
				this.SetStoreAction(value);
			}
		}

		// Token: 0x0600086C RID: 2156
		[MethodImpl(4096)]
		private static extern void SetLoadAction_Injected(ref RenderBuffer _unity_self, RenderBufferLoadAction action);

		// Token: 0x0600086D RID: 2157
		[MethodImpl(4096)]
		private static extern void SetStoreAction_Injected(ref RenderBuffer _unity_self, RenderBufferStoreAction action);

		// Token: 0x0600086E RID: 2158
		[MethodImpl(4096)]
		private static extern RenderBufferLoadAction GetLoadAction_Injected(ref RenderBuffer _unity_self);

		// Token: 0x0600086F RID: 2159
		[MethodImpl(4096)]
		private static extern RenderBufferStoreAction GetStoreAction_Injected(ref RenderBuffer _unity_self);

		// Token: 0x06000870 RID: 2160
		[MethodImpl(4096)]
		private static extern IntPtr GetNativeRenderBufferPtr_Injected(ref RenderBuffer _unity_self);

		// Token: 0x040003C0 RID: 960
		internal int m_RenderTextureInstanceID;

		// Token: 0x040003C1 RID: 961
		internal IntPtr m_BufferPtr;
	}
}

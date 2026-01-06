using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003A RID: 58
	[NativeHeader("Modules/IMGUI/GUIState.h")]
	internal class ObjectGUIState : IDisposable
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0000F1CB File Offset: 0x0000D3CB
		public ObjectGUIState()
		{
			this.m_Ptr = ObjectGUIState.Internal_Create();
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000F1E0 File Offset: 0x0000D3E0
		public void Dispose()
		{
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
		~ObjectGUIState()
		{
			this.Destroy();
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000F224 File Offset: 0x0000D424
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ObjectGUIState.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x0600040D RID: 1037
		[MethodImpl(4096)]
		private static extern IntPtr Internal_Create();

		// Token: 0x0600040E RID: 1038
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x04000125 RID: 293
		internal IntPtr m_Ptr;
	}
}

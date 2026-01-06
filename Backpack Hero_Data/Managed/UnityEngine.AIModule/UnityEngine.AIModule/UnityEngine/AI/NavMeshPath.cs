using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x02000005 RID: 5
	[MovedFrom("UnityEngine")]
	[NativeHeader("Modules/AI/NavMeshPath.bindings.h")]
	[StructLayout(0)]
	public sealed class NavMeshPath
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002059 File Offset: 0x00000259
		public NavMeshPath()
		{
			this.m_Ptr = NavMeshPath.InitializeNavMeshPath();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		~NavMeshPath()
		{
			NavMeshPath.DestroyNavMeshPath(this.m_Ptr);
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06000005 RID: 5
		[FreeFunction("NavMeshPathScriptBindings::InitializeNavMeshPath")]
		[MethodImpl(4096)]
		private static extern IntPtr InitializeNavMeshPath();

		// Token: 0x06000006 RID: 6
		[FreeFunction("NavMeshPathScriptBindings::DestroyNavMeshPath", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void DestroyNavMeshPath(IntPtr ptr);

		// Token: 0x06000007 RID: 7
		[FreeFunction("NavMeshPathScriptBindings::GetCornersNonAlloc", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int GetCornersNonAlloc([Out] Vector3[] results);

		// Token: 0x06000008 RID: 8
		[FreeFunction("NavMeshPathScriptBindings::CalculateCornersInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Vector3[] CalculateCornersInternal();

		// Token: 0x06000009 RID: 9
		[FreeFunction("NavMeshPathScriptBindings::ClearCornersInternal", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void ClearCornersInternal();

		// Token: 0x0600000A RID: 10 RVA: 0x000020B0 File Offset: 0x000002B0
		public void ClearCorners()
		{
			this.ClearCornersInternal();
			this.m_Corners = null;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020C4 File Offset: 0x000002C4
		private void CalculateCorners()
		{
			bool flag = this.m_Corners == null;
			if (flag)
			{
				this.m_Corners = this.CalculateCornersInternal();
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020EC File Offset: 0x000002EC
		public Vector3[] corners
		{
			get
			{
				this.CalculateCorners();
				return this.m_Corners;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13
		public extern NavMeshPathStatus status
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x04000005 RID: 5
		internal IntPtr m_Ptr;

		// Token: 0x04000006 RID: 6
		internal Vector3[] m_Corners;
	}
}

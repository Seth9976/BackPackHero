using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000102 RID: 258
	[NativeHeader("Runtime/Export/Camera/CullingGroup.bindings.h")]
	[StructLayout(0)]
	public class CullingGroup : IDisposable
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x0000803F File Offset: 0x0000623F
		public CullingGroup()
		{
			this.m_Ptr = CullingGroup.Init(this);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000805C File Offset: 0x0000625C
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					this.FinalizerFailure();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060005B4 RID: 1460
		[FreeFunction("CullingGroup_Bindings::Dispose", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void DisposeInternal();

		// Token: 0x060005B5 RID: 1461 RVA: 0x000080A4 File Offset: 0x000062A4
		public void Dispose()
		{
			this.DisposeInternal();
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x000080BC File Offset: 0x000062BC
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x000080D4 File Offset: 0x000062D4
		public CullingGroup.StateChanged onStateChanged
		{
			get
			{
				return this.m_OnStateChanged;
			}
			set
			{
				this.m_OnStateChanged = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005B8 RID: 1464
		// (set) Token: 0x060005B9 RID: 1465
		public extern bool enabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005BA RID: 1466
		// (set) Token: 0x060005BB RID: 1467
		public extern Camera targetCamera
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060005BC RID: 1468
		[MethodImpl(4096)]
		public extern void SetBoundingSpheres(BoundingSphere[] array);

		// Token: 0x060005BD RID: 1469
		[MethodImpl(4096)]
		public extern void SetBoundingSphereCount(int count);

		// Token: 0x060005BE RID: 1470
		[MethodImpl(4096)]
		public extern void EraseSwapBack(int index);

		// Token: 0x060005BF RID: 1471 RVA: 0x000080DE File Offset: 0x000062DE
		public static void EraseSwapBack<T>(int index, T[] myArray, ref int size)
		{
			size--;
			myArray[index] = myArray[size];
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000080F8 File Offset: 0x000062F8
		public int QueryIndices(bool visible, int[] result, int firstIndex)
		{
			return this.QueryIndices(visible, -1, CullingQueryOptions.IgnoreDistance, result, firstIndex);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00008118 File Offset: 0x00006318
		public int QueryIndices(int distanceIndex, int[] result, int firstIndex)
		{
			return this.QueryIndices(false, distanceIndex, CullingQueryOptions.IgnoreVisibility, result, firstIndex);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00008138 File Offset: 0x00006338
		public int QueryIndices(bool visible, int distanceIndex, int[] result, int firstIndex)
		{
			return this.QueryIndices(visible, distanceIndex, CullingQueryOptions.Normal, result, firstIndex);
		}

		// Token: 0x060005C3 RID: 1475
		[FreeFunction("CullingGroup_Bindings::QueryIndices", HasExplicitThis = true)]
		[NativeThrows]
		[MethodImpl(4096)]
		private extern int QueryIndices(bool visible, int distanceIndex, CullingQueryOptions options, int[] result, int firstIndex);

		// Token: 0x060005C4 RID: 1476
		[NativeThrows]
		[FreeFunction("CullingGroup_Bindings::IsVisible", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool IsVisible(int index);

		// Token: 0x060005C5 RID: 1477
		[NativeThrows]
		[FreeFunction("CullingGroup_Bindings::GetDistance", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int GetDistance(int index);

		// Token: 0x060005C6 RID: 1478
		[FreeFunction("CullingGroup_Bindings::SetBoundingDistances", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetBoundingDistances(float[] distances);

		// Token: 0x060005C7 RID: 1479 RVA: 0x00008156 File Offset: 0x00006356
		[FreeFunction("CullingGroup_Bindings::SetDistanceReferencePoint", HasExplicitThis = true)]
		private void SetDistanceReferencePoint_InternalVector3(Vector3 point)
		{
			this.SetDistanceReferencePoint_InternalVector3_Injected(ref point);
		}

		// Token: 0x060005C8 RID: 1480
		[NativeMethod("SetDistanceReferenceTransform")]
		[MethodImpl(4096)]
		private extern void SetDistanceReferencePoint_InternalTransform(Transform transform);

		// Token: 0x060005C9 RID: 1481 RVA: 0x00008160 File Offset: 0x00006360
		public void SetDistanceReferencePoint(Vector3 point)
		{
			this.SetDistanceReferencePoint_InternalVector3(point);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000816B File Offset: 0x0000636B
		public void SetDistanceReferencePoint(Transform transform)
		{
			this.SetDistanceReferencePoint_InternalTransform(transform);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00008178 File Offset: 0x00006378
		[SecuritySafeCritical]
		[RequiredByNativeCode]
		private unsafe static void SendEvents(CullingGroup cullingGroup, IntPtr eventsPtr, int count)
		{
			CullingGroupEvent* ptr = (CullingGroupEvent*)eventsPtr.ToPointer();
			bool flag = cullingGroup.m_OnStateChanged == null;
			if (!flag)
			{
				for (int i = 0; i < count; i++)
				{
					cullingGroup.m_OnStateChanged(ptr[i]);
				}
			}
		}

		// Token: 0x060005CC RID: 1484
		[FreeFunction("CullingGroup_Bindings::Init")]
		[MethodImpl(4096)]
		private static extern IntPtr Init(object scripting);

		// Token: 0x060005CD RID: 1485
		[FreeFunction("CullingGroup_Bindings::FinalizerFailure", HasExplicitThis = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void FinalizerFailure();

		// Token: 0x060005CE RID: 1486
		[MethodImpl(4096)]
		private extern void SetDistanceReferencePoint_InternalVector3_Injected(ref Vector3 point);

		// Token: 0x0400036E RID: 878
		internal IntPtr m_Ptr;

		// Token: 0x0400036F RID: 879
		private CullingGroup.StateChanged m_OnStateChanged = null;

		// Token: 0x02000103 RID: 259
		// (Invoke) Token: 0x060005D0 RID: 1488
		public delegate void StateChanged(CullingGroupEvent sphere);
	}
}

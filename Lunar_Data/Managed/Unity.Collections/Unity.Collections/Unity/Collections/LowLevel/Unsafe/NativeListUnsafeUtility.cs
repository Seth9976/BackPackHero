using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000119 RID: 281
	[BurstCompatible]
	public static class NativeListUnsafeUtility
	{
		// Token: 0x06000A52 RID: 2642 RVA: 0x0001E965 File Offset: 0x0001CB65
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void* GetUnsafePtr<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType
		{
			return (void*)list.m_ListData->Ptr;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001E965 File Offset: 0x0001CB65
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void* GetUnsafeReadOnlyPtr<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType
		{
			return (void*)list.m_ListData->Ptr;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001E972 File Offset: 0x0001CB72
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void* GetInternalListDataPtrUnchecked<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(ref NativeList<T> list) where T : struct, ValueType
		{
			return (void*)list.m_ListData;
		}
	}
}

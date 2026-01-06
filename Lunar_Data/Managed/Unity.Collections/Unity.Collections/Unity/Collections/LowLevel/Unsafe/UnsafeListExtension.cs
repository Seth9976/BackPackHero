using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000FF RID: 255
	public static class UnsafeListExtension
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x0001D4D0 File Offset: 0x0001B6D0
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		internal static ref UnsafeList ListData<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeList<T> from) where T : struct, ValueType
		{
			return UnsafeUtility.As<UnsafeList<T>, UnsafeList>(ref from);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		public static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeList list) where T : struct, ValueType, IComparable<T>
		{
			list.Sort(default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
		public static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			NativeSortExtension.IntroSort<T, U>(list.Ptr, list.Length, comp);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001D508 File Offset: 0x0001B708
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this UnsafeList).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public static JobHandle Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeList container, JobHandle inputDeps) where T : struct, ValueType, IComparable<T>
		{
			return container.Sort(default(NativeSortExtension.DefaultComparer<T>), inputDeps);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001D528 File Offset: 0x0001B728
		public unsafe static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeList list) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.SortJob<T, NativeSortExtension.DefaultComparer<T>>((T*)list.Ptr, list.Length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001D54F File Offset: 0x0001B74F
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this UnsafeList, U).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public unsafe static JobHandle Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList container, U comp, JobHandle inputDeps) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.Sort<T, U>((T*)container.Ptr, container.Length, comp, inputDeps);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001D564 File Offset: 0x0001B764
		public unsafe static SortJob<T, U> SortJob<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.SortJob<T, U>((T*)list.Ptr, list.Length, comp);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001D578 File Offset: 0x0001B778
		public static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafeList container, T value) where T : struct, ValueType, IComparable<T>
		{
			return container.BinarySearch(value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001D595 File Offset: 0x0001B795
		public unsafe static int BinarySearch<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this UnsafeList container, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.BinarySearch<T, U>((T*)container.Ptr, container.Length, value, comp);
		}
	}
}

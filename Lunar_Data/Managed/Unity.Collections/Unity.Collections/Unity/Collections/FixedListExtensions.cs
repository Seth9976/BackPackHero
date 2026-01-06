using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x0200008B RID: 139
	public static class FixedListExtensions
	{
		// Token: 0x06000356 RID: 854 RVA: 0x00008F20 File Offset: 0x00007120
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this FixedList32Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00008F54 File Offset: 0x00007154
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList32Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T, U>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length, comp);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00008F8C File Offset: 0x0000718C
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this FixedList64Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length);
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00008FC0 File Offset: 0x000071C0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList64Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T, U>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length, comp);
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00008FF8 File Offset: 0x000071F8
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this FixedList128Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000902C File Offset: 0x0000722C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList128Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T, U>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length, comp);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00009064 File Offset: 0x00007264
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this FixedList512Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00009098 File Offset: 0x00007298
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList512Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T, U>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length, comp);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000090D0 File Offset: 0x000072D0
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this FixedList4096Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009104 File Offset: 0x00007304
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[global::System.Runtime.CompilerServices.IsUnmanaged] T, U>(this FixedList4096Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T, U>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length, comp);
			}
		}
	}
}

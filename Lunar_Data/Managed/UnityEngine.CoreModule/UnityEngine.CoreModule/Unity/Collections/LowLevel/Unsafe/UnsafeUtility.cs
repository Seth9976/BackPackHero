using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.Burst;
using UnityEngine.Bindings;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000AC RID: 172
	[NativeHeader("Runtime/Export/Unsafe/UnsafeUtility.bindings.h")]
	[StaticAccessor("UnsafeUtility", StaticAccessorType.DoubleColon)]
	public static class UnsafeUtility
	{
		// Token: 0x060002EE RID: 750
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern int GetFieldOffsetInStruct(FieldInfo field);

		// Token: 0x060002EF RID: 751
		[ThreadSafe]
		[MethodImpl(4096)]
		private static extern int GetFieldOffsetInClass(FieldInfo field);

		// Token: 0x060002F0 RID: 752 RVA: 0x000057A0 File Offset: 0x000039A0
		public static int GetFieldOffset(FieldInfo field)
		{
			bool isValueType = field.DeclaringType.IsValueType;
			int num;
			if (isValueType)
			{
				num = UnsafeUtility.GetFieldOffsetInStruct(field);
			}
			else
			{
				bool isClass = field.DeclaringType.IsClass;
				if (isClass)
				{
					num = UnsafeUtility.GetFieldOffsetInClass(field);
				}
				else
				{
					num = -1;
				}
			}
			return num;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000057E4 File Offset: 0x000039E4
		public unsafe static void* PinGCObjectAndGetAddress(object target, out ulong gcHandle)
		{
			return UnsafeUtility.PinSystemObjectAndGetAddress(target, out gcHandle);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00005800 File Offset: 0x00003A00
		public unsafe static void* PinGCArrayAndGetDataAddress(Array target, out ulong gcHandle)
		{
			return UnsafeUtility.PinSystemArrayAndGetAddress(target, out gcHandle);
		}

		// Token: 0x060002F3 RID: 755
		[ThreadSafe]
		[MethodImpl(4096)]
		private unsafe static extern void* PinSystemArrayAndGetAddress(object target, out ulong gcHandle);

		// Token: 0x060002F4 RID: 756
		[ThreadSafe]
		[MethodImpl(4096)]
		private unsafe static extern void* PinSystemObjectAndGetAddress(object target, out ulong gcHandle);

		// Token: 0x060002F5 RID: 757
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern void ReleaseGCObject(ulong gcHandle);

		// Token: 0x060002F6 RID: 758
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void CopyObjectAddressToPtr(object target, void* dstPtr);

		// Token: 0x060002F7 RID: 759 RVA: 0x0000581C File Offset: 0x00003A1C
		public static bool IsBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable(typeof(T));
		}

		// Token: 0x060002F8 RID: 760
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(4096)]
		public static extern int CheckForLeaks();

		// Token: 0x060002F9 RID: 761
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(4096)]
		public static extern int ForgiveLeaks();

		// Token: 0x060002FA RID: 762
		[BurstAuthorizedExternalMethod]
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(4096)]
		public static extern NativeLeakDetectionMode GetLeakDetectionMode();

		// Token: 0x060002FB RID: 763
		[BurstAuthorizedExternalMethod]
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(4096)]
		public static extern void SetLeakDetectionMode(NativeLeakDetectionMode value);

		// Token: 0x060002FC RID: 764
		[ThreadSafe(ThrowsException = false)]
		[BurstAuthorizedExternalMethod]
		[MethodImpl(4096)]
		internal static extern int LeakRecord(IntPtr handle, LeakCategory category, int callstacksToSkip);

		// Token: 0x060002FD RID: 765
		[ThreadSafe(ThrowsException = false)]
		[BurstAuthorizedExternalMethod]
		[MethodImpl(4096)]
		internal static extern int LeakErase(IntPtr handle, LeakCategory category);

		// Token: 0x060002FE RID: 766
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void* MallocTracked(long size, int alignment, Allocator allocator, int callstacksToSkip);

		// Token: 0x060002FF RID: 767
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void FreeTracked(void* memory, Allocator allocator);

		// Token: 0x06000300 RID: 768
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void* Malloc(long size, int alignment, Allocator allocator);

		// Token: 0x06000301 RID: 769
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void Free(void* memory, Allocator allocator);

		// Token: 0x06000302 RID: 770 RVA: 0x00005840 File Offset: 0x00003A40
		public static bool IsValidAllocator(Allocator allocator)
		{
			return allocator > Allocator.None;
		}

		// Token: 0x06000303 RID: 771
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void MemCpy(void* destination, void* source, long size);

		// Token: 0x06000304 RID: 772
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void MemCpyReplicate(void* destination, void* source, int size, int count);

		// Token: 0x06000305 RID: 773
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void MemCpyStride(void* destination, int destinationStride, void* source, int sourceStride, int elementSize, int count);

		// Token: 0x06000306 RID: 774
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void MemMove(void* destination, void* source, long size);

		// Token: 0x06000307 RID: 775
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern void MemSet(void* destination, byte value, long size);

		// Token: 0x06000308 RID: 776 RVA: 0x00005856 File Offset: 0x00003A56
		public unsafe static void MemClear(void* destination, long size)
		{
			UnsafeUtility.MemSet(destination, 0, size);
		}

		// Token: 0x06000309 RID: 777
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(4096)]
		public unsafe static extern int MemCmp(void* ptr1, void* ptr2, long size);

		// Token: 0x0600030A RID: 778
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern int SizeOf(Type type);

		// Token: 0x0600030B RID: 779
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool IsBlittable(Type type);

		// Token: 0x0600030C RID: 780
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool IsUnmanaged(Type type);

		// Token: 0x0600030D RID: 781
		[ThreadSafe]
		[MethodImpl(4096)]
		public static extern bool IsValidNativeContainerElementType(Type type);

		// Token: 0x0600030E RID: 782
		[ThreadSafe]
		[MethodImpl(4096)]
		internal static extern void LogError(string msg, string filename, int linenumber);

		// Token: 0x0600030F RID: 783 RVA: 0x00005864 File Offset: 0x00003A64
		private static bool IsBlittableValueType(Type t)
		{
			return t.IsValueType && UnsafeUtility.IsBlittable(t);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00005888 File Offset: 0x00003A88
		private static string GetReasonForTypeNonBlittableImpl(Type t, string name)
		{
			bool flag = !t.IsValueType;
			string text;
			if (flag)
			{
				text = string.Format("{0} is not blittable because it is not of value type ({1})\n", name, t);
			}
			else
			{
				bool isPrimitive = t.IsPrimitive;
				if (isPrimitive)
				{
					text = string.Format("{0} is not blittable ({1})\n", name, t);
				}
				else
				{
					string text2 = "";
					foreach (FieldInfo fieldInfo in t.GetFields(52))
					{
						bool flag2 = !UnsafeUtility.IsBlittableValueType(fieldInfo.FieldType);
						if (flag2)
						{
							text2 += UnsafeUtility.GetReasonForTypeNonBlittableImpl(fieldInfo.FieldType, string.Format("{0}.{1}", name, fieldInfo.Name));
						}
					}
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000593C File Offset: 0x00003B3C
		internal static bool IsArrayBlittable(Array arr)
		{
			return UnsafeUtility.IsBlittableValueType(arr.GetType().GetElementType());
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00005960 File Offset: 0x00003B60
		internal static bool IsGenericListBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable<T>();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00005978 File Offset: 0x00003B78
		internal static string GetReasonForArrayNonBlittable(Array arr)
		{
			Type elementType = arr.GetType().GetElementType();
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(elementType, elementType.Name);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000059A4 File Offset: 0x00003BA4
		internal static string GetReasonForGenericListNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000059D0 File Offset: 0x00003BD0
		internal static string GetReasonForTypeNonBlittable(Type t)
		{
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(t, t.Name);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000059F0 File Offset: 0x00003BF0
		internal static string GetReasonForValueTypeNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00005A1C File Offset: 0x00003C1C
		public static bool IsUnmanaged<T>()
		{
			int num = UnsafeUtility.IsUnmanagedCache<T>.value;
			bool flag = num == 1;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = num == 0;
				if (flag3)
				{
					num = (UnsafeUtility.IsUnmanagedCache<T>.value = (UnsafeUtility.IsUnmanaged(typeof(T)) ? 1 : (-1)));
				}
				flag2 = num == 1;
			}
			return flag2;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00005A68 File Offset: 0x00003C68
		public static bool IsValidNativeContainerElementType<T>()
		{
			int num = UnsafeUtility.IsValidNativeContainerElementTypeCache<T>.value;
			bool flag = num == -1;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = num == 0;
				if (flag3)
				{
					num = (UnsafeUtility.IsValidNativeContainerElementTypeCache<T>.value = (UnsafeUtility.IsValidNativeContainerElementType(typeof(T)) ? 1 : (-1)));
				}
				flag2 = num == 1;
			}
			return flag2;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00005AB4 File Offset: 0x00003CB4
		public static int AlignOf<T>() where T : struct
		{
			return UnsafeUtility.SizeOf<UnsafeUtility.AlignOfHelper<T>>() - UnsafeUtility.SizeOf<T>();
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00005AD1 File Offset: 0x00003CD1
		[MethodImpl(256)]
		public unsafe static void CopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			UnsafeUtility.InternalCopyPtrToStructure<T>(ptr, out output);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00005ADC File Offset: 0x00003CDC
		private unsafe static void InternalCopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			output = *(T*)ptr;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00005AEA File Offset: 0x00003CEA
		[MethodImpl(256)]
		public unsafe static void CopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			UnsafeUtility.InternalCopyStructureToPtr<T>(ref input, ptr);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00005ADC File Offset: 0x00003CDC
		private unsafe static void InternalCopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			*(T*)ptr = input;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00005AF5 File Offset: 0x00003CF5
		public unsafe static T ReadArrayElement<T>(void* source, int index)
		{
			return *(T*)((byte*)source + (long)index * (long)sizeof(T));
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00005B09 File Offset: 0x00003D09
		public unsafe static T ReadArrayElementWithStride<T>(void* source, int index, int stride)
		{
			return *(T*)((byte*)source + (long)index * (long)stride);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00005B18 File Offset: 0x00003D18
		public unsafe static void WriteArrayElement<T>(void* destination, int index, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)sizeof(T)) = value;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00005B2D File Offset: 0x00003D2D
		public unsafe static void WriteArrayElementWithStride<T>(void* destination, int index, int stride, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)stride) = value;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00005B3D File Offset: 0x00003D3D
		public unsafe static void* AddressOf<T>(ref T output) where T : struct
		{
			return (void*)(&output);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00005B40 File Offset: 0x00003D40
		public static int SizeOf<T>() where T : struct
		{
			return sizeof(T);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00005B3D File Offset: 0x00003D3D
		public static ref T As<U, T>(ref U from)
		{
			return ref from;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00005B3D File Offset: 0x00003D3D
		public unsafe static ref T AsRef<T>(void* ptr) where T : struct
		{
			return ref *(T*)ptr;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00005B48 File Offset: 0x00003D48
		public unsafe static ref T ArrayElementAsRef<T>(void* ptr, int index) where T : struct
		{
			return ref *(T*)((byte*)ptr + (long)index * (long)sizeof(T));
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00005B58 File Offset: 0x00003D58
		public static int EnumToInt<T>(T enumValue) where T : struct, IConvertible
		{
			int num = 0;
			UnsafeUtility.InternalEnumToInt<T>(ref enumValue, ref num);
			return num;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00005B77 File Offset: 0x00003D77
		private static void InternalEnumToInt<T>(ref T enumValue, ref int intValue)
		{
			intValue = enumValue;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00005B7D File Offset: 0x00003D7D
		public static bool EnumEquals<T>(T lhs, T rhs) where T : struct, IConvertible
		{
			return lhs == rhs;
		}

		// Token: 0x020000AD RID: 173
		internal struct IsUnmanagedCache<T>
		{
			// Token: 0x04000238 RID: 568
			internal static int value;
		}

		// Token: 0x020000AE RID: 174
		internal struct IsValidNativeContainerElementTypeCache<T>
		{
			// Token: 0x04000239 RID: 569
			internal static int value;
		}

		// Token: 0x020000AF RID: 175
		private struct AlignOfHelper<T> where T : struct
		{
			// Token: 0x0400023A RID: 570
			public byte dummy;

			// Token: 0x0400023B RID: 571
			public T data;
		}
	}
}

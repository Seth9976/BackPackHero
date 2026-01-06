using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000124 RID: 292
	internal static class ArrayHelpers
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x0004D9DB File Offset: 0x0004BBDB
		public static int LengthSafe<TValue>(this TValue[] array)
		{
			if (array == null)
			{
				return 0;
			}
			return array.Length;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0004D9E5 File Offset: 0x0004BBE5
		public static void Clear<TValue>(this TValue[] array)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, array.Length);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004D9F5 File Offset: 0x0004BBF5
		public static void Clear<TValue>(this TValue[] array, int count)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, count);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0004DA03 File Offset: 0x0004BC03
		public static void Clear<TValue>(this TValue[] array, ref int count)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, count);
			count = 0;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0004DA15 File Offset: 0x0004BC15
		public static void EnsureCapacity<TValue>(ref TValue[] array, int count, int capacity, int capacityIncrement = 10)
		{
			if (capacity == 0)
			{
				return;
			}
			if (array == null)
			{
				array = new TValue[Math.Max(capacity, capacityIncrement)];
				return;
			}
			if (array.Length - count >= capacity)
			{
				return;
			}
			ArrayHelpers.DuplicateWithCapacity<TValue>(ref array, count, capacity, capacityIncrement);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0004DA44 File Offset: 0x0004BC44
		public static void DuplicateWithCapacity<TValue>(ref TValue[] array, int count, int capacity, int capacityIncrement = 10)
		{
			if (array == null)
			{
				array = new TValue[Math.Max(capacity, capacityIncrement)];
				return;
			}
			TValue[] array2 = new TValue[count + Math.Max(capacity, capacityIncrement)];
			Array.Copy(array, array2, count);
			array = array2;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0004DA80 File Offset: 0x0004BC80
		public static bool Contains<TValue>(TValue[] array, TValue value)
		{
			if (array == null)
			{
				return false;
			}
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = 0; i < array.Length; i++)
			{
				if (@default.Equals(array[i], value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0004DAB9 File Offset: 0x0004BCB9
		public static bool ContainsReference<TValue>(this TValue[] array, TValue value) where TValue : class
		{
			return array != null && array.ContainsReference(array.Length, value);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0004DACA File Offset: 0x0004BCCA
		public static bool ContainsReference<TFirst, TSecond>(this TFirst[] array, int count, TSecond value) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, count) != -1;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0004DADA File Offset: 0x0004BCDA
		public static bool ContainsReference<TFirst, TSecond>(this TFirst[] array, int startIndex, int count, TSecond value) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, startIndex, count) != -1;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0004DAEC File Offset: 0x0004BCEC
		public static bool HaveDuplicateReferences<TFirst>(this TFirst[] first, int index, int count)
		{
			for (int i = 0; i < count; i++)
			{
				TFirst tfirst = first[i];
				for (int j = i + 1; j < count - i; j++)
				{
					if (tfirst == first[j])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0004DB34 File Offset: 0x0004BD34
		public static bool HaveEqualElements<TValue>(TValue[] first, TValue[] second, int count = 2147483647)
		{
			if (first == null || second == null)
			{
				return second == first;
			}
			int num = Math.Min(count, first.Length);
			int num2 = Math.Min(count, second.Length);
			if (num != num2)
			{
				return false;
			}
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = 0; i < num; i++)
			{
				if (!@default.Equals(first[i], second[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0004DB94 File Offset: 0x0004BD94
		public static int IndexOf<TValue>(TValue[] array, TValue value, int startIndex = 0, int count = -1)
		{
			if (array == null)
			{
				return -1;
			}
			if (count < 0)
			{
				count = array.Length - startIndex;
			}
			EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (@default.Equals(array[i], value))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0004DBD8 File Offset: 0x0004BDD8
		public static int IndexOf<TValue>(this TValue[] array, Predicate<TValue> predicate)
		{
			if (array == null)
			{
				return -1;
			}
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (predicate(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0004DC0C File Offset: 0x0004BE0C
		public static int IndexOf<TValue>(this TValue[] array, Predicate<TValue> predicate, int startIndex = 0, int count = -1)
		{
			if (array == null)
			{
				return -1;
			}
			int num = startIndex + ((count < 0) ? (array.Length - startIndex) : count);
			for (int i = startIndex; i < num; i++)
			{
				if (predicate(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0004DC4B File Offset: 0x0004BE4B
		public static int IndexOfReference<TFirst, TSecond>(this TFirst[] array, TSecond value, int count = -1) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, 0, count);
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0004DC58 File Offset: 0x0004BE58
		public static int IndexOfReference<TFirst, TSecond>(this TFirst[] array, TSecond value, int startIndex, int count) where TFirst : TSecond where TSecond : class
		{
			if (array == null)
			{
				return -1;
			}
			if (count < 0)
			{
				count = array.Length - startIndex;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0004DC9C File Offset: 0x0004BE9C
		public static int IndexOfValue<TValue>(this TValue[] array, TValue value, int startIndex = 0, int count = -1) where TValue : struct, IEquatable<TValue>
		{
			if (array == null)
			{
				return -1;
			}
			if (count < 0)
			{
				count = array.Length - startIndex;
			}
			for (int i = startIndex; i < startIndex + count; i++)
			{
				if (value.Equals(array[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x0004DCE0 File Offset: 0x0004BEE0
		public static void Resize<TValue>(ref NativeArray<TValue> array, int newSize, Allocator allocator) where TValue : struct
		{
			int length = array.Length;
			if (length == newSize)
			{
				return;
			}
			if (newSize == 0)
			{
				if (array.IsCreated)
				{
					array.Dispose();
				}
				array = default(NativeArray<TValue>);
				return;
			}
			NativeArray<TValue> nativeArray = new NativeArray<TValue>(newSize, allocator, NativeArrayOptions.ClearMemory);
			if (length != 0)
			{
				UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<TValue>(), array.GetUnsafeReadOnlyPtr<TValue>(), (long)(UnsafeUtility.SizeOf<TValue>() * ((newSize < length) ? newSize : length)));
				array.Dispose();
			}
			array = nativeArray;
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0004DD54 File Offset: 0x0004BF54
		public static int Append<TValue>(ref TValue[] array, TValue value)
		{
			if (array == null)
			{
				array = new TValue[1];
				array[0] = value;
				return 0;
			}
			int num = array.Length;
			Array.Resize<TValue>(ref array, num + 1);
			array[num] = value;
			return num;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0004DD90 File Offset: 0x0004BF90
		public static int Append<TValue>(ref TValue[] array, IEnumerable<TValue> values)
		{
			if (array == null)
			{
				array = values.ToArray<TValue>();
				return 0;
			}
			int num = array.Length;
			int num2 = values.Count<TValue>();
			Array.Resize<TValue>(ref array, num + num2);
			int num3 = num;
			foreach (TValue tvalue in values)
			{
				array[num3++] = tvalue;
			}
			return num;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0004DE08 File Offset: 0x0004C008
		public static int AppendToImmutable<TValue>(ref TValue[] array, TValue[] values)
		{
			if (array == null)
			{
				array = values;
				return 0;
			}
			if (values != null && values.Length != 0)
			{
				int num = array.Length;
				int num2 = values.Length;
				Array.Resize<TValue>(ref array, num + num2);
				Array.Copy(values, 0, array, num, num2);
				return num;
			}
			return array.Length;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0004DE48 File Offset: 0x0004C048
		public static int AppendWithCapacity<TValue>(ref TValue[] array, ref int count, TValue value, int capacityIncrement = 10)
		{
			if (array == null)
			{
				array = new TValue[capacityIncrement];
				array[0] = value;
				count++;
				return 0;
			}
			int num = array.Length;
			if (num == count)
			{
				num += capacityIncrement;
				Array.Resize<TValue>(ref array, num);
			}
			int num2 = count;
			array[num2] = value;
			count++;
			return num2;
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0004DE9C File Offset: 0x0004C09C
		public static int AppendListWithCapacity<TValue, TValues>(ref TValue[] array, ref int length, TValues values, int capacityIncrement = 10) where TValues : IReadOnlyList<TValue>
		{
			int count = values.Count;
			if (array == null)
			{
				int num = Math.Max(count, capacityIncrement);
				array = new TValue[num];
				for (int i = 0; i < count; i++)
				{
					array[i] = values[i];
				}
				length += count;
				return 0;
			}
			int num2 = array.Length;
			if (num2 < length + count)
			{
				num2 += Math.Max(length + count, capacityIncrement);
				Array.Resize<TValue>(ref array, num2);
			}
			int num3 = length;
			for (int j = 0; j < count; j++)
			{
				array[num3 + j] = values[j];
			}
			length += count;
			return num3;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0004DF50 File Offset: 0x0004C150
		public static int AppendWithCapacity<TValue>(ref NativeArray<TValue> array, ref int count, TValue value, int capacityIncrement = 10, Allocator allocator = Allocator.Persistent) where TValue : struct
		{
			if (array.Length == count)
			{
				ArrayHelpers.GrowBy<TValue>(ref array, (capacityIncrement > 1) ? capacityIncrement : 1, allocator);
			}
			int num = count;
			array[num] = value;
			count++;
			return num;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0004DF8C File Offset: 0x0004C18C
		public static void InsertAt<TValue>(ref TValue[] array, int index, TValue value)
		{
			if (array != null)
			{
				int num = array.Length;
				Array.Resize<TValue>(ref array, num + 1);
				if (index != num)
				{
					Array.Copy(array, index, array, index + 1, num - index);
				}
				array[index] = value;
				return;
			}
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			array = new TValue[1];
			array[0] = value;
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x0004DFE8 File Offset: 0x0004C1E8
		public static void InsertAtWithCapacity<TValue>(ref TValue[] array, ref int count, int index, TValue value, int capacityIncrement = 10)
		{
			ArrayHelpers.EnsureCapacity<TValue>(ref array, count, count + 1, capacityIncrement);
			if (index != count)
			{
				Array.Copy(array, index, array, index + 1, count - index);
			}
			array[index] = value;
			count++;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0004E020 File Offset: 0x0004C220
		public static void PutAtIfNotSet<TValue>(ref TValue[] array, int index, Func<TValue> valueFn)
		{
			if (array.LengthSafe<TValue>() < index + 1)
			{
				Array.Resize<TValue>(ref array, index + 1);
			}
			if (EqualityComparer<TValue>.Default.Equals(array[index], default(TValue)))
			{
				array[index] = valueFn();
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0004E070 File Offset: 0x0004C270
		public static int GrowBy<TValue>(ref TValue[] array, int count)
		{
			if (array == null)
			{
				array = new TValue[count];
				return 0;
			}
			int num = array.Length;
			Array.Resize<TValue>(ref array, num + count);
			return num;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0004E09C File Offset: 0x0004C29C
		public static int GrowBy<TValue>(ref NativeArray<TValue> array, int count, Allocator allocator = Allocator.Persistent) where TValue : struct
		{
			int length = array.Length;
			if (length == 0)
			{
				array = new NativeArray<TValue>(count, allocator, NativeArrayOptions.ClearMemory);
				return 0;
			}
			NativeArray<TValue> nativeArray = new NativeArray<TValue>(length + count, allocator, NativeArrayOptions.ClearMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<TValue>(), array.GetUnsafeReadOnlyPtr<TValue>(), (long)length * (long)UnsafeUtility.SizeOf<TValue>());
			array.Dispose();
			array = nativeArray;
			return length;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0004E0FC File Offset: 0x0004C2FC
		public static int GrowWithCapacity<TValue>(ref TValue[] array, ref int count, int growBy, int capacityIncrement = 10)
		{
			if (((array != null) ? array.Length : 0) < count + growBy)
			{
				if (capacityIncrement < growBy)
				{
					capacityIncrement = growBy;
				}
				ArrayHelpers.GrowBy<TValue>(ref array, capacityIncrement);
			}
			int num = count;
			count += growBy;
			return num;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0004E126 File Offset: 0x0004C326
		public static int GrowWithCapacity<TValue>(ref NativeArray<TValue> array, ref int count, int growBy, int capacityIncrement = 10, Allocator allocator = Allocator.Persistent) where TValue : struct
		{
			if (array.Length < count + growBy)
			{
				if (capacityIncrement < growBy)
				{
					capacityIncrement = growBy;
				}
				ArrayHelpers.GrowBy<TValue>(ref array, capacityIncrement, allocator);
			}
			int num = count;
			count += growBy;
			return num;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0004E150 File Offset: 0x0004C350
		public static TValue[] Join<TValue>(TValue value, params TValue[] values)
		{
			int num = 0;
			if (value != null)
			{
				num++;
			}
			if (values != null)
			{
				num += values.Length;
			}
			if (num == 0)
			{
				return null;
			}
			TValue[] array = new TValue[num];
			int num2 = 0;
			if (value != null)
			{
				array[num2++] = value;
			}
			if (values != null)
			{
				Array.Copy(values, 0, array, num2, values.Length);
			}
			return array;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0004E1A8 File Offset: 0x0004C3A8
		public static TValue[] Merge<TValue>(TValue[] first, TValue[] second) where TValue : IEquatable<TValue>
		{
			if (first == null)
			{
				return second;
			}
			if (second == null)
			{
				return first;
			}
			List<TValue> list = new List<TValue>();
			list.AddRange(first);
			for (int i = 0; i < second.Length; i++)
			{
				TValue secondValue = second[i];
				if (!list.Exists((TValue x) => x.Equals(secondValue)))
				{
					list.Add(secondValue);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0004E214 File Offset: 0x0004C414
		public static TValue[] Merge<TValue>(TValue[] first, TValue[] second, IEqualityComparer<TValue> comparer)
		{
			if (first == null)
			{
				return second;
			}
			if (second == null)
			{
				return null;
			}
			List<TValue> list = new List<TValue>();
			list.AddRange(first);
			for (int i = 0; i < second.Length; i++)
			{
				TValue secondValue = second[i];
				if (!list.Exists((TValue x) => comparer.Equals(secondValue)))
				{
					list.Add(secondValue);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0004E294 File Offset: 0x0004C494
		public static void EraseAt<TValue>(ref TValue[] array, int index)
		{
			int num = array.Length;
			if (index == 0 && num == 1)
			{
				array = null;
				return;
			}
			if (index < num - 1)
			{
				Array.Copy(array, index + 1, array, index, num - index - 1);
			}
			Array.Resize<TValue>(ref array, num - 1);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0004E2D4 File Offset: 0x0004C4D4
		public static void EraseAtWithCapacity<TValue>(this TValue[] array, ref int count, int index)
		{
			if (index < count - 1)
			{
				Array.Copy(array, index + 1, array, index, count - index - 1);
			}
			array[count - 1] = default(TValue);
			count--;
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0004E314 File Offset: 0x0004C514
		public unsafe static void EraseAtWithCapacity<TValue>(NativeArray<TValue> array, ref int count, int index) where TValue : struct
		{
			if (index < count - 1)
			{
				int num = UnsafeUtility.SizeOf<TValue>();
				byte* unsafePtr = (byte*)array.GetUnsafePtr<TValue>();
				UnsafeUtility.MemCpy((void*)(unsafePtr + num * index), (void*)(unsafePtr + num * (index + 1)), (long)((count - index - 1) * num));
			}
			count--;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0004E358 File Offset: 0x0004C558
		public static bool Erase<TValue>(ref TValue[] array, TValue value)
		{
			int num = ArrayHelpers.IndexOf<TValue>(array, value, 0, -1);
			if (num != -1)
			{
				ArrayHelpers.EraseAt<TValue>(ref array, num);
				return true;
			}
			return false;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0004E380 File Offset: 0x0004C580
		public static void EraseAtByMovingTail<TValue>(TValue[] array, ref int count, int index)
		{
			if (index != count - 1)
			{
				array[index] = array[count - 1];
			}
			if (count >= 1)
			{
				array[count - 1] = default(TValue);
			}
			count--;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0004E3C4 File Offset: 0x0004C5C4
		public static TValue[] Copy<TValue>(TValue[] array)
		{
			if (array == null)
			{
				return null;
			}
			int num = array.Length;
			TValue[] array2 = new TValue[num];
			Array.Copy(array, array2, num);
			return array2;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0004E3EC File Offset: 0x0004C5EC
		public static TValue[] Clone<TValue>(TValue[] array) where TValue : ICloneable
		{
			if (array == null)
			{
				return null;
			}
			int num = array.Length;
			TValue[] array2 = new TValue[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = (TValue)((object)array[i].Clone());
			}
			return array2;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0004E438 File Offset: 0x0004C638
		public static TNew[] Select<TOld, TNew>(TOld[] array, Func<TOld, TNew> converter)
		{
			if (array == null)
			{
				return null;
			}
			int num = array.Length;
			TNew[] array2 = new TNew[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = converter(array[i]);
			}
			return array2;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0004E478 File Offset: 0x0004C678
		private static void Swap<TValue>(ref TValue first, ref TValue second)
		{
			TValue tvalue = first;
			first = second;
			second = tvalue;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0004E4A0 File Offset: 0x0004C6A0
		public static void MoveSlice<TValue>(TValue[] array, int sourceIndex, int destinationIndex, int count)
		{
			if (count <= 0 || sourceIndex == destinationIndex)
			{
				return;
			}
			int num;
			if (destinationIndex > sourceIndex)
			{
				num = destinationIndex + count - sourceIndex;
			}
			else
			{
				num = sourceIndex + count - destinationIndex;
			}
			if (num == count * 2)
			{
				for (int i = 0; i < count; i++)
				{
					ArrayHelpers.Swap<TValue>(ref array[sourceIndex + i], ref array[destinationIndex + i]);
				}
				return;
			}
			int num2 = num - 1;
			int num3 = destinationIndex;
			for (int j = 0; j < num2; j++)
			{
				ArrayHelpers.Swap<TValue>(ref array[num3], ref array[sourceIndex]);
				if (destinationIndex > sourceIndex)
				{
					num3 -= count;
					if (num3 < sourceIndex)
					{
						num3 = destinationIndex + count - Math.Abs(sourceIndex - num3);
					}
				}
				else
				{
					num3 += count;
					if (num3 >= sourceIndex + count)
					{
						num3 = destinationIndex + (num3 - (sourceIndex + count));
					}
				}
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0004E548 File Offset: 0x0004C748
		public static void EraseSliceWithCapacity<TValue>(ref TValue[] array, ref int length, int index, int count)
		{
			if (count < length)
			{
				Array.Copy(array, index + count, array, index, length - index - count);
			}
			for (int i = 0; i < count; i++)
			{
				array[length - i - 1] = default(TValue);
			}
			length -= count;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0004E595 File Offset: 0x0004C795
		public static void SwapElements<TValue>(this TValue[] array, int index1, int index2)
		{
			MemoryHelpers.Swap<TValue>(ref array[index1], ref array[index2]);
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0004E5AC File Offset: 0x0004C7AC
		public static void SwapElements<TValue>(this NativeArray<TValue> array, int index1, int index2) where TValue : struct
		{
			TValue tvalue = array[index1];
			array[index1] = array[index2];
			array[index2] = tvalue;
		}
	}
}

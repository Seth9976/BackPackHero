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
		// Token: 0x06001046 RID: 4166 RVA: 0x0004DA27 File Offset: 0x0004BC27
		public static int LengthSafe<TValue>(this TValue[] array)
		{
			if (array == null)
			{
				return 0;
			}
			return array.Length;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0004DA31 File Offset: 0x0004BC31
		public static void Clear<TValue>(this TValue[] array)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, array.Length);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0004DA41 File Offset: 0x0004BC41
		public static void Clear<TValue>(this TValue[] array, int count)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, count);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0004DA4F File Offset: 0x0004BC4F
		public static void Clear<TValue>(this TValue[] array, ref int count)
		{
			if (array == null)
			{
				return;
			}
			Array.Clear(array, 0, count);
			count = 0;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0004DA61 File Offset: 0x0004BC61
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

		// Token: 0x0600104B RID: 4171 RVA: 0x0004DA90 File Offset: 0x0004BC90
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

		// Token: 0x0600104C RID: 4172 RVA: 0x0004DACC File Offset: 0x0004BCCC
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

		// Token: 0x0600104D RID: 4173 RVA: 0x0004DB05 File Offset: 0x0004BD05
		public static bool ContainsReference<TValue>(this TValue[] array, TValue value) where TValue : class
		{
			return array != null && array.ContainsReference(array.Length, value);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0004DB16 File Offset: 0x0004BD16
		public static bool ContainsReference<TFirst, TSecond>(this TFirst[] array, int count, TSecond value) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, count) != -1;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0004DB26 File Offset: 0x0004BD26
		public static bool ContainsReference<TFirst, TSecond>(this TFirst[] array, int startIndex, int count, TSecond value) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, startIndex, count) != -1;
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0004DB38 File Offset: 0x0004BD38
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

		// Token: 0x06001051 RID: 4177 RVA: 0x0004DB80 File Offset: 0x0004BD80
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

		// Token: 0x06001052 RID: 4178 RVA: 0x0004DBE0 File Offset: 0x0004BDE0
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

		// Token: 0x06001053 RID: 4179 RVA: 0x0004DC24 File Offset: 0x0004BE24
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

		// Token: 0x06001054 RID: 4180 RVA: 0x0004DC58 File Offset: 0x0004BE58
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

		// Token: 0x06001055 RID: 4181 RVA: 0x0004DC97 File Offset: 0x0004BE97
		public static int IndexOfReference<TFirst, TSecond>(this TFirst[] array, TSecond value, int count = -1) where TFirst : TSecond where TSecond : class
		{
			return array.IndexOfReference(value, 0, count);
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0004DCA4 File Offset: 0x0004BEA4
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

		// Token: 0x06001057 RID: 4183 RVA: 0x0004DCE8 File Offset: 0x0004BEE8
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

		// Token: 0x06001058 RID: 4184 RVA: 0x0004DD2C File Offset: 0x0004BF2C
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

		// Token: 0x06001059 RID: 4185 RVA: 0x0004DDA0 File Offset: 0x0004BFA0
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

		// Token: 0x0600105A RID: 4186 RVA: 0x0004DDDC File Offset: 0x0004BFDC
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

		// Token: 0x0600105B RID: 4187 RVA: 0x0004DE54 File Offset: 0x0004C054
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

		// Token: 0x0600105C RID: 4188 RVA: 0x0004DE94 File Offset: 0x0004C094
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

		// Token: 0x0600105D RID: 4189 RVA: 0x0004DEE8 File Offset: 0x0004C0E8
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

		// Token: 0x0600105E RID: 4190 RVA: 0x0004DF9C File Offset: 0x0004C19C
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

		// Token: 0x0600105F RID: 4191 RVA: 0x0004DFD8 File Offset: 0x0004C1D8
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

		// Token: 0x06001060 RID: 4192 RVA: 0x0004E034 File Offset: 0x0004C234
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

		// Token: 0x06001061 RID: 4193 RVA: 0x0004E06C File Offset: 0x0004C26C
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

		// Token: 0x06001062 RID: 4194 RVA: 0x0004E0BC File Offset: 0x0004C2BC
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

		// Token: 0x06001063 RID: 4195 RVA: 0x0004E0E8 File Offset: 0x0004C2E8
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

		// Token: 0x06001064 RID: 4196 RVA: 0x0004E148 File Offset: 0x0004C348
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

		// Token: 0x06001065 RID: 4197 RVA: 0x0004E172 File Offset: 0x0004C372
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

		// Token: 0x06001066 RID: 4198 RVA: 0x0004E19C File Offset: 0x0004C39C
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

		// Token: 0x06001067 RID: 4199 RVA: 0x0004E1F4 File Offset: 0x0004C3F4
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

		// Token: 0x06001068 RID: 4200 RVA: 0x0004E260 File Offset: 0x0004C460
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

		// Token: 0x06001069 RID: 4201 RVA: 0x0004E2E0 File Offset: 0x0004C4E0
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

		// Token: 0x0600106A RID: 4202 RVA: 0x0004E320 File Offset: 0x0004C520
		public static void EraseAtWithCapacity<TValue>(this TValue[] array, ref int count, int index)
		{
			if (index < count - 1)
			{
				Array.Copy(array, index + 1, array, index, count - index - 1);
			}
			array[count - 1] = default(TValue);
			count--;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0004E360 File Offset: 0x0004C560
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

		// Token: 0x0600106C RID: 4204 RVA: 0x0004E3A4 File Offset: 0x0004C5A4
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

		// Token: 0x0600106D RID: 4205 RVA: 0x0004E3CC File Offset: 0x0004C5CC
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

		// Token: 0x0600106E RID: 4206 RVA: 0x0004E410 File Offset: 0x0004C610
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

		// Token: 0x0600106F RID: 4207 RVA: 0x0004E438 File Offset: 0x0004C638
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

		// Token: 0x06001070 RID: 4208 RVA: 0x0004E484 File Offset: 0x0004C684
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

		// Token: 0x06001071 RID: 4209 RVA: 0x0004E4C4 File Offset: 0x0004C6C4
		private static void Swap<TValue>(ref TValue first, ref TValue second)
		{
			TValue tvalue = first;
			first = second;
			second = tvalue;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0004E4EC File Offset: 0x0004C6EC
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

		// Token: 0x06001073 RID: 4211 RVA: 0x0004E594 File Offset: 0x0004C794
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

		// Token: 0x06001074 RID: 4212 RVA: 0x0004E5E1 File Offset: 0x0004C7E1
		public static void SwapElements<TValue>(this TValue[] array, int index1, int index2)
		{
			MemoryHelpers.Swap<TValue>(ref array[index1], ref array[index2]);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0004E5F8 File Offset: 0x0004C7F8
		public static void SwapElements<TValue>(this NativeArray<TValue> array, int index1, int index2) where TValue : struct
		{
			TValue tvalue = array[index1];
			array[index1] = array[index2];
			array[index2] = tvalue;
		}
	}
}

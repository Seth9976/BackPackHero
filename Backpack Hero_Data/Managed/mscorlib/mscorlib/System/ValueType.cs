using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	/// <summary>Provides the base class for value types.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000266 RID: 614
	[ComVisible(true)]
	[Serializable]
	public abstract class ValueType
	{
		// Token: 0x06001C0E RID: 7182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalEquals(object o1, object o2, out object[] fields);

		// Token: 0x06001C0F RID: 7183 RVA: 0x00068AB4 File Offset: 0x00066CB4
		internal static bool DefaultEquals(object o1, object o2)
		{
			if (o1 == null && o2 == null)
			{
				return true;
			}
			if (o1 == null || o2 == null)
			{
				return false;
			}
			RuntimeType runtimeType = (RuntimeType)o1.GetType();
			RuntimeType runtimeType2 = (RuntimeType)o2.GetType();
			if (runtimeType != runtimeType2)
			{
				return false;
			}
			object[] array;
			bool flag = ValueType.InternalEquals(o1, o2, out array);
			if (array == null)
			{
				return flag;
			}
			for (int i = 0; i < array.Length; i += 2)
			{
				object obj = array[i];
				object obj2 = array[i + 1];
				if (obj == null)
				{
					if (obj2 != null)
					{
						return false;
					}
				}
				else if (!obj.Equals(obj2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false. </returns>
		/// <param name="obj">The object to compare with the current instance. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C10 RID: 7184 RVA: 0x000558B7 File Offset: 0x00053AB7
		public override bool Equals(object obj)
		{
			return ValueType.DefaultEquals(this, obj);
		}

		// Token: 0x06001C11 RID: 7185
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int InternalGetHashCode(object o, out object[] fields);

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C12 RID: 7186 RVA: 0x00068B34 File Offset: 0x00066D34
		public override int GetHashCode()
		{
			object[] array;
			int num = ValueType.InternalGetHashCode(this, out array);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						num ^= array[i].GetHashCode();
					}
				}
			}
			return num;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00068B6C File Offset: 0x00066D6C
		internal static int GetHashCodeOfPtr(IntPtr ptr)
		{
			int num = (int)ptr;
			int num2 = ValueType.Internal.hash_code_of_ptr_seed;
			if (num2 == 0)
			{
				num2 = num;
				Interlocked.CompareExchange(ref ValueType.Internal.hash_code_of_ptr_seed, num2, 0);
				num2 = ValueType.Internal.hash_code_of_ptr_seed;
			}
			return num - num2;
		}

		/// <summary>Returns the fully qualified type name of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001C14 RID: 7188 RVA: 0x00068BA1 File Offset: 0x00066DA1
		public override string ToString()
		{
			return base.GetType().FullName;
		}

		// Token: 0x02000267 RID: 615
		private static class Internal
		{
			// Token: 0x040019A5 RID: 6565
			public static int hash_code_of_ptr_seed;
		}
	}
}

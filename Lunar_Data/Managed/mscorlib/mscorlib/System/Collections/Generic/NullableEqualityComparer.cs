using System;

namespace System.Collections.Generic
{
	// Token: 0x02000AC7 RID: 2759
	[Serializable]
	internal class NullableEqualityComparer<T> : EqualityComparer<T?> where T : struct, IEquatable<T>
	{
		// Token: 0x06006298 RID: 25240 RVA: 0x0014A09D File Offset: 0x0014829D
		public override bool Equals(T? x, T? y)
		{
			if (x != null)
			{
				return y != null && x.value.Equals(y.value);
			}
			return y == null;
		}

		// Token: 0x06006299 RID: 25241 RVA: 0x0014A0D8 File Offset: 0x001482D8
		public override int GetHashCode(T? obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x0014A0E8 File Offset: 0x001482E8
		internal override int IndexOf(T?[] array, T? value, int startIndex, int count)
		{
			int num = startIndex + count;
			if (value == null)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num; j++)
				{
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x0014A160 File Offset: 0x00148360
		internal override int LastIndexOf(T?[] array, T? value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			if (value == null)
			{
				for (int i = startIndex; i >= num; i--)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j >= num; j--)
				{
					if (array[j] != null && array[j].value.Equals(value.value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600629C RID: 25244 RVA: 0x0014A1D7 File Offset: 0x001483D7
		public override bool Equals(object obj)
		{
			return obj is NullableEqualityComparer<T>;
		}

		// Token: 0x0600629D RID: 25245 RVA: 0x00149C6E File Offset: 0x00147E6E
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}

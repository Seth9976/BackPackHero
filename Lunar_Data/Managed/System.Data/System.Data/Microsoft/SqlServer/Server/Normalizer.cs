using System;
using System.IO;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003AE RID: 942
	internal abstract class Normalizer
	{
		// Token: 0x06002E8F RID: 11919 RVA: 0x000CA3C0 File Offset: 0x000C85C0
		internal static Normalizer GetNormalizer(Type t)
		{
			Normalizer normalizer = null;
			if (t.IsPrimitive)
			{
				if (t == typeof(byte))
				{
					normalizer = new ByteNormalizer();
				}
				else if (t == typeof(sbyte))
				{
					normalizer = new SByteNormalizer();
				}
				else if (t == typeof(bool))
				{
					normalizer = new BooleanNormalizer();
				}
				else if (t == typeof(short))
				{
					normalizer = new ShortNormalizer();
				}
				else if (t == typeof(ushort))
				{
					normalizer = new UShortNormalizer();
				}
				else if (t == typeof(int))
				{
					normalizer = new IntNormalizer();
				}
				else if (t == typeof(uint))
				{
					normalizer = new UIntNormalizer();
				}
				else if (t == typeof(float))
				{
					normalizer = new FloatNormalizer();
				}
				else if (t == typeof(double))
				{
					normalizer = new DoubleNormalizer();
				}
				else if (t == typeof(long))
				{
					normalizer = new LongNormalizer();
				}
				else if (t == typeof(ulong))
				{
					normalizer = new ULongNormalizer();
				}
			}
			else if (t.IsValueType)
			{
				normalizer = new BinaryOrderedUdtNormalizer(t, false);
			}
			if (normalizer == null)
			{
				throw new Exception(SR.GetString("Cannot create normalizer for '{0}'.", new object[] { t.FullName }));
			}
			normalizer._skipNormalize = false;
			return normalizer;
		}

		// Token: 0x06002E90 RID: 11920
		internal abstract void Normalize(FieldInfo fi, object recvr, Stream s);

		// Token: 0x06002E91 RID: 11921
		internal abstract void DeNormalize(FieldInfo fi, object recvr, Stream s);

		// Token: 0x06002E92 RID: 11922 RVA: 0x000CA544 File Offset: 0x000C8744
		protected void FlipAllBits(byte[] b)
		{
			for (int i = 0; i < b.Length; i++)
			{
				b[i] = ~b[i];
			}
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000CA567 File Offset: 0x000C8767
		protected object GetValue(FieldInfo fi, object obj)
		{
			return fi.GetValue(obj);
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000CA570 File Offset: 0x000C8770
		protected void SetValue(FieldInfo fi, object recvr, object value)
		{
			fi.SetValue(recvr, value);
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06002E95 RID: 11925
		internal abstract int Size { get; }

		// Token: 0x04001BAB RID: 7083
		protected bool _skipNormalize;
	}
}

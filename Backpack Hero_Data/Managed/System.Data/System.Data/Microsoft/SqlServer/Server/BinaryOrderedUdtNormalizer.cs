using System;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003AD RID: 941
	internal sealed class BinaryOrderedUdtNormalizer : Normalizer
	{
		// Token: 0x06002E86 RID: 11910 RVA: 0x000CA092 File Offset: 0x000C8292
		private FieldInfo[] GetFields(Type t)
		{
			return t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000CA09C File Offset: 0x000C829C
		internal BinaryOrderedUdtNormalizer(Type t, bool isTopLevelUdt)
		{
			this._skipNormalize = false;
			if (this._skipNormalize)
			{
				this._isTopLevelUdt = true;
			}
			this._isTopLevelUdt = true;
			FieldInfo[] fields = this.GetFields(t);
			this.FieldsToNormalize = new FieldInfoEx[fields.Length];
			int num = 0;
			foreach (FieldInfo fieldInfo in fields)
			{
				int num2 = Marshal.OffsetOf(fieldInfo.DeclaringType, fieldInfo.Name).ToInt32();
				this.FieldsToNormalize[num++] = new FieldInfoEx(fieldInfo, num2, Normalizer.GetNormalizer(fieldInfo.FieldType));
			}
			Array.Sort<FieldInfoEx>(this.FieldsToNormalize);
			if (!this._isTopLevelUdt && typeof(INullable).IsAssignableFrom(t))
			{
				PropertyInfo property = t.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
				if (property == null || property.PropertyType != t)
				{
					FieldInfo field = t.GetField("Null", BindingFlags.Static | BindingFlags.Public);
					if (field == null || field.FieldType != t)
					{
						throw new Exception("could not find Null field/property in nullable type " + ((t != null) ? t.ToString() : null));
					}
					this.NullInstance = field.GetValue(null);
				}
				else
				{
					this.NullInstance = property.GetValue(null, null);
				}
				this._padBuffer = new byte[this.Size - 1];
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x000CA202 File Offset: 0x000C8402
		internal bool IsNullable
		{
			get
			{
				return this.NullInstance != null;
			}
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000CA20D File Offset: 0x000C840D
		internal void NormalizeTopObject(object udt, Stream s)
		{
			this.Normalize(null, udt, s);
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000CA218 File Offset: 0x000C8418
		internal object DeNormalizeTopObject(Type t, Stream s)
		{
			return this.DeNormalizeInternal(t, s);
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000CA224 File Offset: 0x000C8424
		[MethodImpl(MethodImplOptions.NoInlining)]
		private object DeNormalizeInternal(Type t, Stream s)
		{
			object obj = null;
			if (!this._isTopLevelUdt && typeof(INullable).IsAssignableFrom(t) && (byte)s.ReadByte() == 0)
			{
				obj = this.NullInstance;
				s.Read(this._padBuffer, 0, this._padBuffer.Length);
				return obj;
			}
			if (obj == null)
			{
				obj = Activator.CreateInstance(t);
			}
			foreach (FieldInfoEx fieldInfoEx in this.FieldsToNormalize)
			{
				fieldInfoEx.Normalizer.DeNormalize(fieldInfoEx.FieldInfo, obj, s);
			}
			return obj;
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000CA2AC File Offset: 0x000C84AC
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			object obj2;
			if (fi == null)
			{
				obj2 = obj;
			}
			else
			{
				obj2 = base.GetValue(fi, obj);
			}
			INullable nullable = obj2 as INullable;
			if (nullable != null && !this._isTopLevelUdt)
			{
				if (nullable.IsNull)
				{
					s.WriteByte(0);
					s.Write(this._padBuffer, 0, this._padBuffer.Length);
					return;
				}
				s.WriteByte(1);
			}
			foreach (FieldInfoEx fieldInfoEx in this.FieldsToNormalize)
			{
				fieldInfoEx.Normalizer.Normalize(fieldInfoEx.FieldInfo, obj2, s);
			}
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000CA33C File Offset: 0x000C853C
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			base.SetValue(fi, recvr, this.DeNormalizeInternal(fi.FieldType, s));
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x000CA354 File Offset: 0x000C8554
		internal override int Size
		{
			get
			{
				if (this._size != 0)
				{
					return this._size;
				}
				if (this.IsNullable && !this._isTopLevelUdt)
				{
					this._size = 1;
				}
				foreach (FieldInfoEx fieldInfoEx in this.FieldsToNormalize)
				{
					this._size += fieldInfoEx.Normalizer.Size;
				}
				return this._size;
			}
		}

		// Token: 0x04001BA6 RID: 7078
		internal readonly FieldInfoEx[] FieldsToNormalize;

		// Token: 0x04001BA7 RID: 7079
		private int _size;

		// Token: 0x04001BA8 RID: 7080
		private byte[] _padBuffer;

		// Token: 0x04001BA9 RID: 7081
		internal readonly object NullInstance;

		// Token: 0x04001BAA RID: 7082
		private bool _isTopLevelUdt;
	}
}

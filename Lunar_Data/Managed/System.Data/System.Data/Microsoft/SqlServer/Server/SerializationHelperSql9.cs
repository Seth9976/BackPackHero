using System;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003BA RID: 954
	internal class SerializationHelperSql9
	{
		// Token: 0x06002EC3 RID: 11971 RVA: 0x00003D55 File Offset: 0x00001F55
		private SerializationHelperSql9()
		{
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000CAB9C File Offset: 0x000C8D9C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static int SizeInBytes(Type t)
		{
			return SerializationHelperSql9.SizeInBytes(Activator.CreateInstance(t));
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000CABAC File Offset: 0x000C8DAC
		internal static int SizeInBytes(object instance)
		{
			SerializationHelperSql9.GetFormat(instance.GetType());
			DummyStream dummyStream = new DummyStream();
			SerializationHelperSql9.GetSerializer(instance.GetType()).Serialize(dummyStream, instance);
			return (int)dummyStream.Length;
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000CABE4 File Offset: 0x000C8DE4
		internal static void Serialize(Stream s, object instance)
		{
			SerializationHelperSql9.GetSerializer(instance.GetType()).Serialize(s, instance);
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000CABF8 File Offset: 0x000C8DF8
		internal static object Deserialize(Stream s, Type resultType)
		{
			return SerializationHelperSql9.GetSerializer(resultType).Deserialize(s);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000CAC06 File Offset: 0x000C8E06
		private static Format GetFormat(Type t)
		{
			return SerializationHelperSql9.GetUdtAttribute(t).Format;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000CAC14 File Offset: 0x000C8E14
		private static Serializer GetSerializer(Type t)
		{
			if (SerializationHelperSql9.s_types2Serializers == null)
			{
				SerializationHelperSql9.s_types2Serializers = new Hashtable();
			}
			Serializer serializer = (Serializer)SerializationHelperSql9.s_types2Serializers[t];
			if (serializer == null)
			{
				serializer = SerializationHelperSql9.GetNewSerializer(t);
				SerializationHelperSql9.s_types2Serializers[t] = serializer;
			}
			return serializer;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000CAC5C File Offset: 0x000C8E5C
		internal static int GetUdtMaxLength(Type t)
		{
			SqlUdtInfo fromType = SqlUdtInfo.GetFromType(t);
			if (Format.Native == fromType.SerializationFormat)
			{
				return SerializationHelperSql9.SizeInBytes(t);
			}
			return fromType.MaxByteSize;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x000CAC86 File Offset: 0x000C8E86
		private static object[] GetCustomAttributes(Type t)
		{
			return t.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), false);
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000CAC9C File Offset: 0x000C8E9C
		internal static SqlUserDefinedTypeAttribute GetUdtAttribute(Type t)
		{
			object[] customAttributes = SerializationHelperSql9.GetCustomAttributes(t);
			if (customAttributes != null && customAttributes.Length == 1)
			{
				return (SqlUserDefinedTypeAttribute)customAttributes[0];
			}
			throw InvalidUdtException.Create(t, "no UDT attribute");
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000CACD4 File Offset: 0x000C8ED4
		private static Serializer GetNewSerializer(Type t)
		{
			SerializationHelperSql9.GetUdtAttribute(t);
			Format format = SerializationHelperSql9.GetFormat(t);
			switch (format)
			{
			case Format.Native:
				return new NormalizedSerializer(t);
			case Format.UserDefined:
				return new BinarySerializeSerializer(t);
			}
			throw ADP.InvalidUserDefinedTypeSerializationFormat(format);
		}

		// Token: 0x04001BAC RID: 7084
		[ThreadStatic]
		private static Hashtable s_types2Serializers;
	}
}

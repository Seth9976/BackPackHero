using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x020003A1 RID: 929
	internal abstract class SmiRecordBuffer : SmiTypedGetterSetter, ITypedGettersV3, ITypedSettersV3, ITypedGetters, ITypedSetters, IDisposable
	{
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002C70 RID: 11376 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal override bool CanGet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x0000CD07 File Offset: 0x0000AF07
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void Dispose()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual bool IsDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlDbType GetVariantType(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual bool GetBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual byte GetByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual char GetChar(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual short GetInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual int GetInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual long GetInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual float GetFloat(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual double GetDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual string GetString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual decimal GetDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual DateTime GetDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual Guid GetGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlBoolean GetSqlBoolean(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlByte GetSqlByte(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlInt16 GetSqlInt16(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlInt32 GetSqlInt32(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlInt64 GetSqlInt64(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlSingle GetSqlSingle(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlDouble GetSqlDouble(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlMoney GetSqlMoney(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlDateTime GetSqlDateTime(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlDecimal GetSqlDecimal(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlString GetSqlString(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlBinary GetSqlBinary(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlGuid GetSqlGuid(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlChars GetSqlChars(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlBytes GetSqlBytes(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlXml GetSqlXml(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlXml GetSqlXmlRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlBytes GetSqlBytesRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual SqlChars GetSqlCharsRef(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetDBNull(int ordinal)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetBoolean(int ordinal, bool value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetByte(int ordinal, byte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetChar(int ordinal, char value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetInt16(int ordinal, short value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetInt32(int ordinal, int value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetInt64(int ordinal, long value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetFloat(int ordinal, float value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetDouble(int ordinal, double value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetString(int ordinal, string value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetString(int ordinal, string value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetDecimal(int ordinal, decimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetDateTime(int ordinal, DateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetGuid(int ordinal, Guid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlBoolean(int ordinal, SqlBoolean value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlByte(int ordinal, SqlByte value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlInt16(int ordinal, SqlInt16 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlInt32(int ordinal, SqlInt32 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlInt64(int ordinal, SqlInt64 value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlSingle(int ordinal, SqlSingle value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlDouble(int ordinal, SqlDouble value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlMoney(int ordinal, SqlMoney value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlDateTime(int ordinal, SqlDateTime value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlDecimal(int ordinal, SqlDecimal value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlString(int ordinal, SqlString value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlString(int ordinal, SqlString value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlBinary(int ordinal, SqlBinary value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlBinary(int ordinal, SqlBinary value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlGuid(int ordinal, SqlGuid value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlChars(int ordinal, SqlChars value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlChars(int ordinal, SqlChars value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlBytes(int ordinal, SqlBytes value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlBytes(int ordinal, SqlBytes value, int offset)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x000C2577 File Offset: 0x000C0777
		public virtual void SetSqlXml(int ordinal, SqlXml value)
		{
			throw ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000216 RID: 534
	internal class SqlMetaDataPriv
	{
		// Token: 0x060018DF RID: 6367 RVA: 0x0007D9F8 File Offset: 0x0007BBF8
		internal SqlMetaDataPriv()
		{
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0007DA18 File Offset: 0x0007BC18
		internal virtual void CopyFrom(SqlMetaDataPriv original)
		{
			this.type = original.type;
			this.tdsType = original.tdsType;
			this.precision = original.precision;
			this.scale = original.scale;
			this.length = original.length;
			this.collation = original.collation;
			this.codePage = original.codePage;
			this.encoding = original.encoding;
			this.isNullable = original.isNullable;
			this.isMultiValued = original.isMultiValued;
			this.udtDatabaseName = original.udtDatabaseName;
			this.udtSchemaName = original.udtSchemaName;
			this.udtTypeName = original.udtTypeName;
			this.udtAssemblyQualifiedName = original.udtAssemblyQualifiedName;
			this.udtType = original.udtType;
			this.xmlSchemaCollectionDatabase = original.xmlSchemaCollectionDatabase;
			this.xmlSchemaCollectionOwningSchema = original.xmlSchemaCollectionOwningSchema;
			this.xmlSchemaCollectionName = original.xmlSchemaCollectionName;
			this.metaType = original.metaType;
			this.structuredTypeDatabaseName = original.structuredTypeDatabaseName;
			this.structuredTypeSchemaName = original.structuredTypeSchemaName;
			this.structuredTypeName = original.structuredTypeName;
			this.structuredFields = original.structuredFields;
		}

		// Token: 0x040011E3 RID: 4579
		internal SqlDbType type;

		// Token: 0x040011E4 RID: 4580
		internal byte tdsType;

		// Token: 0x040011E5 RID: 4581
		internal byte precision = byte.MaxValue;

		// Token: 0x040011E6 RID: 4582
		internal byte scale = byte.MaxValue;

		// Token: 0x040011E7 RID: 4583
		internal int length;

		// Token: 0x040011E8 RID: 4584
		internal SqlCollation collation;

		// Token: 0x040011E9 RID: 4585
		internal int codePage;

		// Token: 0x040011EA RID: 4586
		internal Encoding encoding;

		// Token: 0x040011EB RID: 4587
		internal bool isNullable;

		// Token: 0x040011EC RID: 4588
		internal bool isMultiValued;

		// Token: 0x040011ED RID: 4589
		internal string udtDatabaseName;

		// Token: 0x040011EE RID: 4590
		internal string udtSchemaName;

		// Token: 0x040011EF RID: 4591
		internal string udtTypeName;

		// Token: 0x040011F0 RID: 4592
		internal string udtAssemblyQualifiedName;

		// Token: 0x040011F1 RID: 4593
		internal Type udtType;

		// Token: 0x040011F2 RID: 4594
		internal string xmlSchemaCollectionDatabase;

		// Token: 0x040011F3 RID: 4595
		internal string xmlSchemaCollectionOwningSchema;

		// Token: 0x040011F4 RID: 4596
		internal string xmlSchemaCollectionName;

		// Token: 0x040011F5 RID: 4597
		internal MetaType metaType;

		// Token: 0x040011F6 RID: 4598
		internal string structuredTypeDatabaseName;

		// Token: 0x040011F7 RID: 4599
		internal string structuredTypeSchemaName;

		// Token: 0x040011F8 RID: 4600
		internal string structuredTypeName;

		// Token: 0x040011F9 RID: 4601
		internal IList<SmiMetaData> structuredFields;
	}
}

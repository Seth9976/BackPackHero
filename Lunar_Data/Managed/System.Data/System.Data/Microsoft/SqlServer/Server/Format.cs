using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used by <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> and <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedAggregateAttribute" /> to indicate the serialization format of a user-defined type (UDT) or aggregate.</summary>
	// Token: 0x020003C9 RID: 969
	public enum Format
	{
		/// <summary>The serialization format is unknown.</summary>
		// Token: 0x04001BD7 RID: 7127
		Unknown,
		/// <summary>The Native serialization format uses a very simple algorithm that enables SQL Server to store an efficient representation of the UDT on disk. Types marked for Native serialization can only have value types (structs in Microsoft Visual C# and structures in Microsoft Visual Basic .NET) as members. Members of reference types (such as classes in Visual C# and Visual Basic), either user-defined or those existing in the framework (such as <see cref="T:System.String" />), are not supported.</summary>
		// Token: 0x04001BD8 RID: 7128
		Native,
		/// <summary>The UserDefined serialization format gives the developer full control over the binary format through the <see cref="T:Microsoft.SqlServer.Server.IBinarySerialize" />.Write and <see cref="T:Microsoft.SqlServer.Server.IBinarySerialize" />.Read methods.</summary>
		// Token: 0x04001BD9 RID: 7129
		UserDefined
	}
}

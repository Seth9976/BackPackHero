using System;

namespace System.Data
{
	/// <summary>Used by the Visual Basic .NET Data Designers to represent a parameter to a Command object, and optionally, its mapping to <see cref="T:System.Data.DataSet" /> columns.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B5 RID: 181
	public interface IDbDataParameter : IDataParameter
	{
		/// <summary>Indicates the precision of numeric parameters.</summary>
		/// <returns>The maximum number of digits used to represent the Value property of a data provider Parameter object. The default value is 0, which indicates that a data provider sets the precision for Value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000B70 RID: 2928
		// (set) Token: 0x06000B71 RID: 2929
		byte Precision { get; set; }

		/// <summary>Indicates the scale of numeric parameters.</summary>
		/// <returns>The number of decimal places to which <see cref="T:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000B72 RID: 2930
		// (set) Token: 0x06000B73 RID: 2931
		byte Scale { get; set; }

		/// <summary>The size of the parameter.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the the parameter value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000B74 RID: 2932
		// (set) Token: 0x06000B75 RID: 2933
		int Size { get; set; }
	}
}

using System;

namespace System.Data
{
	/// <summary>Specifies the type of a parameter within a query relative to the <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C2 RID: 194
	public enum ParameterDirection
	{
		/// <summary>The parameter is an input parameter.</summary>
		// Token: 0x04000781 RID: 1921
		Input = 1,
		/// <summary>The parameter is an output parameter.</summary>
		// Token: 0x04000782 RID: 1922
		Output,
		/// <summary>The parameter is capable of both input and output.</summary>
		// Token: 0x04000783 RID: 1923
		InputOutput,
		/// <summary>The parameter represents a return value from an operation such as a stored procedure, built-in function, or user-defined function.</summary>
		// Token: 0x04000784 RID: 1924
		ReturnValue = 6
	}
}

using System;

namespace System.ComponentModel
{
	/// <summary>Provides the functionality to offer custom error information that a user interface can bind to.</summary>
	// Token: 0x020006C9 RID: 1737
	public interface IDataErrorInfo
	{
		/// <summary>Gets the error message for the property with the given name.</summary>
		/// <returns>The error message for the property. The default is an empty string ("").</returns>
		/// <param name="columnName">The name of the property whose error message to get. </param>
		// Token: 0x17000CCE RID: 3278
		string this[string columnName] { get; }

		/// <summary>Gets an error message indicating what is wrong with this object.</summary>
		/// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06003792 RID: 14226
		string Error { get; }
	}
}

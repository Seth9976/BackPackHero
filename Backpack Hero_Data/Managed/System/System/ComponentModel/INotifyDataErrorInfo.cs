using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Defines members that data entity classes can implement to provide custom synchronous and asynchronous validation support.</summary>
	// Token: 0x02000715 RID: 1813
	public interface INotifyDataErrorInfo
	{
		/// <summary>Gets a value that indicates whether the entity has validation errors. </summary>
		/// <returns>true if the entity currently has validation errors; otherwise, false.</returns>
		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x060039C2 RID: 14786
		bool HasErrors { get; }

		/// <summary>Gets the validation errors for a specified property or for the entire entity.</summary>
		/// <returns>The validation errors for the property or entity.</returns>
		/// <param name="propertyName">The name of the property to retrieve validation errors for; or null or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
		// Token: 0x060039C3 RID: 14787
		IEnumerable GetErrors(string propertyName);

		/// <summary>Occurs when the validation errors have changed for a property or for the entire entity. </summary>
		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060039C4 RID: 14788
		// (remove) Token: 0x060039C5 RID: 14789
		event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
	}
}

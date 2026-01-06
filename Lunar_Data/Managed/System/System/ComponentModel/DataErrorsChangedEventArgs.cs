using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyDataErrorInfo.ErrorsChanged" /> event.</summary>
	// Token: 0x02000714 RID: 1812
	public class DataErrorsChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataErrorsChangedEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property that has an error.  null or <see cref="F:System.String.Empty" /> if the error is object-level.</param>
		// Token: 0x060039C0 RID: 14784 RVA: 0x000C8D8D File Offset: 0x000C6F8D
		public DataErrorsChangedEventArgs(string propertyName)
		{
			this._propertyName = propertyName;
		}

		/// <summary>Gets the name of the property that has an error.</summary>
		/// <returns>The name of the property that has an error. null or <see cref="F:System.String.Empty" /> if the error is object-level.</returns>
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000C8D9C File Offset: 0x000C6F9C
		public virtual string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x04002166 RID: 8550
		private readonly string _propertyName;
	}
}

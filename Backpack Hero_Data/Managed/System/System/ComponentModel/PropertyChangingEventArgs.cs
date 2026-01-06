using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanging.PropertyChanging" /> event. </summary>
	// Token: 0x0200071A RID: 1818
	public class PropertyChangingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangingEventArgs" /> class. </summary>
		/// <param name="propertyName">The name of the property whose value is changing.</param>
		// Token: 0x060039D0 RID: 14800 RVA: 0x000C8DBB File Offset: 0x000C6FBB
		public PropertyChangingEventArgs(string propertyName)
		{
			this._propertyName = propertyName;
		}

		/// <summary>Gets the name of the property whose value is changing.</summary>
		/// <returns>The name of the property whose value is changing.</returns>
		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x060039D1 RID: 14801 RVA: 0x000C8DCA File Offset: 0x000C6FCA
		public virtual string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x04002168 RID: 8552
		private readonly string _propertyName;
	}
}

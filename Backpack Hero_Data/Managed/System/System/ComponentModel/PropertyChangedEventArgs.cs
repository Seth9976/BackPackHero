using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event.</summary>
	// Token: 0x02000718 RID: 1816
	public class PropertyChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property that changed. </param>
		// Token: 0x060039CA RID: 14794 RVA: 0x000C8DA4 File Offset: 0x000C6FA4
		public PropertyChangedEventArgs(string propertyName)
		{
			this._propertyName = propertyName;
		}

		/// <summary>Gets the name of the property that changed.</summary>
		/// <returns>The name of the property that changed.</returns>
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x060039CB RID: 14795 RVA: 0x000C8DB3 File Offset: 0x000C6FB3
		public virtual string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x04002167 RID: 8551
		private readonly string _propertyName;
	}
}

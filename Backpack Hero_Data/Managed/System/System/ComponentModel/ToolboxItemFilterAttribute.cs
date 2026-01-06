using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the filter string and filter type to use for a toolbox item.</summary>
	// Token: 0x02000705 RID: 1797
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	[Serializable]
	public sealed class ToolboxItemFilterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string.</summary>
		/// <param name="filterString">The filter string for the toolbox item. </param>
		// Token: 0x06003973 RID: 14707 RVA: 0x000C869F File Offset: 0x000C689F
		public ToolboxItemFilterAttribute(string filterString)
			: this(filterString, ToolboxItemFilterType.Allow)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string and type.</summary>
		/// <param name="filterString">The filter string for the toolbox item. </param>
		/// <param name="filterType">A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> indicating the type of the filter. </param>
		// Token: 0x06003974 RID: 14708 RVA: 0x000C86A9 File Offset: 0x000C68A9
		public ToolboxItemFilterAttribute(string filterString, ToolboxItemFilterType filterType)
		{
			this.FilterString = filterString ?? string.Empty;
			this.FilterType = filterType;
		}

		/// <summary>Gets the filter string for the toolbox item.</summary>
		/// <returns>The filter string for the toolbox item.</returns>
		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x000C86C8 File Offset: 0x000C68C8
		public string FilterString { get; }

		/// <summary>Gets the type of the filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> that indicates the type of the filter.</returns>
		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06003976 RID: 14710 RVA: 0x000C86D0 File Offset: 0x000C68D0
		public ToolboxItemFilterType FilterType { get; }

		/// <summary>Gets the type ID for the attribute.</summary>
		/// <returns>The type ID for this attribute. All <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects with the same filter string return the same type ID.</returns>
		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06003977 RID: 14711 RVA: 0x000C86D8 File Offset: 0x000C68D8
		public override object TypeId
		{
			get
			{
				string text;
				if ((text = this._typeId) == null)
				{
					text = (this._typeId = base.GetType().FullName + this.FilterString);
				}
				return text;
			}
		}

		/// <param name="obj">The object to compare.</param>
		// Token: 0x06003978 RID: 14712 RVA: 0x000C8710 File Offset: 0x000C6910
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterType.Equals(this.FilterType) && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000C8761 File Offset: 0x000C6961
		public override int GetHashCode()
		{
			return this.FilterString.GetHashCode();
		}

		/// <summary>Indicates whether the specified object has a matching filter string.</summary>
		/// <returns>true if the specified object has a matching filter string; otherwise, false.</returns>
		/// <param name="obj">The object to test for a matching filter string. </param>
		// Token: 0x0600397A RID: 14714 RVA: 0x000C8770 File Offset: 0x000C6970
		public override bool Match(object obj)
		{
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000C879F File Offset: 0x000C699F
		public override string ToString()
		{
			return this.FilterString + "," + Enum.GetName(typeof(ToolboxItemFilterType), this.FilterType);
		}

		// Token: 0x04002154 RID: 8532
		private string _typeId;
	}
}

using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Represents an attribute of a toolbox item.</summary>
	// Token: 0x020006B7 RID: 1719
	[AttributeUsage(AttributeTargets.All)]
	public class ToolboxItemAttribute : Attribute
	{
		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x060036E5 RID: 14053 RVA: 0x000C2B8D File Offset: 0x000C0D8D
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ToolboxItemAttribute.Default);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and specifies whether to use default initialization values.</summary>
		/// <param name="defaultType">true to create a toolbox item attribute for a default type; false to associate no default toolbox item support for this attribute. </param>
		// Token: 0x060036E6 RID: 14054 RVA: 0x000C2B9A File Offset: 0x000C0D9A
		public ToolboxItemAttribute(bool defaultType)
		{
			if (defaultType)
			{
				this._toolboxItemTypeName = "System.Drawing.Design.ToolboxItem, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class using the specified name of the type.</summary>
		/// <param name="toolboxItemTypeName">The names of the type of the toolbox item and of the assembly that contains the type. </param>
		// Token: 0x060036E7 RID: 14055 RVA: 0x000C2BB0 File Offset: 0x000C0DB0
		public ToolboxItemAttribute(string toolboxItemTypeName)
		{
			toolboxItemTypeName.ToUpper(CultureInfo.InvariantCulture);
			this._toolboxItemTypeName = toolboxItemTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class using the specified type of the toolbox item.</summary>
		/// <param name="toolboxItemType">The type of the toolbox item. </param>
		// Token: 0x060036E8 RID: 14056 RVA: 0x000C2BCB File Offset: 0x000C0DCB
		public ToolboxItemAttribute(Type toolboxItemType)
		{
			this._toolboxItemType = toolboxItemType;
			this._toolboxItemTypeName = toolboxItemType.AssemblyQualifiedName;
		}

		/// <summary>Gets or sets the type of the toolbox item.</summary>
		/// <returns>The type of the toolbox item.</returns>
		/// <exception cref="T:System.ArgumentException">The type cannot be found. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060036E9 RID: 14057 RVA: 0x000C2BE8 File Offset: 0x000C0DE8
		public Type ToolboxItemType
		{
			get
			{
				if (this._toolboxItemType == null && this._toolboxItemTypeName != null)
				{
					try
					{
						this._toolboxItemType = Type.GetType(this._toolboxItemTypeName, true);
					}
					catch (Exception ex)
					{
						throw new ArgumentException(SR.Format("Failed to create ToolboxItem of type: {0}", this._toolboxItemTypeName), ex);
					}
				}
				return this._toolboxItemType;
			}
		}

		/// <summary>Gets or sets the name of the type of the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <returns>The fully qualified type name of the current toolbox item.</returns>
		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060036EA RID: 14058 RVA: 0x000C2C50 File Offset: 0x000C0E50
		public string ToolboxItemTypeName
		{
			get
			{
				if (this._toolboxItemTypeName == null)
				{
					return string.Empty;
				}
				return this._toolboxItemTypeName;
			}
		}

		/// <param name="obj">The object to compare.</param>
		// Token: 0x060036EB RID: 14059 RVA: 0x000C2C68 File Offset: 0x000C0E68
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemAttribute toolboxItemAttribute = obj as ToolboxItemAttribute;
			return toolboxItemAttribute != null && toolboxItemAttribute.ToolboxItemTypeName == this.ToolboxItemTypeName;
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000C2C98 File Offset: 0x000C0E98
		public override int GetHashCode()
		{
			if (this._toolboxItemTypeName != null)
			{
				return this._toolboxItemTypeName.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x04002099 RID: 8345
		private Type _toolboxItemType;

		// Token: 0x0400209A RID: 8346
		private string _toolboxItemTypeName;

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and sets the type to the default, <see cref="T:System.Drawing.Design.ToolboxItem" />. This field is read-only.</summary>
		// Token: 0x0400209B RID: 8347
		public static readonly ToolboxItemAttribute Default = new ToolboxItemAttribute("System.Drawing.Design.ToolboxItem, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemAttribute" /> class and sets the type to null. This field is read-only.</summary>
		// Token: 0x0400209C RID: 8348
		public static readonly ToolboxItemAttribute None = new ToolboxItemAttribute(false);
	}
}

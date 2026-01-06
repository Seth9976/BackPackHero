using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Specifies the editor to use to change a property. This class cannot be inherited.</summary>
	// Token: 0x020006BA RID: 1722
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public sealed class EditorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the default editor, which is no editor.</summary>
		// Token: 0x060036FB RID: 14075 RVA: 0x000C2DBD File Offset: 0x000C0FBD
		public EditorAttribute()
		{
			this.EditorTypeName = string.Empty;
			this.EditorBaseTypeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and base type name of the editor.</summary>
		/// <param name="typeName">The fully qualified type name of the editor. </param>
		/// <param name="baseTypeName">The fully qualified type name of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x060036FC RID: 14076 RVA: 0x000C2DDB File Offset: 0x000C0FDB
		public EditorAttribute(string typeName, string baseTypeName)
		{
			typeName.ToUpper(CultureInfo.InvariantCulture);
			this.EditorTypeName = typeName;
			this.EditorBaseTypeName = baseTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and the base type.</summary>
		/// <param name="typeName">The fully qualified type name of the editor. </param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x060036FD RID: 14077 RVA: 0x000C2DFD File Offset: 0x000C0FFD
		public EditorAttribute(string typeName, Type baseType)
		{
			typeName.ToUpper(CultureInfo.InvariantCulture);
			this.EditorTypeName = typeName;
			this.EditorBaseTypeName = baseType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type and the base type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the editor. </param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x060036FE RID: 14078 RVA: 0x000C2E24 File Offset: 0x000C1024
		public EditorAttribute(Type type, Type baseType)
		{
			this.EditorTypeName = type.AssemblyQualifiedName;
			this.EditorBaseTypeName = baseType.AssemblyQualifiedName;
		}

		/// <summary>Gets the name of the base class or interface serving as a lookup key for this editor.</summary>
		/// <returns>The name of the base class or interface serving as a lookup key for this editor.</returns>
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x000C2E44 File Offset: 0x000C1044
		public string EditorBaseTypeName { get; }

		/// <summary>Gets the name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</summary>
		/// <returns>The name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</returns>
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x000C2E4C File Offset: 0x000C104C
		public string EditorTypeName { get; }

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x000C2E54 File Offset: 0x000C1054
		public override object TypeId
		{
			get
			{
				if (this._typeId == null)
				{
					string text = this.EditorBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this._typeId = base.GetType().FullName + text;
				}
				return this._typeId;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.EditorAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current object; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06003702 RID: 14082 RVA: 0x000C2EA4 File Offset: 0x000C10A4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			EditorAttribute editorAttribute = obj as EditorAttribute;
			return editorAttribute != null && editorAttribute.EditorTypeName == this.EditorTypeName && editorAttribute.EditorBaseTypeName == this.EditorBaseTypeName;
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000BE516 File Offset: 0x000BC716
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040020A1 RID: 8353
		private string _typeId;
	}
}

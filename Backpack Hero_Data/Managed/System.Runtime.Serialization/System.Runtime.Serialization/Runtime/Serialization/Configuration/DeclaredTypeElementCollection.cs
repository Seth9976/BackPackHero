using System;
using System.Configuration;

namespace System.Runtime.Serialization.Configuration
{
	/// <summary>Handles the XML elements used to configure XML serialization using the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
	// Token: 0x020001A2 RID: 418
	[ConfigurationCollection(typeof(DeclaredTypeElement))]
	public sealed class DeclaredTypeElementCollection : ConfigurationElementCollection
	{
		/// <summary>Returns the configuration element specified by its index.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.Configuration.DeclaredTypeElement" /> specified by its index.</returns>
		/// <param name="index">The index of the element to access.</param>
		// Token: 0x17000464 RID: 1124
		public DeclaredTypeElement this[int index]
		{
			get
			{
				return (DeclaredTypeElement)base.BaseGet(index);
			}
			set
			{
				if (!this.IsReadOnly())
				{
					if (value == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
					}
					if (base.BaseGet(index) != null)
					{
						base.BaseRemoveAt(index);
					}
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Returns the item specified by its key.</summary>
		/// <returns>The configuration element specified by its key.</returns>
		/// <param name="typeName">The name of the type (that functions as a key) to return.</param>
		// Token: 0x17000465 RID: 1125
		public DeclaredTypeElement this[string typeName]
		{
			get
			{
				if (string.IsNullOrEmpty(typeName))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
				}
				return (DeclaredTypeElement)base.BaseGet(typeName);
			}
			set
			{
				if (!this.IsReadOnly())
				{
					if (string.IsNullOrEmpty(typeName))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
					}
					if (value == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
					}
					if (base.BaseGet(typeName) == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new IndexOutOfRangeException(global::System.Runtime.Serialization.SR.GetString("For type '{0}', configuration index is out of range.", new object[] { typeName })));
					}
					base.BaseRemove(typeName);
				}
				this.Add(value);
			}
		}

		/// <summary>Adds the specified configuration element.</summary>
		/// <param name="element">The <see cref="T:System.Runtime.Serialization.Configuration.DeclaredTypeElement" /> to add.</param>
		// Token: 0x0600152B RID: 5419 RVA: 0x00053FFA File Offset: 0x000521FA
		public void Add(DeclaredTypeElement element)
		{
			if (!this.IsReadOnly() && element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			this.BaseAdd(element);
		}

		/// <summary>Clears the collection of all items.</summary>
		// Token: 0x0600152C RID: 5420 RVA: 0x00054019 File Offset: 0x00052219
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns a value if the collection contains the item specified by its type name.</summary>
		/// <returns>true if the collection contains the specified item; otherwise, false.</returns>
		/// <param name="typeName">The name of the configuration element to search for.</param>
		// Token: 0x0600152D RID: 5421 RVA: 0x00054021 File Offset: 0x00052221
		public bool Contains(string typeName)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
			}
			return base.BaseGet(typeName) != null;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00054040 File Offset: 0x00052240
		protected override ConfigurationElement CreateNewElement()
		{
			return new DeclaredTypeElement();
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00054047 File Offset: 0x00052247
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			return ((DeclaredTypeElement)element).Type;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The index of the specified configuration element; otherwise, -1.</returns>
		/// <param name="element">The <see cref="T:System.Runtime.Serialization.Configuration.DeclaredTypeElement" /> to find.</param>
		// Token: 0x06001530 RID: 5424 RVA: 0x00054062 File Offset: 0x00052262
		public int IndexOf(DeclaredTypeElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Runtime.Serialization.Configuration.DeclaredTypeElement" /> to remove.</param>
		// Token: 0x06001531 RID: 5425 RVA: 0x00054079 File Offset: 0x00052279
		public void Remove(DeclaredTypeElement element)
		{
			if (!this.IsReadOnly() && element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			base.BaseRemove(this.GetElementKey(element));
		}

		/// <summary>Removes the configuration element specified by its key.</summary>
		/// <param name="typeName">The type name (that functions as a key) of the configuration element to remove.</param>
		// Token: 0x06001532 RID: 5426 RVA: 0x0005409E File Offset: 0x0005229E
		public void Remove(string typeName)
		{
			if (!this.IsReadOnly() && string.IsNullOrEmpty(typeName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("typeName");
			}
			base.BaseRemove(typeName);
		}

		/// <summary>Removes the configuration element at the specified index.</summary>
		/// <param name="index">The index of the configuration element to remove.</param>
		// Token: 0x06001533 RID: 5427 RVA: 0x000540C2 File Offset: 0x000522C2
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}

using System;

namespace System.ComponentModel
{
	/// <summary>Provides metadata for a property representing a data field. This class cannot be inherited.</summary>
	// Token: 0x020006AA RID: 1706
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DataObjectFieldAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		// Token: 0x06003695 RID: 13973 RVA: 0x000C2196 File Offset: 0x000C0396
		public DataObjectFieldAttribute(bool primaryKey)
			: this(primaryKey, false, false, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, and whether the field is a database identity field.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		// Token: 0x06003696 RID: 13974 RVA: 0x000C21A2 File Offset: 0x000C03A2
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity)
			: this(primaryKey, isIdentity, false, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether the field is a database identity field, and whether the field can be null.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		/// <param name="isNullable">true to indicate that the field can be null in the data store; otherwise, false.</param>
		// Token: 0x06003697 RID: 13975 RVA: 0x000C21AE File Offset: 0x000C03AE
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable)
			: this(primaryKey, isIdentity, isNullable, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether it is a database identity field, and whether it can be null and sets the length of the field.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		/// <param name="isNullable">true to indicate that the field can be null in the data store; otherwise, false.</param>
		/// <param name="length">The length of the field in bytes.</param>
		// Token: 0x06003698 RID: 13976 RVA: 0x000C21BA File Offset: 0x000C03BA
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable, int length)
		{
			this.PrimaryKey = primaryKey;
			this.IsIdentity = isIdentity;
			this.IsNullable = isNullable;
			this.Length = length;
		}

		/// <summary>Gets a value indicating whether a property represents an identity field in the underlying data.</summary>
		/// <returns>true if the property represents an identity field in the underlying data; otherwise, false. The default value is false.</returns>
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x000C21DF File Offset: 0x000C03DF
		public bool IsIdentity { get; }

		/// <summary>Gets a value indicating whether a property represents a field that can be null in the underlying data store.</summary>
		/// <returns>true if the property represents a field that can be null in the underlying data store; otherwise, false.</returns>
		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600369A RID: 13978 RVA: 0x000C21E7 File Offset: 0x000C03E7
		public bool IsNullable { get; }

		/// <summary>Gets the length of the property in bytes.</summary>
		/// <returns>The length of the property in bytes, or -1 if not set.</returns>
		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x0600369B RID: 13979 RVA: 0x000C21EF File Offset: 0x000C03EF
		public int Length { get; }

		/// <summary>Gets a value indicating whether a property is in the primary key in the underlying data.</summary>
		/// <returns>true if the property is in the primary key of the data store; otherwise, false.</returns>
		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x0600369C RID: 13980 RVA: 0x000C21F7 File Offset: 0x000C03F7
		public bool PrimaryKey { get; }

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectFieldAttribute" />.</param>
		// Token: 0x0600369D RID: 13981 RVA: 0x000C2200 File Offset: 0x000C0400
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectFieldAttribute dataObjectFieldAttribute = obj as DataObjectFieldAttribute;
			return dataObjectFieldAttribute != null && dataObjectFieldAttribute.IsIdentity == this.IsIdentity && dataObjectFieldAttribute.IsNullable == this.IsNullable && dataObjectFieldAttribute.Length == this.Length && dataObjectFieldAttribute.PrimaryKey == this.PrimaryKey;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600369E RID: 13982 RVA: 0x000BE516 File Offset: 0x000BC716
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}

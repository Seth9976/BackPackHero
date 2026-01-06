using System;

namespace System.Configuration
{
	/// <summary>Used internally as the class that represents metadata about an individual configuration property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CC RID: 460
	public class SettingsProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class, based on the supplied parameter.</summary>
		/// <param name="propertyToCopy">Specifies a copy of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000BF8 RID: 3064 RVA: 0x00031DCC File Offset: 0x0002FFCC
		public SettingsProperty(SettingsProperty propertyToCopy)
			: this(propertyToCopy.Name, propertyToCopy.PropertyType, propertyToCopy.Provider, propertyToCopy.IsReadOnly, propertyToCopy.DefaultValue, propertyToCopy.SerializeAs, new SettingsAttributeDictionary(propertyToCopy.Attributes), propertyToCopy.ThrowOnErrorDeserializing, propertyToCopy.ThrowOnErrorSerializing)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class. based on the supplied parameter.</summary>
		/// <param name="name">Specifies the name of an existing <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		// Token: 0x06000BF9 RID: 3065 RVA: 0x00031E1C File Offset: 0x0003001C
		public SettingsProperty(string name)
			: this(name, null, null, false, null, SettingsSerializeAs.String, new SettingsAttributeDictionary(), false, false)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.SettingsProperty" /> class based on the supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="propertyType">The type of <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="provider">A <see cref="T:System.Configuration.SettingsProvider" /> object to use for persistence.</param>
		/// <param name="isReadOnly">A <see cref="T:System.Boolean" /> value specifying whether the <see cref="T:System.Configuration.SettingsProperty" /> object is read-only.</param>
		/// <param name="defaultValue">The default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</param>
		/// <param name="serializeAs">A <see cref="T:System.Configuration.SettingsSerializeAs" /> object. This object is an enumeration used to set the serialization scheme for storing application settings.</param>
		/// <param name="attributes">A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</param>
		/// <param name="throwOnErrorDeserializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</param>
		/// <param name="throwOnErrorSerializing">A Boolean value specifying whether an error will be thrown when the property is unsuccessfully serialized.</param>
		// Token: 0x06000BFA RID: 3066 RVA: 0x00031E3C File Offset: 0x0003003C
		public SettingsProperty(string name, Type propertyType, SettingsProvider provider, bool isReadOnly, object defaultValue, SettingsSerializeAs serializeAs, SettingsAttributeDictionary attributes, bool throwOnErrorDeserializing, bool throwOnErrorSerializing)
		{
			this.name = name;
			this.propertyType = propertyType;
			this.provider = provider;
			this.isReadOnly = isReadOnly;
			this.defaultValue = defaultValue;
			this.serializeAs = serializeAs;
			this.attributes = attributes;
			this.throwOnErrorDeserializing = throwOnErrorDeserializing;
			this.throwOnErrorSerializing = throwOnErrorSerializing;
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object containing the attributes of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsAttributeDictionary" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00031E94 File Offset: 0x00030094
		public virtual SettingsAttributeDictionary Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets or sets the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</summary>
		/// <returns>An object containing the default value of the <see cref="T:System.Configuration.SettingsProperty" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00031E9C File Offset: 0x0003009C
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x00031EA4 File Offset: 0x000300A4
		public virtual object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether a <see cref="T:System.Configuration.SettingsProperty" /> object is read-only. </summary>
		/// <returns>true if the <see cref="T:System.Configuration.SettingsProperty" /> is read-only; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00031EAD File Offset: 0x000300AD
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x00031EB5 File Offset: 0x000300B5
		public virtual bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00031EBE File Offset: 0x000300BE
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00031EC6 File Offset: 0x000300C6
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the type for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>The type for the <see cref="T:System.Configuration.SettingsProperty" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00031ECF File Offset: 0x000300CF
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00031ED7 File Offset: 0x000300D7
		public virtual Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
			set
			{
				this.propertyType = value;
			}
		}

		/// <summary>Gets or sets the provider for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsProvider" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00031EE0 File Offset: 0x000300E0
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00031EE8 File Offset: 0x000300E8
		public virtual SettingsProvider Provider
		{
			get
			{
				return this.provider;
			}
			set
			{
				this.provider = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Configuration.SettingsSerializeAs" /> object for the <see cref="T:System.Configuration.SettingsProperty" />.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsSerializeAs" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00031EF1 File Offset: 0x000300F1
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x00031EF9 File Offset: 0x000300F9
		public virtual SettingsSerializeAs SerializeAs
		{
			get
			{
				return this.serializeAs;
			}
			set
			{
				this.serializeAs = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully deserialized.</summary>
		/// <returns>true if the error will be thrown when the property is unsuccessfully deserialized; otherwise, false.</returns>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00031F02 File Offset: 0x00030102
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x00031F0A File Offset: 0x0003010A
		public bool ThrowOnErrorDeserializing
		{
			get
			{
				return this.throwOnErrorDeserializing;
			}
			set
			{
				this.throwOnErrorDeserializing = value;
			}
		}

		/// <summary>Gets or sets a value specifying whether an error will be thrown when the property is unsuccessfully serialized.</summary>
		/// <returns>true if the error will be thrown when the property is unsuccessfully serialized; otherwise, false.</returns>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00031F13 File Offset: 0x00030113
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00031F1B File Offset: 0x0003011B
		public bool ThrowOnErrorSerializing
		{
			get
			{
				return this.throwOnErrorSerializing;
			}
			set
			{
				this.throwOnErrorSerializing = value;
			}
		}

		// Token: 0x0400079C RID: 1948
		private string name;

		// Token: 0x0400079D RID: 1949
		private Type propertyType;

		// Token: 0x0400079E RID: 1950
		private SettingsProvider provider;

		// Token: 0x0400079F RID: 1951
		private bool isReadOnly;

		// Token: 0x040007A0 RID: 1952
		private object defaultValue;

		// Token: 0x040007A1 RID: 1953
		private SettingsSerializeAs serializeAs;

		// Token: 0x040007A2 RID: 1954
		private SettingsAttributeDictionary attributes;

		// Token: 0x040007A3 RID: 1955
		private bool throwOnErrorDeserializing;

		// Token: 0x040007A4 RID: 1956
		private bool throwOnErrorSerializing;
	}
}

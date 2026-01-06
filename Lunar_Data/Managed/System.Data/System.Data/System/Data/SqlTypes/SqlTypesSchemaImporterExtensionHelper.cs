using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Serialization.Advanced;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x020002DA RID: 730
	public class SqlTypesSchemaImporterExtensionHelper : SchemaImporterExtension
	{
		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
		/// <param name="name">The name as a string.</param>
		/// <param name="targetNamespace">The target namespace.</param>
		/// <param name="references">String array of references.</param>
		/// <param name="namespaceImports">Array of CodeNamespaceImport objects.</param>
		/// <param name="destinationType">The destination type as a string.</param>
		/// <param name="direct">A Boolean for direct.</param>
		// Token: 0x0600222D RID: 8749 RVA: 0x0009E5E5 File Offset: 0x0009C7E5
		public SqlTypesSchemaImporterExtensionHelper(string name, string targetNamespace, string[] references, CodeNamespaceImport[] namespaceImports, string destinationType, bool direct)
		{
			this.Init(name, targetNamespace, references, namespaceImports, destinationType, direct);
		}

		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
		/// <param name="name">The name as a string.</param>
		/// <param name="destinationType">The destination type as a string.</param>
		// Token: 0x0600222E RID: 8750 RVA: 0x0009E5FC File Offset: 0x0009C7FC
		public SqlTypesSchemaImporterExtensionHelper(string name, string destinationType)
		{
			this.Init(name, SqlTypesSchemaImporterExtensionHelper.SqlTypesNamespace, null, null, destinationType, true);
		}

		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
		/// <param name="name">The name as a string.</param>
		/// <param name="destinationType">The destination type as a string.</param>
		/// <param name="direct">A Boolean.</param>
		// Token: 0x0600222F RID: 8751 RVA: 0x0009E614 File Offset: 0x0009C814
		public SqlTypesSchemaImporterExtensionHelper(string name, string destinationType, bool direct)
		{
			this.Init(name, SqlTypesSchemaImporterExtensionHelper.SqlTypesNamespace, null, null, destinationType, direct);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0009E62C File Offset: 0x0009C82C
		private void Init(string name, string targetNamespace, string[] references, CodeNamespaceImport[] namespaceImports, string destinationType, bool direct)
		{
			this.m_name = name;
			this.m_targetNamespace = targetNamespace;
			if (references == null)
			{
				this.m_references = new string[1];
				this.m_references[0] = "System.Data.dll";
			}
			else
			{
				this.m_references = references;
			}
			if (namespaceImports == null)
			{
				this.m_namespaceImports = new CodeNamespaceImport[2];
				this.m_namespaceImports[0] = new CodeNamespaceImport("System.Data");
				this.m_namespaceImports[1] = new CodeNamespaceImport("System.Data.SqlTypes");
			}
			else
			{
				this.m_namespaceImports = namespaceImports;
			}
			this.m_destinationType = destinationType;
			this.m_direct = direct;
		}

		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
		/// <returns>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</returns>
		/// <param name="name">
		///   <paramref name="name" />
		/// </param>
		/// <param name="xmlNamespace">
		///   <paramref name="xmlNamespace" />
		/// </param>
		/// <param name="context">
		///   <paramref name="context" />
		/// </param>
		/// <param name="schemas">
		///   <paramref name="schemas" />
		/// </param>
		/// <param name="importer">
		///   <paramref name="importer" />
		/// </param>
		/// <param name="compileUnit">
		///   <paramref name="compileUnit" />
		/// </param>
		/// <param name="mainNamespace">
		///   <paramref name="mainNamespace" />
		/// </param>
		/// <param name="options">
		///   <paramref name="options" />
		/// </param>
		/// <param name="codeProvider">
		///   <paramref name="codeProvider" />
		/// </param>
		// Token: 0x06002231 RID: 8753 RVA: 0x0009E6BC File Offset: 0x0009C8BC
		public override string ImportSchemaType(string name, string xmlNamespace, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			if (this.m_direct && context is XmlSchemaElement && string.CompareOrdinal(this.m_name, name) == 0 && string.CompareOrdinal(this.m_targetNamespace, xmlNamespace) == 0)
			{
				compileUnit.ReferencedAssemblies.AddRange(this.m_references);
				mainNamespace.Imports.AddRange(this.m_namespaceImports);
				return this.m_destinationType;
			}
			return null;
		}

		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
		/// <returns>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</returns>
		/// <param name="type">
		///   <paramref name="type" />
		/// </param>
		/// <param name="context">
		///   <paramref name="context" />
		/// </param>
		/// <param name="schemas">
		///   <paramref name="schemas" />
		/// </param>
		/// <param name="importer">
		///   <paramref name="importer" />
		/// </param>
		/// <param name="compileUnit">
		///   <paramref name="compileUnit" />
		/// </param>
		/// <param name="mainNamespace">
		///   <paramref name="mainNamespace" />
		/// </param>
		/// <param name="options">
		///   <paramref name="options" />
		/// </param>
		/// <param name="codeProvider">
		///   <paramref name="codeProvider" />
		/// </param>
		// Token: 0x06002232 RID: 8754 RVA: 0x0009E724 File Offset: 0x0009C924
		public override string ImportSchemaType(XmlSchemaType type, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			if (!this.m_direct && type is XmlSchemaSimpleType && context is XmlSchemaElement)
			{
				XmlQualifiedName qualifiedName = ((XmlSchemaSimpleType)type).BaseXmlSchemaType.QualifiedName;
				if (string.CompareOrdinal(this.m_name, qualifiedName.Name) == 0 && string.CompareOrdinal(this.m_targetNamespace, qualifiedName.Namespace) == 0)
				{
					compileUnit.ReferencedAssemblies.AddRange(this.m_references);
					mainNamespace.Imports.AddRange(this.m_namespaceImports);
					return this.m_destinationType;
				}
			}
			return null;
		}

		// Token: 0x04001711 RID: 5905
		private string m_name;

		// Token: 0x04001712 RID: 5906
		private string m_targetNamespace;

		// Token: 0x04001713 RID: 5907
		private string[] m_references;

		// Token: 0x04001714 RID: 5908
		private CodeNamespaceImport[] m_namespaceImports;

		// Token: 0x04001715 RID: 5909
		private string m_destinationType;

		// Token: 0x04001716 RID: 5910
		private bool m_direct;

		/// <summary>The <see cref="T:System.Data.SqlTypes.SqlTypesSchemaImporterExtensionHelper" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality. </summary>
		// Token: 0x04001717 RID: 5911
		protected static readonly string SqlTypesNamespace = "http://schemas.microsoft.com/sqlserver/2004/sqltypes";
	}
}

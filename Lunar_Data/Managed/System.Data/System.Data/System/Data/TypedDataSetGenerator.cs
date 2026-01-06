using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data
{
	/// <summary>Used to create a strongly typed <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>3</filterpriority>
	// Token: 0x02000105 RID: 261
	[Obsolete("TypedDataSetGenerator class will be removed in a future release. Please use System.Data.Design.TypedDataSetGenerator in System.Design.dll.")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, Synchronization = true)]
	public class TypedDataSetGenerator
	{
		/// <summary>Generates a strongly typed <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataSet">The source <see cref="T:System.Data.DataSet" /> that specifies the metadata for the typed <see cref="T:System.Data.DataSet" />. </param>
		/// <param name="codeNamespace">The namespace that provides the target namespace for the typed <see cref="T:System.Data.DataSet" />. </param>
		/// <param name="codeGen">The generator used to create the typed <see cref="T:System.Data.DataSet" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000E23 RID: 3619 RVA: 0x00049BE9 File Offset: 0x00047DE9
		public static void Generate(DataSet dataSet, CodeNamespace codeNamespace, ICodeGenerator codeGen)
		{
			new TypedDataSetGenerator().GenerateCode(dataSet, codeNamespace, codeGen);
			CodeGenerator.ValidateIdentifiers(codeNamespace);
		}

		/// <summary>Transforms a string in a valid, typed <see cref="T:System.Data.DataSet" /> name.</summary>
		/// <returns>A string that is the converted name.</returns>
		/// <param name="name">The source name to transform into a valid, typed <see cref="T:System.Data.DataSet" /> name. </param>
		/// <param name="codeGen">The generator used to perform the conversion. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000E24 RID: 3620 RVA: 0x00049C00 File Offset: 0x00047E00
		public static string GenerateIdName(string name, ICodeGenerator codeGen)
		{
			if (codeGen.IsValidIdentifier(name))
			{
				return name;
			}
			string text = name.Replace(' ', '_');
			if (!codeGen.IsValidIdentifier(text))
			{
				text = "_" + text;
				for (int i = 1; i < text.Length; i++)
				{
					UnicodeCategory unicodeCategory = char.GetUnicodeCategory(text[i]);
					if (unicodeCategory != UnicodeCategory.UppercaseLetter && UnicodeCategory.LowercaseLetter != unicodeCategory && UnicodeCategory.TitlecaseLetter != unicodeCategory && UnicodeCategory.ModifierLetter != unicodeCategory && UnicodeCategory.OtherLetter != unicodeCategory && UnicodeCategory.LetterNumber != unicodeCategory && UnicodeCategory.NonSpacingMark != unicodeCategory && UnicodeCategory.SpacingCombiningMark != unicodeCategory && UnicodeCategory.DecimalDigitNumber != unicodeCategory && UnicodeCategory.ConnectorPunctuation != unicodeCategory)
					{
						text = text.Replace(text[i], '_');
					}
				}
			}
			return text;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00049C90 File Offset: 0x00047E90
		internal CodeTypeDeclaration GenerateCode(DataSet dataSet, CodeNamespace codeNamespace, ICodeGenerator codeGen)
		{
			this.useExtendedNaming = false;
			this.errorList = new ArrayList();
			this.conflictingTables = new ArrayList();
			this.codeGen = codeGen;
			CodeTypeDeclaration codeTypeDeclaration = this.CreateTypedDataSet(dataSet);
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				codeTypeDeclaration.Members.Add(this.CreateTypedRowEventHandler(dataTable));
			}
			foreach (object obj2 in dataSet.Tables)
			{
				DataTable dataTable2 = (DataTable)obj2;
				codeTypeDeclaration.Members.Add(this.CreateTypedTable(dataTable2));
				codeTypeDeclaration.Members.Add(this.CreateTypedRow(dataTable2));
				codeTypeDeclaration.Members.Add(this.CreateTypedRowEvent(dataTable2));
			}
			if (this.errorList.Count > 0)
			{
				throw new TypedDataSetGeneratorException(this.errorList);
			}
			codeNamespace.Types.Add(codeTypeDeclaration);
			return codeTypeDeclaration;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00049DC8 File Offset: 0x00047FC8
		private void InitLookupIdentifiers()
		{
			this.lookupIdentifiers = new Hashtable();
			foreach (PropertyInfo propertyInfo in typeof(DataRow).GetProperties())
			{
				this.lookupIdentifiers[propertyInfo.Name] = "_" + propertyInfo.Name;
			}
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00049E24 File Offset: 0x00048024
		private string FixIdName(string inVarName)
		{
			if (this.lookupIdentifiers == null)
			{
				this.InitLookupIdentifiers();
			}
			string text = (string)this.lookupIdentifiers[inVarName];
			if (text == null)
			{
				text = TypedDataSetGenerator.GenerateIdName(inVarName, this.codeGen);
				while (this.lookupIdentifiers.ContainsValue(text))
				{
					text = "_" + text;
				}
				this.lookupIdentifiers[inVarName] = text;
				if (!this.codeGen.IsValidIdentifier(text))
				{
					this.errorList.Add(Res.GetString("Cannot generate identifier for name '{0}'", new object[] { text }));
				}
			}
			return text;
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00049EB9 File Offset: 0x000480B9
		private static bool isEmpty(string s)
		{
			return s == null || s.Length == 0;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00049ECC File Offset: 0x000480CC
		private string RowClassName(DataTable table)
		{
			string text = (string)table.ExtendedProperties["typedName"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				text = this.FixIdName(table.TableName) + "Row";
			}
			return text;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00049F10 File Offset: 0x00048110
		private string RowBaseClassName(DataTable table)
		{
			if (this.useExtendedNaming)
			{
				string text = (string)table.ExtendedProperties["typedBaseClass"];
				if (TypedDataSetGenerator.isEmpty(text))
				{
					text = (string)table.DataSet.ExtendedProperties["typedBaseClass"];
					if (TypedDataSetGenerator.isEmpty(text))
					{
						text = "DataRow";
					}
				}
				return text;
			}
			return "DataRow";
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00049F74 File Offset: 0x00048174
		private string RowConcreteClassName(DataTable table)
		{
			if (this.useExtendedNaming)
			{
				string text = (string)table.ExtendedProperties["typedConcreteClass"];
				if (TypedDataSetGenerator.isEmpty(text))
				{
					text = this.RowClassName(table);
				}
				return text;
			}
			return this.RowClassName(table);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00049FB8 File Offset: 0x000481B8
		private string TableClassName(DataTable table)
		{
			string text = (string)table.ExtendedProperties["typedPlural"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				text = (string)table.ExtendedProperties["typedName"];
				if (TypedDataSetGenerator.isEmpty(text))
				{
					if (table.DataSet.Tables.InternalIndexOf(table.TableName) == -3 && !this.conflictingTables.Contains(table.TableName))
					{
						this.conflictingTables.Add(table.TableName);
						this.errorList.Add(Res.GetString("There is more than one table with the same name '{0}' (even if namespace is different)", new object[] { table.TableName }));
					}
					text = this.FixIdName(table.TableName);
				}
			}
			return text + "DataTable";
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0004A080 File Offset: 0x00048280
		private string TablePropertyName(DataTable table)
		{
			string text = (string)table.ExtendedProperties["typedPlural"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				text = (string)table.ExtendedProperties["typedName"];
				if (TypedDataSetGenerator.isEmpty(text))
				{
					text = this.FixIdName(table.TableName);
				}
				else
				{
					text += "Table";
				}
			}
			return text;
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0004A0E5 File Offset: 0x000482E5
		private string TableFieldName(DataTable table)
		{
			return "table" + this.TablePropertyName(table);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0004A0F8 File Offset: 0x000482F8
		private string RowColumnPropertyName(DataColumn column)
		{
			string text = (string)column.ExtendedProperties["typedName"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				text = this.FixIdName(column.ColumnName);
			}
			return text;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0004A134 File Offset: 0x00048334
		private string TableColumnFieldName(DataColumn column)
		{
			string text = this.RowColumnPropertyName(column);
			if (string.Compare("column", text, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return "column" + text;
			}
			return "columnField" + text;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0004A16E File Offset: 0x0004836E
		private string TableColumnPropertyName(DataColumn column)
		{
			return this.RowColumnPropertyName(column) + "Column";
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0004A184 File Offset: 0x00048384
		private static int TablesConnectedness(DataTable parentTable, DataTable childTable)
		{
			int num = 0;
			DataRelationCollection parentRelations = childTable.ParentRelations;
			for (int i = 0; i < parentRelations.Count; i++)
			{
				if (parentRelations[i].ParentTable == parentTable)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0004A1C0 File Offset: 0x000483C0
		private string ChildPropertyName(DataRelation relation)
		{
			string text = (string)relation.ExtendedProperties["typedChildren"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				string text2 = (string)relation.ChildTable.ExtendedProperties["typedPlural"];
				if (TypedDataSetGenerator.isEmpty(text2))
				{
					text2 = (string)relation.ChildTable.ExtendedProperties["typedName"];
					if (TypedDataSetGenerator.isEmpty(text2))
					{
						text = "Get" + relation.ChildTable.TableName + "Rows";
						if (1 < TypedDataSetGenerator.TablesConnectedness(relation.ParentTable, relation.ChildTable))
						{
							text = text + "By" + relation.RelationName;
						}
						return this.FixIdName(text);
					}
					text2 += "Rows";
				}
				text = "Get" + text2;
			}
			return text;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0004A298 File Offset: 0x00048498
		private string ParentPropertyName(DataRelation relation)
		{
			string text = (string)relation.ExtendedProperties["typedParent"];
			if (TypedDataSetGenerator.isEmpty(text))
			{
				text = this.RowClassName(relation.ParentTable);
				if (relation.ChildTable == relation.ParentTable || relation.ChildColumnsReference.Length != 1)
				{
					text += "Parent";
				}
				if (1 < TypedDataSetGenerator.TablesConnectedness(relation.ParentTable, relation.ChildTable))
				{
					text = text + "By" + this.FixIdName(relation.RelationName);
				}
			}
			return text;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0004A324 File Offset: 0x00048524
		private string RelationFieldName(DataRelation relation)
		{
			return this.FixIdName("relation" + relation.RelationName);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0004A33C File Offset: 0x0004853C
		private string GetTypeName(Type t)
		{
			return t.FullName;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0004A344 File Offset: 0x00048544
		private bool ChildRelationFollowable(DataRelation relation)
		{
			return relation != null && (relation.ChildTable != relation.ParentTable || relation.ChildTable.Columns.Count != 1);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0004A370 File Offset: 0x00048570
		private static CodeMemberMethod CreateOnRowEventMethod(string eventName, string rowClassName)
		{
			CodeMemberMethod codeMemberMethod = TypedDataSetGenerator.MethodDecl(typeof(void), "OnRow" + eventName, (MemberAttributes)12292);
			codeMemberMethod.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(DataRowChangeEventArgs), "e"));
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Base(), "OnRow" + eventName, TypedDataSetGenerator.Argument("e")));
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Event(rowClassName + eventName), TypedDataSetGenerator.Primitive(null)), TypedDataSetGenerator.Stm(TypedDataSetGenerator.DelegateCall(TypedDataSetGenerator.Event(rowClassName + eventName), TypedDataSetGenerator.New(rowClassName + "ChangeEvent", new CodeExpression[]
			{
				TypedDataSetGenerator.Cast(rowClassName, TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("e"), "Row")),
				TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("e"), "Action")
			})))));
			return codeMemberMethod;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0004A474 File Offset: 0x00048674
		private CodeTypeDeclaration CreateTypedTable(DataTable table)
		{
			string text = this.RowClassName(table);
			string text2 = this.TableClassName(table);
			string text3 = this.RowConcreteClassName(table);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(text2);
			codeTypeDeclaration.BaseTypes.Add(typeof(DataTable));
			codeTypeDeclaration.BaseTypes.Add(typeof(IEnumerable));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Serializable"));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
			for (int i = 0; i < table.Columns.Count; i++)
			{
				codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(typeof(DataColumn), this.TableColumnFieldName(table.Columns[i])));
			}
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.EventDecl(text + "ChangeEventHandler", text + "Changed"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.EventDecl(text + "ChangeEventHandler", text + "Changing"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.EventDecl(text + "ChangeEventHandler", text + "Deleted"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.EventDecl(text + "ChangeEventHandler", text + "Deleting"));
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = (MemberAttributes)4098;
			codeConstructor.BaseConstructorArgs.Add(TypedDataSetGenerator.Str(table.TableName));
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitClass"));
			codeTypeDeclaration.Members.Add(codeConstructor);
			codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Family;
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(SerializationInfo), "info"));
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(StreamingContext), "context"));
			codeConstructor.BaseConstructorArgs.AddRange(new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("info"),
				TypedDataSetGenerator.Argument("context")
			});
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars"));
			codeTypeDeclaration.Members.Add(codeConstructor);
			codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = (MemberAttributes)4098;
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(DataTable), "table"));
			codeConstructor.BaseConstructorArgs.Add(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "TableName"));
			codeConstructor.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "CaseSensitive"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "DataSet"), "CaseSensitive")), TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "CaseSensitive"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "CaseSensitive"))));
			codeConstructor.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "Locale"), "ToString"), TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "DataSet"), "Locale"), "ToString")), TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Locale"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "Locale"))));
			codeConstructor.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "Namespace"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "DataSet"), "Namespace")), TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Namespace"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "Namespace"))));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Prefix"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "Prefix")));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "MinimumCapacity"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "MinimumCapacity")));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "DisplayExpression"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("table"), "DisplayExpression")));
			codeTypeDeclaration.Members.Add(codeConstructor);
			CodeMemberProperty codeMemberProperty = TypedDataSetGenerator.PropertyDecl(typeof(int), "Count", (MemberAttributes)24578);
			codeMemberProperty.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.ComponentModel.Browsable", TypedDataSetGenerator.Primitive(false)));
			codeMemberProperty.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "Count")));
			codeTypeDeclaration.Members.Add(codeMemberProperty);
			for (int j = 0; j < table.Columns.Count; j++)
			{
				DataColumn dataColumn = table.Columns[j];
				CodeMemberProperty codeMemberProperty2 = TypedDataSetGenerator.PropertyDecl(typeof(DataColumn), this.TableColumnPropertyName(dataColumn), (MemberAttributes)4098);
				codeMemberProperty2.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableColumnFieldName(dataColumn))));
				codeTypeDeclaration.Members.Add(codeMemberProperty2);
			}
			CodeMemberProperty codeMemberProperty3 = TypedDataSetGenerator.PropertyDecl(text3, "Item", (MemberAttributes)24578);
			codeMemberProperty3.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(int), "index"));
			codeMemberProperty3.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(text3, TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), TypedDataSetGenerator.Argument("index")))));
			codeTypeDeclaration.Members.Add(codeMemberProperty3);
			CodeMemberMethod codeMemberMethod = TypedDataSetGenerator.MethodDecl(typeof(void), "Add" + text, (MemberAttributes)24578);
			codeMemberMethod.Parameters.Add(TypedDataSetGenerator.ParameterDecl(text3, "row"));
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "Add", TypedDataSetGenerator.Argument("row")));
			codeTypeDeclaration.Members.Add(codeMemberMethod);
			ArrayList arrayList = new ArrayList();
			for (int k = 0; k < table.Columns.Count; k++)
			{
				if (!table.Columns[k].AutoIncrement)
				{
					arrayList.Add(table.Columns[k]);
				}
			}
			CodeMemberMethod codeMemberMethod2 = TypedDataSetGenerator.MethodDecl(text3, "Add" + text, (MemberAttributes)24578);
			DataColumn[] array = new DataColumn[arrayList.Count];
			arrayList.CopyTo(array, 0);
			for (int l = 0; l < array.Length; l++)
			{
				Type dataType = array[l].DataType;
				DataRelation dataRelation = array[l].FindParentRelation();
				if (this.ChildRelationFollowable(dataRelation))
				{
					string text4 = this.RowClassName(dataRelation.ParentTable);
					string text5 = this.FixIdName("parent" + text4 + "By" + dataRelation.RelationName);
					codeMemberMethod2.Parameters.Add(TypedDataSetGenerator.ParameterDecl(text4, text5));
				}
				else
				{
					codeMemberMethod2.Parameters.Add(TypedDataSetGenerator.ParameterDecl(this.GetTypeName(dataType), this.RowColumnPropertyName(array[l])));
				}
			}
			codeMemberMethod2.Statements.Add(TypedDataSetGenerator.VariableDecl(text3, "row" + text, TypedDataSetGenerator.Cast(text3, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "NewRow"))));
			CodeExpression codeExpression = TypedDataSetGenerator.Variable("row" + text);
			CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
			codeAssignStatement.Left = TypedDataSetGenerator.Property(codeExpression, "ItemArray");
			CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression();
			codeArrayCreateExpression.CreateType = TypedDataSetGenerator.Type(typeof(object));
			array = new DataColumn[table.Columns.Count];
			table.Columns.CopyTo(array, 0);
			for (int m = 0; m < array.Length; m++)
			{
				if (array[m].AutoIncrement)
				{
					codeArrayCreateExpression.Initializers.Add(TypedDataSetGenerator.Primitive(null));
				}
				else
				{
					DataRelation dataRelation2 = array[m].FindParentRelation();
					if (this.ChildRelationFollowable(dataRelation2))
					{
						string text6 = this.RowClassName(dataRelation2.ParentTable);
						string text7 = this.FixIdName("parent" + text6 + "By" + dataRelation2.RelationName);
						codeArrayCreateExpression.Initializers.Add(TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Argument(text7), TypedDataSetGenerator.Primitive(dataRelation2.ParentColumnsReference[0].Ordinal)));
					}
					else
					{
						codeArrayCreateExpression.Initializers.Add(TypedDataSetGenerator.Argument(this.RowColumnPropertyName(array[m])));
					}
				}
			}
			codeAssignStatement.Right = codeArrayCreateExpression;
			codeMemberMethod2.Statements.Add(codeAssignStatement);
			codeMemberMethod2.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "Add", codeExpression));
			codeMemberMethod2.Statements.Add(TypedDataSetGenerator.Return(codeExpression));
			codeTypeDeclaration.Members.Add(codeMemberMethod2);
			for (int n = 0; n < table.Constraints.Count; n++)
			{
				if (table.Constraints[n] is UniqueConstraint && ((UniqueConstraint)table.Constraints[n]).IsPrimaryKey)
				{
					DataColumn[] columnsReference = ((UniqueConstraint)table.Constraints[n]).ColumnsReference;
					string text8 = "FindBy";
					bool flag = true;
					for (int num = 0; num < columnsReference.Length; num++)
					{
						text8 += this.RowColumnPropertyName(columnsReference[num]);
						if (columnsReference[num].ColumnMapping != MappingType.Hidden)
						{
							flag = false;
						}
					}
					if (!flag)
					{
						CodeMemberMethod codeMemberMethod3 = TypedDataSetGenerator.MethodDecl(text, this.FixIdName(text8), (MemberAttributes)24578);
						for (int num2 = 0; num2 < columnsReference.Length; num2++)
						{
							codeMemberMethod3.Parameters.Add(TypedDataSetGenerator.ParameterDecl(this.GetTypeName(columnsReference[num2].DataType), this.RowColumnPropertyName(columnsReference[num2])));
						}
						CodeArrayCreateExpression codeArrayCreateExpression2 = new CodeArrayCreateExpression(typeof(object), columnsReference.Length);
						for (int num3 = 0; num3 < columnsReference.Length; num3++)
						{
							codeArrayCreateExpression2.Initializers.Add(TypedDataSetGenerator.Argument(this.RowColumnPropertyName(columnsReference[num3])));
						}
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(text, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "Find", codeArrayCreateExpression2))));
						codeTypeDeclaration.Members.Add(codeMemberMethod3);
					}
				}
			}
			CodeMemberMethod codeMemberMethod4 = TypedDataSetGenerator.MethodDecl(typeof(IEnumerator), "GetEnumerator", (MemberAttributes)24578);
			codeMemberMethod4.ImplementationTypes.Add(TypedDataSetGenerator.Type("System.Collections.IEnumerable"));
			codeMemberMethod4.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "GetEnumerator")));
			codeTypeDeclaration.Members.Add(codeMemberMethod4);
			CodeMemberMethod codeMemberMethod5 = TypedDataSetGenerator.MethodDecl(typeof(DataTable), "Clone", (MemberAttributes)24580);
			codeMemberMethod5.Statements.Add(TypedDataSetGenerator.VariableDecl(text2, "cln", TypedDataSetGenerator.Cast(text2, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Base(), "Clone", new CodeExpression[0]))));
			codeMemberMethod5.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Variable("cln"), "InitVars", new CodeExpression[0]));
			codeMemberMethod5.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Variable("cln")));
			codeTypeDeclaration.Members.Add(codeMemberMethod5);
			CodeMemberMethod codeMemberMethod6 = TypedDataSetGenerator.MethodDecl(typeof(DataTable), "CreateInstance", (MemberAttributes)12292);
			codeMemberMethod6.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.New(text2, new CodeExpression[0])));
			codeTypeDeclaration.Members.Add(codeMemberMethod6);
			CodeMemberMethod codeMemberMethod7 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitClass", MemberAttributes.Private);
			CodeMemberMethod codeMemberMethod8 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitVars", (MemberAttributes)4098);
			for (int num4 = 0; num4 < table.Columns.Count; num4++)
			{
				DataColumn dataColumn2 = table.Columns[num4];
				string text9 = this.TableColumnFieldName(dataColumn2);
				CodeExpression codeExpression2 = TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text9);
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(codeExpression2, TypedDataSetGenerator.New(typeof(DataColumn), new CodeExpression[]
				{
					TypedDataSetGenerator.Str(dataColumn2.ColumnName),
					TypedDataSetGenerator.TypeOf(this.GetTypeName(dataColumn2.DataType)),
					TypedDataSetGenerator.Primitive(null),
					TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(MappingType)), (dataColumn2.ColumnMapping == MappingType.SimpleContent) ? "SimpleContent" : ((dataColumn2.ColumnMapping == MappingType.Attribute) ? "Attribute" : ((dataColumn2.ColumnMapping == MappingType.Hidden) ? "Hidden" : "Element")))
				})));
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Columns"), "Add", TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text9)));
			}
			for (int num5 = 0; num5 < table.Constraints.Count; num5++)
			{
				if (table.Constraints[num5] is UniqueConstraint)
				{
					UniqueConstraint uniqueConstraint = (UniqueConstraint)table.Constraints[num5];
					DataColumn[] columnsReference2 = uniqueConstraint.ColumnsReference;
					CodeExpression[] array2 = new CodeExpression[columnsReference2.Length];
					for (int num6 = 0; num6 < columnsReference2.Length; num6++)
					{
						array2[num6] = TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableColumnFieldName(columnsReference2[num6]));
					}
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Constraints"), "Add", TypedDataSetGenerator.New(typeof(UniqueConstraint), new CodeExpression[]
					{
						TypedDataSetGenerator.Str(uniqueConstraint.ConstraintName),
						new CodeArrayCreateExpression(typeof(DataColumn), array2),
						TypedDataSetGenerator.Primitive(uniqueConstraint.IsPrimaryKey)
					})));
				}
			}
			for (int num7 = 0; num7 < table.Columns.Count; num7++)
			{
				DataColumn dataColumn3 = table.Columns[num7];
				string text10 = this.TableColumnFieldName(dataColumn3);
				CodeExpression codeExpression3 = TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text10);
				codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(codeExpression3, TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Columns"), TypedDataSetGenerator.Str(dataColumn3.ColumnName))));
				if (dataColumn3.AutoIncrement)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "AutoIncrement"), TypedDataSetGenerator.Primitive(true)));
				}
				if (dataColumn3.AutoIncrementSeed != 0L)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "AutoIncrementSeed"), TypedDataSetGenerator.Primitive(dataColumn3.AutoIncrementSeed)));
				}
				if (dataColumn3.AutoIncrementStep != 1L)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "AutoIncrementStep"), TypedDataSetGenerator.Primitive(dataColumn3.AutoIncrementStep)));
				}
				if (!dataColumn3.AllowDBNull)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "AllowDBNull"), TypedDataSetGenerator.Primitive(false)));
				}
				if (dataColumn3.ReadOnly)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "ReadOnly"), TypedDataSetGenerator.Primitive(true)));
				}
				if (dataColumn3.Unique)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "Unique"), TypedDataSetGenerator.Primitive(true)));
				}
				if (!ADP.IsEmpty(dataColumn3.Prefix))
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "Prefix"), TypedDataSetGenerator.Str(dataColumn3.Prefix)));
				}
				if (dataColumn3._columnUri != null)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "Namespace"), TypedDataSetGenerator.Str(dataColumn3.Namespace)));
				}
				if (dataColumn3.Caption != dataColumn3.ColumnName)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "Caption"), TypedDataSetGenerator.Str(dataColumn3.Caption)));
				}
				if (dataColumn3.DefaultValue != DBNull.Value)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "DefaultValue"), TypedDataSetGenerator.Primitive(dataColumn3.DefaultValue)));
				}
				if (dataColumn3.MaxLength != -1)
				{
					codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "MaxLength"), TypedDataSetGenerator.Primitive(dataColumn3.MaxLength)));
				}
			}
			if (table.ShouldSerializeCaseSensitive())
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "CaseSensitive"), TypedDataSetGenerator.Primitive(table.CaseSensitive)));
			}
			if (table.ShouldSerializeLocale())
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Locale"), TypedDataSetGenerator.New(typeof(CultureInfo), new CodeExpression[] { TypedDataSetGenerator.Str(table.Locale.ToString()) })));
			}
			if (!ADP.IsEmpty(table.Prefix))
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Prefix"), TypedDataSetGenerator.Str(table.Prefix)));
			}
			if (table._tableNamespace != null)
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Namespace"), TypedDataSetGenerator.Str(table.Namespace)));
			}
			if (table.MinimumCapacity != 50)
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "MinimumCapacity"), TypedDataSetGenerator.Primitive(table.MinimumCapacity)));
			}
			if (table._displayExpression != null)
			{
				codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "DisplayExpression"), TypedDataSetGenerator.Str(table.DisplayExpressionInternal)));
			}
			codeTypeDeclaration.Members.Add(codeMemberMethod8);
			codeTypeDeclaration.Members.Add(codeMemberMethod7);
			CodeMemberMethod codeMemberMethod9 = TypedDataSetGenerator.MethodDecl(text3, "New" + text, (MemberAttributes)24578);
			codeMemberMethod9.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(text3, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "NewRow"))));
			codeTypeDeclaration.Members.Add(codeMemberMethod9);
			CodeMemberMethod codeMemberMethod10 = TypedDataSetGenerator.MethodDecl(typeof(DataRow), "NewRowFromBuilder", (MemberAttributes)12292);
			codeMemberMethod10.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(DataRowBuilder), "builder"));
			codeMemberMethod10.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.New(text3, new CodeExpression[] { TypedDataSetGenerator.Argument("builder") })));
			codeTypeDeclaration.Members.Add(codeMemberMethod10);
			CodeMemberMethod codeMemberMethod11 = TypedDataSetGenerator.MethodDecl(typeof(Type), "GetRowType", (MemberAttributes)12292);
			codeMemberMethod11.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.TypeOf(text3)));
			codeTypeDeclaration.Members.Add(codeMemberMethod11);
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.CreateOnRowEventMethod("Changed", text));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.CreateOnRowEventMethod("Changing", text));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.CreateOnRowEventMethod("Deleted", text));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.CreateOnRowEventMethod("Deleting", text));
			CodeMemberMethod codeMemberMethod12 = TypedDataSetGenerator.MethodDecl(typeof(void), "Remove" + text, (MemberAttributes)24578);
			codeMemberMethod12.Parameters.Add(TypedDataSetGenerator.ParameterDecl(text3, "row"));
			codeMemberMethod12.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Rows"), "Remove", TypedDataSetGenerator.Argument("row")));
			codeTypeDeclaration.Members.Add(codeMemberMethod12);
			return codeTypeDeclaration;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0004B96C File Offset: 0x00049B6C
		private CodeTypeDeclaration CreateTypedRow(DataTable table)
		{
			string text = this.RowClassName(table);
			string text2 = this.TableClassName(table);
			string text3 = this.TableFieldName(table);
			bool flag = false;
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
			codeTypeDeclaration.Name = text;
			string text4 = this.RowBaseClassName(table);
			if (string.Compare(text4, "DataRow", StringComparison.Ordinal) == 0)
			{
				codeTypeDeclaration.BaseTypes.Add(typeof(DataRow));
			}
			else
			{
				codeTypeDeclaration.BaseTypes.Add(text4);
			}
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(text2, text3));
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = (MemberAttributes)4098;
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(DataRowBuilder), "rb"));
			codeConstructor.BaseConstructorArgs.Add(TypedDataSetGenerator.Argument("rb"));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text3), TypedDataSetGenerator.Cast(text2, TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Table"))));
			codeTypeDeclaration.Members.Add(codeConstructor);
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.ColumnMapping != MappingType.Hidden)
				{
					Type dataType = dataColumn.DataType;
					string text5 = this.RowColumnPropertyName(dataColumn);
					string text6 = this.TableColumnPropertyName(dataColumn);
					CodeMemberProperty codeMemberProperty = TypedDataSetGenerator.PropertyDecl(dataType, text5, (MemberAttributes)24578);
					CodeStatement codeStatement = TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(this.GetTypeName(dataType), TypedDataSetGenerator.Indexer(TypedDataSetGenerator.This(), TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text3), text6))));
					if (dataColumn.AllowDBNull)
					{
						string text7 = (string)dataColumn.ExtendedProperties["nullValue"];
						if (text7 == null || text7 == "_throw")
						{
							codeStatement = TypedDataSetGenerator.Try(codeStatement, TypedDataSetGenerator.Catch(typeof(InvalidCastException), "e", TypedDataSetGenerator.Throw(typeof(StrongTypingException), "StrongTyping_CananotAccessDBNull", "e")));
						}
						else
						{
							CodeExpression codeExpression = null;
							CodeExpression codeExpression2;
							if (text7 == "_null")
							{
								if (dataColumn.DataType.IsSubclassOf(typeof(ValueType)))
								{
									this.errorList.Add(Res.GetString("Column '{0}': Type '{1}' cannot be null", new object[]
									{
										dataColumn.ColumnName,
										dataColumn.DataType.Name
									}));
									continue;
								}
								codeExpression2 = TypedDataSetGenerator.Primitive(null);
							}
							else if (text7 == "_empty")
							{
								if (dataColumn.DataType == typeof(string))
								{
									codeExpression2 = TypedDataSetGenerator.Property(TypedDataSetGenerator.TypeExpr(dataColumn.DataType), "Empty");
								}
								else
								{
									codeExpression2 = TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(text), text5 + "_nullValue");
									ConstructorInfo constructor = dataColumn.DataType.GetConstructor(new Type[] { typeof(string) });
									if (constructor == null)
									{
										this.errorList.Add(Res.GetString("Column '{0}': Type '{1}' does not have parameterless constructor", new object[]
										{
											dataColumn.ColumnName,
											dataColumn.DataType.Name
										}));
										continue;
									}
									constructor.Invoke(new object[0]);
									codeExpression = TypedDataSetGenerator.New(dataColumn.DataType, new CodeExpression[0]);
								}
							}
							else
							{
								if (!flag)
								{
									table.NewRow();
									flag = true;
								}
								object obj2 = dataColumn.ConvertXmlToObject(text7);
								if (dataColumn.DataType == typeof(char) || dataColumn.DataType == typeof(string) || dataColumn.DataType == typeof(decimal) || dataColumn.DataType == typeof(bool) || dataColumn.DataType == typeof(float) || dataColumn.DataType == typeof(double) || dataColumn.DataType == typeof(sbyte) || dataColumn.DataType == typeof(byte) || dataColumn.DataType == typeof(short) || dataColumn.DataType == typeof(ushort) || dataColumn.DataType == typeof(int) || dataColumn.DataType == typeof(uint) || dataColumn.DataType == typeof(long) || dataColumn.DataType == typeof(ulong))
								{
									codeExpression2 = TypedDataSetGenerator.Primitive(obj2);
								}
								else
								{
									codeExpression2 = TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(text), text5 + "_nullValue");
									if (dataColumn.DataType == typeof(byte[]))
									{
										codeExpression = TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.TypeExpr(typeof(Convert)), "FromBase64String", TypedDataSetGenerator.Primitive(text7));
									}
									else if (dataColumn.DataType == typeof(DateTime) || dataColumn.DataType == typeof(TimeSpan))
									{
										codeExpression = TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.TypeExpr(dataColumn.DataType), "Parse", TypedDataSetGenerator.Primitive(obj2.ToString()));
									}
									else
									{
										ConstructorInfo constructor2 = dataColumn.DataType.GetConstructor(new Type[] { typeof(string) });
										if (constructor2 == null)
										{
											this.errorList.Add(Res.GetString("Column '{0}': Type '{1}' does not have constructor with string argument", new object[]
											{
												dataColumn.ColumnName,
												dataColumn.DataType.Name
											}));
											continue;
										}
										constructor2.Invoke(new object[] { text7 });
										codeExpression = TypedDataSetGenerator.New(dataColumn.DataType, new CodeExpression[] { TypedDataSetGenerator.Primitive(text7) });
									}
								}
							}
							codeStatement = TypedDataSetGenerator.If(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "Is" + text5 + "Null"), new CodeStatement[] { TypedDataSetGenerator.Return(codeExpression2) }, new CodeStatement[] { codeStatement });
							if (codeExpression != null)
							{
								CodeMemberField codeMemberField = TypedDataSetGenerator.FieldDecl(dataColumn.DataType, text5 + "_nullValue");
								codeMemberField.Attributes = (MemberAttributes)20483;
								codeMemberField.InitExpression = codeExpression;
								codeTypeDeclaration.Members.Add(codeMemberField);
							}
						}
					}
					codeMemberProperty.GetStatements.Add(codeStatement);
					codeMemberProperty.SetStatements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Indexer(TypedDataSetGenerator.This(), TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text3), text6)), TypedDataSetGenerator.Value()));
					codeTypeDeclaration.Members.Add(codeMemberProperty);
					if (dataColumn.AllowDBNull)
					{
						CodeMemberMethod codeMemberMethod = TypedDataSetGenerator.MethodDecl(typeof(bool), "Is" + text5 + "Null", (MemberAttributes)24578);
						codeMemberMethod.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "IsNull", TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text3), text6))));
						codeTypeDeclaration.Members.Add(codeMemberMethod);
						CodeMemberMethod codeMemberMethod2 = TypedDataSetGenerator.MethodDecl(typeof(void), "Set" + text5 + "Null", (MemberAttributes)24578);
						codeMemberMethod2.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Indexer(TypedDataSetGenerator.This(), TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text3), text6)), TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(Convert)), "DBNull")));
						codeTypeDeclaration.Members.Add(codeMemberMethod2);
					}
				}
			}
			DataRelationCollection childRelations = table.ChildRelations;
			for (int i = 0; i < childRelations.Count; i++)
			{
				DataRelation dataRelation = childRelations[i];
				string text8 = this.RowConcreteClassName(dataRelation.ChildTable);
				CodeMemberMethod codeMemberMethod3 = TypedDataSetGenerator.Method(TypedDataSetGenerator.Type(text8, 1), this.ChildPropertyName(dataRelation), (MemberAttributes)24578);
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(TypedDataSetGenerator.Type(text8, 1), TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "GetChildRows", TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Table"), "ChildRelations"), TypedDataSetGenerator.Str(dataRelation.RelationName))))));
				codeTypeDeclaration.Members.Add(codeMemberMethod3);
			}
			DataRelationCollection parentRelations = table.ParentRelations;
			for (int j = 0; j < parentRelations.Count; j++)
			{
				DataRelation dataRelation2 = parentRelations[j];
				string text9 = this.RowClassName(dataRelation2.ParentTable);
				CodeMemberProperty codeMemberProperty2 = TypedDataSetGenerator.PropertyDecl(text9, this.ParentPropertyName(dataRelation2), (MemberAttributes)24578);
				codeMemberProperty2.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Cast(text9, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "GetParentRow", TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Table"), "ParentRelations"), TypedDataSetGenerator.Str(dataRelation2.RelationName))))));
				codeMemberProperty2.SetStatements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "SetParentRow", new CodeExpression[]
				{
					TypedDataSetGenerator.Value(),
					TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Table"), "ParentRelations"), TypedDataSetGenerator.Str(dataRelation2.RelationName))
				}));
				codeTypeDeclaration.Members.Add(codeMemberProperty2);
			}
			return codeTypeDeclaration;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0004C394 File Offset: 0x0004A594
		private CodeTypeDeclaration CreateTypedRowEvent(DataTable table)
		{
			string text = this.RowClassName(table);
			this.TableClassName(table);
			string text2 = this.RowConcreteClassName(table);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration();
			codeTypeDeclaration.Name = text + "ChangeEvent";
			codeTypeDeclaration.BaseTypes.Add(typeof(EventArgs));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(text2, "eventRow"));
			codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(typeof(DataRowAction), "eventAction"));
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = (MemberAttributes)24578;
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(text2, "row"));
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(DataRowAction), "action"));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), "eventRow"), TypedDataSetGenerator.Argument("row")));
			codeConstructor.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), "eventAction"), TypedDataSetGenerator.Argument("action")));
			codeTypeDeclaration.Members.Add(codeConstructor);
			CodeMemberProperty codeMemberProperty = TypedDataSetGenerator.PropertyDecl(text2, "Row", (MemberAttributes)24578);
			codeMemberProperty.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), "eventRow")));
			codeTypeDeclaration.Members.Add(codeMemberProperty);
			codeMemberProperty = TypedDataSetGenerator.PropertyDecl(typeof(DataRowAction), "Action", (MemberAttributes)24578);
			codeMemberProperty.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), "eventAction")));
			codeTypeDeclaration.Members.Add(codeMemberProperty);
			return codeTypeDeclaration;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0004C560 File Offset: 0x0004A760
		private CodeTypeDelegate CreateTypedRowEventHandler(DataTable table)
		{
			string text = this.RowClassName(table);
			CodeTypeDelegate codeTypeDelegate = new CodeTypeDelegate(text + "ChangeEventHandler");
			codeTypeDelegate.TypeAttributes |= TypeAttributes.Public;
			codeTypeDelegate.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(object), "sender"));
			codeTypeDelegate.Parameters.Add(TypedDataSetGenerator.ParameterDecl(text + "ChangeEvent", "e"));
			return codeTypeDelegate;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0004C5D4 File Offset: 0x0004A7D4
		private CodeTypeDeclaration CreateTypedDataSet(DataSet dataSet)
		{
			string text = this.FixIdName(dataSet.DataSetName);
			CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(text);
			codeTypeDeclaration.BaseTypes.Add(typeof(DataSet));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Serializable"));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.ComponentModel.DesignerCategoryAttribute", TypedDataSetGenerator.Str("code")));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.Diagnostics.DebuggerStepThrough"));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.ComponentModel.ToolboxItem", TypedDataSetGenerator.Primitive(true)));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl(typeof(XmlSchemaProviderAttribute).FullName, TypedDataSetGenerator.Primitive("GetTypedDataSetSchema")));
			codeTypeDeclaration.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl(typeof(XmlRootAttribute).FullName, TypedDataSetGenerator.Primitive(text)));
			for (int i = 0; i < dataSet.Tables.Count; i++)
			{
				codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(this.TableClassName(dataSet.Tables[i]), this.TableFieldName(dataSet.Tables[i])));
			}
			for (int j = 0; j < dataSet.Relations.Count; j++)
			{
				codeTypeDeclaration.Members.Add(TypedDataSetGenerator.FieldDecl(typeof(DataRelation), this.RelationFieldName(dataSet.Relations[j])));
			}
			CodeConstructor codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Public;
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "BeginInit"));
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitClass"));
			codeConstructor.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(CollectionChangeEventHandler), "schemaChangedHandler", new CodeDelegateCreateExpression(TypedDataSetGenerator.Type(typeof(CollectionChangeEventHandler)), TypedDataSetGenerator.This(), "SchemaChanged")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Relations"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler")));
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "EndInit"));
			codeTypeDeclaration.Members.Add(codeConstructor);
			codeConstructor = new CodeConstructor();
			codeConstructor.Attributes = MemberAttributes.Family;
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(SerializationInfo), "info"));
			codeConstructor.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(StreamingContext), "context"));
			codeConstructor.BaseConstructorArgs.AddRange(new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("info"),
				TypedDataSetGenerator.Argument("context")
			});
			codeConstructor.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.EQ(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "IsBinarySerialized", new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("info"),
				TypedDataSetGenerator.Argument("context")
			}), TypedDataSetGenerator.Primitive(true)), new CodeStatement[]
			{
				TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars", TypedDataSetGenerator.Primitive(false))),
				TypedDataSetGenerator.VariableDecl(typeof(CollectionChangeEventHandler), "schemaChangedHandler1", new CodeDelegateCreateExpression(TypedDataSetGenerator.Type(typeof(CollectionChangeEventHandler)), TypedDataSetGenerator.This(), "SchemaChanged")),
				new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler1")),
				new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Relations"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler1")),
				TypedDataSetGenerator.Return()
			}));
			codeConstructor.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(string), "strSchema", TypedDataSetGenerator.Cast("System.String", TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Argument("info"), "GetValue", new CodeExpression[]
			{
				TypedDataSetGenerator.Str("XmlSchema"),
				TypedDataSetGenerator.TypeOf("System.String")
			}))));
			ArrayList arrayList = new ArrayList();
			arrayList.Add(TypedDataSetGenerator.VariableDecl(typeof(DataSet), "ds", TypedDataSetGenerator.New(typeof(DataSet), new CodeExpression[0])));
			arrayList.Add(TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Variable("ds"), "ReadXmlSchema", new CodeExpression[] { TypedDataSetGenerator.New(typeof(XmlTextReader), new CodeExpression[] { TypedDataSetGenerator.New("System.IO.StringReader", new CodeExpression[] { TypedDataSetGenerator.Variable("strSchema") }) }) })));
			for (int k = 0; k < dataSet.Tables.Count; k++)
			{
				arrayList.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Tables"), TypedDataSetGenerator.Str(dataSet.Tables[k].TableName)), TypedDataSetGenerator.Primitive(null)), TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "Add", TypedDataSetGenerator.New(this.TableClassName(dataSet.Tables[k]), new CodeExpression[] { TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Tables"), TypedDataSetGenerator.Str(dataSet.Tables[k].TableName)) })))));
			}
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "DataSetName"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "DataSetName")));
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Prefix"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Prefix")));
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Namespace"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Namespace")));
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Locale"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Locale")));
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "CaseSensitive"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "CaseSensitive")));
			arrayList.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "EnforceConstraints"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "EnforceConstraints")));
			arrayList.Add(TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "Merge", new CodeExpression[]
			{
				TypedDataSetGenerator.Variable("ds"),
				TypedDataSetGenerator.Primitive(false),
				TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(MissingSchemaAction)), "Add")
			})));
			arrayList.Add(TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars")));
			CodeStatement[] array = new CodeStatement[arrayList.Count];
			arrayList.CopyTo(array);
			codeConstructor.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Variable("strSchema"), TypedDataSetGenerator.Primitive(null)), array, new CodeStatement[]
			{
				TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "BeginInit")),
				TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitClass")),
				TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "EndInit"))
			}));
			codeConstructor.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "GetSerializationData", new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("info"),
				TypedDataSetGenerator.Argument("context")
			}));
			codeConstructor.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(CollectionChangeEventHandler), "schemaChangedHandler", new CodeDelegateCreateExpression(TypedDataSetGenerator.Type(typeof(CollectionChangeEventHandler)), TypedDataSetGenerator.This(), "SchemaChanged")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler")));
			codeConstructor.Statements.Add(new CodeAttachEventStatement(new CodeEventReferenceExpression(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Relations"), "CollectionChanged"), TypedDataSetGenerator.Variable("schemaChangedHandler")));
			codeTypeDeclaration.Members.Add(codeConstructor);
			CodeMemberMethod codeMemberMethod = TypedDataSetGenerator.MethodDecl(typeof(DataSet), "Clone", (MemberAttributes)24580);
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.VariableDecl(text, "cln", TypedDataSetGenerator.Cast(text, TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Base(), "Clone", new CodeExpression[0]))));
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Variable("cln"), "InitVars", new CodeExpression[0]));
			codeMemberMethod.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Variable("cln")));
			codeTypeDeclaration.Members.Add(codeMemberMethod);
			CodeMemberMethod codeMemberMethod2 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitVars", (MemberAttributes)4098);
			codeMemberMethod2.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars", new CodeExpression[] { TypedDataSetGenerator.Primitive(true) }));
			codeTypeDeclaration.Members.Add(codeMemberMethod2);
			CodeMemberMethod codeMemberMethod3 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitClass", MemberAttributes.Private);
			CodeMemberMethod codeMemberMethod4 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitVars", (MemberAttributes)4098);
			codeMemberMethod4.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(bool), "initTable"));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "DataSetName"), TypedDataSetGenerator.Str(dataSet.DataSetName)));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Prefix"), TypedDataSetGenerator.Str(dataSet.Prefix)));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Namespace"), TypedDataSetGenerator.Str(dataSet.Namespace)));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Locale"), TypedDataSetGenerator.New(typeof(CultureInfo), new CodeExpression[] { TypedDataSetGenerator.Str(dataSet.Locale.ToString()) })));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "CaseSensitive"), TypedDataSetGenerator.Primitive(dataSet.CaseSensitive)));
			codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "EnforceConstraints"), TypedDataSetGenerator.Primitive(dataSet.EnforceConstraints)));
			for (int l = 0; l < dataSet.Tables.Count; l++)
			{
				CodeExpression codeExpression = TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataSet.Tables[l]));
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(codeExpression, TypedDataSetGenerator.New(this.TableClassName(dataSet.Tables[l]), new CodeExpression[0])));
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "Add", codeExpression));
				codeMemberMethod4.Statements.Add(TypedDataSetGenerator.Assign(codeExpression, TypedDataSetGenerator.Cast(this.TableClassName(dataSet.Tables[l]), TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), TypedDataSetGenerator.Str(dataSet.Tables[l].TableName)))));
				codeMemberMethod4.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.EQ(TypedDataSetGenerator.Variable("initTable"), TypedDataSetGenerator.Primitive(true)), new CodeStatement[] { TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(codeExpression, TypedDataSetGenerator.Primitive(null)), TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(codeExpression, "InitVars"))) }));
			}
			CodeMemberMethod codeMemberMethod5 = TypedDataSetGenerator.MethodDecl(typeof(bool), "ShouldSerializeTables", (MemberAttributes)12292);
			codeMemberMethod5.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Primitive(false)));
			codeTypeDeclaration.Members.Add(codeMemberMethod5);
			CodeMemberMethod codeMemberMethod6 = TypedDataSetGenerator.MethodDecl(typeof(bool), "ShouldSerializeRelations", (MemberAttributes)12292);
			codeMemberMethod6.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Primitive(false)));
			codeTypeDeclaration.Members.Add(codeMemberMethod6);
			CodeMemberMethod codeMemberMethod7 = TypedDataSetGenerator.MethodDecl(typeof(XmlSchemaComplexType), "GetTypedDataSetSchema", (MemberAttributes)24579);
			codeMemberMethod7.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(XmlSchemaSet), "xs"));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.VariableDecl(text, "ds", TypedDataSetGenerator.New(text, new CodeExpression[0])));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Argument("xs"), "Add", new CodeExpression[] { TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Variable("ds"), "GetSchemaSerializable", new CodeExpression[0]) }));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(XmlSchemaComplexType), "type", TypedDataSetGenerator.New(typeof(XmlSchemaComplexType), new CodeExpression[0])));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(XmlSchemaSequence), "sequence", TypedDataSetGenerator.New(typeof(XmlSchemaSequence), new CodeExpression[0])));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(XmlSchemaAny), "any", TypedDataSetGenerator.New(typeof(XmlSchemaAny), new CodeExpression[0])));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("any"), "Namespace"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Namespace")));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("sequence"), "Items"), "Add", new CodeExpression[] { TypedDataSetGenerator.Variable("any") }));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("type"), "Particle"), TypedDataSetGenerator.Variable("sequence")));
			codeMemberMethod7.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Variable("type")));
			codeTypeDeclaration.Members.Add(codeMemberMethod7);
			CodeMemberMethod codeMemberMethod8 = TypedDataSetGenerator.MethodDecl(typeof(void), "ReadXmlSerializable", (MemberAttributes)12292);
			codeMemberMethod8.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(XmlReader), "reader"));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "Reset", new CodeExpression[0]));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(DataSet), "ds", TypedDataSetGenerator.New(typeof(DataSet), new CodeExpression[0])));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Variable("ds"), "ReadXml", new CodeExpression[] { TypedDataSetGenerator.Argument("reader") }));
			for (int m = 0; m < dataSet.Tables.Count; m++)
			{
				codeMemberMethod8.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.IdNotEQ(TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Tables"), TypedDataSetGenerator.Str(dataSet.Tables[m].TableName)), TypedDataSetGenerator.Primitive(null)), TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Tables"), "Add", TypedDataSetGenerator.New(this.TableClassName(dataSet.Tables[m]), new CodeExpression[] { TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Tables"), TypedDataSetGenerator.Str(dataSet.Tables[m].TableName)) })))));
			}
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "DataSetName"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "DataSetName")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Prefix"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Prefix")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Namespace"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Namespace")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Locale"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "Locale")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "CaseSensitive"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "CaseSensitive")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "EnforceConstraints"), TypedDataSetGenerator.Property(TypedDataSetGenerator.Variable("ds"), "EnforceConstraints")));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "Merge", new CodeExpression[]
			{
				TypedDataSetGenerator.Variable("ds"),
				TypedDataSetGenerator.Primitive(false),
				TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(MissingSchemaAction)), "Add")
			}));
			codeMemberMethod8.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars"));
			codeTypeDeclaration.Members.Add(codeMemberMethod8);
			CodeMemberMethod codeMemberMethod9 = TypedDataSetGenerator.MethodDecl(typeof(XmlSchema), "GetSchemaSerializable", (MemberAttributes)12292);
			codeMemberMethod9.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(MemoryStream), "stream", TypedDataSetGenerator.New(typeof(MemoryStream), new CodeExpression[0])));
			codeMemberMethod9.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "WriteXmlSchema", TypedDataSetGenerator.New(typeof(XmlTextWriter), new CodeExpression[]
			{
				TypedDataSetGenerator.Argument("stream"),
				TypedDataSetGenerator.Primitive(null)
			})));
			codeMemberMethod9.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("stream"), "Position"), TypedDataSetGenerator.Primitive(0)));
			codeMemberMethod9.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.TypeExpr("System.Xml.Schema.XmlSchema"), "Read", new CodeExpression[]
			{
				TypedDataSetGenerator.New(typeof(XmlTextReader), new CodeExpression[] { TypedDataSetGenerator.Argument("stream") }),
				TypedDataSetGenerator.Primitive(null)
			})));
			codeTypeDeclaration.Members.Add(codeMemberMethod9);
			CodeExpression codeExpression2 = null;
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				foreach (object obj2 in dataTable.Constraints)
				{
					Constraint constraint = (Constraint)obj2;
					if (constraint is ForeignKeyConstraint)
					{
						ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraint;
						CodeArrayCreateExpression codeArrayCreateExpression = new CodeArrayCreateExpression(typeof(DataColumn), 0);
						foreach (DataColumn dataColumn in foreignKeyConstraint.Columns)
						{
							codeArrayCreateExpression.Initializers.Add(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataColumn.Table)), this.TableColumnPropertyName(dataColumn)));
						}
						CodeArrayCreateExpression codeArrayCreateExpression2 = new CodeArrayCreateExpression(typeof(DataColumn), 0);
						foreach (DataColumn dataColumn2 in foreignKeyConstraint.RelatedColumnsReference)
						{
							codeArrayCreateExpression2.Initializers.Add(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataColumn2.Table)), this.TableColumnPropertyName(dataColumn2)));
						}
						if (codeExpression2 == null)
						{
							codeMemberMethod3.Statements.Add(TypedDataSetGenerator.VariableDecl(typeof(ForeignKeyConstraint), "fkc"));
							codeExpression2 = TypedDataSetGenerator.Variable("fkc");
						}
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(codeExpression2, TypedDataSetGenerator.New(typeof(ForeignKeyConstraint), new CodeExpression[]
						{
							TypedDataSetGenerator.Str(foreignKeyConstraint.ConstraintName),
							codeArrayCreateExpression2,
							codeArrayCreateExpression
						})));
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataTable)), "Constraints"), "Add", codeExpression2));
						string text2 = foreignKeyConstraint.AcceptRejectRule.ToString();
						string text3 = foreignKeyConstraint.DeleteRule.ToString();
						string text4 = foreignKeyConstraint.UpdateRule.ToString();
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "AcceptRejectRule"), TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(foreignKeyConstraint.AcceptRejectRule.GetType()), text2)));
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "DeleteRule"), TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(foreignKeyConstraint.DeleteRule.GetType()), text3)));
						codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression2, "UpdateRule"), TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(foreignKeyConstraint.UpdateRule.GetType()), text4)));
					}
				}
			}
			foreach (object obj3 in dataSet.Relations)
			{
				DataRelation dataRelation = (DataRelation)obj3;
				CodeArrayCreateExpression codeArrayCreateExpression3 = new CodeArrayCreateExpression(typeof(DataColumn), 0);
				string text5 = this.TableFieldName(dataRelation.ParentTable);
				foreach (DataColumn dataColumn3 in dataRelation.ParentColumnsReference)
				{
					codeArrayCreateExpression3.Initializers.Add(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text5), this.TableColumnPropertyName(dataColumn3)));
				}
				CodeArrayCreateExpression codeArrayCreateExpression4 = new CodeArrayCreateExpression(typeof(DataColumn), 0);
				string text6 = this.TableFieldName(dataRelation.ChildTable);
				foreach (DataColumn dataColumn4 in dataRelation.ChildColumnsReference)
				{
					codeArrayCreateExpression4.Initializers.Add(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), text6), this.TableColumnPropertyName(dataColumn4)));
				}
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.RelationFieldName(dataRelation)), TypedDataSetGenerator.New(typeof(DataRelation), new CodeExpression[]
				{
					TypedDataSetGenerator.Str(dataRelation.RelationName),
					codeArrayCreateExpression3,
					codeArrayCreateExpression4,
					TypedDataSetGenerator.Primitive(false)
				})));
				if (dataRelation.Nested)
				{
					codeMemberMethod3.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.RelationFieldName(dataRelation)), "Nested"), TypedDataSetGenerator.Primitive(true)));
				}
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Relations"), "Add", TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.RelationFieldName(dataRelation))));
				codeMemberMethod4.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.RelationFieldName(dataRelation)), TypedDataSetGenerator.Indexer(TypedDataSetGenerator.Property(TypedDataSetGenerator.This(), "Relations"), TypedDataSetGenerator.Str(dataRelation.RelationName))));
			}
			codeTypeDeclaration.Members.Add(codeMemberMethod4);
			codeTypeDeclaration.Members.Add(codeMemberMethod3);
			for (int num = 0; num < dataSet.Tables.Count; num++)
			{
				string text7 = this.TablePropertyName(dataSet.Tables[num]);
				CodeMemberProperty codeMemberProperty = TypedDataSetGenerator.PropertyDecl(this.TableClassName(dataSet.Tables[num]), text7, (MemberAttributes)24578);
				codeMemberProperty.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.ComponentModel.Browsable", TypedDataSetGenerator.Primitive(false)));
				codeMemberProperty.CustomAttributes.Add(TypedDataSetGenerator.AttributeDecl("System.ComponentModel.DesignerSerializationVisibilityAttribute", TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(DesignerSerializationVisibility)), "Content")));
				codeMemberProperty.GetStatements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataSet.Tables[num]))));
				codeTypeDeclaration.Members.Add(codeMemberProperty);
				CodeMemberMethod codeMemberMethod10 = TypedDataSetGenerator.MethodDecl(typeof(bool), "ShouldSerialize" + text7, MemberAttributes.Private);
				codeMemberMethod10.Statements.Add(TypedDataSetGenerator.Return(TypedDataSetGenerator.Primitive(false)));
				codeTypeDeclaration.Members.Add(codeMemberMethod10);
			}
			CodeMemberMethod codeMemberMethod11 = TypedDataSetGenerator.MethodDecl(typeof(void), "SchemaChanged", MemberAttributes.Private);
			codeMemberMethod11.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(object), "sender"));
			codeMemberMethod11.Parameters.Add(TypedDataSetGenerator.ParameterDecl(typeof(CollectionChangeEventArgs), "e"));
			codeMemberMethod11.Statements.Add(TypedDataSetGenerator.If(TypedDataSetGenerator.EQ(TypedDataSetGenerator.Property(TypedDataSetGenerator.Argument("e"), "Action"), TypedDataSetGenerator.Field(TypedDataSetGenerator.TypeExpr(typeof(CollectionChangeAction)), "Remove")), TypedDataSetGenerator.Stm(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitVars"))));
			codeTypeDeclaration.Members.Add(codeMemberMethod11);
			bool flag = false;
			CodeMemberMethod codeMemberMethod12 = TypedDataSetGenerator.MethodDecl(typeof(void), "InitExpressions", MemberAttributes.Private);
			foreach (object obj4 in dataSet.Tables)
			{
				DataTable dataTable2 = (DataTable)obj4;
				for (int num2 = 0; num2 < dataTable2.Columns.Count; num2++)
				{
					DataColumn dataColumn5 = dataTable2.Columns[num2];
					CodeExpression codeExpression3 = TypedDataSetGenerator.Property(TypedDataSetGenerator.Field(TypedDataSetGenerator.This(), this.TableFieldName(dataTable2)), this.TableColumnPropertyName(dataColumn5));
					if (dataColumn5.Expression.Length > 0)
					{
						flag = true;
						codeMemberMethod12.Statements.Add(TypedDataSetGenerator.Assign(TypedDataSetGenerator.Property(codeExpression3, "Expression"), TypedDataSetGenerator.Str(dataColumn5.Expression)));
					}
				}
			}
			if (flag)
			{
				codeTypeDeclaration.Members.Add(codeMemberMethod12);
				codeMemberMethod3.Statements.Add(TypedDataSetGenerator.MethodCall(TypedDataSetGenerator.This(), "InitExpressions"));
			}
			return codeTypeDeclaration;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0004E2A4 File Offset: 0x0004C4A4
		private static CodeExpression This()
		{
			return new CodeThisReferenceExpression();
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0004E2AB File Offset: 0x0004C4AB
		private static CodeExpression Base()
		{
			return new CodeBaseReferenceExpression();
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0004E2B2 File Offset: 0x0004C4B2
		private static CodeExpression Value()
		{
			return new CodePropertySetValueReferenceExpression();
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0004E2B9 File Offset: 0x0004C4B9
		private static CodeTypeReference Type(string type)
		{
			return new CodeTypeReference(type);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0004E2C1 File Offset: 0x0004C4C1
		private static CodeTypeReference Type(Type type)
		{
			return new CodeTypeReference(type);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0004E2C9 File Offset: 0x0004C4C9
		private static CodeTypeReference Type(string type, int rank)
		{
			return new CodeTypeReference(type, rank);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0004E2D2 File Offset: 0x0004C4D2
		private static CodeTypeReferenceExpression TypeExpr(Type type)
		{
			return new CodeTypeReferenceExpression(type);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0004E2DA File Offset: 0x0004C4DA
		private static CodeTypeReferenceExpression TypeExpr(string type)
		{
			return new CodeTypeReferenceExpression(type);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0004E2E2 File Offset: 0x0004C4E2
		private static CodeExpression Cast(string type, CodeExpression expr)
		{
			return new CodeCastExpression(type, expr);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0004E2EB File Offset: 0x0004C4EB
		private static CodeExpression Cast(CodeTypeReference type, CodeExpression expr)
		{
			return new CodeCastExpression(type, expr);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0004E2F4 File Offset: 0x0004C4F4
		private static CodeExpression TypeOf(string type)
		{
			return new CodeTypeOfExpression(type);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0004E2FC File Offset: 0x0004C4FC
		private static CodeExpression Field(CodeExpression exp, string field)
		{
			return new CodeFieldReferenceExpression(exp, field);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0004E305 File Offset: 0x0004C505
		private static CodeExpression Property(CodeExpression exp, string property)
		{
			return new CodePropertyReferenceExpression(exp, property);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0004E30E File Offset: 0x0004C50E
		private static CodeExpression Argument(string argument)
		{
			return new CodeArgumentReferenceExpression(argument);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0004E316 File Offset: 0x0004C516
		private static CodeExpression Variable(string variable)
		{
			return new CodeVariableReferenceExpression(variable);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0004E31E File Offset: 0x0004C51E
		private static CodeExpression Event(string eventName)
		{
			return new CodeEventReferenceExpression(TypedDataSetGenerator.This(), eventName);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0004E32B File Offset: 0x0004C52B
		private static CodeExpression New(string type, CodeExpression[] parameters)
		{
			return new CodeObjectCreateExpression(type, parameters);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0004E334 File Offset: 0x0004C534
		private static CodeExpression New(Type type, CodeExpression[] parameters)
		{
			return new CodeObjectCreateExpression(type, parameters);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0004E33D File Offset: 0x0004C53D
		private static CodeExpression Primitive(object primitive)
		{
			return new CodePrimitiveExpression(primitive);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0004E345 File Offset: 0x0004C545
		private static CodeExpression Str(string str)
		{
			return TypedDataSetGenerator.Primitive(str);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0004E34D File Offset: 0x0004C54D
		private static CodeExpression MethodCall(CodeExpression targetObject, string methodName, CodeExpression[] parameters)
		{
			return new CodeMethodInvokeExpression(targetObject, methodName, parameters);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0004E357 File Offset: 0x0004C557
		private static CodeExpression MethodCall(CodeExpression targetObject, string methodName)
		{
			return new CodeMethodInvokeExpression(targetObject, methodName, Array.Empty<CodeExpression>());
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0004E365 File Offset: 0x0004C565
		private static CodeExpression MethodCall(CodeExpression targetObject, string methodName, CodeExpression par)
		{
			return new CodeMethodInvokeExpression(targetObject, methodName, new CodeExpression[] { par });
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0004E378 File Offset: 0x0004C578
		private static CodeExpression DelegateCall(CodeExpression targetObject, CodeExpression par)
		{
			return new CodeDelegateInvokeExpression(targetObject, new CodeExpression[]
			{
				TypedDataSetGenerator.This(),
				par
			});
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0004E392 File Offset: 0x0004C592
		private static CodeExpression Indexer(CodeExpression targetObject, CodeExpression indices)
		{
			return new CodeIndexerExpression(targetObject, new CodeExpression[] { indices });
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0004E3A4 File Offset: 0x0004C5A4
		private static CodeBinaryOperatorExpression BinOperator(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
		{
			return new CodeBinaryOperatorExpression(left, op, right);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0004E3AE File Offset: 0x0004C5AE
		private static CodeBinaryOperatorExpression IdNotEQ(CodeExpression left, CodeExpression right)
		{
			return TypedDataSetGenerator.BinOperator(left, CodeBinaryOperatorType.IdentityInequality, right);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0004E3B8 File Offset: 0x0004C5B8
		private static CodeBinaryOperatorExpression EQ(CodeExpression left, CodeExpression right)
		{
			return TypedDataSetGenerator.BinOperator(left, CodeBinaryOperatorType.ValueEquality, right);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0004E3C2 File Offset: 0x0004C5C2
		private static CodeStatement Stm(CodeExpression expr)
		{
			return new CodeExpressionStatement(expr);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0004E3CA File Offset: 0x0004C5CA
		private static CodeStatement Return(CodeExpression expr)
		{
			return new CodeMethodReturnStatement(expr);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0004E3D2 File Offset: 0x0004C5D2
		private static CodeStatement Return()
		{
			return new CodeMethodReturnStatement();
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0004E3D9 File Offset: 0x0004C5D9
		private static CodeStatement Assign(CodeExpression left, CodeExpression right)
		{
			return new CodeAssignStatement(left, right);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0004E3E2 File Offset: 0x0004C5E2
		private static CodeStatement Throw(Type exception, string arg, string inner)
		{
			return new CodeThrowExceptionStatement(TypedDataSetGenerator.New(exception, new CodeExpression[]
			{
				TypedDataSetGenerator.Str(Res.GetString(arg)),
				TypedDataSetGenerator.Variable(inner)
			}));
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0004E40C File Offset: 0x0004C60C
		private static CodeStatement If(CodeExpression cond, CodeStatement[] trueStms, CodeStatement[] falseStms)
		{
			return new CodeConditionStatement(cond, trueStms, falseStms);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0004E416 File Offset: 0x0004C616
		private static CodeStatement If(CodeExpression cond, CodeStatement[] trueStms)
		{
			return new CodeConditionStatement(cond, trueStms);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0004E41F File Offset: 0x0004C61F
		private static CodeStatement If(CodeExpression cond, CodeStatement trueStm)
		{
			return TypedDataSetGenerator.If(cond, new CodeStatement[] { trueStm });
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0004E431 File Offset: 0x0004C631
		private static CodeMemberField FieldDecl(string type, string name)
		{
			return new CodeMemberField(type, name);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0004E43A File Offset: 0x0004C63A
		private static CodeMemberField FieldDecl(Type type, string name)
		{
			return new CodeMemberField(type, name);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0004E443 File Offset: 0x0004C643
		private static CodeMemberMethod Method(CodeTypeReference type, string name, MemberAttributes attributes)
		{
			return new CodeMemberMethod
			{
				ReturnType = type,
				Name = name,
				Attributes = attributes
			};
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0004E45F File Offset: 0x0004C65F
		private static CodeMemberMethod MethodDecl(Type type, string name, MemberAttributes attributes)
		{
			return TypedDataSetGenerator.Method(TypedDataSetGenerator.Type(type), name, attributes);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0004E46E File Offset: 0x0004C66E
		private static CodeMemberMethod MethodDecl(string type, string name, MemberAttributes attributes)
		{
			return TypedDataSetGenerator.Method(TypedDataSetGenerator.Type(type), name, attributes);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0004E47D File Offset: 0x0004C67D
		private static CodeMemberProperty PropertyDecl(string type, string name, MemberAttributes attributes)
		{
			return new CodeMemberProperty
			{
				Type = TypedDataSetGenerator.Type(type),
				Name = name,
				Attributes = attributes
			};
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0004E49E File Offset: 0x0004C69E
		private static CodeMemberProperty PropertyDecl(Type type, string name, MemberAttributes attributes)
		{
			return new CodeMemberProperty
			{
				Type = TypedDataSetGenerator.Type(type),
				Name = name,
				Attributes = attributes
			};
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0004E4BF File Offset: 0x0004C6BF
		private static CodeStatement VariableDecl(Type type, string name)
		{
			return new CodeVariableDeclarationStatement(type, name);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0004E4C8 File Offset: 0x0004C6C8
		private static CodeStatement VariableDecl(string type, string name, CodeExpression initExpr)
		{
			return new CodeVariableDeclarationStatement(type, name, initExpr);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0004E4D2 File Offset: 0x0004C6D2
		private static CodeStatement VariableDecl(Type type, string name, CodeExpression initExpr)
		{
			return new CodeVariableDeclarationStatement(type, name, initExpr);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0004E4DC File Offset: 0x0004C6DC
		private static CodeMemberEvent EventDecl(string type, string name)
		{
			return new CodeMemberEvent
			{
				Name = name,
				Type = TypedDataSetGenerator.Type(type),
				Attributes = (MemberAttributes)24578
			};
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0004E501 File Offset: 0x0004C701
		private static CodeParameterDeclarationExpression ParameterDecl(string type, string name)
		{
			return new CodeParameterDeclarationExpression(type, name);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0004E50A File Offset: 0x0004C70A
		private static CodeParameterDeclarationExpression ParameterDecl(Type type, string name)
		{
			return new CodeParameterDeclarationExpression(type, name);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0004E513 File Offset: 0x0004C713
		private static CodeAttributeDeclaration AttributeDecl(string name)
		{
			return new CodeAttributeDeclaration(name);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0004E51B File Offset: 0x0004C71B
		private static CodeAttributeDeclaration AttributeDecl(string name, CodeExpression value)
		{
			return new CodeAttributeDeclaration(name, new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(value)
			});
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0004E532 File Offset: 0x0004C732
		private static CodeStatement Try(CodeStatement tryStmnt, CodeCatchClause catchClause)
		{
			return new CodeTryCatchFinallyStatement(new CodeStatement[] { tryStmnt }, new CodeCatchClause[] { catchClause });
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0004E54D File Offset: 0x0004C74D
		private static CodeCatchClause Catch(Type type, string name, CodeStatement catchStmnt)
		{
			return new CodeCatchClause
			{
				CatchExceptionType = TypedDataSetGenerator.Type(type),
				LocalName = name,
				Statements = { catchStmnt }
			};
		}

		// Token: 0x0400098B RID: 2443
		private bool useExtendedNaming;

		// Token: 0x0400098C RID: 2444
		private ICodeGenerator codeGen;

		// Token: 0x0400098D RID: 2445
		private ArrayList errorList;

		// Token: 0x0400098E RID: 2446
		private ArrayList conflictingTables;

		// Token: 0x0400098F RID: 2447
		private Hashtable lookupIdentifiers;
	}
}

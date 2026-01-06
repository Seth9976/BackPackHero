using System;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000D2 RID: 210
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MemberFilter : Attribute, ICloneable
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x0000DE58 File Offset: 0x0000C058
		public MemberFilter()
		{
			this.Fields = false;
			this.Properties = false;
			this.Methods = false;
			this.Constructors = false;
			this.Gettable = false;
			this.Settable = false;
			this.Inherited = true;
			this.Targeted = true;
			this.NonTargeted = true;
			this.Public = true;
			this.NonPublic = false;
			this.ReadOnly = true;
			this.WriteOnly = true;
			this.Extensions = true;
			this.Operators = true;
			this.Conversions = true;
			this.Parameters = true;
			this.Obsolete = false;
			this.OpenConstructedGeneric = false;
			this.TypeInitializers = true;
			this.ClsNonCompliant = true;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000DEFE File Offset: 0x0000C0FE
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0000DF06 File Offset: 0x0000C106
		public bool Fields { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000DF0F File Offset: 0x0000C10F
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0000DF17 File Offset: 0x0000C117
		public bool Properties { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000DF20 File Offset: 0x0000C120
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0000DF28 File Offset: 0x0000C128
		public bool Methods { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000DF31 File Offset: 0x0000C131
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0000DF39 File Offset: 0x0000C139
		public bool Constructors { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000DF42 File Offset: 0x0000C142
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x0000DF4A File Offset: 0x0000C14A
		public bool Gettable { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000DF53 File Offset: 0x0000C153
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0000DF5B File Offset: 0x0000C15B
		public bool Settable { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000DF64 File Offset: 0x0000C164
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x0000DF6C File Offset: 0x0000C16C
		public bool Inherited { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000DF75 File Offset: 0x0000C175
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0000DF7D File Offset: 0x0000C17D
		public bool Targeted { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000DF86 File Offset: 0x0000C186
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0000DF8E File Offset: 0x0000C18E
		public bool NonTargeted { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000DF97 File Offset: 0x0000C197
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0000DF9F File Offset: 0x0000C19F
		public bool Public { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0000DFB0 File Offset: 0x0000C1B0
		public bool NonPublic { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000DFB9 File Offset: 0x0000C1B9
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0000DFC1 File Offset: 0x0000C1C1
		public bool ReadOnly { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000DFCA File Offset: 0x0000C1CA
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0000DFD2 File Offset: 0x0000C1D2
		public bool WriteOnly { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000DFDB File Offset: 0x0000C1DB
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x0000DFE3 File Offset: 0x0000C1E3
		public bool Extensions { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000DFEC File Offset: 0x0000C1EC
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
		public bool Operators { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0000DFFD File Offset: 0x0000C1FD
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0000E005 File Offset: 0x0000C205
		public bool Conversions { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000E00E File Offset: 0x0000C20E
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0000E016 File Offset: 0x0000C216
		public bool Setters { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000E01F File Offset: 0x0000C21F
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0000E027 File Offset: 0x0000C227
		public bool Parameters { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000E030 File Offset: 0x0000C230
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0000E038 File Offset: 0x0000C238
		public bool Obsolete { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000E041 File Offset: 0x0000C241
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0000E049 File Offset: 0x0000C249
		public bool OpenConstructedGeneric { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000E052 File Offset: 0x0000C252
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0000E05A File Offset: 0x0000C25A
		public bool TypeInitializers { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000E063 File Offset: 0x0000C263
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0000E06B File Offset: 0x0000C26B
		public bool ClsNonCompliant { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000E074 File Offset: 0x0000C274
		public BindingFlags validBindingFlags
		{
			get
			{
				BindingFlags bindingFlags = BindingFlags.Default;
				if (this.Public)
				{
					bindingFlags |= BindingFlags.Public;
				}
				if (this.NonPublic)
				{
					bindingFlags |= BindingFlags.NonPublic;
				}
				if (this.Targeted || this.Constructors)
				{
					bindingFlags |= BindingFlags.Instance;
				}
				if (this.NonTargeted)
				{
					bindingFlags |= BindingFlags.Static;
				}
				if (!this.Inherited)
				{
					bindingFlags |= BindingFlags.DeclaredOnly;
				}
				if (this.NonTargeted && this.Inherited)
				{
					bindingFlags |= BindingFlags.FlattenHierarchy;
				}
				return bindingFlags;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		public MemberTypes validMemberTypes
		{
			get
			{
				MemberTypes memberTypes = (MemberTypes)0;
				if (this.Fields || this.Gettable || this.Settable)
				{
					memberTypes |= MemberTypes.Field;
				}
				if (this.Properties || this.Gettable || this.Settable)
				{
					memberTypes |= MemberTypes.Property;
				}
				if (this.Methods || this.Gettable)
				{
					memberTypes |= MemberTypes.Method;
				}
				if (this.Constructors || this.Gettable)
				{
					memberTypes |= MemberTypes.Constructor;
				}
				return memberTypes;
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000E151 File Offset: 0x0000C351
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000E15C File Offset: 0x0000C35C
		public MemberFilter Clone()
		{
			return new MemberFilter
			{
				Fields = this.Fields,
				Properties = this.Properties,
				Methods = this.Methods,
				Constructors = this.Constructors,
				Gettable = this.Gettable,
				Settable = this.Settable,
				Inherited = this.Inherited,
				Targeted = this.Targeted,
				NonTargeted = this.NonTargeted,
				Public = this.Public,
				NonPublic = this.NonPublic,
				ReadOnly = this.ReadOnly,
				WriteOnly = this.WriteOnly,
				Extensions = this.Extensions,
				Operators = this.Operators,
				Conversions = this.Conversions,
				Parameters = this.Parameters,
				Obsolete = this.Obsolete,
				OpenConstructedGeneric = this.OpenConstructedGeneric,
				TypeInitializers = this.TypeInitializers,
				ClsNonCompliant = this.ClsNonCompliant
			};
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000E26C File Offset: 0x0000C46C
		public override bool Equals(object obj)
		{
			MemberFilter memberFilter = obj as MemberFilter;
			return memberFilter != null && (this.Fields == memberFilter.Fields && this.Properties == memberFilter.Properties && this.Methods == memberFilter.Methods && this.Constructors == memberFilter.Constructors && this.Gettable == memberFilter.Gettable && this.Settable == memberFilter.Settable && this.Inherited == memberFilter.Inherited && this.Targeted == memberFilter.Targeted && this.NonTargeted == memberFilter.NonTargeted && this.Public == memberFilter.Public && this.NonPublic == memberFilter.NonPublic && this.ReadOnly == memberFilter.ReadOnly && this.WriteOnly == memberFilter.WriteOnly && this.Extensions == memberFilter.Extensions && this.Operators == memberFilter.Operators && this.Conversions == memberFilter.Conversions && this.Parameters == memberFilter.Parameters && this.Obsolete == memberFilter.Obsolete && this.OpenConstructedGeneric == memberFilter.OpenConstructedGeneric && this.TypeInitializers == memberFilter.TypeInitializers) && this.ClsNonCompliant == memberFilter.ClsNonCompliant;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		public override int GetHashCode()
		{
			return ((((((((((((((((((((17 * 23 + this.Fields.GetHashCode()) * 23 + this.Properties.GetHashCode()) * 23 + this.Methods.GetHashCode()) * 23 + this.Constructors.GetHashCode()) * 23 + this.Gettable.GetHashCode()) * 23 + this.Settable.GetHashCode()) * 23 + this.Inherited.GetHashCode()) * 23 + this.Targeted.GetHashCode()) * 23 + this.NonTargeted.GetHashCode()) * 23 + this.Public.GetHashCode()) * 23 + this.NonPublic.GetHashCode()) * 23 + this.ReadOnly.GetHashCode()) * 23 + this.WriteOnly.GetHashCode()) * 23 + this.Extensions.GetHashCode()) * 23 + this.Operators.GetHashCode()) * 23 + this.Conversions.GetHashCode()) * 23 + this.Parameters.GetHashCode()) * 23 + this.Obsolete.GetHashCode()) * 23 + this.OpenConstructedGeneric.GetHashCode()) * 23 + this.TypeInitializers.GetHashCode()) * 23 + this.ClsNonCompliant.GetHashCode();
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000E55C File Offset: 0x0000C75C
		public bool ValidateMember(MemberInfo member, TypeFilter typeFilter = null)
		{
			if (member is FieldInfo)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				bool flag = true;
				bool flag2 = !fieldInfo.IsLiteral && !fieldInfo.IsInitOnly;
				if (!this.Fields && (!this.Gettable || !flag) && (!this.Settable || !flag2))
				{
					return false;
				}
				bool flag3 = !fieldInfo.IsStatic;
				if (!this.Targeted && flag3)
				{
					return false;
				}
				if (!this.NonTargeted && !flag3)
				{
					return false;
				}
				if (!this.WriteOnly && !flag)
				{
					return false;
				}
				if (!this.ReadOnly && !flag2)
				{
					return false;
				}
				if (!this.Public && fieldInfo.IsPublic)
				{
					return false;
				}
				if (!this.NonPublic && !fieldInfo.IsPublic)
				{
					return false;
				}
				if (typeFilter != null && !typeFilter.ValidateType(fieldInfo.FieldType))
				{
					return false;
				}
				if (fieldInfo.IsSpecialName)
				{
					return false;
				}
			}
			else if (member is PropertyInfo)
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				MethodInfo getMethod = propertyInfo.GetGetMethod(true);
				MethodInfo setMethod = propertyInfo.GetSetMethod(true);
				bool canRead = propertyInfo.CanRead;
				bool canWrite = propertyInfo.CanWrite;
				if (!this.Properties && (!this.Gettable || !canRead) && (!this.Settable || !canWrite))
				{
					return false;
				}
				bool flag4 = !this.WriteOnly || (!this.Properties && this.Gettable);
				bool flag5 = !this.ReadOnly || (!this.Properties && this.Settable);
				bool flag6 = propertyInfo.CanRead && (this.NonPublic || getMethod.IsPublic);
				bool flag7 = propertyInfo.CanWrite && (this.NonPublic || setMethod.IsPublic);
				if (flag4 && !flag6)
				{
					return false;
				}
				if (flag5 && !flag7)
				{
					return false;
				}
				bool flag8 = !(getMethod ?? setMethod).IsStatic;
				if (!this.Targeted && flag8)
				{
					return false;
				}
				if (!this.NonTargeted && !flag8)
				{
					return false;
				}
				if (typeFilter != null && !typeFilter.ValidateType(propertyInfo.PropertyType))
				{
					return false;
				}
				if (propertyInfo.IsSpecialName)
				{
					return false;
				}
				if (propertyInfo.GetIndexParameters().Any<ParameterInfo>())
				{
					return false;
				}
			}
			else if (member is MethodBase)
			{
				MethodBase methodBase = (MethodBase)member;
				bool flag9 = methodBase.IsExtensionMethod();
				bool flag10 = !methodBase.IsStatic || flag9;
				if (!this.Public && methodBase.IsPublic)
				{
					return false;
				}
				if (!this.NonPublic && !methodBase.IsPublic)
				{
					return false;
				}
				if (!this.Parameters && methodBase.GetParameters().Length > (flag9 ? 1 : 0))
				{
					return false;
				}
				if (!this.OpenConstructedGeneric && methodBase.ContainsGenericParameters)
				{
					return false;
				}
				if (member is MethodInfo)
				{
					MethodInfo methodInfo = (MethodInfo)member;
					bool flag11 = methodInfo.IsOperator();
					bool flag12 = methodInfo.IsUserDefinedConversion();
					bool flag13 = methodInfo.ReturnType != typeof(void);
					bool flag14 = false;
					if (!this.Methods && (!this.Gettable || !flag13) && (!this.Settable || !flag14))
					{
						return false;
					}
					if (!this.Targeted && flag10)
					{
						return false;
					}
					if (!this.NonTargeted && !flag10)
					{
						return false;
					}
					if (!this.Operators && flag11)
					{
						return false;
					}
					if (!this.Extensions && flag9)
					{
						return false;
					}
					if (typeFilter != null && !typeFilter.ValidateType(methodInfo.ReturnType))
					{
						return false;
					}
					if (methodInfo.IsSpecialName && (!flag11 && !flag12))
					{
						return false;
					}
					if (flag13 && methodInfo.ReturnType.ToString().Contains("ReadOnlySpan"))
					{
						return false;
					}
				}
				else if (member is ConstructorInfo)
				{
					ConstructorInfo constructorInfo = (ConstructorInfo)member;
					bool flag15 = true;
					bool flag16 = false;
					if (!this.Constructors && (!this.Gettable || !flag15) && (!this.Settable || !flag16))
					{
						return false;
					}
					if (typeFilter != null && !typeFilter.ValidateType(constructorInfo.DeclaringType))
					{
						return false;
					}
					if (constructorInfo.IsStatic && !this.TypeInitializers)
					{
						return false;
					}
					if (typeof(Component).IsAssignableFrom(member.DeclaringType) || typeof(ScriptableObject).IsAssignableFrom(member.DeclaringType))
					{
						return false;
					}
				}
			}
			if (!this.Obsolete && member.HasAttribute(false))
			{
				return false;
			}
			if (!this.ClsNonCompliant)
			{
				CLSCompliantAttribute attribute = member.GetAttribute(true);
				if (attribute != null && !attribute.IsCompliant)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Fields: {0}", this.Fields));
			stringBuilder.AppendLine(string.Format("Properties: {0}", this.Properties));
			stringBuilder.AppendLine(string.Format("Methods: {0}", this.Methods));
			stringBuilder.AppendLine(string.Format("Constructors: {0}", this.Constructors));
			stringBuilder.AppendLine(string.Format("Gettable: {0}", this.Gettable));
			stringBuilder.AppendLine(string.Format("Settable: {0}", this.Settable));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(string.Format("Inherited: {0}", this.Inherited));
			stringBuilder.AppendLine(string.Format("Instance: {0}", this.Targeted));
			stringBuilder.AppendLine(string.Format("Static: {0}", this.NonTargeted));
			stringBuilder.AppendLine(string.Format("Public: {0}", this.Public));
			stringBuilder.AppendLine(string.Format("NonPublic: {0}", this.NonPublic));
			stringBuilder.AppendLine(string.Format("ReadOnly: {0}", this.ReadOnly));
			stringBuilder.AppendLine(string.Format("WriteOnly: {0}", this.WriteOnly));
			stringBuilder.AppendLine(string.Format("Extensions: {0}", this.Extensions));
			stringBuilder.AppendLine(string.Format("Operators: {0}", this.Operators));
			stringBuilder.AppendLine(string.Format("Conversions: {0}", this.Conversions));
			stringBuilder.AppendLine(string.Format("Parameters: {0}", this.Parameters));
			stringBuilder.AppendLine(string.Format("Obsolete: {0}", this.Obsolete));
			stringBuilder.AppendLine(string.Format("OpenConstructedGeneric: {0}", this.OpenConstructedGeneric));
			stringBuilder.AppendLine(string.Format("TypeInitializers: {0}", this.TypeInitializers));
			stringBuilder.AppendLine(string.Format("ClsNonCompliant: {0}", this.ClsNonCompliant));
			return stringBuilder.ToString();
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000EC1E File Offset: 0x0000CE1E
		public static MemberFilter Any
		{
			get
			{
				return new MemberFilter
				{
					Fields = true,
					Properties = true,
					Methods = true,
					Constructors = true
				};
			}
		}
	}
}

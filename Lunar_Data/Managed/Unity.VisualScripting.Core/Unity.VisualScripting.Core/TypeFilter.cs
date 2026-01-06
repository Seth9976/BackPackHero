using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Unity.VisualScripting
{
	// Token: 0x02000126 RID: 294
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class TypeFilter : Attribute, ICloneable
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x00023088 File Offset: 0x00021288
		public TypeFilter(TypesMatching matching, IEnumerable<Type> types)
		{
			Ensure.That("types").IsNotNull<IEnumerable<Type>>(types);
			this.Matching = matching;
			this.types = new HashSet<Type>(types);
			this.Value = true;
			this.Reference = true;
			this.Classes = true;
			this.Interfaces = true;
			this.Structs = true;
			this.Enums = true;
			this.Public = true;
			this.NonPublic = false;
			this.Abstract = true;
			this.Generic = true;
			this.OpenConstructedGeneric = false;
			this.Static = true;
			this.Sealed = true;
			this.Nested = true;
			this.Primitives = true;
			this.Object = true;
			this.NonSerializable = true;
			this.Obsolete = false;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0002313C File Offset: 0x0002133C
		public TypeFilter(TypesMatching matching, params Type[] types)
			: this(matching, types)
		{
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00023146 File Offset: 0x00021346
		public TypeFilter(IEnumerable<Type> types)
			: this(TypesMatching.ConvertibleToAny, types)
		{
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00023150 File Offset: 0x00021350
		public TypeFilter(params Type[] types)
			: this(TypesMatching.ConvertibleToAny, types)
		{
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0002315A File Offset: 0x0002135A
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x00023162 File Offset: 0x00021362
		public TypesMatching Matching { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0002316B File Offset: 0x0002136B
		public HashSet<Type> Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00023173 File Offset: 0x00021373
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x0002317B File Offset: 0x0002137B
		public bool Value { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00023184 File Offset: 0x00021384
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x0002318C File Offset: 0x0002138C
		public bool Reference { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00023195 File Offset: 0x00021395
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x0002319D File Offset: 0x0002139D
		public bool Classes { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x000231A6 File Offset: 0x000213A6
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x000231AE File Offset: 0x000213AE
		public bool Interfaces { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x000231B7 File Offset: 0x000213B7
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x000231BF File Offset: 0x000213BF
		public bool Structs { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x000231C8 File Offset: 0x000213C8
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x000231D0 File Offset: 0x000213D0
		public bool Enums { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x000231D9 File Offset: 0x000213D9
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x000231E1 File Offset: 0x000213E1
		public bool Public { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x000231EA File Offset: 0x000213EA
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x000231F2 File Offset: 0x000213F2
		public bool NonPublic { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x000231FB File Offset: 0x000213FB
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x00023203 File Offset: 0x00021403
		public bool Abstract { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0002320C File Offset: 0x0002140C
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x00023214 File Offset: 0x00021414
		public bool Generic { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0002321D File Offset: 0x0002141D
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x00023225 File Offset: 0x00021425
		public bool OpenConstructedGeneric { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0002322E File Offset: 0x0002142E
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00023236 File Offset: 0x00021436
		public bool Static { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0002323F File Offset: 0x0002143F
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00023247 File Offset: 0x00021447
		public bool Sealed { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00023250 File Offset: 0x00021450
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00023258 File Offset: 0x00021458
		public bool Nested { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00023261 File Offset: 0x00021461
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00023269 File Offset: 0x00021469
		public bool Primitives { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00023272 File Offset: 0x00021472
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0002327A File Offset: 0x0002147A
		public bool Object { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00023283 File Offset: 0x00021483
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x0002328B File Offset: 0x0002148B
		public bool NonSerializable { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00023294 File Offset: 0x00021494
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x0002329C File Offset: 0x0002149C
		public bool Obsolete { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000232A5 File Offset: 0x000214A5
		public bool ExpectsBoolean
		{
			get
			{
				return this.Types.Count == 1 && this.Types.Single<Type>() == typeof(bool);
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000232D1 File Offset: 0x000214D1
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000232DC File Offset: 0x000214DC
		public TypeFilter Clone()
		{
			return new TypeFilter(this.Matching, this.Types.ToArray<Type>())
			{
				Value = this.Value,
				Reference = this.Reference,
				Classes = this.Classes,
				Interfaces = this.Interfaces,
				Structs = this.Structs,
				Enums = this.Enums,
				Public = this.Public,
				NonPublic = this.NonPublic,
				Abstract = this.Abstract,
				Generic = this.Generic,
				OpenConstructedGeneric = this.OpenConstructedGeneric,
				Static = this.Static,
				Sealed = this.Sealed,
				Nested = this.Nested,
				Primitives = this.Primitives,
				Object = this.Object,
				NonSerializable = this.NonSerializable,
				Obsolete = this.Obsolete
			};
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000233D8 File Offset: 0x000215D8
		public override bool Equals(object obj)
		{
			TypeFilter typeFilter = obj as TypeFilter;
			return typeFilter != null && (this.Matching == typeFilter.Matching && this.types.SetEquals(typeFilter.types) && this.Value == typeFilter.Value && this.Reference == typeFilter.Reference && this.Classes == typeFilter.Classes && this.Interfaces == typeFilter.Interfaces && this.Structs == typeFilter.Structs && this.Enums == typeFilter.Enums && this.Public == typeFilter.Public && this.NonPublic == typeFilter.NonPublic && this.Abstract == typeFilter.Abstract && this.Generic == typeFilter.Generic && this.OpenConstructedGeneric == typeFilter.OpenConstructedGeneric && this.Static == typeFilter.Static && this.Sealed == typeFilter.Sealed && this.Nested == typeFilter.Nested && this.Primitives == typeFilter.Primitives && this.Object == typeFilter.Object && this.NonSerializable == typeFilter.NonSerializable) && this.Obsolete == typeFilter.Obsolete;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00023530 File Offset: 0x00021730
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + this.Matching.GetHashCode();
			foreach (Type type in this.types)
			{
				if (type != null)
				{
					num = num * 23 + type.GetHashCode();
				}
			}
			num = num * 23 + this.Value.GetHashCode();
			num = num * 23 + this.Reference.GetHashCode();
			num = num * 23 + this.Classes.GetHashCode();
			num = num * 23 + this.Interfaces.GetHashCode();
			num = num * 23 + this.Structs.GetHashCode();
			num = num * 23 + this.Enums.GetHashCode();
			num = num * 23 + this.Public.GetHashCode();
			num = num * 23 + this.NonPublic.GetHashCode();
			num = num * 23 + this.Abstract.GetHashCode();
			num = num * 23 + this.Generic.GetHashCode();
			num = num * 23 + this.OpenConstructedGeneric.GetHashCode();
			num = num * 23 + this.Static.GetHashCode();
			num = num * 23 + this.Sealed.GetHashCode();
			num = num * 23 + this.Nested.GetHashCode();
			num = num * 23 + this.Primitives.GetHashCode();
			num = num * 23 + this.Object.GetHashCode();
			num = num * 23 + this.NonSerializable.GetHashCode();
			num = num * 23 + this.Obsolete.GetHashCode();
			return num;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0002372C File Offset: 0x0002192C
		public bool ValidateType(Type type)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			if (!this.Generic && type.IsGenericType)
			{
				return false;
			}
			if (!this.OpenConstructedGeneric && type.ContainsGenericParameters)
			{
				return false;
			}
			if (!this.Value && type.IsValueType)
			{
				return false;
			}
			if (!this.Reference && !type.IsValueType)
			{
				return false;
			}
			if (!this.Classes && type.IsClass)
			{
				return false;
			}
			if (!this.Interfaces && type.IsInterface)
			{
				return false;
			}
			if (!this.Structs && type.IsValueType && !type.IsEnum && !type.IsPrimitive)
			{
				return false;
			}
			if (!this.Enums && type.IsEnum)
			{
				return false;
			}
			if (!this.Public && type.IsVisible)
			{
				return false;
			}
			if (!this.NonPublic && !type.IsVisible)
			{
				return false;
			}
			if (!this.Abstract && type.IsAbstract())
			{
				return false;
			}
			if (!this.Static && type.IsStatic())
			{
				return false;
			}
			if (!this.Sealed && type.IsSealed)
			{
				return false;
			}
			if (!this.Nested && type.IsNested)
			{
				return false;
			}
			if (!this.Primitives && type.IsPrimitive)
			{
				return false;
			}
			if (!this.Object && type == typeof(object))
			{
				return false;
			}
			if (!this.NonSerializable && !type.IsSerializable)
			{
				return false;
			}
			if (type.IsSpecialName || type.HasAttribute(true))
			{
				return false;
			}
			if (!this.Obsolete && type.HasAttribute(true))
			{
				return false;
			}
			bool flag = true;
			if (this.Types.Count > 0)
			{
				flag = this.Matching == TypesMatching.AssignableToAll;
				foreach (Type type2 in this.Types)
				{
					if (this.Matching == TypesMatching.Any)
					{
						if (type == type2)
						{
							flag = true;
							break;
						}
					}
					else if (this.Matching == TypesMatching.ConvertibleToAny)
					{
						if (type.IsConvertibleTo(type2, true))
						{
							flag = true;
							break;
						}
					}
					else
					{
						if (this.Matching != TypesMatching.AssignableToAll)
						{
							throw new UnexpectedEnumValueException<TypesMatching>(this.Matching);
						}
						flag &= type2.IsAssignableFrom(type);
						if (!flag)
						{
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00023968 File Offset: 0x00021B68
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Matching: {0}", this.Matching));
			stringBuilder.AppendLine("Types: " + this.types.ToCommaSeparatedString());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(string.Format("Value: {0}", this.Value));
			stringBuilder.AppendLine(string.Format("Reference: {0}", this.Reference));
			stringBuilder.AppendLine(string.Format("Classes: {0}", this.Classes));
			stringBuilder.AppendLine(string.Format("Interfaces: {0}", this.Interfaces));
			stringBuilder.AppendLine(string.Format("Structs: {0}", this.Structs));
			stringBuilder.AppendLine(string.Format("Enums: {0}", this.Enums));
			stringBuilder.AppendLine(string.Format("Public: {0}", this.Public));
			stringBuilder.AppendLine(string.Format("NonPublic: {0}", this.NonPublic));
			stringBuilder.AppendLine(string.Format("Abstract: {0}", this.Abstract));
			stringBuilder.AppendLine(string.Format("Generic: {0}", this.Generic));
			stringBuilder.AppendLine(string.Format("OpenConstructedGeneric: {0}", this.OpenConstructedGeneric));
			stringBuilder.AppendLine(string.Format("Static: {0}", this.Static));
			stringBuilder.AppendLine(string.Format("Sealed: {0}", this.Sealed));
			stringBuilder.AppendLine(string.Format("Nested: {0}", this.Nested));
			stringBuilder.AppendLine(string.Format("Primitives: {0}", this.Primitives));
			stringBuilder.AppendLine(string.Format("Object: {0}", this.Object));
			stringBuilder.AppendLine(string.Format("NonSerializable: {0}", this.NonSerializable));
			stringBuilder.AppendLine(string.Format("Obsolete: {0}", this.Obsolete));
			return stringBuilder.ToString();
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00023BB6 File Offset: 0x00021DB6
		public static TypeFilter Any
		{
			get
			{
				return new TypeFilter(Array.Empty<Type>());
			}
		}

		// Token: 0x040001D5 RID: 469
		private readonly HashSet<Type> types;
	}
}

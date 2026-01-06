using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000D1 RID: 209
	[SerializationVersion("A", new Type[] { })]
	public sealed class Member : ISerializationCallbackReceiver
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		[Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public Member()
		{
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		public Member(Type targetType, string name, Type[] parameterTypes = null)
		{
			Ensure.That("targetType").IsNotNull<Type>(targetType);
			Ensure.That("name").IsNotNull(name);
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentNullException("parameterTypes" + string.Format("[{0}]", i));
					}
				}
			}
			this.targetType = targetType;
			this.name = name;
			this.parameterTypes = parameterTypes;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000C644 File Offset: 0x0000A844
		public Member(Type targetType, FieldInfo fieldInfo)
		{
			Ensure.That("targetType").IsNotNull<Type>(targetType);
			Ensure.That("fieldInfo").IsNotNull<FieldInfo>(fieldInfo);
			this.source = Member.Source.Field;
			this.fieldInfo = fieldInfo;
			this.targetType = targetType;
			this.name = fieldInfo.Name;
			this.parameterTypes = null;
			this.isReflected = true;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		public Member(Type targetType, PropertyInfo propertyInfo)
		{
			Ensure.That("targetType").IsNotNull<Type>(targetType);
			Ensure.That("propertyInfo").IsNotNull<PropertyInfo>(propertyInfo);
			this.source = Member.Source.Property;
			this.propertyInfo = propertyInfo;
			this.targetType = targetType;
			this.name = propertyInfo.Name;
			this.parameterTypes = null;
			this.isReflected = true;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000C70C File Offset: 0x0000A90C
		public Member(Type targetType, MethodInfo methodInfo)
		{
			Ensure.That("targetType").IsNotNull<Type>(targetType);
			Ensure.That("methodInfo").IsNotNull<MethodInfo>(methodInfo);
			this.source = Member.Source.Method;
			this.methodInfo = methodInfo;
			this.targetType = targetType;
			this.name = methodInfo.Name;
			this.isExtension = methodInfo.IsExtension();
			this.isInvokedAsExtension = methodInfo.IsInvokedAsExtension(targetType);
			this.parameterTypes = (from pi in methodInfo.GetInvocationParameters(this._isInvokedAsExtension)
				select pi.ParameterType).ToArray<Type>();
			this.isReflected = true;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000C7BC File Offset: 0x0000A9BC
		public Member(Type targetType, ConstructorInfo constructorInfo)
		{
			Ensure.That("targetType").IsNotNull<Type>(targetType);
			Ensure.That("constructorInfo").IsNotNull<ConstructorInfo>(constructorInfo);
			this.source = Member.Source.Constructor;
			this.constructorInfo = constructorInfo;
			this.targetType = targetType;
			this.name = constructorInfo.Name;
			this.parameterTypes = (from pi in constructorInfo.GetParameters()
				select pi.ParameterType).ToArray<Type>();
			this.isReflected = true;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000C84C File Offset: 0x0000AA4C
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0000C854 File Offset: 0x0000AA54
		[DoNotSerialize]
		public Type targetType
		{
			get
			{
				return this._targetType;
			}
			private set
			{
				if (value == this.targetType)
				{
					return;
				}
				this.isReflected = false;
				this._targetType = value;
				if (value == null)
				{
					this._targetTypeName = null;
					return;
				}
				this._targetTypeName = RuntimeCodebase.SerializeType(value);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000C890 File Offset: 0x0000AA90
		[DoNotSerialize]
		public string targetTypeName
		{
			get
			{
				return this._targetTypeName;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000C898 File Offset: 0x0000AA98
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
		[DoNotSerialize]
		public string name
		{
			get
			{
				return this._name;
			}
			private set
			{
				if (value != this.name)
				{
					this.isReflected = false;
				}
				this._name = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000C8BE File Offset: 0x0000AABE
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0000C8C6 File Offset: 0x0000AAC6
		[DoNotSerialize]
		public bool isReflected { get; private set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000C8CF File Offset: 0x0000AACF
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000C8DD File Offset: 0x0000AADD
		[DoNotSerialize]
		public Member.Source source
		{
			get
			{
				this.EnsureReflected();
				return this._source;
			}
			private set
			{
				this._source = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000C8E6 File Offset: 0x0000AAE6
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0000C8F4 File Offset: 0x0000AAF4
		[DoNotSerialize]
		public FieldInfo fieldInfo
		{
			get
			{
				this.EnsureReflected();
				return this._fieldInfo;
			}
			private set
			{
				this._fieldInfo = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000C8FD File Offset: 0x0000AAFD
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0000C90B File Offset: 0x0000AB0B
		[DoNotSerialize]
		public PropertyInfo propertyInfo
		{
			get
			{
				this.EnsureReflected();
				return this._propertyInfo;
			}
			private set
			{
				this._propertyInfo = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000C914 File Offset: 0x0000AB14
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000C922 File Offset: 0x0000AB22
		[DoNotSerialize]
		public MethodInfo methodInfo
		{
			get
			{
				this.EnsureReflected();
				return this._methodInfo;
			}
			private set
			{
				this._methodInfo = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000C92B File Offset: 0x0000AB2B
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000C939 File Offset: 0x0000AB39
		[DoNotSerialize]
		public ConstructorInfo constructorInfo
		{
			get
			{
				this.EnsureReflected();
				return this._constructorInfo;
			}
			private set
			{
				this._constructorInfo = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000C942 File Offset: 0x0000AB42
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000C950 File Offset: 0x0000AB50
		[DoNotSerialize]
		public bool isExtension
		{
			get
			{
				this.EnsureReflected();
				return this._isExtension;
			}
			private set
			{
				this._isExtension = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000C959 File Offset: 0x0000AB59
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0000C967 File Offset: 0x0000AB67
		[DoNotSerialize]
		public bool isInvokedAsExtension
		{
			get
			{
				this.EnsureReflected();
				return this._isInvokedAsExtension;
			}
			private set
			{
				this._isInvokedAsExtension = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000C970 File Offset: 0x0000AB70
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0000C978 File Offset: 0x0000AB78
		[DoNotSerialize]
		public Type[] parameterTypes
		{
			get
			{
				return this._parameterTypes;
			}
			private set
			{
				this._parameterTypes = value;
				this.isReflected = false;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000C988 File Offset: 0x0000AB88
		public MethodBase methodBase
		{
			get
			{
				Member.Source source = this.source;
				if (source == Member.Source.Method)
				{
					return this.methodInfo;
				}
				if (source != Member.Source.Constructor)
				{
					return null;
				}
				return this.constructorInfo;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		private MemberInfo _info
		{
			get
			{
				switch (this.source)
				{
				case Member.Source.Field:
					return this._fieldInfo;
				case Member.Source.Property:
					return this._propertyInfo;
				case Member.Source.Method:
					return this._methodInfo;
				case Member.Source.Constructor:
					return this._constructorInfo;
				default:
					throw new UnexpectedEnumValueException<Member.Source>(this.source);
				}
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000CA10 File Offset: 0x0000AC10
		public MemberInfo info
		{
			get
			{
				switch (this.source)
				{
				case Member.Source.Field:
					return this.fieldInfo;
				case Member.Source.Property:
					return this.propertyInfo;
				case Member.Source.Method:
					return this.methodInfo;
				case Member.Source.Constructor:
					return this.constructorInfo;
				default:
					throw new UnexpectedEnumValueException<Member.Source>(this.source);
				}
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0000CA68 File Offset: 0x0000AC68
		public Type type
		{
			get
			{
				switch (this.source)
				{
				case Member.Source.Field:
					return this.fieldInfo.FieldType;
				case Member.Source.Property:
					return this.propertyInfo.PropertyType;
				case Member.Source.Method:
					return this.methodInfo.ReturnType;
				case Member.Source.Constructor:
					return this.constructorInfo.DeclaringType;
				default:
					throw new UnexpectedEnumValueException<Member.Source>(this.source);
				}
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000CAD1 File Offset: 0x0000ACD1
		public bool isCoroutine
		{
			get
			{
				return this.isGettable && this.type == typeof(IEnumerator);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000CAF2 File Offset: 0x0000ACF2
		public bool isYieldInstruction
		{
			get
			{
				return this.isGettable && typeof(YieldInstruction).IsAssignableFrom(this.type);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000CB13 File Offset: 0x0000AD13
		public bool isGettable
		{
			get
			{
				return this.IsGettable(true);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		public bool isPubliclyGettable
		{
			get
			{
				return this.IsGettable(false);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0000CB25 File Offset: 0x0000AD25
		public bool isSettable
		{
			get
			{
				return this.IsSettable(true);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000CB2E File Offset: 0x0000AD2E
		public bool isPubliclySettable
		{
			get
			{
				return this.IsSettable(false);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0000CB37 File Offset: 0x0000AD37
		public bool isInvocable
		{
			get
			{
				return this.IsInvocable(true);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000CB40 File Offset: 0x0000AD40
		public bool isPubliclyInvocable
		{
			get
			{
				return this.IsInvocable(false);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000CB4C File Offset: 0x0000AD4C
		public bool isAccessor
		{
			get
			{
				switch (this.source)
				{
				case Member.Source.Field:
					return true;
				case Member.Source.Property:
					return true;
				case Member.Source.Method:
					return false;
				case Member.Source.Constructor:
					return false;
				default:
					throw new UnexpectedEnumValueException<Member.Source>(this.source);
				}
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000CB8D File Offset: 0x0000AD8D
		public bool isField
		{
			get
			{
				return this.source == Member.Source.Field;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000CB98 File Offset: 0x0000AD98
		public bool isProperty
		{
			get
			{
				return this.source == Member.Source.Property;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000CBA3 File Offset: 0x0000ADA3
		public bool isMethod
		{
			get
			{
				return this.source == Member.Source.Method;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000CBAE File Offset: 0x0000ADAE
		public bool isConstructor
		{
			get
			{
				return this.source == Member.Source.Constructor;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public bool requiresTarget
		{
			get
			{
				switch (this.source)
				{
				case Member.Source.Field:
					return !this.fieldInfo.IsStatic;
				case Member.Source.Property:
					return !(this.propertyInfo.GetGetMethod(true) ?? this.propertyInfo.GetSetMethod(true)).IsStatic;
				case Member.Source.Method:
					return !this.methodInfo.IsStatic || this.isInvokedAsExtension;
				case Member.Source.Constructor:
					return false;
				default:
					throw new UnexpectedEnumValueException<Member.Source>(this.source);
				}
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000CC41 File Offset: 0x0000AE41
		public bool isOperator
		{
			get
			{
				return this.isMethod && this.methodInfo.IsOperator();
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0000CC58 File Offset: 0x0000AE58
		public bool isConversion
		{
			get
			{
				return this.isMethod && this.methodInfo.IsUserDefinedConversion();
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000CC6F File Offset: 0x0000AE6F
		public int order
		{
			get
			{
				return this.info.MetadataToken;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		public Type declaringType
		{
			get
			{
				return this.info.ExtendedDeclaringType(this.isInvokedAsExtension);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000CC8F File Offset: 0x0000AE8F
		public bool isInherited
		{
			get
			{
				return this.targetType != this.declaringType;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		public Type pseudoDeclaringType
		{
			get
			{
				Type declaringType = this.declaringType;
				if (typeof(Object).IsAssignableFrom(this.targetType))
				{
					if (this.targetType == typeof(GameObject) || this.targetType == typeof(Component) || this.targetType == typeof(ScriptableObject))
					{
						return this.targetType;
					}
					if (declaringType != typeof(Object) && declaringType != typeof(GameObject) && declaringType != typeof(Component) && declaringType != typeof(MonoBehaviour) && declaringType != typeof(ScriptableObject) && declaringType != typeof(object))
					{
						return this.targetType;
					}
				}
				return declaringType;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public bool isPseudoInherited
		{
			get
			{
				return this.targetType != this.pseudoDeclaringType || (this.isMethod && this.methodInfo.IsGenericExtension());
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000CDBE File Offset: 0x0000AFBE
		public bool isIndexer
		{
			get
			{
				return this.isProperty && this.propertyInfo.GetIndexParameters().Length != 0;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000CDD9 File Offset: 0x0000AFD9
		public bool isPredictable
		{
			get
			{
				return this.isField || this.info.HasAttribute(true);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000CDF1 File Offset: 0x0000AFF1
		public bool allowsNull
		{
			get
			{
				return this.isSettable && ((this.type.IsReferenceType() && this.info.HasAttribute(true)) || Nullable.GetUnderlyingType(this.type) != null);
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000CE2B File Offset: 0x0000B02B
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000CE30 File Offset: 0x0000B030
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (this.targetType != null)
			{
				this._targetTypeName = RuntimeCodebase.SerializeType(this.targetType);
				return;
			}
			if (this._targetTypeName != null)
			{
				try
				{
					this.targetType = RuntimeCodebase.DeserializeType(this._targetTypeName);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000CE8C File Offset: 0x0000B08C
		public bool IsGettable(bool nonPublic)
		{
			switch (this.source)
			{
			case Member.Source.Field:
				return nonPublic || this.fieldInfo.IsPublic;
			case Member.Source.Property:
				return this.propertyInfo.CanRead && (nonPublic || this.propertyInfo.GetGetMethod(false) != null);
			case Member.Source.Method:
				return this.methodInfo.ReturnType != typeof(void) && (nonPublic || this.methodInfo.IsPublic);
			case Member.Source.Constructor:
				return nonPublic || this.constructorInfo.IsPublic;
			default:
				throw new UnexpectedEnumValueException<Member.Source>(this.source);
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000CF40 File Offset: 0x0000B140
		public bool IsSettable(bool nonPublic)
		{
			switch (this.source)
			{
			case Member.Source.Field:
				return !this.fieldInfo.IsLiteral && !this.fieldInfo.IsInitOnly && (nonPublic || this.fieldInfo.IsPublic);
			case Member.Source.Property:
				return this.propertyInfo.CanWrite && (nonPublic || this.propertyInfo.GetSetMethod(false) != null);
			case Member.Source.Method:
				return false;
			case Member.Source.Constructor:
				return false;
			default:
				throw new UnexpectedEnumValueException<Member.Source>(this.source);
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		public bool IsInvocable(bool nonPublic)
		{
			switch (this.source)
			{
			case Member.Source.Field:
				return false;
			case Member.Source.Property:
				return false;
			case Member.Source.Method:
				return nonPublic || this.methodInfo.IsPublic;
			case Member.Source.Constructor:
				return nonPublic || this.constructorInfo.IsPublic;
			default:
				throw new UnexpectedEnumValueException<Member.Source>(this.source);
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000D033 File Offset: 0x0000B233
		private void EnsureExplicitParameterTypes()
		{
			if (this.parameterTypes == null)
			{
				throw new InvalidOperationException("Missing parameter types.");
			}
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0000D048 File Offset: 0x0000B248
		public void Reflect()
		{
			if (this.targetType == null)
			{
				if (this.targetTypeName != null)
				{
					throw new MissingMemberException(this.targetTypeName, this.name);
				}
				throw new MissingMemberException("Target type not found.");
			}
			else
			{
				this._source = Member.Source.Unknown;
				this._fieldInfo = null;
				this._propertyInfo = null;
				this._methodInfo = null;
				this._constructorInfo = null;
				this.fieldAccessor = null;
				this.propertyAccessor = null;
				this.methodInvoker = null;
				MemberInfo[] array;
				try
				{
					array = this.targetType.GetExtendedMember(this.name, MemberTypes.Constructor | MemberTypes.Field | MemberTypes.Method | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
				}
				catch (NotSupportedException ex)
				{
					throw new InvalidOperationException(string.Format("An error occured when trying to reflect the member '{0}' of the type '{1}' in a '{2}' unit. Supported member types: {3}, supported binding flags: {4}", new object[]
					{
						this.name,
						this.targetType.FullName,
						base.GetType().Name,
						MemberTypes.Constructor | MemberTypes.Field | MemberTypes.Method | MemberTypes.Property,
						BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy
					}), ex);
				}
				string text;
				if (array.Length == 0 && RuntimeCodebase.RenamedMembers(this.targetType).TryGetValue(this.name, out text))
				{
					this.name = text;
					try
					{
						array = this.targetType.GetExtendedMember(this.name, MemberTypes.Constructor | MemberTypes.Field | MemberTypes.Method | MemberTypes.Property, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
					}
					catch (NotSupportedException ex2)
					{
						throw new InvalidOperationException(string.Format("An error occured when trying to reflect the renamed member '{0}' of the type '{1}' in a '{2}' unit. Supported member types: {3}, supported binding flags: {4}", new object[]
						{
							this.name,
							this.targetType.FullName,
							base.GetType().Name,
							MemberTypes.Constructor | MemberTypes.Field | MemberTypes.Method | MemberTypes.Property,
							BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy
						}), ex2);
					}
				}
				if (array.Length == 0)
				{
					throw new MissingMemberException(string.Concat(new string[]
					{
						"No matching member found: '",
						this.targetType.Name,
						".",
						this.name,
						"'"
					}));
				}
				MemberTypes? memberTypes = null;
				foreach (MemberInfo memberInfo in array)
				{
					if (memberTypes == null)
					{
						memberTypes = new MemberTypes?(memberInfo.MemberType);
					}
					else
					{
						MemberTypes memberType = memberInfo.MemberType;
						MemberTypes? memberTypes2 = memberTypes;
						if (!((memberType == memberTypes2.GetValueOrDefault()) & (memberTypes2 != null)) && !memberInfo.IsExtensionMethod())
						{
							Debug.LogWarning(string.Concat(new string[]
							{
								"Multiple members with the same name are of a different type: '",
								this.targetType.Name,
								".",
								this.name,
								"'"
							}));
							break;
						}
					}
				}
				if (memberTypes != null)
				{
					MemberTypes valueOrDefault = memberTypes.GetValueOrDefault();
					if (valueOrDefault <= MemberTypes.Field)
					{
						if (valueOrDefault != MemberTypes.Constructor)
						{
							if (valueOrDefault != MemberTypes.Field)
							{
								goto IL_02AA;
							}
							this.ReflectField(array);
						}
						else
						{
							this.ReflectConstructor(array);
						}
					}
					else if (valueOrDefault != MemberTypes.Method)
					{
						if (valueOrDefault != MemberTypes.Property)
						{
							goto IL_02AA;
						}
						this.ReflectProperty(array);
					}
					else
					{
						this.ReflectMethod(array);
					}
					this.isReflected = true;
					return;
				}
				IL_02AA:
				throw new UnexpectedEnumValueException<MemberTypes>(memberTypes.Value);
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000D330 File Offset: 0x0000B530
		private void ReflectField(IEnumerable<MemberInfo> candidates)
		{
			this._source = Member.Source.Field;
			this._fieldInfo = candidates.OfType<FieldInfo>().Disambiguate(this.targetType);
			if (this._fieldInfo == null)
			{
				throw new MissingMemberException(string.Concat(new string[]
				{
					"No matching field found: '",
					this.targetType.Name,
					".",
					this.name,
					"'"
				}));
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000D3AC File Offset: 0x0000B5AC
		private void ReflectProperty(IEnumerable<MemberInfo> candidates)
		{
			this._source = Member.Source.Property;
			this._propertyInfo = candidates.OfType<PropertyInfo>().Disambiguate(this.targetType);
			if (this._propertyInfo == null)
			{
				throw new MissingMemberException(string.Concat(new string[]
				{
					"No matching property found: '",
					this.targetType.Name,
					".",
					this.name,
					"'"
				}));
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000D428 File Offset: 0x0000B628
		private void ReflectConstructor(IEnumerable<MemberInfo> candidates)
		{
			this._source = Member.Source.Constructor;
			this.EnsureExplicitParameterTypes();
			this._constructorInfo = (from c in candidates.OfType<ConstructorInfo>()
				where !c.IsStatic
				select c).Disambiguate(this.targetType, this.parameterTypes);
			if (this._constructorInfo == null)
			{
				string[] array = new string[5];
				array[0] = "No matching constructor found: '";
				array[1] = this.targetType.Name;
				array[2] = " (";
				array[3] = this.parameterTypes.Select((Type t) => t.Name).ToCommaSeparatedString();
				array[4] = ")'";
				throw new MissingMemberException(string.Concat(array));
			}
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		private void ReflectMethod(IEnumerable<MemberInfo> candidates)
		{
			this._source = Member.Source.Method;
			this.EnsureExplicitParameterTypes();
			this._methodInfo = candidates.OfType<MethodInfo>().Disambiguate(this.targetType, this.parameterTypes);
			if (this._methodInfo == null)
			{
				string[] array = new string[8];
				array[0] = "No matching method found: '";
				array[1] = this.targetType.Name;
				array[2] = ".";
				array[3] = this.name;
				array[4] = " (";
				array[5] = this.parameterTypes.Select((Type t) => t.Name).ToCommaSeparatedString();
				array[6] = ")'\nCandidates:\n";
				array[7] = candidates.ToLineSeparatedString();
				throw new MissingMemberException(string.Concat(array));
			}
			this._isExtension = this._methodInfo.IsExtension();
			this._isInvokedAsExtension = this._methodInfo.IsInvokedAsExtension(this.targetType);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000D5F0 File Offset: 0x0000B7F0
		public void Prewarm()
		{
			if (this.fieldAccessor == null)
			{
				FieldInfo fieldInfo = this.fieldInfo;
				this.fieldAccessor = ((fieldInfo != null) ? fieldInfo.Prewarm() : null);
			}
			if (this.propertyAccessor == null)
			{
				PropertyInfo propertyInfo = this.propertyInfo;
				this.propertyAccessor = ((propertyInfo != null) ? propertyInfo.Prewarm() : null);
			}
			if (this.methodInvoker == null)
			{
				MethodInfo methodInfo = this.methodInfo;
				this.methodInvoker = ((methodInfo != null) ? methodInfo.Prewarm() : null);
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000D65D File Offset: 0x0000B85D
		public void EnsureReflected()
		{
			if (!this.isReflected)
			{
				this.Reflect();
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000D670 File Offset: 0x0000B870
		public void EnsureReady(object target)
		{
			this.EnsureReflected();
			if (target == null && this.requiresTarget)
			{
				throw new InvalidOperationException(string.Format("Missing target object for '{0}.{1}'.", this.targetType, this.name));
			}
			if (target != null && !this.requiresTarget)
			{
				throw new InvalidOperationException(string.Format("Superfluous target object for '{0}.{1}'.", this.targetType, this.name));
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		public object Get(object target)
		{
			this.EnsureReady(target);
			switch (this.source)
			{
			case Member.Source.Field:
				if (this.fieldAccessor == null)
				{
					this.fieldAccessor = this.fieldInfo.Prewarm();
				}
				return this.fieldAccessor.GetValue(target);
			case Member.Source.Property:
				if (this.propertyAccessor == null)
				{
					this.propertyAccessor = this.propertyInfo.Prewarm();
				}
				return this.propertyAccessor.GetValue(target);
			case Member.Source.Method:
				throw new NotSupportedException("Member is a method. Consider using 'Invoke' instead.");
			case Member.Source.Constructor:
				throw new NotSupportedException("Member is a constructor. Consider using 'Invoke' instead.");
			default:
				throw new UnexpectedEnumValueException<Member.Source>(this.source);
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000D776 File Offset: 0x0000B976
		public T Get<T>(object target)
		{
			return (T)((object)this.Get(target));
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000D784 File Offset: 0x0000B984
		public object Set(object target, object value)
		{
			this.EnsureReady(target);
			switch (this.source)
			{
			case Member.Source.Field:
				if (this.fieldAccessor == null)
				{
					this.fieldAccessor = this.fieldInfo.Prewarm();
				}
				this.fieldAccessor.SetValue(target, value);
				return value;
			case Member.Source.Property:
				if (this.propertyAccessor == null)
				{
					this.propertyAccessor = this.propertyInfo.Prewarm();
				}
				this.propertyAccessor.SetValue(target, value);
				return value;
			case Member.Source.Method:
				throw new NotSupportedException("Member is a method.");
			case Member.Source.Constructor:
				throw new NotSupportedException("Member is a constructor.");
			default:
				throw new UnexpectedEnumValueException<Member.Source>(this.source);
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000D82C File Offset: 0x0000BA2C
		private void EnsureInvocable(object target)
		{
			this.EnsureReady(target);
			if (this.source == Member.Source.Field || this.source == Member.Source.Property)
			{
				throw new NotSupportedException("Member is a field or property.");
			}
			if (this.source == Member.Source.Method)
			{
				if (this.methodInfo.ContainsGenericParameters)
				{
					throw new NotSupportedException(string.Format("Trying to invoke an open-constructed generic method: '{0}'.", this.methodInfo));
				}
				if (this.methodInvoker == null)
				{
					this.methodInvoker = this.methodInfo.Prewarm();
					return;
				}
			}
			else
			{
				if (this.source != Member.Source.Constructor)
				{
					throw new UnexpectedEnumValueException<Member.Source>(this.source);
				}
				if (this.constructorInfo.ContainsGenericParameters)
				{
					throw new NotSupportedException(string.Format("Trying to invoke an open-constructed generic constructor: '{0}'.", this.constructorInfo));
				}
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000D8DB File Offset: 0x0000BADB
		public IEnumerable<ParameterInfo> GetParameterInfos()
		{
			this.EnsureReflected();
			return this.methodBase.GetInvocationParameters(this.isInvokedAsExtension);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000D8F4 File Offset: 0x0000BAF4
		public object Invoke(object target)
		{
			this.EnsureInvocable(target);
			if (this.source != Member.Source.Method)
			{
				return this.constructorInfo.Invoke(Member.EmptyObjects);
			}
			if (this.isInvokedAsExtension)
			{
				return this.methodInvoker.Invoke(null, target);
			}
			return this.methodInvoker.Invoke(target);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000D944 File Offset: 0x0000BB44
		public object Invoke(object target, object arg0)
		{
			this.EnsureInvocable(target);
			if (this.source != Member.Source.Method)
			{
				return this.constructorInfo.Invoke(new object[] { arg0 });
			}
			if (this.isInvokedAsExtension)
			{
				return this.methodInvoker.Invoke(null, target, arg0);
			}
			return this.methodInvoker.Invoke(target, arg0);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000D99C File Offset: 0x0000BB9C
		public object Invoke(object target, object arg0, object arg1)
		{
			this.EnsureInvocable(target);
			if (this.source != Member.Source.Method)
			{
				return this.constructorInfo.Invoke(new object[] { arg0, arg1 });
			}
			if (this.isInvokedAsExtension)
			{
				return this.methodInvoker.Invoke(null, target, arg0, arg1);
			}
			return this.methodInvoker.Invoke(target, arg0, arg1);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		public object Invoke(object target, object arg0, object arg1, object arg2)
		{
			this.EnsureInvocable(target);
			if (this.source != Member.Source.Method)
			{
				return this.constructorInfo.Invoke(new object[] { arg0, arg1, arg2 });
			}
			if (this.isInvokedAsExtension)
			{
				return this.methodInvoker.Invoke(null, target, arg0, arg1, arg2);
			}
			return this.methodInvoker.Invoke(target, arg0, arg1, arg2);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000DA64 File Offset: 0x0000BC64
		public object Invoke(object target, object arg0, object arg1, object arg2, object arg3)
		{
			this.EnsureInvocable(target);
			if (this.source != Member.Source.Method)
			{
				return this.constructorInfo.Invoke(new object[] { arg0, arg1, arg2, arg3 });
			}
			if (this.isInvokedAsExtension)
			{
				return this.methodInvoker.Invoke(null, target, arg0, arg1, arg2, arg3);
			}
			return this.methodInvoker.Invoke(target, arg0, arg1, arg2, arg3);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		public object Invoke(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			this.EnsureInvocable(target);
			if (this.source != Member.Source.Method)
			{
				return this.constructorInfo.Invoke(new object[] { arg0, arg1, arg2, arg3, arg4 });
			}
			if (this.isInvokedAsExtension)
			{
				return this.methodInvoker.Invoke(null, new object[] { target, arg0, arg1, arg2, arg3, arg4 });
			}
			return this.methodInvoker.Invoke(target, arg0, arg1, arg2, arg3, arg4);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000DB64 File Offset: 0x0000BD64
		public object Invoke(object target, params object[] arguments)
		{
			this.EnsureInvocable(target);
			if (this.source != Member.Source.Method)
			{
				return this.constructorInfo.Invoke(arguments);
			}
			if (this.isInvokedAsExtension)
			{
				object[] array = new object[arguments.Length + 1];
				array[0] = target;
				Array.Copy(arguments, 0, array, 1, arguments.Length);
				return this.methodInvoker.Invoke(null, array);
			}
			return this.methodInvoker.Invoke(target, arguments);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		public T Invoke<T>(object target)
		{
			return (T)((object)this.Invoke(target));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000DBDA File Offset: 0x0000BDDA
		public T Invoke<T>(object target, object arg0)
		{
			return (T)((object)this.Invoke(target, arg0));
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000DBE9 File Offset: 0x0000BDE9
		public T Invoke<T>(object target, object arg0, object arg1)
		{
			return (T)((object)this.Invoke(target, arg0, arg1));
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000DBF9 File Offset: 0x0000BDF9
		public T Invoke<T>(object target, object arg0, object arg1, object arg2)
		{
			return (T)((object)this.Invoke(target, arg0, arg1, arg2));
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000DC0B File Offset: 0x0000BE0B
		public T Invoke<T>(object target, object arg0, object arg1, object arg2, object arg3)
		{
			return (T)((object)this.Invoke(target, arg0, arg1, arg2, arg3));
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000DC1F File Offset: 0x0000BE1F
		public T Invoke<T>(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			return (T)((object)this.Invoke(target, arg0, arg1, arg2, arg3, arg4));
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000DC35 File Offset: 0x0000BE35
		public T Invoke<T>(object target, params object[] arguments)
		{
			return (T)((object)this.Invoke(target, arguments));
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000DC44 File Offset: 0x0000BE44
		public override bool Equals(object obj)
		{
			Member member = obj as Member;
			if (!(member != null) || !(this.targetType == member.targetType) || !(this.name == member.name))
			{
				return false;
			}
			bool flag = this.parameterTypes != null;
			bool flag2 = member.parameterTypes != null;
			if (flag != flag2)
			{
				return false;
			}
			if (flag)
			{
				int num = this.parameterTypes.Length;
				int num2 = member.parameterTypes.Length;
				if (num != num2)
				{
					return false;
				}
				for (int i = 0; i < num; i++)
				{
					if (this.parameterTypes[i] != member.parameterTypes[i])
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		public override int GetHashCode()
		{
			int num = 17;
			int num2 = num * 23;
			Type targetType = this.targetType;
			num = num2 + ((targetType != null) ? targetType.GetHashCode() : 0);
			int num3 = num * 23;
			string name = this.name;
			num = num3 + ((name != null) ? name.GetHashCode() : 0);
			if (this.parameterTypes != null)
			{
				foreach (Type type in this.parameterTypes)
				{
					num = num * 23 + type.GetHashCode();
				}
			}
			else
			{
				num *= 23;
			}
			return num;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000DD65 File Offset: 0x0000BF65
		public static bool operator ==(Member a, Member b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		public static bool operator !=(Member a, Member b)
		{
			return !(a == b);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public string ToUniqueString()
		{
			string text = this.targetType.FullName + "." + this.name;
			if (this.parameterTypes != null)
			{
				text += "(";
				foreach (Type type in this.parameterTypes)
				{
					text += type.FullName;
				}
				text += ")";
			}
			return text;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000DDF8 File Offset: 0x0000BFF8
		public override string ToString()
		{
			return this.targetType.CSharpName(true) + "." + this.name;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000DE16 File Offset: 0x0000C016
		public Member ToDeclarer()
		{
			return new Member(this.declaringType, this.name, this.parameterTypes);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000DE2F File Offset: 0x0000C02F
		public Member ToPseudoDeclarer()
		{
			return new Member(this.pseudoDeclaringType, this.name, this.parameterTypes);
		}

		// Token: 0x04000127 RID: 295
		[SerializeAs("name")]
		private string _name;

		// Token: 0x04000128 RID: 296
		[SerializeAs("parameterTypes")]
		private Type[] _parameterTypes;

		// Token: 0x04000129 RID: 297
		[SerializeAs("targetType")]
		private Type _targetType;

		// Token: 0x0400012A RID: 298
		[SerializeAs("targetTypeName")]
		private string _targetTypeName;

		// Token: 0x0400012B RID: 299
		[DoNotSerialize]
		private Member.Source _source;

		// Token: 0x0400012C RID: 300
		[DoNotSerialize]
		private FieldInfo _fieldInfo;

		// Token: 0x0400012D RID: 301
		[DoNotSerialize]
		private PropertyInfo _propertyInfo;

		// Token: 0x0400012E RID: 302
		[DoNotSerialize]
		private MethodInfo _methodInfo;

		// Token: 0x0400012F RID: 303
		[DoNotSerialize]
		private ConstructorInfo _constructorInfo;

		// Token: 0x04000130 RID: 304
		[DoNotSerialize]
		private bool _isExtension;

		// Token: 0x04000131 RID: 305
		[DoNotSerialize]
		private bool _isInvokedAsExtension;

		// Token: 0x04000132 RID: 306
		[DoNotSerialize]
		private IOptimizedAccessor fieldAccessor;

		// Token: 0x04000133 RID: 307
		[DoNotSerialize]
		private IOptimizedAccessor propertyAccessor;

		// Token: 0x04000134 RID: 308
		[DoNotSerialize]
		private IOptimizedInvoker methodInvoker;

		// Token: 0x04000136 RID: 310
		public const MemberTypes SupportedMemberTypes = MemberTypes.Constructor | MemberTypes.Field | MemberTypes.Method | MemberTypes.Property;

		// Token: 0x04000137 RID: 311
		public const BindingFlags SupportedBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

		// Token: 0x04000138 RID: 312
		private static readonly object[] EmptyObjects = new object[0];

		// Token: 0x020001CE RID: 462
		public enum Source
		{
			// Token: 0x04000324 RID: 804
			Unknown,
			// Token: 0x04000325 RID: 805
			Field,
			// Token: 0x04000326 RID: 806
			Property,
			// Token: 0x04000327 RID: 807
			Method,
			// Token: 0x04000328 RID: 808
			Constructor
		}
	}
}

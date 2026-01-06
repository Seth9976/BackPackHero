using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x020001A8 RID: 424
	public class fsMetaType
	{
		// Token: 0x06000B56 RID: 2902 RVA: 0x00030334 File Offset: 0x0002E534
		private fsMetaType(fsConfig config, Type reflectedType)
		{
			this.ReflectedType = reflectedType;
			List<fsMetaProperty> list = new List<fsMetaProperty>();
			fsMetaType.CollectProperties(config, list, reflectedType);
			this.Properties = list.ToArray();
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00030368 File Offset: 0x0002E568
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x00030370 File Offset: 0x0002E570
		public fsMetaProperty[] Properties { get; private set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0003037C File Offset: 0x0002E57C
		public bool HasDefaultConstructor
		{
			get
			{
				if (this._hasDefaultConstructorCache == null)
				{
					if (this.ReflectedType.Resolve().IsArray)
					{
						this._hasDefaultConstructorCache = new bool?(true);
						this._isDefaultConstructorPublic = true;
					}
					else if (this.ReflectedType.Resolve().IsValueType)
					{
						this._hasDefaultConstructorCache = new bool?(true);
						this._isDefaultConstructorPublic = true;
					}
					else
					{
						ConstructorInfo declaredConstructor = this.ReflectedType.GetDeclaredConstructor(fsPortableReflection.EmptyTypes);
						this._hasDefaultConstructorCache = new bool?(declaredConstructor != null);
						if (declaredConstructor != null)
						{
							this._isDefaultConstructorPublic = declaredConstructor.IsPublic;
						}
					}
				}
				return this._hasDefaultConstructorCache.Value;
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0003042C File Offset: 0x0002E62C
		public bool EmitAotData()
		{
			if (this._hasEmittedAotData)
			{
				return false;
			}
			this._hasEmittedAotData = true;
			for (int i = 0; i < this.Properties.Length; i++)
			{
				if (!this.Properties[i].IsPublic)
				{
					return false;
				}
				if (this.Properties[i].IsReadOnly)
				{
					return false;
				}
			}
			if (!this.HasDefaultConstructor)
			{
				return false;
			}
			fsAotCompilationManager.AddAotCompilation(this.ReflectedType, this.Properties, this._isDefaultConstructorPublic);
			return true;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x000304A4 File Offset: 0x0002E6A4
		public object CreateInstance()
		{
			if (this.ReflectedType.Resolve().IsInterface || this.ReflectedType.Resolve().IsAbstract)
			{
				string text = "Cannot create an instance of an interface or abstract type for ";
				Type reflectedType = this.ReflectedType;
				throw new Exception(text + ((reflectedType != null) ? reflectedType.ToString() : null));
			}
			if (typeof(ScriptableObject).IsAssignableFrom(this.ReflectedType))
			{
				return ScriptableObject.CreateInstance(this.ReflectedType);
			}
			if (typeof(string) == this.ReflectedType)
			{
				return string.Empty;
			}
			if (!this.HasDefaultConstructor)
			{
				return FormatterServices.GetSafeUninitializedObject(this.ReflectedType);
			}
			if (this.ReflectedType.Resolve().IsArray)
			{
				return Array.CreateInstance(this.ReflectedType.GetElementType(), 0);
			}
			object obj;
			try
			{
				obj = Activator.CreateInstance(this.ReflectedType, true);
			}
			catch (MissingMethodException ex)
			{
				string text2 = "Unable to create instance of ";
				Type reflectedType2 = this.ReflectedType;
				throw new InvalidOperationException(text2 + ((reflectedType2 != null) ? reflectedType2.ToString() : null) + "; there is no default constructor", ex);
			}
			catch (TargetInvocationException ex2)
			{
				string text3 = "Constructor of ";
				Type reflectedType3 = this.ReflectedType;
				throw new InvalidOperationException(text3 + ((reflectedType3 != null) ? reflectedType3.ToString() : null) + " threw an exception when creating an instance", ex2);
			}
			catch (MemberAccessException ex3)
			{
				string text4 = "Unable to access constructor of ";
				Type reflectedType4 = this.ReflectedType;
				throw new InvalidOperationException(text4 + ((reflectedType4 != null) ? reflectedType4.ToString() : null), ex3);
			}
			return obj;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00030620 File Offset: 0x0002E820
		public static fsMetaType Get(fsConfig config, Type type)
		{
			Type typeFromHandle = typeof(fsMetaType);
			Dictionary<Type, fsMetaType> dictionary;
			lock (typeFromHandle)
			{
				if (!fsMetaType._configMetaTypes.TryGetValue(config, out dictionary))
				{
					dictionary = (fsMetaType._configMetaTypes[config] = new Dictionary<Type, fsMetaType>());
				}
			}
			fsMetaType fsMetaType;
			if (!dictionary.TryGetValue(type, out fsMetaType))
			{
				fsMetaType = new fsMetaType(config, type);
				dictionary[type] = fsMetaType;
			}
			return fsMetaType;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000306A0 File Offset: 0x0002E8A0
		public static void ClearCache()
		{
			Type typeFromHandle = typeof(fsMetaType);
			lock (typeFromHandle)
			{
				fsMetaType._configMetaTypes = new Dictionary<fsConfig, Dictionary<Type, fsMetaType>>();
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000306E8 File Offset: 0x0002E8E8
		private static void CollectProperties(fsConfig config, List<fsMetaProperty> properties, Type reflectedType)
		{
			bool flag = config.DefaultMemberSerialization == fsMemberSerialization.OptIn;
			bool flag2 = config.DefaultMemberSerialization == fsMemberSerialization.OptOut;
			fsObjectAttribute attribute = fsPortableReflection.GetAttribute<fsObjectAttribute>(reflectedType);
			if (attribute != null)
			{
				flag = attribute.MemberSerialization == fsMemberSerialization.OptIn;
				flag2 = attribute.MemberSerialization == fsMemberSerialization.OptOut;
			}
			MemberInfo[] declaredMembers = reflectedType.GetDeclaredMembers();
			MemberInfo[] array = declaredMembers;
			for (int i = 0; i < array.Length; i++)
			{
				MemberInfo member = array[i];
				if (!config.IgnoreSerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(member, t)))
				{
					PropertyInfo propertyInfo = member as PropertyInfo;
					FieldInfo fieldInfo = member as FieldInfo;
					if ((!(propertyInfo == null) || !(fieldInfo == null)) && (!(propertyInfo != null) || config.EnablePropertySerialization) && (!flag || config.SerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(member, t))) && (!flag2 || !config.IgnoreSerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(member, t))))
					{
						if (propertyInfo != null)
						{
							if (fsMetaType.CanSerializeProperty(config, propertyInfo, declaredMembers, flag2))
							{
								properties.Add(new fsMetaProperty(config, propertyInfo));
							}
						}
						else if (fieldInfo != null && fsMetaType.CanSerializeField(config, fieldInfo, flag2))
						{
							properties.Add(new fsMetaProperty(config, fieldInfo));
						}
					}
				}
			}
			if (reflectedType.Resolve().BaseType != null)
			{
				fsMetaType.CollectProperties(config, properties, reflectedType.Resolve().BaseType);
			}
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0003086A File Offset: 0x0002EA6A
		private static bool IsAutoProperty(PropertyInfo property, MemberInfo[] members)
		{
			return property.CanWrite && property.CanRead && fsPortableReflection.HasAttribute(property.GetGetMethod(), typeof(CompilerGeneratedAttribute), false);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00030894 File Offset: 0x0002EA94
		private static bool CanSerializeProperty(fsConfig config, PropertyInfo property, MemberInfo[] members, bool annotationFreeValue)
		{
			if (typeof(Delegate).IsAssignableFrom(property.PropertyType))
			{
				return false;
			}
			MethodInfo getMethod = property.GetGetMethod(false);
			MethodInfo setMethod = property.GetSetMethod(false);
			return (!(getMethod != null) || !getMethod.IsStatic) && (!(setMethod != null) || !setMethod.IsStatic) && property.GetIndexParameters().Length == 0 && (config.SerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(property, t)) || (property.CanRead && property.CanWrite && ((getMethod != null && (config.SerializeNonPublicSetProperties || setMethod != null) && (config.SerializeNonAutoProperties || fsMetaType.IsAutoProperty(property, members))) || annotationFreeValue)));
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00030984 File Offset: 0x0002EB84
		private static bool CanSerializeField(fsConfig config, FieldInfo field, bool annotationFreeValue)
		{
			return !typeof(Delegate).IsAssignableFrom(field.FieldType) && !Attribute.IsDefined(field, typeof(CompilerGeneratedAttribute), false) && !field.IsStatic && (config.SerializeAttributes.Any((Type t) => fsPortableReflection.HasAttribute(field, t)) || annotationFreeValue || field.IsPublic);
		}

		// Token: 0x040002BB RID: 699
		public Type ReflectedType;

		// Token: 0x040002BC RID: 700
		private bool _hasEmittedAotData;

		// Token: 0x040002BD RID: 701
		private bool? _hasDefaultConstructorCache;

		// Token: 0x040002BE RID: 702
		private bool _isDefaultConstructorPublic;

		// Token: 0x040002C0 RID: 704
		private static Dictionary<fsConfig, Dictionary<Type, fsMetaType>> _configMetaTypes = new Dictionary<fsConfig, Dictionary<Type, fsMetaType>>();
	}
}

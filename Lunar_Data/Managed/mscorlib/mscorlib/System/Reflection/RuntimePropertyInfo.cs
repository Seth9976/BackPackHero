using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using Mono;

namespace System.Reflection
{
	// Token: 0x02000901 RID: 2305
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimePropertyInfo : PropertyInfo, ISerializable
	{
		// Token: 0x06004E0B RID: 19979
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void get_property_info(RuntimePropertyInfo prop, ref MonoPropertyInfo info, PInfo req_info);

		// Token: 0x06004E0C RID: 19980
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type[] GetTypeModifiers(RuntimePropertyInfo prop, bool optional);

		// Token: 0x06004E0D RID: 19981
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object get_default_value(RuntimePropertyInfo prop);

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004E0E RID: 19982 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004E0F RID: 19983 RVA: 0x000F5723 File Offset: 0x000F3923
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x000F39BE File Offset: 0x000F1BBE
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004E11 RID: 19985 RVA: 0x000F39CB File Offset: 0x000F1BCB
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x000F572B File Offset: 0x000F392B
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x000F5738 File Offset: 0x000F3938
		public override string ToString()
		{
			return this.FormatNameAndSig(false);
		}

		// Token: 0x06004E14 RID: 19988 RVA: 0x000F5744 File Offset: 0x000F3944
		private string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.PropertyType.FormatTypeName(serialization));
			stringBuilder.Append(" ");
			stringBuilder.Append(this.Name);
			ParameterInfo[] indexParameters = this.GetIndexParameters();
			if (indexParameters.Length != 0)
			{
				stringBuilder.Append(" [");
				RuntimeParameterInfo.FormatParameters(stringBuilder, indexParameters, (CallingConventions)0, serialization);
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x000F57AE File Offset: 0x000F39AE
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Property, null);
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x000F57DF File Offset: 0x000F39DF
		internal string SerializationToString()
		{
			return this.FormatNameAndSig(true);
		}

		// Token: 0x06004E17 RID: 19991 RVA: 0x000F57E8 File Offset: 0x000F39E8
		private void CachePropertyInfo(PInfo flags)
		{
			if ((this.cached & flags) != flags)
			{
				RuntimePropertyInfo.get_property_info(this, ref this.info, flags);
				this.cached |= flags;
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004E18 RID: 19992 RVA: 0x000F5810 File Offset: 0x000F3A10
		public override PropertyAttributes Attributes
		{
			get
			{
				this.CachePropertyInfo(PInfo.Attributes);
				return this.info.attrs;
			}
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06004E19 RID: 19993 RVA: 0x000F5824 File Offset: 0x000F3A24
		public override bool CanRead
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod);
				return this.info.get_method != null;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004E1A RID: 19994 RVA: 0x000F583E File Offset: 0x000F3A3E
		public override bool CanWrite
		{
			get
			{
				this.CachePropertyInfo(PInfo.SetMethod);
				return this.info.set_method != null;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004E1B RID: 19995 RVA: 0x000F5858 File Offset: 0x000F3A58
		public override Type PropertyType
		{
			get
			{
				this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
				if (this.info.get_method != null)
				{
					return this.info.get_method.ReturnType;
				}
				ParameterInfo[] parametersInternal = this.info.set_method.GetParametersInternal();
				return parametersInternal[parametersInternal.Length - 1].ParameterType;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004E1C RID: 19996 RVA: 0x000F58AB File Offset: 0x000F3AAB
		public override Type ReflectedType
		{
			get
			{
				this.CachePropertyInfo(PInfo.ReflectedType);
				return this.info.parent;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004E1D RID: 19997 RVA: 0x000F58BF File Offset: 0x000F3ABF
		public override Type DeclaringType
		{
			get
			{
				this.CachePropertyInfo(PInfo.DeclaringType);
				return this.info.declaring_type;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06004E1E RID: 19998 RVA: 0x000F58D4 File Offset: 0x000F3AD4
		public override string Name
		{
			get
			{
				this.CachePropertyInfo(PInfo.Name);
				return this.info.name;
			}
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x000F58EC File Offset: 0x000F3AEC
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			int num = 0;
			int num2 = 0;
			this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
			if (this.info.set_method != null && (nonPublic || this.info.set_method.IsPublic))
			{
				num2 = 1;
			}
			if (this.info.get_method != null && (nonPublic || this.info.get_method.IsPublic))
			{
				num = 1;
			}
			MethodInfo[] array = new MethodInfo[num + num2];
			int num3 = 0;
			if (num2 != 0)
			{
				array[num3++] = this.info.set_method;
			}
			if (num != 0)
			{
				array[num3++] = this.info.get_method;
			}
			return array;
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x000F598E File Offset: 0x000F3B8E
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.GetMethod);
			if (this.info.get_method != null && (nonPublic || this.info.get_method.IsPublic))
			{
				return this.info.get_method;
			}
			return null;
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x000F59CC File Offset: 0x000F3BCC
		public override ParameterInfo[] GetIndexParameters()
		{
			this.CachePropertyInfo(PInfo.GetMethod | PInfo.SetMethod);
			ParameterInfo[] array;
			int num;
			if (this.info.get_method != null)
			{
				array = this.info.get_method.GetParametersInternal();
				num = array.Length;
			}
			else
			{
				if (!(this.info.set_method != null))
				{
					return EmptyArray<ParameterInfo>.Value;
				}
				array = this.info.set_method.GetParametersInternal();
				num = array.Length - 1;
			}
			ParameterInfo[] array2 = new ParameterInfo[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = RuntimeParameterInfo.New(array[i], this);
			}
			return array2;
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x000F5A5C File Offset: 0x000F3C5C
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			this.CachePropertyInfo(PInfo.SetMethod);
			if (this.info.set_method != null && (nonPublic || this.info.set_method.IsPublic))
			{
				return this.info.set_method;
			}
			return null;
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x000F5A9A File Offset: 0x000F3C9A
		public override object GetConstantValue()
		{
			return RuntimePropertyInfo.get_default_value(this);
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x000F5A9A File Offset: 0x000F3C9A
		public override object GetRawConstantValue()
		{
			return RuntimePropertyInfo.get_default_value(this);
		}

		// Token: 0x06004E25 RID: 20005 RVA: 0x000F5AA2 File Offset: 0x000F3CA2
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, false);
		}

		// Token: 0x06004E26 RID: 20006 RVA: 0x000F5478 File Offset: 0x000F3678
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, false);
		}

		// Token: 0x06004E27 RID: 20007 RVA: 0x000F5481 File Offset: 0x000F3681
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, false);
		}

		// Token: 0x06004E28 RID: 20008 RVA: 0x000F5AAC File Offset: 0x000F3CAC
		private static object GetterAdapterFrame<T, R>(RuntimePropertyInfo.Getter<T, R> getter, object obj)
		{
			return getter((T)((object)obj));
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x000F5ABF File Offset: 0x000F3CBF
		private static object StaticGetterAdapterFrame<R>(RuntimePropertyInfo.StaticGetter<R> getter, object obj)
		{
			return getter();
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x000F5ACC File Offset: 0x000F3CCC
		private static RuntimePropertyInfo.GetterAdapter CreateGetterDelegate(MethodInfo method)
		{
			Type[] array;
			Type type;
			string text;
			if (method.IsStatic)
			{
				array = new Type[] { method.ReturnType };
				type = typeof(RuntimePropertyInfo.StaticGetter<>);
				text = "StaticGetterAdapterFrame";
			}
			else
			{
				array = new Type[] { method.DeclaringType, method.ReturnType };
				type = typeof(RuntimePropertyInfo.Getter<, >);
				text = "GetterAdapterFrame";
			}
			object obj = Delegate.CreateDelegate(type.MakeGenericType(array), method);
			MethodInfo methodInfo = typeof(RuntimePropertyInfo).GetMethod(text, BindingFlags.Static | BindingFlags.NonPublic);
			methodInfo = methodInfo.MakeGenericMethod(array);
			return (RuntimePropertyInfo.GetterAdapter)Delegate.CreateDelegate(typeof(RuntimePropertyInfo.GetterAdapter), obj, methodInfo, true);
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x000F5B74 File Offset: 0x000F3D74
		public override object GetValue(object obj, object[] index)
		{
			if (index == null || index.Length == 0)
			{
				if (this.cached_getter == null)
				{
					MethodInfo getMethod = this.GetGetMethod(true);
					if (getMethod == null)
					{
						throw new ArgumentException("Get Method not found for '" + this.Name + "'");
					}
					if (this.DeclaringType.IsValueType || this.PropertyType.IsByRef || getMethod.ContainsGenericParameters)
					{
						goto IL_0097;
					}
					this.cached_getter = RuntimePropertyInfo.CreateGetterDelegate(getMethod);
					try
					{
						return this.cached_getter(obj);
					}
					catch (Exception ex)
					{
						throw new TargetInvocationException(ex);
					}
				}
				try
				{
					return this.cached_getter(obj);
				}
				catch (Exception ex2)
				{
					throw new TargetInvocationException(ex2);
				}
			}
			IL_0097:
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x000F5C44 File Offset: 0x000F3E44
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			object obj2 = null;
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException("Get Method not found for '" + this.Name + "'");
			}
			try
			{
				if (index == null || index.Length == 0)
				{
					obj2 = getMethod.Invoke(obj, invokeAttr, binder, null, culture);
				}
				else
				{
					obj2 = getMethod.Invoke(obj, invokeAttr, binder, index, culture);
				}
			}
			catch (SecurityException ex)
			{
				throw new TargetInvocationException(ex);
			}
			return obj2;
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x000F5CC0 File Offset: 0x000F3EC0
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo setMethod = this.GetSetMethod(true);
			if (setMethod == null)
			{
				throw new ArgumentException("Set Method not found for '" + this.Name + "'");
			}
			object[] array;
			if (index == null || index.Length == 0)
			{
				array = new object[] { value };
			}
			else
			{
				int num = index.Length;
				array = new object[num + 1];
				index.CopyTo(array, 0);
				array[num] = value;
			}
			setMethod.Invoke(obj, invokeAttr, binder, array, culture);
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x000F5D38 File Offset: 0x000F3F38
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.GetCustomModifiers(true);
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x000F5D41 File Offset: 0x000F3F41
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.GetCustomModifiers(false);
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x000F5D4A File Offset: 0x000F3F4A
		private Type[] GetCustomModifiers(bool optional)
		{
			return RuntimePropertyInfo.GetTypeModifiers(this, optional) ?? Type.EmptyTypes;
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x000F3C24 File Offset: 0x000F1E24
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x000F5D5C File Offset: 0x000F3F5C
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimePropertyInfo>(other);
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06004E33 RID: 20019 RVA: 0x000F5D65 File Offset: 0x000F3F65
		public override int MetadataToken
		{
			get
			{
				return RuntimePropertyInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06004E34 RID: 20020
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimePropertyInfo monoProperty);

		// Token: 0x06004E35 RID: 20021
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PropertyInfo internal_from_handle_type(IntPtr event_handle, IntPtr type_handle);

		// Token: 0x06004E36 RID: 20022 RVA: 0x000F5D70 File Offset: 0x000F3F70
		internal static PropertyInfo GetPropertyFromHandle(RuntimePropertyHandle handle, RuntimeTypeHandle reflectedType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			PropertyInfo propertyInfo = RuntimePropertyInfo.internal_from_handle_type(handle.Value, reflectedType.Value);
			if (propertyInfo == null)
			{
				throw new ArgumentException("The property handle and the type handle are incompatible.");
			}
			return propertyInfo;
		}

		// Token: 0x04003079 RID: 12409
		internal IntPtr klass;

		// Token: 0x0400307A RID: 12410
		internal IntPtr prop;

		// Token: 0x0400307B RID: 12411
		private MonoPropertyInfo info;

		// Token: 0x0400307C RID: 12412
		private PInfo cached;

		// Token: 0x0400307D RID: 12413
		private RuntimePropertyInfo.GetterAdapter cached_getter;

		// Token: 0x02000902 RID: 2306
		// (Invoke) Token: 0x06004E39 RID: 20025
		private delegate object GetterAdapter(object _this);

		// Token: 0x02000903 RID: 2307
		// (Invoke) Token: 0x06004E3D RID: 20029
		private delegate R Getter<T, R>(T _this);

		// Token: 0x02000904 RID: 2308
		// (Invoke) Token: 0x06004E41 RID: 20033
		private delegate R StaticGetter<R>();
	}
}

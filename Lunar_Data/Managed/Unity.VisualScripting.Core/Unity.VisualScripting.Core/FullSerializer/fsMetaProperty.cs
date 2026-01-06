using System;
using System.Reflection;
using Unity.VisualScripting.FullSerializer.Internal;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x020001A7 RID: 423
	public class fsMetaProperty
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x00030094 File Offset: 0x0002E294
		internal fsMetaProperty(fsConfig config, FieldInfo field)
		{
			this._memberInfo = field;
			this.StorageType = field.FieldType;
			this.MemberName = field.Name;
			this.IsPublic = field.IsPublic;
			this.IsReadOnly = field.IsInitOnly;
			this.CanRead = true;
			this.CanWrite = true;
			this.CommonInitialize(config);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000300F4 File Offset: 0x0002E2F4
		internal fsMetaProperty(fsConfig config, PropertyInfo property)
		{
			this._memberInfo = property;
			this.StorageType = property.PropertyType;
			this.MemberName = property.Name;
			this.IsPublic = property.GetGetMethod() != null && property.GetGetMethod().IsPublic && property.GetSetMethod() != null && property.GetSetMethod().IsPublic;
			this.IsReadOnly = false;
			this.CanRead = property.CanRead;
			this.CanWrite = property.CanWrite;
			this.CommonInitialize(config);
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00030189 File Offset: 0x0002E389
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x00030191 File Offset: 0x0002E391
		public Type StorageType { get; private set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0003019A File Offset: 0x0002E39A
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x000301A2 File Offset: 0x0002E3A2
		public Type OverrideConverterType { get; private set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x000301AB File Offset: 0x0002E3AB
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x000301B3 File Offset: 0x0002E3B3
		public bool CanRead { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x000301BC File Offset: 0x0002E3BC
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x000301C4 File Offset: 0x0002E3C4
		public bool CanWrite { get; private set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x000301CD File Offset: 0x0002E3CD
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x000301D5 File Offset: 0x0002E3D5
		public string JsonName { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x000301DE File Offset: 0x0002E3DE
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x000301E6 File Offset: 0x0002E3E6
		public string MemberName { get; private set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x000301EF File Offset: 0x0002E3EF
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x000301F7 File Offset: 0x0002E3F7
		public bool IsPublic { get; private set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00030200 File Offset: 0x0002E400
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x00030208 File Offset: 0x0002E408
		public bool IsReadOnly { get; private set; }

		// Token: 0x06000B53 RID: 2899 RVA: 0x00030214 File Offset: 0x0002E414
		private void CommonInitialize(fsConfig config)
		{
			fsPropertyAttribute attribute = fsPortableReflection.GetAttribute<fsPropertyAttribute>(this._memberInfo);
			if (attribute != null)
			{
				this.JsonName = attribute.Name;
				this.OverrideConverterType = attribute.Converter;
			}
			if (string.IsNullOrEmpty(this.JsonName))
			{
				this.JsonName = config.GetJsonNameFromMemberName(this.MemberName, this._memberInfo);
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00030274 File Offset: 0x0002E474
		public void Write(object context, object value)
		{
			FieldInfo fieldInfo = this._memberInfo as FieldInfo;
			PropertyInfo propertyInfo = this._memberInfo as PropertyInfo;
			if (!(fieldInfo != null))
			{
				if (propertyInfo != null)
				{
					if (PlatformUtility.supportsJit)
					{
						if (propertyInfo.CanWrite)
						{
							propertyInfo.SetValueOptimized(context, value);
							return;
						}
					}
					else
					{
						MethodInfo setMethod = propertyInfo.GetSetMethod(true);
						if (setMethod != null)
						{
							setMethod.Invoke(context, new object[] { value });
						}
					}
				}
				return;
			}
			if (PlatformUtility.supportsJit)
			{
				fieldInfo.SetValueOptimized(context, value);
				return;
			}
			fieldInfo.SetValue(context, value);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x000302FF File Offset: 0x0002E4FF
		public object Read(object context)
		{
			if (this._memberInfo is PropertyInfo)
			{
				return ((PropertyInfo)this._memberInfo).GetValue(context, null);
			}
			return ((FieldInfo)this._memberInfo).GetValue(context);
		}

		// Token: 0x040002B2 RID: 690
		internal MemberInfo _memberInfo;
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020008F4 RID: 2292
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class RuntimeEventInfo : EventInfo, ISerializable
	{
		// Token: 0x06004CFA RID: 19706
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_event_info(RuntimeEventInfo ev, out MonoEventInfo info);

		// Token: 0x06004CFB RID: 19707 RVA: 0x000F3998 File Offset: 0x000F1B98
		internal static MonoEventInfo GetEventInfo(RuntimeEventInfo ev)
		{
			MonoEventInfo monoEventInfo;
			RuntimeEventInfo.get_event_info(ev, out monoEventInfo);
			return monoEventInfo;
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004CFC RID: 19708 RVA: 0x000F39AE File Offset: 0x000F1BAE
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004CFD RID: 19709 RVA: 0x000F39B6 File Offset: 0x000F1BB6
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.GetBindingFlags();
			}
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x000F39BE File Offset: 0x000F1BBE
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004CFF RID: 19711 RVA: 0x000F39CB File Offset: 0x000F1BCB
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x000F39D8 File Offset: 0x000F1BD8
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x000F39E5 File Offset: 0x000F1BE5
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, null, MemberTypes.Event);
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x000F3A0C File Offset: 0x000F1C0C
		internal BindingFlags GetBindingFlags()
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			MethodInfo methodInfo = eventInfo.add_method;
			if (methodInfo == null)
			{
				methodInfo = eventInfo.remove_method;
			}
			if (methodInfo == null)
			{
				methodInfo = eventInfo.raise_method;
			}
			return RuntimeType.FilterPreCalculate(methodInfo != null && methodInfo.IsPublic, this.GetDeclaringTypeInternal() != this.ReflectedType, methodInfo != null && methodInfo.IsStatic);
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004D03 RID: 19715 RVA: 0x000F3A81 File Offset: 0x000F1C81
		public override EventAttributes Attributes
		{
			get
			{
				return RuntimeEventInfo.GetEventInfo(this).attrs;
			}
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x000F3A90 File Offset: 0x000F1C90
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.add_method != null && eventInfo.add_method.IsPublic))
			{
				return eventInfo.add_method;
			}
			return null;
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x000F3ACC File Offset: 0x000F1CCC
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.raise_method != null && eventInfo.raise_method.IsPublic))
			{
				return eventInfo.raise_method;
			}
			return null;
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x000F3B08 File Offset: 0x000F1D08
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.remove_method != null && eventInfo.remove_method.IsPublic))
			{
				return eventInfo.remove_method;
			}
			return null;
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x000F3B44 File Offset: 0x000F1D44
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			if (nonPublic)
			{
				return eventInfo.other_methods;
			}
			int num = 0;
			MethodInfo[] array = eventInfo.other_methods;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].IsPublic)
				{
					num++;
				}
			}
			if (num == eventInfo.other_methods.Length)
			{
				return eventInfo.other_methods;
			}
			MethodInfo[] array2 = new MethodInfo[num];
			num = 0;
			foreach (MethodInfo methodInfo in eventInfo.other_methods)
			{
				if (methodInfo.IsPublic)
				{
					array2[num++] = methodInfo;
				}
			}
			return array2;
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004D08 RID: 19720 RVA: 0x000F3BD9 File Offset: 0x000F1DD9
		public override Type DeclaringType
		{
			get
			{
				return RuntimeEventInfo.GetEventInfo(this).declaring_type;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004D09 RID: 19721 RVA: 0x000F3BE6 File Offset: 0x000F1DE6
		public override Type ReflectedType
		{
			get
			{
				return RuntimeEventInfo.GetEventInfo(this).reflected_type;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004D0A RID: 19722 RVA: 0x000F3BF3 File Offset: 0x000F1DF3
		public override string Name
		{
			get
			{
				return RuntimeEventInfo.GetEventInfo(this).name;
			}
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x000F3C00 File Offset: 0x000F1E00
		public override string ToString()
		{
			Type eventHandlerType = this.EventHandlerType;
			return ((eventHandlerType != null) ? eventHandlerType.ToString() : null) + " " + this.Name;
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x00052A6A File Offset: 0x00050C6A
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x000F18F1 File Offset: 0x000EFAF1
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x000F18FA File Offset: 0x000EFAFA
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x000F3C24 File Offset: 0x000F1E24
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004D10 RID: 19728 RVA: 0x000F3C2C File Offset: 0x000F1E2C
		public override int MetadataToken
		{
			get
			{
				return RuntimeEventInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x000F3C34 File Offset: 0x000F1E34
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeEventInfo>(other);
		}

		// Token: 0x06004D12 RID: 19730
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeEventInfo monoEvent);

		// Token: 0x0400304E RID: 12366
		private IntPtr klass;

		// Token: 0x0400304F RID: 12367
		private IntPtr handle;
	}
}

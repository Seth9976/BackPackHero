using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000916 RID: 2326
	[StructLayout(LayoutKind.Sequential)]
	internal class ConstructorOnTypeBuilderInst : ConstructorInfo
	{
		// Token: 0x06004F1E RID: 20254 RVA: 0x000F859E File Offset: 0x000F679E
		public ConstructorOnTypeBuilderInst(TypeBuilderInstantiation instantiation, ConstructorInfo cb)
		{
			this.instantiation = instantiation;
			this.cb = cb;
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06004F1F RID: 20255 RVA: 0x000F85B4 File Offset: 0x000F67B4
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004F20 RID: 20256 RVA: 0x000F85BC File Offset: 0x000F67BC
		public override string Name
		{
			get
			{
				return this.cb.Name;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004F21 RID: 20257 RVA: 0x000F85B4 File Offset: 0x000F67B4
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06004F22 RID: 20258 RVA: 0x000F85C9 File Offset: 0x000F67C9
		public override Module Module
		{
			get
			{
				return this.cb.Module;
			}
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x000F85D6 File Offset: 0x000F67D6
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.cb.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x000F85E5 File Offset: 0x000F67E5
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.cb.GetCustomAttributes(inherit);
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x000F85F3 File Offset: 0x000F67F3
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.cb.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x000F8602 File Offset: 0x000F6802
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.cb.GetMethodImplementationFlags();
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x000F860F File Offset: 0x000F680F
		public override ParameterInfo[] GetParameters()
		{
			if (!this.instantiation.IsCreated)
			{
				throw new NotSupportedException();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x000F862C File Offset: 0x000F682C
		internal override ParameterInfo[] GetParametersInternal()
		{
			ParameterInfo[] array;
			if (this.cb is ConstructorBuilder)
			{
				ConstructorBuilder constructorBuilder = (ConstructorBuilder)this.cb;
				array = new ParameterInfo[constructorBuilder.parameters.Length];
				for (int i = 0; i < constructorBuilder.parameters.Length; i++)
				{
					Type type = this.instantiation.InflateType(constructorBuilder.parameters[i]);
					ParameterInfo[] array2 = array;
					int num = i;
					ParameterBuilder[] pinfo = constructorBuilder.pinfo;
					array2[num] = RuntimeParameterInfo.New((pinfo != null) ? pinfo[i] : null, type, this, i + 1);
				}
			}
			else
			{
				ParameterInfo[] parameters = this.cb.GetParameters();
				array = new ParameterInfo[parameters.Length];
				for (int j = 0; j < parameters.Length; j++)
				{
					Type type2 = this.instantiation.InflateType(parameters[j].ParameterType);
					array[j] = RuntimeParameterInfo.New(parameters[j], type2, this, j + 1);
				}
			}
			return array;
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x000F86FC File Offset: 0x000F68FC
		internal override Type[] GetParameterTypes()
		{
			if (this.cb is ConstructorBuilder)
			{
				return (this.cb as ConstructorBuilder).parameters;
			}
			ParameterInfo[] parameters = this.cb.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return array;
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x000F8756 File Offset: 0x000F6956
		internal ConstructorInfo RuntimeResolve()
		{
			return this.instantiation.InternalResolve().GetConstructor(this.cb);
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06004F2B RID: 20267 RVA: 0x000F876E File Offset: 0x000F696E
		public override int MetadataToken
		{
			get
			{
				return base.MetadataToken;
			}
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x000F8776 File Offset: 0x000F6976
		internal override int GetParametersCount()
		{
			return this.cb.GetParametersCount();
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x000F8783 File Offset: 0x000F6983
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.cb.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06004F2E RID: 20270 RVA: 0x000F8797 File Offset: 0x000F6997
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.cb.MethodHandle;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06004F2F RID: 20271 RVA: 0x000F87A4 File Offset: 0x000F69A4
		public override MethodAttributes Attributes
		{
			get
			{
				return this.cb.Attributes;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06004F30 RID: 20272 RVA: 0x000F87B1 File Offset: 0x000F69B1
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.cb.CallingConvention;
			}
		}

		// Token: 0x06004F31 RID: 20273 RVA: 0x000F87BE File Offset: 0x000F69BE
		public override Type[] GetGenericArguments()
		{
			return this.cb.GetGenericArguments();
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06004F32 RID: 20274 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06004F33 RID: 20275 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06004F34 RID: 20276 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004F35 RID: 20277 RVA: 0x00084B61 File Offset: 0x00082D61
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04003127 RID: 12583
		internal TypeBuilderInstantiation instantiation;

		// Token: 0x04003128 RID: 12584
		internal ConstructorInfo cb;
	}
}

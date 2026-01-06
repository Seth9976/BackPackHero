using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000925 RID: 2341
	[StructLayout(LayoutKind.Sequential)]
	internal class FieldOnTypeBuilderInst : FieldInfo
	{
		// Token: 0x0600502E RID: 20526 RVA: 0x000FAE18 File Offset: 0x000F9018
		public FieldOnTypeBuilderInst(TypeBuilderInstantiation instantiation, FieldInfo fb)
		{
			this.instantiation = instantiation;
			this.fb = fb;
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x0600502F RID: 20527 RVA: 0x000FAE2E File Offset: 0x000F902E
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x000FAE36 File Offset: 0x000F9036
		public override string Name
		{
			get
			{
				return this.fb.Name;
			}
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x000FAE2E File Offset: 0x000F902E
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x000472CC File Offset: 0x000454CC
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x000472CC File Offset: 0x000454CC
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x000472CC File Offset: 0x000454CC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x000FAE43 File Offset: 0x000F9043
		public override string ToString()
		{
			return this.fb.FieldType.ToString() + " " + this.Name;
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06005036 RID: 20534 RVA: 0x000FAE65 File Offset: 0x000F9065
		public override FieldAttributes Attributes
		{
			get
			{
				return this.fb.Attributes;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06005037 RID: 20535 RVA: 0x000472CC File Offset: 0x000454CC
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06005038 RID: 20536 RVA: 0x00084B61 File Offset: 0x00082D61
		public override int MetadataToken
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06005039 RID: 20537 RVA: 0x000472CC File Offset: 0x000454CC
		public override Type FieldType
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x000472CC File Offset: 0x000454CC
		public override object GetValue(object obj)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x000472CC File Offset: 0x000454CC
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x000FAE72 File Offset: 0x000F9072
		internal FieldInfo RuntimeResolve()
		{
			return this.instantiation.RuntimeResolve().GetField(this.fb);
		}

		// Token: 0x04003169 RID: 12649
		internal TypeBuilderInstantiation instantiation;

		// Token: 0x0400316A RID: 12650
		internal FieldInfo fb;
	}
}

using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C2 RID: 706
	public class UxmlTemplateFactory : UxmlFactory<VisualElement, UxmlTemplateTraits>
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x0006008C File Offset: 0x0005E28C
		public override string uxmlName
		{
			get
			{
				return "Template";
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x00060093 File Offset: 0x0005E293
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0005FF17 File Offset: 0x0005E117
		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0005FF28 File Offset: 0x0005E128
		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x0005FF42 File Offset: 0x0005E142
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x000600AC File Offset: 0x0005E2AC
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x04000A15 RID: 2581
		internal const string k_ElementName = "Template";
	}
}

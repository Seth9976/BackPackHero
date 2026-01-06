using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C5 RID: 709
	public class UxmlAttributeOverridesFactory : UxmlFactory<VisualElement, UxmlAttributeOverridesTraits>
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x000601EC File Offset: 0x0005E3EC
		public override string uxmlName
		{
			get
			{
				return "AttributeOverrides";
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000601F3 File Offset: 0x0005E3F3
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0005FF17 File Offset: 0x0005E117
		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x0005FF28 File Offset: 0x0005E128
		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0005FF42 File Offset: 0x0005E142
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0006020C File Offset: 0x0005E40C
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x04000A1D RID: 2589
		internal const string k_ElementName = "AttributeOverrides";
	}
}

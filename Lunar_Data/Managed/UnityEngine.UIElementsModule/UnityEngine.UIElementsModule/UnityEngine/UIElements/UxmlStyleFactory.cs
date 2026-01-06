using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BF RID: 703
	public class UxmlStyleFactory : UxmlFactory<VisualElement, UxmlStyleTraits>
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0005FEF8 File Offset: 0x0005E0F8
		public override string uxmlName
		{
			get
			{
				return "Style";
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x0005FEFF File Offset: 0x0005E0FF
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0005FF17 File Offset: 0x0005E117
		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x0005FF28 File Offset: 0x0005E128
		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x0005FF42 File Offset: 0x0005E142
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0005FF54 File Offset: 0x0005E154
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x04000A0D RID: 2573
		internal const string k_ElementName = "Style";
	}
}

using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E6 RID: 742
	public interface IUxmlFactory
	{
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600186B RID: 6251
		string uxmlName { get; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600186C RID: 6252
		string uxmlNamespace { get; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600186D RID: 6253
		string uxmlQualifiedName { get; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600186E RID: 6254
		bool canHaveAnyAttribute { get; }

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600186F RID: 6255
		IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription { get; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001870 RID: 6256
		IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription { get; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001871 RID: 6257
		string substituteForTypeName { get; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001872 RID: 6258
		string substituteForTypeNamespace { get; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001873 RID: 6259
		string substituteForTypeQualifiedName { get; }

		// Token: 0x06001874 RID: 6260
		bool AcceptsAttributeBag(IUxmlAttributes bag, CreationContext cc);

		// Token: 0x06001875 RID: 6261
		VisualElement Create(IUxmlAttributes bag, CreationContext cc);
	}
}

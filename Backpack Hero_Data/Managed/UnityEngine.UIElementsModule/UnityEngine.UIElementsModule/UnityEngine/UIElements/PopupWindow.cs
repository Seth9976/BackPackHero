using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000157 RID: 343
	public class PopupWindow : TextElement
	{
		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002C498 File Offset: 0x0002A698
		public PopupWindow()
		{
			base.AddToClassList(PopupWindow.ussClassName);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.AddToClassList(PopupWindow.contentUssClassName);
			base.hierarchy.Add(this.m_ContentContainer);
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0002C4F8 File Offset: 0x0002A6F8
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x040004EC RID: 1260
		private VisualElement m_ContentContainer;

		// Token: 0x040004ED RID: 1261
		public new static readonly string ussClassName = "unity-popup-window";

		// Token: 0x040004EE RID: 1262
		public static readonly string contentUssClassName = PopupWindow.ussClassName + "__content-container";

		// Token: 0x02000158 RID: 344
		public new class UxmlFactory : UxmlFactory<PopupWindow, PopupWindow.UxmlTraits>
		{
		}

		// Token: 0x02000159 RID: 345
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x17000223 RID: 547
			// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0002C53C File Offset: 0x0002A73C
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield return new UxmlChildElementDescription(typeof(VisualElement));
					yield break;
				}
			}
		}
	}
}

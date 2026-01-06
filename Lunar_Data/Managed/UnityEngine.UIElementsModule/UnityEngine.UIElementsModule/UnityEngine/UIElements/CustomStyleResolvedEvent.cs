using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000223 RID: 547
	public class CustomStyleResolvedEvent : EventBase<CustomStyleResolvedEvent>
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0004014C File Offset: 0x0003E34C
		public ICustomStyle customStyle
		{
			get
			{
				VisualElement visualElement = base.target as VisualElement;
				return (visualElement != null) ? visualElement.customStyle : null;
			}
		}
	}
}

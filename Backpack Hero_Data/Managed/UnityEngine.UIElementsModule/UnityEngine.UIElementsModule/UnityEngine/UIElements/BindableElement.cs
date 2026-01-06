using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200000A RID: 10
	public class BindableElement : VisualElement, IBindable
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002AA7 File Offset: 0x00000CA7
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002AAF File Offset: 0x00000CAF
		public IBinding binding { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002AB8 File Offset: 0x00000CB8
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002AC0 File Offset: 0x00000CC0
		public string bindingPath { get; set; }

		// Token: 0x0200000B RID: 11
		public new class UxmlFactory : UxmlFactory<BindableElement, BindableElement.UxmlTraits>
		{
		}

		// Token: 0x0200000C RID: 12
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x0600003A RID: 58 RVA: 0x00002ADB File Offset: 0x00000CDB
			public UxmlTraits()
			{
				this.m_PropertyPath = new UxmlStringAttributeDescription
				{
					name = "binding-path"
				};
			}

			// Token: 0x0600003B RID: 59 RVA: 0x00002AFC File Offset: 0x00000CFC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				string valueFromBag = this.m_PropertyPath.GetValueFromBag(bag, cc);
				bool flag = !string.IsNullOrEmpty(valueFromBag);
				if (flag)
				{
					IBindable bindable = ve as IBindable;
					bool flag2 = bindable != null;
					if (flag2)
					{
						bindable.bindingPath = valueFromBag;
					}
				}
			}

			// Token: 0x0400001A RID: 26
			private UxmlStringAttributeDescription m_PropertyPath;
		}
	}
}

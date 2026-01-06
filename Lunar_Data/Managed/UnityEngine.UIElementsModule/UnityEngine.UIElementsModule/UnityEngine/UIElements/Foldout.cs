using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000135 RID: 309
	public class Foldout : BindableElement, INotifyValueChanged<bool>
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00027644 File Offset: 0x00025844
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_Container;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002764C File Offset: 0x0002584C
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x00027659 File Offset: 0x00025859
		public string text
		{
			get
			{
				return this.m_Toggle.text;
			}
			set
			{
				this.m_Toggle.text = value;
				VisualElement visualElement = this.m_Toggle.visualInput.Q(null, Toggle.textUssClassName);
				if (visualElement != null)
				{
					visualElement.AddToClassList(Foldout.textUssClassName);
				}
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00027690 File Offset: 0x00025890
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x00027698 File Offset: 0x00025898
		public bool value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = this.m_Value == value;
				if (!flag)
				{
					using (ChangeEvent<bool> pooled = ChangeEvent<bool>.GetPooled(this.m_Value, value))
					{
						pooled.target = this;
						this.SetValueWithoutNotify(value);
						this.SendEvent(pooled);
						base.SaveViewData();
					}
				}
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00027700 File Offset: 0x00025900
		public void SetValueWithoutNotify(bool newValue)
		{
			this.m_Value = newValue;
			this.m_Toggle.value = this.m_Value;
			this.contentContainer.style.display = (newValue ? DisplayStyle.Flex : DisplayStyle.None);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002773C File Offset: 0x0002593C
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			string fullHierarchicalViewDataKey = base.GetFullHierarchicalViewDataKey();
			base.OverwriteFromViewData(this, fullHierarchicalViewDataKey);
			this.SetValueWithoutNotify(this.m_Value);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00027770 File Offset: 0x00025970
		public Foldout()
		{
			this.m_Value = true;
			base.AddToClassList(Foldout.ussClassName);
			this.m_Toggle = new Toggle
			{
				value = true
			};
			this.m_Toggle.RegisterValueChangedCallback(delegate(ChangeEvent<bool> evt)
			{
				this.value = this.m_Toggle.value;
				evt.StopPropagation();
			});
			this.m_Toggle.AddToClassList(Foldout.toggleUssClassName);
			this.m_Toggle.visualInput.AddToClassList(Foldout.inputUssClassName);
			this.m_Toggle.visualInput.Q(null, Toggle.checkmarkUssClassName).AddToClassList(Foldout.checkmarkUssClassName);
			base.hierarchy.Add(this.m_Toggle);
			this.m_Container = new VisualElement
			{
				name = "unity-content"
			};
			this.m_Container.AddToClassList(Foldout.contentUssClassName);
			base.hierarchy.Add(this.m_Container);
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00027874 File Offset: 0x00025A74
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			for (int i = 0; i <= Foldout.ussFoldoutMaxDepth; i++)
			{
				base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + i.ToString());
			}
			base.RemoveFromClassList(Foldout.ussFoldoutDepthClassName + "max");
			int foldoutDepth = this.GetFoldoutDepth();
			bool flag = foldoutDepth > Foldout.ussFoldoutMaxDepth;
			if (flag)
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + "max");
			}
			else
			{
				base.AddToClassList(Foldout.ussFoldoutDepthClassName + foldoutDepth.ToString());
			}
		}

		// Token: 0x04000448 RID: 1096
		private Toggle m_Toggle;

		// Token: 0x04000449 RID: 1097
		private VisualElement m_Container;

		// Token: 0x0400044A RID: 1098
		[SerializeField]
		private bool m_Value;

		// Token: 0x0400044B RID: 1099
		public static readonly string ussClassName = "unity-foldout";

		// Token: 0x0400044C RID: 1100
		public static readonly string toggleUssClassName = Foldout.ussClassName + "__toggle";

		// Token: 0x0400044D RID: 1101
		public static readonly string contentUssClassName = Foldout.ussClassName + "__content";

		// Token: 0x0400044E RID: 1102
		public static readonly string inputUssClassName = Foldout.ussClassName + "__input";

		// Token: 0x0400044F RID: 1103
		public static readonly string checkmarkUssClassName = Foldout.ussClassName + "__checkmark";

		// Token: 0x04000450 RID: 1104
		public static readonly string textUssClassName = Foldout.ussClassName + "__text";

		// Token: 0x04000451 RID: 1105
		internal static readonly string ussFoldoutDepthClassName = Foldout.ussClassName + "--depth-";

		// Token: 0x04000452 RID: 1106
		internal static readonly int ussFoldoutMaxDepth = 4;

		// Token: 0x02000136 RID: 310
		public new class UxmlFactory : UxmlFactory<Foldout, Foldout.UxmlTraits>
		{
		}

		// Token: 0x02000137 RID: 311
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x06000A27 RID: 2599 RVA: 0x000279CC File Offset: 0x00025BCC
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Foldout foldout = ve as Foldout;
				bool flag = foldout != null;
				if (flag)
				{
					foldout.text = this.m_Text.GetValueFromBag(bag, cc);
					foldout.SetValueWithoutNotify(this.m_Value.GetValueFromBag(bag, cc));
				}
			}

			// Token: 0x04000453 RID: 1107
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};

			// Token: 0x04000454 RID: 1108
			private UxmlBoolAttributeDescription m_Value = new UxmlBoolAttributeDescription
			{
				name = "value",
				defaultValue = true
			};
		}
	}
}

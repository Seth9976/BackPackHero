using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000116 RID: 278
	public abstract class BaseBoolField : BaseField<bool>
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x0002271C File Offset: 0x0002091C
		public BaseBoolField(string label)
			: base(label, null)
		{
			this.m_CheckMark = new VisualElement
			{
				name = "unity-checkmark",
				pickingMode = PickingMode.Ignore
			};
			base.visualInput.Add(this.m_CheckMark);
			base.visualInput.pickingMode = PickingMode.Position;
			this.text = null;
			this.AddManipulator(this.m_Clickable = new Clickable(new Action<EventBase>(this.OnClickEvent)));
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x000227C2 File Offset: 0x000209C2
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.ToggleValue();
			evt.StopPropagation();
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000227D4 File Offset: 0x000209D4
		private void OnKeyDown(KeyDownEvent evt)
		{
			IPanel panel = base.panel;
			bool flag = panel == null || panel.contextType != ContextType.Editor;
			if (!flag)
			{
				bool flag2 = evt.keyCode == KeyCode.KeypadEnter || evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.Space;
				if (flag2)
				{
					this.ToggleValue();
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00022838 File Offset: 0x00020A38
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x0002285C File Offset: 0x00020A5C
		public string text
		{
			get
			{
				Label label = this.m_Label;
				return (label != null) ? label.text : null;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					bool flag2 = this.m_Label == null;
					if (flag2)
					{
						this.InitLabel();
					}
					this.m_Label.text = value;
				}
				else
				{
					bool flag3 = this.m_Label != null;
					if (flag3)
					{
						this.m_Label.RemoveFromHierarchy();
						this.m_Label = null;
					}
				}
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000228C0 File Offset: 0x00020AC0
		protected virtual void InitLabel()
		{
			this.m_Label = new Label
			{
				pickingMode = PickingMode.Ignore
			};
			base.visualInput.Add(this.m_Label);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000228E8 File Offset: 0x00020AE8
		public override void SetValueWithoutNotify(bool newValue)
		{
			if (newValue)
			{
				base.visualInput.pseudoStates |= PseudoStates.Checked;
				base.pseudoStates |= PseudoStates.Checked;
			}
			else
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
			}
			base.SetValueWithoutNotify(newValue);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00022954 File Offset: 0x00020B54
		private void OnClickEvent(EventBase evt)
		{
			bool flag = evt.eventTypeId == EventBase<MouseUpEvent>.TypeId();
			if (flag)
			{
				IMouseEvent mouseEvent = (IMouseEvent)evt;
				bool flag2 = mouseEvent.button == 0;
				if (flag2)
				{
					this.ToggleValue();
				}
			}
			else
			{
				bool flag3 = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId() || evt.eventTypeId == EventBase<ClickEvent>.TypeId();
				if (flag3)
				{
					IPointerEvent pointerEvent = (IPointerEvent)evt;
					bool flag4 = pointerEvent.button == 0;
					if (flag4)
					{
						this.ToggleValue();
					}
				}
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000229D8 File Offset: 0x00020BD8
		protected virtual void ToggleValue()
		{
			this.value = !this.value;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000229EC File Offset: 0x00020BEC
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
				this.m_CheckMark.RemoveFromHierarchy();
				base.visualInput.Add(base.mixedValueLabel);
				this.m_OriginalText = this.text;
				this.text = "";
			}
			else
			{
				base.mixedValueLabel.RemoveFromHierarchy();
				base.visualInput.Add(this.m_CheckMark);
				bool flag = this.m_OriginalText != null;
				if (flag)
				{
					this.text = this.m_OriginalText;
				}
			}
		}

		// Token: 0x040003AD RID: 941
		protected Label m_Label;

		// Token: 0x040003AE RID: 942
		protected readonly VisualElement m_CheckMark;

		// Token: 0x040003AF RID: 943
		internal Clickable m_Clickable;

		// Token: 0x040003B0 RID: 944
		private string m_OriginalText;
	}
}

using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000129 RID: 297
	public class Button : TextElement
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x000268F8 File Offset: 0x00024AF8
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00026910 File Offset: 0x00024B10
		public Clickable clickable
		{
			get
			{
				return this.m_Clickable;
			}
			set
			{
				bool flag = this.m_Clickable != null && this.m_Clickable.target == this;
				if (flag)
				{
					this.RemoveManipulator(this.m_Clickable);
				}
				this.m_Clickable = value;
				bool flag2 = this.m_Clickable != null;
				if (flag2)
				{
					this.AddManipulator(this.m_Clickable);
				}
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060009DE RID: 2526 RVA: 0x0002696D File Offset: 0x00024B6D
		// (remove) Token: 0x060009DF RID: 2527 RVA: 0x00026978 File Offset: 0x00024B78
		[Obsolete("onClick is obsolete. Use clicked instead (UnityUpgradable) -> clicked", true)]
		public event Action onClick
		{
			add
			{
				this.clicked += value;
			}
			remove
			{
				this.clicked -= value;
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060009E0 RID: 2528 RVA: 0x00026984 File Offset: 0x00024B84
		// (remove) Token: 0x060009E1 RID: 2529 RVA: 0x000269C0 File Offset: 0x00024BC0
		public event Action clicked
		{
			add
			{
				bool flag = this.m_Clickable == null;
				if (flag)
				{
					this.clickable = new Clickable(value);
				}
				else
				{
					this.m_Clickable.clicked += value;
				}
			}
			remove
			{
				bool flag = this.m_Clickable != null;
				if (flag)
				{
					this.m_Clickable.clicked -= value;
				}
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000269EA File Offset: 0x00024BEA
		public Button()
			: this(null)
		{
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x000269F8 File Offset: 0x00024BF8
		public Button(Action clickEvent)
		{
			base.AddToClassList(Button.ussClassName);
			this.clickable = new Clickable(clickEvent);
			base.focusable = true;
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00026A56 File Offset: 0x00024C56
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			Clickable clickable = this.clickable;
			if (clickable != null)
			{
				clickable.SimulateSingleClick(evt, 100);
			}
			evt.StopPropagation();
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00026A78 File Offset: 0x00024C78
		private void OnKeyDown(KeyDownEvent evt)
		{
			IPanel panel = base.panel;
			bool flag = panel == null || panel.contextType != ContextType.Editor;
			if (!flag)
			{
				bool flag2 = evt.keyCode == KeyCode.KeypadEnter || evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.Space;
				if (flag2)
				{
					Clickable clickable = this.clickable;
					if (clickable != null)
					{
						clickable.SimulateSingleClick(evt, 100);
					}
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00026AEC File Offset: 0x00024CEC
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			string text = this.text;
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				text = Button.NonEmptyString;
			}
			return base.MeasureTextSize(text, desiredWidth, widthMode, desiredHeight, heightMode);
		}

		// Token: 0x04000428 RID: 1064
		public new static readonly string ussClassName = "unity-button";

		// Token: 0x04000429 RID: 1065
		private Clickable m_Clickable;

		// Token: 0x0400042A RID: 1066
		private static readonly string NonEmptyString = " ";

		// Token: 0x0200012A RID: 298
		public new class UxmlFactory : UxmlFactory<Button, Button.UxmlTraits>
		{
		}

		// Token: 0x0200012B RID: 299
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x060009E9 RID: 2537 RVA: 0x00026B42 File Offset: 0x00024D42
			public UxmlTraits()
			{
				base.focusable.defaultValue = true;
			}
		}
	}
}

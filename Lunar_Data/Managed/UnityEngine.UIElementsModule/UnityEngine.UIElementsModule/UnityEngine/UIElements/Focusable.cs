using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002C RID: 44
	public abstract class Focusable : CallbackEventHandler
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00005AE4 File Offset: 0x00003CE4
		protected Focusable()
		{
			this.focusable = true;
			this.tabIndex = 0;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010A RID: 266
		public abstract FocusController focusController { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005B05 File Offset: 0x00003D05
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00005B0D File Offset: 0x00003D0D
		public bool focusable { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005B16 File Offset: 0x00003D16
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00005B1E File Offset: 0x00003D1E
		public int tabIndex { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005B28 File Offset: 0x00003D28
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00005B40 File Offset: 0x00003D40
		public bool delegatesFocus
		{
			get
			{
				return this.m_DelegatesFocus;
			}
			set
			{
				bool flag = !((VisualElement)this).isCompositeRoot;
				if (flag)
				{
					throw new InvalidOperationException("delegatesFocus should only be set on composite roots.");
				}
				this.m_DelegatesFocus = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005B74 File Offset: 0x00003D74
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00005B8C File Offset: 0x00003D8C
		internal bool excludeFromFocusRing
		{
			get
			{
				return this.m_ExcludeFromFocusRing;
			}
			set
			{
				bool flag = !((VisualElement)this).isCompositeRoot;
				if (flag)
				{
					throw new InvalidOperationException("excludeFromFocusRing should only be set on composite roots.");
				}
				this.m_ExcludeFromFocusRing = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005BBF File Offset: 0x00003DBF
		public virtual bool canGrabFocus
		{
			get
			{
				return this.focusable;
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public virtual void Focus()
		{
			bool flag = this.focusController != null;
			if (flag)
			{
				bool canGrabFocus = this.canGrabFocus;
				if (canGrabFocus)
				{
					Focusable focusDelegate = this.GetFocusDelegate();
					this.focusController.SwitchFocus(focusDelegate, this != focusDelegate, DispatchMode.Default);
				}
				else
				{
					this.focusController.SwitchFocus(null, false, DispatchMode.Default);
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005C20 File Offset: 0x00003E20
		public virtual void Blur()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.Blur(this, false, DispatchMode.Default);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005C38 File Offset: 0x00003E38
		internal void BlurImmediately()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.Blur(this, false, DispatchMode.Immediate);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005C50 File Offset: 0x00003E50
		private Focusable GetFocusDelegate()
		{
			Focusable focusable = this;
			while (focusable != null && focusable.delegatesFocus)
			{
				focusable = Focusable.GetFirstFocusableChild(focusable as VisualElement);
			}
			return focusable;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005C88 File Offset: 0x00003E88
		private static Focusable GetFirstFocusableChild(VisualElement ve)
		{
			int childCount = ve.hierarchy.childCount;
			int i = 0;
			while (i < childCount)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = visualElement.canGrabFocus && visualElement.tabIndex >= 0;
				if (!flag)
				{
					bool flag2 = visualElement.hierarchy.parent != null && visualElement == visualElement.hierarchy.parent.contentContainer;
					bool flag3 = !visualElement.isCompositeRoot && !flag2;
					if (flag3)
					{
						Focusable firstFocusableChild = Focusable.GetFirstFocusableChild(visualElement);
						bool flag4 = firstFocusableChild != null;
						if (flag4)
						{
							return firstFocusableChild;
						}
					}
					i++;
					continue;
				}
				return visualElement;
			}
			return null;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005D56 File Offset: 0x00003F56
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			this.ProcessEvent(evt);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005D69 File Offset: 0x00003F69
		internal override void ExecuteDefaultActionDisabled(EventBase evt)
		{
			base.ExecuteDefaultActionDisabled(evt);
			this.ProcessEvent(evt);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005D7C File Offset: 0x00003F7C
		private void ProcessEvent(EventBase evt)
		{
			bool flag = evt != null && evt.target == evt.leafTarget;
			if (flag)
			{
				FocusController focusController = this.focusController;
				if (focusController != null)
				{
					focusController.SwitchFocusOnEvent(evt);
				}
			}
		}

		// Token: 0x0400007E RID: 126
		private bool m_DelegatesFocus;

		// Token: 0x0400007F RID: 127
		private bool m_ExcludeFromFocusRing;

		// Token: 0x04000080 RID: 128
		internal bool isIMGUIContainer = false;
	}
}

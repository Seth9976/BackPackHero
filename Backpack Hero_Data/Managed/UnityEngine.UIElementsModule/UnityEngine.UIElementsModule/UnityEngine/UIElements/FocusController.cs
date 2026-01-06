using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200002F RID: 47
	public class FocusController
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00005E37 File Offset: 0x00004037
		public FocusController(IFocusRing focusRing)
		{
			this.focusRing = focusRing;
			this.imguiKeyboardControl = 0;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005E62 File Offset: 0x00004062
		private IFocusRing focusRing { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005E6C File Offset: 0x0000406C
		public Focusable focusedElement
		{
			get
			{
				Focusable retargetedFocusedElement = this.GetRetargetedFocusedElement(null);
				return this.IsLocalElement(retargetedFocusedElement) ? retargetedFocusedElement : null;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005E94 File Offset: 0x00004094
		internal bool IsFocused(Focusable f)
		{
			bool flag = !this.IsLocalElement(f);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				foreach (FocusController.FocusedElement focusedElement in this.m_FocusedElements)
				{
					bool flag3 = focusedElement.m_FocusedElement == f;
					if (flag3)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005F10 File Offset: 0x00004110
		internal Focusable GetRetargetedFocusedElement(VisualElement retargetAgainst)
		{
			VisualElement visualElement = ((retargetAgainst != null) ? retargetAgainst.hierarchy.parent : null);
			bool flag = visualElement == null;
			if (flag)
			{
				bool flag2 = this.m_FocusedElements.Count > 0;
				if (flag2)
				{
					return this.m_FocusedElements[this.m_FocusedElements.Count - 1].m_FocusedElement;
				}
			}
			else
			{
				while (!visualElement.isCompositeRoot && visualElement.hierarchy.parent != null)
				{
					visualElement = visualElement.hierarchy.parent;
				}
				foreach (FocusController.FocusedElement focusedElement in this.m_FocusedElements)
				{
					bool flag3 = focusedElement.m_SubTreeRoot == visualElement;
					if (flag3)
					{
						return focusedElement.m_FocusedElement;
					}
				}
			}
			return null;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006018 File Offset: 0x00004218
		internal Focusable GetLeafFocusedElement()
		{
			bool flag = this.m_FocusedElements.Count > 0;
			Focusable focusable;
			if (flag)
			{
				Focusable focusedElement = this.m_FocusedElements[0].m_FocusedElement;
				focusable = (this.IsLocalElement(focusedElement) ? focusedElement : null);
			}
			else
			{
				focusable = null;
			}
			return focusable;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006060 File Offset: 0x00004260
		private bool IsLocalElement(Focusable f)
		{
			return ((f != null) ? f.focusController : null) == this;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006081 File Offset: 0x00004281
		internal void ClearPendingFocusEvents()
		{
			this.m_PendingFocusCount = 0;
			this.m_LastPendingFocusedElement = null;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006094 File Offset: 0x00004294
		internal bool IsPendingFocus(Focusable f)
		{
			for (VisualElement visualElement = this.m_LastPendingFocusedElement as VisualElement; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				bool flag = f == visualElement;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000060DC File Offset: 0x000042DC
		internal void SetFocusToLastFocusedElement()
		{
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				this.m_LastFocusedElement.Focus();
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006118 File Offset: 0x00004318
		internal void BlurLastFocusedElement()
		{
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				Focusable lastFocusedElement = this.m_LastFocusedElement;
				this.m_LastFocusedElement.Blur();
				this.m_LastFocusedElement = lastFocusedElement;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006164 File Offset: 0x00004364
		internal void DoFocusChange(Focusable f)
		{
			this.m_FocusedElements.Clear();
			for (VisualElement visualElement = f as VisualElement; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				bool flag = visualElement.hierarchy.parent == null || visualElement.isCompositeRoot;
				if (flag)
				{
					this.m_FocusedElements.Add(new FocusController.FocusedElement
					{
						m_SubTreeRoot = visualElement,
						m_FocusedElement = f
					});
					f = visualElement;
				}
			}
			this.m_PendingFocusCount--;
			bool flag2 = this.m_PendingFocusCount == 0;
			if (flag2)
			{
				this.m_LastPendingFocusedElement = null;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006210 File Offset: 0x00004410
		internal Focusable FocusNextInDirection(FocusChangeDirection direction)
		{
			Focusable nextFocusable = this.focusRing.GetNextFocusable(this.GetLeafFocusedElement(), direction);
			direction.ApplyTo(this, nextFocusable);
			return nextFocusable;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006240 File Offset: 0x00004440
		private void AboutToReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			using (FocusOutEvent pooled = FocusEventBase<FocusOutEvent>.GetPooled(focusable, willGiveFocusTo, direction, this, false))
			{
				focusable.SendEvent(pooled, dispatchMode);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006284 File Offset: 0x00004484
		private void ReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			using (BlurEvent pooled = FocusEventBase<BlurEvent>.GetPooled(focusable, willGiveFocusTo, direction, this, false))
			{
				focusable.SendEvent(pooled, dispatchMode);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000062C8 File Offset: 0x000044C8
		private void AboutToGrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			using (FocusInEvent pooled = FocusEventBase<FocusInEvent>.GetPooled(focusable, willTakeFocusFrom, direction, this, false))
			{
				focusable.SendEvent(pooled, dispatchMode);
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000630C File Offset: 0x0000450C
		private void GrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction, bool bIsFocusDelegated, DispatchMode dispatchMode)
		{
			using (FocusEvent pooled = FocusEventBase<FocusEvent>.GetPooled(focusable, willTakeFocusFrom, direction, this, bIsFocusDelegated))
			{
				focusable.SendEvent(pooled, dispatchMode);
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006350 File Offset: 0x00004550
		internal void Blur(Focusable focusable, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			bool flag = ((this.m_PendingFocusCount > 0) ? this.IsPendingFocus(focusable) : this.IsFocused(focusable));
			bool flag2 = flag;
			if (flag2)
			{
				this.SwitchFocus(null, bIsFocusDelegated, dispatchMode);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006389 File Offset: 0x00004589
		internal void SwitchFocus(Focusable newFocusedElement, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			this.SwitchFocus(newFocusedElement, FocusChangeDirection.unspecified, bIsFocusDelegated, dispatchMode);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000639C File Offset: 0x0000459C
		internal void SwitchFocus(Focusable newFocusedElement, FocusChangeDirection direction, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			this.m_LastFocusedElement = newFocusedElement;
			Focusable focusable = ((this.m_PendingFocusCount > 0) ? this.m_LastPendingFocusedElement : this.GetLeafFocusedElement());
			bool flag = focusable == newFocusedElement;
			if (!flag)
			{
				bool flag2 = newFocusedElement == null || !newFocusedElement.canGrabFocus;
				if (flag2)
				{
					bool flag3 = focusable != null;
					if (flag3)
					{
						this.m_LastPendingFocusedElement = null;
						this.m_PendingFocusCount++;
						this.AboutToReleaseFocus(focusable, null, direction, dispatchMode);
						this.ReleaseFocus(focusable, null, direction, dispatchMode);
					}
				}
				else
				{
					bool flag4 = newFocusedElement != focusable;
					if (flag4)
					{
						VisualElement visualElement = newFocusedElement as VisualElement;
						Focusable focusable2 = ((visualElement != null) ? visualElement.RetargetElement(focusable as VisualElement) : null) ?? newFocusedElement;
						VisualElement visualElement2 = focusable as VisualElement;
						Focusable focusable3 = ((visualElement2 != null) ? visualElement2.RetargetElement(newFocusedElement as VisualElement) : null) ?? focusable;
						this.m_LastPendingFocusedElement = newFocusedElement;
						this.m_PendingFocusCount++;
						bool flag5 = focusable != null;
						if (flag5)
						{
							this.AboutToReleaseFocus(focusable, focusable2, direction, dispatchMode);
						}
						this.AboutToGrabFocus(newFocusedElement, focusable3, direction, dispatchMode);
						bool flag6 = focusable != null;
						if (flag6)
						{
							this.ReleaseFocus(focusable, focusable2, direction, dispatchMode);
						}
						this.GrabFocus(newFocusedElement, focusable3, direction, bIsFocusDelegated, dispatchMode);
					}
				}
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000064DC File Offset: 0x000046DC
		internal Focusable SwitchFocusOnEvent(EventBase e)
		{
			bool processedByFocusController = e.processedByFocusController;
			Focusable focusable;
			if (processedByFocusController)
			{
				focusable = this.GetLeafFocusedElement();
			}
			else
			{
				using (FocusChangeDirection focusChangeDirection = this.focusRing.GetFocusChangeDirection(this.GetLeafFocusedElement(), e))
				{
					bool flag = focusChangeDirection != FocusChangeDirection.none;
					if (flag)
					{
						Focusable focusable2 = this.FocusNextInDirection(focusChangeDirection);
						e.processedByFocusController = true;
						return focusable2;
					}
				}
				focusable = this.GetLeafFocusedElement();
			}
			return focusable;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006560 File Offset: 0x00004760
		internal void ReevaluateFocus()
		{
			VisualElement visualElement = this.focusedElement as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				bool flag2 = !visualElement.isHierarchyDisplayed || !visualElement.visible;
				if (flag2)
				{
					visualElement.Blur();
				}
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000065A4 File Offset: 0x000047A4
		internal bool GetFocusableParentForPointerEvent(Focusable target, out Focusable effectiveTarget)
		{
			bool flag = target == null || !target.focusable;
			bool flag2;
			if (flag)
			{
				effectiveTarget = target;
				flag2 = target != null;
			}
			else
			{
				effectiveTarget = target;
				for (;;)
				{
					VisualElement visualElement = effectiveTarget as VisualElement;
					bool flag3 = visualElement != null && (!visualElement.enabledInHierarchy || !visualElement.focusable) && visualElement.hierarchy.parent != null;
					if (!flag3)
					{
						break;
					}
					effectiveTarget = visualElement.hierarchy.parent;
				}
				flag2 = !this.IsFocused(effectiveTarget);
			}
			return flag2;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000662B File Offset: 0x0000482B
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00006633 File Offset: 0x00004833
		internal int imguiKeyboardControl { get; set; }

		// Token: 0x06000140 RID: 320 RVA: 0x0000663C File Offset: 0x0000483C
		internal void SyncIMGUIFocus(int imguiKeyboardControlID, Focusable imguiContainerHavingKeyboardControl, bool forceSwitch)
		{
			this.imguiKeyboardControl = imguiKeyboardControlID;
			bool flag = forceSwitch || this.imguiKeyboardControl != 0;
			if (flag)
			{
				this.SwitchFocus(imguiContainerHavingKeyboardControl, FocusChangeDirection.unspecified, false, DispatchMode.Default);
			}
			else
			{
				this.SwitchFocus(null, FocusChangeDirection.unspecified, false, DispatchMode.Default);
			}
		}

		// Token: 0x04000086 RID: 134
		private List<FocusController.FocusedElement> m_FocusedElements = new List<FocusController.FocusedElement>();

		// Token: 0x04000087 RID: 135
		private Focusable m_LastFocusedElement;

		// Token: 0x04000088 RID: 136
		private Focusable m_LastPendingFocusedElement;

		// Token: 0x04000089 RID: 137
		private int m_PendingFocusCount = 0;

		// Token: 0x02000030 RID: 48
		private struct FocusedElement
		{
			// Token: 0x0400008B RID: 139
			public VisualElement m_SubTreeRoot;

			// Token: 0x0400008C RID: 140
			public Focusable m_FocusedElement;
		}
	}
}

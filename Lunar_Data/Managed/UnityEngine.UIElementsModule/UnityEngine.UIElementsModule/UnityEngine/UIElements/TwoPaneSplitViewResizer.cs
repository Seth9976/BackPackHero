using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000199 RID: 409
	internal class TwoPaneSplitViewResizer : PointerManipulator
	{
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x000362F8 File Offset: 0x000344F8
		private VisualElement fixedPane
		{
			get
			{
				return this.m_SplitView.fixedPane;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00036305 File Offset: 0x00034505
		private VisualElement flexedPane
		{
			get
			{
				return this.m_SplitView.flexedPane;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00036314 File Offset: 0x00034514
		private float fixedPaneMinDimension
		{
			get
			{
				bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				float num;
				if (flag)
				{
					num = this.fixedPane.resolvedStyle.minWidth.value;
				}
				else
				{
					num = this.fixedPane.resolvedStyle.minHeight.value;
				}
				return num;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00036368 File Offset: 0x00034568
		private float flexedPaneMinDimension
		{
			get
			{
				bool flag = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
				float num;
				if (flag)
				{
					num = this.flexedPane.resolvedStyle.minWidth.value;
				}
				else
				{
					num = this.flexedPane.resolvedStyle.minHeight.value;
				}
				return num;
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000363BC File Offset: 0x000345BC
		public TwoPaneSplitViewResizer(TwoPaneSplitView splitView, int dir, TwoPaneSplitViewOrientation orientation)
		{
			this.m_Orientation = orientation;
			this.m_SplitView = splitView;
			this.m_Direction = dir;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.m_Active = false;
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003640C File Offset: 0x0003460C
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00036468 File Offset: 0x00034668
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000364C4 File Offset: 0x000346C4
		public void ApplyDelta(float delta)
		{
			float num = ((this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.fixedPane.resolvedStyle.width : this.fixedPane.resolvedStyle.height);
			float num2 = num + delta;
			bool flag = num2 < num && num2 < this.fixedPaneMinDimension;
			if (flag)
			{
				num2 = this.fixedPaneMinDimension;
			}
			float num3 = ((this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal) ? this.m_SplitView.resolvedStyle.width : this.m_SplitView.resolvedStyle.height);
			num3 -= this.flexedPaneMinDimension;
			bool flag2 = num2 > num && num2 > num3;
			if (flag2)
			{
				num2 = num3;
			}
			bool flag3 = this.m_Orientation == TwoPaneSplitViewOrientation.Horizontal;
			if (flag3)
			{
				this.fixedPane.style.width = num2;
				bool flag4 = this.m_SplitView.fixedPaneIndex == 0;
				if (flag4)
				{
					base.target.style.left = num2;
				}
				else
				{
					base.target.style.left = this.m_SplitView.resolvedStyle.width - num2;
				}
			}
			else
			{
				this.fixedPane.style.height = num2;
				bool flag5 = this.m_SplitView.fixedPaneIndex == 0;
				if (flag5)
				{
					base.target.style.top = num2;
				}
				else
				{
					base.target.style.top = this.m_SplitView.resolvedStyle.height - num2;
				}
			}
			this.m_SplitView.fixedPaneDimension = num2;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00036660 File Offset: 0x00034860
		protected void OnPointerDown(PointerDownEvent e)
		{
			bool active = this.m_Active;
			if (active)
			{
				e.StopImmediatePropagation();
			}
			else
			{
				bool flag = base.CanStartManipulation(e);
				if (flag)
				{
					this.m_Start = e.localPosition;
					this.m_Active = true;
					base.target.CapturePointer(e.pointerId);
					e.StopPropagation();
				}
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000366BC File Offset: 0x000348BC
		protected void OnPointerMove(PointerMoveEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId);
			if (!flag)
			{
				Vector2 vector = e.localPosition - this.m_Start;
				float num = vector.x;
				bool flag2 = this.m_Orientation == TwoPaneSplitViewOrientation.Vertical;
				if (flag2)
				{
					num = vector.y;
				}
				float num2 = (float)this.m_Direction * num;
				this.ApplyDelta(num2);
				e.StopPropagation();
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0003673C File Offset: 0x0003493C
		protected void OnPointerUp(PointerUpEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId) || !base.CanStopManipulation(e);
			if (!flag)
			{
				this.m_Active = false;
				base.target.ReleasePointer(e.pointerId);
				e.StopPropagation();
			}
		}

		// Token: 0x04000619 RID: 1561
		private Vector3 m_Start;

		// Token: 0x0400061A RID: 1562
		protected bool m_Active;

		// Token: 0x0400061B RID: 1563
		private TwoPaneSplitView m_SplitView;

		// Token: 0x0400061C RID: 1564
		private int m_Direction;

		// Token: 0x0400061D RID: 1565
		private TwoPaneSplitViewOrientation m_Orientation;
	}
}

using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000165 RID: 357
	public class RepeatButton : TextElement
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x0002D088 File Offset: 0x0002B288
		public RepeatButton()
		{
			base.AddToClassList(RepeatButton.ussClassName);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002D09E File Offset: 0x0002B29E
		public RepeatButton(Action clickEvent, long delay, long interval)
			: this()
		{
			this.SetAction(clickEvent, delay, interval);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0002D0B2 File Offset: 0x0002B2B2
		public void SetAction(Action clickEvent, long delay, long interval)
		{
			this.RemoveManipulator(this.m_Clickable);
			this.m_Clickable = new Clickable(clickEvent, delay, interval);
			this.AddManipulator(this.m_Clickable);
		}

		// Token: 0x04000511 RID: 1297
		private Clickable m_Clickable;

		// Token: 0x04000512 RID: 1298
		public new static readonly string ussClassName = "unity-repeat-button";

		// Token: 0x02000166 RID: 358
		public new class UxmlFactory : UxmlFactory<RepeatButton, RepeatButton.UxmlTraits>
		{
		}

		// Token: 0x02000167 RID: 359
		public new class UxmlTraits : TextElement.UxmlTraits
		{
			// Token: 0x06000B37 RID: 2871 RVA: 0x0002D0F4 File Offset: 0x0002B2F4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RepeatButton repeatButton = (RepeatButton)ve;
				repeatButton.SetAction(null, this.m_Delay.GetValueFromBag(bag, cc), this.m_Interval.GetValueFromBag(bag, cc));
			}

			// Token: 0x04000513 RID: 1299
			private UxmlLongAttributeDescription m_Delay = new UxmlLongAttributeDescription
			{
				name = "delay"
			};

			// Token: 0x04000514 RID: 1300
			private UxmlLongAttributeDescription m_Interval = new UxmlLongAttributeDescription
			{
				name = "interval"
			};
		}
	}
}

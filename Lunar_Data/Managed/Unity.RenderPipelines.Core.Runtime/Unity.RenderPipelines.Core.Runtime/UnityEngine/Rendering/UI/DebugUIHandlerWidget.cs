using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000106 RID: 262
	public class DebugUIHandlerWidget : MonoBehaviour
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00022108 File Offset: 0x00020308
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x00022110 File Offset: 0x00020310
		public DebugUIHandlerWidget parentUIHandler { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00022119 File Offset: 0x00020319
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x00022121 File Offset: 0x00020321
		public DebugUIHandlerWidget previousUIHandler { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0002212A File Offset: 0x0002032A
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x00022132 File Offset: 0x00020332
		public DebugUIHandlerWidget nextUIHandler { get; set; }

		// Token: 0x060007CE RID: 1998 RVA: 0x0002213B File Offset: 0x0002033B
		protected virtual void OnEnable()
		{
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002213D File Offset: 0x0002033D
		internal virtual void SetWidget(DebugUI.Widget widget)
		{
			this.m_Widget = widget;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00022146 File Offset: 0x00020346
		internal DebugUI.Widget GetWidget()
		{
			return this.m_Widget;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00022150 File Offset: 0x00020350
		protected T CastWidget<T>() where T : DebugUI.Widget
		{
			T t = this.m_Widget as T;
			string text = ((this.m_Widget == null) ? "null" : this.m_Widget.GetType().ToString());
			if (t == null)
			{
				string text2 = "Can't cast ";
				string text3 = text;
				string text4 = " to ";
				Type typeFromHandle = typeof(T);
				throw new InvalidOperationException(text2 + text3 + text4 + ((typeFromHandle != null) ? typeFromHandle.ToString() : null));
			}
			return t;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000221C1 File Offset: 0x000203C1
		public virtual bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			return true;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000221C4 File Offset: 0x000203C4
		public virtual void OnDeselection()
		{
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x000221C6 File Offset: 0x000203C6
		public virtual void OnAction()
		{
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000221C8 File Offset: 0x000203C8
		public virtual void OnIncrement(bool fast)
		{
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000221CA File Offset: 0x000203CA
		public virtual void OnDecrement(bool fast)
		{
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000221CC File Offset: 0x000203CC
		public virtual DebugUIHandlerWidget Previous()
		{
			if (this.previousUIHandler != null)
			{
				return this.previousUIHandler;
			}
			if (this.parentUIHandler != null)
			{
				return this.parentUIHandler;
			}
			return null;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000221FC File Offset: 0x000203FC
		public virtual DebugUIHandlerWidget Next()
		{
			if (this.nextUIHandler != null)
			{
				return this.nextUIHandler;
			}
			if (this.parentUIHandler != null)
			{
				DebugUIHandlerWidget debugUIHandlerWidget = this.parentUIHandler;
				while (debugUIHandlerWidget != null)
				{
					DebugUIHandlerWidget nextUIHandler = debugUIHandlerWidget.nextUIHandler;
					if (nextUIHandler != null)
					{
						return nextUIHandler;
					}
					debugUIHandlerWidget = debugUIHandlerWidget.parentUIHandler;
				}
			}
			return null;
		}

		// Token: 0x04000444 RID: 1092
		[HideInInspector]
		public Color colorDefault = new Color(0.8f, 0.8f, 0.8f, 1f);

		// Token: 0x04000445 RID: 1093
		[HideInInspector]
		public Color colorSelected = new Color(0.25f, 0.65f, 0.8f, 1f);

		// Token: 0x04000449 RID: 1097
		protected DebugUI.Widget m_Widget;
	}
}

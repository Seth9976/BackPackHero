using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000029 RID: 41
	[AssetFileNameExtension("guiskin", new string[] { })]
	[RequiredByNativeCode]
	[ExecuteInEditMode]
	[Serializable]
	public sealed class GUISkin : ScriptableObject
	{
		// Token: 0x0600028F RID: 655 RVA: 0x0000A71E File Offset: 0x0000891E
		public GUISkin()
		{
			this.m_CustomStyles = new GUIStyle[1];
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A746 File Offset: 0x00008946
		internal void OnEnable()
		{
			this.Apply();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A750 File Offset: 0x00008950
		internal static void CleanupRoots()
		{
			GUISkin.current = null;
			GUISkin.ms_Error = null;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000A760 File Offset: 0x00008960
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000A778 File Offset: 0x00008978
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
				bool flag = GUISkin.current == this;
				if (flag)
				{
					GUIStyle.SetDefaultFont(this.m_Font);
				}
				this.Apply();
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000A7B0 File Offset: 0x000089B0
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000A7C8 File Offset: 0x000089C8
		public GUIStyle box
		{
			get
			{
				return this.m_box;
			}
			set
			{
				this.m_box = value;
				this.Apply();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000A7DC File Offset: 0x000089DC
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000A7F4 File Offset: 0x000089F4
		public GUIStyle label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
				this.Apply();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000A808 File Offset: 0x00008A08
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000A820 File Offset: 0x00008A20
		public GUIStyle textField
		{
			get
			{
				return this.m_textField;
			}
			set
			{
				this.m_textField = value;
				this.Apply();
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000A834 File Offset: 0x00008A34
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000A84C File Offset: 0x00008A4C
		public GUIStyle textArea
		{
			get
			{
				return this.m_textArea;
			}
			set
			{
				this.m_textArea = value;
				this.Apply();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000A860 File Offset: 0x00008A60
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000A878 File Offset: 0x00008A78
		public GUIStyle button
		{
			get
			{
				return this.m_button;
			}
			set
			{
				this.m_button = value;
				this.Apply();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000A88C File Offset: 0x00008A8C
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		public GUIStyle toggle
		{
			get
			{
				return this.m_toggle;
			}
			set
			{
				this.m_toggle = value;
				this.Apply();
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		public GUIStyle window
		{
			get
			{
				return this.m_window;
			}
			set
			{
				this.m_window = value;
				this.Apply();
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000A8FC File Offset: 0x00008AFC
		public GUIStyle horizontalSlider
		{
			get
			{
				return this.m_horizontalSlider;
			}
			set
			{
				this.m_horizontalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000A910 File Offset: 0x00008B10
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000A928 File Offset: 0x00008B28
		public GUIStyle horizontalSliderThumb
		{
			get
			{
				return this.m_horizontalSliderThumb;
			}
			set
			{
				this.m_horizontalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000A93C File Offset: 0x00008B3C
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000A954 File Offset: 0x00008B54
		internal GUIStyle horizontalSliderThumbExtent
		{
			get
			{
				return this.m_horizontalSliderThumbExtent;
			}
			set
			{
				this.m_horizontalSliderThumbExtent = value;
				this.Apply();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000A968 File Offset: 0x00008B68
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000A980 File Offset: 0x00008B80
		internal GUIStyle sliderMixed
		{
			get
			{
				return this.m_SliderMixed;
			}
			set
			{
				this.m_SliderMixed = value;
				this.Apply();
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000A994 File Offset: 0x00008B94
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000A9AC File Offset: 0x00008BAC
		public GUIStyle verticalSlider
		{
			get
			{
				return this.m_verticalSlider;
			}
			set
			{
				this.m_verticalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public GUIStyle verticalSliderThumb
		{
			get
			{
				return this.m_verticalSliderThumb;
			}
			set
			{
				this.m_verticalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000A9EC File Offset: 0x00008BEC
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000AA04 File Offset: 0x00008C04
		internal GUIStyle verticalSliderThumbExtent
		{
			get
			{
				return this.m_verticalSliderThumbExtent;
			}
			set
			{
				this.m_verticalSliderThumbExtent = value;
				this.Apply();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000AA18 File Offset: 0x00008C18
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000AA30 File Offset: 0x00008C30
		public GUIStyle horizontalScrollbar
		{
			get
			{
				return this.m_horizontalScrollbar;
			}
			set
			{
				this.m_horizontalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000AA44 File Offset: 0x00008C44
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000AA5C File Offset: 0x00008C5C
		public GUIStyle horizontalScrollbarThumb
		{
			get
			{
				return this.m_horizontalScrollbarThumb;
			}
			set
			{
				this.m_horizontalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000AA70 File Offset: 0x00008C70
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000AA88 File Offset: 0x00008C88
		public GUIStyle horizontalScrollbarLeftButton
		{
			get
			{
				return this.m_horizontalScrollbarLeftButton;
			}
			set
			{
				this.m_horizontalScrollbarLeftButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000AA9C File Offset: 0x00008C9C
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000AAB4 File Offset: 0x00008CB4
		public GUIStyle horizontalScrollbarRightButton
		{
			get
			{
				return this.m_horizontalScrollbarRightButton;
			}
			set
			{
				this.m_horizontalScrollbarRightButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		public GUIStyle verticalScrollbar
		{
			get
			{
				return this.m_verticalScrollbar;
			}
			set
			{
				this.m_verticalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000AB0C File Offset: 0x00008D0C
		public GUIStyle verticalScrollbarThumb
		{
			get
			{
				return this.m_verticalScrollbarThumb;
			}
			set
			{
				this.m_verticalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000AB20 File Offset: 0x00008D20
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000AB38 File Offset: 0x00008D38
		public GUIStyle verticalScrollbarUpButton
		{
			get
			{
				return this.m_verticalScrollbarUpButton;
			}
			set
			{
				this.m_verticalScrollbarUpButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000AB4C File Offset: 0x00008D4C
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000AB64 File Offset: 0x00008D64
		public GUIStyle verticalScrollbarDownButton
		{
			get
			{
				return this.m_verticalScrollbarDownButton;
			}
			set
			{
				this.m_verticalScrollbarDownButton = value;
				this.Apply();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000AB78 File Offset: 0x00008D78
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000AB90 File Offset: 0x00008D90
		public GUIStyle scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
			set
			{
				this.m_ScrollView = value;
				this.Apply();
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000ABA4 File Offset: 0x00008DA4
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000ABBC File Offset: 0x00008DBC
		public GUIStyle[] customStyles
		{
			get
			{
				return this.m_CustomStyles;
			}
			set
			{
				this.m_CustomStyles = value;
				this.Apply();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public GUISettings settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		internal static GUIStyle error
		{
			get
			{
				bool flag = GUISkin.ms_Error == null;
				if (flag)
				{
					GUISkin.ms_Error = new GUIStyle();
					GUISkin.ms_Error.name = "StyleNotFoundError";
				}
				return GUISkin.ms_Error;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000AC28 File Offset: 0x00008E28
		internal void Apply()
		{
			bool flag = this.m_CustomStyles == null;
			if (flag)
			{
				Debug.Log("custom styles is null");
			}
			this.BuildStyleCache();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000AC58 File Offset: 0x00008E58
		private void BuildStyleCache()
		{
			bool flag = this.m_box == null;
			if (flag)
			{
				this.m_box = new GUIStyle();
			}
			bool flag2 = this.m_button == null;
			if (flag2)
			{
				this.m_button = new GUIStyle();
			}
			bool flag3 = this.m_toggle == null;
			if (flag3)
			{
				this.m_toggle = new GUIStyle();
			}
			bool flag4 = this.m_label == null;
			if (flag4)
			{
				this.m_label = new GUIStyle();
			}
			bool flag5 = this.m_window == null;
			if (flag5)
			{
				this.m_window = new GUIStyle();
			}
			bool flag6 = this.m_textField == null;
			if (flag6)
			{
				this.m_textField = new GUIStyle();
			}
			bool flag7 = this.m_textArea == null;
			if (flag7)
			{
				this.m_textArea = new GUIStyle();
			}
			bool flag8 = this.m_horizontalSlider == null;
			if (flag8)
			{
				this.m_horizontalSlider = new GUIStyle();
			}
			bool flag9 = this.m_horizontalSliderThumb == null;
			if (flag9)
			{
				this.m_horizontalSliderThumb = new GUIStyle();
			}
			bool flag10 = this.m_verticalSlider == null;
			if (flag10)
			{
				this.m_verticalSlider = new GUIStyle();
			}
			bool flag11 = this.m_verticalSliderThumb == null;
			if (flag11)
			{
				this.m_verticalSliderThumb = new GUIStyle();
			}
			bool flag12 = this.m_horizontalScrollbar == null;
			if (flag12)
			{
				this.m_horizontalScrollbar = new GUIStyle();
			}
			bool flag13 = this.m_horizontalScrollbarThumb == null;
			if (flag13)
			{
				this.m_horizontalScrollbarThumb = new GUIStyle();
			}
			bool flag14 = this.m_horizontalScrollbarLeftButton == null;
			if (flag14)
			{
				this.m_horizontalScrollbarLeftButton = new GUIStyle();
			}
			bool flag15 = this.m_horizontalScrollbarRightButton == null;
			if (flag15)
			{
				this.m_horizontalScrollbarRightButton = new GUIStyle();
			}
			bool flag16 = this.m_verticalScrollbar == null;
			if (flag16)
			{
				this.m_verticalScrollbar = new GUIStyle();
			}
			bool flag17 = this.m_verticalScrollbarThumb == null;
			if (flag17)
			{
				this.m_verticalScrollbarThumb = new GUIStyle();
			}
			bool flag18 = this.m_verticalScrollbarUpButton == null;
			if (flag18)
			{
				this.m_verticalScrollbarUpButton = new GUIStyle();
			}
			bool flag19 = this.m_verticalScrollbarDownButton == null;
			if (flag19)
			{
				this.m_verticalScrollbarDownButton = new GUIStyle();
			}
			bool flag20 = this.m_ScrollView == null;
			if (flag20)
			{
				this.m_ScrollView = new GUIStyle();
			}
			this.m_Styles = new Dictionary<string, GUIStyle>(StringComparer.OrdinalIgnoreCase);
			this.m_Styles["box"] = this.m_box;
			this.m_box.name = "box";
			this.m_Styles["button"] = this.m_button;
			this.m_button.name = "button";
			this.m_Styles["toggle"] = this.m_toggle;
			this.m_toggle.name = "toggle";
			this.m_Styles["label"] = this.m_label;
			this.m_label.name = "label";
			this.m_Styles["window"] = this.m_window;
			this.m_window.name = "window";
			this.m_Styles["textfield"] = this.m_textField;
			this.m_textField.name = "textfield";
			this.m_Styles["textarea"] = this.m_textArea;
			this.m_textArea.name = "textarea";
			this.m_Styles["horizontalslider"] = this.m_horizontalSlider;
			this.m_horizontalSlider.name = "horizontalslider";
			this.m_Styles["horizontalsliderthumb"] = this.m_horizontalSliderThumb;
			this.m_horizontalSliderThumb.name = "horizontalsliderthumb";
			this.m_Styles["verticalslider"] = this.m_verticalSlider;
			this.m_verticalSlider.name = "verticalslider";
			this.m_Styles["verticalsliderthumb"] = this.m_verticalSliderThumb;
			this.m_verticalSliderThumb.name = "verticalsliderthumb";
			this.m_Styles["horizontalscrollbar"] = this.m_horizontalScrollbar;
			this.m_horizontalScrollbar.name = "horizontalscrollbar";
			this.m_Styles["horizontalscrollbarthumb"] = this.m_horizontalScrollbarThumb;
			this.m_horizontalScrollbarThumb.name = "horizontalscrollbarthumb";
			this.m_Styles["horizontalscrollbarleftbutton"] = this.m_horizontalScrollbarLeftButton;
			this.m_horizontalScrollbarLeftButton.name = "horizontalscrollbarleftbutton";
			this.m_Styles["horizontalscrollbarrightbutton"] = this.m_horizontalScrollbarRightButton;
			this.m_horizontalScrollbarRightButton.name = "horizontalscrollbarrightbutton";
			this.m_Styles["verticalscrollbar"] = this.m_verticalScrollbar;
			this.m_verticalScrollbar.name = "verticalscrollbar";
			this.m_Styles["verticalscrollbarthumb"] = this.m_verticalScrollbarThumb;
			this.m_verticalScrollbarThumb.name = "verticalscrollbarthumb";
			this.m_Styles["verticalscrollbarupbutton"] = this.m_verticalScrollbarUpButton;
			this.m_verticalScrollbarUpButton.name = "verticalscrollbarupbutton";
			this.m_Styles["verticalscrollbardownbutton"] = this.m_verticalScrollbarDownButton;
			this.m_verticalScrollbarDownButton.name = "verticalscrollbardownbutton";
			this.m_Styles["scrollview"] = this.m_ScrollView;
			this.m_ScrollView.name = "scrollview";
			bool flag21 = this.m_CustomStyles != null;
			if (flag21)
			{
				for (int i = 0; i < this.m_CustomStyles.Length; i++)
				{
					bool flag22 = this.m_CustomStyles[i] == null;
					if (!flag22)
					{
						this.m_Styles[this.m_CustomStyles[i].name] = this.m_CustomStyles[i];
					}
				}
			}
			bool flag23 = !this.m_Styles.TryGetValue("HorizontalSliderThumbExtent", ref this.m_horizontalSliderThumbExtent);
			if (flag23)
			{
				this.m_horizontalSliderThumbExtent = new GUIStyle();
				this.m_horizontalSliderThumbExtent.name = "horizontalsliderthumbextent";
				this.m_Styles["HorizontalSliderThumbExtent"] = this.m_horizontalSliderThumbExtent;
			}
			bool flag24 = !this.m_Styles.TryGetValue("SliderMixed", ref this.m_SliderMixed);
			if (flag24)
			{
				this.m_SliderMixed = new GUIStyle();
				this.m_SliderMixed.name = "SliderMixed";
				this.m_Styles["SliderMixed"] = this.m_SliderMixed;
			}
			bool flag25 = !this.m_Styles.TryGetValue("VerticalSliderThumbExtent", ref this.m_verticalSliderThumbExtent);
			if (flag25)
			{
				this.m_verticalSliderThumbExtent = new GUIStyle();
				this.m_Styles["VerticalSliderThumbExtent"] = this.m_verticalSliderThumbExtent;
				this.m_verticalSliderThumbExtent.name = "verticalsliderthumbextent";
			}
			GUISkin.error.stretchHeight = true;
			GUISkin.error.normal.textColor = Color.red;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B31C File Offset: 0x0000951C
		public GUIStyle GetStyle(string styleName)
		{
			GUIStyle guistyle = this.FindStyle(styleName);
			bool flag = guistyle != null;
			GUIStyle guistyle2;
			if (flag)
			{
				guistyle2 = guistyle;
			}
			else
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to find style '",
					styleName,
					"' in skin '",
					base.name,
					"' ",
					(Event.current != null) ? Event.current.type.ToString() : "<called outside OnGUI>"
				}));
				guistyle2 = GUISkin.error;
			}
			return guistyle2;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B3A8 File Offset: 0x000095A8
		public GUIStyle FindStyle(string styleName)
		{
			bool flag = this.m_Styles == null;
			if (flag)
			{
				this.BuildStyleCache();
			}
			GUIStyle guistyle;
			bool flag2 = this.m_Styles.TryGetValue(styleName, ref guistyle);
			GUIStyle guistyle2;
			if (flag2)
			{
				guistyle2 = guistyle;
			}
			else
			{
				guistyle2 = null;
			}
			return guistyle2;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000B3E8 File Offset: 0x000095E8
		internal void MakeCurrent()
		{
			GUISkin.current = this;
			GUIStyle.SetDefaultFont(this.font);
			bool flag = GUISkin.m_SkinChanged != null;
			if (flag)
			{
				GUISkin.m_SkinChanged();
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000B420 File Offset: 0x00009620
		public IEnumerator GetEnumerator()
		{
			bool flag = this.m_Styles == null;
			if (flag)
			{
				this.BuildStyleCache();
			}
			return this.m_Styles.Values.GetEnumerator();
		}

		// Token: 0x040000AA RID: 170
		[SerializeField]
		private Font m_Font;

		// Token: 0x040000AB RID: 171
		[SerializeField]
		private GUIStyle m_box;

		// Token: 0x040000AC RID: 172
		[SerializeField]
		private GUIStyle m_button;

		// Token: 0x040000AD RID: 173
		[SerializeField]
		private GUIStyle m_toggle;

		// Token: 0x040000AE RID: 174
		[SerializeField]
		private GUIStyle m_label;

		// Token: 0x040000AF RID: 175
		[SerializeField]
		private GUIStyle m_textField;

		// Token: 0x040000B0 RID: 176
		[SerializeField]
		private GUIStyle m_textArea;

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		private GUIStyle m_window;

		// Token: 0x040000B2 RID: 178
		[SerializeField]
		private GUIStyle m_horizontalSlider;

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		private GUIStyle m_horizontalSliderThumb;

		// Token: 0x040000B4 RID: 180
		[NonSerialized]
		private GUIStyle m_horizontalSliderThumbExtent;

		// Token: 0x040000B5 RID: 181
		[SerializeField]
		private GUIStyle m_verticalSlider;

		// Token: 0x040000B6 RID: 182
		[SerializeField]
		private GUIStyle m_verticalSliderThumb;

		// Token: 0x040000B7 RID: 183
		[NonSerialized]
		private GUIStyle m_verticalSliderThumbExtent;

		// Token: 0x040000B8 RID: 184
		[NonSerialized]
		private GUIStyle m_SliderMixed;

		// Token: 0x040000B9 RID: 185
		[SerializeField]
		private GUIStyle m_horizontalScrollbar;

		// Token: 0x040000BA RID: 186
		[SerializeField]
		private GUIStyle m_horizontalScrollbarThumb;

		// Token: 0x040000BB RID: 187
		[SerializeField]
		private GUIStyle m_horizontalScrollbarLeftButton;

		// Token: 0x040000BC RID: 188
		[SerializeField]
		private GUIStyle m_horizontalScrollbarRightButton;

		// Token: 0x040000BD RID: 189
		[SerializeField]
		private GUIStyle m_verticalScrollbar;

		// Token: 0x040000BE RID: 190
		[SerializeField]
		private GUIStyle m_verticalScrollbarThumb;

		// Token: 0x040000BF RID: 191
		[SerializeField]
		private GUIStyle m_verticalScrollbarUpButton;

		// Token: 0x040000C0 RID: 192
		[SerializeField]
		private GUIStyle m_verticalScrollbarDownButton;

		// Token: 0x040000C1 RID: 193
		[SerializeField]
		private GUIStyle m_ScrollView;

		// Token: 0x040000C2 RID: 194
		[SerializeField]
		internal GUIStyle[] m_CustomStyles;

		// Token: 0x040000C3 RID: 195
		[SerializeField]
		private GUISettings m_Settings = new GUISettings();

		// Token: 0x040000C4 RID: 196
		internal static GUIStyle ms_Error;

		// Token: 0x040000C5 RID: 197
		private Dictionary<string, GUIStyle> m_Styles = null;

		// Token: 0x040000C6 RID: 198
		internal static GUISkin.SkinChangedDelegate m_SkinChanged;

		// Token: 0x040000C7 RID: 199
		internal static GUISkin current;

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x060002CD RID: 717
		internal delegate void SkinChangedDelegate();
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[RequiredByNativeCode]
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
	[NativeHeader("IMGUIScriptingClasses.h")]
	[Serializable]
	[StructLayout(0)]
	public sealed class GUIStyle
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002E2 RID: 738
		// (set) Token: 0x060002E3 RID: 739
		[NativeProperty("Name", false, TargetType.Function)]
		internal extern string rawName
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002E4 RID: 740
		// (set) Token: 0x060002E5 RID: 741
		[NativeProperty("Font", false, TargetType.Function)]
		public extern Font font
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002E6 RID: 742
		// (set) Token: 0x060002E7 RID: 743
		[NativeProperty("m_ImagePosition", false, TargetType.Field)]
		public extern ImagePosition imagePosition
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002E8 RID: 744
		// (set) Token: 0x060002E9 RID: 745
		[NativeProperty("m_Alignment", false, TargetType.Field)]
		public extern TextAnchor alignment
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002EA RID: 746
		// (set) Token: 0x060002EB RID: 747
		[NativeProperty("m_WordWrap", false, TargetType.Field)]
		public extern bool wordWrap
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002EC RID: 748
		// (set) Token: 0x060002ED RID: 749
		[NativeProperty("m_Clipping", false, TargetType.Field)]
		public extern TextClipping clipping
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000B5C8 File Offset: 0x000097C8
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000B5DE File Offset: 0x000097DE
		[NativeProperty("m_ContentOffset", false, TargetType.Field)]
		public Vector2 contentOffset
		{
			get
			{
				Vector2 vector;
				this.get_contentOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_contentOffset_Injected(ref value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002F0 RID: 752
		// (set) Token: 0x060002F1 RID: 753
		[NativeProperty("m_FixedWidth", false, TargetType.Field)]
		public extern float fixedWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002F2 RID: 754
		// (set) Token: 0x060002F3 RID: 755
		[NativeProperty("m_FixedHeight", false, TargetType.Field)]
		public extern float fixedHeight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002F4 RID: 756
		// (set) Token: 0x060002F5 RID: 757
		[NativeProperty("m_StretchWidth", false, TargetType.Field)]
		public extern bool stretchWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002F6 RID: 758
		// (set) Token: 0x060002F7 RID: 759
		[NativeProperty("m_StretchHeight", false, TargetType.Field)]
		public extern bool stretchHeight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002F8 RID: 760
		// (set) Token: 0x060002F9 RID: 761
		[NativeProperty("m_FontSize", false, TargetType.Field)]
		public extern int fontSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002FA RID: 762
		// (set) Token: 0x060002FB RID: 763
		[NativeProperty("m_FontStyle", false, TargetType.Field)]
		public extern FontStyle fontStyle
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002FC RID: 764
		// (set) Token: 0x060002FD RID: 765
		[NativeProperty("m_RichText", false, TargetType.Field)]
		public extern bool richText
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000B5E8 File Offset: 0x000097E8
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000B5FE File Offset: 0x000097FE
		[Obsolete("Don't use clipOffset - put things inside BeginGroup instead. This functionality will be removed in a later version.", false)]
		[NativeProperty("m_ClipOffset", false, TargetType.Field)]
		public Vector2 clipOffset
		{
			get
			{
				Vector2 vector;
				this.get_clipOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_clipOffset_Injected(ref value);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000B608 File Offset: 0x00009808
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000B61E File Offset: 0x0000981E
		[NativeProperty("m_ClipOffset", false, TargetType.Field)]
		internal Vector2 Internal_clipOffset
		{
			get
			{
				Vector2 vector;
				this.get_Internal_clipOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_Internal_clipOffset_Injected(ref value);
			}
		}

		// Token: 0x06000302 RID: 770
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Create", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Internal_Create(GUIStyle self);

		// Token: 0x06000303 RID: 771
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Copy", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern IntPtr Internal_Copy(GUIStyle self, GUIStyle other);

		// Token: 0x06000304 RID: 772
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Destroy", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr self);

		// Token: 0x06000305 RID: 773
		[FreeFunction(Name = "GUIStyle_Bindings::GetStyleStatePtr", IsThreadSafe = true, HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern IntPtr GetStyleStatePtr(int idx);

		// Token: 0x06000306 RID: 774
		[FreeFunction(Name = "GUIStyle_Bindings::AssignStyleState", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void AssignStyleState(int idx, IntPtr srcStyleState);

		// Token: 0x06000307 RID: 775
		[FreeFunction(Name = "GUIStyle_Bindings::GetRectOffsetPtr", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern IntPtr GetRectOffsetPtr(int idx);

		// Token: 0x06000308 RID: 776
		[FreeFunction(Name = "GUIStyle_Bindings::AssignRectOffset", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void AssignRectOffset(int idx, IntPtr srcRectOffset);

		// Token: 0x06000309 RID: 777
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetLineHeight")]
		[MethodImpl(4096)]
		private static extern float Internal_GetLineHeight(IntPtr target);

		// Token: 0x0600030A RID: 778 RVA: 0x0000B628 File Offset: 0x00009828
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw", HasExplicitThis = true)]
		private void Internal_Draw(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Internal_Draw_Injected(ref screenRect, content, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000B63A File Offset: 0x0000983A
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_Draw2", HasExplicitThis = true)]
		private void Internal_Draw2(Rect position, GUIContent content, int controlID, bool on)
		{
			this.Internal_Draw2_Injected(ref position, content, controlID, on);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B648 File Offset: 0x00009848
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawCursor", HasExplicitThis = true)]
		private void Internal_DrawCursor(Rect position, GUIContent content, int pos, Color cursorColor)
		{
			this.Internal_DrawCursor_Injected(ref position, content, pos, ref cursorColor);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B658 File Offset: 0x00009858
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_DrawWithTextSelection", HasExplicitThis = true)]
		private void Internal_DrawWithTextSelection(Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, Color cursorColor, Color selectionColor)
		{
			this.Internal_DrawWithTextSelection_Injected(ref screenRect, content, isHover, isActive, on, hasKeyboardFocus, drawSelectionAsComposition, cursorFirst, cursorLast, ref cursorColor, ref selectionColor);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B680 File Offset: 0x00009880
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorPixelPosition", HasExplicitThis = true)]
		internal Vector2 Internal_GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			Vector2 vector;
			this.Internal_GetCursorPixelPosition_Injected(ref position, content, cursorStringIndex, out vector);
			return vector;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B69A File Offset: 0x0000989A
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorStringIndex", HasExplicitThis = true)]
		internal int Internal_GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return this.Internal_GetCursorStringIndex_Injected(ref position, content, ref cursorPixelPosition);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B6A7 File Offset: 0x000098A7
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetSelectedRenderedText", HasExplicitThis = true)]
		internal string Internal_GetSelectedRenderedText(Rect localPosition, GUIContent mContent, int selectIndex, int cursorIndex)
		{
			return this.Internal_GetSelectedRenderedText_Injected(ref localPosition, mContent, selectIndex, cursorIndex);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B6B5 File Offset: 0x000098B5
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetHyperlinksRect", HasExplicitThis = true)]
		internal Rect[] Internal_GetHyperlinksRect(Rect localPosition, GUIContent mContent)
		{
			return this.Internal_GetHyperlinksRect_Injected(ref localPosition, mContent);
		}

		// Token: 0x06000312 RID: 786
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetNumCharactersThatFitWithinWidth", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern int Internal_GetNumCharactersThatFitWithinWidth(string text, float width);

		// Token: 0x06000313 RID: 787 RVA: 0x0000B6C0 File Offset: 0x000098C0
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSize", HasExplicitThis = true)]
		internal Vector2 Internal_CalcSize(GUIContent content)
		{
			Vector2 vector;
			this.Internal_CalcSize_Injected(content, out vector);
			return vector;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B6D8 File Offset: 0x000098D8
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcSizeWithConstraints", HasExplicitThis = true)]
		internal Vector2 Internal_CalcSizeWithConstraints(GUIContent content, Vector2 maxSize)
		{
			Vector2 vector;
			this.Internal_CalcSizeWithConstraints_Injected(content, ref maxSize, out vector);
			return vector;
		}

		// Token: 0x06000315 RID: 789
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcHeight", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern float Internal_CalcHeight(GUIContent content, float width);

		// Token: 0x06000316 RID: 790 RVA: 0x0000B6F4 File Offset: 0x000098F4
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_CalcMinMaxWidth", HasExplicitThis = true)]
		private Vector2 Internal_CalcMinMaxWidth(GUIContent content)
		{
			Vector2 vector;
			this.Internal_CalcMinMaxWidth_Injected(content, out vector);
			return vector;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B70B File Offset: 0x0000990B
		[FreeFunction(Name = "GUIStyle_Bindings::SetMouseTooltip")]
		internal static void SetMouseTooltip(string tooltip, Rect screenRect)
		{
			GUIStyle.SetMouseTooltip_Injected(tooltip, ref screenRect);
		}

		// Token: 0x06000318 RID: 792
		[FreeFunction(Name = "GUIStyle_Bindings::IsTooltipActive")]
		[MethodImpl(4096)]
		internal static extern bool IsTooltipActive(string tooltip);

		// Token: 0x06000319 RID: 793
		[FreeFunction(Name = "GUIStyle_Bindings::Internal_GetCursorFlashOffset")]
		[MethodImpl(4096)]
		private static extern float Internal_GetCursorFlashOffset();

		// Token: 0x0600031A RID: 794
		[FreeFunction(Name = "GUIStyle::SetDefaultFont")]
		[MethodImpl(4096)]
		internal static extern void SetDefaultFont(Font font);

		// Token: 0x0600031B RID: 795 RVA: 0x0000B715 File Offset: 0x00009915
		public GUIStyle()
		{
			this.m_Ptr = GUIStyle.Internal_Create(this);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000B72C File Offset: 0x0000992C
		public GUIStyle(GUIStyle other)
		{
			bool flag = other == null;
			if (flag)
			{
				Debug.LogError("Copied style is null. Using StyleNotFound instead.");
				other = GUISkin.error;
			}
			this.m_Ptr = GUIStyle.Internal_Copy(this, other);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000B76C File Offset: 0x0000996C
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					GUIStyle.Internal_Destroy(this.m_Ptr);
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B7C4 File Offset: 0x000099C4
		internal static void CleanupRoots()
		{
			GUIStyle.s_None = null;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B7D0 File Offset: 0x000099D0
		internal void InternalOnAfterDeserialize()
		{
			this.m_Normal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(0));
			this.m_Hover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(1));
			this.m_Active = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(2));
			this.m_Focused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(3));
			this.m_OnNormal = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(4));
			this.m_OnHover = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(5));
			this.m_OnActive = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(6));
			this.m_OnFocused = GUIStyleState.ProduceGUIStyleStateFromDeserialization(this, this.GetStyleStatePtr(7));
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000B878 File Offset: 0x00009A78
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000B8A3 File Offset: 0x00009AA3
		public string name
		{
			get
			{
				string text;
				if ((text = this.m_Name) == null)
				{
					text = (this.m_Name = this.rawName);
				}
				return text;
			}
			set
			{
				this.m_Name = value;
				this.rawName = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000B8EA File Offset: 0x00009AEA
		public GUIStyleState normal
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Normal) == null)
				{
					guistyleState = (this.m_Normal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(0)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(0, value.m_Ptr);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000B8FC File Offset: 0x00009AFC
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000B92E File Offset: 0x00009B2E
		public GUIStyleState hover
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Hover) == null)
				{
					guistyleState = (this.m_Hover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(1)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(1, value.m_Ptr);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000B940 File Offset: 0x00009B40
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000B972 File Offset: 0x00009B72
		public GUIStyleState active
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Active) == null)
				{
					guistyleState = (this.m_Active = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(2)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(2, value.m_Ptr);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000B984 File Offset: 0x00009B84
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000B9B6 File Offset: 0x00009BB6
		public GUIStyleState onNormal
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnNormal) == null)
				{
					guistyleState = (this.m_OnNormal = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(4)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(4, value.m_Ptr);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		// (set) Token: 0x0600032B RID: 811 RVA: 0x0000B9FA File Offset: 0x00009BFA
		public GUIStyleState onHover
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnHover) == null)
				{
					guistyleState = (this.m_OnHover = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(5)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(5, value.m_Ptr);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000BA0C File Offset: 0x00009C0C
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000BA3E File Offset: 0x00009C3E
		public GUIStyleState onActive
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnActive) == null)
				{
					guistyleState = (this.m_OnActive = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(6)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(6, value.m_Ptr);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000BA50 File Offset: 0x00009C50
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000BA82 File Offset: 0x00009C82
		public GUIStyleState focused
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_Focused) == null)
				{
					guistyleState = (this.m_Focused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(3)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(3, value.m_Ptr);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000BA94 File Offset: 0x00009C94
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000BAC6 File Offset: 0x00009CC6
		public GUIStyleState onFocused
		{
			get
			{
				GUIStyleState guistyleState;
				if ((guistyleState = this.m_OnFocused) == null)
				{
					guistyleState = (this.m_OnFocused = GUIStyleState.GetGUIStyleState(this, this.GetStyleStatePtr(7)));
				}
				return guistyleState;
			}
			set
			{
				this.AssignStyleState(7, value.m_Ptr);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000BB0A File Offset: 0x00009D0A
		public RectOffset border
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Border) == null)
				{
					rectOffset = (this.m_Border = new RectOffset(this, this.GetRectOffsetPtr(0)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(0, value.m_Ptr);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000BB1C File Offset: 0x00009D1C
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000BB4E File Offset: 0x00009D4E
		public RectOffset margin
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Margin) == null)
				{
					rectOffset = (this.m_Margin = new RectOffset(this, this.GetRectOffsetPtr(1)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(1, value.m_Ptr);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000BB60 File Offset: 0x00009D60
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000BB92 File Offset: 0x00009D92
		public RectOffset padding
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Padding) == null)
				{
					rectOffset = (this.m_Padding = new RectOffset(this, this.GetRectOffsetPtr(2)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(2, value.m_Ptr);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000BBD6 File Offset: 0x00009DD6
		public RectOffset overflow
		{
			get
			{
				RectOffset rectOffset;
				if ((rectOffset = this.m_Overflow) == null)
				{
					rectOffset = (this.m_Overflow = new RectOffset(this, this.GetRectOffsetPtr(3)));
				}
				return rectOffset;
			}
			set
			{
				this.AssignRectOffset(3, value.m_Ptr);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000BBE7 File Offset: 0x00009DE7
		public float lineHeight
		{
			get
			{
				return Mathf.Round(GUIStyle.Internal_GetLineHeight(this.m_Ptr));
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000BBF9 File Offset: 0x00009DF9
		public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.none, -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000BC10 File Offset: 0x00009E10
		public void Draw(Rect position, string text, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.Temp(text), -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000BC29 File Offset: 0x00009E29
		public void Draw(Rect position, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, GUIContent.Temp(image), -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000BC42 File Offset: 0x00009E42
		public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			this.Draw(position, content, -1, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000BC56 File Offset: 0x00009E56
		public void Draw(Rect position, GUIContent content, int controlID)
		{
			this.Draw(position, content, controlID, false, false, false, false);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000BC67 File Offset: 0x00009E67
		public void Draw(Rect position, GUIContent content, int controlID, bool on)
		{
			this.Draw(position, content, controlID, false, false, on, false);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000BC79 File Offset: 0x00009E79
		public void Draw(Rect position, GUIContent content, int controlID, bool on, bool hover)
		{
			this.Draw(position, content, controlID, hover, GUIUtility.hotControl == controlID, on, GUIUtility.HasKeyFocus(controlID));
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000BC98 File Offset: 0x00009E98
		private void Draw(Rect position, GUIContent content, int controlId, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			bool flag = controlId == -1;
			if (flag)
			{
				this.Internal_Draw(position, content, isHover, isActive, on, hasKeyboardFocus);
			}
			else
			{
				this.Internal_Draw2(position, content, controlId, on);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		public void DrawCursor(Rect position, GUIContent content, int controlID, int character)
		{
			Event current = Event.current;
			bool flag = current.type == EventType.Repaint;
			if (flag)
			{
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				bool flag2 = cursorFlashSpeed == 0f || num < 0.5f;
				if (flag2)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				this.Internal_DrawCursor(position, content, character, cursorColor);
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000BD68 File Offset: 0x00009F68
		internal void DrawWithTextSelection(Rect position, GUIContent content, bool isActive, bool hasKeyboardFocus, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition, Color selectionColor)
		{
			Color cursorColor = new Color(0f, 0f, 0f, 0f);
			float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
			float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
			bool flag = cursorFlashSpeed == 0f || num < 0.5f;
			if (flag)
			{
				cursorColor = GUI.skin.settings.cursorColor;
			}
			bool flag2 = position.Contains(Event.current.mousePosition);
			this.Internal_DrawWithTextSelection(position, content, flag2, isActive, false, hasKeyboardFocus, drawSelectionAsComposition, firstSelectedCharacter, lastSelectedCharacter, cursorColor, selectionColor);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000BE08 File Offset: 0x0000A008
		internal void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition)
		{
			this.DrawWithTextSelection(position, content, controlID == GUIUtility.hotControl, controlID == GUIUtility.keyboardControl && GUIStyle.showKeyboardFocus, firstSelectedCharacter, lastSelectedCharacter, drawSelectionAsComposition, GUI.skin.settings.selectionColor);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000BE4C File Offset: 0x0000A04C
		public void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
		{
			this.DrawWithTextSelection(position, content, controlID, firstSelectedCharacter, lastSelectedCharacter, false);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000BE60 File Offset: 0x0000A060
		public static implicit operator GUIStyle(string str)
		{
			bool flag = GUISkin.current == null;
			GUIStyle guistyle;
			if (flag)
			{
				Debug.LogError("Unable to use a named GUIStyle without a current skin. Most likely you need to move your GUIStyle initialization code to OnGUI");
				guistyle = GUISkin.error;
			}
			else
			{
				guistyle = GUISkin.current.GetStyle(str);
			}
			return guistyle;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		public static GUIStyle none
		{
			get
			{
				GUIStyle guistyle;
				if ((guistyle = GUIStyle.s_None) == null)
				{
					guistyle = (GUIStyle.s_None = new GUIStyle());
				}
				return guistyle;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			return this.Internal_GetCursorPixelPosition(position, content, cursorStringIndex);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public int GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return this.Internal_GetCursorStringIndex(position, content, cursorPixelPosition);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		internal int GetNumCharactersThatFitWithinWidth(string text, float width)
		{
			return this.Internal_GetNumCharactersThatFitWithinWidth(text, width);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000BF0C File Offset: 0x0000A10C
		public Vector2 CalcSize(GUIContent content)
		{
			return this.Internal_CalcSize(content);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BF28 File Offset: 0x0000A128
		internal Vector2 CalcSizeWithConstraints(GUIContent content, Vector2 constraints)
		{
			return this.Internal_CalcSizeWithConstraints(content, constraints);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000BF44 File Offset: 0x0000A144
		public Vector2 CalcScreenSize(Vector2 contentSize)
		{
			return new Vector2((this.fixedWidth != 0f) ? this.fixedWidth : Mathf.Ceil(contentSize.x + (float)this.padding.left + (float)this.padding.right), (this.fixedHeight != 0f) ? this.fixedHeight : Mathf.Ceil(contentSize.y + (float)this.padding.top + (float)this.padding.bottom));
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BFD0 File Offset: 0x0000A1D0
		public float CalcHeight(GUIContent content, float width)
		{
			return this.Internal_CalcHeight(content, width);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000BFEA File Offset: 0x0000A1EA
		public bool isHeightDependantOnWidth
		{
			get
			{
				return this.fixedHeight == 0f && this.wordWrap && this.imagePosition != ImagePosition.ImageOnly;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C014 File Offset: 0x0000A214
		public void CalcMinMaxWidth(GUIContent content, out float minWidth, out float maxWidth)
		{
			Vector2 vector = this.Internal_CalcMinMaxWidth(content);
			minWidth = vector.x;
			maxWidth = vector.y;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000C03C File Offset: 0x0000A23C
		public override string ToString()
		{
			return UnityString.Format("GUIStyle '{0}'", new object[] { this.name });
		}

		// Token: 0x06000354 RID: 852
		[MethodImpl(4096)]
		private extern void get_contentOffset_Injected(out Vector2 ret);

		// Token: 0x06000355 RID: 853
		[MethodImpl(4096)]
		private extern void set_contentOffset_Injected(ref Vector2 value);

		// Token: 0x06000356 RID: 854
		[MethodImpl(4096)]
		private extern void get_clipOffset_Injected(out Vector2 ret);

		// Token: 0x06000357 RID: 855
		[MethodImpl(4096)]
		private extern void set_clipOffset_Injected(ref Vector2 value);

		// Token: 0x06000358 RID: 856
		[MethodImpl(4096)]
		private extern void get_Internal_clipOffset_Injected(out Vector2 ret);

		// Token: 0x06000359 RID: 857
		[MethodImpl(4096)]
		private extern void set_Internal_clipOffset_Injected(ref Vector2 value);

		// Token: 0x0600035A RID: 858
		[MethodImpl(4096)]
		private extern void Internal_Draw_Injected(ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus);

		// Token: 0x0600035B RID: 859
		[MethodImpl(4096)]
		private extern void Internal_Draw2_Injected(ref Rect position, GUIContent content, int controlID, bool on);

		// Token: 0x0600035C RID: 860
		[MethodImpl(4096)]
		private extern void Internal_DrawCursor_Injected(ref Rect position, GUIContent content, int pos, ref Color cursorColor);

		// Token: 0x0600035D RID: 861
		[MethodImpl(4096)]
		private extern void Internal_DrawWithTextSelection_Injected(ref Rect screenRect, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus, bool drawSelectionAsComposition, int cursorFirst, int cursorLast, ref Color cursorColor, ref Color selectionColor);

		// Token: 0x0600035E RID: 862
		[MethodImpl(4096)]
		private extern void Internal_GetCursorPixelPosition_Injected(ref Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret);

		// Token: 0x0600035F RID: 863
		[MethodImpl(4096)]
		private extern int Internal_GetCursorStringIndex_Injected(ref Rect position, GUIContent content, ref Vector2 cursorPixelPosition);

		// Token: 0x06000360 RID: 864
		[MethodImpl(4096)]
		private extern string Internal_GetSelectedRenderedText_Injected(ref Rect localPosition, GUIContent mContent, int selectIndex, int cursorIndex);

		// Token: 0x06000361 RID: 865
		[MethodImpl(4096)]
		private extern Rect[] Internal_GetHyperlinksRect_Injected(ref Rect localPosition, GUIContent mContent);

		// Token: 0x06000362 RID: 866
		[MethodImpl(4096)]
		private extern void Internal_CalcSize_Injected(GUIContent content, out Vector2 ret);

		// Token: 0x06000363 RID: 867
		[MethodImpl(4096)]
		private extern void Internal_CalcSizeWithConstraints_Injected(GUIContent content, ref Vector2 maxSize, out Vector2 ret);

		// Token: 0x06000364 RID: 868
		[MethodImpl(4096)]
		private extern void Internal_CalcMinMaxWidth_Injected(GUIContent content, out Vector2 ret);

		// Token: 0x06000365 RID: 869
		[MethodImpl(4096)]
		private static extern void SetMouseTooltip_Injected(string tooltip, ref Rect screenRect);

		// Token: 0x040000CB RID: 203
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040000CC RID: 204
		[NonSerialized]
		private GUIStyleState m_Normal;

		// Token: 0x040000CD RID: 205
		[NonSerialized]
		private GUIStyleState m_Hover;

		// Token: 0x040000CE RID: 206
		[NonSerialized]
		private GUIStyleState m_Active;

		// Token: 0x040000CF RID: 207
		[NonSerialized]
		private GUIStyleState m_Focused;

		// Token: 0x040000D0 RID: 208
		[NonSerialized]
		private GUIStyleState m_OnNormal;

		// Token: 0x040000D1 RID: 209
		[NonSerialized]
		private GUIStyleState m_OnHover;

		// Token: 0x040000D2 RID: 210
		[NonSerialized]
		private GUIStyleState m_OnActive;

		// Token: 0x040000D3 RID: 211
		[NonSerialized]
		private GUIStyleState m_OnFocused;

		// Token: 0x040000D4 RID: 212
		[NonSerialized]
		private RectOffset m_Border;

		// Token: 0x040000D5 RID: 213
		[NonSerialized]
		private RectOffset m_Padding;

		// Token: 0x040000D6 RID: 214
		[NonSerialized]
		private RectOffset m_Margin;

		// Token: 0x040000D7 RID: 215
		[NonSerialized]
		private RectOffset m_Overflow;

		// Token: 0x040000D8 RID: 216
		[NonSerialized]
		private string m_Name;

		// Token: 0x040000D9 RID: 217
		internal static bool showKeyboardFocus = true;

		// Token: 0x040000DA RID: 218
		private static GUIStyle s_None;
	}
}

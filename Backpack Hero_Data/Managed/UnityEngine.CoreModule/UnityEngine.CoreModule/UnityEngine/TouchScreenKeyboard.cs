using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000250 RID: 592
	[NativeHeader("Runtime/Export/TouchScreenKeyboard/TouchScreenKeyboard.bindings.h")]
	[NativeConditional("ENABLE_ONSCREEN_KEYBOARD")]
	[NativeHeader("Runtime/Input/KeyboardOnScreen.h")]
	public class TouchScreenKeyboard
	{
		// Token: 0x060019AC RID: 6572
		[FreeFunction("TouchScreenKeyboard_Destroy", IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x060019AD RID: 6573 RVA: 0x000296F4 File Offset: 0x000278F4
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				TouchScreenKeyboard.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00029738 File Offset: 0x00027938
		~TouchScreenKeyboard()
		{
			this.Destroy();
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00029768 File Offset: 0x00027968
		public TouchScreenKeyboard(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder, int characterLimit)
		{
			TouchScreenKeyboard_InternalConstructorHelperArguments touchScreenKeyboard_InternalConstructorHelperArguments = default(TouchScreenKeyboard_InternalConstructorHelperArguments);
			touchScreenKeyboard_InternalConstructorHelperArguments.keyboardType = Convert.ToUInt32(keyboardType);
			touchScreenKeyboard_InternalConstructorHelperArguments.autocorrection = Convert.ToUInt32(autocorrection);
			touchScreenKeyboard_InternalConstructorHelperArguments.multiline = Convert.ToUInt32(multiline);
			touchScreenKeyboard_InternalConstructorHelperArguments.secure = Convert.ToUInt32(secure);
			touchScreenKeyboard_InternalConstructorHelperArguments.alert = Convert.ToUInt32(alert);
			touchScreenKeyboard_InternalConstructorHelperArguments.characterLimit = characterLimit;
			this.m_Ptr = TouchScreenKeyboard.TouchScreenKeyboard_InternalConstructorHelper(ref touchScreenKeyboard_InternalConstructorHelperArguments, text, textPlaceholder);
		}

		// Token: 0x060019B0 RID: 6576
		[FreeFunction("TouchScreenKeyboard_InternalConstructorHelper")]
		[MethodImpl(4096)]
		private static extern IntPtr TouchScreenKeyboard_InternalConstructorHelper(ref TouchScreenKeyboard_InternalConstructorHelperArguments arguments, string text, string textPlaceholder);

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x000297E8 File Offset: 0x000279E8
		public static bool isSupported
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				RuntimePlatform runtimePlatform = platform;
				RuntimePlatform runtimePlatform2 = runtimePlatform;
				if (runtimePlatform2 <= RuntimePlatform.Android)
				{
					if (runtimePlatform2 != RuntimePlatform.IPhonePlayer && runtimePlatform2 != RuntimePlatform.Android)
					{
						goto IL_005C;
					}
				}
				else if (runtimePlatform2 - RuntimePlatform.MetroPlayerX86 > 2 && runtimePlatform2 != RuntimePlatform.PS4)
				{
					switch (runtimePlatform2)
					{
					case RuntimePlatform.tvOS:
					case RuntimePlatform.Switch:
					case RuntimePlatform.Stadia:
					case RuntimePlatform.GameCoreXboxSeries:
					case RuntimePlatform.GameCoreXboxOne:
					case RuntimePlatform.PS5:
						break;
					case RuntimePlatform.Lumin:
					case RuntimePlatform.CloudRendering:
						goto IL_005C;
					default:
						goto IL_005C;
					}
				}
				return true;
				IL_005C:
				return false;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00029856 File Offset: 0x00027A56
		// (set) Token: 0x060019B3 RID: 6579 RVA: 0x0002985D File Offset: 0x00027A5D
		internal static bool disableInPlaceEditing { get; set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00029868 File Offset: 0x00027A68
		public static bool isInPlaceEditingAllowed
		{
			get
			{
				bool disableInPlaceEditing = TouchScreenKeyboard.disableInPlaceEditing;
				return disableInPlaceEditing && false;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00029888 File Offset: 0x00027A88
		internal static bool isRequiredToForceOpen
		{
			get
			{
				return TouchScreenKeyboard.IsRequiredToForceOpen();
			}
		}

		// Token: 0x060019B6 RID: 6582
		[FreeFunction("TouchScreenKeyboard_IsRequiredToForceOpen")]
		[MethodImpl(4096)]
		private static extern bool IsRequiredToForceOpen();

		// Token: 0x060019B7 RID: 6583 RVA: 0x000298A0 File Offset: 0x00027AA0
		public static TouchScreenKeyboard Open(string text, [DefaultValue("TouchScreenKeyboardType.Default")] TouchScreenKeyboardType keyboardType, [DefaultValue("true")] bool autocorrection, [DefaultValue("false")] bool multiline, [DefaultValue("false")] bool secure, [DefaultValue("false")] bool alert, [DefaultValue("\"\"")] string textPlaceholder, [DefaultValue("0")] int characterLimit)
		{
			return new TouchScreenKeyboard(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, characterLimit);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000298C4 File Offset: 0x00027AC4
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert, string textPlaceholder)
		{
			int num = 0;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, textPlaceholder, num);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000298E8 File Offset: 0x00027AE8
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert)
		{
			int num = 0;
			string text2 = "";
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, text2, num);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00029914 File Offset: 0x00027B14
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, flag, text2, num);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00029940 File Offset: 0x00027B40
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, flag2, flag, text2, num);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00029970 File Offset: 0x00027B70
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, flag3, flag2, flag, text2, num);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000299A4 File Offset: 0x00027BA4
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			return TouchScreenKeyboard.Open(text, keyboardType, flag4, flag3, flag2, flag, text2, num);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000299DC File Offset: 0x00027BDC
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text)
		{
			int num = 0;
			string text2 = "";
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			TouchScreenKeyboardType touchScreenKeyboardType = TouchScreenKeyboardType.Default;
			return TouchScreenKeyboard.Open(text, touchScreenKeyboardType, flag4, flag3, flag2, flag, text2, num);
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060019BF RID: 6591
		// (set) Token: 0x060019C0 RID: 6592
		public extern string text
		{
			[NativeName("GetText")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetText")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060019C1 RID: 6593
		// (set) Token: 0x060019C2 RID: 6594
		public static extern bool hideInput
		{
			[NativeName("IsInputHidden")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetInputHidden")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060019C3 RID: 6595
		// (set) Token: 0x060019C4 RID: 6596
		public extern bool active
		{
			[NativeName("IsActive")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetActive")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060019C5 RID: 6597
		[FreeFunction("TouchScreenKeyboard_GetDone")]
		[MethodImpl(4096)]
		private static extern bool GetDone(IntPtr ptr);

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00029A18 File Offset: 0x00027C18
		[Obsolete("Property done is deprecated, use status instead")]
		public bool done
		{
			get
			{
				return TouchScreenKeyboard.GetDone(this.m_Ptr);
			}
		}

		// Token: 0x060019C7 RID: 6599
		[FreeFunction("TouchScreenKeyboard_GetWasCanceled")]
		[MethodImpl(4096)]
		private static extern bool GetWasCanceled(IntPtr ptr);

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x00029A38 File Offset: 0x00027C38
		[Obsolete("Property wasCanceled is deprecated, use status instead.")]
		public bool wasCanceled
		{
			get
			{
				return TouchScreenKeyboard.GetWasCanceled(this.m_Ptr);
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060019C9 RID: 6601
		public extern TouchScreenKeyboard.Status status
		{
			[NativeName("GetKeyboardStatus")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060019CA RID: 6602
		// (set) Token: 0x060019CB RID: 6603
		public extern int characterLimit
		{
			[NativeName("GetCharacterLimit")]
			[MethodImpl(4096)]
			get;
			[NativeName("SetCharacterLimit")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060019CC RID: 6604
		public extern bool canGetSelection
		{
			[NativeName("CanGetSelection")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060019CD RID: 6605
		public extern bool canSetSelection
		{
			[NativeName("CanSetSelection")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x00029A58 File Offset: 0x00027C58
		// (set) Token: 0x060019CF RID: 6607 RVA: 0x00029A80 File Offset: 0x00027C80
		public RangeInt selection
		{
			get
			{
				RangeInt rangeInt;
				TouchScreenKeyboard.GetSelection(out rangeInt.start, out rangeInt.length);
				return rangeInt;
			}
			set
			{
				bool flag = value.start < 0 || value.length < 0 || value.start + value.length > this.text.Length;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("selection", "Selection is out of range.");
				}
				TouchScreenKeyboard.SetSelection(value.start, value.length);
			}
		}

		// Token: 0x060019D0 RID: 6608
		[MethodImpl(4096)]
		private static extern void GetSelection(out int start, out int length);

		// Token: 0x060019D1 RID: 6609
		[MethodImpl(4096)]
		private static extern void SetSelection(int start, int length);

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060019D2 RID: 6610
		public extern TouchScreenKeyboardType type
		{
			[NativeName("GetKeyboardType")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x00029AE4 File Offset: 0x00027CE4
		// (set) Token: 0x060019D4 RID: 6612 RVA: 0x00004557 File Offset: 0x00002757
		public int targetDisplay
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x00029AF8 File Offset: 0x00027CF8
		[NativeConditional("ENABLE_ONSCREEN_KEYBOARD", "RectT<float>()")]
		public static Rect area
		{
			[NativeName("GetRect")]
			get
			{
				Rect rect;
				TouchScreenKeyboard.get_area_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060019D6 RID: 6614
		public static extern bool visible
		{
			[NativeName("IsVisible")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060019D7 RID: 6615
		[MethodImpl(4096)]
		private static extern void get_area_Injected(out Rect ret);

		// Token: 0x04000877 RID: 2167
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x02000251 RID: 593
		public enum Status
		{
			// Token: 0x0400087A RID: 2170
			Visible,
			// Token: 0x0400087B RID: 2171
			Done,
			// Token: 0x0400087C RID: 2172
			Canceled,
			// Token: 0x0400087D RID: 2173
			LostFocus
		}

		// Token: 0x02000252 RID: 594
		public class Android
		{
			// Token: 0x1700052E RID: 1326
			// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00029B10 File Offset: 0x00027D10
			// (set) Token: 0x060019D9 RID: 6617 RVA: 0x00029B27 File Offset: 0x00027D27
			[Obsolete("TouchScreenKeyboard.Android.closeKeyboardOnOutsideTap is obsolete. Use TouchScreenKeyboard.Android.consumesOutsideTouches instead (UnityUpgradable) -> UnityEngine.TouchScreenKeyboard/Android.consumesOutsideTouches")]
			public static bool closeKeyboardOnOutsideTap
			{
				get
				{
					return TouchScreenKeyboard.Android.consumesOutsideTouches;
				}
				set
				{
					TouchScreenKeyboard.Android.consumesOutsideTouches = value;
				}
			}

			// Token: 0x1700052F RID: 1327
			// (get) Token: 0x060019DB RID: 6619 RVA: 0x00029B3C File Offset: 0x00027D3C
			// (set) Token: 0x060019DA RID: 6618 RVA: 0x00029B31 File Offset: 0x00027D31
			public static bool consumesOutsideTouches
			{
				get
				{
					return TouchScreenKeyboard.Android.TouchScreenKeyboard_GetAndroidKeyboardConsumesOutsideTouches();
				}
				set
				{
					TouchScreenKeyboard.Android.TouchScreenKeyboard_SetAndroidKeyboardConsumesOutsideTouches(value);
				}
			}

			// Token: 0x060019DC RID: 6620
			[NativeConditional("PLATFORM_ANDROID")]
			[FreeFunction("TouchScreenKeyboard_SetAndroidKeyboardConsumesOutsideTouches")]
			[MethodImpl(4096)]
			private static extern void TouchScreenKeyboard_SetAndroidKeyboardConsumesOutsideTouches(bool enable);

			// Token: 0x060019DD RID: 6621
			[FreeFunction("TouchScreenKeyboard_GetAndroidKeyboardConsumesOutsideTouches")]
			[NativeConditional("PLATFORM_ANDROID")]
			[MethodImpl(4096)]
			private static extern bool TouchScreenKeyboard_GetAndroidKeyboardConsumesOutsideTouches();
		}
	}
}

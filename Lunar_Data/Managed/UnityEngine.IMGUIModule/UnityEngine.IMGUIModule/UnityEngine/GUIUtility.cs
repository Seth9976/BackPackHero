using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000032 RID: 50
	[NativeHeader("Runtime/Utilities/CopyPaste.h")]
	[NativeHeader("Runtime/Camera/RenderLayers/GUITexture.h")]
	[NativeHeader("Runtime/Input/InputManager.h")]
	[NativeHeader("Modules/IMGUI/GUIManager.h")]
	[NativeHeader("Runtime/Input/InputBindings.h")]
	[NativeHeader("Modules/IMGUI/GUIUtility.h")]
	public class GUIUtility
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000375 RID: 885
		public static extern bool hasModalWindow
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000376 RID: 886
		[NativeProperty("GetGUIState().m_PixelsPerPoint", true, TargetType.Field)]
		internal static extern float pixelsPerPoint
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000377 RID: 887
		[NativeProperty("GetGUIState().m_OnGUIDepth", true, TargetType.Field)]
		internal static extern int guiDepth
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000C230 File Offset: 0x0000A430
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000C245 File Offset: 0x0000A445
		internal static Vector2 s_EditorScreenPointOffset
		{
			[NativeMethod("GetGUIState().GetGUIPixelOffset", true)]
			get
			{
				Vector2 vector;
				GUIUtility.get_s_EditorScreenPointOffset_Injected(out vector);
				return vector;
			}
			[NativeMethod("GetGUIState().SetGUIPixelOffset", true)]
			set
			{
				GUIUtility.set_s_EditorScreenPointOffset_Injected(ref value);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600037A RID: 890
		// (set) Token: 0x0600037B RID: 891
		[NativeProperty("GetGUIState().m_CanvasGUIState.m_IsMouseUsed", true, TargetType.Field)]
		internal static extern bool mouseUsed
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600037C RID: 892
		// (set) Token: 0x0600037D RID: 893
		[StaticAccessor("GetInputManager()", StaticAccessorType.Dot)]
		internal static extern bool textFieldInput
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600037E RID: 894
		// (set) Token: 0x0600037F RID: 895
		internal static extern bool manualTex2SRGBEnabled
		{
			[FreeFunction("GUITexture::IsManualTex2SRGBEnabled")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("GUITexture::SetManualTex2SRGBEnabled")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000380 RID: 896
		// (set) Token: 0x06000381 RID: 897
		public static extern string systemCopyBuffer
		{
			[FreeFunction("GetCopyBuffer")]
			[MethodImpl(4096)]
			get;
			[FreeFunction("SetCopyBuffer")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000C24E File Offset: 0x0000A44E
		[FreeFunction("GetGUIState().GetControlID")]
		private static int Internal_GetControlID(int hint, FocusType focusType, Rect rect)
		{
			return GUIUtility.Internal_GetControlID_Injected(hint, focusType, ref rect);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000C25C File Offset: 0x0000A45C
		public static int GetControlID(int hint, FocusType focusType, Rect rect)
		{
			GUIUtility.s_ControlCount++;
			return GUIUtility.Internal_GetControlID(hint, focusType, rect);
		}

		// Token: 0x06000384 RID: 900
		[MethodImpl(4096)]
		internal static extern void BeginContainerFromOwner(ScriptableObject owner);

		// Token: 0x06000385 RID: 901
		[MethodImpl(4096)]
		internal static extern void BeginContainer(ObjectGUIState objectGUIState);

		// Token: 0x06000386 RID: 902
		[NativeMethod("EndContainer")]
		[MethodImpl(4096)]
		internal static extern void Internal_EndContainer();

		// Token: 0x06000387 RID: 903
		[FreeFunction("GetSpecificGUIState(0).m_EternalGUIState->GetNextUniqueID")]
		[MethodImpl(4096)]
		internal static extern int GetPermanentControlID();

		// Token: 0x06000388 RID: 904
		[MethodImpl(4096)]
		internal static extern int CheckForTabEvent(Event evt);

		// Token: 0x06000389 RID: 905
		[MethodImpl(4096)]
		internal static extern void SetKeyboardControlToFirstControlId();

		// Token: 0x0600038A RID: 906
		[MethodImpl(4096)]
		internal static extern void SetKeyboardControlToLastControlId();

		// Token: 0x0600038B RID: 907
		[MethodImpl(4096)]
		internal static extern bool HasFocusableControls();

		// Token: 0x0600038C RID: 908
		[MethodImpl(4096)]
		internal static extern bool OwnsId(int id);

		// Token: 0x0600038D RID: 909 RVA: 0x0000C284 File Offset: 0x0000A484
		public static Rect AlignRectToDevice(Rect rect, out int widthInPixels, out int heightInPixels)
		{
			Rect rect2;
			GUIUtility.AlignRectToDevice_Injected(ref rect, out widthInPixels, out heightInPixels, out rect2);
			return rect2;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600038E RID: 910
		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static extern string compositionString
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600038F RID: 911
		// (set) Token: 0x06000390 RID: 912
		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static extern IMECompositionMode imeCompositionMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000C2A0 File Offset: 0x0000A4A0
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000C2B5 File Offset: 0x0000A4B5
		[StaticAccessor("InputBindings", StaticAccessorType.DoubleColon)]
		internal static Vector2 compositionCursorPos
		{
			get
			{
				Vector2 vector;
				GUIUtility.get_compositionCursorPos_Injected(out vector);
				return vector;
			}
			set
			{
				GUIUtility.set_compositionCursorPos_Injected(ref value);
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		internal static Vector3 Internal_MultiplyPoint(Vector3 point, Matrix4x4 transform)
		{
			Vector3 vector;
			GUIUtility.Internal_MultiplyPoint_Injected(ref point, ref transform, out vector);
			return vector;
		}

		// Token: 0x06000394 RID: 916
		[MethodImpl(4096)]
		internal static extern bool GetChanged();

		// Token: 0x06000395 RID: 917
		[MethodImpl(4096)]
		internal static extern void SetChanged(bool changed);

		// Token: 0x06000396 RID: 918
		[MethodImpl(4096)]
		internal static extern void SetDidGUIWindowsEatLastEvent(bool value);

		// Token: 0x06000397 RID: 919
		[MethodImpl(4096)]
		private static extern int Internal_GetHotControl();

		// Token: 0x06000398 RID: 920
		[MethodImpl(4096)]
		private static extern int Internal_GetKeyboardControl();

		// Token: 0x06000399 RID: 921
		[MethodImpl(4096)]
		private static extern void Internal_SetHotControl(int value);

		// Token: 0x0600039A RID: 922
		[MethodImpl(4096)]
		private static extern void Internal_SetKeyboardControl(int value);

		// Token: 0x0600039B RID: 923
		[MethodImpl(4096)]
		private static extern object Internal_GetDefaultSkin(int skinMode);

		// Token: 0x0600039C RID: 924
		[MethodImpl(4096)]
		private static extern Object Internal_GetBuiltinSkin(int skin);

		// Token: 0x0600039D RID: 925
		[MethodImpl(4096)]
		private static extern void Internal_ExitGUI();

		// Token: 0x0600039E RID: 926 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		private static Vector2 InternalWindowToScreenPoint(Vector2 windowPoint)
		{
			Vector2 vector;
			GUIUtility.InternalWindowToScreenPoint_Injected(ref windowPoint, out vector);
			return vector;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
		private static Vector2 InternalScreenToWindowPoint(Vector2 screenPoint)
		{
			Vector2 vector;
			GUIUtility.InternalScreenToWindowPoint_Injected(ref screenPoint, out vector);
			return vector;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000C30B File Offset: 0x0000A50B
		[RequiredByNativeCode]
		private static void MarkGUIChanged()
		{
			Action action = GUIUtility.guiChanged;
			if (action != null)
			{
				action.Invoke();
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000C320 File Offset: 0x0000A520
		public static int GetControlID(FocusType focus)
		{
			return GUIUtility.GetControlID(0, focus);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000C33C File Offset: 0x0000A53C
		public static int GetControlID(GUIContent contents, FocusType focus)
		{
			return GUIUtility.GetControlID(contents.hash, focus);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000C35C File Offset: 0x0000A55C
		public static int GetControlID(FocusType focus, Rect position)
		{
			return GUIUtility.GetControlID(0, focus, position);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000C378 File Offset: 0x0000A578
		public static int GetControlID(GUIContent contents, FocusType focus, Rect position)
		{
			return GUIUtility.GetControlID(contents.hash, focus, position);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000C398 File Offset: 0x0000A598
		public static int GetControlID(int hint, FocusType focus)
		{
			return GUIUtility.GetControlID(hint, focus, Rect.zero);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public static object GetStateObject(Type t, int controlID)
		{
			return GUIStateObjects.GetStateObject(t, controlID);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000C3D4 File Offset: 0x0000A5D4
		public static object QueryStateObject(Type t, int controlID)
		{
			return GUIStateObjects.QueryStateObject(t, controlID);
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000C3ED File Offset: 0x0000A5ED
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		internal static bool guiIsExiting { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0000C413 File Offset: 0x0000A613
		public static int hotControl
		{
			get
			{
				return GUIUtility.Internal_GetHotControl();
			}
			set
			{
				GUIUtility.Internal_SetHotControl(value);
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000C41D File Offset: 0x0000A61D
		[RequiredByNativeCode]
		internal static void TakeCapture()
		{
			Action action = GUIUtility.takeCapture;
			if (action != null)
			{
				action.Invoke();
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000C431 File Offset: 0x0000A631
		[RequiredByNativeCode]
		internal static void RemoveCapture()
		{
			Action action = GUIUtility.releaseCapture;
			if (action != null)
			{
				action.Invoke();
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000C448 File Offset: 0x0000A648
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0000C45F File Offset: 0x0000A65F
		public static int keyboardControl
		{
			get
			{
				return GUIUtility.Internal_GetKeyboardControl();
			}
			set
			{
				GUIUtility.Internal_SetKeyboardControl(value);
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000C46C File Offset: 0x0000A66C
		internal static bool HasKeyFocus(int controlID)
		{
			return controlID == GUIUtility.keyboardControl && (GUIUtility.s_HasCurrentWindowKeyFocusFunc == null || GUIUtility.s_HasCurrentWindowKeyFocusFunc.Invoke());
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000C49D File Offset: 0x0000A69D
		public static void ExitGUI()
		{
			throw new ExitGUIException();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		internal static GUISkin GetDefaultSkin(int skinMode)
		{
			return GUIUtility.Internal_GetDefaultSkin(skinMode) as GUISkin;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		internal static GUISkin GetDefaultSkin()
		{
			return GUIUtility.Internal_GetDefaultSkin(GUIUtility.s_SkinMode) as GUISkin;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		internal static GUISkin GetBuiltinSkin(int skin)
		{
			return GUIUtility.Internal_GetBuiltinSkin(skin) as GUISkin;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000C50C File Offset: 0x0000A70C
		[RequiredByNativeCode]
		internal static void ProcessEvent(int instanceID, IntPtr nativeEventPtr, out bool result)
		{
			result = false;
			bool flag = GUIUtility.processEvent != null;
			if (flag)
			{
				result = GUIUtility.processEvent.Invoke(instanceID, nativeEventPtr);
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000C537 File Offset: 0x0000A737
		internal static void EndContainer()
		{
			GUIUtility.Internal_EndContainer();
			GUIUtility.Internal_ExitGUI();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000C546 File Offset: 0x0000A746
		internal static void CleanupRoots()
		{
			Action action = GUIUtility.cleanupRoots;
			if (action != null)
			{
				action.Invoke();
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000C55C File Offset: 0x0000A75C
		[RequiredByNativeCode]
		internal static void BeginGUI(int skinMode, int instanceID, int useGUILayout)
		{
			GUIUtility.s_SkinMode = skinMode;
			GUIUtility.s_OriginalID = instanceID;
			GUIUtility.ResetGlobalState();
			bool flag = useGUILayout != 0;
			if (flag)
			{
				GUILayoutUtility.Begin(instanceID);
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000C590 File Offset: 0x0000A790
		[RequiredByNativeCode]
		internal static void EndGUI(int layoutType)
		{
			try
			{
				bool flag = Event.current.type == EventType.Layout;
				if (flag)
				{
					switch (layoutType)
					{
					case 1:
						GUILayoutUtility.Layout();
						break;
					case 2:
						GUILayoutUtility.LayoutFromEditorWindow();
						break;
					}
				}
				GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
				GUIContent.ClearStaticCache();
			}
			finally
			{
				GUIUtility.Internal_ExitGUI();
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000C60C File Offset: 0x0000A80C
		[RequiredByNativeCode]
		internal static bool EndGUIFromException(Exception exception)
		{
			GUIUtility.Internal_ExitGUI();
			return GUIUtility.ShouldRethrowException(exception);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000C62C File Offset: 0x0000A82C
		[RequiredByNativeCode]
		internal static bool EndContainerGUIFromException(Exception exception)
		{
			bool flag = GUIUtility.endContainerGUIFromException != null;
			return flag && GUIUtility.endContainerGUIFromException.Invoke(exception);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000C659 File Offset: 0x0000A859
		internal static void ResetGlobalState()
		{
			GUI.skin = null;
			GUIUtility.guiIsExiting = false;
			GUI.changed = false;
			GUI.scrollViewStates.Clear();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000C67C File Offset: 0x0000A87C
		internal static bool IsExitGUIException(Exception exception)
		{
			while (exception is TargetInvocationException && exception.InnerException != null)
			{
				exception = exception.InnerException;
			}
			return exception is ExitGUIException;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		internal static bool ShouldRethrowException(Exception exception)
		{
			return GUIUtility.IsExitGUIException(exception);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		internal static void CheckOnGUI()
		{
			bool flag = GUIUtility.guiDepth <= 0;
			if (flag)
			{
				throw new ArgumentException("You can only call GUI functions from inside OnGUI.");
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		internal static float RoundToPixelGrid(float v)
		{
			return Mathf.Floor(v * GUIUtility.pixelsPerPoint + 0.48f) / GUIUtility.pixelsPerPoint;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000C728 File Offset: 0x0000A928
		public static Vector2 GUIToScreenPoint(Vector2 guiPoint)
		{
			return GUIUtility.InternalWindowToScreenPoint(GUIClip.UnclipToWindow(guiPoint));
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000C748 File Offset: 0x0000A948
		public static Rect GUIToScreenRect(Rect guiRect)
		{
			Vector2 vector = GUIUtility.GUIToScreenPoint(new Vector2(guiRect.x, guiRect.y));
			guiRect.x = vector.x;
			guiRect.y = vector.y;
			return guiRect;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000C790 File Offset: 0x0000A990
		public static Vector2 ScreenToGUIPoint(Vector2 screenPoint)
		{
			return GUIClip.ClipToWindow(GUIUtility.InternalScreenToWindowPoint(screenPoint));
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		public static Rect ScreenToGUIRect(Rect screenRect)
		{
			Vector2 vector = GUIUtility.ScreenToGUIPoint(new Vector2(screenRect.x, screenRect.y));
			screenRect.x = vector.x;
			screenRect.y = vector.y;
			return screenRect;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		public static void RotateAroundPivot(float angle, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			GUI.matrix = Matrix4x4.identity;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.Euler(0f, 0f, angle), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = matrix4x * matrix;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000C86C File Offset: 0x0000AA6C
		public static void ScaleAroundPivot(Vector2 scale, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.identity, new Vector3(scale.x, scale.y, 1f)) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = matrix4x * matrix;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000C8DC File Offset: 0x0000AADC
		public static Rect AlignRectToDevice(Rect rect)
		{
			int num;
			int num2;
			return GUIUtility.AlignRectToDevice(rect, out num, out num2);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		internal static bool HitTest(Rect rect, Vector2 point, int offset)
		{
			return point.x >= rect.xMin - (float)offset && point.x < rect.xMax + (float)offset && point.y >= rect.yMin - (float)offset && point.y < rect.yMax + (float)offset;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000C958 File Offset: 0x0000AB58
		internal static bool HitTest(Rect rect, Vector2 point, bool isDirectManipulationDevice)
		{
			int num = 0;
			return GUIUtility.HitTest(rect, point, num);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000C974 File Offset: 0x0000AB74
		internal static bool HitTest(Rect rect, Event evt)
		{
			return GUIUtility.HitTest(rect, evt.mousePosition, evt.isDirectManipulationDevice);
		}

		// Token: 0x060003CC RID: 972
		[MethodImpl(4096)]
		private static extern void get_s_EditorScreenPointOffset_Injected(out Vector2 ret);

		// Token: 0x060003CD RID: 973
		[MethodImpl(4096)]
		private static extern void set_s_EditorScreenPointOffset_Injected(ref Vector2 value);

		// Token: 0x060003CE RID: 974
		[MethodImpl(4096)]
		private static extern int Internal_GetControlID_Injected(int hint, FocusType focusType, ref Rect rect);

		// Token: 0x060003CF RID: 975
		[MethodImpl(4096)]
		private static extern void AlignRectToDevice_Injected(ref Rect rect, out int widthInPixels, out int heightInPixels, out Rect ret);

		// Token: 0x060003D0 RID: 976
		[MethodImpl(4096)]
		private static extern void get_compositionCursorPos_Injected(out Vector2 ret);

		// Token: 0x060003D1 RID: 977
		[MethodImpl(4096)]
		private static extern void set_compositionCursorPos_Injected(ref Vector2 value);

		// Token: 0x060003D2 RID: 978
		[MethodImpl(4096)]
		private static extern void Internal_MultiplyPoint_Injected(ref Vector3 point, ref Matrix4x4 transform, out Vector3 ret);

		// Token: 0x060003D3 RID: 979
		[MethodImpl(4096)]
		private static extern void InternalWindowToScreenPoint_Injected(ref Vector2 windowPoint, out Vector2 ret);

		// Token: 0x060003D4 RID: 980
		[MethodImpl(4096)]
		private static extern void InternalScreenToWindowPoint_Injected(ref Vector2 screenPoint, out Vector2 ret);

		// Token: 0x040000E4 RID: 228
		internal static int s_ControlCount;

		// Token: 0x040000E5 RID: 229
		internal static int s_SkinMode;

		// Token: 0x040000E6 RID: 230
		internal static int s_OriginalID;

		// Token: 0x040000E7 RID: 231
		internal static Action takeCapture;

		// Token: 0x040000E8 RID: 232
		internal static Action releaseCapture;

		// Token: 0x040000E9 RID: 233
		internal static Func<int, IntPtr, bool> processEvent;

		// Token: 0x040000EA RID: 234
		internal static Action cleanupRoots;

		// Token: 0x040000EB RID: 235
		internal static Func<Exception, bool> endContainerGUIFromException;

		// Token: 0x040000EC RID: 236
		internal static Action guiChanged;

		// Token: 0x040000EE RID: 238
		internal static Func<bool> s_HasCurrentWindowKeyFocusFunc;
	}
}

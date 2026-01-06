using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Unity.VisualScripting.FullSerializer.Internal;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000184 RID: 388
	public class fsConverterRegistrar
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x0002B564 File Offset: 0x00029764
		static fsConverterRegistrar()
		{
			foreach (FieldInfo fieldInfo in typeof(fsConverterRegistrar).GetDeclaredFields())
			{
				if (fieldInfo.Name.StartsWith("Register_"))
				{
					fsConverterRegistrar.Converters.Add(fieldInfo.FieldType);
				}
			}
			foreach (MethodInfo methodInfo in typeof(fsConverterRegistrar).GetDeclaredMethods())
			{
				if (methodInfo.Name.StartsWith("Register_"))
				{
					methodInfo.Invoke(null, null);
				}
			}
		}

		// Token: 0x0400025E RID: 606
		public static AnimationCurve_DirectConverter Register_AnimationCurve_DirectConverter;

		// Token: 0x0400025F RID: 607
		public static Bounds_DirectConverter Register_Bounds_DirectConverter;

		// Token: 0x04000260 RID: 608
		public static Gradient_DirectConverter Register_Gradient_DirectConverter;

		// Token: 0x04000261 RID: 609
		public static GUIStyleState_DirectConverter Register_GUIStyleState_DirectConverter;

		// Token: 0x04000262 RID: 610
		public static GUIStyle_DirectConverter Register_GUIStyle_DirectConverter;

		// Token: 0x04000263 RID: 611
		[UsedImplicitly]
		public static InputAction_DirectConverter Register_InputAction_DirectConverter;

		// Token: 0x04000264 RID: 612
		public static Keyframe_DirectConverter Register_Keyframe_DirectConverter;

		// Token: 0x04000265 RID: 613
		public static LayerMask_DirectConverter Register_LayerMask_DirectConverter;

		// Token: 0x04000266 RID: 614
		public static RectOffset_DirectConverter Register_RectOffset_DirectConverter;

		// Token: 0x04000267 RID: 615
		public static Rect_DirectConverter Register_Rect_DirectConverter;

		// Token: 0x04000268 RID: 616
		public static List<Type> Converters = new List<Type>();
	}
}

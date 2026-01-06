using System;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000264 RID: 612
	internal static class UIRUtility
	{
		// Token: 0x0600125B RID: 4699 RVA: 0x0004830C File Offset: 0x0004650C
		[MethodImpl(256)]
		public static bool ShapeWindingIsClockwise(int maskDepth, int stencilRef)
		{
			Debug.Assert(maskDepth == stencilRef || maskDepth == stencilRef + 1);
			return maskDepth == stencilRef;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00048338 File Offset: 0x00046538
		public static Vector4 ToVector4(Rect rc)
		{
			return new Vector4(rc.xMin, rc.yMin, rc.xMax, rc.yMax);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0004836C File Offset: 0x0004656C
		public static bool IsRoundRect(VisualElement ve)
		{
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			return resolvedStyle.borderTopLeftRadius >= 1E-30f || resolvedStyle.borderTopRightRadius >= 1E-30f || resolvedStyle.borderBottomLeftRadius >= 1E-30f || resolvedStyle.borderBottomRightRadius >= 1E-30f;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x000483C0 File Offset: 0x000465C0
		public static void Multiply2D(this Quaternion rotation, ref Vector2 point)
		{
			float num = rotation.z * 2f;
			float num2 = 1f - rotation.z * num;
			float num3 = rotation.w * num;
			point = new Vector2(num2 * point.x - num3 * point.y, num3 * point.x + num2 * point.y);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00048420 File Offset: 0x00046620
		public static bool IsVectorImageBackground(VisualElement ve)
		{
			return ve.computedStyle.backgroundImage.vectorImage != null;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0004844C File Offset: 0x0004664C
		public static bool IsElementSelfHidden(VisualElement ve)
		{
			return ve.resolvedStyle.visibility == Visibility.Hidden;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004846C File Offset: 0x0004666C
		public static void Destroy(Object obj)
		{
			bool flag = obj == null;
			if (!flag)
			{
				bool isPlaying = Application.isPlaying;
				if (isPlaying)
				{
					Object.Destroy(obj);
				}
				else
				{
					Object.DestroyImmediate(obj);
				}
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x000484A0 File Offset: 0x000466A0
		public static int GetPrevPow2(int n)
		{
			int num = 0;
			while (n > 1)
			{
				n >>= 1;
				num++;
			}
			return 1 << num;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x000484D0 File Offset: 0x000466D0
		public static int GetNextPow2(int n)
		{
			int i;
			for (i = 1; i < n; i <<= 1)
			{
			}
			return i;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000484F4 File Offset: 0x000466F4
		public static int GetNextPow2Exp(int n)
		{
			int i = 1;
			int num = 0;
			while (i < n)
			{
				i <<= 1;
				num++;
			}
			return num;
		}

		// Token: 0x04000877 RID: 2167
		public static readonly string k_DefaultShaderName = Shaders.k_Runtime;

		// Token: 0x04000878 RID: 2168
		public static readonly string k_DefaultWorldSpaceShaderName = Shaders.k_RuntimeWorld;

		// Token: 0x04000879 RID: 2169
		public const float k_Epsilon = 1E-30f;

		// Token: 0x0400087A RID: 2170
		public const float k_ClearZ = 0.99f;

		// Token: 0x0400087B RID: 2171
		public const float k_MeshPosZ = 0f;

		// Token: 0x0400087C RID: 2172
		public const float k_MaskPosZ = 1f;

		// Token: 0x0400087D RID: 2173
		public const int k_MaxMaskDepth = 7;
	}
}

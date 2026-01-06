using System;
using Unity.Collections;
using UnityEngine.Assertions;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B8 RID: 696
	internal struct TextNativeHandle : ITextHandle
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x0005F4F6 File Offset: 0x0005D6F6
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x0005F4FE File Offset: 0x0005D6FE
		public Vector2 MeasuredSizes { readonly get; set; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x0005F507 File Offset: 0x0005D707
		// (set) Token: 0x0600176F RID: 5999 RVA: 0x0005F50F File Offset: 0x0005D70F
		public Vector2 RoundedSizes { readonly get; set; }

		// Token: 0x06001770 RID: 6000 RVA: 0x0005F518 File Offset: 0x0005D718
		public static ITextHandle New()
		{
			return new TextNativeHandle
			{
				textVertices = default(NativeArray<TextVertex>)
			};
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0005F548 File Offset: 0x0005D748
		public bool IsLegacy()
		{
			return true;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x000020E6 File Offset: 0x000002E6
		public void SetDirty()
		{
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0005F55C File Offset: 0x0005D75C
		ITextHandle ITextHandle.New()
		{
			return TextNativeHandle.New();
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0005F574 File Offset: 0x0005D774
		public float GetLineHeight(int characterIndex, MeshGenerationContextUtils.TextParams textParams, float textScaling, float pixelPerPoint)
		{
			textParams.wordWrapWidth = 0f;
			textParams.wordWrap = false;
			return this.ComputeTextHeight(textParams, textScaling);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0005F5A4 File Offset: 0x0005D7A4
		public TextInfo Update(MeshGenerationContextUtils.TextParams parms, float pixelsPerPoint)
		{
			Debug.Log("TextNative Update should not be called");
			return null;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0005F5C4 File Offset: 0x0005D7C4
		public int VerticesCount(MeshGenerationContextUtils.TextParams parms, float pixelPerPoint)
		{
			return this.GetVertices(parms, pixelPerPoint).Length;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0005F5E8 File Offset: 0x0005D7E8
		public NativeArray<TextVertex> GetVertices(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			Vector2 vector = parms.rect.size;
			bool flag = Mathf.Abs(parms.rect.size.x - this.RoundedSizes.x) < 0.01f && Mathf.Abs(parms.rect.size.y - this.RoundedSizes.y) < 0.01f;
			if (flag)
			{
				vector = this.MeasuredSizes;
				parms.wordWrapWidth = vector.x;
			}
			else
			{
				this.RoundedSizes = vector;
				this.MeasuredSizes = vector;
			}
			parms.rect = new Rect(Vector2.zero, vector);
			int hashCode = parms.GetHashCode();
			bool flag2 = this.m_PreviousTextParamsHash == hashCode;
			NativeArray<TextVertex> nativeArray;
			if (flag2)
			{
				nativeArray = this.textVertices;
			}
			else
			{
				this.m_PreviousTextParamsHash = hashCode;
				TextNativeSettings textNativeSettings = MeshGenerationContextUtils.TextParams.GetTextNativeSettings(parms, scaling);
				Assert.IsNotNull<Font>(textNativeSettings.font);
				this.textVertices = TextNative.GetVertices(textNativeSettings);
				nativeArray = this.textVertices;
			}
			return nativeArray;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x0005F6F4 File Offset: 0x0005D8F4
		public Vector2 GetCursorPosition(CursorPositionStylePainterParameters parms, float scaling)
		{
			return TextNative.GetCursorPosition(parms.GetTextNativeSettings(scaling), parms.rect, parms.cursorIndex);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0005F720 File Offset: 0x0005D920
		public float ComputeTextWidth(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			float num = TextNative.ComputeTextWidth(MeshGenerationContextUtils.TextParams.GetTextNativeSettings(parms, scaling));
			bool flag = scaling != 1f && num != 0f;
			float num2;
			if (flag)
			{
				num2 = num + 0.0001f;
			}
			else
			{
				num2 = num;
			}
			return num2;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0005F764 File Offset: 0x0005D964
		public float ComputeTextHeight(MeshGenerationContextUtils.TextParams parms, float scaling)
		{
			return TextNative.ComputeTextHeight(MeshGenerationContextUtils.TextParams.GetTextNativeSettings(parms, scaling));
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0005F784 File Offset: 0x0005D984
		public bool IsElided()
		{
			return false;
		}

		// Token: 0x04000A01 RID: 2561
		internal NativeArray<TextVertex> textVertices;

		// Token: 0x04000A02 RID: 2562
		private int m_PreviousTextParamsHash;
	}
}

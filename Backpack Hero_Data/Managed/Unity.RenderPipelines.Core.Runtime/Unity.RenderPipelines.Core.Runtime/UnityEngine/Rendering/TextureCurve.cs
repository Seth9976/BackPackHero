using System;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public class TextureCurve : IDisposable
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001C43E File Offset: 0x0001A63E
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x0001C446 File Offset: 0x0001A646
		public int length { get; private set; }

		// Token: 0x170000C7 RID: 199
		public Keyframe this[int index]
		{
			get
			{
				return this.m_Curve[index];
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001C45D File Offset: 0x0001A65D
		public TextureCurve(AnimationCurve baseCurve, float zeroValue, bool loop, in Vector2 bounds)
			: this(baseCurve.keys, zeroValue, loop, in bounds)
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001C470 File Offset: 0x0001A670
		public TextureCurve(Keyframe[] keys, float zeroValue, bool loop, in Vector2 bounds)
		{
			this.m_Curve = new AnimationCurve(keys);
			this.m_ZeroValue = zeroValue;
			this.m_Loop = loop;
			Vector2 vector = bounds;
			this.m_Range = vector.magnitude;
			this.length = keys.Length;
			this.SetDirty();
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001C4C4 File Offset: 0x0001A6C4
		~TextureCurve()
		{
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001C4EC File Offset: 0x0001A6EC
		[Obsolete("Please use Release() instead.")]
		public void Dispose()
		{
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001C4EE File Offset: 0x0001A6EE
		public void Release()
		{
			CoreUtils.Destroy(this.m_Texture);
			this.m_Texture = null;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001C502 File Offset: 0x0001A702
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetDirty()
		{
			this.m_IsCurveDirty = true;
			this.m_IsTextureDirty = true;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001C512 File Offset: 0x0001A712
		private static GraphicsFormat GetTextureFormat()
		{
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R16_SFloat, FormatUsage.SetPixels))
			{
				return GraphicsFormat.R16_SFloat;
			}
			if (SystemInfo.IsFormatSupported(GraphicsFormat.R8_UNorm, FormatUsage.SetPixels))
			{
				return GraphicsFormat.R8_UNorm;
			}
			return GraphicsFormat.R8G8B8A8_UNorm;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001C530 File Offset: 0x0001A730
		public Texture2D GetTexture()
		{
			if (this.m_Texture == null)
			{
				this.m_Texture = new Texture2D(128, 1, TextureCurve.GetTextureFormat(), TextureCreationFlags.None);
				this.m_Texture.name = "CurveTexture";
				this.m_Texture.hideFlags = HideFlags.HideAndDontSave;
				this.m_Texture.filterMode = FilterMode.Bilinear;
				this.m_Texture.wrapMode = TextureWrapMode.Clamp;
				this.m_IsTextureDirty = true;
			}
			if (this.m_IsTextureDirty)
			{
				Color[] array = new Color[128];
				for (int i = 0; i < array.Length; i++)
				{
					array[i].r = this.Evaluate((float)i * 0.0078125f);
				}
				this.m_Texture.SetPixels(array);
				this.m_Texture.Apply(false, false);
				this.m_IsTextureDirty = false;
			}
			return this.m_Texture;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001C600 File Offset: 0x0001A800
		public float Evaluate(float time)
		{
			if (this.m_IsCurveDirty)
			{
				this.length = this.m_Curve.length;
			}
			if (this.length == 0)
			{
				return this.m_ZeroValue;
			}
			if (!this.m_Loop || this.length == 1)
			{
				return this.m_Curve.Evaluate(time);
			}
			if (this.m_IsCurveDirty)
			{
				if (this.m_LoopingCurve == null)
				{
					this.m_LoopingCurve = new AnimationCurve();
				}
				Keyframe keyframe = this.m_Curve[this.length - 1];
				keyframe.time -= this.m_Range;
				Keyframe keyframe2 = this.m_Curve[0];
				keyframe2.time += this.m_Range;
				this.m_LoopingCurve.keys = this.m_Curve.keys;
				this.m_LoopingCurve.AddKey(keyframe);
				this.m_LoopingCurve.AddKey(keyframe2);
				this.m_IsCurveDirty = false;
			}
			return this.m_LoopingCurve.Evaluate(time);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001C6FD File Offset: 0x0001A8FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int AddKey(float time, float value)
		{
			int num = this.m_Curve.AddKey(time, value);
			if (num > -1)
			{
				this.SetDirty();
			}
			return num;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001C716 File Offset: 0x0001A916
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int MoveKey(int index, in Keyframe key)
		{
			int num = this.m_Curve.MoveKey(index, key);
			this.SetDirty();
			return num;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001C730 File Offset: 0x0001A930
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveKey(int index)
		{
			this.m_Curve.RemoveKey(index);
			this.SetDirty();
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001C744 File Offset: 0x0001A944
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SmoothTangents(int index, float weight)
		{
			this.m_Curve.SmoothTangents(index, weight);
			this.SetDirty();
		}

		// Token: 0x0400037E RID: 894
		private const int k_Precision = 128;

		// Token: 0x0400037F RID: 895
		private const float k_Step = 0.0078125f;

		// Token: 0x04000381 RID: 897
		[SerializeField]
		private bool m_Loop;

		// Token: 0x04000382 RID: 898
		[SerializeField]
		private float m_ZeroValue;

		// Token: 0x04000383 RID: 899
		[SerializeField]
		private float m_Range;

		// Token: 0x04000384 RID: 900
		[SerializeField]
		private AnimationCurve m_Curve;

		// Token: 0x04000385 RID: 901
		private AnimationCurve m_LoopingCurve;

		// Token: 0x04000386 RID: 902
		private Texture2D m_Texture;

		// Token: 0x04000387 RID: 903
		private bool m_IsCurveDirty;

		// Token: 0x04000388 RID: 904
		private bool m_IsTextureDirty;
	}
}

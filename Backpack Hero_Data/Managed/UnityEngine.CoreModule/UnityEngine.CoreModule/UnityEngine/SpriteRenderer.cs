using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000263 RID: 611
	[NativeType("Runtime/Graphics/Mesh/SpriteRenderer.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class SpriteRenderer : Renderer
	{
		// Token: 0x06001AB0 RID: 6832 RVA: 0x0002AD7C File Offset: 0x00028F7C
		public void RegisterSpriteChangeCallback(UnityAction<SpriteRenderer> callback)
		{
			bool flag = this.m_SpriteChangeEvent == null;
			if (flag)
			{
				this.m_SpriteChangeEvent = new UnityEvent<SpriteRenderer>();
			}
			this.m_SpriteChangeEvent.AddListener(callback);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0002ADB0 File Offset: 0x00028FB0
		public void UnregisterSpriteChangeCallback(UnityAction<SpriteRenderer> callback)
		{
			bool flag = this.m_SpriteChangeEvent != null;
			if (flag)
			{
				this.m_SpriteChangeEvent.RemoveListener(callback);
			}
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0002ADD8 File Offset: 0x00028FD8
		[RequiredByNativeCode]
		private void InvokeSpriteChanged()
		{
			try
			{
				UnityEvent<SpriteRenderer> spriteChangeEvent = this.m_SpriteChangeEvent;
				if (spriteChangeEvent != null)
				{
					spriteChangeEvent.Invoke(this);
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001AB3 RID: 6835
		internal extern bool shouldSupportTiling
		{
			[NativeMethod("ShouldSupportTiling")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001AB4 RID: 6836
		// (set) Token: 0x06001AB5 RID: 6837
		public extern Sprite sprite
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001AB6 RID: 6838
		// (set) Token: 0x06001AB7 RID: 6839
		public extern SpriteDrawMode drawMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0002AE1C File Offset: 0x0002901C
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x0002AE32 File Offset: 0x00029032
		public Vector2 size
		{
			get
			{
				Vector2 vector;
				this.get_size_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001ABA RID: 6842
		// (set) Token: 0x06001ABB RID: 6843
		public extern float adaptiveModeThreshold
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001ABC RID: 6844
		// (set) Token: 0x06001ABD RID: 6845
		public extern SpriteTileMode tileMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0002AE3C File Offset: 0x0002903C
		// (set) Token: 0x06001ABF RID: 6847 RVA: 0x0002AE52 File Offset: 0x00029052
		public Color color
		{
			get
			{
				Color color;
				this.get_color_Injected(out color);
				return color;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001AC0 RID: 6848
		// (set) Token: 0x06001AC1 RID: 6849
		public extern SpriteMaskInteraction maskInteraction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001AC2 RID: 6850
		// (set) Token: 0x06001AC3 RID: 6851
		public extern bool flipX
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001AC4 RID: 6852
		// (set) Token: 0x06001AC5 RID: 6853
		public extern bool flipY
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001AC6 RID: 6854
		// (set) Token: 0x06001AC7 RID: 6855
		public extern SpriteSortPoint spriteSortPoint
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0002AE5C File Offset: 0x0002905C
		[NativeMethod(Name = "GetSpriteBounds")]
		internal Bounds Internal_GetSpriteBounds(SpriteDrawMode mode)
		{
			Bounds bounds;
			this.Internal_GetSpriteBounds_Injected(mode, out bounds);
			return bounds;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0002AE74 File Offset: 0x00029074
		internal Bounds GetSpriteBounds()
		{
			return this.Internal_GetSpriteBounds(this.drawMode);
		}

		// Token: 0x06001ACB RID: 6859
		[MethodImpl(4096)]
		private extern void get_size_Injected(out Vector2 ret);

		// Token: 0x06001ACC RID: 6860
		[MethodImpl(4096)]
		private extern void set_size_Injected(ref Vector2 value);

		// Token: 0x06001ACD RID: 6861
		[MethodImpl(4096)]
		private extern void get_color_Injected(out Color ret);

		// Token: 0x06001ACE RID: 6862
		[MethodImpl(4096)]
		private extern void set_color_Injected(ref Color value);

		// Token: 0x06001ACF RID: 6863
		[MethodImpl(4096)]
		private extern void Internal_GetSpriteBounds_Injected(SpriteDrawMode mode, out Bounds ret);

		// Token: 0x040008C6 RID: 2246
		private UnityEvent<SpriteRenderer> m_SpriteChangeEvent;
	}
}

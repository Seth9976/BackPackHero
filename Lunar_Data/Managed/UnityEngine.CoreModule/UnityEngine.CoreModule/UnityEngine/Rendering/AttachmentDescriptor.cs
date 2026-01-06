using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x020003EB RID: 1003
	public struct AttachmentDescriptor : IEquatable<AttachmentDescriptor>
	{
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x00037FEC File Offset: 0x000361EC
		// (set) Token: 0x060021F5 RID: 8693 RVA: 0x00038004 File Offset: 0x00036204
		public RenderBufferLoadAction loadAction
		{
			get
			{
				return this.m_LoadAction;
			}
			set
			{
				this.m_LoadAction = value;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x00038010 File Offset: 0x00036210
		// (set) Token: 0x060021F7 RID: 8695 RVA: 0x00038028 File Offset: 0x00036228
		public RenderBufferStoreAction storeAction
		{
			get
			{
				return this.m_StoreAction;
			}
			set
			{
				this.m_StoreAction = value;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x00038034 File Offset: 0x00036234
		// (set) Token: 0x060021F9 RID: 8697 RVA: 0x0003804C File Offset: 0x0003624C
		public GraphicsFormat graphicsFormat
		{
			get
			{
				return this.m_Format;
			}
			set
			{
				this.m_Format = value;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060021FA RID: 8698 RVA: 0x00038058 File Offset: 0x00036258
		// (set) Token: 0x060021FB RID: 8699 RVA: 0x00038075 File Offset: 0x00036275
		public RenderTextureFormat format
		{
			get
			{
				return GraphicsFormatUtility.GetRenderTextureFormat(this.m_Format);
			}
			set
			{
				this.m_Format = GraphicsFormatUtility.GetGraphicsFormat(value, RenderTextureReadWrite.Default);
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x00038088 File Offset: 0x00036288
		// (set) Token: 0x060021FD RID: 8701 RVA: 0x000380A0 File Offset: 0x000362A0
		public RenderTargetIdentifier loadStoreTarget
		{
			get
			{
				return this.m_LoadStoreTarget;
			}
			set
			{
				this.m_LoadStoreTarget = value;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x000380AC File Offset: 0x000362AC
		// (set) Token: 0x060021FF RID: 8703 RVA: 0x000380C4 File Offset: 0x000362C4
		public RenderTargetIdentifier resolveTarget
		{
			get
			{
				return this.m_ResolveTarget;
			}
			set
			{
				this.m_ResolveTarget = value;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x000380D0 File Offset: 0x000362D0
		// (set) Token: 0x06002201 RID: 8705 RVA: 0x000380E8 File Offset: 0x000362E8
		public Color clearColor
		{
			get
			{
				return this.m_ClearColor;
			}
			set
			{
				this.m_ClearColor = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x000380F4 File Offset: 0x000362F4
		// (set) Token: 0x06002203 RID: 8707 RVA: 0x0003810C File Offset: 0x0003630C
		public float clearDepth
		{
			get
			{
				return this.m_ClearDepth;
			}
			set
			{
				this.m_ClearDepth = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x00038118 File Offset: 0x00036318
		// (set) Token: 0x06002205 RID: 8709 RVA: 0x00038130 File Offset: 0x00036330
		public uint clearStencil
		{
			get
			{
				return this.m_ClearStencil;
			}
			set
			{
				this.m_ClearStencil = value;
			}
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0003813C File Offset: 0x0003633C
		public void ConfigureTarget(RenderTargetIdentifier target, bool loadExistingContents, bool storeResults)
		{
			this.m_LoadStoreTarget = target;
			bool flag = loadExistingContents && this.m_LoadAction != RenderBufferLoadAction.Clear;
			if (flag)
			{
				this.m_LoadAction = RenderBufferLoadAction.Load;
			}
			if (storeResults)
			{
				bool flag2 = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Resolve;
				if (flag2)
				{
					this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
				}
				else
				{
					this.m_StoreAction = RenderBufferStoreAction.Store;
				}
			}
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000381A0 File Offset: 0x000363A0
		public void ConfigureResolveTarget(RenderTargetIdentifier target)
		{
			this.m_ResolveTarget = target;
			bool flag = this.m_StoreAction == RenderBufferStoreAction.StoreAndResolve || this.m_StoreAction == RenderBufferStoreAction.Store;
			if (flag)
			{
				this.m_StoreAction = RenderBufferStoreAction.StoreAndResolve;
			}
			else
			{
				this.m_StoreAction = RenderBufferStoreAction.Resolve;
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000381DE File Offset: 0x000363DE
		public void ConfigureClear(Color clearColor, float clearDepth = 1f, uint clearStencil = 0U)
		{
			this.m_ClearColor = clearColor;
			this.m_ClearDepth = clearDepth;
			this.m_ClearStencil = clearStencil;
			this.m_LoadAction = RenderBufferLoadAction.Clear;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00038200 File Offset: 0x00036400
		public AttachmentDescriptor(GraphicsFormat format)
		{
			this = default(AttachmentDescriptor);
			this.m_LoadAction = RenderBufferLoadAction.DontCare;
			this.m_StoreAction = RenderBufferStoreAction.DontCare;
			this.m_Format = format;
			this.m_LoadStoreTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ResolveTarget = new RenderTargetIdentifier(BuiltinRenderTextureType.None);
			this.m_ClearColor = new Color(0f, 0f, 0f, 0f);
			this.m_ClearDepth = 1f;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0003826C File Offset: 0x0003646C
		public AttachmentDescriptor(RenderTextureFormat format)
		{
			this = new AttachmentDescriptor(GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Default));
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0003826C File Offset: 0x0003646C
		public AttachmentDescriptor(RenderTextureFormat format, RenderTargetIdentifier target, bool loadExistingContents = false, bool storeResults = false, bool resolve = false)
		{
			this = new AttachmentDescriptor(GraphicsFormatUtility.GetGraphicsFormat(format, RenderTextureReadWrite.Default));
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x00038280 File Offset: 0x00036480
		public bool Equals(AttachmentDescriptor other)
		{
			return this.m_LoadAction == other.m_LoadAction && this.m_StoreAction == other.m_StoreAction && this.m_Format == other.m_Format && this.m_LoadStoreTarget.Equals(other.m_LoadStoreTarget) && this.m_ResolveTarget.Equals(other.m_ResolveTarget) && this.m_ClearColor.Equals(other.m_ClearColor) && this.m_ClearDepth.Equals(other.m_ClearDepth) && this.m_ClearStencil == other.m_ClearStencil;
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0003831C File Offset: 0x0003651C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is AttachmentDescriptor && this.Equals((AttachmentDescriptor)obj);
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x00038354 File Offset: 0x00036554
		public override int GetHashCode()
		{
			int num = (int)this.m_LoadAction;
			num = (num * 397) ^ (int)this.m_StoreAction;
			num = (num * 397) ^ (int)this.m_Format;
			num = (num * 397) ^ this.m_LoadStoreTarget.GetHashCode();
			num = (num * 397) ^ this.m_ResolveTarget.GetHashCode();
			num = (num * 397) ^ this.m_ClearColor.GetHashCode();
			num = (num * 397) ^ this.m_ClearDepth.GetHashCode();
			return (num * 397) ^ (int)this.m_ClearStencil;
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x00038400 File Offset: 0x00036600
		public static bool operator ==(AttachmentDescriptor left, AttachmentDescriptor right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0003841C File Offset: 0x0003661C
		public static bool operator !=(AttachmentDescriptor left, AttachmentDescriptor right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000C60 RID: 3168
		private RenderBufferLoadAction m_LoadAction;

		// Token: 0x04000C61 RID: 3169
		private RenderBufferStoreAction m_StoreAction;

		// Token: 0x04000C62 RID: 3170
		private GraphicsFormat m_Format;

		// Token: 0x04000C63 RID: 3171
		private RenderTargetIdentifier m_LoadStoreTarget;

		// Token: 0x04000C64 RID: 3172
		private RenderTargetIdentifier m_ResolveTarget;

		// Token: 0x04000C65 RID: 3173
		private Color m_ClearColor;

		// Token: 0x04000C66 RID: 3174
		private float m_ClearDepth;

		// Token: 0x04000C67 RID: 3175
		private uint m_ClearStencil;
	}
}

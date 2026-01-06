using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000005 RID: 5
	internal abstract class AtlasBase
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020C0 File Offset: 0x000002C0
		public virtual bool TryGetAtlas(VisualElement ctx, Texture2D src, out TextureId atlas, out RectInt atlasRect)
		{
			atlas = TextureId.invalid;
			atlasRect = default(RectInt);
			return false;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E6 File Offset: 0x000002E6
		public virtual void ReturnAtlas(VisualElement ctx, Texture2D src, TextureId atlas)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E6 File Offset: 0x000002E6
		public virtual void Reset()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020E6 File Offset: 0x000002E6
		protected virtual void OnAssignedToPanel(IPanel panel)
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020E6 File Offset: 0x000002E6
		protected virtual void OnRemovedFromPanel(IPanel panel)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020E6 File Offset: 0x000002E6
		protected virtual void OnUpdateDynamicTextures(IPanel panel)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020E9 File Offset: 0x000002E9
		internal void InvokeAssignedToPanel(IPanel panel)
		{
			this.OnAssignedToPanel(panel);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020F4 File Offset: 0x000002F4
		internal void InvokeRemovedFromPanel(IPanel panel)
		{
			this.OnRemovedFromPanel(panel);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020FF File Offset: 0x000002FF
		internal void InvokeUpdateDynamicTextures(IPanel panel)
		{
			this.OnUpdateDynamicTextures(panel);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000210C File Offset: 0x0000030C
		protected static void RepaintTexturedElements(IPanel panel)
		{
			Panel panel2 = panel as Panel;
			UIRRepaintUpdater uirrepaintUpdater = ((panel2 != null) ? panel2.GetUpdater(VisualTreeUpdatePhase.Repaint) : null) as UIRRepaintUpdater;
			if (uirrepaintUpdater != null)
			{
				RenderChain renderChain = uirrepaintUpdater.renderChain;
				if (renderChain != null)
				{
					renderChain.RepaintTexturedElements();
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000214C File Offset: 0x0000034C
		protected TextureId AllocateDynamicTexture()
		{
			return this.textureRegistry.AllocAndAcquireDynamic();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002169 File Offset: 0x00000369
		protected void FreeDynamicTexture(TextureId id)
		{
			this.textureRegistry.Release(id);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002179 File Offset: 0x00000379
		protected void SetDynamicTexture(TextureId id, Texture texture)
		{
			this.textureRegistry.UpdateDynamic(id, texture);
		}

		// Token: 0x04000001 RID: 1
		internal TextureRegistry textureRegistry = TextureRegistry.instance;
	}
}

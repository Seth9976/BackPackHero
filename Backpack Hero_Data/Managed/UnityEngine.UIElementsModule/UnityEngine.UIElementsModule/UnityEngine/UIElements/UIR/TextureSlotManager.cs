using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000320 RID: 800
	internal class TextureSlotManager
	{
		// Token: 0x060019E6 RID: 6630 RVA: 0x0006E20C File Offset: 0x0006C40C
		static TextureSlotManager()
		{
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				TextureSlotManager.slotIds[i] = Shader.PropertyToID(string.Format("_Texture{0}", i));
			}
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x0006E278 File Offset: 0x0006C478
		public TextureSlotManager()
		{
			this.m_Textures = new TextureId[TextureSlotManager.k_SlotCount];
			this.m_Tickets = new int[TextureSlotManager.k_SlotCount];
			this.m_GpuTextures = new Vector4[TextureSlotManager.k_SlotCount];
			this.Reset();
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0006E2DC File Offset: 0x0006C4DC
		public void Reset()
		{
			this.m_CurrentTicket = 0;
			this.m_FirstUsedTicket = 0;
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				this.m_Textures[i] = TextureId.invalid;
				this.m_Tickets[i] = -1;
				this.m_GpuTextures[i] = new Vector4(-1f, 1f, 1f, 0f);
			}
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0006E350 File Offset: 0x0006C550
		public void StartNewBatch()
		{
			int num = this.m_CurrentTicket + 1;
			this.m_CurrentTicket = num;
			this.m_FirstUsedTicket = num;
			this.FreeSlots = TextureSlotManager.k_SlotCount;
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0006E384 File Offset: 0x0006C584
		public int IndexOf(TextureId id)
		{
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				bool flag = this.m_Textures[i].index == id.index;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0006E3D0 File Offset: 0x0006C5D0
		[MethodImpl(256)]
		public void MarkUsed(int slotIndex)
		{
			int num = this.m_Tickets[slotIndex];
			bool flag = num < this.m_FirstUsedTicket;
			int num2;
			if (flag)
			{
				num2 = this.FreeSlots - 1;
				this.FreeSlots = num2;
			}
			int[] tickets = this.m_Tickets;
			num2 = this.m_CurrentTicket + 1;
			this.m_CurrentTicket = num2;
			tickets[slotIndex] = num2;
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x0006E41E File Offset: 0x0006C61E
		// (set) Token: 0x060019ED RID: 6637 RVA: 0x0006E426 File Offset: 0x0006C626
		public int FreeSlots { get; private set; } = TextureSlotManager.k_SlotCount;

		// Token: 0x060019EE RID: 6638 RVA: 0x0006E430 File Offset: 0x0006C630
		public int FindOldestSlot()
		{
			int num = this.m_Tickets[0];
			int num2 = 0;
			for (int i = 1; i < TextureSlotManager.k_SlotCount; i++)
			{
				bool flag = this.m_Tickets[i] < num;
				if (flag)
				{
					num = this.m_Tickets[i];
					num2 = i;
				}
			}
			return num2;
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0006E484 File Offset: 0x0006C684
		public void Bind(TextureId id, int slot, MaterialPropertyBlock mat)
		{
			Texture texture = this.textureRegistry.GetTexture(id);
			bool flag = texture == null;
			if (flag)
			{
				texture = Texture2D.whiteTexture;
			}
			this.m_Textures[slot] = id;
			this.MarkUsed(slot);
			this.m_GpuTextures[slot] = new Vector4(id.ConvertToGpu(), 1f / (float)texture.width, 1f / (float)texture.height, 0f);
			mat.SetTexture(TextureSlotManager.slotIds[slot], texture);
			mat.SetVectorArray(TextureSlotManager.textureTableId, this.m_GpuTextures);
		}

		// Token: 0x04000BC8 RID: 3016
		private static readonly int k_SlotCount = (UIRenderDevice.shaderModelIs35 ? 8 : 4);

		// Token: 0x04000BC9 RID: 3017
		internal static readonly int[] slotIds = new int[TextureSlotManager.k_SlotCount];

		// Token: 0x04000BCA RID: 3018
		internal static readonly int textureTableId = Shader.PropertyToID("_TextureInfo");

		// Token: 0x04000BCB RID: 3019
		private TextureId[] m_Textures;

		// Token: 0x04000BCC RID: 3020
		private int[] m_Tickets;

		// Token: 0x04000BCD RID: 3021
		private int m_CurrentTicket;

		// Token: 0x04000BCE RID: 3022
		private int m_FirstUsedTicket;

		// Token: 0x04000BCF RID: 3023
		private Vector4[] m_GpuTextures;

		// Token: 0x04000BD1 RID: 3025
		internal TextureRegistry textureRegistry = TextureRegistry.instance;
	}
}

using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	public struct SpriteState : IEquatable<SpriteState>
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000147B2 File Offset: 0x000129B2
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x000147BA File Offset: 0x000129BA
		public Sprite highlightedSprite
		{
			get
			{
				return this.m_HighlightedSprite;
			}
			set
			{
				this.m_HighlightedSprite = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x000147C3 File Offset: 0x000129C3
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x000147CB File Offset: 0x000129CB
		public Sprite pressedSprite
		{
			get
			{
				return this.m_PressedSprite;
			}
			set
			{
				this.m_PressedSprite = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x000147D4 File Offset: 0x000129D4
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x000147DC File Offset: 0x000129DC
		public Sprite selectedSprite
		{
			get
			{
				return this.m_SelectedSprite;
			}
			set
			{
				this.m_SelectedSprite = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x000147E5 File Offset: 0x000129E5
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x000147ED File Offset: 0x000129ED
		public Sprite disabledSprite
		{
			get
			{
				return this.m_DisabledSprite;
			}
			set
			{
				this.m_DisabledSprite = value;
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000147F8 File Offset: 0x000129F8
		public bool Equals(SpriteState other)
		{
			return this.highlightedSprite == other.highlightedSprite && this.pressedSprite == other.pressedSprite && this.selectedSprite == other.selectedSprite && this.disabledSprite == other.disabledSprite;
		}

		// Token: 0x0400016A RID: 362
		[SerializeField]
		private Sprite m_HighlightedSprite;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private Sprite m_PressedSprite;

		// Token: 0x0400016C RID: 364
		[FormerlySerializedAs("m_HighlightedSprite")]
		[SerializeField]
		private Sprite m_SelectedSprite;

		// Token: 0x0400016D RID: 365
		[SerializeField]
		private Sprite m_DisabledSprite;
	}
}

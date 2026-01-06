using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000011 RID: 17
	[MovedFrom("UnityEngine.Experimental.U2D.Animation")]
	[Serializable]
	internal class SpriteCategoryEntry : INameHash
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002EFF File Offset: 0x000010FF
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002F07 File Offset: 0x00001107
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
				this.m_Hash = SpriteLibraryUtility.GetStringHash(this.m_Name);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002F26 File Offset: 0x00001126
		public int hash
		{
			get
			{
				return this.m_Hash;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002F2E File Offset: 0x0000112E
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002F36 File Offset: 0x00001136
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				this.m_Sprite = value;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F3F File Offset: 0x0000113F
		public void UpdateHash()
		{
			this.m_Hash = SpriteLibraryUtility.GetStringHash(this.m_Name);
		}

		// Token: 0x0400001F RID: 31
		[SerializeField]
		private string m_Name;

		// Token: 0x04000020 RID: 32
		[HideInInspector]
		[SerializeField]
		private int m_Hash;

		// Token: 0x04000021 RID: 33
		[SerializeField]
		private Sprite m_Sprite;
	}
}

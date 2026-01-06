using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000012 RID: 18
	[MovedFrom("UnityEngine.Experimental.U2D.Animation")]
	[Serializable]
	internal class SpriteLibCategory : INameHash
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002F57 File Offset: 0x00001157
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002F5F File Offset: 0x0000115F
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002F7E File Offset: 0x0000117E
		public int hash
		{
			get
			{
				return this.m_Hash;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002F86 File Offset: 0x00001186
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002F8E File Offset: 0x0000118E
		public List<SpriteCategoryEntry> categoryList
		{
			get
			{
				return this.m_CategoryList;
			}
			set
			{
				this.m_CategoryList = value;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F98 File Offset: 0x00001198
		public void UpdateHash()
		{
			this.m_Hash = SpriteLibraryUtility.GetStringHash(this.m_Name);
			foreach (SpriteCategoryEntry spriteCategoryEntry in this.m_CategoryList)
			{
				spriteCategoryEntry.UpdateHash();
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003000 File Offset: 0x00001200
		internal void ValidateLabels()
		{
			SpriteLibraryAsset.RenameDuplicate(this.m_CategoryList, delegate(string originalName, string newName)
			{
				Debug.LogWarning(string.Format("Label {0} renamed to {1} due to hash clash", originalName, newName));
			});
		}

		// Token: 0x04000022 RID: 34
		[SerializeField]
		private string m_Name;

		// Token: 0x04000023 RID: 35
		[SerializeField]
		private int m_Hash;

		// Token: 0x04000024 RID: 36
		[SerializeField]
		private List<SpriteCategoryEntry> m_CategoryList;
	}
}

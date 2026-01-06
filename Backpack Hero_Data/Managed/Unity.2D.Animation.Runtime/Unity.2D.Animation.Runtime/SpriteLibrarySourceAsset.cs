using System;
using System.Collections.Generic;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000021 RID: 33
	internal class SpriteLibrarySourceAsset : ScriptableObject
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003820 File Offset: 0x00001A20
		public IReadOnlyList<SpriteLibCategoryOverride> library
		{
			get
			{
				return this.m_Library;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003828 File Offset: 0x00001A28
		public void InitializeWithAsset(SpriteLibrarySourceAsset source)
		{
			this.m_Library = new List<SpriteLibCategoryOverride>(source.m_Library);
			this.m_PrimaryLibraryGUID = source.m_PrimaryLibraryGUID;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003847 File Offset: 0x00001A47
		public void SetLibrary(IList<SpriteLibCategoryOverride> newLibrary)
		{
			if (!this.m_Library.Equals(newLibrary))
			{
				this.m_Library = new List<SpriteLibCategoryOverride>(newLibrary);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003863 File Offset: 0x00001A63
		public void SetPrimaryLibraryGUID(string newPrimaryLibraryGUID)
		{
			this.m_PrimaryLibraryGUID = newPrimaryLibraryGUID;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000386C File Offset: 0x00001A6C
		public void AddCategory(SpriteLibCategoryOverride newCategory)
		{
			if (!this.m_Library.Contains(newCategory))
			{
				this.m_Library.Add(newCategory);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003888 File Offset: 0x00001A88
		public void RemoveCategory(SpriteLibCategoryOverride categoryToRemove)
		{
			if (this.m_Library.Contains(categoryToRemove))
			{
				this.m_Library.Remove(categoryToRemove);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000038A5 File Offset: 0x00001AA5
		public void RemoveCategory(int indexToRemove)
		{
			if (indexToRemove >= 0 && indexToRemove < this.m_Library.Count)
			{
				this.m_Library.RemoveAt(indexToRemove);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000038C5 File Offset: 0x00001AC5
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003863 File Offset: 0x00001A63
		public string primaryLibraryID
		{
			get
			{
				return this.m_PrimaryLibraryGUID;
			}
			set
			{
				this.m_PrimaryLibraryGUID = value;
			}
		}

		// Token: 0x0400003F RID: 63
		public const string defaultName = "New Sprite Library Asset";

		// Token: 0x04000040 RID: 64
		public const string extension = ".spriteLib";

		// Token: 0x04000041 RID: 65
		[SerializeField]
		private List<SpriteLibCategoryOverride> m_Library = new List<SpriteLibCategoryOverride>();

		// Token: 0x04000042 RID: 66
		[SerializeField]
		private string m_PrimaryLibraryGUID;
	}
}

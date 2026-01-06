using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000023 RID: 35
	[MovedFrom("UnityEngine.Experimental.U2D.Animation")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.animation@latest/index.html?subfolder=/manual/SLAsset.html%23sprite-resolver-component")]
	[DefaultExecutionOrder(-2)]
	[AddComponentMenu("2D Animation/Sprite Resolver")]
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	public class SpriteResolver : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x0000392D File Offset: 0x00001B2D
		private void Reset()
		{
			if (this.spriteRenderer)
			{
				this.SetSprite(this.spriteRenderer.sprite);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003950 File Offset: 0x00001B50
		private void SetSprite(Sprite sprite)
		{
			SpriteLibrary spriteLibrary = this.spriteLibrary;
			if (spriteLibrary != null && sprite != null)
			{
				foreach (string text in spriteLibrary.categoryNames)
				{
					foreach (string text2 in spriteLibrary.GetEntryNames(text))
					{
						if (spriteLibrary.GetSprite(text, text2) == sprite)
						{
							this.spriteKeyInt = SpriteLibrary.GetHashForCategoryAndEntry(text, text2);
							return;
						}
					}
				}
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003A08 File Offset: 0x00001C08
		private void OnEnable()
		{
			this.m_CategoryHashInt = SpriteResolver.ConvertFloatToInt(this.m_CategoryHash);
			this.m_PreviousCategoryHash = this.m_CategoryHashInt;
			this.m_LabelHashInt = SpriteResolver.ConvertFloatToInt(this.m_labelHash);
			this.m_PreviousLabelHash = this.m_LabelHashInt;
			this.m_SpriteKeyInt = SpriteLibraryUtility.Convert32BitTo30BitHash(SpriteResolver.ConvertFloatToInt(this.m_SpriteKey));
			if (this.m_SpriteKeyInt == 0)
			{
				this.m_SpriteKey = SpriteResolver.ConvertCategoryLabelHashToSpriteKey(this.spriteLibrary, SpriteLibraryUtility.Convert32BitTo30BitHash(this.m_CategoryHashInt), SpriteLibraryUtility.Convert32BitTo30BitHash(this.m_LabelHashInt));
				this.m_SpriteKeyInt = SpriteLibraryUtility.Convert32BitTo30BitHash(SpriteResolver.ConvertFloatToInt(this.m_SpriteKey));
			}
			string text;
			string text2;
			if (this.spriteLibrary != null && this.spriteLibrary.GetCategoryAndEntryNameFromHash(this.m_SpriteKeyInt, out text, out text2))
			{
				this.m_CategoryHashInt = SpriteLibraryUtility.GetStringHash(text);
				this.m_LabelHashInt = SpriteLibraryUtility.GetStringHash(text2);
				this.m_CategoryHash = SpriteResolver.ConvertIntToFloat(this.m_CategoryHashInt);
				this.m_labelHash = SpriteResolver.ConvertIntToFloat(this.m_LabelHashInt);
			}
			this.m_PreviousSpriteKeyInt = this.m_SpriteKeyInt;
			this.ResolveSpriteToSpriteRenderer();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003B25 File Offset: 0x00001D25
		private SpriteRenderer spriteRenderer
		{
			get
			{
				return base.GetComponent<SpriteRenderer>();
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003B2D File Offset: 0x00001D2D
		public void SetCategoryAndLabel(string category, string label)
		{
			this.spriteKeyInt = SpriteLibrary.GetHashForCategoryAndEntry(category, label);
			this.m_PreviousSpriteKeyInt = this.spriteKeyInt;
			this.ResolveSpriteToSpriteRenderer();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003B50 File Offset: 0x00001D50
		public string GetCategory()
		{
			string text = "";
			SpriteLibrary spriteLibrary = this.spriteLibrary;
			if (spriteLibrary)
			{
				string text2;
				spriteLibrary.GetCategoryAndEntryNameFromHash(this.spriteKeyInt, out text, out text2);
			}
			return text;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003B84 File Offset: 0x00001D84
		public string GetLabel()
		{
			string text = "";
			SpriteLibrary spriteLibrary = this.spriteLibrary;
			if (spriteLibrary)
			{
				string text2;
				spriteLibrary.GetCategoryAndEntryNameFromHash(this.spriteKeyInt, out text2, out text);
			}
			return text;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public SpriteLibrary spriteLibrary
		{
			get
			{
				return base.gameObject.GetComponentInParent<SpriteLibrary>(true);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003BC8 File Offset: 0x00001DC8
		private void LateUpdate()
		{
			this.m_SpriteKeyInt = SpriteLibraryUtility.Convert32BitTo30BitHash(SpriteResolver.ConvertFloatToInt(this.m_SpriteKey));
			if (this.m_SpriteKeyInt != this.m_PreviousSpriteKeyInt)
			{
				this.m_PreviousSpriteKeyInt = this.m_SpriteKeyInt;
				this.ResolveSpriteToSpriteRenderer();
				return;
			}
			this.m_CategoryHashInt = SpriteResolver.ConvertFloatToInt(this.m_CategoryHash);
			this.m_LabelHashInt = SpriteResolver.ConvertFloatToInt(this.m_labelHash);
			if ((this.m_LabelHashInt != this.m_PreviousLabelHash || this.m_CategoryHashInt != this.m_PreviousCategoryHash) && this.spriteLibrary != null)
			{
				this.m_PreviousCategoryHash = this.m_CategoryHashInt;
				this.m_PreviousLabelHash = this.m_LabelHashInt;
				this.m_SpriteKey = SpriteResolver.ConvertCategoryLabelHashToSpriteKey(this.spriteLibrary, SpriteLibraryUtility.Convert32BitTo30BitHash(this.m_CategoryHashInt), SpriteLibraryUtility.Convert32BitTo30BitHash(this.m_LabelHashInt));
				this.m_SpriteKeyInt = SpriteLibraryUtility.Convert32BitTo30BitHash(SpriteResolver.ConvertFloatToInt(this.m_SpriteKey));
				this.m_PreviousSpriteKeyInt = this.m_SpriteKeyInt;
				this.ResolveSpriteToSpriteRenderer();
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003CC0 File Offset: 0x00001EC0
		internal static float ConvertCategoryLabelHashToSpriteKey(SpriteLibrary library, int categoryHash, int labelHash)
		{
			if (library != null)
			{
				foreach (string text in library.categoryNames)
				{
					if (categoryHash == SpriteLibraryUtility.GetStringHash(text))
					{
						IEnumerable<string> entryNames = library.GetEntryNames(text);
						if (entryNames != null)
						{
							foreach (string text2 in entryNames)
							{
								if (labelHash == SpriteLibraryUtility.GetStringHash(text2))
								{
									return SpriteResolver.ConvertIntToFloat(SpriteLibrary.GetHashForCategoryAndEntry(text, text2));
								}
							}
						}
					}
				}
			}
			return 0f;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003D84 File Offset: 0x00001F84
		internal Sprite GetSprite(out bool validEntry)
		{
			SpriteLibrary spriteLibrary = this.spriteLibrary;
			if (spriteLibrary != null)
			{
				return spriteLibrary.GetSpriteFromCategoryAndEntryHash(this.m_SpriteKeyInt, out validEntry);
			}
			validEntry = false;
			return null;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public void ResolveSpriteToSpriteRenderer()
		{
			this.m_PreviousSpriteKeyInt = this.m_SpriteKeyInt;
			bool flag;
			Sprite sprite = this.GetSprite(out flag);
			SpriteRenderer spriteRenderer = this.spriteRenderer;
			if (spriteRenderer != null && (sprite != null || flag))
			{
				spriteRenderer.sprite = sprite;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003DF8 File Offset: 0x00001FF8
		private void OnTransformParentChanged()
		{
			this.ResolveSpriteToSpriteRenderer();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003E00 File Offset: 0x00002000
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003E08 File Offset: 0x00002008
		private int spriteKeyInt
		{
			get
			{
				return this.m_SpriteKeyInt;
			}
			set
			{
				this.m_SpriteKeyInt = value;
				this.m_SpriteKey = SpriteResolver.ConvertIntToFloat(this.m_SpriteKeyInt);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003E24 File Offset: 0x00002024
		internal unsafe static int ConvertFloatToInt(float f)
		{
			int* ptr = (int*)(&f);
			return *ptr;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003E38 File Offset: 0x00002038
		internal unsafe static float ConvertIntToFloat(int f)
		{
			float* ptr = (float*)(&f);
			return *ptr;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000026F3 File Offset: 0x000008F3
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000026F3 File Offset: 0x000008F3
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
		}

		// Token: 0x04000044 RID: 68
		[SerializeField]
		private float m_CategoryHash;

		// Token: 0x04000045 RID: 69
		[SerializeField]
		private float m_labelHash;

		// Token: 0x04000046 RID: 70
		[SerializeField]
		private float m_SpriteKey;

		// Token: 0x04000047 RID: 71
		private int m_CategoryHashInt;

		// Token: 0x04000048 RID: 72
		private int m_LabelHashInt;

		// Token: 0x04000049 RID: 73
		private int m_SpriteKeyInt;

		// Token: 0x0400004A RID: 74
		private int m_PreviousCategoryHash;

		// Token: 0x0400004B RID: 75
		private int m_PreviousLabelHash;

		// Token: 0x0400004C RID: 76
		private int m_PreviousSpriteKeyInt;
	}
}

using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	public class TextStyle
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
		public static TextStyle NormalStyle
		{
			get
			{
				bool flag = TextStyle.k_NormalStyle == null;
				if (flag)
				{
					TextStyle.k_NormalStyle = new TextStyle("Normal", string.Empty, string.Empty);
				}
				return TextStyle.k_NormalStyle;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0001C60C File Offset: 0x0001A80C
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0001C624 File Offset: 0x0001A824
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = value != this.m_Name;
				if (flag)
				{
					this.m_Name = value;
				}
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0001C64C File Offset: 0x0001A84C
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0001C664 File Offset: 0x0001A864
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
			set
			{
				bool flag = value != this.m_HashCode;
				if (flag)
				{
					this.m_HashCode = value;
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0001C68C File Offset: 0x0001A88C
		public string styleOpeningDefinition
		{
			get
			{
				return this.m_OpeningDefinition;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0001C6A4 File Offset: 0x0001A8A4
		public string styleClosingDefinition
		{
			get
			{
				return this.m_ClosingDefinition;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0001C6BC File Offset: 0x0001A8BC
		public int[] styleOpeningTagArray
		{
			get
			{
				return this.m_OpeningTagArray;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0001C6D4 File Offset: 0x0001A8D4
		public int[] styleClosingTagArray
		{
			get
			{
				return this.m_ClosingTagArray;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0001C6EC File Offset: 0x0001A8EC
		internal TextStyle(string styleName, string styleOpeningDefinition, string styleClosingDefinition)
		{
			this.m_Name = styleName;
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(styleName);
			this.m_OpeningDefinition = styleOpeningDefinition;
			this.m_ClosingDefinition = styleClosingDefinition;
			this.RefreshStyle();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0001C720 File Offset: 0x0001A920
		public void RefreshStyle()
		{
			this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Name);
			this.m_OpeningTagArray = new int[this.m_OpeningDefinition.Length];
			this.m_OpeningTagUnicodeArray = new uint[this.m_OpeningDefinition.Length];
			for (int i = 0; i < this.m_OpeningDefinition.Length; i++)
			{
				this.m_OpeningTagArray[i] = (int)this.m_OpeningDefinition.get_Chars(i);
				this.m_OpeningTagUnicodeArray[i] = (uint)this.m_OpeningDefinition.get_Chars(i);
			}
			this.m_ClosingTagArray = new int[this.m_ClosingDefinition.Length];
			this.m_ClosingTagUnicodeArray = new uint[this.m_ClosingDefinition.Length];
			for (int j = 0; j < this.m_ClosingDefinition.Length; j++)
			{
				this.m_ClosingTagArray[j] = (int)this.m_ClosingDefinition.get_Chars(j);
				this.m_ClosingTagUnicodeArray[j] = (uint)this.m_ClosingDefinition.get_Chars(j);
			}
		}

		// Token: 0x04000380 RID: 896
		internal static TextStyle k_NormalStyle;

		// Token: 0x04000381 RID: 897
		[SerializeField]
		private string m_Name;

		// Token: 0x04000382 RID: 898
		[SerializeField]
		private int m_HashCode;

		// Token: 0x04000383 RID: 899
		[SerializeField]
		private string m_OpeningDefinition;

		// Token: 0x04000384 RID: 900
		[SerializeField]
		private string m_ClosingDefinition;

		// Token: 0x04000385 RID: 901
		[SerializeField]
		private int[] m_OpeningTagArray;

		// Token: 0x04000386 RID: 902
		[SerializeField]
		private int[] m_ClosingTagArray;

		// Token: 0x04000387 RID: 903
		[SerializeField]
		internal uint[] m_OpeningTagUnicodeArray;

		// Token: 0x04000388 RID: 904
		[SerializeField]
		internal uint[] m_ClosingTagUnicodeArray;
	}
}

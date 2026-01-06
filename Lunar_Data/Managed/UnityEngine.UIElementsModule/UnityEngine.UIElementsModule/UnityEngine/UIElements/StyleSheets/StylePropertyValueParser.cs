using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000371 RID: 881
	internal class StylePropertyValueParser
	{
		// Token: 0x06001C34 RID: 7220 RVA: 0x00083650 File Offset: 0x00081850
		public string[] Parse(string propertyValue)
		{
			this.m_PropertyValue = propertyValue;
			this.m_ValueList.Clear();
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			this.m_ParseIndex = 0;
			while (this.m_ParseIndex < this.m_PropertyValue.Length)
			{
				char c = this.m_PropertyValue.get_Chars(this.m_ParseIndex);
				char c2 = c;
				char c3 = c2;
				if (c3 != ' ')
				{
					if (c3 != '(')
					{
						if (c3 != ',')
						{
							this.m_StringBuilder.Append(c);
						}
						else
						{
							this.EatSpace();
							this.AddValuePart();
							this.m_ValueList.Add(",");
						}
					}
					else
					{
						this.AppendFunction();
					}
				}
				else
				{
					this.EatSpace();
					this.AddValuePart();
				}
				this.m_ParseIndex++;
			}
			string text = this.m_StringBuilder.ToString();
			bool flag = !string.IsNullOrEmpty(text);
			if (flag)
			{
				this.m_ValueList.Add(text);
			}
			return this.m_ValueList.ToArray();
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00083770 File Offset: 0x00081970
		private void AddValuePart()
		{
			string text = this.m_StringBuilder.ToString();
			this.m_StringBuilder.Remove(0, this.m_StringBuilder.Length);
			this.m_ValueList.Add(text);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000837B0 File Offset: 0x000819B0
		private void AppendFunction()
		{
			while (this.m_ParseIndex < this.m_PropertyValue.Length && this.m_PropertyValue.get_Chars(this.m_ParseIndex) != ')')
			{
				this.m_StringBuilder.Append(this.m_PropertyValue.get_Chars(this.m_ParseIndex));
				this.m_ParseIndex++;
			}
			this.m_StringBuilder.Append(this.m_PropertyValue.get_Chars(this.m_ParseIndex));
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x0008383C File Offset: 0x00081A3C
		private void EatSpace()
		{
			while (this.m_ParseIndex + 1 < this.m_PropertyValue.Length && this.m_PropertyValue.get_Chars(this.m_ParseIndex + 1) == ' ')
			{
				this.m_ParseIndex++;
			}
		}

		// Token: 0x04000E02 RID: 3586
		private string m_PropertyValue;

		// Token: 0x04000E03 RID: 3587
		private List<string> m_ValueList = new List<string>();

		// Token: 0x04000E04 RID: 3588
		private StringBuilder m_StringBuilder = new StringBuilder();

		// Token: 0x04000E05 RID: 3589
		private int m_ParseIndex = 0;
	}
}

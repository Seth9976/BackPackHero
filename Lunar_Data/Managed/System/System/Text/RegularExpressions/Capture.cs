using System;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single successful subexpression capture. </summary>
	// Token: 0x020001E7 RID: 487
	public class Capture
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x0003543F File Offset: 0x0003363F
		internal Capture(string text, int index, int length)
		{
			this.Text = text;
			this.Index = index;
			this.Length = length;
		}

		/// <summary>The position in the original string where the first character of the captured substring is found.</summary>
		/// <returns>The zero-based starting position in the original string where the captured substring is found.</returns>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0003545C File Offset: 0x0003365C
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00035464 File Offset: 0x00033664
		public int Index { get; private protected set; }

		/// <summary>Gets the length of the captured substring.</summary>
		/// <returns>The length of the captured substring.</returns>
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0003546D File Offset: 0x0003366D
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x00035475 File Offset: 0x00033675
		public int Length { get; private protected set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0003547E File Offset: 0x0003367E
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x00035486 File Offset: 0x00033686
		protected internal string Text { internal get; private protected set; }

		/// <summary>Gets the captured substring from the input string.</summary>
		/// <returns>The substring that is captured by the match.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0003548F File Offset: 0x0003368F
		public string Value
		{
			get
			{
				return this.Text.Substring(this.Index, this.Length);
			}
		}

		/// <summary>Retrieves the captured substring from the input string by calling the <see cref="P:System.Text.RegularExpressions.Capture.Value" /> property. </summary>
		/// <returns>The substring that was captured by the match.</returns>
		// Token: 0x06000CC0 RID: 3264 RVA: 0x000354A8 File Offset: 0x000336A8
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000354B0 File Offset: 0x000336B0
		internal ReadOnlySpan<char> GetLeftSubstring()
		{
			return this.Text.AsSpan(0, this.Index);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000354C4 File Offset: 0x000336C4
		internal ReadOnlySpan<char> GetRightSubstring()
		{
			return this.Text.AsSpan(this.Index + this.Length, this.Text.Length - this.Index - this.Length);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00013B26 File Offset: 0x00011D26
		internal Capture()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

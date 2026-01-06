using System;
using System.Globalization;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012F RID: 303
	public struct InternedString : IEquatable<InternedString>, IComparable<InternedString>
	{
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0004F6CA File Offset: 0x0004D8CA
		public int length
		{
			get
			{
				string stringLowerCase = this.m_StringLowerCase;
				if (stringLowerCase == null)
				{
					return 0;
				}
				return stringLowerCase.Length;
			}
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0004F6DD File Offset: 0x0004D8DD
		public InternedString(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				this.m_StringOriginalCase = null;
				this.m_StringLowerCase = null;
				return;
			}
			this.m_StringOriginalCase = string.Intern(text);
			this.m_StringLowerCase = string.Intern(text.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0004F718 File Offset: 0x0004D918
		public bool IsEmpty()
		{
			return this.m_StringLowerCase == null;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0004F723 File Offset: 0x0004D923
		public string ToLower()
		{
			return this.m_StringLowerCase;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0004F72C File Offset: 0x0004D92C
		public override bool Equals(object obj)
		{
			if (obj is InternedString)
			{
				InternedString internedString = (InternedString)obj;
				return this.Equals(internedString);
			}
			string text = obj as string;
			if (text == null)
			{
				return false;
			}
			if (this.m_StringLowerCase == null)
			{
				return string.IsNullOrEmpty(text);
			}
			return string.Equals(this.m_StringLowerCase, text.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0004F781 File Offset: 0x0004D981
		public bool Equals(InternedString other)
		{
			return this.m_StringLowerCase == other.m_StringLowerCase;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0004F791 File Offset: 0x0004D991
		public int CompareTo(InternedString other)
		{
			return string.Compare(this.m_StringLowerCase, other.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0004F7A5 File Offset: 0x0004D9A5
		public override int GetHashCode()
		{
			if (this.m_StringLowerCase == null)
			{
				return 0;
			}
			return this.m_StringLowerCase.GetHashCode();
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0004F7BC File Offset: 0x0004D9BC
		public override string ToString()
		{
			return this.m_StringOriginalCase ?? string.Empty;
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0004F7CD File Offset: 0x0004D9CD
		public static bool operator ==(InternedString a, InternedString b)
		{
			return a.Equals(b);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0004F7D7 File Offset: 0x0004D9D7
		public static bool operator !=(InternedString a, InternedString b)
		{
			return !a.Equals(b);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0004F7E4 File Offset: 0x0004D9E4
		public static bool operator ==(InternedString a, string b)
		{
			return string.Compare(a.m_StringLowerCase, b.ToLower(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0004F800 File Offset: 0x0004DA00
		public static bool operator !=(InternedString a, string b)
		{
			return string.Compare(a.m_StringLowerCase, b.ToLower(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase) != 0;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0004F81C File Offset: 0x0004DA1C
		public static bool operator ==(string a, InternedString b)
		{
			return string.Compare(a.ToLower(CultureInfo.InvariantCulture), b.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0004F838 File Offset: 0x0004DA38
		public static bool operator !=(string a, InternedString b)
		{
			return string.Compare(a.ToLower(CultureInfo.InvariantCulture), b.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) != 0;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0004F854 File Offset: 0x0004DA54
		public static bool operator <(InternedString left, InternedString right)
		{
			return string.Compare(left.m_StringLowerCase, right.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) < 0;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0004F86B File Offset: 0x0004DA6B
		public static bool operator >(InternedString left, InternedString right)
		{
			return string.Compare(left.m_StringLowerCase, right.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) > 0;
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0004F882 File Offset: 0x0004DA82
		public static implicit operator string(InternedString str)
		{
			return str.ToString();
		}

		// Token: 0x040006BA RID: 1722
		private readonly string m_StringOriginalCase;

		// Token: 0x040006BB RID: 1723
		private readonly string m_StringLowerCase;
	}
}

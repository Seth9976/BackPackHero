using System;
using System.Globalization;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200012F RID: 303
	public struct InternedString : IEquatable<InternedString>, IComparable<InternedString>
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0004F872 File Offset: 0x0004DA72
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

		// Token: 0x060010B9 RID: 4281 RVA: 0x0004F885 File Offset: 0x0004DA85
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

		// Token: 0x060010BA RID: 4282 RVA: 0x0004F8C0 File Offset: 0x0004DAC0
		public bool IsEmpty()
		{
			return this.m_StringLowerCase == null;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0004F8CB File Offset: 0x0004DACB
		public string ToLower()
		{
			return this.m_StringLowerCase;
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0004F8D4 File Offset: 0x0004DAD4
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

		// Token: 0x060010BD RID: 4285 RVA: 0x0004F929 File Offset: 0x0004DB29
		public bool Equals(InternedString other)
		{
			return this.m_StringLowerCase == other.m_StringLowerCase;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0004F939 File Offset: 0x0004DB39
		public int CompareTo(InternedString other)
		{
			return string.Compare(this.m_StringLowerCase, other.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0004F94D File Offset: 0x0004DB4D
		public override int GetHashCode()
		{
			if (this.m_StringLowerCase == null)
			{
				return 0;
			}
			return this.m_StringLowerCase.GetHashCode();
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0004F964 File Offset: 0x0004DB64
		public override string ToString()
		{
			return this.m_StringOriginalCase ?? string.Empty;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0004F975 File Offset: 0x0004DB75
		public static bool operator ==(InternedString a, InternedString b)
		{
			return a.Equals(b);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0004F97F File Offset: 0x0004DB7F
		public static bool operator !=(InternedString a, InternedString b)
		{
			return !a.Equals(b);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0004F98C File Offset: 0x0004DB8C
		public static bool operator ==(InternedString a, string b)
		{
			return string.Compare(a.m_StringLowerCase, b.ToLower(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0004F9A8 File Offset: 0x0004DBA8
		public static bool operator !=(InternedString a, string b)
		{
			return string.Compare(a.m_StringLowerCase, b.ToLower(CultureInfo.InvariantCulture), StringComparison.InvariantCultureIgnoreCase) != 0;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0004F9C4 File Offset: 0x0004DBC4
		public static bool operator ==(string a, InternedString b)
		{
			return string.Compare(a.ToLower(CultureInfo.InvariantCulture), b.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0004F9E0 File Offset: 0x0004DBE0
		public static bool operator !=(string a, InternedString b)
		{
			return string.Compare(a.ToLower(CultureInfo.InvariantCulture), b.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) != 0;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0004F9FC File Offset: 0x0004DBFC
		public static bool operator <(InternedString left, InternedString right)
		{
			return string.Compare(left.m_StringLowerCase, right.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) < 0;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0004FA13 File Offset: 0x0004DC13
		public static bool operator >(InternedString left, InternedString right)
		{
			return string.Compare(left.m_StringLowerCase, right.m_StringLowerCase, StringComparison.InvariantCultureIgnoreCase) > 0;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0004FA2A File Offset: 0x0004DC2A
		public static implicit operator string(InternedString str)
		{
			return str.ToString();
		}

		// Token: 0x040006BB RID: 1723
		private readonly string m_StringOriginalCase;

		// Token: 0x040006BC RID: 1724
		private readonly string m_StringLowerCase;
	}
}

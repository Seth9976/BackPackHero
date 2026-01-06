using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000145 RID: 325
	internal struct Substring : IComparable<Substring>, IEquatable<Substring>
	{
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x00053869 File Offset: 0x00051A69
		public bool isEmpty
		{
			get
			{
				return this.m_Length == 0;
			}
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00053874 File Offset: 0x00051A74
		public Substring(string str)
		{
			this.m_String = str;
			this.m_Index = 0;
			if (str != null)
			{
				this.m_Length = str.Length;
				return;
			}
			this.m_Length = 0;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0005389B File Offset: 0x00051A9B
		public Substring(string str, int index, int length)
		{
			this.m_String = str;
			this.m_Index = index;
			this.m_Length = length;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000538B2 File Offset: 0x00051AB2
		public Substring(string str, int index)
		{
			this.m_String = str;
			this.m_Index = index;
			this.m_Length = str.Length - index;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000538D0 File Offset: 0x00051AD0
		public override bool Equals(object obj)
		{
			if (obj is Substring)
			{
				Substring substring = (Substring)obj;
				return this.Equals(substring);
			}
			string text = obj as string;
			return text != null && this.Equals(text);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00053908 File Offset: 0x00051B08
		public bool Equals(string other)
		{
			if (string.IsNullOrEmpty(other))
			{
				return this.m_Length == 0;
			}
			if (other.Length != this.m_Length)
			{
				return false;
			}
			for (int i = 0; i < this.m_Length; i++)
			{
				if (other[i] != this.m_String[this.m_Index + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00053967 File Offset: 0x00051B67
		public bool Equals(Substring other)
		{
			return this.CompareTo(other) == 0;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00053973 File Offset: 0x00051B73
		public bool Equals(InternedString other)
		{
			return this.length == other.length && string.Compare(this.m_String, this.m_Index, other.ToString(), 0, this.length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000539AF File Offset: 0x00051BAF
		public int CompareTo(Substring other)
		{
			return Substring.Compare(this, other, StringComparison.CurrentCulture);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x000539C0 File Offset: 0x00051BC0
		public static int Compare(Substring left, Substring right, StringComparison comparison)
		{
			if (left.m_Length == right.m_Length)
			{
				return string.Compare(left.m_String, left.m_Index, right.m_String, right.m_Index, left.m_Length, comparison);
			}
			if (left.m_Length < right.m_Length)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00053A14 File Offset: 0x00051C14
		public bool StartsWith(string str)
		{
			if (str.Length > this.length)
			{
				return false;
			}
			for (int i = 0; i < str.Length; i++)
			{
				if (this.m_String[this.m_Index + i] != str[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00053A61 File Offset: 0x00051C61
		public string Substr(int index = 0, int length = -1)
		{
			if (length < 0)
			{
				length = this.length - index;
			}
			return this.m_String.Substring(this.m_Index + index, length);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00053A85 File Offset: 0x00051C85
		public override string ToString()
		{
			if (this.m_String == null)
			{
				return string.Empty;
			}
			return this.m_String.Substring(this.m_Index, this.m_Length);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00053AAC File Offset: 0x00051CAC
		public override int GetHashCode()
		{
			if (this.m_String == null)
			{
				return 0;
			}
			if (this.m_Index == 0 && this.m_Length == this.m_String.Length)
			{
				return this.m_String.GetHashCode();
			}
			return this.ToString().GetHashCode();
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x00053AFB File Offset: 0x00051CFB
		public static bool operator ==(Substring a, Substring b)
		{
			return a.Equals(b);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00053B05 File Offset: 0x00051D05
		public static bool operator !=(Substring a, Substring b)
		{
			return !a.Equals(b);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00053B12 File Offset: 0x00051D12
		public static bool operator ==(Substring a, InternedString b)
		{
			return a.Equals(b);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00053B1C File Offset: 0x00051D1C
		public static bool operator !=(Substring a, InternedString b)
		{
			return !a.Equals(b);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00053B29 File Offset: 0x00051D29
		public static bool operator ==(InternedString a, Substring b)
		{
			return b.Equals(a);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00053B33 File Offset: 0x00051D33
		public static bool operator !=(InternedString a, Substring b)
		{
			return !b.Equals(a);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00053B40 File Offset: 0x00051D40
		public static implicit operator Substring(string s)
		{
			return new Substring(s);
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00053B48 File Offset: 0x00051D48
		public int length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00053B50 File Offset: 0x00051D50
		public int index
		{
			get
			{
				return this.m_Index;
			}
		}

		// Token: 0x170004C5 RID: 1221
		public char this[int index]
		{
			get
			{
				if (index < 0 || index >= this.m_Length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.m_String[this.m_Index + index];
			}
		}

		// Token: 0x040006E9 RID: 1769
		private readonly string m_String;

		// Token: 0x040006EA RID: 1770
		private readonly int m_Index;

		// Token: 0x040006EB RID: 1771
		private readonly int m_Length;
	}
}

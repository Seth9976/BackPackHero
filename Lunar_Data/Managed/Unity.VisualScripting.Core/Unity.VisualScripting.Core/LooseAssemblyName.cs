using System;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x020000D0 RID: 208
	public struct LooseAssemblyName
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0000C52C File Offset: 0x0000A72C
		public LooseAssemblyName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000C543 File Offset: 0x0000A743
		public override bool Equals(object obj)
		{
			return obj is LooseAssemblyName && ((LooseAssemblyName)obj).name == this.name;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000C565 File Offset: 0x0000A765
		public override int GetHashCode()
		{
			return HashUtility.GetHashCode<string>(this.name);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000C572 File Offset: 0x0000A772
		public static bool operator ==(LooseAssemblyName a, LooseAssemblyName b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000C587 File Offset: 0x0000A787
		public static bool operator !=(LooseAssemblyName a, LooseAssemblyName b)
		{
			return !(a == b);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000C593 File Offset: 0x0000A793
		public static implicit operator LooseAssemblyName(string name)
		{
			return new LooseAssemblyName(name);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000C59B File Offset: 0x0000A79B
		public static implicit operator string(LooseAssemblyName name)
		{
			return name.name;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000C5A3 File Offset: 0x0000A7A3
		public static explicit operator LooseAssemblyName(AssemblyName strongAssemblyName)
		{
			return new LooseAssemblyName(strongAssemblyName.Name);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x04000126 RID: 294
		public readonly string name;
	}
}

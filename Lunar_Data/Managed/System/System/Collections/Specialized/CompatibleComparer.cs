using System;
using System.Globalization;

namespace System.Collections.Specialized
{
	// Token: 0x020007D2 RID: 2002
	[Serializable]
	internal class CompatibleComparer : IEqualityComparer
	{
		// Token: 0x06003FD0 RID: 16336 RVA: 0x000DF453 File Offset: 0x000DD653
		internal CompatibleComparer(IComparer comparer, IHashCodeProvider hashCodeProvider)
		{
			this._comparer = comparer;
			this._hcp = hashCodeProvider;
		}

		// Token: 0x06003FD1 RID: 16337 RVA: 0x000DF46C File Offset: 0x000DD66C
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			try
			{
				if (this._comparer != null)
				{
					return this._comparer.Compare(a, b) == 0;
				}
				IComparable comparable = a as IComparable;
				if (comparable != null)
				{
					return comparable.CompareTo(b) == 0;
				}
			}
			catch (ArgumentException)
			{
				return false;
			}
			return a.Equals(b);
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x000DF4DC File Offset: 0x000DD6DC
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._hcp != null)
			{
				return this._hcp.GetHashCode(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x000DF507 File Offset: 0x000DD707
		public IComparer Comparer
		{
			get
			{
				return this._comparer;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x000DF50F File Offset: 0x000DD70F
		public IHashCodeProvider HashCodeProvider
		{
			get
			{
				return this._hcp;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x000DF517 File Offset: 0x000DD717
		public static IComparer DefaultComparer
		{
			get
			{
				if (CompatibleComparer.defaultComparer == null)
				{
					CompatibleComparer.defaultComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultComparer;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003FD6 RID: 16342 RVA: 0x000DF53A File Offset: 0x000DD73A
		public static IHashCodeProvider DefaultHashCodeProvider
		{
			get
			{
				if (CompatibleComparer.defaultHashProvider == null)
				{
					CompatibleComparer.defaultHashProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture);
				}
				return CompatibleComparer.defaultHashProvider;
			}
		}

		// Token: 0x040026A4 RID: 9892
		private IComparer _comparer;

		// Token: 0x040026A5 RID: 9893
		private static volatile IComparer defaultComparer;

		// Token: 0x040026A6 RID: 9894
		private IHashCodeProvider _hcp;

		// Token: 0x040026A7 RID: 9895
		private static volatile IHashCodeProvider defaultHashProvider;
	}
}

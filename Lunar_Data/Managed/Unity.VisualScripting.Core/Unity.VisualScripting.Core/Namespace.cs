using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Unity.VisualScripting
{
	// Token: 0x020000D6 RID: 214
	public sealed class Namespace
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x0000F900 File Offset: 0x0000DB00
		private Namespace(string fullName)
		{
			this.FullName = fullName;
			if (fullName == null)
			{
				this.Root = this;
				this.IsRoot = true;
				this.IsGlobal = true;
				return;
			}
			string[] array = fullName.Split('.', StringSplitOptions.None);
			this.Name = array[array.Length - 1];
			if (array.Length > 1)
			{
				this.Root = array[0];
				this.Parent = fullName.Substring(0, fullName.LastIndexOf('.'));
				return;
			}
			this.Root = this;
			this.IsRoot = true;
			this.Parent = Namespace.Global;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0000F992 File Offset: 0x0000DB92
		public Namespace Root { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000F99A File Offset: 0x0000DB9A
		public Namespace Parent { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000F9A2 File Offset: 0x0000DBA2
		public string FullName { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000F9AA File Offset: 0x0000DBAA
		public string Name { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000F9B2 File Offset: 0x0000DBB2
		public bool IsRoot { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000F9BA File Offset: 0x0000DBBA
		public bool IsGlobal { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000F9C2 File Offset: 0x0000DBC2
		public IEnumerable<Namespace> Ancestors
		{
			get
			{
				Namespace ancestor = this.Parent;
				while (ancestor != null)
				{
					yield return ancestor;
					ancestor = ancestor.Parent;
				}
				yield break;
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000F9D2 File Offset: 0x0000DBD2
		public IEnumerable<Namespace> AndAncestors()
		{
			yield return this;
			foreach (Namespace @namespace in this.Ancestors)
			{
				yield return @namespace;
			}
			IEnumerator<Namespace> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000F9E2 File Offset: 0x0000DBE2
		public override int GetHashCode()
		{
			if (this.FullName == null)
			{
				return 0;
			}
			return this.FullName.GetHashCode();
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0000F9F9 File Offset: 0x0000DBF9
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000FA18 File Offset: 0x0000DC18
		public static Namespace Global { get; } = new Namespace(null);

		// Token: 0x06000610 RID: 1552 RVA: 0x0000FA20 File Offset: 0x0000DC20
		public static Namespace FromFullName(string fullName)
		{
			if (fullName == null)
			{
				return Namespace.Global;
			}
			Namespace @namespace;
			if (!Namespace.collection.TryGetValue(fullName, out @namespace))
			{
				@namespace = new Namespace(fullName);
				Namespace.collection.Add(@namespace);
			}
			return @namespace;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0000FA58 File Offset: 0x0000DC58
		public override bool Equals(object obj)
		{
			Namespace @namespace = obj as Namespace;
			return !(@namespace == null) && this.FullName == @namespace.FullName;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0000FA88 File Offset: 0x0000DC88
		public static implicit operator Namespace(string fullName)
		{
			return Namespace.FromFullName(fullName);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public static implicit operator string(Namespace @namespace)
		{
			return @namespace.FullName;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0000FA98 File Offset: 0x0000DC98
		public static bool operator ==(Namespace a, Namespace b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0000FAAF File Offset: 0x0000DCAF
		public static bool operator !=(Namespace a, Namespace b)
		{
			return !(a == b);
		}

		// Token: 0x04000159 RID: 345
		private static readonly Namespace.Collection collection = new Namespace.Collection();

		// Token: 0x020001DA RID: 474
		private class Collection : KeyedCollection<string, Namespace>, IKeyedCollection<string, Namespace>, ICollection<Namespace>, IEnumerable<Namespace>, IEnumerable
		{
			// Token: 0x06000C49 RID: 3145 RVA: 0x00032CE5 File Offset: 0x00030EE5
			protected override string GetKeyForItem(Namespace item)
			{
				return item.FullName;
			}

			// Token: 0x06000C4A RID: 3146 RVA: 0x00032CED File Offset: 0x00030EED
			public new bool TryGetValue(string key, out Namespace value)
			{
				if (base.Dictionary == null)
				{
					value = null;
					return false;
				}
				return base.Dictionary.TryGetValue(key, out value);
			}

			// Token: 0x06000C4C RID: 3148 RVA: 0x00032D11 File Offset: 0x00030F11
			Namespace IKeyedCollection<string, Namespace>.get_Item(string key)
			{
				return base[key];
			}

			// Token: 0x06000C4D RID: 3149 RVA: 0x00032D1A File Offset: 0x00030F1A
			bool IKeyedCollection<string, Namespace>.Contains(string key)
			{
				return base.Contains(key);
			}

			// Token: 0x06000C4E RID: 3150 RVA: 0x00032D23 File Offset: 0x00030F23
			bool IKeyedCollection<string, Namespace>.Remove(string key)
			{
				return base.Remove(key);
			}
		}
	}
}

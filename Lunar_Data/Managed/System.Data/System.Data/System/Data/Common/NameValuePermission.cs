using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x02000377 RID: 887
	[Serializable]
	internal sealed class NameValuePermission : IComparable
	{
		// Token: 0x06002AFC RID: 11004 RVA: 0x00003D55 File Offset: 0x00001F55
		internal NameValuePermission()
		{
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000BDD7A File Offset: 0x000BBF7A
		private NameValuePermission(string keyword)
		{
			this._value = keyword;
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000BDD89 File Offset: 0x000BBF89
		private NameValuePermission(string value, DBConnectionString entry)
		{
			this._value = value;
			this._entry = entry;
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000BDDA0 File Offset: 0x000BBFA0
		private NameValuePermission(NameValuePermission permit)
		{
			this._value = permit._value;
			this._entry = permit._entry;
			this._tree = permit._tree;
			if (this._tree != null)
			{
				NameValuePermission[] array = this._tree.Clone() as NameValuePermission[];
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						array[i] = array[i].CopyNameValue();
					}
				}
				this._tree = array;
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000BDE15 File Offset: 0x000BC015
		int IComparable.CompareTo(object a)
		{
			return StringComparer.Ordinal.Compare(this._value, ((NameValuePermission)a)._value);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000BDE34 File Offset: 0x000BC034
		internal static void AddEntry(NameValuePermission kvtree, ArrayList entries, DBConnectionString entry)
		{
			if (entry.KeyChain != null)
			{
				for (NameValuePair nameValuePair = entry.KeyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
				{
					NameValuePermission nameValuePermission = kvtree.CheckKeyForValue(nameValuePair.Name);
					if (nameValuePermission == null)
					{
						nameValuePermission = new NameValuePermission(nameValuePair.Name);
						kvtree.Add(nameValuePermission);
					}
					kvtree = nameValuePermission;
					nameValuePermission = kvtree.CheckKeyForValue(nameValuePair.Value);
					if (nameValuePermission == null)
					{
						DBConnectionString dbconnectionString = ((nameValuePair.Next != null) ? null : entry);
						nameValuePermission = new NameValuePermission(nameValuePair.Value, dbconnectionString);
						kvtree.Add(nameValuePermission);
						if (dbconnectionString != null)
						{
							entries.Add(dbconnectionString);
						}
					}
					else if (nameValuePair.Next == null)
					{
						if (nameValuePermission._entry != null)
						{
							entries.Remove(nameValuePermission._entry);
							nameValuePermission._entry = nameValuePermission._entry.Intersect(entry);
						}
						else
						{
							nameValuePermission._entry = entry;
						}
						entries.Add(nameValuePermission._entry);
					}
					kvtree = nameValuePermission;
				}
				return;
			}
			DBConnectionString entry2 = kvtree._entry;
			if (entry2 != null)
			{
				entries.Remove(entry2);
				kvtree._entry = entry2.Intersect(entry);
			}
			else
			{
				kvtree._entry = entry;
			}
			entries.Add(kvtree._entry);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000BDF48 File Offset: 0x000BC148
		internal void Intersect(ArrayList entries, NameValuePermission target)
		{
			if (target == null)
			{
				this._tree = null;
				this._entry = null;
				return;
			}
			if (this._entry != null)
			{
				entries.Remove(this._entry);
				this._entry = this._entry.Intersect(target._entry);
				entries.Add(this._entry);
			}
			else if (target._entry != null)
			{
				this._entry = target._entry.Intersect(null);
				entries.Add(this._entry);
			}
			if (this._tree != null)
			{
				int num = this._tree.Length;
				for (int i = 0; i < this._tree.Length; i++)
				{
					NameValuePermission nameValuePermission = target.CheckKeyForValue(this._tree[i]._value);
					if (nameValuePermission != null)
					{
						this._tree[i].Intersect(entries, nameValuePermission);
					}
					else
					{
						this._tree[i] = null;
						num--;
					}
				}
				if (num == 0)
				{
					this._tree = null;
					return;
				}
				if (num < this._tree.Length)
				{
					NameValuePermission[] array = new NameValuePermission[num];
					int j = 0;
					int num2 = 0;
					while (j < this._tree.Length)
					{
						if (this._tree[j] != null)
						{
							array[num2++] = this._tree[j];
						}
						j++;
					}
					this._tree = array;
				}
			}
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000BE080 File Offset: 0x000BC280
		private void Add(NameValuePermission permit)
		{
			NameValuePermission[] tree = this._tree;
			int num = ((tree != null) ? tree.Length : 0);
			NameValuePermission[] array = new NameValuePermission[1 + num];
			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = tree[i];
			}
			array[num] = permit;
			Array.Sort<NameValuePermission>(array);
			this._tree = array;
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000BE0D0 File Offset: 0x000BC2D0
		internal bool CheckValueForKeyPermit(DBConnectionString parsetable)
		{
			if (parsetable == null)
			{
				return false;
			}
			bool flag = false;
			NameValuePermission[] tree = this._tree;
			if (tree != null)
			{
				flag = parsetable.IsEmpty;
				if (!flag)
				{
					foreach (NameValuePermission nameValuePermission in tree)
					{
						if (nameValuePermission != null)
						{
							string value = nameValuePermission._value;
							if (parsetable.ContainsKey(value))
							{
								string text = parsetable[value];
								NameValuePermission nameValuePermission2 = nameValuePermission.CheckKeyForValue(text);
								if (nameValuePermission2 == null)
								{
									return false;
								}
								if (!nameValuePermission2.CheckValueForKeyPermit(parsetable))
								{
									return false;
								}
								flag = true;
							}
						}
					}
				}
			}
			DBConnectionString entry = this._entry;
			if (entry != null)
			{
				flag = entry.IsSupersetOf(parsetable);
			}
			return flag;
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000BE160 File Offset: 0x000BC360
		private NameValuePermission CheckKeyForValue(string keyInQuestion)
		{
			NameValuePermission[] tree = this._tree;
			if (tree != null)
			{
				foreach (NameValuePermission nameValuePermission in tree)
				{
					if (string.Equals(keyInQuestion, nameValuePermission._value, StringComparison.OrdinalIgnoreCase))
					{
						return nameValuePermission;
					}
				}
			}
			return null;
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000BE19B File Offset: 0x000BC39B
		internal NameValuePermission CopyNameValue()
		{
			return new NameValuePermission(this);
		}

		// Token: 0x040019EE RID: 6638
		private string _value;

		// Token: 0x040019EF RID: 6639
		private DBConnectionString _entry;

		// Token: 0x040019F0 RID: 6640
		private NameValuePermission[] _tree;

		// Token: 0x040019F1 RID: 6641
		internal static readonly NameValuePermission Default;
	}
}

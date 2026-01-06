using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace System.Data.Common
{
	// Token: 0x02000375 RID: 885
	[Serializable]
	internal sealed class DBConnectionString
	{
		// Token: 0x06002AE7 RID: 10983 RVA: 0x000BD6A2 File Offset: 0x000BB8A2
		internal DBConnectionString(string value, string restrictions, KeyRestrictionBehavior behavior, Dictionary<string, string> synonyms, bool useOdbcRules)
			: this(new DbConnectionOptions(value, synonyms, useOdbcRules), restrictions, behavior, synonyms, false)
		{
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000BD6B9 File Offset: 0x000BB8B9
		internal DBConnectionString(DbConnectionOptions connectionOptions)
			: this(connectionOptions, null, KeyRestrictionBehavior.AllowOnly, null, true)
		{
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000BD6C8 File Offset: 0x000BB8C8
		private DBConnectionString(DbConnectionOptions connectionOptions, string restrictions, KeyRestrictionBehavior behavior, Dictionary<string, string> synonyms, bool mustCloneDictionary)
		{
			if (behavior <= KeyRestrictionBehavior.PreventUsage)
			{
				this._behavior = behavior;
				this._encryptedUsersConnectionString = connectionOptions.UsersConnectionString(false);
				this._hasPassword = connectionOptions._hasPasswordKeyword;
				this._parsetable = connectionOptions.Parsetable;
				this._keychain = connectionOptions._keyChain;
				if (this._hasPassword && !connectionOptions.HasPersistablePassword)
				{
					if (mustCloneDictionary)
					{
						this._parsetable = new Dictionary<string, string>(this._parsetable);
					}
					if (this._parsetable.ContainsKey("password"))
					{
						this._parsetable["password"] = "*";
					}
					if (this._parsetable.ContainsKey("pwd"))
					{
						this._parsetable["pwd"] = "*";
					}
					this._keychain = connectionOptions.ReplacePasswordPwd(out this._encryptedUsersConnectionString, true);
				}
				if (!string.IsNullOrEmpty(restrictions))
				{
					this._restrictionValues = DBConnectionString.ParseRestrictions(restrictions, synonyms);
					this._restrictions = restrictions;
				}
				return;
			}
			throw ADP.InvalidKeyRestrictionBehavior(behavior);
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000BD7C4 File Offset: 0x000BB9C4
		private DBConnectionString(DBConnectionString connectionString, string[] restrictionValues, KeyRestrictionBehavior behavior)
		{
			this._encryptedUsersConnectionString = connectionString._encryptedUsersConnectionString;
			this._parsetable = connectionString._parsetable;
			this._keychain = connectionString._keychain;
			this._hasPassword = connectionString._hasPassword;
			this._restrictionValues = restrictionValues;
			this._restrictions = null;
			this._behavior = behavior;
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x000BD81C File Offset: 0x000BBA1C
		internal KeyRestrictionBehavior Behavior
		{
			get
			{
				return this._behavior;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06002AEC RID: 10988 RVA: 0x000BD824 File Offset: 0x000BBA24
		internal string ConnectionString
		{
			get
			{
				return this._encryptedUsersConnectionString;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x000BD82C File Offset: 0x000BBA2C
		internal bool IsEmpty
		{
			get
			{
				return this._keychain == null;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x000BD837 File Offset: 0x000BBA37
		internal NameValuePair KeyChain
		{
			get
			{
				return this._keychain;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x000BD840 File Offset: 0x000BBA40
		internal string Restrictions
		{
			get
			{
				string text = this._restrictions;
				if (text == null)
				{
					string[] restrictionValues = this._restrictionValues;
					if (restrictionValues != null && restrictionValues.Length != 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						for (int i = 0; i < restrictionValues.Length; i++)
						{
							if (!string.IsNullOrEmpty(restrictionValues[i]))
							{
								stringBuilder.Append(restrictionValues[i]);
								stringBuilder.Append("=;");
							}
						}
						text = stringBuilder.ToString();
					}
				}
				if (text == null)
				{
					return "";
				}
				return text;
			}
		}

		// Token: 0x17000727 RID: 1831
		internal string this[string keyword]
		{
			get
			{
				return this._parsetable[keyword];
			}
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000BD8B8 File Offset: 0x000BBAB8
		internal bool ContainsKey(string keyword)
		{
			return this._parsetable.ContainsKey(keyword);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000BD8C8 File Offset: 0x000BBAC8
		internal DBConnectionString Intersect(DBConnectionString entry)
		{
			KeyRestrictionBehavior keyRestrictionBehavior = this._behavior;
			string[] array = null;
			if (entry == null)
			{
				keyRestrictionBehavior = KeyRestrictionBehavior.AllowOnly;
			}
			else if (this._behavior != entry._behavior)
			{
				keyRestrictionBehavior = KeyRestrictionBehavior.AllowOnly;
				if (entry._behavior == KeyRestrictionBehavior.AllowOnly)
				{
					if (!ADP.IsEmptyArray(this._restrictionValues))
					{
						if (!ADP.IsEmptyArray(entry._restrictionValues))
						{
							array = DBConnectionString.NewRestrictionAllowOnly(entry._restrictionValues, this._restrictionValues);
						}
					}
					else
					{
						array = entry._restrictionValues;
					}
				}
				else if (!ADP.IsEmptyArray(this._restrictionValues))
				{
					if (!ADP.IsEmptyArray(entry._restrictionValues))
					{
						array = DBConnectionString.NewRestrictionAllowOnly(this._restrictionValues, entry._restrictionValues);
					}
					else
					{
						array = this._restrictionValues;
					}
				}
			}
			else if (KeyRestrictionBehavior.PreventUsage == this._behavior)
			{
				if (ADP.IsEmptyArray(this._restrictionValues))
				{
					array = entry._restrictionValues;
				}
				else if (ADP.IsEmptyArray(entry._restrictionValues))
				{
					array = this._restrictionValues;
				}
				else
				{
					array = DBConnectionString.NoDuplicateUnion(this._restrictionValues, entry._restrictionValues);
				}
			}
			else if (!ADP.IsEmptyArray(this._restrictionValues) && !ADP.IsEmptyArray(entry._restrictionValues))
			{
				if (this._restrictionValues.Length <= entry._restrictionValues.Length)
				{
					array = DBConnectionString.NewRestrictionIntersect(this._restrictionValues, entry._restrictionValues);
				}
				else
				{
					array = DBConnectionString.NewRestrictionIntersect(entry._restrictionValues, this._restrictionValues);
				}
			}
			return new DBConnectionString(this, array, keyRestrictionBehavior);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000BDA28 File Offset: 0x000BBC28
		[Conditional("DEBUG")]
		private void ValidateCombinedSet(DBConnectionString componentSet, DBConnectionString combinedSet)
		{
			if (componentSet != null && combinedSet._restrictionValues != null && componentSet._restrictionValues != null)
			{
				if (componentSet._behavior == KeyRestrictionBehavior.AllowOnly)
				{
					if (combinedSet._behavior != KeyRestrictionBehavior.AllowOnly)
					{
						KeyRestrictionBehavior behavior = combinedSet._behavior;
						return;
					}
				}
				else if (componentSet._behavior == KeyRestrictionBehavior.PreventUsage && combinedSet._behavior != KeyRestrictionBehavior.AllowOnly)
				{
					KeyRestrictionBehavior behavior2 = combinedSet._behavior;
				}
			}
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000BDA7C File Offset: 0x000BBC7C
		private bool IsRestrictedKeyword(string key)
		{
			return this._restrictionValues == null || 0 > Array.BinarySearch<string>(this._restrictionValues, key, StringComparer.Ordinal);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000BDA9C File Offset: 0x000BBC9C
		internal bool IsSupersetOf(DBConnectionString entry)
		{
			KeyRestrictionBehavior behavior = this._behavior;
			if (behavior != KeyRestrictionBehavior.AllowOnly)
			{
				if (behavior != KeyRestrictionBehavior.PreventUsage)
				{
					throw ADP.InvalidKeyRestrictionBehavior(this._behavior);
				}
				if (this._restrictionValues != null)
				{
					foreach (string text in this._restrictionValues)
					{
						if (entry.ContainsKey(text))
						{
							return false;
						}
					}
				}
			}
			else
			{
				for (NameValuePair nameValuePair = entry.KeyChain; nameValuePair != null; nameValuePair = nameValuePair.Next)
				{
					if (!this.ContainsKey(nameValuePair.Name) && this.IsRestrictedKeyword(nameValuePair.Name))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000BDB2C File Offset: 0x000BBD2C
		private static string[] NewRestrictionAllowOnly(string[] allowonly, string[] preventusage)
		{
			List<string> list = null;
			for (int i = 0; i < allowonly.Length; i++)
			{
				if (0 > Array.BinarySearch<string>(preventusage, allowonly[i], StringComparer.Ordinal))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(allowonly[i]);
				}
			}
			string[] array = null;
			if (list != null)
			{
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000BDB7C File Offset: 0x000BBD7C
		private static string[] NewRestrictionIntersect(string[] a, string[] b)
		{
			List<string> list = null;
			for (int i = 0; i < a.Length; i++)
			{
				if (0 <= Array.BinarySearch<string>(b, a[i], StringComparer.Ordinal))
				{
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(a[i]);
				}
			}
			string[] array = null;
			if (list != null)
			{
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000BDBCC File Offset: 0x000BBDCC
		private static string[] NoDuplicateUnion(string[] a, string[] b)
		{
			List<string> list = new List<string>(a.Length + b.Length);
			for (int i = 0; i < a.Length; i++)
			{
				list.Add(a[i]);
			}
			for (int j = 0; j < b.Length; j++)
			{
				if (0 > Array.BinarySearch<string>(a, b[j], StringComparer.Ordinal))
				{
					list.Add(b[j]);
				}
			}
			string[] array = list.ToArray();
			Array.Sort<string>(array, StringComparer.Ordinal);
			return array;
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000BDC38 File Offset: 0x000BBE38
		private static string[] ParseRestrictions(string restrictions, Dictionary<string, string> synonyms)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder(restrictions.Length);
			int i = 0;
			int length = restrictions.Length;
			while (i < length)
			{
				int num = i;
				string text;
				string text2;
				i = DbConnectionOptions.GetKeyValuePair(restrictions, num, stringBuilder, false, out text, out text2);
				if (!string.IsNullOrEmpty(text))
				{
					string text3 = ((synonyms != null) ? synonyms[text] : text);
					if (string.IsNullOrEmpty(text3))
					{
						throw ADP.KeywordNotSupported(text);
					}
					list.Add(text3);
				}
			}
			return DBConnectionString.RemoveDuplicates(list.ToArray());
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000BDCB8 File Offset: 0x000BBEB8
		internal static string[] RemoveDuplicates(string[] restrictions)
		{
			int num = restrictions.Length;
			if (0 < num)
			{
				Array.Sort<string>(restrictions, StringComparer.Ordinal);
				for (int i = 1; i < restrictions.Length; i++)
				{
					string text = restrictions[i - 1];
					if (text.Length == 0 || text == restrictions[i])
					{
						restrictions[i - 1] = null;
						num--;
					}
				}
				if (restrictions[restrictions.Length - 1].Length == 0)
				{
					restrictions[restrictions.Length - 1] = null;
					num--;
				}
				if (num != restrictions.Length)
				{
					string[] array = new string[num];
					num = 0;
					for (int j = 0; j < restrictions.Length; j++)
					{
						if (restrictions[j] != null)
						{
							array[num++] = restrictions[j];
						}
					}
					restrictions = array;
				}
			}
			return restrictions;
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000BDD5C File Offset: 0x000BBF5C
		[Conditional("DEBUG")]
		private static void Verify(string[] restrictionValues)
		{
			if (restrictionValues != null)
			{
				for (int i = 1; i < restrictionValues.Length; i++)
				{
				}
			}
		}

		// Token: 0x040019E3 RID: 6627
		private readonly string _encryptedUsersConnectionString;

		// Token: 0x040019E4 RID: 6628
		private readonly Dictionary<string, string> _parsetable;

		// Token: 0x040019E5 RID: 6629
		private readonly NameValuePair _keychain;

		// Token: 0x040019E6 RID: 6630
		private readonly bool _hasPassword;

		// Token: 0x040019E7 RID: 6631
		private readonly string[] _restrictionValues;

		// Token: 0x040019E8 RID: 6632
		private readonly string _restrictions;

		// Token: 0x040019E9 RID: 6633
		private readonly KeyRestrictionBehavior _behavior;

		// Token: 0x040019EA RID: 6634
		private readonly string _encryptedActualConnectionString;

		// Token: 0x02000376 RID: 886
		private static class KEY
		{
			// Token: 0x040019EB RID: 6635
			internal const string Password = "password";

			// Token: 0x040019EC RID: 6636
			internal const string PersistSecurityInfo = "persist security info";

			// Token: 0x040019ED RID: 6637
			internal const string Pwd = "pwd";
		}
	}
}

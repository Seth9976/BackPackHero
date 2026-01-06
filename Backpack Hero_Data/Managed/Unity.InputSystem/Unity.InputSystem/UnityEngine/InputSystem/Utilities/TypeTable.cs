using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000147 RID: 327
	internal struct TypeTable
	{
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00053E64 File Offset: 0x00052064
		public IEnumerable<string> names
		{
			get
			{
				return this.table.Keys.Select((InternedString x) => x.ToString());
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00053E95 File Offset: 0x00052095
		public IEnumerable<InternedString> internedNames
		{
			get
			{
				return this.table.Keys;
			}
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00053EA2 File Offset: 0x000520A2
		public void Initialize()
		{
			this.table = new Dictionary<InternedString, Type>();
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00053EB0 File Offset: 0x000520B0
		public InternedString FindNameForType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			foreach (KeyValuePair<InternedString, Type> keyValuePair in this.table)
			{
				if (keyValuePair.Value == type)
				{
					return keyValuePair.Key;
				}
			}
			return default(InternedString);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00053F34 File Offset: 0x00052134
		public void AddTypeRegistration(string name, Type type)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name cannot be null or empty", "name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			InternedString internedString = new InternedString(name);
			this.table[internedString] = type;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00053F84 File Offset: 0x00052184
		public Type LookupTypeRegistration(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (this.table == null)
			{
				throw new InvalidOperationException("Input System not yet initialized");
			}
			InternedString internedString = new InternedString(name);
			Type type;
			if (this.table.TryGetValue(internedString, out type))
			{
				return type;
			}
			return null;
		}

		// Token: 0x040006EC RID: 1772
		public Dictionary<InternedString, Type> table;
	}
}

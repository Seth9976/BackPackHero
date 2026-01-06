using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000147 RID: 327
	internal struct TypeTable
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00053C50 File Offset: 0x00051E50
		public IEnumerable<string> names
		{
			get
			{
				return this.table.Keys.Select((InternedString x) => x.ToString());
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00053C81 File Offset: 0x00051E81
		public IEnumerable<InternedString> internedNames
		{
			get
			{
				return this.table.Keys;
			}
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00053C8E File Offset: 0x00051E8E
		public void Initialize()
		{
			this.table = new Dictionary<InternedString, Type>();
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00053C9C File Offset: 0x00051E9C
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

		// Token: 0x060011C1 RID: 4545 RVA: 0x00053D20 File Offset: 0x00051F20
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

		// Token: 0x060011C2 RID: 4546 RVA: 0x00053D70 File Offset: 0x00051F70
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

		// Token: 0x040006EB RID: 1771
		public Dictionary<InternedString, Type> table;
	}
}

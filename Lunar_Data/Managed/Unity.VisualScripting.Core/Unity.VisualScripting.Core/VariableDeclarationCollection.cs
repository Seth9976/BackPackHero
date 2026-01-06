using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Unity.VisualScripting
{
	// Token: 0x0200016E RID: 366
	[SerializationVersion("A", new Type[] { })]
	public sealed class VariableDeclarationCollection : KeyedCollection<string, VariableDeclaration>, IKeyedCollection<string, VariableDeclaration>, ICollection<VariableDeclaration>, IEnumerable<VariableDeclaration>, IEnumerable
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x00029534 File Offset: 0x00027734
		protected override string GetKeyForItem(VariableDeclaration item)
		{
			return item.name;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002953C File Offset: 0x0002773C
		public void EditorRename(VariableDeclaration item, string newName)
		{
			base.ChangeItemKey(item, newName);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00029546 File Offset: 0x00027746
		public new bool TryGetValue(string key, out VariableDeclaration value)
		{
			if (base.Dictionary == null)
			{
				value = null;
				return false;
			}
			return base.Dictionary.TryGetValue(key, out value);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002956A File Offset: 0x0002776A
		VariableDeclaration IKeyedCollection<string, VariableDeclaration>.get_Item(string key)
		{
			return base[key];
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00029573 File Offset: 0x00027773
		bool IKeyedCollection<string, VariableDeclaration>.Contains(string key)
		{
			return base.Contains(key);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002957C File Offset: 0x0002777C
		bool IKeyedCollection<string, VariableDeclaration>.Remove(string key)
		{
			return base.Remove(key);
		}
	}
}

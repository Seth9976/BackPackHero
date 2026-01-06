using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class Lore : MonoBehaviour
{
	// Token: 0x0600022D RID: 557 RVA: 0x0000DA84 File Offset: 0x0000BC84
	public Lore.LoreType GetLoreType(string name)
	{
		foreach (Lore.LoreType loreType in this.loreTypes)
		{
			if (loreType.nameKey.Trim().ToLower() == name.Trim().ToLower())
			{
				loreType.descriptionKeys.Clear();
				for (int i = 0; i < 10; i++)
				{
					string text = name + i.ToString();
					if (LangaugeManager.main.KeyExists(text))
					{
						loreType.descriptionKeys.Insert(0, text);
					}
				}
				return loreType;
			}
		}
		return null;
	}

	// Token: 0x04000172 RID: 370
	[SerializeField]
	public List<Lore.LoreType> loreTypes = new List<Lore.LoreType>();

	// Token: 0x02000280 RID: 640
	[Serializable]
	public class LoreType
	{
		// Token: 0x04000F69 RID: 3945
		public Sprite sprite;

		// Token: 0x04000F6A RID: 3946
		public string nameKey;

		// Token: 0x04000F6B RID: 3947
		[NonSerialized]
		public List<string> descriptionKeys = new List<string>();
	}
}

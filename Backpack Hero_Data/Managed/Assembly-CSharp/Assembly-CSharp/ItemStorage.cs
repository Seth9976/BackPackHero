using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class ItemStorage : MonoBehaviour
{
	// Token: 0x0600020B RID: 523 RVA: 0x0000CC60 File Offset: 0x0000AE60
	public void AddStoredItems(string[] itemsToAdd)
	{
		foreach (string text in itemsToAdd)
		{
			if (text != null && !(text == ""))
			{
				this.storedItems.Add(text);
			}
		}
	}

	// Token: 0x0400015C RID: 348
	[SerializeField]
	public List<string> storedItems = new List<string>();
}

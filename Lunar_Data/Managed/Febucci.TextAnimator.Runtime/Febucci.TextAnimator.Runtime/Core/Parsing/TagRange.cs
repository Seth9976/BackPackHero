using System;
using System.Text;
using UnityEngine;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x02000051 RID: 81
	public struct TagRange
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00007A9B File Offset: 0x00005C9B
		public TagRange(Vector2Int indexes, params ModifierInfo[] modifiers)
		{
			this.indexes = indexes;
			this.modifiers = modifiers;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007AAC File Offset: 0x00005CAC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("indexes: ");
			stringBuilder.Append(this.indexes);
			if (this.modifiers == null || this.modifiers.Length == 0)
			{
				stringBuilder.Append("\n no modifiers");
			}
			else
			{
				for (int i = 0; i < this.modifiers.Length; i++)
				{
					stringBuilder.Append('\n');
					stringBuilder.Append('-');
					stringBuilder.Append(this.modifiers[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000120 RID: 288
		public Vector2Int indexes;

		// Token: 0x04000121 RID: 289
		public ModifierInfo[] modifiers;
	}
}

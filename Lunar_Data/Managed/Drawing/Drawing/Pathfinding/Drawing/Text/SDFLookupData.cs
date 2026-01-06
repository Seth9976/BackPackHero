using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Pathfinding.Drawing.Text
{
	// Token: 0x0200005A RID: 90
	internal struct SDFLookupData
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000F494 File Offset: 0x0000D694
		public SDFLookupData(SDFFont font)
		{
			int num = 0;
			SDFCharacter sdfcharacter = font.characters[0];
			for (int i = 0; i < font.characters.Length; i++)
			{
				if (font.characters[i].codePoint == '?')
				{
					sdfcharacter = font.characters[i];
				}
				if (font.characters[i].codePoint >= '\u0080')
				{
					num++;
				}
			}
			this.characters = new NativeArray<SDFCharacter>(128 + num, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			for (int j = 0; j < this.characters.Length; j++)
			{
				this.characters[j] = sdfcharacter;
			}
			this.lookup = new Dictionary<char, int>();
			this.material = font.material;
			num = 0;
			for (int k = 0; k < font.characters.Length; k++)
			{
				SDFCharacter sdfcharacter2 = font.characters[k];
				int num2 = (int)sdfcharacter2.codePoint;
				if (sdfcharacter2.codePoint >= '\u0080')
				{
					num2 = 128 + num;
					num++;
				}
				this.characters[num2] = sdfcharacter2;
				this.lookup[sdfcharacter2.codePoint] = num2;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		public int GetIndex(char c)
		{
			int num;
			if (this.lookup.TryGetValue(c, out num))
			{
				return num;
			}
			if (c == '\n')
			{
				return 65535;
			}
			return this.lookup['?'];
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000F5F7 File Offset: 0x0000D7F7
		public void Dispose()
		{
			if (this.characters.IsCreated)
			{
				this.characters.Dispose();
			}
		}

		// Token: 0x04000175 RID: 373
		public NativeArray<SDFCharacter> characters;

		// Token: 0x04000176 RID: 374
		private Dictionary<char, int> lookup;

		// Token: 0x04000177 RID: 375
		public Material material;

		// Token: 0x04000178 RID: 376
		public const ushort Newline = 65535;
	}
}

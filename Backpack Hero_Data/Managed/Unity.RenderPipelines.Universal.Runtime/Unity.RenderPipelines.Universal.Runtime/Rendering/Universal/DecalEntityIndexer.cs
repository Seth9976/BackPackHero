using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000069 RID: 105
	internal class DecalEntityIndexer
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x00015DE1 File Offset: 0x00013FE1
		public bool IsValid(DecalEntity decalEntity)
		{
			return this.m_Entities.Count > decalEntity.index && this.m_Entities[decalEntity.index].version == decalEntity.version;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00015E18 File Offset: 0x00014018
		public DecalEntity CreateDecalEntity(int arrayIndex, int chunkIndex)
		{
			if (this.m_FreeIndices.Count != 0)
			{
				int num = this.m_FreeIndices.Dequeue();
				int num2 = this.m_Entities[num].version + 1;
				this.m_Entities[num] = new DecalEntityIndexer.DecalEntityItem
				{
					arrayIndex = arrayIndex,
					chunkIndex = chunkIndex,
					version = num2
				};
				return new DecalEntity
				{
					index = num,
					version = num2
				};
			}
			int count = this.m_Entities.Count;
			int num3 = 1;
			this.m_Entities.Add(new DecalEntityIndexer.DecalEntityItem
			{
				arrayIndex = arrayIndex,
				chunkIndex = chunkIndex,
				version = num3
			});
			return new DecalEntity
			{
				index = count,
				version = num3
			};
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00015EF4 File Offset: 0x000140F4
		public void DestroyDecalEntity(DecalEntity decalEntity)
		{
			this.m_FreeIndices.Enqueue(decalEntity.index);
			DecalEntityIndexer.DecalEntityItem decalEntityItem = this.m_Entities[decalEntity.index];
			decalEntityItem.version++;
			this.m_Entities[decalEntity.index] = decalEntityItem;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00015F42 File Offset: 0x00014142
		public DecalEntityIndexer.DecalEntityItem GetItem(DecalEntity decalEntity)
		{
			return this.m_Entities[decalEntity.index];
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00015F58 File Offset: 0x00014158
		public void UpdateIndex(DecalEntity decalEntity, int newArrayIndex)
		{
			DecalEntityIndexer.DecalEntityItem decalEntityItem = this.m_Entities[decalEntity.index];
			decalEntityItem.arrayIndex = newArrayIndex;
			decalEntityItem.version = decalEntity.version;
			this.m_Entities[decalEntity.index] = decalEntityItem;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00015FA0 File Offset: 0x000141A0
		public void RemapChunkIndices(List<int> remaper)
		{
			for (int i = 0; i < this.m_Entities.Count; i++)
			{
				int num = remaper[this.m_Entities[i].chunkIndex];
				DecalEntityIndexer.DecalEntityItem decalEntityItem = this.m_Entities[i];
				decalEntityItem.chunkIndex = num;
				this.m_Entities[i] = decalEntityItem;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00015FFD File Offset: 0x000141FD
		public void Clear()
		{
			this.m_Entities.Clear();
			this.m_FreeIndices.Clear();
		}

		// Token: 0x040002AF RID: 687
		private List<DecalEntityIndexer.DecalEntityItem> m_Entities = new List<DecalEntityIndexer.DecalEntityItem>();

		// Token: 0x040002B0 RID: 688
		private Queue<int> m_FreeIndices = new Queue<int>();

		// Token: 0x02000167 RID: 359
		public struct DecalEntityItem
		{
			// Token: 0x0400093B RID: 2363
			public int chunkIndex;

			// Token: 0x0400093C RID: 2364
			public int arrayIndex;

			// Token: 0x0400093D RID: 2365
			public int version;
		}
	}
}

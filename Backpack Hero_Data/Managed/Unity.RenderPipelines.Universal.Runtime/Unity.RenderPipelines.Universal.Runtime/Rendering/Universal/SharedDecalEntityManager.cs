using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000AE RID: 174
	internal class SharedDecalEntityManager : IDisposable
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0001E014 File Offset: 0x0001C214
		public DecalEntityManager Get()
		{
			if (this.m_DecalEntityManager == null)
			{
				this.m_DecalEntityManager = new DecalEntityManager();
				foreach (DecalProjector decalProjector in Object.FindObjectsOfType<DecalProjector>())
				{
					if (decalProjector.isActiveAndEnabled && !this.m_DecalEntityManager.IsValid(decalProjector.decalEntity))
					{
						decalProjector.decalEntity = this.m_DecalEntityManager.CreateDecalEntity(decalProjector);
					}
				}
				DecalProjector.onDecalAdd += this.OnDecalAdd;
				DecalProjector.onDecalRemove += this.OnDecalRemove;
				DecalProjector.onDecalPropertyChange += this.OnDecalPropertyChange;
				DecalProjector.onDecalMaterialChange += this.OnDecalMaterialChange;
			}
			this.m_ReferenceCounter++;
			return this.m_DecalEntityManager;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001E0D4 File Offset: 0x0001C2D4
		public void Release(DecalEntityManager decalEntityManager)
		{
			if (this.m_ReferenceCounter == 0)
			{
				return;
			}
			this.m_ReferenceCounter--;
			if (this.m_ReferenceCounter == 0)
			{
				this.Dispose();
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001E0FC File Offset: 0x0001C2FC
		public void Dispose()
		{
			this.m_DecalEntityManager.Dispose();
			this.m_DecalEntityManager = null;
			this.m_ReferenceCounter = 0;
			DecalProjector.onDecalAdd -= this.OnDecalAdd;
			DecalProjector.onDecalRemove -= this.OnDecalRemove;
			DecalProjector.onDecalPropertyChange -= this.OnDecalPropertyChange;
			DecalProjector.onDecalMaterialChange -= this.OnDecalMaterialChange;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001E166 File Offset: 0x0001C366
		private void OnDecalAdd(DecalProjector decalProjector)
		{
			if (!this.m_DecalEntityManager.IsValid(decalProjector.decalEntity))
			{
				decalProjector.decalEntity = this.m_DecalEntityManager.CreateDecalEntity(decalProjector);
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001E18D File Offset: 0x0001C38D
		private void OnDecalRemove(DecalProjector decalProjector)
		{
			this.m_DecalEntityManager.DestroyDecalEntity(decalProjector.decalEntity);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		private void OnDecalPropertyChange(DecalProjector decalProjector)
		{
			if (this.m_DecalEntityManager.IsValid(decalProjector.decalEntity))
			{
				this.m_DecalEntityManager.UpdateDecalEntityData(decalProjector.decalEntity, decalProjector);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001E1C7 File Offset: 0x0001C3C7
		private void OnDecalMaterialChange(DecalProjector decalProjector)
		{
			this.OnDecalRemove(decalProjector);
			this.OnDecalAdd(decalProjector);
		}

		// Token: 0x04000414 RID: 1044
		private DecalEntityManager m_DecalEntityManager;

		// Token: 0x04000415 RID: 1045
		private int m_ReferenceCounter;
	}
}

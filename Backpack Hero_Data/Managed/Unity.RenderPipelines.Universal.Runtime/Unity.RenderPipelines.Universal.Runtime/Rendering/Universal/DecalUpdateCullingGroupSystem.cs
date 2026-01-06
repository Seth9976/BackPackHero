using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000072 RID: 114
	internal class DecalUpdateCullingGroupSystem
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000175CC File Offset: 0x000157CC
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x000175D6 File Offset: 0x000157D6
		public float boundingDistance
		{
			get
			{
				return this.m_BoundingDistance[0];
			}
			set
			{
				this.m_BoundingDistance[0] = value;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000175E1 File Offset: 0x000157E1
		public DecalUpdateCullingGroupSystem(DecalEntityManager entityManager, float drawDistance)
		{
			this.m_EntityManager = entityManager;
			this.m_BoundingDistance[0] = drawDistance;
			this.m_Sampler = new ProfilingSampler("DecalUpdateCullingGroupsSystem.Execute");
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00017618 File Offset: 0x00015818
		public void Execute(Camera camera)
		{
			using (new ProfilingScope(null, this.m_Sampler))
			{
				this.m_Camera = camera;
				for (int i = 0; i < this.m_EntityManager.chunkCount; i++)
				{
					this.Execute(this.m_EntityManager.cachedChunks[i], this.m_EntityManager.culledChunks[i], this.m_EntityManager.culledChunks[i].count);
				}
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000176B0 File Offset: 0x000158B0
		public void Execute(DecalCachedChunk cachedChunk, DecalCulledChunk culledChunk, int count)
		{
			cachedChunk.currentJobHandle.Complete();
			CullingGroup cullingGroups = culledChunk.cullingGroups;
			cullingGroups.targetCamera = this.m_Camera;
			cullingGroups.SetDistanceReferencePoint(this.m_Camera.transform.position);
			cullingGroups.SetBoundingDistances(this.m_BoundingDistance);
			cachedChunk.boundingSpheres.CopyTo(cachedChunk.boundingSphereArray);
			cullingGroups.SetBoundingSpheres(cachedChunk.boundingSphereArray);
			cullingGroups.SetBoundingSphereCount(count);
			culledChunk.cameraPosition = this.m_Camera.transform.position;
			culledChunk.cullingMask = this.m_Camera.cullingMask;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00017749 File Offset: 0x00015949
		internal static ulong GetSceneCullingMaskFromCamera(Camera camera)
		{
			return 0UL;
		}

		// Token: 0x040002EB RID: 747
		private float[] m_BoundingDistance = new float[1];

		// Token: 0x040002EC RID: 748
		private Camera m_Camera;

		// Token: 0x040002ED RID: 749
		private DecalEntityManager m_EntityManager;

		// Token: 0x040002EE RID: 750
		private ProfilingSampler m_Sampler;
	}
}

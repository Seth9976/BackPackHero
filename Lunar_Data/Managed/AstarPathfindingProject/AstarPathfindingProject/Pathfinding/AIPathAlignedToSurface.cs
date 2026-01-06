using System;
using System.Collections.Generic;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

namespace Pathfinding
{
	// Token: 0x0200000F RID: 15
	[HelpURL("https://arongranberg.com/astar/documentation/stable/aipathalignedtosurface.html")]
	public class AIPathAlignedToSurface : AIPath
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003E07 File Offset: 0x00002007
		protected override void OnEnable()
		{
			base.OnEnable();
			this.movementPlane = new SimpleMovementPlane(this.rotation);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003E20 File Offset: 0x00002020
		protected override void ApplyGravity(float deltaTime)
		{
			if (base.usingGravity)
			{
				this.verticalVelocity += deltaTime * (float.IsNaN(this.gravity.x) ? Physics.gravity.y : this.gravity.y);
				return;
			}
			this.verticalVelocity = 0f;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003E7C File Offset: 0x0000207C
		public unsafe static void UpdateMovementPlanes(AIPathAlignedToSurface[] components, int count)
		{
			List<Mesh> list = ListPool<Mesh>.Claim();
			List<List<AIPathAlignedToSurface>> list2 = new List<List<AIPathAlignedToSurface>>();
			Dictionary<Mesh, int> dictionary = AIPathAlignedToSurface.scratchDictionary;
			for (int i = 0; i < count; i++)
			{
				MeshCollider meshCollider = components[i].lastRaycastHit.collider as MeshCollider;
				if (meshCollider != null && components[i].lastRaycastHit.triangleIndex != -1)
				{
					Mesh sharedMesh = meshCollider.sharedMesh;
					int num;
					if (dictionary.TryGetValue(sharedMesh, out num))
					{
						list2[num].Add(components[i]);
					}
					else if (sharedMesh != null && sharedMesh.isReadable)
					{
						dictionary[sharedMesh] = list.Count;
						list.Add(sharedMesh);
						list2.Add(ListPool<AIPathAlignedToSurface>.Claim());
						list2[list.Count - 1].Add(components[i]);
					}
					else
					{
						components[i].SetInterpolatedNormal(components[i].lastRaycastHit.normal);
					}
				}
				else
				{
					components[i].SetInterpolatedNormal(components[i].lastRaycastHit.normal);
				}
			}
			Mesh.MeshDataArray meshDataArray = Mesh.AcquireReadOnlyMeshData(list);
			for (int j = 0; j < list.Count; j++)
			{
				Mesh mesh = list[j];
				int num2 = dictionary[mesh];
				Mesh.MeshData meshData = meshDataArray[num2];
				List<AIPathAlignedToSurface> list3 = list2[num2];
				int vertexAttributeStream = meshData.GetVertexAttributeStream(VertexAttribute.Normal);
				if (vertexAttributeStream == -1)
				{
					for (int k = 0; k < list3.Count; k++)
					{
						list3[k].SetInterpolatedNormal(list3[k].lastRaycastHit.normal);
					}
				}
				else
				{
					NativeArray<byte> vertexData = meshData.GetVertexData<byte>(vertexAttributeStream);
					int vertexBufferStride = meshData.GetVertexBufferStride(vertexAttributeStream);
					int vertexAttributeOffset = meshData.GetVertexAttributeOffset(VertexAttribute.Normal);
					byte* ptr = (byte*)vertexData.GetUnsafeReadOnlyPtr<byte>() + vertexAttributeOffset;
					for (int l = 0; l < list3.Count; l++)
					{
						AIPathAlignedToSurface aipathAlignedToSurface = list3[l];
						RaycastHit lastRaycastHit = aipathAlignedToSurface.lastRaycastHit;
						int num3;
						int num4;
						int num5;
						if (meshData.indexFormat == IndexFormat.UInt16)
						{
							NativeArray<ushort> indexData = meshData.GetIndexData<ushort>();
							num3 = (int)indexData[lastRaycastHit.triangleIndex * 3];
							num4 = (int)indexData[lastRaycastHit.triangleIndex * 3 + 1];
							num5 = (int)indexData[lastRaycastHit.triangleIndex * 3 + 2];
						}
						else
						{
							NativeArray<int> indexData2 = meshData.GetIndexData<int>();
							num3 = indexData2[lastRaycastHit.triangleIndex * 3];
							num4 = indexData2[lastRaycastHit.triangleIndex * 3 + 1];
							num5 = indexData2[lastRaycastHit.triangleIndex * 3 + 2];
						}
						Vector3 vector = *(Vector3*)(ptr + num3 * vertexBufferStride);
						Vector3 vector2 = *(Vector3*)(ptr + num4 * vertexBufferStride);
						Vector3 vector3 = *(Vector3*)(ptr + num5 * vertexBufferStride);
						Vector3 barycentricCoordinate = lastRaycastHit.barycentricCoordinate;
						Vector3 vector4 = (vector * barycentricCoordinate.x + vector2 * barycentricCoordinate.y + vector3 * barycentricCoordinate.z).normalized;
						vector4 = lastRaycastHit.collider.transform.TransformDirection(vector4);
						aipathAlignedToSurface.SetInterpolatedNormal(vector4);
					}
				}
			}
			meshDataArray.Dispose();
			for (int m = 0; m < list2.Count; m++)
			{
				ListPool<AIPathAlignedToSurface>.Release(list2[m]);
			}
			ListPool<Mesh>.Release(ref list);
			AIPathAlignedToSurface.scratchDictionary.Clear();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000041D0 File Offset: 0x000023D0
		private void SetInterpolatedNormal(Vector3 normal)
		{
			if (normal != Vector3.zero)
			{
				Vector3 vector = Vector3.Cross(this.movementPlane.rotation * Vector3.right, normal);
				this.movementPlane = new SimpleMovementPlane(Quaternion.LookRotation(vector, normal));
			}
			if (this.rvoController != null)
			{
				this.rvoController.movementPlane = this.movementPlane;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000033F6 File Offset: 0x000015F6
		protected override void UpdateMovementPlane()
		{
		}

		// Token: 0x0400006A RID: 106
		private static readonly Dictionary<Mesh, int> scratchDictionary = new Dictionary<Mesh, int>();
	}
}

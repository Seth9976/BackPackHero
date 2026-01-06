using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200002B RID: 43
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[RequiredByNativeCode]
	public struct MeshGenerationResult : IEquatable<MeshGenerationResult>
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000047C3 File Offset: 0x000029C3
		public readonly MeshId MeshId { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000047CB File Offset: 0x000029CB
		public readonly Mesh Mesh { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000047D3 File Offset: 0x000029D3
		public readonly MeshCollider MeshCollider { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000047DB File Offset: 0x000029DB
		public readonly MeshGenerationStatus Status { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000047E3 File Offset: 0x000029E3
		public readonly MeshVertexAttributes Attributes { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000047EB File Offset: 0x000029EB
		public readonly ulong Timestamp { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000047F3 File Offset: 0x000029F3
		public readonly Vector3 Position { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000047FB File Offset: 0x000029FB
		public readonly Quaternion Rotation { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00004803 File Offset: 0x00002A03
		public readonly Vector3 Scale { get; }

		// Token: 0x0600014A RID: 330 RVA: 0x0000480C File Offset: 0x00002A0C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is MeshGenerationResult);
			return !flag && this.Equals((MeshGenerationResult)obj);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004840 File Offset: 0x00002A40
		public bool Equals(MeshGenerationResult other)
		{
			return this.MeshId.Equals(other.MeshId) && this.Mesh.Equals(other.Mesh) && this.MeshCollider.Equals(other.MeshCollider) && this.Status == other.Status && this.Attributes == other.Attributes && this.Position.Equals(other.Position) && this.Rotation.Equals(other.Rotation) && this.Scale.Equals(other.Scale);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000048F8 File Offset: 0x00002AF8
		public static bool operator ==(MeshGenerationResult lhs, MeshGenerationResult rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004914 File Offset: 0x00002B14
		public static bool operator !=(MeshGenerationResult lhs, MeshGenerationResult rhs)
		{
			return !lhs.Equals(rhs);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004934 File Offset: 0x00002B34
		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(this.MeshId.GetHashCode(), this.Mesh.GetHashCode(), this.MeshCollider.GetHashCode(), ((int)this.Status).GetHashCode(), ((int)this.Attributes).GetHashCode(), this.Position.GetHashCode(), this.Rotation.GetHashCode(), this.Scale.GetHashCode());
		}
	}
}

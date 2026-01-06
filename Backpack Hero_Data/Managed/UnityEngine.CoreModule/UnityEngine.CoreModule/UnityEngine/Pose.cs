using System;

namespace UnityEngine
{
	// Token: 0x02000255 RID: 597
	[Serializable]
	public struct Pose : IEquatable<Pose>
	{
		// Token: 0x060019E2 RID: 6626 RVA: 0x00029C48 File Offset: 0x00027E48
		public Pose(Vector3 position, Quaternion rotation)
		{
			this.position = position;
			this.rotation = rotation;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00029C5C File Offset: 0x00027E5C
		public override string ToString()
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.position.ToString(),
				this.rotation.ToString()
			});
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00029CA8 File Offset: 0x00027EA8
		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.position.ToString(format),
				this.rotation.ToString(format)
			});
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00029CE8 File Offset: 0x00027EE8
		public Pose GetTransformedBy(Pose lhs)
		{
			return new Pose
			{
				position = lhs.position + lhs.rotation * this.position,
				rotation = lhs.rotation * this.rotation
			};
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00029D40 File Offset: 0x00027F40
		public Pose GetTransformedBy(Transform lhs)
		{
			return new Pose
			{
				position = lhs.TransformPoint(this.position),
				rotation = lhs.rotation * this.rotation
			};
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x00029D88 File Offset: 0x00027F88
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00029DAC File Offset: 0x00027FAC
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00029DD0 File Offset: 0x00027FD0
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00029DF4 File Offset: 0x00027FF4
		public static Pose identity
		{
			get
			{
				return Pose.k_Identity;
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00029E0C File Offset: 0x0002800C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Pose);
			return !flag && this.Equals((Pose)obj);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00029E40 File Offset: 0x00028040
		public bool Equals(Pose other)
		{
			return this.position == other.position && this.rotation == other.rotation;
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00029E7C File Offset: 0x0002807C
		public override int GetHashCode()
		{
			return this.position.GetHashCode() ^ (this.rotation.GetHashCode() << 1);
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00029EB4 File Offset: 0x000280B4
		public static bool operator ==(Pose a, Pose b)
		{
			return a.Equals(b);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00029ED0 File Offset: 0x000280D0
		public static bool operator !=(Pose a, Pose b)
		{
			return !(a == b);
		}

		// Token: 0x0400088C RID: 2188
		public Vector3 position;

		// Token: 0x0400088D RID: 2189
		public Quaternion rotation;

		// Token: 0x0400088E RID: 2190
		private static readonly Pose k_Identity = new Pose(Vector3.zero, Quaternion.identity);
	}
}

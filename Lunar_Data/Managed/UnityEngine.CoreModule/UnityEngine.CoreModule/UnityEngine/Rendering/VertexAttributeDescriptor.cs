using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003BE RID: 958
	[UsedByNativeCode]
	public struct VertexAttributeDescriptor : IEquatable<VertexAttributeDescriptor>
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x000330A4 File Offset: 0x000312A4
		// (set) Token: 0x06001F5A RID: 8026 RVA: 0x000330AC File Offset: 0x000312AC
		public VertexAttribute attribute { readonly get; set; }

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x000330B5 File Offset: 0x000312B5
		// (set) Token: 0x06001F5C RID: 8028 RVA: 0x000330BD File Offset: 0x000312BD
		public VertexAttributeFormat format { readonly get; set; }

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001F5D RID: 8029 RVA: 0x000330C6 File Offset: 0x000312C6
		// (set) Token: 0x06001F5E RID: 8030 RVA: 0x000330CE File Offset: 0x000312CE
		public int dimension { readonly get; set; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x000330D7 File Offset: 0x000312D7
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x000330DF File Offset: 0x000312DF
		public int stream { readonly get; set; }

		// Token: 0x06001F61 RID: 8033 RVA: 0x000330E8 File Offset: 0x000312E8
		public VertexAttributeDescriptor(VertexAttribute attribute = VertexAttribute.Position, VertexAttributeFormat format = VertexAttributeFormat.Float32, int dimension = 3, int stream = 0)
		{
			this.attribute = attribute;
			this.format = format;
			this.dimension = dimension;
			this.stream = stream;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0003310C File Offset: 0x0003130C
		public override string ToString()
		{
			return string.Format("(attr={0} fmt={1} dim={2} stream={3})", new object[] { this.attribute, this.format, this.dimension, this.stream });
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x00033168 File Offset: 0x00031368
		public override int GetHashCode()
		{
			int num = 17;
			num = (int)(num * 23 + this.attribute);
			num = (int)(num * 23 + this.format);
			num = num * 23 + this.dimension;
			return num * 23 + this.stream;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x000331B0 File Offset: 0x000313B0
		public override bool Equals(object other)
		{
			bool flag = !(other is VertexAttributeDescriptor);
			return !flag && this.Equals((VertexAttributeDescriptor)other);
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000331E4 File Offset: 0x000313E4
		public bool Equals(VertexAttributeDescriptor other)
		{
			return this.attribute == other.attribute && this.format == other.format && this.dimension == other.dimension && this.stream == other.stream;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x00033238 File Offset: 0x00031438
		public static bool operator ==(VertexAttributeDescriptor lhs, VertexAttributeDescriptor rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00033254 File Offset: 0x00031454
		public static bool operator !=(VertexAttributeDescriptor lhs, VertexAttributeDescriptor rhs)
		{
			return !lhs.Equals(rhs);
		}
	}
}

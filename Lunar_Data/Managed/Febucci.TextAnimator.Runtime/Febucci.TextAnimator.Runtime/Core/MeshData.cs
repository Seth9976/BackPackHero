using System;
using System.Text;
using UnityEngine;

namespace Febucci.UI.Core
{
	// Token: 0x0200003F RID: 63
	public struct MeshData : IEquatable<MeshData>
	{
		// Token: 0x06000168 RID: 360 RVA: 0x00006D88 File Offset: 0x00004F88
		public bool Equals(MeshData other)
		{
			for (int i = 0; i < this.positions.Length; i++)
			{
				if (this.positions[i] != other.positions[i])
				{
					return false;
				}
			}
			for (int j = 0; j < this.colors.Length; j++)
			{
				if (!this.colors[j].Equals(other.colors[j]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006E0C File Offset: 0x0000500C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.positions.Length; i++)
			{
				stringBuilder.Append(this.positions[i].ToString());
				stringBuilder.Append(" ");
				stringBuilder.Append(this.colors[i].ToString());
				stringBuilder.Append(" - ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000F7 RID: 247
		public Vector3[] positions;

		// Token: 0x040000F8 RID: 248
		public Color32[] colors;
	}
}

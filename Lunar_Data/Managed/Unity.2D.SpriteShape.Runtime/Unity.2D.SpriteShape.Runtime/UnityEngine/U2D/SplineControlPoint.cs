using System;

namespace UnityEngine.U2D
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class SplineControlPoint
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000071AF File Offset: 0x000053AF
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000071B7 File Offset: 0x000053B7
		public Corner cornerMode
		{
			get
			{
				return this.m_CornerMode;
			}
			set
			{
				this.m_CornerMode = value;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000071C0 File Offset: 0x000053C0
		public override int GetHashCode()
		{
			int num = ((int)this.position.x).GetHashCode() ^ ((int)this.position.y).GetHashCode() ^ this.position.GetHashCode() ^ (this.leftTangent.GetHashCode() << 2) ^ (this.rightTangent.GetHashCode() >> 2);
			int num2 = (int)this.mode;
			return num ^ num2.GetHashCode() ^ this.height.GetHashCode() ^ this.spriteIndex.GetHashCode() ^ this.corner.GetHashCode() ^ (this.m_CornerMode.GetHashCode() << 2);
		}

		// Token: 0x04000070 RID: 112
		public Vector3 position;

		// Token: 0x04000071 RID: 113
		public Vector3 leftTangent;

		// Token: 0x04000072 RID: 114
		public Vector3 rightTangent;

		// Token: 0x04000073 RID: 115
		public ShapeTangentMode mode;

		// Token: 0x04000074 RID: 116
		public float height = 1f;

		// Token: 0x04000075 RID: 117
		public float bevelCutoff;

		// Token: 0x04000076 RID: 118
		public float bevelSize;

		// Token: 0x04000077 RID: 119
		public int spriteIndex;

		// Token: 0x04000078 RID: 120
		public bool corner;

		// Token: 0x04000079 RID: 121
		[SerializeField]
		private Corner m_CornerMode;
	}
}

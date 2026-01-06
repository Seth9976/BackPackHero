using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000135 RID: 309
	[SerializationVersion("A", new Type[] { })]
	[Serializable]
	public struct SerializableType : IEquatable<SerializableType>, IComparable<SerializableType>
	{
		// Token: 0x0600086B RID: 2155 RVA: 0x00025874 File Offset: 0x00023A74
		public SerializableType(string identification)
		{
			this.Identification = identification;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0002587D File Offset: 0x00023A7D
		public bool Equals(SerializableType other)
		{
			return string.Equals(this.Identification, other.Identification);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00025890 File Offset: 0x00023A90
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is SerializableType)
			{
				SerializableType serializableType = (SerializableType)obj;
				return this.Equals(serializableType);
			}
			return false;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000258BA File Offset: 0x00023ABA
		public override int GetHashCode()
		{
			string identification = this.Identification;
			if (identification == null)
			{
				return 0;
			}
			return identification.GetHashCode();
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000258CD File Offset: 0x00023ACD
		public static bool operator ==(SerializableType left, SerializableType right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000258D7 File Offset: 0x00023AD7
		public static bool operator !=(SerializableType left, SerializableType right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x000258E4 File Offset: 0x00023AE4
		public int CompareTo(SerializableType other)
		{
			return string.Compare(this.Identification, other.Identification, StringComparison.Ordinal);
		}

		// Token: 0x04000203 RID: 515
		[Serialize]
		public string Identification;
	}
}

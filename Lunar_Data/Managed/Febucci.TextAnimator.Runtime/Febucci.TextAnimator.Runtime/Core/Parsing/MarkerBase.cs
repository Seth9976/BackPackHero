using System;
using System.Text;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x0200004B RID: 75
	public abstract class MarkerBase : IComparable<MarkerBase>
	{
		// Token: 0x06000187 RID: 391 RVA: 0x000073D2 File Offset: 0x000055D2
		public MarkerBase(string name, int index, int internalOrder, string[] parameters)
		{
			this.name = name;
			this.index = index;
			this.internalOrder = internalOrder;
			this.parameters = parameters;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000073F7 File Offset: 0x000055F7
		public int CompareTo(MarkerBase other)
		{
			return this.internalOrder.CompareTo(other.internalOrder);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000740C File Offset: 0x0000560C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.name);
			stringBuilder.Append(" internal order:");
			stringBuilder.Append(this.internalOrder);
			stringBuilder.Append(" index:");
			stringBuilder.Append(this.index);
			stringBuilder.Append('\n');
			for (int i = 0; i < this.parameters.Length; i++)
			{
				stringBuilder.Append(this.parameters[i]);
				stringBuilder.Append('\n');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400010D RID: 269
		public readonly string name;

		// Token: 0x0400010E RID: 270
		public readonly int index;

		// Token: 0x0400010F RID: 271
		internal readonly int internalOrder;

		// Token: 0x04000110 RID: 272
		public string[] parameters;
	}
}

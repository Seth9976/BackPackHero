using System;

namespace TwitchLib.Client.Enums
{
	// Token: 0x02000008 RID: 8
	public abstract class StringEnum
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public string Value { get; }

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		protected StringEnum(string value)
		{
			this.Value = value;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public override string ToString()
		{
			return this.Value;
		}
	}
}

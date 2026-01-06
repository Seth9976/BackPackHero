using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000031 RID: 49
	public sealed class EmoteBuilder : IBuilder<Emote>
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x0000804D File Offset: 0x0000624D
		private EmoteBuilder()
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008055 File Offset: 0x00006255
		public static EmoteBuilder Create()
		{
			return new EmoteBuilder();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000805C File Offset: 0x0000625C
		public EmoteBuilder WithId(string id)
		{
			this._id = id;
			return this;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008066 File Offset: 0x00006266
		public EmoteBuilder WithName(string name)
		{
			this._name = name;
			return this;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008070 File Offset: 0x00006270
		public EmoteBuilder WithStartIndex(int startIndex)
		{
			this._startIndex = startIndex;
			return this;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000807A File Offset: 0x0000627A
		public EmoteBuilder WithEndIndex(int endIndex)
		{
			this._endIndex = endIndex;
			return this;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008084 File Offset: 0x00006284
		public Emote Build()
		{
			return new Emote(this._id, this._name, this._startIndex, this._endIndex);
		}

		// Token: 0x040001CD RID: 461
		private string _id;

		// Token: 0x040001CE RID: 462
		private string _name;

		// Token: 0x040001CF RID: 463
		private int _startIndex;

		// Token: 0x040001D0 RID: 464
		private int _endIndex;
	}
}

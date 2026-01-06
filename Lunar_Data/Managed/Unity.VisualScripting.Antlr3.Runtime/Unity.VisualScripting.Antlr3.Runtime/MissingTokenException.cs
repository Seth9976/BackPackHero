using System;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class MissingTokenException : MismatchedTokenException
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00005656 File Offset: 0x00004656
		public MissingTokenException()
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000565E File Offset: 0x0000465E
		public MissingTokenException(int expecting, IIntStream input, object inserted)
			: base(expecting, input)
		{
			this.inserted = inserted;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000566F File Offset: 0x0000466F
		public int MissingType
		{
			get
			{
				return base.Expecting;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00005677 File Offset: 0x00004677
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000567F File Offset: 0x0000467F
		public object Inserted
		{
			get
			{
				return this.inserted;
			}
			set
			{
				this.inserted = value;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005688 File Offset: 0x00004688
		public override string ToString()
		{
			if (this.inserted != null && this.token != null)
			{
				return string.Concat(new object[]
				{
					"MissingTokenException(inserted ",
					this.inserted,
					" at ",
					this.token.Text,
					")"
				});
			}
			if (this.token != null)
			{
				return "MissingTokenException(at " + this.token.Text + ")";
			}
			return "MissingTokenException";
		}

		// Token: 0x0400006A RID: 106
		private object inserted;
	}
}

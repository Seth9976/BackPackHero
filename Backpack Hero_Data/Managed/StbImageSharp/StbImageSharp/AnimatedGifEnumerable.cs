using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace StbImageSharp
{
	// Token: 0x02000007 RID: 7
	internal class AnimatedGifEnumerable : IEnumerable<AnimatedFrameResult>, IEnumerable
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002611 File Offset: 0x00000811
		public AnimatedGifEnumerable(Stream input, ColorComponents colorComponents)
		{
			this._input = input;
			this.ColorComponents = colorComponents;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002627 File Offset: 0x00000827
		public ColorComponents ColorComponents { get; }

		// Token: 0x0600002C RID: 44 RVA: 0x0000262F File Offset: 0x0000082F
		public IEnumerator<AnimatedFrameResult> GetEnumerator()
		{
			return new AnimatedGifEnumerator(this._input, this.ColorComponents);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002642 File Offset: 0x00000842
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000008 RID: 8
		private readonly Stream _input;
	}
}

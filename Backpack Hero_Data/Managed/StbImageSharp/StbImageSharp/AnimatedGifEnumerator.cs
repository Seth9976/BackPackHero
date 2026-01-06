using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Hebron.Runtime;

namespace StbImageSharp
{
	// Token: 0x02000006 RID: 6
	internal class AnimatedGifEnumerator : IEnumerator<AnimatedFrameResult>, IEnumerator, IDisposable
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000023B4 File Offset: 0x000005B4
		public AnimatedGifEnumerator(Stream input, ColorComponents colorComponents)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			this._context = new StbImage.stbi__context(input);
			if (StbImage.stbi__gif_test(this._context) == 0)
			{
				throw new Exception("Input stream is not GIF file.");
			}
			this._gif = new StbImage.stbi__gif();
			this.ColorComponents = colorComponents;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000240B File Offset: 0x0000060B
		public ColorComponents ColorComponents { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002413 File Offset: 0x00000613
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000241B File Offset: 0x0000061B
		public AnimatedFrameResult Current { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002424 File Offset: 0x00000624
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000242C File Offset: 0x0000062C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000243C File Offset: 0x0000063C
		public unsafe bool MoveNext()
		{
			int num;
			byte b;
			byte* ptr = StbImage.stbi__gif_load_next(this._context, this._gif, &num, (int)this.ColorComponents, &b);
			if (ptr == null)
			{
				return false;
			}
			if (this.Current == null)
			{
				this.Current = new AnimatedFrameResult
				{
					Width = this._gif.w,
					Height = this._gif.h,
					SourceComp = (ColorComponents)num,
					Comp = (ColorComponents)((this.ColorComponents == ColorComponents.Default) ? num : ((int)this.ColorComponents))
				};
				this.Current.Data = new byte[this.Current.Width * this.Current.Height * (int)this.Current.Comp];
			}
			this.Current.DelayInMs = this._gif.delay;
			Marshal.Copy(new IntPtr((void*)ptr), this.Current.Data, 0, this.Current.Data.Length);
			return true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000252F File Offset: 0x0000072F
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002538 File Offset: 0x00000738
		~AnimatedGifEnumerator()
		{
			this.Dispose(false);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002568 File Offset: 0x00000768
		protected unsafe virtual void Dispose(bool disposing)
		{
			if (disposing && this._gif != null)
			{
				if (this._gif._out_ != null)
				{
					CRuntime.free((void*)this._gif._out_);
					this._gif._out_ = null;
				}
				if (this._gif.history != null)
				{
					CRuntime.free((void*)this._gif.history);
					this._gif.history = null;
				}
				if (this._gif.background != null)
				{
					CRuntime.free((void*)this._gif.background);
					this._gif.background = null;
				}
				this._gif = null;
			}
		}

		// Token: 0x04000004 RID: 4
		private readonly StbImage.stbi__context _context;

		// Token: 0x04000005 RID: 5
		private StbImage.stbi__gif _gif;
	}
}

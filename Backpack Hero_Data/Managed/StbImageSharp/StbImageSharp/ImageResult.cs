using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Hebron.Runtime;

namespace StbImageSharp
{
	// Token: 0x0200000A RID: 10
	public class ImageResult
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026CA File Offset: 0x000008CA
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000026D2 File Offset: 0x000008D2
		public int Width { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000026DB File Offset: 0x000008DB
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000026E3 File Offset: 0x000008E3
		public int Height { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000026EC File Offset: 0x000008EC
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000026F4 File Offset: 0x000008F4
		public ColorComponents SourceComp { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000026FD File Offset: 0x000008FD
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002705 File Offset: 0x00000905
		public ColorComponents Comp { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000270E File Offset: 0x0000090E
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002716 File Offset: 0x00000916
		public byte[] Data { get; set; }

		// Token: 0x06000039 RID: 57 RVA: 0x00002720 File Offset: 0x00000920
		internal unsafe static ImageResult FromResult(byte* result, int width, int height, ColorComponents comp, ColorComponents req_comp)
		{
			if (result == null)
			{
				throw new InvalidOperationException(StbImage.stbi__g_failure_reason);
			}
			ImageResult imageResult = new ImageResult
			{
				Width = width,
				Height = height,
				SourceComp = comp,
				Comp = ((req_comp == ColorComponents.Default) ? comp : req_comp)
			};
			imageResult.Data = new byte[width * height * (int)imageResult.Comp];
			Marshal.Copy(new IntPtr((void*)result), imageResult.Data, 0, imageResult.Data.Length);
			return imageResult;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002798 File Offset: 0x00000998
		public unsafe static ImageResult FromStream(Stream stream, ColorComponents requiredComponents = ColorComponents.Default)
		{
			byte* ptr = null;
			ImageResult imageResult;
			try
			{
				int num;
				int num2;
				int num3;
				ptr = StbImage.stbi__load_and_postprocess_8bit(new StbImage.stbi__context(stream), &num, &num2, &num3, (int)requiredComponents);
				imageResult = ImageResult.FromResult(ptr, num, num2, (ColorComponents)num3, requiredComponents);
			}
			finally
			{
				if (ptr != null)
				{
					CRuntime.free((void*)ptr);
				}
			}
			return imageResult;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027EC File Offset: 0x000009EC
		public static ImageResult FromMemory(byte[] data, ColorComponents requiredComponents = ColorComponents.Default)
		{
			ImageResult imageResult;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				imageResult = ImageResult.FromStream(memoryStream, requiredComponents);
			}
			return imageResult;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002828 File Offset: 0x00000A28
		public static IEnumerable<AnimatedFrameResult> AnimatedGifFramesFromStream(Stream stream, ColorComponents requiredComponents = ColorComponents.Default)
		{
			return new AnimatedGifEnumerable(stream, requiredComponents);
		}
	}
}

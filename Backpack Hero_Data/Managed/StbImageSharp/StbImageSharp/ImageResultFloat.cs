using System;
using System.IO;
using System.Runtime.InteropServices;
using Hebron.Runtime;

namespace StbImageSharp
{
	// Token: 0x0200000B RID: 11
	public class ImageResultFloat
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002839 File Offset: 0x00000A39
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002841 File Offset: 0x00000A41
		public int Width { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000284A File Offset: 0x00000A4A
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002852 File Offset: 0x00000A52
		public int Height { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000285B File Offset: 0x00000A5B
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002863 File Offset: 0x00000A63
		public ColorComponents SourceComp { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000286C File Offset: 0x00000A6C
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002874 File Offset: 0x00000A74
		public ColorComponents Comp { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000287D File Offset: 0x00000A7D
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002885 File Offset: 0x00000A85
		public float[] Data { get; set; }

		// Token: 0x06000048 RID: 72 RVA: 0x00002890 File Offset: 0x00000A90
		internal unsafe static ImageResultFloat FromResult(float* result, int width, int height, ColorComponents comp, ColorComponents req_comp)
		{
			if (result == null)
			{
				throw new InvalidOperationException(StbImage.stbi__g_failure_reason);
			}
			ImageResultFloat imageResultFloat = new ImageResultFloat
			{
				Width = width,
				Height = height,
				SourceComp = comp,
				Comp = ((req_comp == ColorComponents.Default) ? comp : req_comp)
			};
			imageResultFloat.Data = new float[width * height * (int)imageResultFloat.Comp];
			Marshal.Copy(new IntPtr((void*)result), imageResultFloat.Data, 0, imageResultFloat.Data.Length);
			return imageResultFloat;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002908 File Offset: 0x00000B08
		public unsafe static ImageResultFloat FromStream(Stream stream, ColorComponents requiredComponents = ColorComponents.Default)
		{
			float* ptr = null;
			ImageResultFloat imageResultFloat;
			try
			{
				int num;
				int num2;
				int num3;
				ptr = StbImage.stbi__loadf_main(new StbImage.stbi__context(stream), &num, &num2, &num3, (int)requiredComponents);
				imageResultFloat = ImageResultFloat.FromResult(ptr, num, num2, (ColorComponents)num3, requiredComponents);
			}
			finally
			{
				if (ptr != null)
				{
					CRuntime.free((void*)ptr);
				}
			}
			return imageResultFloat;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000295C File Offset: 0x00000B5C
		public static ImageResultFloat FromMemory(byte[] data, ColorComponents requiredComponents = ColorComponents.Default)
		{
			ImageResultFloat imageResultFloat;
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				imageResultFloat = ImageResultFloat.FromStream(memoryStream, requiredComponents);
			}
			return imageResultFloat;
		}
	}
}

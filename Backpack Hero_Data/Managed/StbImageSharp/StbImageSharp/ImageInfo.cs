using System;
using System.IO;

namespace StbImageSharp
{
	// Token: 0x02000009 RID: 9
	public struct ImageInfo
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000264C File Offset: 0x0000084C
		public unsafe static ImageInfo? FromStream(Stream stream)
		{
			StbImage.stbi__context stbi__context = new StbImage.stbi__context(stream);
			bool flag = StbImage.stbi__is_16_main(stbi__context) == 1;
			StbImage.stbi__rewind(stbi__context);
			int num;
			int num2;
			int num3;
			bool flag2 = StbImage.stbi__info_main(stbi__context, &num, &num2, &num3) != 0;
			StbImage.stbi__rewind(stbi__context);
			if (!flag2)
			{
				return default(ImageInfo?);
			}
			return new ImageInfo?(new ImageInfo
			{
				Width = num,
				Height = num2,
				ColorComponents = (ColorComponents)num3,
				BitsPerChannel = (flag ? 16 : 8)
			});
		}

		// Token: 0x04000010 RID: 16
		public int Width;

		// Token: 0x04000011 RID: 17
		public int Height;

		// Token: 0x04000012 RID: 18
		public ColorComponents ColorComponents;

		// Token: 0x04000013 RID: 19
		public int BitsPerChannel;
	}
}

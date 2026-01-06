using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x02000275 RID: 629
	[NativeType(Header = "Runtime/2D/SpriteAtlas/SpriteAtlas.h")]
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	public class SpriteAtlas : Object
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001B53 RID: 6995
		public extern bool isVariant
		{
			[NativeMethod("IsVariant")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001B54 RID: 6996
		public extern string tag
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001B55 RID: 6997
		public extern int spriteCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001B56 RID: 6998
		[MethodImpl(4096)]
		public extern bool CanBindTo([NotNull("ArgumentNullException")] Sprite sprite);

		// Token: 0x06001B57 RID: 6999
		[MethodImpl(4096)]
		public extern Sprite GetSprite(string name);

		// Token: 0x06001B58 RID: 7000 RVA: 0x0002BCD8 File Offset: 0x00029ED8
		public int GetSprites(Sprite[] sprites)
		{
			return this.GetSpritesScripting(sprites);
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0002BCF4 File Offset: 0x00029EF4
		public int GetSprites(Sprite[] sprites, string name)
		{
			return this.GetSpritesWithNameScripting(sprites, name);
		}

		// Token: 0x06001B5A RID: 7002
		[MethodImpl(4096)]
		private extern int GetSpritesScripting(Sprite[] sprites);

		// Token: 0x06001B5B RID: 7003
		[MethodImpl(4096)]
		private extern int GetSpritesWithNameScripting(Sprite[] sprites, string name);
	}
}

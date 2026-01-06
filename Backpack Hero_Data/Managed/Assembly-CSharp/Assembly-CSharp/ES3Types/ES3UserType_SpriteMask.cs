using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200021B RID: 539
	[Preserve]
	[ES3Properties(new string[] { "sprite" })]
	public class ES3UserType_SpriteMask : ES3ComponentType
	{
		// Token: 0x060011FD RID: 4605 RVA: 0x000A965D File Offset: 0x000A785D
		public ES3UserType_SpriteMask()
			: base(typeof(SpriteMask))
		{
			ES3UserType_SpriteMask.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x000A967C File Offset: 0x000A787C
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SpriteMask spriteMask = (SpriteMask)obj;
			writer.WritePropertyByRef("sprite", spriteMask.sprite);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x000A96A4 File Offset: 0x000A78A4
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SpriteMask spriteMask = (SpriteMask)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "sprite")
					{
						spriteMask.sprite = reader.Read<Sprite>(ES3Type_Sprite.Instance);
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x04000E47 RID: 3655
		public static ES3Type Instance;
	}
}

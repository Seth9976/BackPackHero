using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000205 RID: 517
	[Preserve]
	[ES3Properties(new string[] { "slidingButtons" })]
	public class ES3UserType_SlidingRune : ES3ComponentType
	{
		// Token: 0x060011D1 RID: 4561 RVA: 0x000A80AD File Offset: 0x000A62AD
		public ES3UserType_SlidingRune()
			: base(typeof(SlidingRune))
		{
			ES3UserType_SlidingRune.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000A80CC File Offset: 0x000A62CC
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SlidingRune slidingRune = (SlidingRune)obj;
			writer.WritePrivateField("slidingButtons", slidingRune);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000A80EC File Offset: 0x000A62EC
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SlidingRune slidingRune = (SlidingRune)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "slidingButtons")
					{
						reader.SetPrivateField("slidingButtons", reader.Read<List<GameObject>>(), slidingRune);
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x04000E31 RID: 3633
		public static ES3Type Instance;
	}
}

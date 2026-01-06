using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000207 RID: 519
	[Preserve]
	[ES3Properties(new string[] { "direction" })]
	public class ES3UserType_SlidingRuneButton : ES3ComponentType
	{
		// Token: 0x060011D5 RID: 4565 RVA: 0x000A8189 File Offset: 0x000A6389
		public ES3UserType_SlidingRuneButton()
			: base(typeof(SlidingRuneButton))
		{
			ES3UserType_SlidingRuneButton.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000A81A8 File Offset: 0x000A63A8
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			SlidingRuneButton slidingRuneButton = (SlidingRuneButton)obj;
			writer.WritePrivateField("direction", slidingRuneButton);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x000A81C8 File Offset: 0x000A63C8
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			SlidingRuneButton slidingRuneButton = (SlidingRuneButton)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "direction")
					{
						reader.SetPrivateField("direction", reader.Read<Vector2>(), slidingRuneButton);
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x04000E33 RID: 3635
		public static ES3Type Instance;
	}
}

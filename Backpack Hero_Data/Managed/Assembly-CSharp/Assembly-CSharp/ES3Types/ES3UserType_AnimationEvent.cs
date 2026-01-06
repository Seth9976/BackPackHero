using System;
using System.Collections;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001B1 RID: 433
	[Preserve]
	[ES3Properties(new string[] { "dontFindMe" })]
	public class ES3UserType_AnimationEvent : ES3ComponentType
	{
		// Token: 0x06001129 RID: 4393 RVA: 0x000A1E74 File Offset: 0x000A0074
		public ES3UserType_AnimationEvent()
			: base(typeof(AnimationEvent))
		{
			ES3UserType_AnimationEvent.Instance = this;
			this.priority = 1;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000A1E94 File Offset: 0x000A0094
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			AnimationEvent animationEvent = (AnimationEvent)obj;
			writer.WriteProperty("dontFindMe", animationEvent.dontFindMe, ES3Type_bool.Instance);
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x000A1EC4 File Offset: 0x000A00C4
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			AnimationEvent animationEvent = (AnimationEvent)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "dontFindMe")
					{
						animationEvent.dontFindMe = reader.Read<bool>(ES3Type_bool.Instance);
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x04000DDD RID: 3549
		public static ES3Type Instance;
	}
}

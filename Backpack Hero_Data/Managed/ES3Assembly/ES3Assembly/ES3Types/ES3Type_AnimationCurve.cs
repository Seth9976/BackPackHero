using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200006B RID: 107
	[Preserve]
	[ES3Properties(new string[] { "keys", "preWrapMode", "postWrapMode" })]
	public class ES3Type_AnimationCurve : ES3Type
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000E004 File Offset: 0x0000C204
		public ES3Type_AnimationCurve()
			: base(typeof(AnimationCurve))
		{
			ES3Type_AnimationCurve.Instance = this;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000E01C File Offset: 0x0000C21C
		public override void Write(object obj, ES3Writer writer)
		{
			AnimationCurve animationCurve = (AnimationCurve)obj;
			writer.WriteProperty("keys", animationCurve.keys, ES3Type_KeyframeArray.Instance);
			writer.WriteProperty("preWrapMode", animationCurve.preWrapMode);
			writer.WriteProperty("postWrapMode", animationCurve.postWrapMode);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000E074 File Offset: 0x0000C274
		public override object Read<T>(ES3Reader reader)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			this.ReadInto<T>(reader, animationCurve);
			return animationCurve;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000E090 File Offset: 0x0000C290
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			AnimationCurve animationCurve = (AnimationCurve)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (!(text == "keys"))
				{
					if (!(text == "preWrapMode"))
					{
						if (!(text == "postWrapMode"))
						{
							reader.Skip();
						}
						else
						{
							animationCurve.postWrapMode = reader.Read<WrapMode>();
						}
					}
					else
					{
						animationCurve.preWrapMode = reader.Read<WrapMode>();
					}
				}
				else
				{
					animationCurve.keys = reader.Read<Keyframe[]>();
				}
			}
		}

		// Token: 0x0400009E RID: 158
		public static ES3Type Instance;
	}
}

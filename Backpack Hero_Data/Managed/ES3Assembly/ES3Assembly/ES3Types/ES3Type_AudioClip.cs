using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200006C RID: 108
	[Preserve]
	[ES3Properties(new string[] { "name", "samples", "channels", "frequency", "sampleData" })]
	public class ES3Type_AudioClip : ES3UnityObjectType
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000E109 File Offset: 0x0000C309
		public ES3Type_AudioClip()
			: base(typeof(AudioClip))
		{
			ES3Type_AudioClip.Instance = this;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E124 File Offset: 0x0000C324
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			AudioClip audioClip = (AudioClip)obj;
			float[] array = new float[audioClip.samples * audioClip.channels];
			audioClip.GetData(array, 0);
			writer.WriteProperty("name", audioClip.name);
			writer.WriteProperty("samples", audioClip.samples);
			writer.WriteProperty("channels", audioClip.channels);
			writer.WriteProperty("frequency", audioClip.frequency);
			writer.WriteProperty("sampleData", array);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			AudioClip audioClip = (AudioClip)obj;
			using (IEnumerator enumerator = reader.Properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Current == "sampleData")
					{
						audioClip.SetData(reader.Read<float[]>(ES3Type_floatArray.Instance), 0);
					}
					else
					{
						reader.Skip();
					}
				}
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000E234 File Offset: 0x0000C434
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			string text = "";
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			AudioClip audioClip = null;
			foreach (object obj in reader.Properties)
			{
				string text2 = (string)obj;
				if (!(text2 == "name"))
				{
					if (!(text2 == "samples"))
					{
						if (!(text2 == "channels"))
						{
							if (!(text2 == "frequency"))
							{
								if (!(text2 == "sampleData"))
								{
									reader.Skip();
								}
								else
								{
									audioClip = AudioClip.Create(text, num, num2, num3, false);
									audioClip.SetData(reader.Read<float[]>(ES3Type_floatArray.Instance), 0);
								}
							}
							else
							{
								num3 = reader.Read<int>(ES3Type_int.Instance);
							}
						}
						else
						{
							num2 = reader.Read<int>(ES3Type_int.Instance);
						}
					}
					else
					{
						num = reader.Read<int>(ES3Type_int.Instance);
					}
				}
				else
				{
					text = reader.Read<string>(ES3Type_string.Instance);
				}
			}
			return audioClip;
		}

		// Token: 0x0400009F RID: 159
		public static ES3Type Instance;
	}
}

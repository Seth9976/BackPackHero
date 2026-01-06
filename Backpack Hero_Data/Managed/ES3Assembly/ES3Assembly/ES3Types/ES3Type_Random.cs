using System;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000026 RID: 38
	[Preserve]
	[ES3Properties(new string[] { "inext", "inextp", "SeedArray" })]
	public class ES3Type_Random : ES3ObjectType
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000884C File Offset: 0x00006A4C
		public ES3Type_Random()
			: base(typeof(Random))
		{
			ES3Type_Random.Instance = this;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008864 File Offset: 0x00006A64
		protected override void WriteObject(object obj, ES3Writer writer)
		{
			Random random = (Random)obj;
			writer.WritePrivateField("inext", random);
			writer.WritePrivateField("inextp", random);
			writer.WritePrivateField("SeedArray", random);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000889C File Offset: 0x00006A9C
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			Random random = (Random)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				if (!(text == "inext"))
				{
					if (!(text == "inextp"))
					{
						if (!(text == "SeedArray"))
						{
							reader.Skip();
						}
						else
						{
							reader.SetPrivateField("SeedArray", reader.Read<int[]>(), random);
						}
					}
					else
					{
						reader.SetPrivateField("inextp", reader.Read<int>(), random);
					}
				}
				else
				{
					reader.SetPrivateField("inext", reader.Read<int>(), random);
				}
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008970 File Offset: 0x00006B70
		protected override object ReadObject<T>(ES3Reader reader)
		{
			Random random = new Random();
			this.ReadObject<T>(reader, random);
			return random;
		}

		// Token: 0x0400005D RID: 93
		public static ES3Type Instance;
	}
}

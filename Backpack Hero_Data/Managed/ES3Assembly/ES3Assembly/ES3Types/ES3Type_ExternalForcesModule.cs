using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200007A RID: 122
	[Preserve]
	[ES3Properties(new string[] { "enabled", "multiplier" })]
	public class ES3Type_ExternalForcesModule : ES3Type
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000F1DF File Offset: 0x0000D3DF
		public ES3Type_ExternalForcesModule()
			: base(typeof(ParticleSystem.ExternalForcesModule))
		{
			ES3Type_ExternalForcesModule.Instance = this;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.ExternalForcesModule externalForcesModule = (ParticleSystem.ExternalForcesModule)obj;
			writer.WriteProperty("enabled", externalForcesModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("multiplier", externalForcesModule.multiplier, ES3Type_float.Instance);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000F244 File Offset: 0x0000D444
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.ExternalForcesModule externalForcesModule = default(ParticleSystem.ExternalForcesModule);
			this.ReadInto<T>(reader, externalForcesModule);
			return externalForcesModule;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000F26C File Offset: 0x0000D46C
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.ExternalForcesModule externalForcesModule = (ParticleSystem.ExternalForcesModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (!(text == "enabled"))
				{
					if (!(text == "multiplier"))
					{
						reader.Skip();
					}
					else
					{
						externalForcesModule.multiplier = reader.Read<float>(ES3Type_float.Instance);
					}
				}
				else
				{
					externalForcesModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x040000AD RID: 173
		public static ES3Type Instance;
	}
}

using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000078 RID: 120
	[Preserve]
	[ES3Properties(new string[] { "enabled", "color" })]
	public class ES3Type_ColorOverLifetimeModule : ES3Type
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000EF33 File Offset: 0x0000D133
		public ES3Type_ColorOverLifetimeModule()
			: base(typeof(ParticleSystem.ColorOverLifetimeModule))
		{
			ES3Type_ColorOverLifetimeModule.Instance = this;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000EF4C File Offset: 0x0000D14C
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = (ParticleSystem.ColorOverLifetimeModule)obj;
			writer.WriteProperty("enabled", colorOverLifetimeModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("color", colorOverLifetimeModule.color, ES3Type_MinMaxGradient.Instance);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000EF98 File Offset: 0x0000D198
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = default(ParticleSystem.ColorOverLifetimeModule);
			this.ReadInto<T>(reader, colorOverLifetimeModule);
			return colorOverLifetimeModule;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000EFC0 File Offset: 0x0000D1C0
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = (ParticleSystem.ColorOverLifetimeModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (!(text == "enabled"))
				{
					if (!(text == "color"))
					{
						reader.Skip();
					}
					else
					{
						colorOverLifetimeModule.color = reader.Read<ParticleSystem.MinMaxGradient>(ES3Type_MinMaxGradient.Instance);
					}
				}
				else
				{
					colorOverLifetimeModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x040000AB RID: 171
		public static ES3Type Instance;
	}
}

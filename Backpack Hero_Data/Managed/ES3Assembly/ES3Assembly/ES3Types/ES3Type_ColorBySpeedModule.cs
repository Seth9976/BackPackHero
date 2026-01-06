using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000077 RID: 119
	[Preserve]
	[ES3Properties(new string[] { "enabled", "color", "range" })]
	public class ES3Type_ColorBySpeedModule : ES3Type
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x0000EDFE File Offset: 0x0000CFFE
		public ES3Type_ColorBySpeedModule()
			: base(typeof(ParticleSystem.ColorBySpeedModule))
		{
			ES3Type_ColorBySpeedModule.Instance = this;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000EE18 File Offset: 0x0000D018
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.ColorBySpeedModule colorBySpeedModule = (ParticleSystem.ColorBySpeedModule)obj;
			writer.WriteProperty("enabled", colorBySpeedModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("color", colorBySpeedModule.color, ES3Type_MinMaxGradient.Instance);
			writer.WriteProperty("range", colorBySpeedModule.range, ES3Type_Vector2.Instance);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000EE80 File Offset: 0x0000D080
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.ColorBySpeedModule colorBySpeedModule = default(ParticleSystem.ColorBySpeedModule);
			this.ReadInto<T>(reader, colorBySpeedModule);
			return colorBySpeedModule;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.ColorBySpeedModule colorBySpeedModule = (ParticleSystem.ColorBySpeedModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (!(text == "enabled"))
				{
					if (!(text == "color"))
					{
						if (!(text == "range"))
						{
							reader.Skip();
						}
						else
						{
							colorBySpeedModule.range = reader.Read<Vector2>(ES3Type_Vector2.Instance);
						}
					}
					else
					{
						colorBySpeedModule.color = reader.Read<ParticleSystem.MinMaxGradient>(ES3Type_MinMaxGradient.Instance);
					}
				}
				else
				{
					colorBySpeedModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x040000AA RID: 170
		public static ES3Type Instance;
	}
}

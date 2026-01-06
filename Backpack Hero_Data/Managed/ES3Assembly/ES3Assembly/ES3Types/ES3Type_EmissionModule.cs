using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000079 RID: 121
	[Preserve]
	[ES3Properties(new string[] { "enabled", "rateOverTime", "rateOverTimeMultiplier", "rateOverDistance", "rateOverDistanceMultiplier" })]
	public class ES3Type_EmissionModule : ES3Type
	{
		// Token: 0x060002FD RID: 765 RVA: 0x0000F02A File Offset: 0x0000D22A
		public ES3Type_EmissionModule()
			: base(typeof(ParticleSystem.EmissionModule))
		{
			ES3Type_EmissionModule.Instance = this;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000F044 File Offset: 0x0000D244
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.EmissionModule emissionModule = (ParticleSystem.EmissionModule)obj;
			writer.WriteProperty("enabled", emissionModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("rateOverTime", emissionModule.rateOverTime, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("rateOverTimeMultiplier", emissionModule.rateOverTimeMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("rateOverDistance", emissionModule.rateOverDistance, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("rateOverDistanceMultiplier", emissionModule.rateOverDistanceMultiplier, ES3Type_float.Instance);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.EmissionModule emissionModule = default(ParticleSystem.EmissionModule);
			this.ReadInto<T>(reader, emissionModule);
			return emissionModule;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000F10C File Offset: 0x0000D30C
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.EmissionModule emissionModule = (ParticleSystem.EmissionModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (!(text == "enabled"))
				{
					if (!(text == "rateOverTime"))
					{
						if (!(text == "rateOverTimeMultiplier"))
						{
							if (!(text == "rateOverDistance"))
							{
								if (!(text == "rateOverDistanceMultiplier"))
								{
									reader.Skip();
								}
								else
								{
									emissionModule.rateOverDistanceMultiplier = reader.Read<float>(ES3Type_float.Instance);
								}
							}
							else
							{
								emissionModule.rateOverDistance = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							}
						}
						else
						{
							emissionModule.rateOverTimeMultiplier = reader.Read<float>(ES3Type_float.Instance);
						}
					}
					else
					{
						emissionModule.rateOverTime = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
					}
				}
				else
				{
					emissionModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x040000AC RID: 172
		public static ES3Type Instance;
	}
}

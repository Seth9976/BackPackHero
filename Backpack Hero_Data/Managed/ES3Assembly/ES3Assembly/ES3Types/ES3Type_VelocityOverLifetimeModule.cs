using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000C1 RID: 193
	[Preserve]
	[ES3Properties(new string[] { "enabled", "x", "y", "z", "xMultiplier", "yMultiplier", "zMultiplier", "space" })]
	public class ES3Type_VelocityOverLifetimeModule : ES3Type
	{
		// Token: 0x060003CE RID: 974 RVA: 0x0001E4DE File Offset: 0x0001C6DE
		public ES3Type_VelocityOverLifetimeModule()
			: base(typeof(ParticleSystem.VelocityOverLifetimeModule))
		{
			ES3Type_VelocityOverLifetimeModule.Instance = this;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001E4F8 File Offset: 0x0001C6F8
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = (ParticleSystem.VelocityOverLifetimeModule)obj;
			writer.WriteProperty("enabled", velocityOverLifetimeModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("x", velocityOverLifetimeModule.x, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("y", velocityOverLifetimeModule.y, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("z", velocityOverLifetimeModule.z, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("xMultiplier", velocityOverLifetimeModule.xMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("yMultiplier", velocityOverLifetimeModule.yMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("zMultiplier", velocityOverLifetimeModule.zMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("space", velocityOverLifetimeModule.space);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = default(ParticleSystem.VelocityOverLifetimeModule);
			this.ReadInto<T>(reader, velocityOverLifetimeModule);
			return velocityOverLifetimeModule;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001E610 File Offset: 0x0001C810
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = (ParticleSystem.VelocityOverLifetimeModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 3281097867U)
				{
					if (num <= 726266686U)
					{
						if (num != 49525662U)
						{
							if (num == 726266686U)
							{
								if (text == "zMultiplier")
								{
									velocityOverLifetimeModule.zMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "enabled")
						{
							velocityOverLifetimeModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 894689925U)
					{
						if (num == 3281097867U)
						{
							if (text == "yMultiplier")
							{
								velocityOverLifetimeModule.yMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "space")
					{
						velocityOverLifetimeModule.space = reader.Read<ParticleSystemSimulationSpace>();
						continue;
					}
				}
				else if (num <= 4228665076U)
				{
					if (num != 3709916316U)
					{
						if (num == 4228665076U)
						{
							if (text == "y")
							{
								velocityOverLifetimeModule.y = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "xMultiplier")
					{
						velocityOverLifetimeModule.xMultiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 4245442695U)
				{
					if (num == 4278997933U)
					{
						if (text == "z")
						{
							velocityOverLifetimeModule.z = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
							continue;
						}
					}
				}
				else if (text == "x")
				{
					velocityOverLifetimeModule.x = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000F7 RID: 247
		public static ES3Type Instance;
	}
}

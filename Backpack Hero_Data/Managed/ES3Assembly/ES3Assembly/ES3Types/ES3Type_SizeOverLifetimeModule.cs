using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000A9 RID: 169
	[Preserve]
	[ES3Properties(new string[] { "enabled", "size", "sizeMultiplier", "x", "xMultiplier", "y", "yMultiplier", "z", "zMultiplier", "separateAxes" })]
	public class ES3Type_SizeOverLifetimeModule : ES3Type
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0001B8E0 File Offset: 0x00019AE0
		public ES3Type_SizeOverLifetimeModule()
			: base(typeof(ParticleSystem.SizeOverLifetimeModule))
		{
			ES3Type_SizeOverLifetimeModule.Instance = this;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule = (ParticleSystem.SizeOverLifetimeModule)obj;
			writer.WriteProperty("enabled", sizeOverLifetimeModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("size", sizeOverLifetimeModule.size, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("sizeMultiplier", sizeOverLifetimeModule.sizeMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("x", sizeOverLifetimeModule.x, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("xMultiplier", sizeOverLifetimeModule.xMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("y", sizeOverLifetimeModule.y, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("yMultiplier", sizeOverLifetimeModule.yMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("z", sizeOverLifetimeModule.z, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("zMultiplier", sizeOverLifetimeModule.zMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("separateAxes", sizeOverLifetimeModule.separateAxes, ES3Type_bool.Instance);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001BA24 File Offset: 0x00019C24
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule = default(ParticleSystem.SizeOverLifetimeModule);
			this.ReadInto<T>(reader, sizeOverLifetimeModule);
			return sizeOverLifetimeModule;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0001BA4C File Offset: 0x00019C4C
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule = (ParticleSystem.SizeOverLifetimeModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1479031685U)
				{
					if (num <= 597743964U)
					{
						if (num != 49525662U)
						{
							if (num == 597743964U)
							{
								if (text == "size")
								{
									sizeOverLifetimeModule.size = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
									continue;
								}
							}
						}
						else if (text == "enabled")
						{
							sizeOverLifetimeModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 726266686U)
					{
						if (num != 1105436259U)
						{
							if (num == 1479031685U)
							{
								if (text == "separateAxes")
								{
									sizeOverLifetimeModule.separateAxes = reader.Read<bool>(ES3Type_bool.Instance);
									continue;
								}
							}
						}
						else if (text == "sizeMultiplier")
						{
							sizeOverLifetimeModule.sizeMultiplier = reader.Read<float>(ES3Type_float.Instance);
							continue;
						}
					}
					else if (text == "zMultiplier")
					{
						sizeOverLifetimeModule.zMultiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num <= 3709916316U)
				{
					if (num != 3281097867U)
					{
						if (num == 3709916316U)
						{
							if (text == "xMultiplier")
							{
								sizeOverLifetimeModule.xMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "yMultiplier")
					{
						sizeOverLifetimeModule.yMultiplier = reader.Read<float>(ES3Type_float.Instance);
						continue;
					}
				}
				else if (num != 4228665076U)
				{
					if (num != 4245442695U)
					{
						if (num == 4278997933U)
						{
							if (text == "z")
							{
								sizeOverLifetimeModule.z = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "x")
					{
						sizeOverLifetimeModule.x = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						continue;
					}
				}
				else if (text == "y")
				{
					sizeOverLifetimeModule.y = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000DF RID: 223
		public static ES3Type Instance;
	}
}

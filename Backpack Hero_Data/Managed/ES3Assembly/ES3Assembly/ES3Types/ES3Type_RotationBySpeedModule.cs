using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000A3 RID: 163
	[Preserve]
	[ES3Properties(new string[] { "enabled", "x", "xMultiplier", "y", "yMultiplier", "z", "zMultiplier", "separateAxes", "range" })]
	public class ES3Type_RotationBySpeedModule : ES3Type
	{
		// Token: 0x06000379 RID: 889 RVA: 0x0001A64B File Offset: 0x0001884B
		public ES3Type_RotationBySpeedModule()
			: base(typeof(ParticleSystem.RotationBySpeedModule))
		{
			ES3Type_RotationBySpeedModule.Instance = this;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001A664 File Offset: 0x00018864
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.RotationBySpeedModule rotationBySpeedModule = (ParticleSystem.RotationBySpeedModule)obj;
			writer.WriteProperty("enabled", rotationBySpeedModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("x", rotationBySpeedModule.x, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("xMultiplier", rotationBySpeedModule.xMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("y", rotationBySpeedModule.y, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("yMultiplier", rotationBySpeedModule.yMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("z", rotationBySpeedModule.z, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("zMultiplier", rotationBySpeedModule.zMultiplier, ES3Type_float.Instance);
			writer.WriteProperty("separateAxes", rotationBySpeedModule.separateAxes, ES3Type_bool.Instance);
			writer.WriteProperty("range", rotationBySpeedModule.range, ES3Type_Vector2.Instance);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001A774 File Offset: 0x00018974
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.RotationBySpeedModule rotationBySpeedModule = default(ParticleSystem.RotationBySpeedModule);
			this.ReadInto<T>(reader, rotationBySpeedModule);
			return rotationBySpeedModule;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001A79C File Offset: 0x0001899C
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.RotationBySpeedModule rotationBySpeedModule = (ParticleSystem.RotationBySpeedModule)obj;
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
									rotationBySpeedModule.zMultiplier = reader.Read<float>(ES3Type_float.Instance);
									continue;
								}
							}
						}
						else if (text == "enabled")
						{
							rotationBySpeedModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
							continue;
						}
					}
					else if (num != 1479031685U)
					{
						if (num == 3281097867U)
						{
							if (text == "yMultiplier")
							{
								rotationBySpeedModule.yMultiplier = reader.Read<float>(ES3Type_float.Instance);
								continue;
							}
						}
					}
					else if (text == "separateAxes")
					{
						rotationBySpeedModule.separateAxes = reader.Read<bool>(ES3Type_bool.Instance);
						continue;
					}
				}
				else if (num <= 4208725202U)
				{
					if (num != 3709916316U)
					{
						if (num == 4208725202U)
						{
							if (text == "range")
							{
								rotationBySpeedModule.range = reader.Read<Vector2>(ES3Type_Vector2.Instance);
								continue;
							}
						}
					}
					else if (text == "xMultiplier")
					{
						rotationBySpeedModule.xMultiplier = reader.Read<float>(ES3Type_float.Instance);
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
								rotationBySpeedModule.z = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
								continue;
							}
						}
					}
					else if (text == "x")
					{
						rotationBySpeedModule.x = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						continue;
					}
				}
				else if (text == "y")
				{
					rotationBySpeedModule.y = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x040000D9 RID: 217
		public static ES3Type Instance;
	}
}

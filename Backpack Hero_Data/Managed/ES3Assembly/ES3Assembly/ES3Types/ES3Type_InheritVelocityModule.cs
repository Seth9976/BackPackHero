using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000089 RID: 137
	[Preserve]
	[ES3Properties(new string[] { "enabled", "mode", "curve", "curveMultiplier" })]
	public class ES3Type_InheritVelocityModule : ES3Type
	{
		// Token: 0x0600032E RID: 814 RVA: 0x00010229 File Offset: 0x0000E429
		public ES3Type_InheritVelocityModule()
			: base(typeof(ParticleSystem.InheritVelocityModule))
		{
			ES3Type_InheritVelocityModule.Instance = this;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00010244 File Offset: 0x0000E444
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.InheritVelocityModule inheritVelocityModule = (ParticleSystem.InheritVelocityModule)obj;
			writer.WriteProperty("enabled", inheritVelocityModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("mode", inheritVelocityModule.mode);
			writer.WriteProperty("curve", inheritVelocityModule.curve, ES3Type_MinMaxCurve.Instance);
			writer.WriteProperty("curveMultiplier", inheritVelocityModule.curveMultiplier, ES3Type_float.Instance);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000102C4 File Offset: 0x0000E4C4
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.InheritVelocityModule inheritVelocityModule = default(ParticleSystem.InheritVelocityModule);
			this.ReadInto<T>(reader, inheritVelocityModule);
			return inheritVelocityModule;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000102EC File Offset: 0x0000E4EC
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.InheritVelocityModule inheritVelocityModule = (ParticleSystem.InheritVelocityModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (!(text == "enabled"))
				{
					if (!(text == "mode"))
					{
						if (!(text == "curve"))
						{
							if (!(text == "curveMultiplier"))
							{
								reader.Skip();
							}
							else
							{
								inheritVelocityModule.curveMultiplier = reader.Read<float>(ES3Type_float.Instance);
							}
						}
						else
						{
							inheritVelocityModule.curve = reader.Read<ParticleSystem.MinMaxCurve>(ES3Type_MinMaxCurve.Instance);
						}
					}
					else
					{
						inheritVelocityModule.mode = reader.Read<ParticleSystemInheritVelocityMode>();
					}
				}
				else
				{
					inheritVelocityModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x040000BF RID: 191
		public static ES3Type Instance;
	}
}

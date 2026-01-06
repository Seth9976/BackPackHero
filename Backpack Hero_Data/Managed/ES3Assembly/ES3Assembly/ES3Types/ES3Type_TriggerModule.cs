using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000B6 RID: 182
	[Preserve]
	[ES3Properties(new string[] { "enabled", "inside", "outside", "enter", "exit", "radiusScale" })]
	public class ES3Type_TriggerModule : ES3Type
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x0001DEF2 File Offset: 0x0001C0F2
		public ES3Type_TriggerModule()
			: base(typeof(ParticleSystem.TriggerModule))
		{
			ES3Type_TriggerModule.Instance = this;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001DF0C File Offset: 0x0001C10C
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.TriggerModule triggerModule = (ParticleSystem.TriggerModule)obj;
			writer.WriteProperty("enabled", triggerModule.enabled, ES3Type_bool.Instance);
			writer.WriteProperty("inside", triggerModule.inside);
			writer.WriteProperty("outside", triggerModule.outside);
			writer.WriteProperty("enter", triggerModule.enter);
			writer.WriteProperty("exit", triggerModule.exit);
			writer.WriteProperty("radiusScale", triggerModule.radiusScale, ES3Type_float.Instance);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.TriggerModule triggerModule = default(ParticleSystem.TriggerModule);
			this.ReadInto<T>(reader, triggerModule);
			return triggerModule;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001DFDC File Offset: 0x0001C1DC
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.TriggerModule triggerModule = (ParticleSystem.TriggerModule)obj;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (!(text == "enabled"))
				{
					if (!(text == "inside"))
					{
						if (!(text == "outside"))
						{
							if (!(text == "enter"))
							{
								if (!(text == "exit"))
								{
									if (!(text == "radiusScale"))
									{
										reader.Skip();
									}
									else
									{
										triggerModule.radiusScale = reader.Read<float>(ES3Type_float.Instance);
									}
								}
								else
								{
									triggerModule.exit = reader.Read<ParticleSystemOverlapAction>();
								}
							}
							else
							{
								triggerModule.enter = reader.Read<ParticleSystemOverlapAction>();
							}
						}
						else
						{
							triggerModule.outside = reader.Read<ParticleSystemOverlapAction>();
						}
					}
					else
					{
						triggerModule.inside = reader.Read<ParticleSystemOverlapAction>();
					}
				}
				else
				{
					triggerModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
		}

		// Token: 0x040000EC RID: 236
		public static ES3Type Instance;
	}
}

using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020000AF RID: 175
	[Preserve]
	[ES3Properties(new string[] { "properties", "systems", "types" })]
	public class ES3Type_SubEmittersModule : ES3Type
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0001CEF9 File Offset: 0x0001B0F9
		public ES3Type_SubEmittersModule()
			: base(typeof(ParticleSystem.SubEmittersModule))
		{
			ES3Type_SubEmittersModule.Instance = this;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001CF14 File Offset: 0x0001B114
		public override void Write(object obj, ES3Writer writer)
		{
			ParticleSystem.SubEmittersModule subEmittersModule = (ParticleSystem.SubEmittersModule)obj;
			ParticleSystemSubEmitterProperties[] array = new ParticleSystemSubEmitterProperties[subEmittersModule.subEmittersCount];
			ParticleSystem[] array2 = new ParticleSystem[subEmittersModule.subEmittersCount];
			ParticleSystemSubEmitterType[] array3 = new ParticleSystemSubEmitterType[subEmittersModule.subEmittersCount];
			for (int i = 0; i < subEmittersModule.subEmittersCount; i++)
			{
				array[i] = subEmittersModule.GetSubEmitterProperties(i);
				array2[i] = subEmittersModule.GetSubEmitterSystem(i);
				array3[i] = subEmittersModule.GetSubEmitterType(i);
			}
			writer.WriteProperty("properties", array);
			writer.WriteProperty("systems", array2);
			writer.WriteProperty("types", array3);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001CFB0 File Offset: 0x0001B1B0
		public override object Read<T>(ES3Reader reader)
		{
			ParticleSystem.SubEmittersModule subEmittersModule = default(ParticleSystem.SubEmittersModule);
			this.ReadInto<T>(reader, subEmittersModule);
			return subEmittersModule;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001CFD8 File Offset: 0x0001B1D8
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			ParticleSystem.SubEmittersModule subEmittersModule = (ParticleSystem.SubEmittersModule)obj;
			ParticleSystemSubEmitterProperties[] array = null;
			ParticleSystem[] array2 = null;
			ParticleSystemSubEmitterType[] array3 = null;
			string text;
			while ((text = reader.ReadPropertyName()) != null)
			{
				if (!(text == "enabled"))
				{
					if (!(text == "properties"))
					{
						if (!(text == "systems"))
						{
							if (!(text == "types"))
							{
								reader.Skip();
							}
							else
							{
								array3 = reader.Read<ParticleSystemSubEmitterType[]>();
							}
						}
						else
						{
							array2 = reader.Read<ParticleSystem[]>();
						}
					}
					else
					{
						array = reader.Read<ParticleSystemSubEmitterProperties[]>(new ES3ArrayType(typeof(ParticleSystemSubEmitterProperties[])));
					}
				}
				else
				{
					subEmittersModule.enabled = reader.Read<bool>(ES3Type_bool.Instance);
				}
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					subEmittersModule.RemoveSubEmitter(i);
					subEmittersModule.AddSubEmitter(array2[i], array3[i], array[i]);
				}
			}
		}

		// Token: 0x040000E5 RID: 229
		public static ES3Type Instance;
	}
}

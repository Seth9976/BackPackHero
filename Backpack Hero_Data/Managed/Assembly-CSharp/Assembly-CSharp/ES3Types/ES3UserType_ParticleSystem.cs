using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x020001F5 RID: 501
	[Preserve]
	[ES3Properties(new string[] { })]
	public class ES3UserType_ParticleSystem : ES3ComponentType
	{
		// Token: 0x060011B1 RID: 4529 RVA: 0x000A6D85 File Offset: 0x000A4F85
		public ES3UserType_ParticleSystem()
			: base(typeof(ParticleSystem))
		{
			ES3UserType_ParticleSystem.Instance = this;
			this.priority = 1;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000A6DA4 File Offset: 0x000A4FA4
		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			ParticleSystem particleSystem = (ParticleSystem)obj;
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000A6DB0 File Offset: 0x000A4FB0
		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			ParticleSystem particleSystem = (ParticleSystem)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				reader.Skip();
			}
		}

		// Token: 0x04000E21 RID: 3617
		public static ES3Type Instance;
	}
}

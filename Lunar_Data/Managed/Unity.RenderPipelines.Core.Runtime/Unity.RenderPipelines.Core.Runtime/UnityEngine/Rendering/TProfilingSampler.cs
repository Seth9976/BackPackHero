using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000070 RID: 112
	internal class TProfilingSampler<TEnum> : ProfilingSampler where TEnum : Enum
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00011708 File Offset: 0x0000F908
		static TProfilingSampler()
		{
			string[] names = Enum.GetNames(typeof(TEnum));
			Array values = Enum.GetValues(typeof(TEnum));
			for (int i = 0; i < names.Length; i++)
			{
				TProfilingSampler<TEnum> tprofilingSampler = new TProfilingSampler<TEnum>(names[i]);
				TProfilingSampler<TEnum>.samples.Add((TEnum)((object)values.GetValue(i)), tprofilingSampler);
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001176D File Offset: 0x0000F96D
		public TProfilingSampler(string name)
			: base(name)
		{
		}

		// Token: 0x04000245 RID: 581
		internal static Dictionary<TEnum, TProfilingSampler<TEnum>> samples = new Dictionary<TEnum, TProfilingSampler<TEnum>>();
	}
}

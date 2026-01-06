using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A9 RID: 681
	internal static class StyleValueFunctionExtension
	{
		// Token: 0x06001711 RID: 5905 RVA: 0x0005E268 File Offset: 0x0005C468
		public static StyleValueFunction FromUssString(string ussValue)
		{
			ussValue = ussValue.ToLower();
			string text = ussValue;
			string text2 = text;
			StyleValueFunction styleValueFunction;
			if (!(text2 == "var"))
			{
				if (!(text2 == "env"))
				{
					if (!(text2 == "linear-gradient"))
					{
						throw new ArgumentOutOfRangeException("ussValue", ussValue, "Unknown function name");
					}
					styleValueFunction = StyleValueFunction.LinearGradient;
				}
				else
				{
					styleValueFunction = StyleValueFunction.Env;
				}
			}
			else
			{
				styleValueFunction = StyleValueFunction.Var;
			}
			return styleValueFunction;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0005E2CC File Offset: 0x0005C4CC
		public static string ToUssString(this StyleValueFunction svf)
		{
			string text;
			switch (svf)
			{
			case StyleValueFunction.Var:
				text = "var";
				break;
			case StyleValueFunction.Env:
				text = "env";
				break;
			case StyleValueFunction.LinearGradient:
				text = "linear-gradient";
				break;
			default:
				throw new ArgumentOutOfRangeException("svf", svf, "Unknown StyleValueFunction");
			}
			return text;
		}

		// Token: 0x040009BF RID: 2495
		public const string k_Var = "var";

		// Token: 0x040009C0 RID: 2496
		public const string k_Env = "env";

		// Token: 0x040009C1 RID: 2497
		public const string k_LinearGradient = "linear-gradient";
	}
}

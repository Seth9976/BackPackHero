using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000367 RID: 871
	internal static class StyleSheetColor
	{
		// Token: 0x06001BE4 RID: 7140 RVA: 0x000814F4 File Offset: 0x0007F6F4
		public static bool TryGetColor(string name, out Color color)
		{
			Color32 color2;
			bool flag = StyleSheetColor.s_NameToColor.TryGetValue(name, ref color2);
			color = color2;
			return flag;
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x00081524 File Offset: 0x0007F724
		private static Color32 HexToColor32(uint color)
		{
			byte b = (byte)(color & 255U);
			byte b2 = (byte)((color >> 8) & 255U);
			byte b3 = (byte)((color >> 16) & 255U);
			return new Color32(b3, b2, b, byte.MaxValue);
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x00081564 File Offset: 0x0007F764
		// Note: this type is marked as 'beforefieldinit'.
		static StyleSheetColor()
		{
			Dictionary<string, Color32> dictionary = new Dictionary<string, Color32>();
			dictionary.Add("aliceblue", StyleSheetColor.HexToColor32(15792383U));
			dictionary.Add("antiquewhite", StyleSheetColor.HexToColor32(16444375U));
			dictionary.Add("aqua", StyleSheetColor.HexToColor32(65535U));
			dictionary.Add("aquamarine", StyleSheetColor.HexToColor32(8388564U));
			dictionary.Add("azure", StyleSheetColor.HexToColor32(15794175U));
			dictionary.Add("beige", StyleSheetColor.HexToColor32(16119260U));
			dictionary.Add("bisque", StyleSheetColor.HexToColor32(16770244U));
			dictionary.Add("black", StyleSheetColor.HexToColor32(0U));
			dictionary.Add("blanchedalmond", StyleSheetColor.HexToColor32(16772045U));
			dictionary.Add("blue", StyleSheetColor.HexToColor32(255U));
			dictionary.Add("blueviolet", StyleSheetColor.HexToColor32(9055202U));
			dictionary.Add("brown", StyleSheetColor.HexToColor32(10824234U));
			dictionary.Add("burlywood", StyleSheetColor.HexToColor32(14596231U));
			dictionary.Add("cadetblue", StyleSheetColor.HexToColor32(6266528U));
			dictionary.Add("chartreuse", StyleSheetColor.HexToColor32(8388352U));
			dictionary.Add("chocolate", StyleSheetColor.HexToColor32(13789470U));
			dictionary.Add("coral", StyleSheetColor.HexToColor32(16744272U));
			dictionary.Add("cornflowerblue", StyleSheetColor.HexToColor32(6591981U));
			dictionary.Add("cornsilk", StyleSheetColor.HexToColor32(16775388U));
			dictionary.Add("crimson", StyleSheetColor.HexToColor32(14423100U));
			dictionary.Add("cyan", StyleSheetColor.HexToColor32(65535U));
			dictionary.Add("darkblue", StyleSheetColor.HexToColor32(139U));
			dictionary.Add("darkcyan", StyleSheetColor.HexToColor32(35723U));
			dictionary.Add("darkgoldenrod", StyleSheetColor.HexToColor32(12092939U));
			dictionary.Add("darkgray", StyleSheetColor.HexToColor32(11119017U));
			dictionary.Add("darkgreen", StyleSheetColor.HexToColor32(25600U));
			dictionary.Add("darkgrey", StyleSheetColor.HexToColor32(11119017U));
			dictionary.Add("darkkhaki", StyleSheetColor.HexToColor32(12433259U));
			dictionary.Add("darkmagenta", StyleSheetColor.HexToColor32(9109643U));
			dictionary.Add("darkolivegreen", StyleSheetColor.HexToColor32(5597999U));
			dictionary.Add("darkorange", StyleSheetColor.HexToColor32(16747520U));
			dictionary.Add("darkorchid", StyleSheetColor.HexToColor32(10040012U));
			dictionary.Add("darkred", StyleSheetColor.HexToColor32(9109504U));
			dictionary.Add("darksalmon", StyleSheetColor.HexToColor32(15308410U));
			dictionary.Add("darkseagreen", StyleSheetColor.HexToColor32(9419919U));
			dictionary.Add("darkslateblue", StyleSheetColor.HexToColor32(4734347U));
			dictionary.Add("darkslategray", StyleSheetColor.HexToColor32(3100495U));
			dictionary.Add("darkslategrey", StyleSheetColor.HexToColor32(3100495U));
			dictionary.Add("darkturquoise", StyleSheetColor.HexToColor32(52945U));
			dictionary.Add("darkviolet", StyleSheetColor.HexToColor32(9699539U));
			dictionary.Add("deeppink", StyleSheetColor.HexToColor32(16716947U));
			dictionary.Add("deepskyblue", StyleSheetColor.HexToColor32(49151U));
			dictionary.Add("dimgray", StyleSheetColor.HexToColor32(6908265U));
			dictionary.Add("dimgrey", StyleSheetColor.HexToColor32(6908265U));
			dictionary.Add("dodgerblue", StyleSheetColor.HexToColor32(2003199U));
			dictionary.Add("firebrick", StyleSheetColor.HexToColor32(11674146U));
			dictionary.Add("floralwhite", StyleSheetColor.HexToColor32(16775920U));
			dictionary.Add("forestgreen", StyleSheetColor.HexToColor32(2263842U));
			dictionary.Add("fuchsia", StyleSheetColor.HexToColor32(16711935U));
			dictionary.Add("gainsboro", StyleSheetColor.HexToColor32(14474460U));
			dictionary.Add("ghostwhite", StyleSheetColor.HexToColor32(16316671U));
			dictionary.Add("goldenrod", StyleSheetColor.HexToColor32(14329120U));
			dictionary.Add("gold", StyleSheetColor.HexToColor32(16766720U));
			dictionary.Add("gray", StyleSheetColor.HexToColor32(8421504U));
			dictionary.Add("green", StyleSheetColor.HexToColor32(32768U));
			dictionary.Add("greenyellow", StyleSheetColor.HexToColor32(11403055U));
			dictionary.Add("grey", StyleSheetColor.HexToColor32(8421504U));
			dictionary.Add("honeydew", StyleSheetColor.HexToColor32(15794160U));
			dictionary.Add("hotpink", StyleSheetColor.HexToColor32(16738740U));
			dictionary.Add("indianred", StyleSheetColor.HexToColor32(13458524U));
			dictionary.Add("indigo", StyleSheetColor.HexToColor32(4915330U));
			dictionary.Add("ivory", StyleSheetColor.HexToColor32(16777200U));
			dictionary.Add("khaki", StyleSheetColor.HexToColor32(15787660U));
			dictionary.Add("lavenderblush", StyleSheetColor.HexToColor32(16773365U));
			dictionary.Add("lavender", StyleSheetColor.HexToColor32(15132410U));
			dictionary.Add("lawngreen", StyleSheetColor.HexToColor32(8190976U));
			dictionary.Add("lemonchiffon", StyleSheetColor.HexToColor32(16775885U));
			dictionary.Add("lightblue", StyleSheetColor.HexToColor32(11393254U));
			dictionary.Add("lightcoral", StyleSheetColor.HexToColor32(15761536U));
			dictionary.Add("lightcyan", StyleSheetColor.HexToColor32(14745599U));
			dictionary.Add("lightgoldenrodyellow", StyleSheetColor.HexToColor32(16448210U));
			dictionary.Add("lightgray", StyleSheetColor.HexToColor32(13882323U));
			dictionary.Add("lightgreen", StyleSheetColor.HexToColor32(9498256U));
			dictionary.Add("lightgrey", StyleSheetColor.HexToColor32(13882323U));
			dictionary.Add("lightpink", StyleSheetColor.HexToColor32(16758465U));
			dictionary.Add("lightsalmon", StyleSheetColor.HexToColor32(16752762U));
			dictionary.Add("lightseagreen", StyleSheetColor.HexToColor32(2142890U));
			dictionary.Add("lightskyblue", StyleSheetColor.HexToColor32(8900346U));
			dictionary.Add("lightslategray", StyleSheetColor.HexToColor32(7833753U));
			dictionary.Add("lightslategrey", StyleSheetColor.HexToColor32(7833753U));
			dictionary.Add("lightsteelblue", StyleSheetColor.HexToColor32(11584734U));
			dictionary.Add("lightyellow", StyleSheetColor.HexToColor32(16777184U));
			dictionary.Add("lime", StyleSheetColor.HexToColor32(65280U));
			dictionary.Add("limegreen", StyleSheetColor.HexToColor32(3329330U));
			dictionary.Add("linen", StyleSheetColor.HexToColor32(16445670U));
			dictionary.Add("magenta", StyleSheetColor.HexToColor32(16711935U));
			dictionary.Add("maroon", StyleSheetColor.HexToColor32(8388608U));
			dictionary.Add("mediumaquamarine", StyleSheetColor.HexToColor32(6737322U));
			dictionary.Add("mediumblue", StyleSheetColor.HexToColor32(205U));
			dictionary.Add("mediumorchid", StyleSheetColor.HexToColor32(12211667U));
			dictionary.Add("mediumpurple", StyleSheetColor.HexToColor32(9662683U));
			dictionary.Add("mediumseagreen", StyleSheetColor.HexToColor32(3978097U));
			dictionary.Add("mediumslateblue", StyleSheetColor.HexToColor32(8087790U));
			dictionary.Add("mediumspringgreen", StyleSheetColor.HexToColor32(64154U));
			dictionary.Add("mediumturquoise", StyleSheetColor.HexToColor32(4772300U));
			dictionary.Add("mediumvioletred", StyleSheetColor.HexToColor32(13047173U));
			dictionary.Add("midnightblue", StyleSheetColor.HexToColor32(1644912U));
			dictionary.Add("mintcream", StyleSheetColor.HexToColor32(16121850U));
			dictionary.Add("mistyrose", StyleSheetColor.HexToColor32(16770273U));
			dictionary.Add("moccasin", StyleSheetColor.HexToColor32(16770229U));
			dictionary.Add("navajowhite", StyleSheetColor.HexToColor32(16768685U));
			dictionary.Add("navy", StyleSheetColor.HexToColor32(128U));
			dictionary.Add("oldlace", StyleSheetColor.HexToColor32(16643558U));
			dictionary.Add("olive", StyleSheetColor.HexToColor32(8421376U));
			dictionary.Add("olivedrab", StyleSheetColor.HexToColor32(7048739U));
			dictionary.Add("orange", StyleSheetColor.HexToColor32(16753920U));
			dictionary.Add("orangered", StyleSheetColor.HexToColor32(16729344U));
			dictionary.Add("orchid", StyleSheetColor.HexToColor32(14315734U));
			dictionary.Add("palegoldenrod", StyleSheetColor.HexToColor32(15657130U));
			dictionary.Add("palegreen", StyleSheetColor.HexToColor32(10025880U));
			dictionary.Add("paleturquoise", StyleSheetColor.HexToColor32(11529966U));
			dictionary.Add("palevioletred", StyleSheetColor.HexToColor32(14381203U));
			dictionary.Add("papayawhip", StyleSheetColor.HexToColor32(16773077U));
			dictionary.Add("peachpuff", StyleSheetColor.HexToColor32(16767673U));
			dictionary.Add("peru", StyleSheetColor.HexToColor32(13468991U));
			dictionary.Add("pink", StyleSheetColor.HexToColor32(16761035U));
			dictionary.Add("plum", StyleSheetColor.HexToColor32(14524637U));
			dictionary.Add("powderblue", StyleSheetColor.HexToColor32(11591910U));
			dictionary.Add("purple", StyleSheetColor.HexToColor32(8388736U));
			dictionary.Add("rebeccapurple", StyleSheetColor.HexToColor32(6697881U));
			dictionary.Add("red", StyleSheetColor.HexToColor32(16711680U));
			dictionary.Add("rosybrown", StyleSheetColor.HexToColor32(12357519U));
			dictionary.Add("royalblue", StyleSheetColor.HexToColor32(4286945U));
			dictionary.Add("saddlebrown", StyleSheetColor.HexToColor32(9127187U));
			dictionary.Add("salmon", StyleSheetColor.HexToColor32(16416882U));
			dictionary.Add("sandybrown", StyleSheetColor.HexToColor32(16032864U));
			dictionary.Add("seagreen", StyleSheetColor.HexToColor32(3050327U));
			dictionary.Add("seashell", StyleSheetColor.HexToColor32(16774638U));
			dictionary.Add("sienna", StyleSheetColor.HexToColor32(10506797U));
			dictionary.Add("silver", StyleSheetColor.HexToColor32(12632256U));
			dictionary.Add("skyblue", StyleSheetColor.HexToColor32(8900331U));
			dictionary.Add("slateblue", StyleSheetColor.HexToColor32(6970061U));
			dictionary.Add("slategray", StyleSheetColor.HexToColor32(7372944U));
			dictionary.Add("slategrey", StyleSheetColor.HexToColor32(7372944U));
			dictionary.Add("snow", StyleSheetColor.HexToColor32(16775930U));
			dictionary.Add("springgreen", StyleSheetColor.HexToColor32(65407U));
			dictionary.Add("steelblue", StyleSheetColor.HexToColor32(4620980U));
			dictionary.Add("tan", StyleSheetColor.HexToColor32(13808780U));
			dictionary.Add("teal", StyleSheetColor.HexToColor32(32896U));
			dictionary.Add("thistle", StyleSheetColor.HexToColor32(14204888U));
			dictionary.Add("tomato", StyleSheetColor.HexToColor32(16737095U));
			dictionary.Add("transparent", new Color32(0, 0, 0, 0));
			dictionary.Add("turquoise", StyleSheetColor.HexToColor32(4251856U));
			dictionary.Add("violet", StyleSheetColor.HexToColor32(15631086U));
			dictionary.Add("wheat", StyleSheetColor.HexToColor32(16113331U));
			dictionary.Add("white", StyleSheetColor.HexToColor32(16777215U));
			dictionary.Add("whitesmoke", StyleSheetColor.HexToColor32(16119285U));
			dictionary.Add("yellow", StyleSheetColor.HexToColor32(16776960U));
			dictionary.Add("yellowgreen", StyleSheetColor.HexToColor32(10145074U));
			StyleSheetColor.s_NameToColor = dictionary;
		}

		// Token: 0x04000DDC RID: 3548
		private static Dictionary<string, Color32> s_NameToColor;
	}
}

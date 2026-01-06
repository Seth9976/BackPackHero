using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000352 RID: 850
	internal static class StylePropertyCache
	{
		// Token: 0x06001B74 RID: 7028 RVA: 0x0007DCD8 File Offset: 0x0007BED8
		public static bool TryGetSyntax(string name, out string syntax)
		{
			return StylePropertyCache.s_PropertySyntaxCache.TryGetValue(name, ref syntax);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0007DCF8 File Offset: 0x0007BEF8
		public static bool TryGetNonTerminalValue(string name, out string syntax)
		{
			return StylePropertyCache.s_NonTerminalValues.TryGetValue(name, ref syntax);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0007DD18 File Offset: 0x0007BF18
		public static string FindClosestPropertyName(string name)
		{
			float num = float.MaxValue;
			string text = null;
			foreach (string text2 in StylePropertyCache.s_PropertySyntaxCache.Keys)
			{
				float num2 = 1f;
				bool flag = text2.Contains(name);
				if (flag)
				{
					num2 = 0.1f;
				}
				float num3 = (float)StringUtils.LevenshteinDistance(name, text2) * num2;
				bool flag2 = num3 < num;
				if (flag2)
				{
					num = num3;
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0007DDB8 File Offset: 0x0007BFB8
		// Note: this type is marked as 'beforefieldinit'.
		static StylePropertyCache()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("align-content", "flex-start | flex-end | center | stretch | auto");
			dictionary.Add("align-items", "flex-start | flex-end | center | stretch | auto");
			dictionary.Add("align-self", "flex-start | flex-end | center | stretch | auto");
			dictionary.Add("all", "initial");
			dictionary.Add("background-color", "<color>");
			dictionary.Add("background-image", "<resource> | <url> | none");
			dictionary.Add("border-bottom-color", "<color>");
			dictionary.Add("border-bottom-left-radius", "<length-percentage>");
			dictionary.Add("border-bottom-right-radius", "<length-percentage>");
			dictionary.Add("border-bottom-width", "<length>");
			dictionary.Add("border-color", "<color>{1,4}");
			dictionary.Add("border-left-color", "<color>");
			dictionary.Add("border-left-width", "<length>");
			dictionary.Add("border-radius", "[ <length-percentage> ]{1,4}");
			dictionary.Add("border-right-color", "<color>");
			dictionary.Add("border-right-width", "<length>");
			dictionary.Add("border-top-color", "<color>");
			dictionary.Add("border-top-left-radius", "<length-percentage>");
			dictionary.Add("border-top-right-radius", "<length-percentage>");
			dictionary.Add("border-top-width", "<length>");
			dictionary.Add("border-width", "<length>{1,4}");
			dictionary.Add("bottom", "<length-percentage> | auto");
			dictionary.Add("color", "<color>");
			dictionary.Add("cursor", "[ [ <resource> | <url> ] [ <integer> <integer> ]? ] | [ arrow | text | resize-vertical | resize-horizontal | link | slide-arrow | resize-up-right | resize-up-left | move-arrow | rotate-arrow | scale-arrow | arrow-plus | arrow-minus | pan | orbit | zoom | fps | split-resize-up-down | split-resize-left-right ]");
			dictionary.Add("display", "flex | none");
			dictionary.Add("flex", "none | [ <'flex-grow'> <'flex-shrink'>? || <'flex-basis'> ]");
			dictionary.Add("flex-basis", "<'width'>");
			dictionary.Add("flex-direction", "column | row | column-reverse | row-reverse");
			dictionary.Add("flex-grow", "<number>");
			dictionary.Add("flex-shrink", "<number>");
			dictionary.Add("flex-wrap", "nowrap | wrap | wrap-reverse");
			dictionary.Add("font-size", "<length-percentage>");
			dictionary.Add("height", "<length-percentage> | auto");
			dictionary.Add("justify-content", "flex-start | flex-end | center | space-between | space-around");
			dictionary.Add("left", "<length-percentage> | auto");
			dictionary.Add("letter-spacing", "<length>");
			dictionary.Add("margin", "[ <length-percentage> | auto ]{1,4}");
			dictionary.Add("margin-bottom", "<length-percentage> | auto");
			dictionary.Add("margin-left", "<length-percentage> | auto");
			dictionary.Add("margin-right", "<length-percentage> | auto");
			dictionary.Add("margin-top", "<length-percentage> | auto");
			dictionary.Add("max-height", "<length-percentage> | none");
			dictionary.Add("max-width", "<length-percentage> | none");
			dictionary.Add("min-height", "<length-percentage> | auto");
			dictionary.Add("min-width", "<length-percentage> | auto");
			dictionary.Add("opacity", "<number>");
			dictionary.Add("overflow", "visible | hidden | scroll");
			dictionary.Add("padding", "[ <length-percentage> ]{1,4}");
			dictionary.Add("padding-bottom", "<length-percentage>");
			dictionary.Add("padding-left", "<length-percentage>");
			dictionary.Add("padding-right", "<length-percentage>");
			dictionary.Add("padding-top", "<length-percentage>");
			dictionary.Add("position", "relative | absolute");
			dictionary.Add("right", "<length-percentage> | auto");
			dictionary.Add("rotate", "none | <angle>");
			dictionary.Add("scale", "none | <number>{1,3}");
			dictionary.Add("text-overflow", "clip | ellipsis");
			dictionary.Add("text-shadow", "<length>{2,3} && <color>?");
			dictionary.Add("top", "<length-percentage> | auto");
			dictionary.Add("transform-origin", "[ <length> | <percentage> | left | center | right | top | bottom ] | [ [ <length> | <percentage>  | left | center | right ] && [ <length> | <percentage>  | top | center | bottom ] ] <length>?");
			dictionary.Add("transition", "<single-transition>#");
			dictionary.Add("transition-delay", "<time>#");
			dictionary.Add("transition-duration", "<time>#");
			dictionary.Add("transition-property", "none | <single-transition-property>#");
			dictionary.Add("transition-timing-function", "<easing-function>#");
			dictionary.Add("translate", "none | [<length> | <percentage>] [ [<length> | <percentage>] <length>? ]?");
			dictionary.Add("-unity-background-image-tint-color", "<color>");
			dictionary.Add("-unity-background-scale-mode", "stretch-to-fill | scale-and-crop | scale-to-fit");
			dictionary.Add("-unity-font", "<resource> | <url>");
			dictionary.Add("-unity-font-definition", "<resource> | <url>");
			dictionary.Add("-unity-font-style", "normal | italic | bold | bold-and-italic");
			dictionary.Add("-unity-overflow-clip-box", "padding-box | content-box");
			dictionary.Add("-unity-paragraph-spacing", "<length>");
			dictionary.Add("-unity-slice-bottom", "<integer>");
			dictionary.Add("-unity-slice-left", "<integer>");
			dictionary.Add("-unity-slice-right", "<integer>");
			dictionary.Add("-unity-slice-top", "<integer>");
			dictionary.Add("-unity-text-align", "upper-left | middle-left | lower-left | upper-center | middle-center | lower-center | upper-right | middle-right | lower-right");
			dictionary.Add("-unity-text-outline", "<length> || <color>");
			dictionary.Add("-unity-text-outline-color", "<color>");
			dictionary.Add("-unity-text-outline-width", "<length>");
			dictionary.Add("-unity-text-overflow-position", "start | middle | end");
			dictionary.Add("visibility", "visible | hidden");
			dictionary.Add("white-space", "normal | nowrap");
			dictionary.Add("width", "<length-percentage> | auto");
			dictionary.Add("word-spacing", "<length>");
			StylePropertyCache.s_PropertySyntaxCache = dictionary;
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			dictionary2.Add("easing-function", "linear | <timing-function>");
			dictionary2.Add("length-percentage", "<length> | <percentage>");
			dictionary2.Add("single-transition", "[ none | <single-transition-property> ] || <time> || <easing-function> || <time>");
			dictionary2.Add("single-transition-property", "all | <custom-ident>");
			dictionary2.Add("timing-function", "ease | ease-in | ease-out | ease-in-out | ease-in-sine | ease-out-sine | ease-in-out-sine | ease-in-cubic | ease-out-cubic | ease-in-out-cubic | ease-in-circ | ease-out-circ | ease-in-out-circ | ease-in-elastic | ease-out-elastic | ease-in-out-elastic | ease-in-back | ease-out-back | ease-in-out-back | ease-in-bounce | ease-out-bounce | ease-in-out-bounce");
			StylePropertyCache.s_NonTerminalValues = dictionary2;
		}

		// Token: 0x04000D21 RID: 3361
		internal static readonly Dictionary<string, string> s_PropertySyntaxCache;

		// Token: 0x04000D22 RID: 3362
		internal static readonly Dictionary<string, string> s_NonTerminalValues;
	}
}

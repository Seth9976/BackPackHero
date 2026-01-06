using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unity.VisualScripting.AssemblyQualifiedNameParser
{
	// Token: 0x020001B3 RID: 435
	public class ParsedAssemblyQualifiedName
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00031630 File Offset: 0x0002F830
		public string AssemblyDescriptionString { get; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00031638 File Offset: 0x0002F838
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x00031640 File Offset: 0x0002F840
		public string TypeName { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00031649 File Offset: 0x0002F849
		public string ShortAssemblyName { get; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00031651 File Offset: 0x0002F851
		public string Version { get; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00031659 File Offset: 0x0002F859
		public string Culture { get; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00031661 File Offset: 0x0002F861
		public string PublicKeyToken { get; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00031669 File Offset: 0x0002F869
		public List<ParsedAssemblyQualifiedName> GenericParameters { get; } = new List<ParsedAssemblyQualifiedName>();

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00031671 File Offset: 0x0002F871
		public int GenericParameterCount { get; }

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0003167C File Offset: 0x0002F87C
		public ParsedAssemblyQualifiedName(string AssemblyQualifiedName)
		{
			int num = AssemblyQualifiedName.Length;
			bool flag = false;
			ParsedAssemblyQualifiedName.Block block = new ParsedAssemblyQualifiedName.Block();
			int num2 = 0;
			ParsedAssemblyQualifiedName.Block block2 = block;
			for (int i = 0; i < AssemblyQualifiedName.Length; i++)
			{
				char c = AssemblyQualifiedName[i];
				if (c == '[')
				{
					if (AssemblyQualifiedName[i + 1] == ']')
					{
						i++;
					}
					else
					{
						if (num2 == 0)
						{
							num = i;
						}
						num2++;
						ParsedAssemblyQualifiedName.Block block3 = new ParsedAssemblyQualifiedName.Block
						{
							startIndex = i + 1,
							level = num2,
							parentBlock = block2
						};
						block2.innerBlocks.Add(block3);
						block2 = block3;
					}
				}
				else if (c == ']')
				{
					block2.endIndex = i - 1;
					if (AssemblyQualifiedName[block2.startIndex] != '[')
					{
						block2.parsedAssemblyQualifiedName = new ParsedAssemblyQualifiedName(AssemblyQualifiedName.Substring(block2.startIndex, i - block2.startIndex));
						if (num2 == 2)
						{
							this.GenericParameters.Add(block2.parsedAssemblyQualifiedName);
						}
					}
					block2 = block2.parentBlock;
					num2--;
				}
				else if (num2 == 0 && c == ',')
				{
					num = i;
					flag = true;
					break;
				}
			}
			this.TypeName = AssemblyQualifiedName.Substring(0, num);
			int num3 = this.TypeName.IndexOf('`');
			if (num3 >= 0)
			{
				this.TypeName = this.TypeName.Substring(0, num3);
				this.GenericParameterCount = this.GenericParameters.Count;
			}
			if (flag)
			{
				this.AssemblyDescriptionString = AssemblyQualifiedName.Substring(num + 2);
				List<string> list = (from x in this.AssemblyDescriptionString.Split(',', StringSplitOptions.None)
					select x.Trim()).ToList<string>();
				this.Version = ParsedAssemblyQualifiedName.LookForPairThenRemove(list, "Version");
				this.Culture = ParsedAssemblyQualifiedName.LookForPairThenRemove(list, "Culture");
				this.PublicKeyToken = ParsedAssemblyQualifiedName.LookForPairThenRemove(list, "PublicKeyToken");
				if (list.Count > 0)
				{
					this.ShortAssemblyName = list[0];
				}
			}
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00031888 File Offset: 0x0002FA88
		private static string LookForPairThenRemove(List<string> strings, string Name)
		{
			for (int i = 0; i < strings.Count; i++)
			{
				string text = strings[i];
				if (text.IndexOf(Name) == 0)
				{
					int num = text.IndexOf('=');
					if (num > 0)
					{
						string text2 = text.Substring(num + 1);
						strings.RemoveAt(i);
						return text2;
					}
				}
			}
			return null;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000318D8 File Offset: 0x0002FAD8
		public void Replace(string oldTypeName, string newTypeName)
		{
			if (this.TypeName == oldTypeName)
			{
				this.TypeName = newTypeName;
			}
			foreach (ParsedAssemblyQualifiedName parsedAssemblyQualifiedName in this.GenericParameters)
			{
				parsedAssemblyQualifiedName.Replace(oldTypeName, newTypeName);
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00031940 File Offset: 0x0002FB40
		private string ToString(bool includeAssemblyDescription)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.TypeName);
			if (this.GenericParameters.Count > 0)
			{
				stringBuilder.Append("`");
				stringBuilder.Append(this.GenericParameterCount);
				stringBuilder.Append("[[");
				foreach (ParsedAssemblyQualifiedName parsedAssemblyQualifiedName in this.GenericParameters)
				{
					stringBuilder.Append(parsedAssemblyQualifiedName.ToString(true));
				}
				stringBuilder.Append("]]");
			}
			if (includeAssemblyDescription)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(this.AssemblyDescriptionString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00031A10 File Offset: 0x0002FC10
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x02000225 RID: 549
		private class Block
		{
			// Token: 0x040009EA RID: 2538
			internal int startIndex;

			// Token: 0x040009EB RID: 2539
			internal int endIndex;

			// Token: 0x040009EC RID: 2540
			internal int level;

			// Token: 0x040009ED RID: 2541
			internal ParsedAssemblyQualifiedName.Block parentBlock;

			// Token: 0x040009EE RID: 2542
			internal readonly List<ParsedAssemblyQualifiedName.Block> innerBlocks = new List<ParsedAssemblyQualifiedName.Block>();

			// Token: 0x040009EF RID: 2543
			internal ParsedAssemblyQualifiedName parsedAssemblyQualifiedName;
		}
	}
}

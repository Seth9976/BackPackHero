using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200035B RID: 859
	[ConfigurationCollection(typeof(CompilerProviderOption), CollectionType = ConfigurationElementCollectionType.BasicMap, AddItemName = "providerOption")]
	internal sealed class CompilerProviderOptionsCollection : ConfigurationElementCollection
	{
		// Token: 0x06001C80 RID: 7296 RVA: 0x0006734E File Offset: 0x0006554E
		protected override ConfigurationElement CreateNewElement()
		{
			return new CompilerProviderOption();
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00067355 File Offset: 0x00065555
		public CompilerProviderOption Get(int index)
		{
			return (CompilerProviderOption)base.BaseGet(index);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00067363 File Offset: 0x00065563
		public CompilerProviderOption Get(string name)
		{
			return (CompilerProviderOption)base.BaseGet(name);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x00067371 File Offset: 0x00065571
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CompilerProviderOption)element).Name;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0006720A File Offset: 0x0006540A
		public string GetKey(int index)
		{
			return (string)base.BaseGetKey(index);
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00067380 File Offset: 0x00065580
		public string[] AllKeys
		{
			get
			{
				int count = base.Count;
				string[] array = new string[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this[i].Name;
				}
				return array;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x000673B7 File Offset: 0x000655B7
		protected override string ElementName
		{
			get
			{
				return "providerOption";
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x000673BE File Offset: 0x000655BE
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerProviderOptionsCollection.properties;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x000673C8 File Offset: 0x000655C8
		public Dictionary<string, string> ProviderOptions
		{
			get
			{
				int count = base.Count;
				if (count == 0)
				{
					return null;
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>(count);
				for (int i = 0; i < count; i++)
				{
					CompilerProviderOption compilerProviderOption = this[i];
					dictionary.Add(compilerProviderOption.Name, compilerProviderOption.Value);
				}
				return dictionary;
			}
		}

		// Token: 0x170005C4 RID: 1476
		public CompilerProviderOption this[int index]
		{
			get
			{
				return (CompilerProviderOption)base.BaseGet(index);
			}
		}

		// Token: 0x170005C5 RID: 1477
		public CompilerProviderOption this[string name]
		{
			get
			{
				foreach (object obj in this)
				{
					CompilerProviderOption compilerProviderOption = (CompilerProviderOption)obj;
					if (compilerProviderOption.Name == name)
					{
						return compilerProviderOption;
					}
				}
				return null;
			}
		}

		// Token: 0x04000E86 RID: 3718
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200021B RID: 539
	[ConfigurationCollection(typeof(ListenerElement), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SharedListenerElementsCollection : ListenerElementsCollection
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00003062 File Offset: 0x00001262
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00045888 File Offset: 0x00043A88
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement(false);
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00045890 File Offset: 0x00043A90
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}
	}
}

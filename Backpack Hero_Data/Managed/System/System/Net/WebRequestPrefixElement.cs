using System;
using System.Globalization;
using System.Reflection;

namespace System.Net
{
	// Token: 0x02000400 RID: 1024
	internal class WebRequestPrefixElement
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x00077F88 File Offset: 0x00076188
		// (set) Token: 0x060020D8 RID: 8408 RVA: 0x00078008 File Offset: 0x00076208
		public IWebRequestCreate Creator
		{
			get
			{
				if (this.creator == null && this.creatorType != null)
				{
					lock (this)
					{
						if (this.creator == null)
						{
							this.creator = (IWebRequestCreate)Activator.CreateInstance(this.creatorType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[0], CultureInfo.InvariantCulture);
						}
					}
				}
				return this.creator;
			}
			set
			{
				this.creator = value;
			}
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x00078014 File Offset: 0x00076214
		public WebRequestPrefixElement(string P, Type creatorType)
		{
			if (!typeof(IWebRequestCreate).IsAssignableFrom(creatorType))
			{
				throw new InvalidCastException(SR.GetString("Invalid cast from {0} to {1}.", new object[] { creatorType.AssemblyQualifiedName, "IWebRequestCreate" }));
			}
			this.Prefix = P;
			this.creatorType = creatorType;
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x0007806E File Offset: 0x0007626E
		public WebRequestPrefixElement(string P, IWebRequestCreate C)
		{
			this.Prefix = P;
			this.Creator = C;
		}

		// Token: 0x04001285 RID: 4741
		public string Prefix;

		// Token: 0x04001286 RID: 4742
		internal IWebRequestCreate creator;

		// Token: 0x04001287 RID: 4743
		internal Type creatorType;
	}
}

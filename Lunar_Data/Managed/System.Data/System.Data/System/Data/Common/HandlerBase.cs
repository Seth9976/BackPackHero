using System;
using System.Globalization;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000389 RID: 905
	internal static class HandlerBase
	{
		// Token: 0x06002B68 RID: 11112 RVA: 0x000BFC99 File Offset: 0x000BDE99
		internal static void CheckForChildNodes(XmlNode node)
		{
			if (node.HasChildNodes)
			{
				throw ADP.ConfigBaseNoChildNodes(node.FirstChild);
			}
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000BFCAF File Offset: 0x000BDEAF
		private static void CheckForNonElement(XmlNode node)
		{
			if (XmlNodeType.Element != node.NodeType)
			{
				throw ADP.ConfigBaseElementsOnly(node);
			}
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000BFCC1 File Offset: 0x000BDEC1
		internal static void CheckForUnrecognizedAttributes(XmlNode node)
		{
			if (node.Attributes.Count != 0)
			{
				throw ADP.ConfigUnrecognizedAttributes(node);
			}
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x000BFCD7 File Offset: 0x000BDED7
		internal static bool IsIgnorableAlsoCheckForNonElement(XmlNode node)
		{
			if (XmlNodeType.Comment == node.NodeType || XmlNodeType.Whitespace == node.NodeType)
			{
				return true;
			}
			HandlerBase.CheckForNonElement(node);
			return false;
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x000BFCF8 File Offset: 0x000BDEF8
		internal static string RemoveAttribute(XmlNode node, string name, bool required, bool allowEmpty)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode == null)
			{
				if (required)
				{
					throw ADP.ConfigRequiredAttributeMissing(name, node);
				}
				return null;
			}
			else
			{
				string value = xmlNode.Value;
				if (!allowEmpty && value.Length == 0)
				{
					throw ADP.ConfigRequiredAttributeEmpty(name, node);
				}
				return value;
			}
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000BFD3D File Offset: 0x000BDF3D
		internal static DataSet CloneParent(DataSet parentConfig, bool insenstive)
		{
			if (parentConfig == null)
			{
				parentConfig = new DataSet("system.data");
				parentConfig.CaseSensitive = !insenstive;
				parentConfig.Locale = CultureInfo.InvariantCulture;
			}
			else
			{
				parentConfig = parentConfig.Copy();
			}
			return parentConfig;
		}
	}
}

using System;
using System.Data.SqlClient;
using System.Reflection;

namespace System.Data.Common
{
	// Token: 0x0200037A RID: 890
	internal static class DbConnectionStringBuilderUtil
	{
		// Token: 0x06002B11 RID: 11025 RVA: 0x000BE288 File Offset: 0x000BC488
		internal static bool ConvertToBoolean(object value)
		{
			string text = value as string;
			if (text == null)
			{
				bool flag;
				try
				{
					flag = Convert.ToBoolean(value);
				}
				catch (InvalidCastException ex)
				{
					throw ADP.ConvertFailed(value.GetType(), typeof(bool), ex);
				}
				return flag;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "true") || StringComparer.OrdinalIgnoreCase.Equals(text, "yes"))
			{
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "false") || StringComparer.OrdinalIgnoreCase.Equals(text, "no"))
			{
				return false;
			}
			string text2 = text.Trim();
			return StringComparer.OrdinalIgnoreCase.Equals(text2, "true") || StringComparer.OrdinalIgnoreCase.Equals(text2, "yes") || (!StringComparer.OrdinalIgnoreCase.Equals(text2, "false") && !StringComparer.OrdinalIgnoreCase.Equals(text2, "no") && bool.Parse(text));
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000BE37C File Offset: 0x000BC57C
		internal static bool ConvertToIntegratedSecurity(object value)
		{
			string text = value as string;
			if (text == null)
			{
				bool flag;
				try
				{
					flag = Convert.ToBoolean(value);
				}
				catch (InvalidCastException ex)
				{
					throw ADP.ConvertFailed(value.GetType(), typeof(bool), ex);
				}
				return flag;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "sspi") || StringComparer.OrdinalIgnoreCase.Equals(text, "true") || StringComparer.OrdinalIgnoreCase.Equals(text, "yes"))
			{
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(text, "false") || StringComparer.OrdinalIgnoreCase.Equals(text, "no"))
			{
				return false;
			}
			string text2 = text.Trim();
			return StringComparer.OrdinalIgnoreCase.Equals(text2, "sspi") || StringComparer.OrdinalIgnoreCase.Equals(text2, "true") || StringComparer.OrdinalIgnoreCase.Equals(text2, "yes") || (!StringComparer.OrdinalIgnoreCase.Equals(text2, "false") && !StringComparer.OrdinalIgnoreCase.Equals(text2, "no") && bool.Parse(text));
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000BE494 File Offset: 0x000BC694
		internal static int ConvertToInt32(object value)
		{
			int num;
			try
			{
				num = Convert.ToInt32(value);
			}
			catch (InvalidCastException ex)
			{
				throw ADP.ConvertFailed(value.GetType(), typeof(int), ex);
			}
			return num;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000BE4D4 File Offset: 0x000BC6D4
		internal static string ConvertToString(object value)
		{
			string text;
			try
			{
				text = Convert.ToString(value);
			}
			catch (InvalidCastException ex)
			{
				throw ADP.ConvertFailed(value.GetType(), typeof(string), ex);
			}
			return text;
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000BE514 File Offset: 0x000BC714
		internal static bool TryConvertToApplicationIntent(string value, out ApplicationIntent result)
		{
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "ReadOnly"))
			{
				result = ApplicationIntent.ReadOnly;
				return true;
			}
			if (StringComparer.OrdinalIgnoreCase.Equals(value, "ReadWrite"))
			{
				result = ApplicationIntent.ReadWrite;
				return true;
			}
			result = ApplicationIntent.ReadWrite;
			return false;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000BE548 File Offset: 0x000BC748
		internal static bool IsValidApplicationIntentValue(ApplicationIntent value)
		{
			return value == ApplicationIntent.ReadOnly || value == ApplicationIntent.ReadWrite;
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000BE554 File Offset: 0x000BC754
		internal static string ApplicationIntentToString(ApplicationIntent value)
		{
			if (value == ApplicationIntent.ReadOnly)
			{
				return "ReadOnly";
			}
			return "ReadWrite";
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000BE568 File Offset: 0x000BC768
		internal static ApplicationIntent ConvertToApplicationIntent(string keyword, object value)
		{
			string text = value as string;
			if (text != null)
			{
				ApplicationIntent applicationIntent;
				if (DbConnectionStringBuilderUtil.TryConvertToApplicationIntent(text, out applicationIntent))
				{
					return applicationIntent;
				}
				text = text.Trim();
				if (DbConnectionStringBuilderUtil.TryConvertToApplicationIntent(text, out applicationIntent))
				{
					return applicationIntent;
				}
				throw ADP.InvalidConnectionOptionValue(keyword);
			}
			else
			{
				ApplicationIntent applicationIntent2;
				if (value is ApplicationIntent)
				{
					applicationIntent2 = (ApplicationIntent)value;
				}
				else
				{
					if (value.GetType().GetTypeInfo().IsEnum)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(ApplicationIntent), null);
					}
					try
					{
						applicationIntent2 = (ApplicationIntent)Enum.ToObject(typeof(ApplicationIntent), value);
					}
					catch (ArgumentException ex)
					{
						throw ADP.ConvertFailed(value.GetType(), typeof(ApplicationIntent), ex);
					}
				}
				if (DbConnectionStringBuilderUtil.IsValidApplicationIntentValue(applicationIntent2))
				{
					return applicationIntent2;
				}
				throw ADP.InvalidEnumerationValue(typeof(ApplicationIntent), (int)applicationIntent2);
			}
		}

		// Token: 0x040019F5 RID: 6645
		private const string ApplicationIntentReadWriteString = "ReadWrite";

		// Token: 0x040019F6 RID: 6646
		private const string ApplicationIntentReadOnlyString = "ReadOnly";
	}
}

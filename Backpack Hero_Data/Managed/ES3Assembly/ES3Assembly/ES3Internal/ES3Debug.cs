using System;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000C2 RID: 194
	internal static class ES3Debug
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x0001E800 File Offset: 0x0001CA00
		public static void Log(string msg, Object context = null, int indent = 0)
		{
			if (!ES3Settings.defaultSettingsScriptableObject.logDebugInfo)
			{
				return;
			}
			if (context != null)
			{
				Debug.LogFormat(context, ES3Debug.Indent(indent) + msg + "\n<i>To disable these messages from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Info'</i>", Array.Empty<object>());
				return;
			}
			Debug.LogFormat(context, ES3Debug.Indent(indent) + msg, Array.Empty<object>());
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001E858 File Offset: 0x0001CA58
		public static void LogWarning(string msg, Object context = null, int indent = 0)
		{
			if (!ES3Settings.defaultSettingsScriptableObject.logWarnings)
			{
				return;
			}
			if (context != null)
			{
				Debug.LogWarningFormat(context, ES3Debug.Indent(indent) + msg + "\n<i>To disable warnings from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Warnings'</i>", Array.Empty<object>());
				return;
			}
			Debug.LogWarningFormat(context, ES3Debug.Indent(indent) + msg + "\n<i>To disable warnings from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Warnings'</i>", Array.Empty<object>());
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001E8B4 File Offset: 0x0001CAB4
		public static void LogError(string msg, Object context = null, int indent = 0)
		{
			if (!ES3Settings.defaultSettingsScriptableObject.logErrors)
			{
				return;
			}
			if (context != null)
			{
				Debug.LogErrorFormat(context, ES3Debug.Indent(indent) + msg + "\n<i>To disable these error messages from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Errors'</i>", Array.Empty<object>());
				return;
			}
			Debug.LogErrorFormat(context, ES3Debug.Indent(indent) + msg + "\n<i>To disable these error messages from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Errors'</i>", Array.Empty<object>());
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001E910 File Offset: 0x0001CB10
		private static string Indent(int size)
		{
			if (size < 0)
			{
				return "";
			}
			return new string('-', size);
		}

		// Token: 0x040000F8 RID: 248
		private const string disableInfoMsg = "\n<i>To disable these messages from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Info'</i>";

		// Token: 0x040000F9 RID: 249
		private const string disableWarningMsg = "\n<i>To disable warnings from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Warnings'</i>";

		// Token: 0x040000FA RID: 250
		private const string disableErrorMsg = "\n<i>To disable these error messages from Easy Save, go to Window > Easy Save 3 > Settings, and uncheck 'Log Errors'</i>";

		// Token: 0x040000FB RID: 251
		private const char indentChar = '-';
	}
}

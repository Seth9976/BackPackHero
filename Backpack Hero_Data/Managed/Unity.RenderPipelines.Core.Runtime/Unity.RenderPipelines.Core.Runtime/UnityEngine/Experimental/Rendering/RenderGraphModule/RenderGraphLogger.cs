using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000027 RID: 39
	internal class RenderGraphLogger
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00009E2C File Offset: 0x0000802C
		public void Initialize(string logName)
		{
			StringBuilder stringBuilder;
			if (!this.m_LogMap.TryGetValue(logName, out stringBuilder))
			{
				stringBuilder = new StringBuilder();
				this.m_LogMap.Add(logName, stringBuilder);
			}
			this.m_CurrentBuilder = stringBuilder;
			this.m_CurrentBuilder.Clear();
			this.m_CurrentIndentation = 0;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009E76 File Offset: 0x00008076
		public void IncrementIndentation(int value)
		{
			this.m_CurrentIndentation += Math.Abs(value);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00009E8B File Offset: 0x0000808B
		public void DecrementIndentation(int value)
		{
			this.m_CurrentIndentation = Math.Max(0, this.m_CurrentIndentation - Math.Abs(value));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00009EA8 File Offset: 0x000080A8
		public void LogLine(string format, params object[] args)
		{
			for (int i = 0; i < this.m_CurrentIndentation; i++)
			{
				this.m_CurrentBuilder.Append('\t');
			}
			this.m_CurrentBuilder.AppendFormat(format, args);
			this.m_CurrentBuilder.AppendLine();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00009EF0 File Offset: 0x000080F0
		public string GetLog(string logName)
		{
			StringBuilder stringBuilder;
			if (this.m_LogMap.TryGetValue(logName, out stringBuilder))
			{
				return stringBuilder.ToString();
			}
			return "";
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009F1C File Offset: 0x0000811C
		public string GetAllLogs()
		{
			string text = "";
			foreach (KeyValuePair<string, StringBuilder> keyValuePair in this.m_LogMap)
			{
				StringBuilder value = keyValuePair.Value;
				value.AppendLine();
				text += value.ToString();
			}
			return text;
		}

		// Token: 0x04000111 RID: 273
		private Dictionary<string, StringBuilder> m_LogMap = new Dictionary<string, StringBuilder>();

		// Token: 0x04000112 RID: 274
		private StringBuilder m_CurrentBuilder;

		// Token: 0x04000113 RID: 275
		private int m_CurrentIndentation;
	}
}

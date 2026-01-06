using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides the default output methods and behavior for tracing.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000252 RID: 594
	public class DefaultTraceListener : TraceListener
	{
		// Token: 0x06001245 RID: 4677 RVA: 0x0004E9C8 File Offset: 0x0004CBC8
		static DefaultTraceListener()
		{
			if (!DefaultTraceListener.OnWin32)
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_TRACE_LISTENER");
				if (environmentVariable != null)
				{
					string text;
					string text2;
					if (environmentVariable.StartsWith("Console.Out"))
					{
						text = "Console.Out";
						text2 = DefaultTraceListener.GetPrefix(environmentVariable, "Console.Out");
					}
					else if (environmentVariable.StartsWith("Console.Error"))
					{
						text = "Console.Error";
						text2 = DefaultTraceListener.GetPrefix(environmentVariable, "Console.Error");
					}
					else
					{
						text = environmentVariable;
						text2 = "";
					}
					DefaultTraceListener.MonoTraceFile = text;
					DefaultTraceListener.MonoTracePrefix = text2;
				}
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0004EA52 File Offset: 0x0004CC52
		private static string GetPrefix(string var, string target)
		{
			if (var.Length > target.Length)
			{
				return var.Substring(target.Length + 1);
			}
			return "";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DefaultTraceListener" /> class with "Default" as its <see cref="P:System.Diagnostics.TraceListener.Name" /> property value.</summary>
		// Token: 0x06001247 RID: 4679 RVA: 0x0004EA76 File Offset: 0x0004CC76
		public DefaultTraceListener()
			: base("Default")
		{
		}

		/// <summary>Gets or sets a value indicating whether the application is running in user-interface mode.</summary>
		/// <returns>true if user-interface mode is enabled; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x0004EA83 File Offset: 0x0004CC83
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x0004EA8B File Offset: 0x0004CC8B
		[MonoTODO("AssertUiEnabled defaults to False; should follow Environment.UserInteractive.")]
		public bool AssertUiEnabled
		{
			get
			{
				return this.assertUiEnabled;
			}
			set
			{
				this.assertUiEnabled = value;
			}
		}

		/// <summary>Gets or sets the name of a log file to write trace or debug messages to.</summary>
		/// <returns>The name of a log file to write trace or debug messages to.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0004EA94 File Offset: 0x0004CC94
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x0004EA9C File Offset: 0x0004CC9C
		[MonoTODO]
		public string LogFileName
		{
			get
			{
				return this.logFileName;
			}
			set
			{
				this.logFileName = value;
			}
		}

		/// <summary>Emits or displays a message and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600124C RID: 4684 RVA: 0x0004EAA5 File Offset: 0x0004CCA5
		public override void Fail(string message)
		{
			base.Fail(message);
		}

		/// <summary>Emits or displays detailed messages and a stack trace for an assertion that always fails.</summary>
		/// <param name="message">The message to emit or display. </param>
		/// <param name="detailMessage">The detailed message to emit or display. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600124D RID: 4685 RVA: 0x0004EAAE File Offset: 0x0004CCAE
		public override void Fail(string message, string detailMessage)
		{
			base.Fail(message, detailMessage);
			if (this.ProcessUI(message, detailMessage) == DefaultTraceListener.DialogResult.Abort)
			{
				Thread.CurrentThread.Abort();
			}
			this.WriteLine(new StackTrace().ToString());
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0004EAE0 File Offset: 0x0004CCE0
		private DefaultTraceListener.DialogResult ProcessUI(string message, string detailMessage)
		{
			if (!this.AssertUiEnabled)
			{
				return DefaultTraceListener.DialogResult.None;
			}
			object obj;
			MethodInfo method;
			try
			{
				Assembly assembly = Assembly.Load("System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
				if (assembly == null)
				{
					return DefaultTraceListener.DialogResult.None;
				}
				Type type = assembly.GetType("System.Windows.Forms.MessageBoxButtons");
				obj = Enum.Parse(type, "AbortRetryIgnore");
				method = assembly.GetType("System.Windows.Forms.MessageBox").GetMethod("Show", new Type[]
				{
					typeof(string),
					typeof(string),
					type
				});
			}
			catch
			{
				return DefaultTraceListener.DialogResult.None;
			}
			if (method == null || obj == null)
			{
				return DefaultTraceListener.DialogResult.None;
			}
			string text = string.Format("Assertion Failed: {0} to quit, {1} to debug, {2} to continue", "Abort", "Retry", "Ignore");
			string text2 = string.Format("{0}{1}{2}{1}{1}{3}", new object[]
			{
				message,
				Environment.NewLine,
				detailMessage,
				new StackTrace()
			});
			string text3 = method.Invoke(null, new object[] { text2, text, obj }).ToString();
			if (text3 == "Ignore")
			{
				return DefaultTraceListener.DialogResult.Ignore;
			}
			if (!(text3 == "Abort"))
			{
				return DefaultTraceListener.DialogResult.Retry;
			}
			return DefaultTraceListener.DialogResult.Abort;
		}

		// Token: 0x0600124F RID: 4687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void WriteWindowsDebugString(char* message);

		// Token: 0x06001250 RID: 4688 RVA: 0x0004EC20 File Offset: 0x0004CE20
		private unsafe void WriteDebugString(string message)
		{
			if (DefaultTraceListener.OnWin32)
			{
				fixed (string text = message)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					DefaultTraceListener.WriteWindowsDebugString(ptr);
				}
				return;
			}
			this.WriteMonoTrace(message);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004EC54 File Offset: 0x0004CE54
		private void WriteMonoTrace(string message)
		{
			string monoTraceFile = DefaultTraceListener.MonoTraceFile;
			if (monoTraceFile == "Console.Out")
			{
				Console.Out.Write(message);
				return;
			}
			if (!(monoTraceFile == "Console.Error"))
			{
				this.WriteLogFile(message, DefaultTraceListener.MonoTraceFile);
				return;
			}
			Console.Error.Write(message);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0004ECA7 File Offset: 0x0004CEA7
		private void WritePrefix()
		{
			if (!DefaultTraceListener.OnWin32)
			{
				this.WriteMonoTrace(DefaultTraceListener.MonoTracePrefix);
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0004ECBB File Offset: 0x0004CEBB
		private void WriteImpl(string message)
		{
			if (base.NeedIndent)
			{
				this.WriteIndent();
				this.WritePrefix();
			}
			if (Debugger.IsLogging())
			{
				Debugger.Log(0, null, message);
			}
			else
			{
				this.WriteDebugString(message);
			}
			this.WriteLogFile(message, this.LogFileName);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0004ECF8 File Offset: 0x0004CEF8
		private void WriteLogFile(string message, string logFile)
		{
			if (logFile != null && logFile.Length != 0)
			{
				FileInfo fileInfo = new FileInfo(logFile);
				StreamWriter streamWriter = null;
				try
				{
					if (fileInfo.Exists)
					{
						streamWriter = fileInfo.AppendText();
					}
					else
					{
						streamWriter = fileInfo.CreateText();
					}
				}
				catch
				{
					return;
				}
				using (streamWriter)
				{
					streamWriter.Write(message);
					streamWriter.Flush();
				}
			}
		}

		/// <summary>Writes the output to the OutputDebugString function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method.</summary>
		/// <param name="message">The message to write to OutputDebugString and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001255 RID: 4693 RVA: 0x0004ED70 File Offset: 0x0004CF70
		public override void Write(string message)
		{
			this.WriteImpl(message);
		}

		/// <summary>Writes the output to the OutputDebugString function and to the <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" /> method, followed by a carriage return and line feed (\r\n).</summary>
		/// <param name="message">The message to write to OutputDebugString and <see cref="M:System.Diagnostics.Debugger.Log(System.Int32,System.String,System.String)" />. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001256 RID: 4694 RVA: 0x0004ED7C File Offset: 0x0004CF7C
		public override void WriteLine(string message)
		{
			string text = message + Environment.NewLine;
			this.WriteImpl(text);
			base.NeedIndent = true;
		}

		// Token: 0x04000A98 RID: 2712
		private static readonly bool OnWin32 = Path.DirectorySeparatorChar == '\\';

		// Token: 0x04000A99 RID: 2713
		private const string ConsoleOutTrace = "Console.Out";

		// Token: 0x04000A9A RID: 2714
		private const string ConsoleErrorTrace = "Console.Error";

		// Token: 0x04000A9B RID: 2715
		private static readonly string MonoTracePrefix;

		// Token: 0x04000A9C RID: 2716
		private static readonly string MonoTraceFile;

		// Token: 0x04000A9D RID: 2717
		private string logFileName;

		// Token: 0x04000A9E RID: 2718
		private bool assertUiEnabled;

		// Token: 0x02000253 RID: 595
		private enum DialogResult
		{
			// Token: 0x04000AA0 RID: 2720
			None,
			// Token: 0x04000AA1 RID: 2721
			Retry,
			// Token: 0x04000AA2 RID: 2722
			Ignore,
			// Token: 0x04000AA3 RID: 2723
			Abort
		}
	}
}

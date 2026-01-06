using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x02000286 RID: 646
	internal class Win32EventLog : EventLogImpl
	{
		// Token: 0x0600146B RID: 5227 RVA: 0x0005327B File Offset: 0x0005147B
		public Win32EventLog(EventLog coreEventLog)
			: base(coreEventLog)
		{
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00003917 File Offset: 0x00001B17
		public override void BeginInit()
		{
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0005328F File Offset: 0x0005148F
		public override void Clear()
		{
			if (Win32EventLog.PInvoke.ClearEventLog(this.ReadHandle, null) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x000532AC File Offset: 0x000514AC
		public override void Close()
		{
			object eventLock = this._eventLock;
			lock (eventLock)
			{
				if (this._readHandle != IntPtr.Zero)
				{
					this.CloseEventLog(this._readHandle);
					this._readHandle = IntPtr.Zero;
				}
			}
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00053310 File Offset: 0x00051510
		public override void CreateEventSource(EventSourceCreationData sourceData)
		{
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(sourceData.MachineName, true))
			{
				if (eventLogKey == null)
				{
					throw new InvalidOperationException("EventLog registry key is missing.");
				}
				bool flag = false;
				RegistryKey registryKey = null;
				try
				{
					registryKey = eventLogKey.OpenSubKey(sourceData.LogName, true);
					if (registryKey == null)
					{
						base.ValidateCustomerLogName(sourceData.LogName, sourceData.MachineName);
						registryKey = eventLogKey.CreateSubKey(sourceData.LogName);
						registryKey.SetValue("Sources", new string[] { sourceData.LogName, sourceData.Source });
						Win32EventLog.UpdateLogRegistry(registryKey);
						using (RegistryKey registryKey2 = registryKey.CreateSubKey(sourceData.LogName))
						{
							Win32EventLog.UpdateSourceRegistry(registryKey2, sourceData);
						}
						flag = true;
					}
					if (sourceData.LogName != sourceData.Source)
					{
						if (!flag)
						{
							string[] array = (string[])registryKey.GetValue("Sources");
							if (array == null)
							{
								registryKey.SetValue("Sources", new string[] { sourceData.LogName, sourceData.Source });
							}
							else
							{
								bool flag2 = false;
								for (int i = 0; i < array.Length; i++)
								{
									if (array[i] == sourceData.Source)
									{
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									string[] array2 = new string[array.Length + 1];
									Array.Copy(array, 0, array2, 0, array.Length);
									array2[array.Length] = sourceData.Source;
									registryKey.SetValue("Sources", array2);
								}
							}
						}
						using (RegistryKey registryKey3 = registryKey.CreateSubKey(sourceData.Source))
						{
							Win32EventLog.UpdateSourceRegistry(registryKey3, sourceData);
						}
					}
				}
				finally
				{
					if (registryKey != null)
					{
						registryKey.Close();
					}
				}
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00053514 File Offset: 0x00051714
		public override void Delete(string logName, string machineName)
		{
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, true))
			{
				if (eventLogKey == null)
				{
					throw new InvalidOperationException("The event log key does not exist.");
				}
				using (RegistryKey registryKey = eventLogKey.OpenSubKey(logName, false))
				{
					if (registryKey == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' does not exist on computer '{1}'.", logName, machineName));
					}
					base.CoreEventLog.Clear();
					string text = (string)registryKey.GetValue("File");
					if (text != null)
					{
						try
						{
							File.Delete(text);
						}
						catch (Exception)
						{
						}
					}
				}
				eventLogKey.DeleteSubKeyTree(logName);
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x000535CC File Offset: 0x000517CC
		public override void DeleteEventSource(string source, string machineName)
		{
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, true))
			{
				if (registryKey == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The source '{0}' is not registered on computer '{1}'.", source, machineName));
				}
				registryKey.DeleteSubKeyTree(source);
				string[] array = (string[])registryKey.GetValue("Sources");
				if (array != null)
				{
					List<string> list = new List<string>();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != source)
						{
							list.Add(array[i]);
						}
					}
					string[] array2 = list.ToArray();
					registryKey.SetValue("Sources", array2);
				}
			}
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0005199F File Offset: 0x0004FB9F
		public override void Dispose(bool disposing)
		{
			this.Close();
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x00003917 File Offset: 0x00001B17
		public override void EndInit()
		{
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00053674 File Offset: 0x00051874
		public override bool Exists(string logName, string machineName)
		{
			bool flag;
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyByName(logName, machineName, false))
			{
				flag = registryKey != null;
			}
			return flag;
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x000536AC File Offset: 0x000518AC
		[MonoTODO]
		protected override string FormatMessage(string source, uint messageID, string[] replacementStrings)
		{
			string text = null;
			string[] messageResourceDlls = this.GetMessageResourceDlls(source, "EventMessageFile");
			for (int i = 0; i < messageResourceDlls.Length; i++)
			{
				text = Win32EventLog.FetchMessage(messageResourceDlls[i], messageID, replacementStrings);
				if (text != null)
				{
					break;
				}
			}
			if (text == null)
			{
				return string.Join(", ", replacementStrings);
			}
			return text;
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x000536F4 File Offset: 0x000518F4
		private string FormatCategory(string source, int category)
		{
			string text = null;
			string[] messageResourceDlls = this.GetMessageResourceDlls(source, "CategoryMessageFile");
			for (int i = 0; i < messageResourceDlls.Length; i++)
			{
				text = Win32EventLog.FetchMessage(messageResourceDlls[i], (uint)category, new string[0]);
				if (text != null)
				{
					break;
				}
			}
			if (text == null)
			{
				return "(" + category.ToString(CultureInfo.InvariantCulture) + ")";
			}
			return text;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00053754 File Offset: 0x00051954
		protected override int GetEntryCount()
		{
			int num = 0;
			if (Win32EventLog.PInvoke.GetNumberOfEventLogRecords(this.ReadHandle, ref num) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return num;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00053780 File Offset: 0x00051980
		protected override EventLogEntry GetEntry(int index)
		{
			index += this.OldestEventLogEntry;
			int num = 0;
			int num2 = 0;
			byte[] array = new byte[524287];
			this.ReadEventLog(index, array, ref num, ref num2);
			MemoryStream memoryStream = new MemoryStream(array);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			binaryReader.ReadBytes(8);
			int num3 = binaryReader.ReadInt32();
			int num4 = binaryReader.ReadInt32();
			int num5 = binaryReader.ReadInt32();
			uint num6 = binaryReader.ReadUInt32();
			int eventID = EventLog.GetEventID((long)((ulong)num6));
			short num7 = binaryReader.ReadInt16();
			short num8 = binaryReader.ReadInt16();
			short num9 = binaryReader.ReadInt16();
			binaryReader.ReadInt16();
			binaryReader.ReadInt32();
			int num10 = binaryReader.ReadInt32();
			int num11 = binaryReader.ReadInt32();
			int num12 = binaryReader.ReadInt32();
			int num13 = binaryReader.ReadInt32();
			int num14 = binaryReader.ReadInt32();
			DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds((double)num4);
			DateTime dateTime2 = new DateTime(1970, 1, 1).AddSeconds((double)num5);
			StringBuilder stringBuilder = new StringBuilder();
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text = stringBuilder.ToString();
			stringBuilder.Length = 0;
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text2 = stringBuilder.ToString();
			stringBuilder.Length = 0;
			while (binaryReader.PeekChar() != 0)
			{
				stringBuilder.Append(binaryReader.ReadChar());
			}
			binaryReader.ReadChar();
			string text3 = null;
			if (num11 != 0)
			{
				memoryStream.Position = (long)num12;
				byte[] array2 = binaryReader.ReadBytes(num11);
				text3 = Win32EventLog.LookupAccountSid(text2, array2);
			}
			memoryStream.Position = (long)num10;
			string[] array3 = new string[(int)num8];
			for (int i = 0; i < (int)num8; i++)
			{
				stringBuilder.Length = 0;
				while (binaryReader.PeekChar() != 0)
				{
					stringBuilder.Append(binaryReader.ReadChar());
				}
				binaryReader.ReadChar();
				array3[i] = stringBuilder.ToString();
			}
			byte[] array4 = new byte[num13];
			memoryStream.Position = (long)num14;
			binaryReader.Read(array4, 0, num13);
			string text4 = this.FormatMessage(text, num6, array3);
			return new EventLogEntry(this.FormatCategory(text, (int)num9), num9, num3, eventID, text, text4, text3, text2, (EventLogEntryType)num7, dateTime, dateTime2, array4, array3, (long)((ulong)num6));
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00051C64 File Offset: 0x0004FE64
		[MonoTODO]
		protected override string GetLogDisplayName()
		{
			return base.CoreEventLog.Log;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x000539EC File Offset: 0x00051BEC
		protected override string[] GetLogNames(string machineName)
		{
			string[] array;
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, true))
			{
				if (eventLogKey == null)
				{
					array = new string[0];
				}
				else
				{
					array = eventLogKey.GetSubKeyNames();
				}
			}
			return array;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00053A34 File Offset: 0x00051C34
		public override string LogNameFromSourceName(string source, string machineName)
		{
			string text;
			using (RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, false))
			{
				if (registryKey == null)
				{
					text = string.Empty;
				}
				else
				{
					text = Win32EventLog.GetLogName(registryKey);
				}
			}
			return text;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x00053A7C File Offset: 0x00051C7C
		public override bool SourceExists(string source, string machineName)
		{
			RegistryKey registryKey = Win32EventLog.FindLogKeyBySource(source, machineName, false);
			if (registryKey != null)
			{
				registryKey.Close();
				return true;
			}
			return false;
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00053AA0 File Offset: 0x00051CA0
		public override void WriteEntry(string[] replacementStrings, EventLogEntryType type, uint instanceID, short category, byte[] rawData)
		{
			IntPtr intPtr = this.RegisterEventSource();
			try
			{
				if (Win32EventLog.PInvoke.ReportEvent(intPtr, (ushort)type, (ushort)category, instanceID, IntPtr.Zero, (ushort)replacementStrings.Length, (uint)rawData.Length, replacementStrings, rawData) != 1)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				this.DeregisterEventSource(intPtr);
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00053AF8 File Offset: 0x00051CF8
		private static void UpdateLogRegistry(RegistryKey logKey)
		{
			if (logKey.GetValue("File") == null)
			{
				string logName = Win32EventLog.GetLogName(logKey);
				string text;
				if (logName.Length > 8)
				{
					text = logName.Substring(0, 8) + ".evt";
				}
				else
				{
					text = logName + ".evt";
				}
				string text2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "config");
				logKey.SetValue("File", Path.Combine(text2, text));
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00053B68 File Offset: 0x00051D68
		private static void UpdateSourceRegistry(RegistryKey sourceKey, EventSourceCreationData data)
		{
			if (data.CategoryCount > 0)
			{
				sourceKey.SetValue("CategoryCount", data.CategoryCount);
			}
			if (data.CategoryResourceFile != null && data.CategoryResourceFile.Length > 0)
			{
				sourceKey.SetValue("CategoryMessageFile", data.CategoryResourceFile);
			}
			if (data.MessageResourceFile != null && data.MessageResourceFile.Length > 0)
			{
				sourceKey.SetValue("EventMessageFile", data.MessageResourceFile);
			}
			if (data.ParameterResourceFile != null && data.ParameterResourceFile.Length > 0)
			{
				sourceKey.SetValue("ParameterMessageFile", data.ParameterResourceFile);
			}
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00053C09 File Offset: 0x00051E09
		private static string GetLogName(RegistryKey logKey)
		{
			string name = logKey.Name;
			return name.Substring(name.LastIndexOf("\\") + 1);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00053C24 File Offset: 0x00051E24
		private void ReadEventLog(int index, byte[] buffer, ref int bytesRead, ref int minBufferNeeded)
		{
			for (int i = 0; i < 3; i++)
			{
				if (Win32EventLog.PInvoke.ReadEventLog(this.ReadHandle, (Win32EventLog.ReadFlags)6, index, buffer, buffer.Length, ref bytesRead, ref minBufferNeeded) != 1)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (i >= 2)
					{
						throw new Win32Exception(lastWin32Error);
					}
					base.CoreEventLog.Reset();
				}
			}
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00053C72 File Offset: 0x00051E72
		[MonoTODO("Support remote machines")]
		private static RegistryKey GetEventLogKey(string machineName, bool writable)
		{
			return Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\EventLog", writable);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00053C84 File Offset: 0x00051E84
		private static RegistryKey FindSourceKeyByName(string source, string machineName, bool writable)
		{
			if (source == null || source.Length == 0)
			{
				return null;
			}
			RegistryKey registryKey = null;
			RegistryKey registryKey2;
			try
			{
				registryKey = Win32EventLog.GetEventLogKey(machineName, writable);
				if (registryKey == null)
				{
					registryKey2 = null;
				}
				else
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						using (RegistryKey registryKey3 = registryKey.OpenSubKey(subKeyNames[i], writable))
						{
							if (registryKey3 == null)
							{
								break;
							}
							RegistryKey registryKey4 = registryKey3.OpenSubKey(source, writable);
							if (registryKey4 != null)
							{
								return registryKey4;
							}
						}
					}
					registryKey2 = null;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return registryKey2;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00053D24 File Offset: 0x00051F24
		private static RegistryKey FindLogKeyByName(string logName, string machineName, bool writable)
		{
			RegistryKey registryKey;
			using (RegistryKey eventLogKey = Win32EventLog.GetEventLogKey(machineName, writable))
			{
				if (eventLogKey == null)
				{
					registryKey = null;
				}
				else
				{
					registryKey = eventLogKey.OpenSubKey(logName, writable);
				}
			}
			return registryKey;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00053D68 File Offset: 0x00051F68
		private static RegistryKey FindLogKeyBySource(string source, string machineName, bool writable)
		{
			if (source == null || source.Length == 0)
			{
				return null;
			}
			RegistryKey registryKey = null;
			RegistryKey registryKey2;
			try
			{
				registryKey = Win32EventLog.GetEventLogKey(machineName, writable);
				if (registryKey == null)
				{
					registryKey2 = null;
				}
				else
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						RegistryKey registryKey3 = null;
						try
						{
							RegistryKey registryKey4 = registryKey.OpenSubKey(subKeyNames[i], writable);
							if (registryKey4 != null)
							{
								registryKey3 = registryKey4.OpenSubKey(source, writable);
								if (registryKey3 != null)
								{
									return registryKey4;
								}
							}
						}
						finally
						{
							if (registryKey3 != null)
							{
								registryKey3.Close();
							}
						}
					}
					registryKey2 = null;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return registryKey2;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00053E08 File Offset: 0x00052008
		private int OldestEventLogEntry
		{
			get
			{
				int num = 0;
				if (Win32EventLog.PInvoke.GetOldestEventLogRecord(this.ReadHandle, ref num) != 1)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				return num;
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00053E33 File Offset: 0x00052033
		private void CloseEventLog(IntPtr hEventLog)
		{
			if (Win32EventLog.PInvoke.CloseEventLog(hEventLog) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00053E49 File Offset: 0x00052049
		private void DeregisterEventSource(IntPtr hEventLog)
		{
			if (Win32EventLog.PInvoke.DeregisterEventSource(hEventLog) != 1)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00053E60 File Offset: 0x00052060
		private static string LookupAccountSid(string machineName, byte[] sid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			uint capacity = (uint)stringBuilder.Capacity;
			StringBuilder stringBuilder2 = new StringBuilder();
			uint capacity2 = (uint)stringBuilder2.Capacity;
			string text = null;
			while (text == null)
			{
				Win32EventLog.SidNameUse sidNameUse;
				if (!Win32EventLog.PInvoke.LookupAccountSid(machineName, sid, stringBuilder, ref capacity, stringBuilder2, ref capacity2, out sidNameUse))
				{
					if (Marshal.GetLastWin32Error() == 122)
					{
						stringBuilder.EnsureCapacity((int)capacity);
						stringBuilder2.EnsureCapacity((int)capacity2);
					}
					else
					{
						text = string.Empty;
					}
				}
				else
				{
					text = string.Format("{0}\\{1}", stringBuilder2.ToString(), stringBuilder.ToString());
				}
			}
			return text;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00053EE0 File Offset: 0x000520E0
		private static string FetchMessage(string msgDll, uint messageID, string[] replacementStrings)
		{
			IntPtr intPtr = Win32EventLog.PInvoke.LoadLibraryEx(msgDll, IntPtr.Zero, Win32EventLog.LoadFlags.LibraryAsDataFile);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr[] array = new IntPtr[replacementStrings.Length];
			try
			{
				for (int i = 0; i < replacementStrings.Length; i++)
				{
					array[i] = Marshal.StringToHGlobalAuto(replacementStrings[i]);
				}
				if (Win32EventLog.PInvoke.FormatMessage(Win32EventLog.FormatMessageFlags.AllocateBuffer | Win32EventLog.FormatMessageFlags.FromHModule | Win32EventLog.FormatMessageFlags.ArgumentArray, intPtr, messageID, 0, ref intPtr2, 0, array) != 0)
				{
					string text = Marshal.PtrToStringAuto(intPtr2);
					intPtr2 = Win32EventLog.PInvoke.LocalFree(intPtr2);
					return text.TrimEnd(null);
				}
				Marshal.GetLastWin32Error();
			}
			finally
			{
				foreach (IntPtr intPtr3 in array)
				{
					if (intPtr3 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr3);
					}
				}
				Win32EventLog.PInvoke.FreeLibrary(intPtr);
			}
			return null;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00053FB4 File Offset: 0x000521B4
		private string[] GetMessageResourceDlls(string source, string valueName)
		{
			RegistryKey registryKey = Win32EventLog.FindSourceKeyByName(source, base.CoreEventLog.MachineName, false);
			if (registryKey != null)
			{
				string text = registryKey.GetValue(valueName) as string;
				if (text != null)
				{
					return text.Split(';', StringSplitOptions.None);
				}
			}
			return new string[0];
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00053FF8 File Offset: 0x000521F8
		private IntPtr ReadHandle
		{
			get
			{
				if (this._readHandle != IntPtr.Zero)
				{
					return this._readHandle;
				}
				string logName = base.CoreEventLog.GetLogName();
				this._readHandle = Win32EventLog.PInvoke.OpenEventLog(base.CoreEventLog.MachineName, logName);
				if (this._readHandle == IntPtr.Zero)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event Log '{0}' on computer '{1}' cannot be opened.", logName, base.CoreEventLog.MachineName), new Win32Exception());
				}
				return this._readHandle;
			}
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00054080 File Offset: 0x00052280
		private IntPtr RegisterEventSource()
		{
			IntPtr intPtr = Win32EventLog.PInvoke.RegisterEventSource(base.CoreEventLog.MachineName, base.CoreEventLog.Source);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Event source '{0}' on computer '{1}' cannot be opened.", base.CoreEventLog.Source, base.CoreEventLog.MachineName), new Win32Exception());
			}
			return intPtr;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x000540E8 File Offset: 0x000522E8
		public override void DisableNotification()
		{
			object eventLock = this._eventLock;
			lock (eventLock)
			{
				if (this._notifyResetEvent != null)
				{
					this._notifyResetEvent.Close();
					this._notifyResetEvent = null;
				}
				this._notifyThread = null;
			}
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00054144 File Offset: 0x00052344
		public override void EnableNotification()
		{
			object eventLock = this._eventLock;
			lock (eventLock)
			{
				if (this._notifyResetEvent == null)
				{
					this._notifyResetEvent = new ManualResetEvent(false);
					this._lastEntryWritten = this.OldestEventLogEntry + base.EntryCount;
					if (Win32EventLog.PInvoke.NotifyChangeEventLog(this.ReadHandle, this._notifyResetEvent.SafeWaitHandle.DangerousGetHandle()) == 0)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unable to receive notifications for log '{0}' on computer '{1}'.", base.CoreEventLog.GetLogName(), base.CoreEventLog.MachineName), new Win32Exception());
					}
					this._notifyThread = new Thread(delegate
					{
						this.NotifyEventThread(this._notifyResetEvent);
					});
					this._notifyThread.IsBackground = true;
					this._notifyThread.Start();
				}
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00054228 File Offset: 0x00052428
		private void NotifyEventThread(ManualResetEvent resetEvent)
		{
			if (resetEvent == null)
			{
				return;
			}
			for (;;)
			{
				try
				{
					resetEvent.WaitOne();
				}
				catch (ObjectDisposedException)
				{
					break;
				}
				object eventLock = this._eventLock;
				lock (eventLock)
				{
					if (resetEvent == this._notifyResetEvent)
					{
						if (!(this._readHandle == IntPtr.Zero))
						{
							int oldestEventLogEntry = this.OldestEventLogEntry;
							if (this._lastEntryWritten < oldestEventLogEntry)
							{
								this._lastEntryWritten = oldestEventLogEntry;
							}
							int num = this._lastEntryWritten - oldestEventLogEntry;
							int num2 = base.EntryCount + oldestEventLogEntry;
							for (int i = num; i < num2 - 1; i++)
							{
								EventLogEntry entry = this.GetEntry(i);
								base.CoreEventLog.OnEntryWritten(entry);
							}
							this._lastEntryWritten = num2;
							continue;
						}
					}
				}
				break;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0000822E File Offset: 0x0000642E
		public override OverflowAction OverflowAction
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x0000822E File Offset: 0x0000642E
		public override int MinimumRetentionDays
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0000822E File Offset: 0x0000642E
		// (set) Token: 0x06001494 RID: 5268 RVA: 0x0000822E File Offset: 0x0000642E
		public override long MaximumKilobytes
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0000822E File Offset: 0x0000642E
		public override void ModifyOverflowPolicy(OverflowAction action, int retentionDays)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0000822E File Offset: 0x0000642E
		public override void RegisterDisplayName(string resourceFile, long resourceId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000B8D RID: 2957
		private const int MESSAGE_NOT_FOUND = 317;

		// Token: 0x04000B8E RID: 2958
		private ManualResetEvent _notifyResetEvent;

		// Token: 0x04000B8F RID: 2959
		private IntPtr _readHandle;

		// Token: 0x04000B90 RID: 2960
		private Thread _notifyThread;

		// Token: 0x04000B91 RID: 2961
		private int _lastEntryWritten;

		// Token: 0x04000B92 RID: 2962
		private object _eventLock = new object();

		// Token: 0x02000287 RID: 647
		private class PInvoke
		{
			// Token: 0x06001498 RID: 5272
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ClearEventLog(IntPtr hEventLog, string lpBackupFileName);

			// Token: 0x06001499 RID: 5273
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int CloseEventLog(IntPtr hEventLog);

			// Token: 0x0600149A RID: 5274
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int DeregisterEventSource(IntPtr hEventLog);

			// Token: 0x0600149B RID: 5275
			[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
			public static extern int FormatMessage(Win32EventLog.FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, ref IntPtr lpBuffer, int nSize, IntPtr[] arguments);

			// Token: 0x0600149C RID: 5276
			[DllImport("kernel32", SetLastError = true)]
			public static extern bool FreeLibrary(IntPtr hModule);

			// Token: 0x0600149D RID: 5277
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int GetNumberOfEventLogRecords(IntPtr hEventLog, ref int NumberOfRecords);

			// Token: 0x0600149E RID: 5278
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int GetOldestEventLogRecord(IntPtr hEventLog, ref int OldestRecord);

			// Token: 0x0600149F RID: 5279
			[DllImport("kernel32", SetLastError = true)]
			public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, Win32EventLog.LoadFlags dwFlags);

			// Token: 0x060014A0 RID: 5280
			[DllImport("kernel32", SetLastError = true)]
			public static extern IntPtr LocalFree(IntPtr hMem);

			// Token: 0x060014A1 RID: 5281
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern bool LookupAccountSid(string lpSystemName, [MarshalAs(UnmanagedType.LPArray)] byte[] Sid, StringBuilder lpName, ref uint cchName, StringBuilder ReferencedDomainName, ref uint cchReferencedDomainName, out Win32EventLog.SidNameUse peUse);

			// Token: 0x060014A2 RID: 5282
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int NotifyChangeEventLog(IntPtr hEventLog, IntPtr hEvent);

			// Token: 0x060014A3 RID: 5283
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern IntPtr OpenEventLog(string machineName, string logName);

			// Token: 0x060014A4 RID: 5284
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern IntPtr RegisterEventSource(string machineName, string sourceName);

			// Token: 0x060014A5 RID: 5285
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ReportEvent(IntPtr hHandle, ushort wType, ushort wCategory, uint dwEventID, IntPtr sid, ushort wNumStrings, uint dwDataSize, string[] lpStrings, byte[] lpRawData);

			// Token: 0x060014A6 RID: 5286
			[DllImport("advapi32.dll", SetLastError = true)]
			public static extern int ReadEventLog(IntPtr hEventLog, Win32EventLog.ReadFlags dwReadFlags, int dwRecordOffset, byte[] buffer, int nNumberOfBytesToRead, ref int pnBytesRead, ref int pnMinNumberOfBytesNeeded);

			// Token: 0x04000B93 RID: 2963
			public const int ERROR_INSUFFICIENT_BUFFER = 122;

			// Token: 0x04000B94 RID: 2964
			public const int ERROR_EVENTLOG_FILE_CHANGED = 1503;
		}

		// Token: 0x02000288 RID: 648
		private enum ReadFlags
		{
			// Token: 0x04000B96 RID: 2966
			Sequential = 1,
			// Token: 0x04000B97 RID: 2967
			Seek,
			// Token: 0x04000B98 RID: 2968
			ForwardsRead = 4,
			// Token: 0x04000B99 RID: 2969
			BackwardsRead = 8
		}

		// Token: 0x02000289 RID: 649
		private enum LoadFlags : uint
		{
			// Token: 0x04000B9B RID: 2971
			LibraryAsDataFile = 2U
		}

		// Token: 0x0200028A RID: 650
		[Flags]
		private enum FormatMessageFlags
		{
			// Token: 0x04000B9D RID: 2973
			AllocateBuffer = 256,
			// Token: 0x04000B9E RID: 2974
			IgnoreInserts = 512,
			// Token: 0x04000B9F RID: 2975
			FromHModule = 2048,
			// Token: 0x04000BA0 RID: 2976
			FromSystem = 4096,
			// Token: 0x04000BA1 RID: 2977
			ArgumentArray = 8192
		}

		// Token: 0x0200028B RID: 651
		private enum SidNameUse
		{
			// Token: 0x04000BA3 RID: 2979
			User = 1,
			// Token: 0x04000BA4 RID: 2980
			Group,
			// Token: 0x04000BA5 RID: 2981
			Domain,
			// Token: 0x04000BA6 RID: 2982
			lias,
			// Token: 0x04000BA7 RID: 2983
			WellKnownGroup,
			// Token: 0x04000BA8 RID: 2984
			DeletedAccount,
			// Token: 0x04000BA9 RID: 2985
			Invalid,
			// Token: 0x04000BAA RID: 2986
			Unknown,
			// Token: 0x04000BAB RID: 2987
			Computer
		}
	}
}

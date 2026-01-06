using System;

namespace System.Diagnostics
{
	/// <summary>Represents the configuration settings used to create an event log source on the local computer or a remote computer.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000266 RID: 614
	public class EventSourceCreationData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.EventSourceCreationData" /> class with a specified event source and event log name.</summary>
		/// <param name="source">The name to register with the event log as a source of entries. </param>
		/// <param name="logName">The name of the log to which entries from the source are written. </param>
		// Token: 0x06001338 RID: 4920 RVA: 0x00051255 File Offset: 0x0004F455
		public EventSourceCreationData(string source, string logName)
		{
			this._source = source;
			this._logName = logName;
			this._machineName = ".";
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00051276 File Offset: 0x0004F476
		internal EventSourceCreationData(string source, string logName, string machineName)
		{
			this._source = source;
			if (logName == null || logName.Length == 0)
			{
				this._logName = "Application";
			}
			else
			{
				this._logName = logName;
			}
			this._machineName = machineName;
		}

		/// <summary>Gets or sets the number of categories in the category resource file.</summary>
		/// <returns>The number of categories in the category resource file. The default value is zero.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to a negative value or to a value larger than <see cref="F:System.UInt16.MaxValue" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x000512AB File Offset: 0x0004F4AB
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x000512B3 File Offset: 0x0004F4B3
		public int CategoryCount
		{
			get
			{
				return this._categoryCount;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._categoryCount = value;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains category strings for the source.</summary>
		/// <returns>The path of the category resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x000512CB File Offset: 0x0004F4CB
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x000512D3 File Offset: 0x0004F4D3
		public string CategoryResourceFile
		{
			get
			{
				return this._categoryResourceFile;
			}
			set
			{
				this._categoryResourceFile = value;
			}
		}

		/// <summary>Gets or sets the name of the event log to which the source writes entries.</summary>
		/// <returns>The name of the event log. This can be Application, System, or a custom log name. The default value is "Application."</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x000512DC File Offset: 0x0004F4DC
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x000512E4 File Offset: 0x0004F4E4
		public string LogName
		{
			get
			{
				return this._logName;
			}
			set
			{
				this._logName = value;
			}
		}

		/// <summary>Gets or sets the name of the computer on which to register the event source.</summary>
		/// <returns>The name of the system on which to register the event source. The default is the local computer (".").</returns>
		/// <exception cref="T:System.ArgumentException">The computer name is invalid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x000512ED File Offset: 0x0004F4ED
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x000512F5 File Offset: 0x0004F4F5
		public string MachineName
		{
			get
			{
				return this._machineName;
			}
			set
			{
				this._machineName = value;
			}
		}

		/// <summary>Gets or sets the path of the message resource file that contains message formatting strings for the source.</summary>
		/// <returns>The path of the message resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x000512FE File Offset: 0x0004F4FE
		// (set) Token: 0x06001343 RID: 4931 RVA: 0x00051306 File Offset: 0x0004F506
		public string MessageResourceFile
		{
			get
			{
				return this._messageResourceFile;
			}
			set
			{
				this._messageResourceFile = value;
			}
		}

		/// <summary>Gets or sets the path of the resource file that contains message parameter strings for the source.</summary>
		/// <returns>The path of the parameter resource file. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x0005130F File Offset: 0x0004F50F
		// (set) Token: 0x06001345 RID: 4933 RVA: 0x00051317 File Offset: 0x0004F517
		public string ParameterResourceFile
		{
			get
			{
				return this._parameterResourceFile;
			}
			set
			{
				this._parameterResourceFile = value;
			}
		}

		/// <summary>Gets or sets the name to register with the event log as an event source.</summary>
		/// <returns>The name to register with the event log as a source of entries. The default is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00051320 File Offset: 0x0004F520
		// (set) Token: 0x06001347 RID: 4935 RVA: 0x00051328 File Offset: 0x0004F528
		public string Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x04000ADD RID: 2781
		private string _source;

		// Token: 0x04000ADE RID: 2782
		private string _logName;

		// Token: 0x04000ADF RID: 2783
		private string _machineName;

		// Token: 0x04000AE0 RID: 2784
		private string _messageResourceFile;

		// Token: 0x04000AE1 RID: 2785
		private string _parameterResourceFile;

		// Token: 0x04000AE2 RID: 2786
		private string _categoryResourceFile;

		// Token: 0x04000AE3 RID: 2787
		private int _categoryCount;
	}
}

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Interfaces;
using TwitchLib.Communication.Models;
using TwitchLib.Communication.Services;

namespace TwitchLib.Communication.Clients
{
	// Token: 0x02000013 RID: 19
	public class TcpClient : IClient
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002737 File Offset: 0x00000937
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000273F File Offset: 0x0000093F
		public TimeSpan DefaultKeepAliveInterval { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002748 File Offset: 0x00000948
		public int SendQueueLength
		{
			get
			{
				return this._throttlers.SendQueue.Count;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000275A File Offset: 0x0000095A
		public int WhisperQueueLength
		{
			get
			{
				return this._throttlers.WhisperQueue.Count;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000276C File Offset: 0x0000096C
		public bool IsConnected
		{
			get
			{
				TcpClient client = this.Client;
				return client != null && client.Connected;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002780 File Offset: 0x00000980
		public IClientOptions Options { get; }

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000097 RID: 151 RVA: 0x00002788 File Offset: 0x00000988
		// (remove) Token: 0x06000098 RID: 152 RVA: 0x000027C0 File Offset: 0x000009C0
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnConnectedEventArgs> OnConnected;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000099 RID: 153 RVA: 0x000027F8 File Offset: 0x000009F8
		// (remove) Token: 0x0600009A RID: 154 RVA: 0x00002830 File Offset: 0x00000A30
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnDataEventArgs> OnData;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600009B RID: 155 RVA: 0x00002868 File Offset: 0x00000A68
		// (remove) Token: 0x0600009C RID: 156 RVA: 0x000028A0 File Offset: 0x00000AA0
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnDisconnectedEventArgs> OnDisconnected;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600009D RID: 157 RVA: 0x000028D8 File Offset: 0x00000AD8
		// (remove) Token: 0x0600009E RID: 158 RVA: 0x00002910 File Offset: 0x00000B10
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnErrorEventArgs> OnError;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600009F RID: 159 RVA: 0x00002948 File Offset: 0x00000B48
		// (remove) Token: 0x060000A0 RID: 160 RVA: 0x00002980 File Offset: 0x00000B80
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnFatalErrorEventArgs> OnFatality;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060000A1 RID: 161 RVA: 0x000029B8 File Offset: 0x00000BB8
		// (remove) Token: 0x060000A2 RID: 162 RVA: 0x000029F0 File Offset: 0x00000BF0
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnMessageEventArgs> OnMessage;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060000A3 RID: 163 RVA: 0x00002A28 File Offset: 0x00000C28
		// (remove) Token: 0x060000A4 RID: 164 RVA: 0x00002A60 File Offset: 0x00000C60
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnMessageThrottledEventArgs> OnMessageThrottled;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060000A5 RID: 165 RVA: 0x00002A98 File Offset: 0x00000C98
		// (remove) Token: 0x060000A6 RID: 166 RVA: 0x00002AD0 File Offset: 0x00000CD0
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnWhisperThrottledEventArgs> OnWhisperThrottled;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060000A7 RID: 167 RVA: 0x00002B08 File Offset: 0x00000D08
		// (remove) Token: 0x060000A8 RID: 168 RVA: 0x00002B40 File Offset: 0x00000D40
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnSendFailedEventArgs> OnSendFailed;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060000A9 RID: 169 RVA: 0x00002B78 File Offset: 0x00000D78
		// (remove) Token: 0x060000AA RID: 170 RVA: 0x00002BB0 File Offset: 0x00000DB0
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060000AB RID: 171 RVA: 0x00002BE8 File Offset: 0x00000DE8
		// (remove) Token: 0x060000AC RID: 172 RVA: 0x00002C20 File Offset: 0x00000E20
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnReconnectedEventArgs> OnReconnected;

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00002C55 File Offset: 0x00000E55
		private int Port
		{
			get
			{
				return (this.Options != null) ? (this.Options.UseSsl ? 443 : 80) : 0;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002C78 File Offset: 0x00000E78
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00002C80 File Offset: 0x00000E80
		public TcpClient Client { get; private set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x00002C8C File Offset: 0x00000E8C
		public TcpClient(IClientOptions options = null)
		{
			this.Options = options ?? new ClientOptions();
			this._throttlers = new Throttlers(this, this.Options.ThrottlingPeriod, this.Options.WhisperThrottlingPeriod)
			{
				TokenSource = this._tokenSource
			};
			this.InitializeClient();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002D00 File Offset: 0x00000F00
		private void InitializeClient()
		{
			bool stopServices = this._stopServices;
			if (!stopServices)
			{
				this.Client = new TcpClient();
				bool flag = this._monitorTask == null;
				if (flag)
				{
					this._monitorTask = this.StartMonitorTask();
				}
				else
				{
					bool isCompleted = this._monitorTask.IsCompleted;
					if (isCompleted)
					{
						this._monitorTask = this.StartMonitorTask();
					}
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002D60 File Offset: 0x00000F60
		public bool Open()
		{
			this.Reset();
			return this._Open();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002D80 File Offset: 0x00000F80
		private bool _Open()
		{
			bool stopServices = this._stopServices;
			bool flag;
			if (stopServices)
			{
				flag = false;
			}
			else
			{
				try
				{
					bool isConnected = this.IsConnected;
					if (isConnected)
					{
						flag = true;
					}
					else
					{
						Task.Run(delegate
						{
							this.InitializeClient();
							this.Client.Connect(this._server, this.Port);
							bool useSsl = this.Options.UseSsl;
							if (useSsl)
							{
								SslStream sslStream = new SslStream(this.Client.GetStream(), false);
								sslStream.AuthenticateAsClient(this._server);
								this._reader = new StreamReader(sslStream);
								this._writer = new StreamWriter(sslStream);
							}
							else
							{
								this._reader = new StreamReader(this.Client.GetStream());
								this._writer = new StreamWriter(this.Client.GetStream());
							}
						}).Wait(10000);
						bool flag2 = !this.IsConnected;
						if (flag2)
						{
							flag = this._Open();
						}
						else
						{
							this.StartNetworkServices();
							flag = true;
						}
					}
				}
				catch (Exception)
				{
					this.InitializeClient();
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002E08 File Offset: 0x00001008
		public void Close(bool callDisconnect = true)
		{
			StreamReader reader = this._reader;
			if (reader != null)
			{
				reader.Dispose();
			}
			StreamWriter writer = this._writer;
			if (writer != null)
			{
				writer.Dispose();
			}
			TcpClient client = this.Client;
			if (client != null)
			{
				client.Close();
			}
			this._stopServices = callDisconnect;
			this.CleanupServices();
			this.InitializeClient();
			EventHandler<OnDisconnectedEventArgs> onDisconnected = this.OnDisconnected;
			if (onDisconnected != null)
			{
				onDisconnected.Invoke(this, new OnDisconnectedEventArgs());
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002E79 File Offset: 0x00001079
		public void Reconnect()
		{
			this.Reset();
			this._Reconnect();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002E8C File Offset: 0x0000108C
		private void _Reconnect()
		{
			bool stopServices = this._stopServices;
			if (!stopServices)
			{
				Task.Run(delegate
				{
					Task.Delay(20).Wait();
					this.Close(true);
					bool flag = this.Open();
					if (flag)
					{
						EventHandler<OnReconnectedEventArgs> onReconnected = this.OnReconnected;
						if (onReconnected != null)
						{
							onReconnected.Invoke(this, new OnReconnectedEventArgs());
						}
					}
				});
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002EBC File Offset: 0x000010BC
		public bool Send(string message)
		{
			bool flag2;
			try
			{
				bool flag = !this.IsConnected || this.SendQueueLength >= this.Options.SendQueueCapacity;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					this._throttlers.SendQueue.Add(new Tuple<DateTime, string>(DateTime.UtcNow, message));
					flag2 = true;
				}
			}
			catch (Exception ex)
			{
				EventHandler<OnErrorEventArgs> onError = this.OnError;
				if (onError != null)
				{
					onError.Invoke(this, new OnErrorEventArgs
					{
						Exception = ex
					});
				}
				throw;
			}
			return flag2;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00002F4C File Offset: 0x0000114C
		public bool SendWhisper(string message)
		{
			bool flag2;
			try
			{
				bool flag = !this.IsConnected || this.WhisperQueueLength >= this.Options.WhisperQueueCapacity;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					this._throttlers.WhisperQueue.Add(new Tuple<DateTime, string>(DateTime.UtcNow, message));
					flag2 = true;
				}
			}
			catch (Exception ex)
			{
				EventHandler<OnErrorEventArgs> onError = this.OnError;
				if (onError != null)
				{
					onError.Invoke(this, new OnErrorEventArgs
					{
						Exception = ex
					});
				}
				throw;
			}
			return flag2;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002FDC File Offset: 0x000011DC
		private void StartNetworkServices()
		{
			this._networkServicesRunning = true;
			this._networkTasks = Enumerable.ToArray<Task>(new Task[]
			{
				this.StartListenerTask(),
				this._throttlers.StartSenderTask(),
				this._throttlers.StartWhisperSenderTask()
			});
			bool flag = !Enumerable.Any<Task>(this._networkTasks, (Task c) => c.IsFaulted);
			if (!flag)
			{
				this._networkServicesRunning = false;
				this.CleanupServices();
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003068 File Offset: 0x00001268
		public Task SendAsync(string message)
		{
			TcpClient.<>c__DisplayClass72_0 CS$<>8__locals1 = new TcpClient.<>c__DisplayClass72_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.message = message;
			return Task.Run(delegate
			{
				TcpClient.<>c__DisplayClass72_0.<<SendAsync>b__0>d <<SendAsync>b__0>d = new TcpClient.<>c__DisplayClass72_0.<<SendAsync>b__0>d();
				<<SendAsync>b__0>d.<>4__this = CS$<>8__locals1;
				<<SendAsync>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<SendAsync>b__0>d.<>1__state = -1;
				<<SendAsync>b__0>d.<>t__builder.Start<TcpClient.<>c__DisplayClass72_0.<<SendAsync>b__0>d>(ref <<SendAsync>b__0>d);
				return <<SendAsync>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000030A0 File Offset: 0x000012A0
		private Task StartListenerTask()
		{
			return Task.Run(delegate
			{
				TcpClient.<<StartListenerTask>b__73_0>d <<StartListenerTask>b__73_0>d = new TcpClient.<<StartListenerTask>b__73_0>d();
				<<StartListenerTask>b__73_0>d.<>4__this = this;
				<<StartListenerTask>b__73_0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<StartListenerTask>b__73_0>d.<>1__state = -1;
				<<StartListenerTask>b__73_0>d.<>t__builder.Start<TcpClient.<<StartListenerTask>b__73_0>d>(ref <<StartListenerTask>b__73_0>d);
				return <<StartListenerTask>b__73_0>d.<>t__builder.Task;
			});
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000030C4 File Offset: 0x000012C4
		private Task StartMonitorTask()
		{
			return Task.Run(delegate
			{
				bool flag = false;
				int num = 0;
				try
				{
					bool flag2 = this.IsConnected;
					while (!this._tokenSource.IsCancellationRequested)
					{
						bool flag3 = flag2 == this.IsConnected;
						if (flag3)
						{
							Thread.Sleep(200);
							bool flag4 = !this.IsConnected;
							if (flag4)
							{
								this.NotConnectedCounter++;
							}
							else
							{
								num++;
							}
							bool flag5 = num >= 300;
							if (flag5)
							{
								this.Send("PING");
								num = 0;
							}
							int notConnectedCounter = this.NotConnectedCounter;
							int num2 = notConnectedCounter;
							if (num2 <= 75)
							{
								if (num2 != 25 && num2 != 75)
								{
									goto IL_00BF;
								}
								goto IL_00B6;
							}
							else
							{
								if (num2 != 150 && num2 != 300 && num2 != 600)
								{
									goto IL_00BF;
								}
								goto IL_00B6;
							}
							IL_00EE:
							bool flag6 = this.NotConnectedCounter != 0 && this.IsConnected;
							if (flag6)
							{
								this.NotConnectedCounter = 0;
							}
							continue;
							IL_00B6:
							this._Reconnect();
							goto IL_00EE;
							IL_00BF:
							bool flag7 = this.NotConnectedCounter >= 1200 && this.NotConnectedCounter % 600 == 0;
							if (flag7)
							{
								this._Reconnect();
							}
							goto IL_00EE;
						}
						EventHandler<OnStateChangedEventArgs> onStateChanged = this.OnStateChanged;
						if (onStateChanged != null)
						{
							onStateChanged.Invoke(this, new OnStateChangedEventArgs
							{
								IsConnected = this.IsConnected,
								WasConnected = flag2
							});
						}
						bool isConnected = this.IsConnected;
						if (isConnected)
						{
							EventHandler<OnConnectedEventArgs> onConnected = this.OnConnected;
							if (onConnected != null)
							{
								onConnected.Invoke(this, new OnConnectedEventArgs());
							}
						}
						bool flag8 = !this.IsConnected && !this._stopServices;
						if (flag8)
						{
							bool flag9 = flag2 && this.Options.ReconnectionPolicy != null && !this.Options.ReconnectionPolicy.AreAttemptsComplete();
							if (flag9)
							{
								flag = true;
								break;
							}
							EventHandler<OnDisconnectedEventArgs> onDisconnected = this.OnDisconnected;
							if (onDisconnected != null)
							{
								onDisconnected.Invoke(this, new OnDisconnectedEventArgs());
							}
						}
						flag2 = this.IsConnected;
					}
				}
				catch (Exception ex)
				{
					EventHandler<OnErrorEventArgs> onError = this.OnError;
					if (onError != null)
					{
						onError.Invoke(this, new OnErrorEventArgs
						{
							Exception = ex
						});
					}
				}
				bool flag10 = flag && !this._stopServices;
				if (flag10)
				{
					this._Reconnect();
				}
			}, this._tokenSource.Token);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000030F4 File Offset: 0x000012F4
		private void CleanupServices()
		{
			this._tokenSource.Cancel();
			this._tokenSource = new CancellationTokenSource();
			this._throttlers.TokenSource = this._tokenSource;
			bool flag = !this._stopServices;
			if (!flag)
			{
				Task[] networkTasks = this._networkTasks;
				bool flag2 = networkTasks == null || networkTasks.Length == 0;
				if (!flag2)
				{
					bool flag3 = Task.WaitAll(this._networkTasks, 15000);
					if (!flag3)
					{
						EventHandler<OnFatalErrorEventArgs> onFatality = this.OnFatality;
						if (onFatality != null)
						{
							onFatality.Invoke(this, new OnFatalErrorEventArgs
							{
								Reason = "Fatal network error. Network services fail to shut down."
							});
						}
					}
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000318D File Offset: 0x0000138D
		private void Reset()
		{
			this._stopServices = false;
			this._throttlers.Reconnecting = false;
			this._networkServicesRunning = false;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000031AB File Offset: 0x000013AB
		public void WhisperThrottled(OnWhisperThrottledEventArgs eventArgs)
		{
			EventHandler<OnWhisperThrottledEventArgs> onWhisperThrottled = this.OnWhisperThrottled;
			if (onWhisperThrottled != null)
			{
				onWhisperThrottled.Invoke(this, eventArgs);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000031C2 File Offset: 0x000013C2
		public void MessageThrottled(OnMessageThrottledEventArgs eventArgs)
		{
			EventHandler<OnMessageThrottledEventArgs> onMessageThrottled = this.OnMessageThrottled;
			if (onMessageThrottled != null)
			{
				onMessageThrottled.Invoke(this, eventArgs);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000031D9 File Offset: 0x000013D9
		public void SendFailed(OnSendFailedEventArgs eventArgs)
		{
			EventHandler<OnSendFailedEventArgs> onSendFailed = this.OnSendFailed;
			if (onSendFailed != null)
			{
				onSendFailed.Invoke(this, eventArgs);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000031F0 File Offset: 0x000013F0
		public void Error(OnErrorEventArgs eventArgs)
		{
			EventHandler<OnErrorEventArgs> onError = this.OnError;
			if (onError != null)
			{
				onError.Invoke(this, eventArgs);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003208 File Offset: 0x00001408
		public void Dispose()
		{
			this.Close(true);
			this._throttlers.ShouldDispose = true;
			this._tokenSource.Cancel();
			Thread.Sleep(500);
			this._tokenSource.Dispose();
			TcpClient client = this.Client;
			if (client != null)
			{
				client.Dispose();
			}
			GC.Collect();
		}

		// Token: 0x04000034 RID: 52
		private int NotConnectedCounter;

		// Token: 0x04000042 RID: 66
		private readonly string _server = "irc.chat.twitch.tv";

		// Token: 0x04000044 RID: 68
		private StreamReader _reader;

		// Token: 0x04000045 RID: 69
		private StreamWriter _writer;

		// Token: 0x04000046 RID: 70
		private readonly Throttlers _throttlers;

		// Token: 0x04000047 RID: 71
		private CancellationTokenSource _tokenSource = new CancellationTokenSource();

		// Token: 0x04000048 RID: 72
		private bool _stopServices;

		// Token: 0x04000049 RID: 73
		private bool _networkServicesRunning;

		// Token: 0x0400004A RID: 74
		private Task[] _networkTasks;

		// Token: 0x0400004B RID: 75
		private Task _monitorTask;
	}
}

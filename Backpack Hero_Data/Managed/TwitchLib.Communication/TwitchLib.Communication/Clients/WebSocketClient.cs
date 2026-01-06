using System;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Communication.Enums;
using TwitchLib.Communication.Events;
using TwitchLib.Communication.Interfaces;
using TwitchLib.Communication.Models;
using TwitchLib.Communication.Services;

namespace TwitchLib.Communication.Clients
{
	// Token: 0x02000014 RID: 20
	public class WebSocketClient : IClient
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000035F0 File Offset: 0x000017F0
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x000035F8 File Offset: 0x000017F8
		public TimeSpan DefaultKeepAliveInterval { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003601 File Offset: 0x00001801
		public int SendQueueLength
		{
			get
			{
				return this._throttlers.SendQueue.Count;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003613 File Offset: 0x00001813
		public int WhisperQueueLength
		{
			get
			{
				return this._throttlers.WhisperQueue.Count;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003625 File Offset: 0x00001825
		public bool IsConnected
		{
			get
			{
				ClientWebSocket client = this.Client;
				return client != null && client.State == 2;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000363C File Offset: 0x0000183C
		public IClientOptions Options { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003644 File Offset: 0x00001844
		// (set) Token: 0x060000CF RID: 207 RVA: 0x0000364C File Offset: 0x0000184C
		public ClientWebSocket Client { get; private set; }

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060000D0 RID: 208 RVA: 0x00003658 File Offset: 0x00001858
		// (remove) Token: 0x060000D1 RID: 209 RVA: 0x00003690 File Offset: 0x00001890
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnConnectedEventArgs> OnConnected;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060000D2 RID: 210 RVA: 0x000036C8 File Offset: 0x000018C8
		// (remove) Token: 0x060000D3 RID: 211 RVA: 0x00003700 File Offset: 0x00001900
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnDataEventArgs> OnData;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060000D4 RID: 212 RVA: 0x00003738 File Offset: 0x00001938
		// (remove) Token: 0x060000D5 RID: 213 RVA: 0x00003770 File Offset: 0x00001970
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnDisconnectedEventArgs> OnDisconnected;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060000D6 RID: 214 RVA: 0x000037A8 File Offset: 0x000019A8
		// (remove) Token: 0x060000D7 RID: 215 RVA: 0x000037E0 File Offset: 0x000019E0
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnErrorEventArgs> OnError;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060000D8 RID: 216 RVA: 0x00003818 File Offset: 0x00001A18
		// (remove) Token: 0x060000D9 RID: 217 RVA: 0x00003850 File Offset: 0x00001A50
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnFatalErrorEventArgs> OnFatality;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060000DA RID: 218 RVA: 0x00003888 File Offset: 0x00001A88
		// (remove) Token: 0x060000DB RID: 219 RVA: 0x000038C0 File Offset: 0x00001AC0
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnMessageEventArgs> OnMessage;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060000DC RID: 220 RVA: 0x000038F8 File Offset: 0x00001AF8
		// (remove) Token: 0x060000DD RID: 221 RVA: 0x00003930 File Offset: 0x00001B30
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnMessageThrottledEventArgs> OnMessageThrottled;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060000DE RID: 222 RVA: 0x00003968 File Offset: 0x00001B68
		// (remove) Token: 0x060000DF RID: 223 RVA: 0x000039A0 File Offset: 0x00001BA0
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnWhisperThrottledEventArgs> OnWhisperThrottled;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060000E0 RID: 224 RVA: 0x000039D8 File Offset: 0x00001BD8
		// (remove) Token: 0x060000E1 RID: 225 RVA: 0x00003A10 File Offset: 0x00001C10
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnSendFailedEventArgs> OnSendFailed;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060000E2 RID: 226 RVA: 0x00003A48 File Offset: 0x00001C48
		// (remove) Token: 0x060000E3 RID: 227 RVA: 0x00003A80 File Offset: 0x00001C80
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060000E4 RID: 228 RVA: 0x00003AB8 File Offset: 0x00001CB8
		// (remove) Token: 0x060000E5 RID: 229 RVA: 0x00003AF0 File Offset: 0x00001CF0
		[field: DebuggerBrowsable(0)]
		public event EventHandler<OnReconnectedEventArgs> OnReconnected;

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003B25 File Offset: 0x00001D25
		private string Url { get; }

		// Token: 0x060000E7 RID: 231 RVA: 0x00003B30 File Offset: 0x00001D30
		public WebSocketClient(IClientOptions options = null)
		{
			this.Options = options ?? new ClientOptions();
			ClientType clientType = this.Options.ClientType;
			ClientType clientType2 = clientType;
			if (clientType2 != ClientType.Chat)
			{
				if (clientType2 != ClientType.PubSub)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.Url = (this.Options.UseSsl ? "wss://pubsub-edge.twitch.tv:443" : "ws://pubsub-edge.twitch.tv:80");
			}
			else
			{
				this.Url = (this.Options.UseSsl ? "wss://irc-ws.chat.twitch.tv:443" : "ws://irc-ws.chat.twitch.tv:80");
			}
			this._throttlers = new Throttlers(this, this.Options.ThrottlingPeriod, this.Options.WhisperThrottlingPeriod)
			{
				TokenSource = this._tokenSource
			};
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003BF0 File Offset: 0x00001DF0
		private void InitializeClient()
		{
			bool stopServices = this._stopServices;
			if (!stopServices)
			{
				ClientWebSocket client = this.Client;
				if (client != null)
				{
					client.Abort();
				}
				this.Client = new ClientWebSocket();
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

		// Token: 0x060000E9 RID: 233 RVA: 0x00003C60 File Offset: 0x00001E60
		public bool Open()
		{
			this.Reset();
			return this._Open();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003C80 File Offset: 0x00001E80
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
						this.InitializeClient();
						this.Client.ConnectAsync(new Uri(this.Url), this._tokenSource.Token).Wait(10000);
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
				catch (WebSocketException)
				{
					this.InitializeClient();
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003D20 File Offset: 0x00001F20
		public void Close(bool callDisconnect = true)
		{
			ClientWebSocket client = this.Client;
			if (client != null)
			{
				client.Abort();
			}
			this._stopServices = callDisconnect;
			this.CleanupServices();
			bool flag = !callDisconnect;
			if (flag)
			{
				this.InitializeClient();
			}
			EventHandler<OnDisconnectedEventArgs> onDisconnected = this.OnDisconnected;
			if (onDisconnected != null)
			{
				onDisconnected.Invoke(this, new OnDisconnectedEventArgs());
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003D75 File Offset: 0x00001F75
		public void Reconnect()
		{
			this.Reset();
			this._Reconnect();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003D88 File Offset: 0x00001F88
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

		// Token: 0x060000EE RID: 238 RVA: 0x00003DB8 File Offset: 0x00001FB8
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

		// Token: 0x060000EF RID: 239 RVA: 0x00003E48 File Offset: 0x00002048
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

		// Token: 0x060000F0 RID: 240 RVA: 0x00003ED8 File Offset: 0x000020D8
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

		// Token: 0x060000F1 RID: 241 RVA: 0x00003F64 File Offset: 0x00002164
		public Task SendAsync(byte[] message)
		{
			return this.Client.SendAsync(new ArraySegment<byte>(message), 0, true, this._tokenSource.Token);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003F94 File Offset: 0x00002194
		private Task StartListenerTask()
		{
			return Task.Run(delegate
			{
				WebSocketClient.<<StartListenerTask>b__71_0>d <<StartListenerTask>b__71_0>d = new WebSocketClient.<<StartListenerTask>b__71_0>d();
				<<StartListenerTask>b__71_0>d.<>4__this = this;
				<<StartListenerTask>b__71_0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<StartListenerTask>b__71_0>d.<>1__state = -1;
				<<StartListenerTask>b__71_0>d.<>t__builder.Start<WebSocketClient.<<StartListenerTask>b__71_0>d>(ref <<StartListenerTask>b__71_0>d);
				return <<StartListenerTask>b__71_0>d.<>t__builder.Task;
			});
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003FB8 File Offset: 0x000021B8
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
								IsConnected = (this.Client.State == 2),
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
							bool flag10;
							if (this.Client.CloseStatus != null)
							{
								WebSocketCloseStatus? closeStatus = this.Client.CloseStatus;
								WebSocketCloseStatus webSocketCloseStatus = 1000;
								flag10 = !((closeStatus.GetValueOrDefault() == webSocketCloseStatus) & (closeStatus != null));
							}
							else
							{
								flag10 = false;
							}
							bool flag11 = flag10;
							if (flag11)
							{
								EventHandler<OnErrorEventArgs> onError = this.OnError;
								if (onError != null)
								{
									onError.Invoke(this, new OnErrorEventArgs
									{
										Exception = new Exception(this.Client.CloseStatus.ToString() + " " + this.Client.CloseStatusDescription)
									});
								}
							}
						}
						flag2 = this.IsConnected;
					}
				}
				catch (Exception ex)
				{
					EventHandler<OnErrorEventArgs> onError2 = this.OnError;
					if (onError2 != null)
					{
						onError2.Invoke(this, new OnErrorEventArgs
						{
							Exception = ex
						});
					}
				}
				bool flag12 = flag && !this._stopServices;
				if (flag12)
				{
					this._Reconnect();
				}
			}, this._tokenSource.Token);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003FE8 File Offset: 0x000021E8
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

		// Token: 0x060000F5 RID: 245 RVA: 0x00004081 File Offset: 0x00002281
		private void Reset()
		{
			this._stopServices = false;
			this._throttlers.Reconnecting = false;
			this._networkServicesRunning = false;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000409F File Offset: 0x0000229F
		public void WhisperThrottled(OnWhisperThrottledEventArgs eventArgs)
		{
			EventHandler<OnWhisperThrottledEventArgs> onWhisperThrottled = this.OnWhisperThrottled;
			if (onWhisperThrottled != null)
			{
				onWhisperThrottled.Invoke(this, eventArgs);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000040B6 File Offset: 0x000022B6
		public void MessageThrottled(OnMessageThrottledEventArgs eventArgs)
		{
			EventHandler<OnMessageThrottledEventArgs> onMessageThrottled = this.OnMessageThrottled;
			if (onMessageThrottled != null)
			{
				onMessageThrottled.Invoke(this, eventArgs);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000040CD File Offset: 0x000022CD
		public void SendFailed(OnSendFailedEventArgs eventArgs)
		{
			EventHandler<OnSendFailedEventArgs> onSendFailed = this.OnSendFailed;
			if (onSendFailed != null)
			{
				onSendFailed.Invoke(this, eventArgs);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000040E4 File Offset: 0x000022E4
		public void Error(OnErrorEventArgs eventArgs)
		{
			EventHandler<OnErrorEventArgs> onError = this.OnError;
			if (onError != null)
			{
				onError.Invoke(this, eventArgs);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000040FC File Offset: 0x000022FC
		public void Dispose()
		{
			this.Close(true);
			this._throttlers.ShouldDispose = true;
			this._tokenSource.Cancel();
			Thread.Sleep(500);
			this._tokenSource.Dispose();
			ClientWebSocket client = this.Client;
			if (client != null)
			{
				client.Dispose();
			}
			GC.Collect();
		}

		// Token: 0x0400004C RID: 76
		private int NotConnectedCounter;

		// Token: 0x0400005C RID: 92
		private readonly Throttlers _throttlers;

		// Token: 0x0400005D RID: 93
		private CancellationTokenSource _tokenSource = new CancellationTokenSource();

		// Token: 0x0400005E RID: 94
		private bool _stopServices;

		// Token: 0x0400005F RID: 95
		private bool _networkServicesRunning;

		// Token: 0x04000060 RID: 96
		private Task[] _networkTasks;

		// Token: 0x04000061 RID: 97
		private Task _monitorTask;
	}
}

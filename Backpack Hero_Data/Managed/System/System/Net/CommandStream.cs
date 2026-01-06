using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net
{
	// Token: 0x02000391 RID: 913
	internal class CommandStream : NetworkStreamWrapper
	{
		// Token: 0x06001E16 RID: 7702 RVA: 0x0006D764 File Offset: 0x0006B964
		internal CommandStream(TcpClient client)
			: base(client)
		{
			this._decoder = this._encoding.GetDecoder();
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x0006D794 File Offset: 0x0006B994
		internal virtual void Abort(Exception e)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, "closing control Stream", "Abort");
			}
			lock (this)
			{
				if (this._aborted)
				{
					return;
				}
				this._aborted = true;
			}
			try
			{
				base.Close(0);
			}
			finally
			{
				if (e != null)
				{
					this.InvokeRequestCallback(e);
				}
				else
				{
					this.InvokeRequestCallback(null);
				}
			}
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x0006D81C File Offset: 0x0006BA1C
		protected override void Dispose(bool disposing)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "Dispose");
			}
			this.InvokeRequestCallback(null);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x0006D838 File Offset: 0x0006BA38
		protected void InvokeRequestCallback(object obj)
		{
			WebRequest request = this._request;
			if (request != null)
			{
				((FtpWebRequest)request).RequestCallback(obj);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x0006D85B File Offset: 0x0006BA5B
		internal bool RecoverableFailure
		{
			get
			{
				return this._recoverableFailure;
			}
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0006D863 File Offset: 0x0006BA63
		protected void MarkAsRecoverableFailure()
		{
			if (this._index <= 1)
			{
				this._recoverableFailure = true;
			}
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0006D878 File Offset: 0x0006BA78
		internal Stream SubmitRequest(WebRequest request, bool isAsync, bool readInitalResponseOnConnect)
		{
			this.ClearState();
			CommandStream.PipelineEntry[] array = this.BuildCommandsList(request);
			this.InitCommandPipeline(request, array, isAsync);
			if (readInitalResponseOnConnect)
			{
				this._doSend = false;
				this._index = -1;
			}
			return this.ContinueCommandPipeline();
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x0006D8B3 File Offset: 0x0006BAB3
		protected virtual void ClearState()
		{
			this.InitCommandPipeline(null, null, false);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00002F6A File Offset: 0x0000116A
		protected virtual CommandStream.PipelineEntry[] BuildCommandsList(WebRequest request)
		{
			return null;
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0006D8BE File Offset: 0x0006BABE
		protected Exception GenerateException(string message, WebExceptionStatus status, Exception innerException)
		{
			return new WebException(message, innerException, status, null);
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0006D8C9 File Offset: 0x0006BAC9
		protected Exception GenerateException(FtpStatusCode code, string statusDescription, Exception innerException)
		{
			return new WebException(SR.Format("The remote server returned an error: {0}.", NetRes.GetWebStatusCodeString(code, statusDescription)), innerException, WebExceptionStatus.ProtocolError, null);
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x0006D8E4 File Offset: 0x0006BAE4
		protected void InitCommandPipeline(WebRequest request, CommandStream.PipelineEntry[] commands, bool isAsync)
		{
			this._commands = commands;
			this._index = 0;
			this._request = request;
			this._aborted = false;
			this._doRead = true;
			this._doSend = true;
			this._currentResponseDescription = null;
			this._isAsync = isAsync;
			this._recoverableFailure = false;
			this._abortReason = string.Empty;
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x0006D93C File Offset: 0x0006BB3C
		internal void CheckContinuePipeline()
		{
			if (this._isAsync)
			{
				return;
			}
			try
			{
				this.ContinueCommandPipeline();
			}
			catch (Exception ex)
			{
				this.Abort(ex);
			}
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0006D978 File Offset: 0x0006BB78
		protected Stream ContinueCommandPipeline()
		{
			bool isAsync = this._isAsync;
			while (this._index < this._commands.Length)
			{
				if (this._doSend)
				{
					if (this._index < 0)
					{
						throw new InternalException();
					}
					byte[] bytes = this.Encoding.GetBytes(this._commands[this._index].Command);
					if (NetEventSource.Log.IsEnabled())
					{
						string text = this._commands[this._index].Command.Substring(0, this._commands[this._index].Command.Length - 2);
						if (this._commands[this._index].HasFlag(CommandStream.PipelineEntryFlags.DontLogParameter))
						{
							int num = text.IndexOf(' ');
							if (num != -1)
							{
								text = text.Substring(0, num) + " ********";
							}
						}
						if (NetEventSource.IsEnabled)
						{
							NetEventSource.Info(this, FormattableStringFactory.Create("Sending command {0}", new object[] { text }), "ContinueCommandPipeline");
						}
					}
					try
					{
						if (isAsync)
						{
							this.BeginWrite(bytes, 0, bytes.Length, CommandStream.s_writeCallbackDelegate, this);
						}
						else
						{
							this.Write(bytes, 0, bytes.Length);
						}
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					if (isAsync)
					{
						return null;
					}
				}
				Stream stream = null;
				if (this.PostSendCommandProcessing(ref stream))
				{
					return stream;
				}
			}
			lock (this)
			{
				this.Close();
			}
			return null;
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0006DB0C File Offset: 0x0006BD0C
		private bool PostSendCommandProcessing(ref Stream stream)
		{
			if (this._doRead)
			{
				bool isAsync = this._isAsync;
				int index = this._index;
				CommandStream.PipelineEntry[] commands = this._commands;
				try
				{
					ResponseDescription responseDescription = this.ReceiveCommandResponse();
					if (isAsync)
					{
						return true;
					}
					this._currentResponseDescription = responseDescription;
				}
				catch
				{
					if (index < 0 || index >= commands.Length || commands[index].Command != "QUIT\r\n")
					{
						throw;
					}
				}
			}
			return this.PostReadCommandProcessing(ref stream);
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0006DB8C File Offset: 0x0006BD8C
		private bool PostReadCommandProcessing(ref Stream stream)
		{
			if (this._index >= this._commands.Length)
			{
				return false;
			}
			this._doSend = false;
			this._doRead = false;
			CommandStream.PipelineEntry pipelineEntry;
			if (this._index == -1)
			{
				pipelineEntry = null;
			}
			else
			{
				pipelineEntry = this._commands[this._index];
			}
			CommandStream.PipelineInstruction pipelineInstruction;
			if (this._currentResponseDescription == null && pipelineEntry.Command == "QUIT\r\n")
			{
				pipelineInstruction = CommandStream.PipelineInstruction.Advance;
			}
			else
			{
				pipelineInstruction = this.PipelineCallback(pipelineEntry, this._currentResponseDescription, false, ref stream);
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Abort)
			{
				Exception ex;
				if (this._abortReason != string.Empty)
				{
					ex = new WebException(this._abortReason);
				}
				else
				{
					ex = this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
				}
				this.Abort(ex);
				throw ex;
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Advance)
			{
				this._currentResponseDescription = null;
				this._doSend = true;
				this._doRead = true;
				this._index++;
			}
			else
			{
				if (pipelineInstruction == CommandStream.PipelineInstruction.Pause)
				{
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.GiveStream)
				{
					this._currentResponseDescription = null;
					this._doRead = true;
					if (this._isAsync)
					{
						this.ContinueCommandPipeline();
						this.InvokeRequestCallback(stream);
					}
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.Reread)
				{
					this._currentResponseDescription = null;
					this._doRead = true;
				}
			}
			return false;
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual CommandStream.PipelineInstruction PipelineCallback(CommandStream.PipelineEntry entry, ResponseDescription response, bool timeout, ref Stream stream)
		{
			return CommandStream.PipelineInstruction.Abort;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0006DCAC File Offset: 0x0006BEAC
		private static void ReadCallback(IAsyncResult asyncResult)
		{
			ReceiveState receiveState = (ReceiveState)asyncResult.AsyncState;
			try
			{
				Stream connection = receiveState.Connection;
				int num = 0;
				try
				{
					num = connection.EndRead(asyncResult);
					if (num == 0)
					{
						receiveState.Connection.CloseSocket();
					}
				}
				catch (IOException)
				{
					receiveState.Connection.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				receiveState.Connection.ReceiveCommandResponseCallback(receiveState, num);
			}
			catch (Exception ex)
			{
				receiveState.Connection.Abort(ex);
			}
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0006DD40 File Offset: 0x0006BF40
		private static void WriteCallback(IAsyncResult asyncResult)
		{
			CommandStream commandStream = (CommandStream)asyncResult.AsyncState;
			try
			{
				try
				{
					commandStream.EndWrite(asyncResult);
				}
				catch (IOException)
				{
					commandStream.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				Stream stream = null;
				if (!commandStream.PostSendCommandProcessing(ref stream))
				{
					commandStream.ContinueCommandPipeline();
				}
			}
			catch (Exception ex)
			{
				commandStream.Abort(ex);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x0006DDB8 File Offset: 0x0006BFB8
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x0006DDC0 File Offset: 0x0006BFC0
		protected Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
				this._decoder = this._encoding.GetDecoder();
			}
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool CheckValid(ResponseDescription response, ref int validThrough, ref int completeLength)
		{
			return false;
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0006DDDC File Offset: 0x0006BFDC
		private ResponseDescription ReceiveCommandResponse()
		{
			ReceiveState receiveState = new ReceiveState(this);
			try
			{
				if (this._buffer.Length > 0)
				{
					this.ReceiveCommandResponseCallback(receiveState, -1);
				}
				else
				{
					try
					{
						if (this._isAsync)
						{
							this.BeginRead(receiveState.Buffer, 0, receiveState.Buffer.Length, CommandStream.s_readCallbackDelegate, receiveState);
							return null;
						}
						int num = this.Read(receiveState.Buffer, 0, receiveState.Buffer.Length);
						if (num == 0)
						{
							base.CloseSocket();
						}
						this.ReceiveCommandResponseCallback(receiveState, num);
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is WebException)
				{
					throw;
				}
				throw this.GenerateException("The underlying connection was closed: An unexpected error occurred on a receive", WebExceptionStatus.ReceiveFailure, ex);
			}
			return receiveState.Resp;
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0006DEB4 File Offset: 0x0006C0B4
		private void ReceiveCommandResponseCallback(ReceiveState state, int bytesRead)
		{
			int num = -1;
			for (;;)
			{
				int validThrough = state.ValidThrough;
				if (this._buffer.Length > 0)
				{
					state.Resp.StatusBuffer.Append(this._buffer);
					this._buffer = string.Empty;
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						break;
					}
				}
				else
				{
					if (bytesRead <= 0)
					{
						goto Block_3;
					}
					char[] array = new char[this._decoder.GetCharCount(state.Buffer, 0, bytesRead)];
					int chars = this._decoder.GetChars(state.Buffer, 0, bytesRead, array, 0, false);
					string text = new string(array, 0, chars);
					state.Resp.StatusBuffer.Append(text);
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						goto Block_4;
					}
					if (num >= 0)
					{
						int num2 = state.Resp.StatusBuffer.Length - num;
						if (num2 > 0)
						{
							this._buffer = text.Substring(text.Length - num2, num2);
						}
					}
				}
				if (num < 0)
				{
					state.ValidThrough = validThrough;
					try
					{
						if (this._isAsync)
						{
							this.BeginRead(state.Buffer, 0, state.Buffer.Length, CommandStream.s_readCallbackDelegate, state);
							return;
						}
						bytesRead = this.Read(state.Buffer, 0, state.Buffer.Length);
						if (bytesRead == 0)
						{
							base.CloseSocket();
						}
						continue;
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					goto IL_017B;
				}
				goto IL_017B;
			}
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			Block_3:
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			Block_4:
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			IL_017B:
			string text2 = state.Resp.StatusBuffer.ToString();
			state.Resp.StatusDescription = text2.Substring(0, num);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("Received response: {0}", new object[] { text2.Substring(0, num - 2) }), "ReceiveCommandResponseCallback");
			}
			if (this._isAsync)
			{
				if (state.Resp != null)
				{
					this._currentResponseDescription = state.Resp;
				}
				Stream stream = null;
				if (this.PostReadCommandProcessing(ref stream))
				{
					return;
				}
				this.ContinueCommandPipeline();
			}
		}

		// Token: 0x04000FC8 RID: 4040
		private static readonly AsyncCallback s_writeCallbackDelegate = new AsyncCallback(CommandStream.WriteCallback);

		// Token: 0x04000FC9 RID: 4041
		private static readonly AsyncCallback s_readCallbackDelegate = new AsyncCallback(CommandStream.ReadCallback);

		// Token: 0x04000FCA RID: 4042
		private bool _recoverableFailure;

		// Token: 0x04000FCB RID: 4043
		protected WebRequest _request;

		// Token: 0x04000FCC RID: 4044
		protected bool _isAsync;

		// Token: 0x04000FCD RID: 4045
		private bool _aborted;

		// Token: 0x04000FCE RID: 4046
		protected CommandStream.PipelineEntry[] _commands;

		// Token: 0x04000FCF RID: 4047
		protected int _index;

		// Token: 0x04000FD0 RID: 4048
		private bool _doRead;

		// Token: 0x04000FD1 RID: 4049
		private bool _doSend;

		// Token: 0x04000FD2 RID: 4050
		private ResponseDescription _currentResponseDescription;

		// Token: 0x04000FD3 RID: 4051
		protected string _abortReason;

		// Token: 0x04000FD4 RID: 4052
		private const int WaitingForPipeline = 1;

		// Token: 0x04000FD5 RID: 4053
		private const int CompletedPipeline = 2;

		// Token: 0x04000FD6 RID: 4054
		private string _buffer = string.Empty;

		// Token: 0x04000FD7 RID: 4055
		private Encoding _encoding = Encoding.UTF8;

		// Token: 0x04000FD8 RID: 4056
		private Decoder _decoder;

		// Token: 0x02000392 RID: 914
		internal enum PipelineInstruction
		{
			// Token: 0x04000FDA RID: 4058
			Abort,
			// Token: 0x04000FDB RID: 4059
			Advance,
			// Token: 0x04000FDC RID: 4060
			Pause,
			// Token: 0x04000FDD RID: 4061
			Reread,
			// Token: 0x04000FDE RID: 4062
			GiveStream
		}

		// Token: 0x02000393 RID: 915
		[Flags]
		internal enum PipelineEntryFlags
		{
			// Token: 0x04000FE0 RID: 4064
			UserCommand = 1,
			// Token: 0x04000FE1 RID: 4065
			GiveDataStream = 2,
			// Token: 0x04000FE2 RID: 4066
			CreateDataConnection = 4,
			// Token: 0x04000FE3 RID: 4067
			DontLogParameter = 8
		}

		// Token: 0x02000394 RID: 916
		internal class PipelineEntry
		{
			// Token: 0x06001E2F RID: 7727 RVA: 0x0006E100 File Offset: 0x0006C300
			internal PipelineEntry(string command)
			{
				this.Command = command;
			}

			// Token: 0x06001E30 RID: 7728 RVA: 0x0006E10F File Offset: 0x0006C30F
			internal PipelineEntry(string command, CommandStream.PipelineEntryFlags flags)
			{
				this.Command = command;
				this.Flags = flags;
			}

			// Token: 0x06001E31 RID: 7729 RVA: 0x0006E125 File Offset: 0x0006C325
			internal bool HasFlag(CommandStream.PipelineEntryFlags flags)
			{
				return (this.Flags & flags) > (CommandStream.PipelineEntryFlags)0;
			}

			// Token: 0x04000FE4 RID: 4068
			internal string Command;

			// Token: 0x04000FE5 RID: 4069
			internal CommandStream.PipelineEntryFlags Flags;
		}
	}
}

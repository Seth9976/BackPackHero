using System;

namespace System.IO.Ports
{
	// Token: 0x0200083E RID: 2110
	internal static class InternalResources
	{
		// Token: 0x06004308 RID: 17160 RVA: 0x000E98C7 File Offset: 0x000E7AC7
		internal static void EndOfFile()
		{
			throw new EndOfStreamException(SR.GetString("Unable to read beyond the end of the stream."));
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x000E98D8 File Offset: 0x000E7AD8
		internal static string GetMessage(int errorCode)
		{
			return SR.GetString("Unknown Error '{0}'.", new object[] { errorCode });
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x000E98F3 File Offset: 0x000E7AF3
		internal static void FileNotOpen()
		{
			throw new ObjectDisposedException(null, SR.GetString("The port is closed."));
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x000E9905 File Offset: 0x000E7B05
		internal static void WrongAsyncResult()
		{
			throw new ArgumentException(SR.GetString("IAsyncResult object did not come from the corresponding async method on this type."));
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x000E9916 File Offset: 0x000E7B16
		internal static void EndReadCalledTwice()
		{
			throw new ArgumentException(SR.GetString("EndRead can only be called once for each asynchronous operation."));
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x000E9927 File Offset: 0x000E7B27
		internal static void EndWriteCalledTwice()
		{
			throw new ArgumentException(SR.GetString("EndWrite can only be called once for each asynchronous operation."));
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x000E9938 File Offset: 0x000E7B38
		internal static void WinIOError(int errorCode, string str)
		{
			if (errorCode <= 5)
			{
				if (errorCode - 2 > 1)
				{
					if (errorCode == 5)
					{
						if (str.Length == 0)
						{
							throw new UnauthorizedAccessException(SR.GetString("Access to the path is denied."));
						}
						throw new UnauthorizedAccessException(SR.GetString("Access to the path '{0}' is denied.", new object[] { str }));
					}
				}
				else
				{
					if (str.Length == 0)
					{
						throw new IOException(SR.GetString("The specified port does not exist."));
					}
					throw new IOException(SR.GetString("The port '{0}' does not exist.", new object[] { str }));
				}
			}
			else if (errorCode != 32)
			{
				if (errorCode == 206)
				{
					throw new PathTooLongException(SR.GetString("The specified file name or path is too long, or a component of the specified path is too long."));
				}
			}
			else
			{
				if (str.Length == 0)
				{
					throw new IOException(SR.GetString("The process cannot access the file because it is being used by another process."));
				}
				throw new IOException(SR.GetString("The process cannot access the file '{0}' because it is being used by another process.", new object[] { str }));
			}
			throw new IOException(InternalResources.GetMessage(errorCode), InternalResources.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x0600430F RID: 17167 RVA: 0x000E9A24 File Offset: 0x000E7C24
		internal static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}
	}
}

using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000D6 RID: 214
	public static class ES3Stream
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x00021BE0 File Offset: 0x0001FDE0
		public static Stream CreateStream(ES3Settings settings, ES3FileMode fileMode)
		{
			bool flag = fileMode > ES3FileMode.Read;
			Stream stream = null;
			new FileInfo(settings.FullPath);
			Stream stream2;
			try
			{
				if (settings.location == ES3.Location.InternalMS)
				{
					if (!flag)
					{
						return null;
					}
					stream = new MemoryStream(settings.bufferSize);
				}
				else if (settings.location == ES3.Location.File)
				{
					if (!flag && !ES3IO.FileExists(settings.FullPath))
					{
						return null;
					}
					stream = new ES3FileStream(settings.FullPath, fileMode, settings.bufferSize, false);
				}
				else if (settings.location == ES3.Location.PlayerPrefs)
				{
					if (flag)
					{
						stream = new ES3PlayerPrefsStream(settings.FullPath, settings.bufferSize, fileMode == ES3FileMode.Append);
					}
					else
					{
						if (!PlayerPrefs.HasKey(settings.FullPath))
						{
							return null;
						}
						stream = new ES3PlayerPrefsStream(settings.FullPath);
					}
				}
				else if (settings.location == ES3.Location.Resources)
				{
					if (!flag)
					{
						ES3ResourcesStream es3ResourcesStream = new ES3ResourcesStream(settings.FullPath);
						if (!es3ResourcesStream.Exists)
						{
							es3ResourcesStream.Dispose();
							return null;
						}
						stream = es3ResourcesStream;
					}
					else
					{
						if (Application.isEditor)
						{
							throw new NotSupportedException("Cannot write directly to Resources folder. Try writing to a directory outside of Resources, and then manually move the file there.");
						}
						throw new NotSupportedException("Cannot write to Resources folder at runtime. Use a different save location at runtime instead.");
					}
				}
				stream2 = ES3Stream.CreateStream(stream, settings, fileMode);
			}
			catch (Exception ex)
			{
				if (stream != null)
				{
					stream.Dispose();
				}
				throw ex;
			}
			return stream2;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00021D10 File Offset: 0x0001FF10
		public static Stream CreateStream(Stream stream, ES3Settings settings, ES3FileMode fileMode)
		{
			Stream stream2;
			try
			{
				bool flag = fileMode > ES3FileMode.Read;
				if (settings.encryptionType != ES3.EncryptionType.None && stream.GetType() != typeof(UnbufferedCryptoStream))
				{
					EncryptionAlgorithm encryptionAlgorithm = null;
					if (settings.encryptionType == ES3.EncryptionType.AES)
					{
						encryptionAlgorithm = new AESEncryptionAlgorithm();
					}
					stream = new UnbufferedCryptoStream(stream, !flag, settings.encryptionPassword, settings.bufferSize, encryptionAlgorithm);
				}
				if (settings.compressionType != ES3.CompressionType.None && stream.GetType() != typeof(GZipStream) && settings.compressionType == ES3.CompressionType.Gzip)
				{
					stream = (flag ? new GZipStream(stream, CompressionMode.Compress) : new GZipStream(stream, CompressionMode.Decompress));
				}
				stream2 = stream;
			}
			catch (Exception ex)
			{
				if (stream != null)
				{
					stream.Dispose();
				}
				if (ex.GetType() == typeof(CryptographicException))
				{
					throw new CryptographicException("Could not decrypt file. Please ensure that you are using the same password used to encrypt the file.");
				}
				throw ex;
			}
			return stream2;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00021DEC File Offset: 0x0001FFEC
		public static void CopyTo(Stream source, Stream destination)
		{
			source.CopyTo(destination);
		}
	}
}

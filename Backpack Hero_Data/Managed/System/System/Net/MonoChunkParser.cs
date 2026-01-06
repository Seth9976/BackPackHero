using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Net
{
	// Token: 0x020004B3 RID: 1203
	internal class MonoChunkParser
	{
		// Token: 0x060026D5 RID: 9941 RVA: 0x0008FFD0 File Offset: 0x0008E1D0
		public MonoChunkParser(WebHeaderCollection headers)
		{
			this.headers = headers;
			this.saved = new StringBuilder();
			this.chunks = new ArrayList();
			this.chunkSize = -1;
			this.totalWritten = 0;
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x00090003 File Offset: 0x0008E203
		public void WriteAndReadBack(byte[] buffer, int offset, int size, ref int read)
		{
			if (offset + read > 0)
			{
				this.Write(buffer, offset, offset + read);
			}
			read = this.Read(buffer, offset, size);
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x00090026 File Offset: 0x0008E226
		public int Read(byte[] buffer, int offset, int size)
		{
			return this.ReadFromChunks(buffer, offset, size);
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x00090034 File Offset: 0x0008E234
		private int ReadFromChunks(byte[] buffer, int offset, int size)
		{
			int count = this.chunks.Count;
			int num = 0;
			List<MonoChunkParser.Chunk> list = new List<MonoChunkParser.Chunk>(count);
			for (int i = 0; i < count; i++)
			{
				MonoChunkParser.Chunk chunk = (MonoChunkParser.Chunk)this.chunks[i];
				if (chunk.Offset == chunk.Bytes.Length)
				{
					list.Add(chunk);
				}
				else
				{
					num += chunk.Read(buffer, offset + num, size - num);
					if (num == size)
					{
						break;
					}
				}
			}
			foreach (MonoChunkParser.Chunk chunk2 in list)
			{
				this.chunks.Remove(chunk2);
			}
			return num;
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000900F0 File Offset: 0x0008E2F0
		public void Write(byte[] buffer, int offset, int size)
		{
			if (offset < size)
			{
				this.InternalWrite(buffer, ref offset, size);
			}
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x00090100 File Offset: 0x0008E300
		private void InternalWrite(byte[] buffer, ref int offset, int size)
		{
			if (this.state == MonoChunkParser.State.None || this.state == MonoChunkParser.State.PartialSize)
			{
				this.state = this.GetChunkSize(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.PartialSize)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (this.state == MonoChunkParser.State.Body && offset < size)
			{
				this.state = this.ReadBody(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.Body)
				{
					return;
				}
			}
			if (this.state == MonoChunkParser.State.BodyFinished && offset < size)
			{
				this.state = this.ReadCRLF(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.BodyFinished)
				{
					return;
				}
				this.sawCR = false;
			}
			if (this.state == MonoChunkParser.State.Trailer && offset < size)
			{
				this.state = this.ReadTrailer(buffer, ref offset, size);
				if (this.state == MonoChunkParser.State.Trailer)
				{
					return;
				}
				this.saved.Length = 0;
				this.sawCR = false;
				this.gotit = false;
			}
			if (offset < size)
			{
				this.InternalWrite(buffer, ref offset, size);
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x000901F5 File Offset: 0x0008E3F5
		public bool WantMore
		{
			get
			{
				return this.chunkRead != this.chunkSize || this.chunkSize != 0 || this.state > MonoChunkParser.State.None;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x00090218 File Offset: 0x0008E418
		public bool DataAvailable
		{
			get
			{
				int count = this.chunks.Count;
				for (int i = 0; i < count; i++)
				{
					MonoChunkParser.Chunk chunk = (MonoChunkParser.Chunk)this.chunks[i];
					if (chunk != null && chunk.Bytes != null && chunk.Bytes.Length != 0 && chunk.Offset < chunk.Bytes.Length)
					{
						return this.state != MonoChunkParser.State.Body;
					}
				}
				return false;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x00090281 File Offset: 0x0008E481
		public int TotalDataSize
		{
			get
			{
				return this.totalWritten;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x00090289 File Offset: 0x0008E489
		public int ChunkLeft
		{
			get
			{
				return this.chunkSize - this.chunkRead;
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x00090298 File Offset: 0x0008E498
		private MonoChunkParser.State ReadBody(byte[] buffer, ref int offset, int size)
		{
			if (this.chunkSize == 0)
			{
				return MonoChunkParser.State.BodyFinished;
			}
			int num = size - offset;
			if (num + this.chunkRead > this.chunkSize)
			{
				num = this.chunkSize - this.chunkRead;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(buffer, offset, array, 0, num);
			this.chunks.Add(new MonoChunkParser.Chunk(array));
			offset += num;
			this.chunkRead += num;
			this.totalWritten += num;
			if (this.chunkRead != this.chunkSize)
			{
				return MonoChunkParser.State.Body;
			}
			return MonoChunkParser.State.BodyFinished;
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0009032C File Offset: 0x0008E52C
		private MonoChunkParser.State GetChunkSize(byte[] buffer, ref int offset, int size)
		{
			this.chunkRead = 0;
			this.chunkSize = 0;
			char c = '\0';
			while (offset < size)
			{
				int num = offset;
				offset = num + 1;
				c = (char)buffer[num];
				if (c == '\r')
				{
					if (this.sawCR)
					{
						MonoChunkParser.ThrowProtocolViolation("2 CR found");
					}
					this.sawCR = true;
				}
				else
				{
					if (this.sawCR && c == '\n')
					{
						break;
					}
					if (c == ' ')
					{
						this.gotit = true;
					}
					if (!this.gotit)
					{
						this.saved.Append(c);
					}
					if (this.saved.Length > 20)
					{
						MonoChunkParser.ThrowProtocolViolation("chunk size too long.");
					}
				}
			}
			if (!this.sawCR || c != '\n')
			{
				if (offset < size)
				{
					MonoChunkParser.ThrowProtocolViolation("Missing \\n");
				}
				try
				{
					if (this.saved.Length > 0)
					{
						this.chunkSize = int.Parse(MonoChunkParser.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
					}
				}
				catch (Exception)
				{
					MonoChunkParser.ThrowProtocolViolation("Cannot parse chunk size.");
				}
				return MonoChunkParser.State.PartialSize;
			}
			this.chunkRead = 0;
			try
			{
				this.chunkSize = int.Parse(MonoChunkParser.RemoveChunkExtension(this.saved.ToString()), NumberStyles.HexNumber);
			}
			catch (Exception)
			{
				MonoChunkParser.ThrowProtocolViolation("Cannot parse chunk size.");
			}
			if (this.chunkSize == 0)
			{
				this.trailerState = 2;
				return MonoChunkParser.State.Trailer;
			}
			return MonoChunkParser.State.Body;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x00090484 File Offset: 0x0008E684
		private static string RemoveChunkExtension(string input)
		{
			int num = input.IndexOf(';');
			if (num == -1)
			{
				return input;
			}
			return input.Substring(0, num);
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000904A8 File Offset: 0x0008E6A8
		private MonoChunkParser.State ReadCRLF(byte[] buffer, ref int offset, int size)
		{
			if (!this.sawCR)
			{
				int num = offset;
				offset = num + 1;
				if (buffer[num] != 13)
				{
					MonoChunkParser.ThrowProtocolViolation("Expecting \\r");
				}
				this.sawCR = true;
				if (offset == size)
				{
					return MonoChunkParser.State.BodyFinished;
				}
			}
			if (this.sawCR)
			{
				int num = offset;
				offset = num + 1;
				if (buffer[num] != 10)
				{
					MonoChunkParser.ThrowProtocolViolation("Expecting \\n");
				}
			}
			return MonoChunkParser.State.None;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x00090508 File Offset: 0x0008E708
		private MonoChunkParser.State ReadTrailer(byte[] buffer, ref int offset, int size)
		{
			if (this.trailerState == 2 && buffer[offset] == 13 && this.saved.Length == 0)
			{
				offset++;
				if (offset < size && buffer[offset] == 10)
				{
					offset++;
					return MonoChunkParser.State.None;
				}
				offset--;
			}
			int num = this.trailerState;
			while (offset < size && num < 4)
			{
				int num2 = offset;
				offset = num2 + 1;
				char c = (char)buffer[num2];
				if ((num == 0 || num == 2) && c == '\r')
				{
					num++;
				}
				else if ((num == 1 || num == 3) && c == '\n')
				{
					num++;
				}
				else if (num >= 0)
				{
					this.saved.Append(c);
					num = 0;
					if (this.saved.Length > 4196)
					{
						MonoChunkParser.ThrowProtocolViolation("Error reading trailer (too long).");
					}
				}
			}
			if (num < 4)
			{
				this.trailerState = num;
				if (offset < size)
				{
					MonoChunkParser.ThrowProtocolViolation("Error reading trailer.");
				}
				return MonoChunkParser.State.Trailer;
			}
			StringReader stringReader = new StringReader(this.saved.ToString());
			string text;
			while ((text = stringReader.ReadLine()) != null && text != "")
			{
				this.headers.Add(text);
			}
			return MonoChunkParser.State.None;
		}

		// Token: 0x060026E4 RID: 9956 RVA: 0x0009061E File Offset: 0x0008E81E
		private static void ThrowProtocolViolation(string message)
		{
			throw new WebException(message, null, WebExceptionStatus.ServerProtocolViolation, null);
		}

		// Token: 0x0400167B RID: 5755
		private WebHeaderCollection headers;

		// Token: 0x0400167C RID: 5756
		private int chunkSize;

		// Token: 0x0400167D RID: 5757
		private int chunkRead;

		// Token: 0x0400167E RID: 5758
		private int totalWritten;

		// Token: 0x0400167F RID: 5759
		private MonoChunkParser.State state;

		// Token: 0x04001680 RID: 5760
		private StringBuilder saved;

		// Token: 0x04001681 RID: 5761
		private bool sawCR;

		// Token: 0x04001682 RID: 5762
		private bool gotit;

		// Token: 0x04001683 RID: 5763
		private int trailerState;

		// Token: 0x04001684 RID: 5764
		private ArrayList chunks;

		// Token: 0x020004B4 RID: 1204
		private enum State
		{
			// Token: 0x04001686 RID: 5766
			None,
			// Token: 0x04001687 RID: 5767
			PartialSize,
			// Token: 0x04001688 RID: 5768
			Body,
			// Token: 0x04001689 RID: 5769
			BodyFinished,
			// Token: 0x0400168A RID: 5770
			Trailer
		}

		// Token: 0x020004B5 RID: 1205
		private class Chunk
		{
			// Token: 0x060026E5 RID: 9957 RVA: 0x0009062A File Offset: 0x0008E82A
			public Chunk(byte[] chunk)
			{
				this.Bytes = chunk;
			}

			// Token: 0x060026E6 RID: 9958 RVA: 0x0009063C File Offset: 0x0008E83C
			public int Read(byte[] buffer, int offset, int size)
			{
				int num = ((size > this.Bytes.Length - this.Offset) ? (this.Bytes.Length - this.Offset) : size);
				Buffer.BlockCopy(this.Bytes, this.Offset, buffer, offset, num);
				this.Offset += num;
				return num;
			}

			// Token: 0x0400168B RID: 5771
			public byte[] Bytes;

			// Token: 0x0400168C RID: 5772
			public int Offset;
		}
	}
}

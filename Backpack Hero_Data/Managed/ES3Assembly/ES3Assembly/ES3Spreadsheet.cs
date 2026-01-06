using System;
using System.Collections.Generic;
using System.IO;
using ES3Internal;

// Token: 0x0200000B RID: 11
public class ES3Spreadsheet
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000458D File Offset: 0x0000278D
	public int ColumnCount
	{
		get
		{
			return this.cols;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004595 File Offset: 0x00002795
	public int RowCount
	{
		get
		{
			return this.rows;
		}
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x000045A0 File Offset: 0x000027A0
	public void SetCell<T>(int col, int row, T value)
	{
		if (value.GetType() == typeof(string))
		{
			this.SetCellString(col, row, (string)((object)value));
			return;
		}
		ES3Settings es3Settings = new ES3Settings(null, null);
		if (ES3Reflection.IsPrimitive(value.GetType()))
		{
			this.SetCellString(col, row, value.ToString());
		}
		else
		{
			this.SetCellString(col, row, es3Settings.encoding.GetString(ES3.Serialize<T>(value, null)));
		}
		if (col >= this.cols)
		{
			this.cols = col + 1;
		}
		if (row >= this.rows)
		{
			this.rows = row + 1;
		}
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00004650 File Offset: 0x00002850
	private void SetCellString(int col, int row, string value)
	{
		this.cells[new ES3Spreadsheet.Index(col, row)] = value;
		if (col >= this.cols)
		{
			this.cols = col + 1;
		}
		if (row >= this.rows)
		{
			this.rows = row + 1;
		}
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000468C File Offset: 0x0000288C
	public T GetCell<T>(int col, int row)
	{
		object cell = this.GetCell(typeof(T), col, row);
		if (cell == null)
		{
			return default(T);
		}
		return (T)((object)cell);
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x000046C0 File Offset: 0x000028C0
	internal object GetCell(Type type, int col, int row)
	{
		if (col >= this.cols || row >= this.rows)
		{
			throw new IndexOutOfRangeException(string.Concat(new string[]
			{
				"Cell (",
				col.ToString(),
				", ",
				row.ToString(),
				") is out of bounds of spreadsheet (",
				this.cols.ToString(),
				", ",
				this.rows.ToString(),
				")."
			}));
		}
		string text;
		if (!this.cells.TryGetValue(new ES3Spreadsheet.Index(col, row), out text) || string.IsNullOrEmpty(text))
		{
			return null;
		}
		if (type == typeof(string))
		{
			return text;
		}
		ES3Settings es3Settings = new ES3Settings(null, null);
		return ES3.Deserialize(ES3TypeMgr.GetOrCreateES3Type(type, true), es3Settings.encoding.GetBytes(text), es3Settings);
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x0000479E File Offset: 0x0000299E
	public void Load(string filePath)
	{
		this.Load(new ES3Settings(filePath, null));
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x000047AD File Offset: 0x000029AD
	public void Load(string filePath, ES3Settings settings)
	{
		this.Load(new ES3Settings(filePath, settings));
	}

	// Token: 0x060000DA RID: 218 RVA: 0x000047BC File Offset: 0x000029BC
	public void Load(ES3Settings settings)
	{
		this.Load(ES3Stream.CreateStream(settings, ES3FileMode.Read), settings);
	}

	// Token: 0x060000DB RID: 219 RVA: 0x000047CC File Offset: 0x000029CC
	public void LoadRaw(string str)
	{
		this.Load(new MemoryStream(new ES3Settings(null, null).encoding.GetBytes(str)), new ES3Settings(null, null));
	}

	// Token: 0x060000DC RID: 220 RVA: 0x000047F2 File Offset: 0x000029F2
	public void LoadRaw(string str, ES3Settings settings)
	{
		this.Load(new MemoryStream(settings.encoding.GetBytes(str)), settings);
	}

	// Token: 0x060000DD RID: 221 RVA: 0x0000480C File Offset: 0x00002A0C
	private void Load(Stream stream, ES3Settings settings)
	{
		using (StreamReader streamReader = new StreamReader(stream))
		{
			string text = "";
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				int num3 = streamReader.Read();
				char c = (char)num3;
				if (c == '"')
				{
					for (;;)
					{
						c = (char)streamReader.Read();
						if (c == '"')
						{
							if ((ushort)streamReader.Peek() != 34)
							{
								break;
							}
							c = (char)streamReader.Read();
						}
						text += c.ToString();
					}
				}
				else if (c == ',' || c == '\n' || num3 == -1)
				{
					this.SetCell<string>(num, num2, text);
					text = "";
					if (c == ',')
					{
						num++;
					}
					else
					{
						if (c != '\n')
						{
							break;
						}
						num = 0;
						num2++;
					}
				}
				else
				{
					text += c.ToString();
				}
			}
		}
	}

	// Token: 0x060000DE RID: 222 RVA: 0x000048D8 File Offset: 0x00002AD8
	public void Save(string filePath)
	{
		this.Save(new ES3Settings(filePath, null), false);
	}

	// Token: 0x060000DF RID: 223 RVA: 0x000048E8 File Offset: 0x00002AE8
	public void Save(string filePath, ES3Settings settings)
	{
		this.Save(new ES3Settings(filePath, settings), false);
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x000048F8 File Offset: 0x00002AF8
	public void Save(ES3Settings settings)
	{
		this.Save(settings, false);
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x00004902 File Offset: 0x00002B02
	public void Save(string filePath, bool append)
	{
		this.Save(new ES3Settings(filePath, null), append);
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00004912 File Offset: 0x00002B12
	public void Save(string filePath, ES3Settings settings, bool append)
	{
		this.Save(new ES3Settings(filePath, settings), append);
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00004924 File Offset: 0x00002B24
	public void Save(ES3Settings settings, bool append)
	{
		using (StreamWriter streamWriter = new StreamWriter(ES3Stream.CreateStream(settings, append ? ES3FileMode.Append : ES3FileMode.Write)))
		{
			if (append && ES3.FileExists(settings))
			{
				streamWriter.Write('\n');
			}
			string[,] array = this.ToArray();
			for (int i = 0; i < this.rows; i++)
			{
				if (i != 0)
				{
					streamWriter.Write('\n');
				}
				for (int j = 0; j < this.cols; j++)
				{
					if (j != 0)
					{
						streamWriter.Write(',');
					}
					streamWriter.Write(ES3Spreadsheet.Escape(array[j, i], false));
				}
			}
		}
		if (!append)
		{
			ES3IO.CommitBackup(settings);
		}
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x000049D0 File Offset: 0x00002BD0
	private static string Escape(string str, bool isAlreadyWrappedInQuotes = false)
	{
		if (string.IsNullOrEmpty(str))
		{
			return null;
		}
		if (str.Contains("\""))
		{
			str = str.Replace("\"", "\"\"");
		}
		if (str.IndexOfAny(ES3Spreadsheet.CHARS_TO_ESCAPE) > -1)
		{
			str = "\"" + str + "\"";
		}
		return str;
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00004A28 File Offset: 0x00002C28
	private static string Unescape(string str)
	{
		if (str.StartsWith("\"") && str.EndsWith("\""))
		{
			str = str.Substring(1, str.Length - 2);
			if (str.Contains("\"\""))
			{
				str = str.Replace("\"\"", "\"");
			}
		}
		return str;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00004A80 File Offset: 0x00002C80
	private string[,] ToArray()
	{
		string[,] array = new string[this.cols, this.rows];
		foreach (KeyValuePair<ES3Spreadsheet.Index, string> keyValuePair in this.cells)
		{
			array[keyValuePair.Key.col, keyValuePair.Key.row] = keyValuePair.Value;
		}
		return array;
	}

	// Token: 0x04000016 RID: 22
	private int cols;

	// Token: 0x04000017 RID: 23
	private int rows;

	// Token: 0x04000018 RID: 24
	private Dictionary<ES3Spreadsheet.Index, string> cells = new Dictionary<ES3Spreadsheet.Index, string>();

	// Token: 0x04000019 RID: 25
	private const string QUOTE = "\"";

	// Token: 0x0400001A RID: 26
	private const char QUOTE_CHAR = '"';

	// Token: 0x0400001B RID: 27
	private const char COMMA_CHAR = ',';

	// Token: 0x0400001C RID: 28
	private const char NEWLINE_CHAR = '\n';

	// Token: 0x0400001D RID: 29
	private const string ESCAPED_QUOTE = "\"\"";

	// Token: 0x0400001E RID: 30
	private static char[] CHARS_TO_ESCAPE = new char[] { ',', '"', '\n', ' ' };

	// Token: 0x020000E8 RID: 232
	protected struct Index
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x00022C6A File Offset: 0x00020E6A
		public Index(int col, int row)
		{
			this.col = col;
			this.row = row;
		}

		// Token: 0x0400017B RID: 379
		public int col;

		// Token: 0x0400017C RID: 380
		public int row;
	}
}

using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using ES3Internal;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class ES3Settings : ICloneable
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000129 RID: 297 RVA: 0x000050DF File Offset: 0x000032DF
	public static ES3Defaults defaultSettingsScriptableObject
	{
		get
		{
			if (ES3Settings._defaultSettingsScriptableObject == null)
			{
				ES3Settings._defaultSettingsScriptableObject = Resources.Load<ES3Defaults>("ES3/ES3Defaults");
			}
			return ES3Settings._defaultSettingsScriptableObject;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600012A RID: 298 RVA: 0x00005102 File Offset: 0x00003302
	public static ES3Settings defaultSettings
	{
		get
		{
			if (ES3Settings._defaults == null && ES3Settings.defaultSettingsScriptableObject != null)
			{
				ES3Settings._defaults = ES3Settings.defaultSettingsScriptableObject.settings;
			}
			return ES3Settings._defaults;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600012B RID: 299 RVA: 0x0000512C File Offset: 0x0000332C
	internal static ES3Settings unencryptedUncompressedSettings
	{
		get
		{
			if (ES3Settings._unencryptedUncompressedSettings == null)
			{
				ES3Settings._unencryptedUncompressedSettings = new ES3Settings(new Enum[]
				{
					ES3.EncryptionType.None,
					ES3.CompressionType.None
				});
			}
			return ES3Settings._unencryptedUncompressedSettings;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x0600012C RID: 300 RVA: 0x0000515C File Offset: 0x0000335C
	// (set) Token: 0x0600012D RID: 301 RVA: 0x00005180 File Offset: 0x00003380
	public ES3.Location location
	{
		get
		{
			if (this._location == ES3.Location.File && (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.tvOS))
			{
				return ES3.Location.PlayerPrefs;
			}
			return this._location;
		}
		set
		{
			this._location = value;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600012E RID: 302 RVA: 0x0000518C File Offset: 0x0000338C
	public string FullPath
	{
		get
		{
			if (this.path == null)
			{
				throw new NullReferenceException("The 'path' field of this ES3Settings is null, indicating that it was not possible to load the default settings from Resources. Please check that the ES3 Default Settings.prefab exists in Assets/Plugins/Resources/ES3/");
			}
			if (ES3Settings.IsAbsolute(this.path))
			{
				return this.path;
			}
			if (this.location == ES3.Location.File)
			{
				if (this.directory == ES3.Directory.PersistentDataPath)
				{
					return ES3IO.persistentDataPath + "/" + this.path;
				}
				if (this.directory == ES3.Directory.DataPath)
				{
					return Application.dataPath + "/" + this.path;
				}
				throw new NotImplementedException("File directory \"" + this.directory.ToString() + "\" has not been implemented.");
			}
			else
			{
				if (this.location != ES3.Location.Resources)
				{
					return this.path;
				}
				string extension = Path.GetExtension(this.path);
				bool flag = false;
				foreach (string text in ES3Settings.resourcesExtensions)
				{
					if (extension == text)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new ArgumentException("Extension of file in Resources must be .json, .bytes, .txt, .csv, .htm, .html, .xml, .yaml or .fnt, but path given was \"" + this.path + "\"");
				}
				return this.path.Replace(extension, "");
			}
		}
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0000529F File Offset: 0x0000349F
	public ES3Settings(string path = null, ES3Settings settings = null)
		: this(true)
	{
		if (settings != null)
		{
			settings.CopyInto(this);
		}
		if (path != null)
		{
			this.path = path;
		}
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000052BC File Offset: 0x000034BC
	public ES3Settings(string path, params Enum[] enums)
		: this(enums)
	{
		if (path != null)
		{
			this.path = path;
		}
	}

	// Token: 0x06000131 RID: 305 RVA: 0x000052D0 File Offset: 0x000034D0
	public ES3Settings(params Enum[] enums)
		: this(true)
	{
		foreach (Enum @enum in enums)
		{
			if (@enum is ES3.EncryptionType)
			{
				this.encryptionType = (ES3.EncryptionType)@enum;
			}
			else if (@enum is ES3.Location)
			{
				this.location = (ES3.Location)@enum;
			}
			else if (@enum is ES3.CompressionType)
			{
				this.compressionType = (ES3.CompressionType)@enum;
			}
			else if (@enum is ES3.ReferenceMode)
			{
				this.referenceMode = (ES3.ReferenceMode)@enum;
			}
			else if (@enum is ES3.Format)
			{
				this.format = (ES3.Format)@enum;
			}
			else if (@enum is ES3.Directory)
			{
				this.directory = (ES3.Directory)@enum;
			}
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00005380 File Offset: 0x00003580
	public ES3Settings(ES3.EncryptionType encryptionType, string encryptionPassword)
		: this(true)
	{
		this.encryptionType = encryptionType;
		this.encryptionPassword = encryptionPassword;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00005397 File Offset: 0x00003597
	public ES3Settings(string path, ES3.EncryptionType encryptionType, string encryptionPassword, ES3Settings settings = null)
		: this(path, settings)
	{
		this.encryptionType = encryptionType;
		this.encryptionPassword = encryptionPassword;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x000053B0 File Offset: 0x000035B0
	[EditorBrowsable(EditorBrowsableState.Never)]
	public ES3Settings(bool applyDefaults)
	{
		if (applyDefaults && ES3Settings.defaultSettings != null)
		{
			ES3Settings._defaults.CopyInto(this);
		}
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000544B File Offset: 0x0000364B
	private static bool IsAbsolute(string path)
	{
		return (path.Length > 0 && (path[0] == '/' || path[0] == '\\')) || (path.Length > 1 && path[1] == ':');
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00005488 File Offset: 0x00003688
	[EditorBrowsable(EditorBrowsableState.Never)]
	public object Clone()
	{
		ES3Settings es3Settings = new ES3Settings(null, null);
		this.CopyInto(es3Settings);
		return es3Settings;
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000054A8 File Offset: 0x000036A8
	private void CopyInto(ES3Settings newSettings)
	{
		newSettings._location = this._location;
		newSettings.directory = this.directory;
		newSettings.format = this.format;
		newSettings.prettyPrint = this.prettyPrint;
		newSettings.path = this.path;
		newSettings.encryptionType = this.encryptionType;
		newSettings.encryptionPassword = this.encryptionPassword;
		newSettings.compressionType = this.compressionType;
		newSettings.bufferSize = this.bufferSize;
		newSettings.encoding = this.encoding;
		newSettings.typeChecking = this.typeChecking;
		newSettings.safeReflection = this.safeReflection;
		newSettings.memberReferenceMode = this.memberReferenceMode;
		newSettings.assemblyNames = this.assemblyNames;
		newSettings.saveChildren = this.saveChildren;
		newSettings.serializationDepthLimit = this.serializationDepthLimit;
	}

	// Token: 0x04000029 RID: 41
	private static ES3Settings _defaults = null;

	// Token: 0x0400002A RID: 42
	private static ES3Defaults _defaultSettingsScriptableObject;

	// Token: 0x0400002B RID: 43
	private const string defaultSettingsPath = "ES3/ES3Defaults";

	// Token: 0x0400002C RID: 44
	private static ES3Settings _unencryptedUncompressedSettings = null;

	// Token: 0x0400002D RID: 45
	private static readonly string[] resourcesExtensions = new string[] { ".txt", ".htm", ".html", ".xml", ".bytes", ".json", ".csv", ".yaml", ".fnt" };

	// Token: 0x0400002E RID: 46
	[SerializeField]
	private ES3.Location _location;

	// Token: 0x0400002F RID: 47
	public string path = "SaveFile.es3";

	// Token: 0x04000030 RID: 48
	public ES3.EncryptionType encryptionType;

	// Token: 0x04000031 RID: 49
	public ES3.CompressionType compressionType;

	// Token: 0x04000032 RID: 50
	public string encryptionPassword = "password";

	// Token: 0x04000033 RID: 51
	public ES3.Directory directory;

	// Token: 0x04000034 RID: 52
	public ES3.Format format;

	// Token: 0x04000035 RID: 53
	public bool prettyPrint = true;

	// Token: 0x04000036 RID: 54
	public int bufferSize = 2048;

	// Token: 0x04000037 RID: 55
	public Encoding encoding = Encoding.UTF8;

	// Token: 0x04000038 RID: 56
	public bool saveChildren = true;

	// Token: 0x04000039 RID: 57
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool typeChecking = true;

	// Token: 0x0400003A RID: 58
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool safeReflection = true;

	// Token: 0x0400003B RID: 59
	[EditorBrowsable(EditorBrowsableState.Never)]
	public ES3.ReferenceMode memberReferenceMode;

	// Token: 0x0400003C RID: 60
	[EditorBrowsable(EditorBrowsableState.Never)]
	public ES3.ReferenceMode referenceMode = ES3.ReferenceMode.ByRefAndValue;

	// Token: 0x0400003D RID: 61
	[EditorBrowsable(EditorBrowsableState.Never)]
	public int serializationDepthLimit = 64;

	// Token: 0x0400003E RID: 62
	[EditorBrowsable(EditorBrowsableState.Never)]
	public string[] assemblyNames = new string[] { "Assembly-CSharp-firstpass", "Assembly-CSharp" };
}

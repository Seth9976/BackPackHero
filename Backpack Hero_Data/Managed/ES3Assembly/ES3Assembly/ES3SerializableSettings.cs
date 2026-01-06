using System;
using System.ComponentModel;

// Token: 0x02000010 RID: 16
[EditorBrowsable(EditorBrowsableState.Never)]
[Serializable]
public class ES3SerializableSettings : ES3Settings
{
	// Token: 0x06000139 RID: 313 RVA: 0x000055E5 File Offset: 0x000037E5
	public ES3SerializableSettings()
		: base(false)
	{
	}

	// Token: 0x0600013A RID: 314 RVA: 0x000055EE File Offset: 0x000037EE
	public ES3SerializableSettings(bool applyDefaults)
		: base(applyDefaults)
	{
	}

	// Token: 0x0600013B RID: 315 RVA: 0x000055F7 File Offset: 0x000037F7
	public ES3SerializableSettings(string path)
		: base(false)
	{
		this.path = path;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00005607 File Offset: 0x00003807
	public ES3SerializableSettings(string path, ES3.Location location)
		: base(false)
	{
		base.location = location;
	}
}

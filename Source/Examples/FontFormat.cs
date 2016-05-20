using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
	internal class FontFormat
	{
		/// <summary>
		/// Gets the name for the format.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the typical file extension for this format (lowercase).
		/// </summary>
		public string FileExtension { get; private set; }

		// ...

		public FontFormat(string name, string ext)
		{
			if (!ext.StartsWith(".")) ext = "." + ext;
			this.Name = name; this.FileExtension = ext;
		}

	}

	internal class FontFormatCollection : Dictionary<string, FontFormat>
	{

		public void Add(string name, string ext)
		{
			if (!ext.StartsWith(".")) ext = "." + ext;
			this.Add(ext, new FontFormat(name, ext));
		}

		public bool ContainsExt(string ext)
		{
			return this.ContainsKey(ext);
		}

	}

}

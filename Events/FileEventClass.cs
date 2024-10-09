using System;
using System.IO;

namespace Hmw.Events;

class FileEventClass
{
	public event EventHandler<FileArgs> FileFound;

	public void enumerateFiles (string dirPath)
	{
		foreach (var fi in new DirectoryInfo (dirPath).EnumerateFiles ())
		{
			var fileArgs = new FileArgs { fi = fi };
			FileFound?.Invoke (this, fileArgs);
			if (fileArgs.Cancel)
			{
				Console.WriteLine ("обработка прервана");
				break;
			}
		}

	}
}

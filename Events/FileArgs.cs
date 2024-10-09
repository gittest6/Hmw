using System.ComponentModel;
using System.IO;

namespace Hmw.Events;
class FileArgs : CancelEventArgs
{
	public FileInfo fi { get; set; }
}

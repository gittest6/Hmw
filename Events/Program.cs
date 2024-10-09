using System;
using System.Collections.Generic;
using System.IO;
using Hmw.Events;

var listFi = new List<FileInfo>();
var fec = new FileEventClass ();

fec.FileFound += processEvent;
fec.enumerateFiles ("C:\\WINDOWS");

var fiWithMaxSize = listFi.GetMax (fi => fi.Length);
Console.WriteLine ($"{fiWithMaxSize.Name} - {fiWithMaxSize.Length}");

void processEvent (object sender, FileArgs e)
{
	Console.WriteLine ($"файл {e.fi.Name}");
	if (e.fi.Name == "win.ini")
		e.Cancel = true;
	listFi.Add (e.fi);
}

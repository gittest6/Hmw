using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hmw.Parallel;

var dirPath = Environment.GetFolderPath (Environment.SpecialFolder.UserProfile);
var ft = new FileTasks ();
int spaceCount;

var enumerationOptions = new EnumerationOptions () { RecurseSubdirectories = true };
var collFilePaths = Directory.EnumerateFiles (dirPath, "*", enumerationOptions).Take (3);
var collTasks = collFilePaths.Select (filePath => ft.CountSpacesInFile (filePath));
var arrResults = await Task.WhenAll (collTasks);
spaceCount = arrResults.Sum ();
Console.WriteLine (spaceCount);
if (!ft.cqErrorMessages.IsEmpty)
    foreach (var msg in ft.cqErrorMessages)
        Console.WriteLine (msg);

var sw = new Stopwatch ();
foreach (var parallel in new[] { true, false })
{
    sw.Restart ();
    spaceCount = await ft.CountSpacesInDirFiles (dirPath, parallel);
    sw.Stop ();
    Console.WriteLine (spaceCount);
    Console.WriteLine (sw.Elapsed);
    if (!ft.cqErrorMessages.IsEmpty)
        foreach (var msg in ft.cqErrorMessages)
            Console.WriteLine (msg);
}

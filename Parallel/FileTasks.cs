using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hmw.Parallel;

class FileTasks
{
    public ConcurrentQueue<string> cqErrorMessages = new ();
    
    public async Task<int> CountSpacesInFile (string path, int blockSize = 10000)
    {
        int spaceCount = 0;
        try
        {
            var buffer = new char[blockSize];
            using var reader = new StreamReader (path);
            while (true)
            {
                var readCount = await reader.ReadBlockAsync (buffer, 0, buffer.Length);
                if (readCount == 0) break;
                if (readCount < buffer.Length) Array.Resize (ref buffer, readCount);
                spaceCount += buffer.Count (c => c == ' ');
            }
        }
        catch (Exception ex)
        {
            cqErrorMessages.Enqueue (ex.Message);
        }
        return spaceCount;
    }

    public async Task<int> CountSpacesInDirFiles (string dirPath, bool parallel = true)
    {
        var enumerationOptions = new EnumerationOptions () { RecurseSubdirectories = true };
        var collFilePaths = Directory.EnumerateFiles (dirPath, "*", enumerationOptions);
        cqErrorMessages.Clear ();

        var collTasks = collFilePaths.Select (filePath => CountSpacesInFile (filePath));
        if (parallel)
        {
            var arrResults = await Task.WhenAll (collTasks);
            return arrResults.Sum ();
        }
        else
            return collTasks.Select (task => task.Result).Sum ();
    }
}

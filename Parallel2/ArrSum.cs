using BenchmarkDotNet.Attributes;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class ArrSum
{
    [Params (100000, 1000000, 10000000)]
    public int length;

    int[] array;

    long total;

    int threadCount;
    int[][] threadRanges;

    [GlobalSetup]
    public void GlobalSetup ()
    {
        array = new int[length];
        var random = new Random ();
        for (var i = 0; i < length; i++)
            array[i] = random.Next ();
    }

    [Benchmark]
    public void SingleThread ()
    {
        total = 0;
        for (var i = 0; i < length; i++)
            total += array[i];
    }

    void MultiThreadSetup ()
    {
        threadCount = Environment.ProcessorCount;
        var partSize = length / threadCount;

        threadRanges = new int[threadCount][];
        for (var i = 0; i < threadCount; i++)
        {
            threadRanges[i] = new int[2];
            threadRanges[i][0] = i * partSize;
            threadRanges[i][1] = (i + 1) * partSize - 1;
        }
    }

    [Benchmark]
    public void ParallelThread ()
    {
        MultiThreadSetup ();
        total = 0;
        var threads = new Thread[threadCount];
        for (var i = 0; i < threadCount; i++)
        {
            threads[i] = new Thread (ThreadProc);
            threads[i].Start (threadRanges[i]);
        }
        for (var i = 0; i < threadCount; i++)
            threads[i].Join ();
    }

    void ThreadProc (object objIndexes)
    {
        var indexes = (int[]) objIndexes;
        long sum = 0;
        for (var i = indexes[0]; i <= indexes[1]; i++)
            sum += array[i];

        Interlocked.Add (ref total, sum);
    }

    [Benchmark]
    public void ParallelLinq ()
    {
        MultiThreadSetup ();
        total = 0;

        threadRanges.AsParallel ().ForAll (indexes =>
        {
            long sum = 0;
            for (var i = indexes[0]; i <= indexes[1]; i++)
                sum += array[i];
            Interlocked.Add (ref total, sum);
        });
    }

    [Benchmark]
    public void ParallelForEach ()
    {
        MultiThreadSetup ();
        total = 0;

        Parallel.ForEach (threadRanges, indexes =>
        {
            long sum = 0;
            for (var i = indexes[0]; i <= indexes[1]; i++)
                sum += array[i];
            Interlocked.Add (ref total, sum);
        });
    }
}

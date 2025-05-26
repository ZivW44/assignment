using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

class Program
{
    static HashSet<int> daysScheduledByInvestor = new HashSet<int>();

    static int countMeetings(int[] firstDay, int[] lastDay)
    {
        daysScheduledByInvestor.Clear();
        int n = firstDay.Length;
        List<(int start, int end)> investors = new List<(int, int)>();
        for (int i = 0; i < n; i++)
        {
            investors.Add((firstDay[i], lastDay[i]));
        }

        investors.Sort((a, b) =>
        {
            if (a.end == b.end) return a.start.CompareTo(b.start);
            return a.end.CompareTo(b.end);
        });

        int meetings = 0;
        foreach (var (start, end) in investors)
        {
            for (int d = start; d <= end; d++)
            {
                if (!daysScheduledByInvestor.Contains(d))
                {
                    daysScheduledByInvestor.Add(d);
                    meetings++;
                    break;
                }
            }
        }

        return meetings;
    }

    static void Main2()
    {
        Stopwatch sw = Stopwatch.StartNew();
        int n = 100000;
        int[] firstDay = Enumerable.Repeat(1, n).ToArray();
        int[] lastDay = Enumerable.Repeat(100000, n).ToArray();
        int expectedMeetings = 100000;
        Console.WriteLine(countMeetings(firstDay, lastDay) == expectedMeetings);
        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
    }
}

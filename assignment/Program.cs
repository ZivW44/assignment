using System.Diagnostics;
using System.Linq;

public record InvestorMeeting (int firstDay, int lastDay);

public class MeetingCounter
{
    public static void Main(string[] args)
    {
        Stopwatch sw = Stopwatch.StartNew();
        int n = 100000;
        int[] firstDay = Enumerable.Repeat(1, n).ToArray();
        int[] lastDay = Enumerable.Repeat(100000, n).ToArray();
        int expectedMeetings = 100000;
        Console.WriteLine(countMeetings(firstDay, lastDay) == expectedMeetings);
        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);

        //foreach (var item in daysScheduledByInvestor)
        //{
        //    Console.WriteLine($"{item.Key}:{item.Value}");
        //}

    }

    //Implementation of 'interface' 
    static HashSet<int> daysScheduledByInvestor = new HashSet<int>();
    static bool isDayTaken(int day) => daysScheduledByInvestor.Contains(day);
    static int countMeetings() => daysScheduledByInvestor.Count;
    static void setupAMeet(int day) => daysScheduledByInvestor.Add(day);
   
    //The assigment
    static int countMeetings(int[] firstDay, int[] lastDay)
    {
        if (!areMeetingsDaysValid(firstDay, lastDay))
            throw new ArgumentException("Inputs are invalid");

        List<InvestorMeeting> investors = sortInvestors(firstDay, lastDay);

        foreach (var investor in investors)
        {
            for (int day = investor.firstDay; day <= investor.lastDay; day++)
            {
                if (!isDayTaken(day))
                {
                    setupAMeet(day);
                    break;
                }
            }
        }

        return countMeetings();

    }

    //Helper method
    private static List<InvestorMeeting> sortInvestors(int[] firstDay, int[] lastDay)
    {
        var investors = new List<InvestorMeeting>();
        for (int i = 0; i < firstDay.Length; i++)
        {
            investors.Add(new InvestorMeeting(firstDay[i], lastDay[i]));
        }

        investors.Sort((first,second) => {
            if (first.lastDay == second.lastDay) 
                return first.firstDay.CompareTo(second.firstDay);

            return first.lastDay.CompareTo(second.lastDay);
        });

        return investors;
    }

    //Validation
    static bool areMeetingsDaysValid(int[] firstDay, int[] lastDay)
    {
        int minDay = 1;
        double maxDay = Math.Pow(10, 5);
        if (firstDay == null || lastDay == null)
            return false;

        if (firstDay.Length != lastDay.Length)
            return false;

        if (firstDay.Length == 0)
            return false;

        for (int i = 0; i < firstDay.Length; i++)
        {
            if (firstDay[i] > lastDay[i])
                return false;

            if (firstDay[i] > maxDay ||
                lastDay[i] > maxDay)
                return false;


            if (firstDay[i] < minDay ||
                lastDay[i] < minDay)
                return false;
        }

        return true;
    }
}
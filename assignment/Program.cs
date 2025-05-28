using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int[] firstDay = [1, 2, 3, 3, 3] ;
            int[] lastDay = [2, 2, 3, 4, 4];
            var q1 = new Question1_MeetingCounter();
            var result1 = q1.countMeetings(firstDay, lastDay);
            Console.WriteLine(result1); //4


            string[] words = ["desserts", "stressed", "bats", "stabs", "are", "not"];
            string[] phrases = ["bats are not stressed"];
            var q2 = new Question2_Anagrams();
            var result2 = q2.substitutions(words, phrases);
            Console.WriteLine($"[{string.Join(", ", result2)}]"); //[2]

        }
    }
}

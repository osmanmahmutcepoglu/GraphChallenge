using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphChallenge
{
    internal class Program
    {
        public class Neighbours
        {
            public string Head { get; set; }
            public List<string> Ways { get; set; }
        }
        static void Main(string[] args)
        {
            string[] strArr = { "(A,B,C,D)", "(A-B,A-D,B-D,A-C)", "(C,A,D,B)" };

            Console.WriteLine(HamiltonianPath(strArr));
        }
        public static string HamiltonianPath(string[] strArr)
        {
            for (int i = 0; i < strArr.Length; i++)
            {
                strArr[i] = strArr[i].Replace("(", "").Replace(")", "");
            }

            List<Neighbours> neighboursList = new List<Neighbours>();

            string[] neighbours = strArr[0].Split(',');
            string[] ways = strArr[1].Split(',');
            List<string> path = strArr[2].Split(',').ToList();

            foreach (string str in neighbours)
            {
                neighboursList.Add(new Neighbours { Head = str, Ways = new List<string> { } });
            }

            foreach (var item in ways)
            {
                string[] waysArray = item.Split('-');
                var neighbourA = neighboursList.FirstOrDefault(x => x.Head == waysArray[0]);
                var neighbourB = neighboursList.FirstOrDefault(x => x.Head == waysArray[1]);
                if (neighbourA != null)
                {
                    neighbourA.Ways.Add(waysArray[1].ToString());
                }
                if (neighbourB != null)
                {
                    neighbourB.Ways.Add(waysArray[0].ToString());
                }
            }
            string firstHead = path[0];
            path.RemoveAt(0);

            return lookup(neighboursList, firstHead, path);
        }
        public static string lookup(List<Neighbours> tree, string head, List<string> rest)
        {
            if (rest.Count == 0)
            {
                return "yes";
            }
            var currentNeighbours = tree.FirstOrDefault(x => x.Head == head);
            if (!currentNeighbours.Ways.Any(x => x == rest[0]))
            {
                return head;
            }
            string current = rest[0];
            rest.RemoveAt(0);
            return lookup(tree, current, rest);
        }
    }
}

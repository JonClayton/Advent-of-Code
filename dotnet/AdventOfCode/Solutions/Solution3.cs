using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

namespace AdventOfCode.Solutions
{
    public class Solution3 : Solution
    {
        protected override int FirstSolution(List<string> lines)
        {
            return 0;
            // var result_array = most_common_array(lines);
            // var gamma = calculate_binary_value(result_array);
            // var epsilon = calculate_binary_value((invert_binary_array(result_array)));
            // return gamma * epsilon;
        }

        protected override int SecondSolution(List<string> lines) => 0;
        // MapLinesToActions(lines).Aggregate(StartingLocation(), CalculateLocationWithAim).Take(2).Product();

        // private List<int> MostCommonList(List<List<int>> observations)
        // {
        //     // observations.Aggregate(GetDictionaries(observations.First().Count), WriteObservations)
        // }
        //
        // private static IEnumerable<Dictionary<int, int>> WriteObservations(IEnumerable<Dictionary<int, int>> dictionaries, List<int> nums)
        // {
        //     // nums.Aggregate(dictionaries, num, )
        // }
        //
        // private IEnumerable<Dictionary<int, int>> GetDictionaries(int n) => Enumerable.Range(0, n).Select(i => new Dictionary<int, int>());
    }
}
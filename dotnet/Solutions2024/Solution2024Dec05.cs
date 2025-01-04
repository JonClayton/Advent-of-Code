using OldAdventOfCode.Classes2024;

namespace OldAdventOfCode.Solutions2024;

public class Solution2024Dec05 : Solution2024
{
    protected override long FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(IEnumerable<string> lines, bool isFirst)
    {
        long firstResult = 0;
        long secondResult = 0;
        var rulesDictionary = new Dictionary<int, List<int>>();

        var rules = new List<List<int>>();
        var updates = new List<List<int>>();
        var isRules = true;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isRules = false;
                continue;
            }
            if (isRules) rules.Add(line!.Trim().Split("|").Select(int.Parse).ToList());
            else updates.Add(line.Trim().Split(",").Select(int.Parse).ToList());
        }

        foreach (var update in updates)
        {
            rulesDictionary.Clear();
            var updateSet = update.ToHashSet();
            foreach (var rule in rules.Where(rule =>
                         updateSet.Contains(rule[0]) && updateSet.Contains(rule[1]) &&
                         !rulesDictionary.TryAdd(rule[0], [rule[1]])))
                rulesDictionary[rule[0]].Add(rule[1]);

            if (TryValidateOrderAndGetValid(update, out var correctOrder)) firstResult += update[update.Count / 2];
            else secondResult += correctOrder[update.Count / 2];
        }

        return isFirst ? firstResult : secondResult;
        
        bool TryValidateOrderAndGetValid(List<int> update, out List<int> correctOrder)
        {
            (var pageOrder, correctOrder) = BuildPageOrder(update.ToHashSet());
            var current = pageOrder[update[0]];
            foreach (var location in update.Select(page => pageOrder[page]))
            {
                if (location < current) return false;
                current = location;
            }

            return true;
        }

        (Dictionary<int, int>, List<int>) BuildPageOrder(HashSet<int> pagesInUpdate)
        {
            var pageOrderDict = new Dictionary<int, int>();
            var pageOrderList = new List<int>();
            while (rulesDictionary.Count > 0)
            {
                var afters = rulesDictionary.Values.SelectMany(page => page).ToHashSet();
                var possibleEntries = rulesDictionary.Keys.Where(page => !afters.Contains(page)).ToList();
                foreach (var entry in possibleEntries)
                {
                    AddEntry(entry);
                    rulesDictionary.Remove(entry);
                    if (rulesDictionary.Keys.Where(pagesInUpdate.Contains).Any() ||
                        pageOrderDict.Keys.ToHashSet().IsSupersetOf(pagesInUpdate)) continue;
                    var lastItem = pagesInUpdate.First(item => !pageOrderDict.ContainsKey(item));
                    AddEntry(lastItem);
                }
            }
            
            return (pageOrderDict, pageOrderList);

            void AddEntry(int entry)
            {
                pageOrderDict.Add(entry, pageOrderDict.Count);
                pageOrderList.Add(entry);
            }
        }
    }
}
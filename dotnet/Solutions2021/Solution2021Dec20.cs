using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Solutions2021;

public class Solution2021Dec20 : Solution
{
    private char[] _algorithm;
    private const char Dot = '.';
    private const char Pound = '#';
    
    protected override long FirstSolution(List<string> lines)
    {
        _algorithm = lines.First().ToCharArray();
        var image = lines.GetRange(2, lines.Count - 2).Select(s => s.ToCharArray().ToList()).ToList();
        image = EnhanceImage(image, 2);
        return image.SelectMany(i => i).Count(c => c.Equals(Pound));
    }

    private List<List<char>> EnhanceImage(List<List<char>> image, int i)
    {
        while (i > 0)
        {
            image = AdjustBorder(image);
            image.ForEach(row => Console.WriteLine(new string(row.ToArray())));
            Console.WriteLine("-----------------------");
            image = image.Select((chars, row) => chars.Select((c, col) => ApplyAlgorithm(image, row, col)).ToList()).ToList();
            i--;
            image.ForEach(row => Console.WriteLine(new string(row.ToArray())));
            Console.WriteLine("-----------------------");
        }

        
        return image;
    }

    private static List<List<char>> AdjustBorder(List<List<char>> image)
    {
        while (image.GetRange(0, 2).SelectMany(row => row).Any(c => c == Pound))
            image.Insert(0, Enumerable.Repeat(Dot, image[0].Count).ToList());
        while (image.GetRange(image.Count -2, 2).SelectMany(row => row).Any(c => c == Pound))
            image.Add(Enumerable.Repeat(Dot, image[0].Count).ToList());
        var check = image.Aggregate(string.Empty, (acc, row) => acc + new string(row.GetRange(0, 2).ToArray()));
        while (image.Aggregate(string.Empty, (acc, row) => acc + new string(row.GetRange(0, 2).ToArray())).Any(c => c == Pound))
            image.ForEach(row => row.Insert(0, Dot));
        while (image.Aggregate(string.Empty, (acc, row) => acc + new string(row.GetRange(row.Count - 2 , 2).ToArray())).Any(c => c == Pound))
            image.ForEach(row => row.Add(Dot));
        return image;
    }

    private char ApplyAlgorithm(List<List<char>> image, int row, int col)
    {
        if (row == 0 || col == 0 || row == image.Count - 1 || col == image.Count - 1) return Dot;
        var nineValues = image.GetRange(row - 1, 3).SelectMany(chars => chars.GetRange(col - 1, 3)).ToArray();
        var str = new string(nineValues).Replace(Dot,'0').Replace(Pound,'1');
        var num = Convert.ToInt32(str, 2);
        return _algorithm[num];
    }

    protected override long SecondSolution(List<string> lines) => 5;
}

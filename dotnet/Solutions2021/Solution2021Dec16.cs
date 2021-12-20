using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Transactions;
using System.Xml.Schema;
using AdventOfCode.Classes;
using AdventOfCode.Utilities;

namespace AdventOfCode.Solutions2021;

public class Solution2021Dec16 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var num = 4;
        var bitString = string.Join(string.Empty, lines[0].ToCharArray().Select(ConvertHexCharToBinaryString));
        var packet = new Packet(lines);
        return 42;
    }

    private string ConvertHexCharToBinaryString(char hex)
    {
        var hexString = Convert.ToString(Convert.ToByte(hex.ToString(), 16), 2);
        return new string('0', 4 - hexString.Length) + hexString;
        // var chars = input.ToCharArray();
        // var strings = chars.Select(c => Convert.ToString(Convert.ToByte(c.ToString(), 16), 2));
        // return string.Join(string.Empty, strings);
    }

    protected override long SecondSolution(List<string> lines) => 5;

    private class Packet
    {
        public string BinaryString;
        private int _packageVersion;
        private int _packetTypeId;
        private int _literalValue;
        private List<Packet> _subPackets = new();
        
        public Packet(IEnumerable<string> lines)
        {
            var binaryString = string.Join(string.Empty, lines.First().ToCharArray().Select(ConvertHexTo4BitString));
            ParseBinaryString(binaryString);
        }
        
        private Packet(string binaryString)
        {
            ParseBinaryString(binaryString);
        }

        private void ParseBinaryString(string binaryString)
        {
            BinaryString = binaryString;
            _packageVersion = Convert.ToInt32(BinaryString[..3], 2);
            _packetTypeId = Convert.ToInt32(BinaryString[3..6], 2);
            if (_packetTypeId.Equals(4)) HandleLiteralValue();
            else HandleOperator();
        }

        public void Print()
        {
            Console.WriteLine(_literalValue);
        }

        private static string ConvertHexTo4BitString(char hex)
        {
            var hexString = Convert.ToString(Convert.ToByte(hex.ToString(), 16), 2);
            return new string('0', 4 - hexString.Length) + hexString;
        }

        private void HandleLiteralValue()
        {
            var currentIndex = 6;
            var literalValueString = string.Empty;
            while (true)
            {
                var chunk = BinaryString.Substring(currentIndex, 5);
                literalValueString += chunk[1..5];
                if (chunk[..1] == "0") break;
                currentIndex += 5;
            }

            _literalValue = Convert.ToInt32(literalValueString, 2);
            BinaryString = BinaryString[0..(currentIndex + 6)];
        }

        private void HandleOperator()
        {
            var lengthTypeId = BinaryString[6..7];
            if (lengthTypeId == "1") ParseSubPacketsByCount();
            else ParseSubPacketsByLength();
        }

        private void ParseSubPacketsByCount()
        {
            throw new NotImplementedException();
        }
        
        private void ParseSubPacketsByLength()
        {
            var length = Convert.ToInt32(BinaryString[7..22], 2);
            var lengthRemaining = 0;
            BinaryString = BinaryString[0..(22 + length)];
            var stringToRead = BinaryString[22..(22+length)];
            var index = 0;
            while (stringToRead.Length > 0)
            {
                var nextPacket = new Packet(stringToRead);
                _subPackets.Add(nextPacket);
                stringToRead = stringToRead.Remove(0, nextPacket.BinaryString.Length);
            } 
        }
    }
}
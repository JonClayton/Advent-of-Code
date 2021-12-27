namespace AdventOfCode.Solutions2021;

public class Solution2021Dec16 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var packet = new Packet(lines);
        return packet.PacketVersionSum;
    }

    protected override long SecondSolution(List<string> lines)
    {
        var packet = new Packet(lines);
        return packet.Value;
    }

    private class Packet
    {
        public long PacketVersionSum => _packetVersion + _subPackets.Select(s => s.PacketVersionSum).Sum();
        public long Value => _packetTypeId switch
                {
                    0 => _subPackets.Select(s => s.Value).Sum(),
                    1 => _subPackets.Select(s => s.Value).Product(),
                    2 => _subPackets.Select(s => s.Value).Min(),
                    3 => _subPackets.Select(s => s.Value).Max(),
                    5 => _subPackets[0].Value > _subPackets[1].Value ? 1 : 0,
                    6 => _subPackets[0].Value < _subPackets[1].Value ? 1 : 0,
                    7 => _subPackets[0].Value == _subPackets[1].Value ? 1 : 0,
                    _ => _literalValue
                };
        
        private string _binaryString;
        private long _literalValue;
        private int _packetTypeId;
        private int _packetVersion;
        private readonly List<Packet> _subPackets = new();

        public Packet(IEnumerable<string> lines)
        {
            var binaryString = string.Join(string.Empty, lines.First().ToCharArray().Select(hex =>
            {
                var hexString = Convert.ToString(Convert.ToByte(hex.ToString(), 16), 2);
                return new string('0', 4 - hexString.Length) + hexString;
            }));
            ParseBinaryString(binaryString);
        }
        
        private Packet(string binaryString)
        {
            ParseBinaryString(binaryString);
        }

        private void ParseBinaryString(string binaryString)
        {
            _binaryString = binaryString;
            _packetVersion = Convert.ToInt32(_binaryString[..3], 2);
            _packetTypeId = Convert.ToInt32(_binaryString[3..6], 2);
            if (_packetTypeId.Equals(4)) ParseLiteralValue();
            else
            {
                var lengthTypeId = _binaryString[6..7];
                if (lengthTypeId == "1") ParseSubPacketsByCount();
                else ParseSubPacketsByLength();
            }
        }

        private void ParseLiteralValue()
        {
            var currentIndex = 6;
            var literalValueString = string.Empty;
            while (true)
            {
                var chunk = _binaryString.Substring(currentIndex, 5);
                literalValueString += chunk[1..5];
                if (chunk[..1] == "0") break;
                currentIndex += 5;
            }

            _literalValue = Convert.ToInt64(literalValueString, 2);
            _binaryString = _binaryString[0..(currentIndex + 5)];
        }

        private string ParseSubPacket(string stringToRead)
        {
            var nextPacket = new Packet(stringToRead);
            _subPackets.Add(nextPacket);
            return stringToRead.Remove(0, nextPacket._binaryString.Length);
        }
        
        private void ParseSubPacketsByCount()
        {
            var count = Convert.ToInt32(_binaryString[7..18], 2);
            var stringToRead = _binaryString[18..];
            while (count > 0)
            {
                stringToRead = ParseSubPacket(stringToRead);
                count--;
            }

            _binaryString = _binaryString[..18] +
                            _subPackets.Select(s => s._binaryString).Aggregate((acc, bs) => acc + bs);
        }
        
        private void ParseSubPacketsByLength()
        {
            var length = Convert.ToInt32(_binaryString[7..22], 2);
            _binaryString = _binaryString[..(22 + length)];
            var stringToRead = _binaryString[22..(22+length)];
            while (stringToRead.Length > 0) stringToRead = ParseSubPacket(stringToRead);
        }
    }
}
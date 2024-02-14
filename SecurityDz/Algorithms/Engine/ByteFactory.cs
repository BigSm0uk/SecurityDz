namespace SecurityDz.Algorithms.Engine;

public static class ByteFactory
{
    private static int GetBitLength(int number)
    {
        var bitLength = 0;
        while (number != 0)
        {
            number >>= 1;
            bitLength++;
        }

        return bitLength;
    }

    private static uint ToBinary(int value, int numBits)
    {
        return Convert.ToUInt32(Convert.ToString(value, 2).PadLeft(numBits, '0'), 2);
    }

    private static uint ToNormalInt(string value)
    {
        uint result = 0;
        for (var (i, k) = (value.Length - 1, 0); i >= 0; i--, k++)
        {
            result += (uint)Math.Pow(2, k);
        }

        return result;
    }
}
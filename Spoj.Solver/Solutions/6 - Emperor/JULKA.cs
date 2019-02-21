﻿using System;
using System.Collections.Generic;
using System.Linq;

// https://www.spoj.com/problems/JULKA/ #big-numbers #math
// Given the apples two girls have together and how many one has over the other, returns
// how many apples each girl has individually (you can just use System.Numerics for this now).
public static class JULKA
{
    public static Tuple<BigInteger, BigInteger> Solve(
        BigInteger totalApples, BigInteger extraKlaudiaApples)
    {
        // totalApples = nataliaApples + klaudiaApples
        // klaudiaApples = nataliaApples + extraKlaudiaApples
        // => totalApples = 2*nataliaApples + extraKlaudiaApples
        // => nataliaApples = (totalApples - extraKlaudiaApples)/2
        BigInteger nataliaApples = (totalApples - extraKlaudiaApples).DivideByTwo();
        BigInteger klaudiaApples = nataliaApples + extraKlaudiaApples;

        return Tuple.Create(klaudiaApples, nataliaApples);
    }
}

public struct BigInteger : IEquatable<BigInteger>
{
    private static readonly BigInteger _zero = new BigInteger(0);
    private static readonly BigInteger _one = new BigInteger(1);
    public static BigInteger Zero => _zero;
    public static BigInteger One => _one;

    private readonly IReadOnlyList<byte> _digits;

    private BigInteger(IReadOnlyList<byte> digits)
    {
        _digits = digits;
    }

    public BigInteger(int n)
        : this(n.ToString())
    { }

    // For a number string, the first character is the most significant digit. For our _digits
    // array, it's most convenient if the first element is the least significant digit, so
    // reverse the order here.
    public BigInteger(string digits)
    {
        byte[] digitsArray = new byte[digits.Length];
        for (int i = 0; i < digits.Length; ++i)
        {
            digitsArray[i] = byte.Parse(digits[digits.Length - i - 1].ToString());
        }

        _digits = Array.AsReadOnly(digitsArray);
    }

    public bool IsZero => this == Zero;
    public bool IsOne => this == One;

    public static BigInteger operator +(BigInteger a, BigInteger b)
    {
        // No more than multiplying the larger by 2, so can't require more than one extra digit.
        int maxDigitCount = Math.Max(a._digits.Count, b._digits.Count);
        var result = new List<byte>(maxDigitCount + 1);
        byte carry = 0;

        for (int i = 0; i < maxDigitCount; ++i)
        {
            byte value = carry;
            if (i < a._digits.Count)
            {
                value += a._digits[i];
            }
            if (i < b._digits.Count)
            {
                value += b._digits[i];
            }
            result.Add((byte)(value % 10));
            carry = (byte)(value / 10);
        }

        if (carry != 0)
        {
            result.Add(carry);
        }

        return new BigInteger(result.AsReadOnly());
    }

    public static BigInteger operator -(BigInteger a, BigInteger b)
    {
        // Assumption right now that a > b, since negatives aren't supported.
        var result = new List<byte>(a._digits.Count);
        byte carry = 0;

        // Rather than calculate a - b = result, we'll calculate the result such that b + result = a.
        for (int i = 0; i < a._digits.Count; ++i)
        {
            byte bAndCarry = carry;
            if (i < b._digits.Count)
            {
                bAndCarry += b._digits[i];
            }

            byte value;
            if (bAndCarry <= a._digits[i])
            {
                // value + bAndCarry = a._digits[i]; no carry is necessary to get the digit correct.
                value = (byte)(a._digits[i] - bAndCarry);
                carry = 0;
            }
            else
            {
                // value + bAndCarry = a._digits[i] + 10; a carry is necessary to get the digit correct.
                value = (byte)(a._digits[i] + 10 - bAndCarry);
                carry = 1;
            }
            result.Add(value);
        }

        RemoveTrailingZeros(result);

        return new BigInteger(result.AsReadOnly());
    }

    public static BigInteger operator *(BigInteger a, BigInteger b)
    {
        var result = BigInteger.Zero;
        for (int i = 0; i < b._digits.Count; ++i)
        {
            result += a.MultiplyByDigit(b._digits[i]).MultiplyByPowerOfTen(i);
        }

        return result;
    }

    private BigInteger MultiplyByDigit(byte digit)
    {
        if (IsZero || digit == 1) return this;
        if (digit == 0) return BigInteger.Zero;

        // Digit is less than 10 so the result can't require more than one extra digit.
        var result = new List<byte>(_digits.Count + 1);
        byte carry = 0;

        for (int i = 0; i < _digits.Count; i++)
        {
            byte value = (byte)(_digits[i] * digit + carry);
            result.Add((byte)(value % 10));
            carry = (byte)(value / 10);
        }

        if (carry != 0)
        {
            result.Add(carry);
        }

        return new BigInteger(result.AsReadOnly());
    }

    private BigInteger MultiplyByPowerOfTen(int power)
    {
        if (IsZero || power == 0) return this;

        byte[] result = new byte[_digits.Count + power];

        for (int i = 0; i < power; ++i)
        {
            result[i] = 0;
        }

        for (int i = 0; i < _digits.Count; ++i)
        {
            result[power + i] = _digits[i];
        }

        return new BigInteger(Array.AsReadOnly(result));
    }

    public BigInteger DivideByTwo()
    {
        var result = new List<byte>(_digits.Count);
        byte carry = 0;

        // Rather than calculate this / 2 = result, we'll calculate result + result = this.
        for (int i = 0; i < _digits.Count; ++i)
        {
            byte sum = (byte)(_digits[i] - carry);

            // There are two ways to create the digit, halving the sum or adding 10 to it
            // and then halving it, in the case where the carry is necessary because the
            // next digit isn't even and so can't be halved without first subtracting a carry.
            if (i < _digits.Count - 1 && _digits[i + 1] % 2 != 0)
            {
                sum += 10;
            }

            result.Add((byte)(sum / 2));
            carry = (byte)(sum / 10);
        }

        RemoveTrailingZeros(result);

        return new BigInteger(result.AsReadOnly());
    }

    private static void RemoveTrailingZeros(List<byte> result)
    {
        // Remove trailing zeroes from the result list, but not the first when result's all zeros.
        for (int i = result.Count - 1; i > 0; --i)
        {
            if (result[i] == 0)
            {
                result.RemoveAt(i);
            }
            else return;
        }
    }

    public override string ToString()
        => string.Concat(_digits.Reverse());

    public bool Equals(BigInteger other)
        => _digits.SequenceEqual(other._digits);

    public override bool Equals(object obj)
        => obj is BigInteger ? Equals((BigInteger)obj) : false;

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(BigInteger a, BigInteger b)
        => a.Equals(b);

    public static bool operator !=(BigInteger a, BigInteger b)
        => !(a == b);
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = 10;
        while (remainingTestCases-- > 0)
        {
            var appleCounts = JULKA.Solve(
                new BigInteger(Console.ReadLine()),
                new BigInteger(Console.ReadLine()));

            Console.WriteLine(appleCounts.Item1);
            Console.WriteLine(appleCounts.Item2);
        }
    }
}

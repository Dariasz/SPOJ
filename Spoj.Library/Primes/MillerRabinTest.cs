﻿using System;

namespace Spoj.Library.Primes
{
    public static class MillerRabinTest
    {
        private static readonly Random _rand = new Random();

        // https://en.wikipedia.org/wiki/Miller%E2%80%93Rabin_primality_test
        public static bool Run(ulong n, int witnessCount = 10)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3) return true;
            if ((n & 1) == 0) return false;

            ulong d = n - 1;
            int r = 0;
            while ((d & 1) == 0)
            {
                d >>= 1;
                ++r;
            }

            while (witnessCount-- > 0)
            {
                if (!Witness(n, d, r))
                    return false; // composite
            }

            return true; // probably prime
        }

        private static bool Witness(ulong n, ulong d, int r)
        {
            ulong a = (ulong)_rand.Next() % (n - 3) + 2;
            ulong x = ModularPow(a, d, n);

            if (x == 1 || x == n - 1)
                return true;

            while (--r > 0)
            {
                x = ModularMultiply(x, x, n);

                if (x == n - 1)
                    return true;
            }

            return false; // composite
        }

        // https://www.geeksforgeeks.org/how-to-avoid-overflow-in-modular-multiplication/
        private static ulong ModularMultiply(ulong a, ulong b, ulong modulus)
        {
            ulong result = 0;
            a = a % modulus;
            while (b > 0)
            {
                if ((b & 1) == 1)
                {
                    result = (result + a) % modulus;
                }
                a = (a << 1) % modulus;
                b >>= 1;
            }

            return result % modulus;
        }

        // https://en.wikipedia.org/wiki/Exponentiation_by_squaring
        // https://stackoverflow.com/a/383596
        // https://en.wikipedia.org/wiki/Modular_exponentiation#Right-to-left_binary_method
        private static ulong ModularPow(ulong @base, ulong exponent, ulong modulus)
        {
            ulong result = 1;
            @base = @base % modulus;
            while (exponent != 0)
            {
                if ((exponent & 1) == 1)
                {
                    result = ModularMultiply(result, @base, modulus);
                }

                @base = ModularMultiply(@base, @base, modulus);
                exponent >>= 1;
            }

            return result;
        }
    }
}

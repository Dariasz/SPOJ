using System;

// Adding Reversed Numbers
// 42 http://www.spoj.com/problems/ADDREV/
// Returns the reversed sum of two reversed integers.
public static class ADDREV
{
    public static int Solve(int a, int b)
        => (a.Reverse() + b.Reverse()).Reverse();

    private static int Reverse(this int a)
    {
        int reverse = 0;

        while (a != 0)
        {
            reverse = reverse * 10 + a % 10; // Make room for the next digit, and then add it.
            a = a / 10; // Remove the digit just added.
        }

        return reverse;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            string[] ints = Console.ReadLine().Split(' ');

            Console.WriteLine(
                ADDREV.Solve(int.Parse(ints[0]), int.Parse(ints[1])));
        }
    }
}
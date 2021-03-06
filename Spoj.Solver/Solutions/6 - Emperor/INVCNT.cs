﻿using System;

// https://www.spoj.com/problems/INVCNT/ #ad-hoc #bst
// Finds the number of inversions (larger index, but smaller value) in an array.
public static class INVCNT
{
    // The array size is limited to 200k elements, but that could be
    // 200k * (200k - 1) / 2 inversions, so we need to use long when counting.
    public static long Solve(int[] array)
    {
        var inversionBST = new InversionBST(array[0]);

        long inversionCount = 0;
        for (int i = 1; i < array.Length; ++i)
        {
            inversionCount += inversionBST.Add(array[i]);
        }

        return inversionCount;
    }

    public static long SolveSlowly(int[] array)
    {
        long inversionCount = 0;
        for (int i = 1; i < array.Length; ++i)
        {
            for (int j = i - 1; j >= 0; --j)
            {
                if (array[i] < array[j])
                {
                    ++inversionCount;
                }
            }
        }

        return inversionCount;
    }
}

public sealed class InversionBST
{
    private Node _root;

    public InversionBST(int rootValue)
    {
        _root = new Node(rootValue);
    }

    // The specialized Add method counts inversions. Nodes are added into the BST from the input
    // array left to right. The value being added is inverted with any values already in the BST
    // (that is, to its left in the array) that are larger than it. These are the values in
    // the right subtrees (and roots) it passes during the add. Subtree sizes are maintained on
    // each node (acting as a root for their subtree), so we don't need to count them each time.
    public int Add(int value)
    {
        int inversionCount = 0;
        Node node = _root;

        while (true)
        {
            // The value being added is going to the left or right; either way, node's subtree gets bigger.
            ++node.SubtreeSize;

            if (value < node.Value)
            {
                // Going to the left, so inverted with this node and everything in its right subtree.
                inversionCount += 1 + (node.RightChild?.SubtreeSize ?? 0);

                if (node.LeftChild == null)
                {
                    node.LeftChild = new Node(value);
                    return inversionCount;
                }

                node = node.LeftChild;
            }
            else
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new Node(value);
                    return inversionCount;
                }

                node = node.RightChild;
            }
        }
    }

    private sealed class Node
    {
        public Node(int value)
        {
            Value = value;
            SubtreeSize = 1;
        }

        public int Value { get; set; }
        public int SubtreeSize { get; set; }
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.ReadLine();

            int[] array = new int[int.Parse(Console.ReadLine())];
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine(
                INVCNT.Solve(array));
        }
    }
}

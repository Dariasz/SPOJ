﻿using System;
using System.Collections.Generic;
using System.Text;

// https://www.spoj.com/problems/GSS1/ #divide-and-conquer #research #segment-tree
// Does maximum sum subrange queries on an array.
public sealed class GSS1
{
    private readonly ArrayBasedSegmentTree _segmentTree;

    public GSS1(IReadOnlyList<int> sourceArray)
    {
        _segmentTree = new ArrayBasedSegmentTree(sourceArray);
    }

    public int Query(int queryStartIndex, int queryEndIndex)
        => _segmentTree.Query(queryStartIndex, queryEndIndex).MaximumSum;
}

// Most guides online cover this approach, but here's one good one:
// https://kartikkukreja.wordpress.com/2014/11/09/a-simple-approach-to-segment-trees/
public sealed class ArrayBasedSegmentTree
{
    private readonly IReadOnlyList<int> _sourceArray;
    private readonly MaximumSumQueryObject[] _treeArray;

    public ArrayBasedSegmentTree(IReadOnlyList<int> sourceArray)
    {
        _sourceArray = sourceArray;
        _treeArray = new MaximumSumQueryObject[2 * MathHelper.FirstPowerOfTwoEqualOrGreater(_sourceArray.Count) - 1];
        Build(0, 0, _sourceArray.Count - 1);
    }

    private void Build(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex)
    {
        if (segmentStartIndex == segmentEndIndex)
        {
            _treeArray[treeArrayIndex] = new MaximumSumQueryObject(_sourceArray[segmentStartIndex]);
            return;
        }

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;

        Build(leftChildTreeArrayIndex, segmentStartIndex, (segmentStartIndex + segmentEndIndex) / 2);
        Build(rightChildTreeArrayIndex, (segmentStartIndex + segmentEndIndex) / 2 + 1, segmentEndIndex);

        _treeArray[treeArrayIndex] = _treeArray[leftChildTreeArrayIndex].Combine(_treeArray[rightChildTreeArrayIndex]);
    }

    public MaximumSumQueryObject Query(int queryStartIndex, int queryEndIndex)
        => Query(0, 0, _sourceArray.Count - 1, queryStartIndex, queryEndIndex);

    private MaximumSumQueryObject Query(int treeArrayIndex, int segmentStartIndex, int segmentEndIndex, int queryStartIndex, int queryEndIndex)
    {
        if (queryStartIndex <= segmentStartIndex && queryEndIndex >= segmentEndIndex)
            return _treeArray[treeArrayIndex];

        int leftChildTreeArrayIndex = 2 * treeArrayIndex + 1;
        int rightChildTreeArrayIndex = leftChildTreeArrayIndex + 1;
        int leftChildSegmentEndIndex = (segmentStartIndex + segmentEndIndex) / 2;

        if (queryStartIndex <= leftChildSegmentEndIndex && queryEndIndex > leftChildSegmentEndIndex)
            return Query(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex, queryStartIndex, queryEndIndex)
                .Combine(Query(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex, queryStartIndex, queryEndIndex));
        else if (queryStartIndex <= leftChildSegmentEndIndex)
            return Query(leftChildTreeArrayIndex, segmentStartIndex, leftChildSegmentEndIndex, queryStartIndex, queryEndIndex);
        else
            return Query(rightChildTreeArrayIndex, leftChildSegmentEndIndex + 1, segmentEndIndex, queryStartIndex, queryEndIndex);
    }
}

public sealed class MaximumSumQueryObject
{
    private MaximumSumQueryObject()
    { }

    public MaximumSumQueryObject(int value)
    {
        Sum = value;
        MaximumSum = value;
        MaximumLeftStartingSum = value;
        MaximumRightStartingSum = value;
    }

    private int Sum { get; set; }
    public int MaximumSum { get; set; }
    private int MaximumLeftStartingSum { get; set; }
    private int MaximumRightStartingSum { get; set; }

    public MaximumSumQueryObject Combine(MaximumSumQueryObject rightAdjacentValue)
        => new MaximumSumQueryObject
        {
            // The sum is just the sum of both.
            Sum = Sum + rightAdjacentValue.Sum,

            // The maximum sum either intersects both segments, or is entirely in one.
            MaximumSum = Math.Max(
                MaximumRightStartingSum + rightAdjacentValue.MaximumLeftStartingSum,
                Math.Max(MaximumSum, rightAdjacentValue.MaximumSum)),

            // The maximum left starting sum starts at the left, and may or may not cross into the right.
            MaximumLeftStartingSum = Math.Max(
                Sum + rightAdjacentValue.MaximumLeftStartingSum,
                MaximumLeftStartingSum),

            // The maximum right starting sum starts at the right, and may or may not cross into the left.
            MaximumRightStartingSum = Math.Max(
                rightAdjacentValue.Sum + MaximumRightStartingSum,
                rightAdjacentValue.MaximumRightStartingSum)
        };
}

public static class MathHelper
{
    public static int FirstPowerOfTwoEqualOrGreater(int value)
    {
        int result = 1;
        while (result < value)
        {
            result <<= 1;
        }

        return result;
    }
}

public static class Program
{
    // Special I/O handling is necessary to work around malformed input and get the time fast enough.
    private static void Main()
    {
        int arrayLength = int.Parse(Console.ReadLine());
        int[] sourceArray = Array.ConvertAll(
            Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
            int.Parse);
        var solver = new GSS1(sourceArray);

        var output = new StringBuilder();
        int queryCount = int.Parse(Console.ReadLine());
        for (int q = 0; q < queryCount; ++q)
        {
            int[] line = Array.ConvertAll(
                Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                int.Parse);

            output.Append(solver.Query(
                queryStartIndex: line[0] - 1,
                queryEndIndex: line[1] - 1));
            output.AppendLine();
        }

        Console.Write(output);
    }
}

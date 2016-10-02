﻿using System.Collections.Generic;

namespace Spoj.Library.BinaryIndexedTrees
{
    // See PURQ and RUPQ before trying to understand this. We know BITs allow us to (quickly) add a value to
    // an index and have queries from that index onward increased by that value. That's the blackbox we're
    // working with, where Update(i, delta) increases all results of Query(j >= i) by delta. We must use that
    // idea to figure out how to accomplish new operations, RURQUpdate(i, j, delta) and RURQQuery(i, j). The
    // first does whatever it needs to do to make the second increase by (j - i + 1) * delta as it finds the
    // sum from i through j. Consider the following solution, which we'll show is correct:
    // RURQUpdate(i, j, delta) =>
    // BIT1: Update(i, delta),
    //       Update(j + 1, -delta).
    // BIT2: Update(i, delta * (i - 1))
    //       Update(j + 1, -delta * j)
    // RURQQuery(0, k): BIT1.Query(k) * k - BIT2.Query(k) [the sum from 0 through k; do ranges like PURQ]

    // Say a RURQUpdate has just run (and assume inductively RURQQuery was correctly computing sums before):
    // Querying before i isn't affected, since BIT updates only affect from an index onward, and all of
    // the indexes affected by RURQUpdate were >= i. Good, so querying before i is still correct.

    // Querying between i and j should be affected; say we're querying at i <= k <= j, then the
    // query result should be increased by (k - i + 1) * delta, as that's how the cumulative sum
    // is affected after a range update by delta. Well, the first update on BIT1 increases BIT1.Query(k)
    // by delta, and the second update on BIT1 does nothing to it as j + 1 is > k. The first update on
    // BIT2 increases BIT2.Query(k) by delta * (i - 1), the second update does nothing. Plugging these in,
    // RURQQuery(k) is increased by delta * k - (delta * (i - 1)) = delta * (k - i + 1), just what we want.

    // Querying after j should be affected, as it needs to reflect the delta * (j - i + 1) cumulative change
    // after the update. For BIT1, the second update kills the first for elements in this range. For BIT2,
    // the change of -delta * j is added to delta * (i - 1) to make a total change of delta * (i - 1 - j).
    // RURQQuery(k) negates that value though, so queries are increased by delta * (j - i + 1) as desired.

    // Using these two BITs, we can do RURQ in O(logn) time each (both PURQ/RUPQ could do RURQ, but half slowly).
    public sealed class RURQBinaryIndexedTree
    {
        private readonly int[] _tree1;
        private readonly int[] _tree2;

        public RURQBinaryIndexedTree(int arraySize)
        {
            _tree1 = new int[arraySize + 1];
            _tree2 = new int[arraySize + 1];
        }

        public RURQBinaryIndexedTree(IReadOnlyList<int> array)
        {
            _tree1 = new int[array.Count + 1];
            _tree2 = new int[array.Count + 1];

            for (int i = 0; i < array.Count; ++i)
            {
                RangeUpdate(i, i, array[i]);
            }
        }

        private void Update(int[] tree, int updateIndex, int delta)
        {
            for (++updateIndex;
                updateIndex < tree.Length;
                updateIndex += updateIndex & -updateIndex)
            {
                tree[updateIndex] += delta;
            }
        }

        public void RangeUpdate(int updateStartIndex, int updateEndIndex, int delta)
        {
            Update(_tree1, updateStartIndex, delta);
            Update(_tree1, updateEndIndex + 1, -delta);
            Update(_tree2, updateStartIndex, delta * (updateStartIndex - 1));
            Update(_tree2, updateEndIndex + 1, -delta * updateEndIndex);
        }

        private int Query(int[] tree, int queryIndex)
        {
            int value = 0;
            for (++queryIndex;
                queryIndex > 0;
                queryIndex -= queryIndex & -queryIndex)
            {
                value += tree[queryIndex];
            }

            return value;
        }

        private int SumQuery(int queryEndIndex)
            => Query(_tree1, queryEndIndex) * queryEndIndex - Query(_tree2, queryEndIndex);

        public int SumQuery(int queryStartIndex, int queryEndIndex)
            => SumQuery(queryEndIndex) - SumQuery(queryStartIndex - 1);
    }
}

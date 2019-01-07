﻿namespace Spoj.Library
{
    // https://en.wikipedia.org/wiki/Disjoint-set_data_structure
    // https://www.youtube.com/watch?v=gcmjC-OcWpI
    public sealed class DisjointSets
    {
        // Can be easily split into two arrays, but let's go with this huge name instead.
        private readonly int[] _elementsParentsOrNegatedSubsetSizes;

        public DisjointSets(int elementCount)
        {
            _elementsParentsOrNegatedSubsetSizes = new int[elementCount];
            for (int i = 0; i < elementCount; ++i)
            {
                _elementsParentsOrNegatedSubsetSizes[i] = -1;
            }

            ElementCount = DisjointSetCount = elementCount;
        }

        public int ElementCount { get; }
        public int DisjointSetCount { get; private set; }

        // Consider the following example:
        // elements:                                   0  1 2  3 4
        // elements' parents or negated subset sizes: -1 -3 1 -1 2
        // Elements with negative numbers are the roots of their sets. The value of the number
        // is the size of the set--so 0 is the root of a set of size 1 (itself), and 1 is the
        // root of a set of size 3. Elements with positive numbers are pointing towards their
        // root. 2 points towards 1 (which we know is the root), and 4 points towards 2, etc.
        private int FindRoot(int element)
        {
            int parentOrNegatedSubsetSize = _elementsParentsOrNegatedSubsetSizes[element];

            return parentOrNegatedSubsetSize >= 0
                // Follow the path towards the parent, and compress. If we run FindParent(4)
                // from the above example, 4 goes to 2 which goes to 1 which returns itself.
                // We compress by setting 4's parent to 1, so it finds it directly next time.
                ? _elementsParentsOrNegatedSubsetSizes[element] = FindRoot(parentOrNegatedSubsetSize)
                // If negative (so a negated subset size), that means it is its own parent.
                : element;
        }

        public void UnionSets(int firstElement, int secondElement)
        {
            int firstRoot = FindRoot(firstElement);
            int secondRoot = FindRoot(secondElement);

            if (firstRoot == secondRoot)
                return;

            int firstSetSize = -1 * _elementsParentsOrNegatedSubsetSizes[firstRoot];
            int secondSetSize = -1* _elementsParentsOrNegatedSubsetSizes[secondRoot];

            int biggerRoot = firstSetSize > secondSetSize ? firstRoot : secondRoot;
            int smallerRoot = firstSetSize > secondSetSize ? secondRoot : firstRoot;

            _elementsParentsOrNegatedSubsetSizes[smallerRoot] = biggerRoot;
            _elementsParentsOrNegatedSubsetSizes[biggerRoot] = -1 * (firstSetSize + secondSetSize);

            --DisjointSetCount;
        }

        public bool AreInSameSet(int firstElement, int secondElement)
            => FindRoot(firstElement) == FindRoot(secondElement);
    }
}

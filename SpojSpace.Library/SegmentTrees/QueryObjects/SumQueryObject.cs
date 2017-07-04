﻿using System;

namespace SpojSpace.Library.SegmentTrees.QueryObjects
{
    public sealed class SumQueryObject : SegmentTreeQueryObject<SumQueryObject, int>
    {
        public override int QueryValue
        {
            get { return Sum; }
            protected set { Sum = value; }
        }

        private int Sum { get; set; }

        public override SumQueryObject Combine(SumQueryObject rightAdjacentObject)
            => new SumQueryObject
            {
                SegmentStartIndex = SegmentStartIndex,
                SegmentEndIndex = rightAdjacentObject.SegmentEndIndex,
                Sum = Sum + rightAdjacentObject.Sum
            };

        public override void Update(SumQueryObject updatedLeftChild, SumQueryObject updatedRightChild)
            => Sum = updatedLeftChild.Sum + updatedRightChild.Sum;
    }
}

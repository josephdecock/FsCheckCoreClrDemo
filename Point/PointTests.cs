using System;
using System.Collections.Generic;
using Xunit;
using FsCheck;

namespace Point
{
    public class PointTests
    {

        [Fact]
        public void IntentionallyBroken()
        {
            Prop.ForAll<int, int>((x, y) =>
            {
                var p1 = new Point(x + 1, y);
                var p2 = new Point(x, y);
                return p1 == p2;
            }).QuickCheckThrowOnFailure();
        }

        [Fact]
        public void SameCoordsShouldBeEqual()
        {
            Prop.ForAll<int, int>((x, y) =>
            {
                var p1 = new Point(x, y);
                var p2 = new Point(x, y);
                return p1 == p2;
            }).QuickCheckThrowOnFailure();
        }

        [Fact]
        public void PointsWithDifferentXCoordsShouldBeNotEqual()
        {
            Prop
            .ForAll<int, int, int>((x1, x2, y) =>
                new Func<bool>(() =>
                {
                    var p1 = new Point(x1, y);
                    var p2 = new Point(x2, y);
                    return p1 != p2;
                })
            .When(x1 != x2))
            .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void PointsWithDifferentYCoordsShouldBeNotEqual()
        {
            Prop
            .ForAll<int, int, int>((y1, y2, x) =>
                new Func<bool>(() =>
                {
                    var p1 = new Point(x, y1);
                    var p2 = new Point(x, y2);
                    return p1 != p2;
                })
            .When(y1 != y2))
            .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void NullPointShouldNotEqualNonNullPoint()
        {
            Prop.ForAll<Point>(p => p != null)
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void EqualPointsUsedAsKeysInADictionaryShouldCollide()
        {
            Prop.ForAll<int, int>((x, y) =>
                {
                    var p1 = new Point(x, y);
                    var p2 = new Point(x, y);

                    var d = new Dictionary<Point, object>();
                    d.Add(p1, new { });
                    Assert.Throws<ArgumentException>(() => d.Add(p2, new { }));
                })
            .QuickCheckThrowOnFailure();
        }
    }
}

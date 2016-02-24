using System;

namespace Point
{
    public class Point : IEquatable<Point>
    {
        public readonly int x;
        public readonly int y;

        public Point(int x, int y)
        {
            this.x = x; this.y = y;
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return p1?.x == p2?.x && p1.y == p2.y;
        }
        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }

        public override int GetHashCode()
        {
            // This algorithm is from Jon Skeet's stack overflow answer about GetHashCode
            const int p1 = 17;
            const int p2 = 23;

            var hashCode = p1;

            hashCode = hashCode * p2 + x.GetHashCode();
            hashCode = hashCode * p2 + y.GetHashCode();

            return hashCode;
        }

        public override bool Equals(object other)
        {
            return this == (other as Point);
        }

        bool IEquatable<Point>.Equals(Point other)
        {
            return this == other;
        }
    }

}

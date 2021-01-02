
namespace STLProjection
{
	public class GeoUtil
	{
		// Project 3D point on plane and transform the projected position to the plane's coordinate system
		public static Vector ProjectOnPlaneTransformed(Vector vec, Vector origin, Vector up, Vector right)
		{
			var r = vec - origin;
			return new Vector(Vector.Dot(right, r), Vector.Dot(up, r));
		}

		// Shortest distance between a point and a line segment in 3D space.  
		public static double PointLineDistance(Vector p, Line l)
		{
			var p1 = l.p1;
			var p2 = l.p2;

			var r1 = p1 - p;
			var r2 = p2 - p;
			var r12 = p2 - p1;
			var r12Norm = r12.Normalized;

			var d = Vector.Dot(r12Norm, r2);
			if (d <= 0)
			{
				return Vector.Distance(p, p2);
			}
			else if (d >= r12.Magnitude)
			{
				return Vector.Distance(p, p1);
			}

			var n = new Vector(r12Norm.y, -r12Norm.x);

			return System.Math.Abs(Vector.Dot(n, r2));
		}
	}
}

namespace STLProjection
{
	public struct Line
	{
		public Vector p1;
		public Vector p2;

		public Line(Vector p1, Vector p2)
		{
			this.p1 = p1;
			this.p2 = p2;
		}

		public override string ToString()
		{
			return $"({p1} , {p2})";
		}
	}
}

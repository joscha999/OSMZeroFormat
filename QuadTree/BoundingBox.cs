using System.Numerics;

namespace SpatialTrees {
	public struct BoundingBox {
		public Vector2 Center { get; }
		public float HalfDimension { get; }

		public float Left => Center.X - HalfDimension;
		public float Right => Center.X + HalfDimension;
		public float Top => Center.Y - HalfDimension;
		public float Bottom => Center.Y + HalfDimension;

		public float Size => HalfDimension * 2f;

		public BoundingBox(Vector2 center, float halfDimension) {
			Center = center;
			HalfDimension = halfDimension;
		}

		public BoundingBox(float centerX, float centerY, float halfDimension)
			: this(new Vector2(centerX, centerY), halfDimension) { }

		public bool Contains(Vector2 v2) => Contains(v2.X, v2.Y);

		public bool Contains(float x, float y)
			=> x > Left && x < Right
			&& y > Top && y < Bottom;

		//https://stackoverflow.com/questions/306316/determine-if-two-rectangles-overlap-each-other
		public bool Intersects(BoundingBox other)
			=> Left < other.Right && Right > other.Left
			&& Bottom > other.Top && Top < other.Bottom;
	}
}
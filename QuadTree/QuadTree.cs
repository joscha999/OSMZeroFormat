using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace SpatialTrees {
	public class QuadTree<TType, TGN> where TGN : IGenericNode<TType> {
		public int NodeCapacity { get; }
		public int MaxDepth { get; }

		private int Depth { get; }
		public BoundingBox Boundary { get; }

		private readonly List<TGN> Points;

		private readonly QuadTree<TType, TGN> Parent;

		public QuadTree<TType, TGN> NorthWest { get; private set; }
		public QuadTree<TType, TGN> NorthEast { get; private set; }
		public QuadTree<TType, TGN> SouthWest { get; private set; }
		public QuadTree<TType, TGN> SouthEast { get; private set; }

		public QuadTree(int nodeCapacity, int maxDepth, BoundingBox boundary) {
			NodeCapacity = nodeCapacity;
			MaxDepth = maxDepth;
			Boundary = boundary;

			Depth = 0;
			Points = new List<TGN>(nodeCapacity);
		}

		private QuadTree(int nodeCapacity, int maxDepth, int depth,
			BoundingBox boundary, QuadTree<TType, TGN> parent)
			: this(nodeCapacity, maxDepth, boundary) {
			Depth = depth;
			Parent = parent;

			if (depth == maxDepth)
				Debug.WriteLine("Depth == MaxDepth (" + depth + ")");
		}

		public bool Insert(TGN gn) {
			if (!Boundary.Contains(gn.Position))
				return false;

			if (Points.Count < NodeCapacity || Depth == MaxDepth) {
				Points.Add(gn);
				gn.QuadTree = (QuadTree<TType, IGenericNode<TType>>)(object)this;
				return true;
			}

			if (NorthWest == null)
				Subdivide();

			if (NorthWest.Insert(gn)) return true;
			if (NorthEast.Insert(gn)) return true;
			if (SouthWest.Insert(gn)) return true;
			if (SouthEast.Insert(gn)) return true;

			return false;
		}

		public bool Update(TGN gn, Vector2 newPos) {
			//isn't currently inside us - ignore
			if (!Boundary.Contains(gn.Position))
				return false;

			if (Points.Contains(gn)) {
				//is inside us and we're not splited
				if (Boundary.Contains(newPos)) {
					//case 1: still inside us - update pos, no change
					gn.Position = newPos;
					return true;
				} else {
					//case 2: not inside us anymore - remove from us, update parent
					if (Parent == null)
						return false;

					Points.Remove(gn);
					gn.Position = newPos;
					return Parent.Insert(gn);
				}
			}

			if (NorthWest == null)
				return false;

			//is inside but and we're splitted
			if (NorthWest.Update(gn, newPos)) return true;
			if (NorthEast.Update(gn, newPos)) return true;
			if (SouthWest.Update(gn, newPos)) return true;
			if (SouthEast.Update(gn, newPos)) return true;

			//not updated
			return false;
		}

		public IEnumerable<TGN> Query(BoundingBox boundary) {
			if (!boundary.Intersects(Boundary))
				yield break;

			for (int i = 0; i < Points.Count; i++) {
				if (boundary.Contains(Points[i].Position))
					yield return Points[i];
			}

			if (NorthWest == null)
				yield break;

			foreach (var i in NorthWest.Query(boundary))
				yield return i;
			foreach (var i in NorthEast.Query(boundary))
				yield return i;
			foreach (var i in SouthWest.Query(boundary))
				yield return i;
			foreach (var i in SouthEast.Query(boundary))
				yield return i;
		}

		private void Subdivide() {
			var newHalfDimension = Boundary.HalfDimension / 2f;
			NorthWest = new QuadTree<TType, TGN>(NodeCapacity, MaxDepth, Depth + 1, new BoundingBox(
				Boundary.Center.X - newHalfDimension,
				Boundary.Center.Y + newHalfDimension, newHalfDimension), this);
			NorthEast = new QuadTree<TType, TGN>(NodeCapacity, MaxDepth, Depth + 1, new BoundingBox(
				Boundary.Center.X + newHalfDimension,
				Boundary.Center.Y + newHalfDimension, newHalfDimension), this);
			SouthWest = new QuadTree<TType, TGN>(NodeCapacity, MaxDepth, Depth + 1, new BoundingBox(
				Boundary.Center.X - newHalfDimension,
				Boundary.Center.Y - newHalfDimension, newHalfDimension), this);
			SouthEast = new QuadTree<TType, TGN>(NodeCapacity, MaxDepth, Depth + 1, new BoundingBox(
				Boundary.Center.X + newHalfDimension,
				Boundary.Center.Y - newHalfDimension, newHalfDimension), this);
		}
	}
}
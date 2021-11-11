using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpatialTrees {
	public interface IGenericNode<T> {
		T Data { get; set; }
		Vector2 Position { get; set; }
		QuadTree<T, IGenericNode<T>> QuadTree { get; set; }
	}
}
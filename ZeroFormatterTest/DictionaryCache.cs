using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroFormatterTest {
	public class DictionaryCache<Tkey, TValue> {
		private readonly Dictionary<Tkey, int> cacheLocations;
		private readonly Dictionary<int, Tkey> keyLocations;

		private TValue[] cache;
		private int position;

		private int _cacheSize;
		public int CacheSize {
			get => _cacheSize;
			set {
				if (_cacheSize == value)
					return;

				_cacheSize = value;

				var oldCache = cache;
				cache = new TValue[CacheSize];
				oldCache.CopyTo(cache, 0);
			}
		}

		public DictionaryCache(int cacheSize) {
			_cacheSize = cacheSize;

			cacheLocations = new Dictionary<Tkey, int>(CacheSize);
			keyLocations = new Dictionary<int, Tkey>(CacheSize);
			cache = new TValue[CacheSize];
		}

		public void Add(Tkey key, TValue value) {
			//check for overwrite (when overwriting remove key - no longer cached)
			if (cache[position] != null)
				cacheLocations.Remove(keyLocations[position]);

			//write
			cacheLocations.Add(key, position);
			keyLocations[position] = key;
			cache[position] = value;

			//increment pos
			position = (position + 1) % CacheSize;
		}

		public bool TryGetValue(Tkey key, out TValue value) {
			value = default;

			if (!cacheLocations.TryGetValue(key, out var pos))
				return false;

			value = cache[pos];
			return true;
		}
	}
}
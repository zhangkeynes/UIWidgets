using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unity.UIWidgets.foundation {
    public delegate void ValueChanged<T>(T value);

    public delegate IEnumerable<T> EnumerableFilter<T>(IEnumerable<T> input);

    public static class ObjectUtils {
        public static T SafeDestroy<T>(T obj) where T : Object {
            if (Application.isEditor) {
                Object.DestroyImmediate(obj);
            }
            else {
                Object.Destroy(obj);
            }

            return null;
        }
    }

    public static class CollectionUtils {
        public static V putIfAbsent<K, V>(this IDictionary<K, V> it, K key, Func<V> ifAbsent) {
            V value;
            if (it.TryGetValue(key, out value)) {
                return value;
            }

            value = ifAbsent();
            it[key] = value;
            return value;
        }

        public static bool isEmpty<T>(this ICollection<T> it) {
            return it.Count == 0;
        }

        public static bool isNotEmpty<T>(this ICollection<T> it) {
            return it.Count != 0;
        }

        public static bool isEmpty<T>(this Queue<T> it) {
            return it.Count == 0;
        }

        public static bool isNotEmpty<T>(this Queue<T> it) {
            return it.Count != 0;
        }

        public static bool isEmpty<TKey, TValue>(this IDictionary<TKey, TValue> it) {
            return it.Count == 0;
        }

        public static bool isNotEmpty<TKey, TValue>(this IDictionary<TKey, TValue> it) {
            return it.Count != 0;
        }

        public static bool isEmpty(this string it) {
            return string.IsNullOrEmpty(it);
        }

        public static bool isNotEmpty(this string it) {
            return !string.IsNullOrEmpty(it);
        }

        public static TValue getOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> it, TKey key) {
            TValue v;
            it.TryGetValue(key, out v);
            return v;
        }

        public static T first<T>(this IList<T> it) {
            return it[0];
        }

        public static T last<T>(this IList<T> it) {
            return it[it.Count - 1];
        }

        public static T removeLast<T>(this IList<T> it) {
            var lastIndex = it.Count - 1;
            var result = it[lastIndex];
            it.RemoveAt(lastIndex);
            return result;
        }

        public static int hashList<T>(this IList<T> it) {
            unchecked {
                var hashCode = 0;
                if (it != null) {
                    foreach (var item in it) {
                        hashCode = (hashCode * 397) ^ item.GetHashCode();
                    }
                }

                return hashCode;
            }
        }

        public static bool equalsList<T>(this IList<T> it, IList<T> list) {
            if (it == null && list == null) {
                return true;
            }

            if (ReferenceEquals(it, list)) {
                return true;
            }

            if (it == null || list == null) {
                return false;
            }

            if (it.Count != list.Count) {
                return false;
            }

            for (int i = it.Count - 1; i >= 0; --i) {
                if (!Equals(it[i], list[i])) {
                    return false;
                }
            }

            return true;
        }

        public static string toStringList<T>(this IList<T> it) {
            if (it == null) {
                return null;
            }
            return "{ " + string.Join(", ", it.Select(item => item.ToString())) + " }";
        }

        public static void resize<T>(this List<T> list, int size, T value) {
            int curSize = list.Count;
            if (size < curSize) {
                list.RemoveRange(size, curSize - size);
            } else if(size > curSize) {
                if (size > list.Capacity) {
                    list.Capacity = size;
                }
                list.AddRange(Enumerable.Repeat(value, size - curSize));
            }
            
            int remains = Math.Min(curSize, size);
            for (int i = 0; i < remains; ++i) {
                list[i] = value;
            }
        }
    }
}
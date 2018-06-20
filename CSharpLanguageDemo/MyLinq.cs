using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpLanguageDemo
{
    static class MyLinq
    {
        public static IEnumerable<IGrouping<TKey, TSource>> MyGroupBy<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var aux = source;
            while (aux.Count() > 0)
            {
                var key = keySelector(aux.First());
                yield return new Group<TKey, TSource>(aux.Where((x) => keySelector(x).Equals(key)), key);
                aux = aux.Where((x) => !(keySelector(x).Equals(key)));
            }   
        }

        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                if (source.Count() == 1)
                    yield return new List<T> { item };
                foreach (var perm in Permutations(source.Where(x => !x.Equals(item))))
                    yield return new List<T>{ item }.Concat(perm);
            }
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> source, int number)
        {
            foreach (var item in source)
            {
                if (number == 1)
                    yield return new List<T> { item };
                foreach (var comb in Combinations(source.Where(x => !x.Equals(item)), number - 1))
                    yield return new List<T> { item }.Concat(comb);
            }
        }

        public static void Print<T>(this IEnumerable<T> source, int size, Action<string> write)
        {
            write("[");
            int i = 0;
            foreach (var item in source)
            {
                if (++i == size)
                    write($"{item}]\n");
                else
                    write($"{item}, ");
            }
        }
    }

    public class Group<TKey, TSource> : IGrouping<TKey, TSource>
    {
        public IEnumerable<TSource> Source { get; set; }
        public TKey Key { get; set; }

        public Group(IEnumerable<TSource> source, TKey key)
        {
            Source = source;
            Key = key;
        }
        public IEnumerator<TSource> GetEnumerator()
        {
            return Source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class MyStudent
    {
        public MyStudent(int age, string name, string group)
        {
            Age = age;
            Name = name;
            Group = group;
        }
        public string Group { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

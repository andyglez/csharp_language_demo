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

    public class Student
    {
        public Student(int age, string name, string group)
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

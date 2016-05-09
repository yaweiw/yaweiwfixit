using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReplaceWithObjects
{
    public abstract class Classbase
    {
        protected internal static ConcurrentDictionary<string,Delegate> GetterDict { get; set; }
        public abstract string ReplaceWithFields(string rawdata);
    }
    public class ClassA : Classbase
    {
        public string PropA { get; }
        public string PropB { get; }

        static ClassA()
        {
            GetterDict = new ConcurrentDictionary<string, Delegate>();
            foreach (PropertyInfo propertyInfo in typeof(ClassA).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                GetterDict.TryAdd(propertyInfo.Name, ClassExtension.CreateGetter<ClassA, string>(propertyInfo.Name));
            }
        }
        public ClassA()
        {
            PropA = "ClassA_A";
            PropB = "ClassA_B";

        }
        public override string ReplaceWithFields(string rawdata)
        {
            foreach (var entry in GetterDict)
            {
                rawdata = rawdata.Replace("$" + entry.Key.ToLower(), ((Func<ClassA, string>)entry.Value)(this));
            }
            return rawdata;
        }
    }

    public class ClassB : Classbase
    {
        public string PropA { get; }
        public string PropB { get; }
        static ClassB()
        {
            GetterDict = new ConcurrentDictionary<string, Delegate>();
            foreach (PropertyInfo propertyInfo in typeof(ClassB).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                GetterDict.TryAdd(propertyInfo.Name, ClassExtension.CreateGetter<ClassB, string>(propertyInfo.Name));
            }
        }
        public ClassB()
        {
            PropA = "ClassB_A";
            PropB = "ClassB_B";
        }
        public override string ReplaceWithFields(string rawdata)
        {
            foreach (var entry in GetterDict)
            {
                rawdata = rawdata.Replace("$" + entry.Key, ((Func<ClassB,string>)entry.Value)(this));
            }
            return rawdata;
        }
    }

    public static class ClassExtension
    {
        private static ConcurrentDictionary<string, List<string>> cprop = new ConcurrentDictionary<string, List<string>>();
        private static ConcurrentDictionary<string, Delegate> cdel = new ConcurrentDictionary<string, Delegate>();

        public static Func<TEntity, TProperty> CreateGetter<TEntity, TProperty>(TProperty property)
        {
            PropertyInfo propertyInfo = typeof(TEntity).GetProperty(property as string);

            ParameterExpression instance = Expression.Parameter(typeof(TEntity), "instance");

            var body = Expression.Call(instance, propertyInfo.GetGetMethod());
            var parameters = new ParameterExpression[] { instance };

            return Expression.Lambda<Func<TEntity, TProperty>>(body, parameters).Compile();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string pat = @"$propa and $propb";
            ClassA ca = new ClassA();
            string output = ca.ReplaceWithFields(pat);

        }
    }
}

using System;
using FakerLib;

namespace IntGenerator
{
    public class IntGenerator : IGenerator
    {
        class A {
            class T {
                public int a;
                private bool isA;
            }

            public char c;
            string myStr;
        }

        bool IGenerator.CanGenerate(Type type)
        {
            return type == typeof(int);
        }

        object IGenerator.Generate(GeneratorContext context)
        {
            return context.Random.Next(1, 100) + 1;
        }
    }
    public class B { 
        
        public void play(int count) { }
    }

}

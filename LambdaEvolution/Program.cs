using System;

namespace LambdaEvolution
{
    class Program
    {
        delegate int FuncForString(string text);

        public static int GetStringLength(string text)
        {
            return text.Length;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Evolution from delegate to lambda");

// C# 1.0 delegate type and delegate instance
FuncForString fun1 = new FuncForString(GetStringLength);

// C# 2.0 method group conversion
FuncForString fun2 = GetStringLength;

// C# 2.0 generic delegate and anonymous method
Func<string, int> fun3 = delegate (string text) { return text.Length; };

// C# 3.0 lamba expression
Func<string, int> fun4 = (string text) => { return text.Length; };
            
// C# 3.0 simple expression, no need braces
Func<string, int> fun5 = (string text) => text.Length;
            
// C# 3.0 let the compiler infer the parameter type
Func<string, int> fun6 = (text) => text.Length;
            
// C# 3.0 no need unnecessary parentheses
Func<string, int> fun7 = text => text.Length;
        }

    }
}

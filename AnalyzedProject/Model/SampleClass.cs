// Questo è un file importante
using System.Collections.Generic;
using System.Threading;

namespace AnalyzedProject.Model
{
    public class SampleClass
    {
        public string ReadLine()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        // TODO: verifica se il valore del parametro è null
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
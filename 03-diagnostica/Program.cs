using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

string text = File.ReadAllText(@"..\AnalyzedProject\Model\SampleClass.cs");

SyntaxTree tree = CSharpSyntaxTree.ParseText(text);
List<Diagnostic> diagnostics = tree.GetDiagnostics().ToList();

Console.WriteLine($"Sono stati trovati {diagnostics.Count} errori");
foreach (Diagnostic diagnostic in diagnostics)
{
    Console.WriteLine(diagnostic);
}
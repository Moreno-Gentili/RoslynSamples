using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

string text = File.ReadAllText(@"..\AnalyzedProject\Model\SampleClass.cs");

SyntaxTree tree = CSharpSyntaxTree.ParseText(text);
SyntaxNode root = await tree.GetRootAsync();

// IEnumerable<MethodDeclarationSyntax> methods = root.DescendantNodes().Where(node => node.IsKind(SyntaxKind.MethodDeclaration)).Cast<MethodDeclarationSyntax>();
IEnumerable<MethodDeclarationSyntax> methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

foreach (MethodDeclarationSyntax method in methods)
{
    // int parameterCount = method.ChildNodes().OfType<ParameterListSyntax>().Single().ChildNodes().OfType<ParameterSyntax>().Count();
    int parameterCount = method.ParameterList.Parameters.Count;
    Console.WriteLine($"Trovato il metodo {method.Identifier.Text}, ha {parameterCount} parametri");
}
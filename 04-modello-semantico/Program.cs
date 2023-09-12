using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

string consoleText = File.ReadAllText(@"..\AnalyzedProject\Model\Console.cs");
string sampleClassText = File.ReadAllText(@"..\AnalyzedProject\Model\SampleClass.cs");

SyntaxTree consoleTree = CSharpSyntaxTree.ParseText(consoleText);
SyntaxTree sampleClassTree = CSharpSyntaxTree.ParseText(sampleClassText);
SyntaxNode sampleClassRoot = await sampleClassTree.GetRootAsync();

List<SyntaxToken> identifiers =
    sampleClassRoot.DescendantTokens()
                   .Where(t => t.IsKind(SyntaxKind.IdentifierToken) && t.Text == "ReadLine")
                   .ToList();

CSharpCompilation compilation = 
    CSharpCompilation.Create("NomeAssembly")
        .AddReferences(MetadataReference.CreateFromFile(typeof(Console).Assembly.Location))
        .AddSyntaxTrees(sampleClassTree, consoleTree);

SemanticModel semanticModel = compilation.GetSemanticModel(sampleClassTree);

foreach (SyntaxToken identifier in identifiers)
{
    SyntaxNode parent = identifier.Parent!;
    ISymbol? symbolInfo = semanticModel.GetSymbolInfo(parent).Symbol ?? semanticModel.GetDeclaredSymbol(parent);
    if (symbolInfo is not null)
    {
        Console.WriteLine($"Trovato metodo {symbolInfo.Name} del tipo {symbolInfo.ContainingType.Name}");
    }
}
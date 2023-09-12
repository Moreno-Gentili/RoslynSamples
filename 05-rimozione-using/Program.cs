using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

string text = File.ReadAllText(@"..\AnalyzedProject\Model\SampleClass.cs");

SyntaxTree tree = CSharpSyntaxTree.ParseText(text);
SyntaxNode root = await tree.GetRootAsync();

CSharpCompilation compilation = 
    CSharpCompilation.Create("NomeAssembly")
        .AddReferences(MetadataReference.CreateFromFile(typeof(Console).Assembly.Location))
        .AddReferences(MetadataReference.CreateFromFile(typeof(List<>).Assembly.Location))
        .AddReferences(MetadataReference.CreateFromFile(typeof(Thread).Assembly.Location))
        .AddSyntaxTrees(tree);

SemanticModel semanticModel = compilation.GetSemanticModel(tree);

// List<UsingDirectiveSyntax> allUsings = root.DescendantNodes().OfType<UsingDirectiveSyntax>().ToList();
List<UsingDirectiveSyntax> removeUsings = new();

IEnumerable<Diagnostic> diagnostics = semanticModel.GetDiagnostics();
List<SyntaxNode> usingsToRemove = diagnostics.Where(d => d.Id == "CS8019") // https://github.com/dotnet/roslyn/issues/41640
                                             .Select(d => root.FindNode(d.Location.SourceSpan))
                                             .ToList();

SyntaxNode newRoot = root.RemoveNodes(usingsToRemove, SyntaxRemoveOptions.KeepExteriorTrivia)!;

Console.WriteLine(newRoot.ToFullString());
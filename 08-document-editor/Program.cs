using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.MSBuild;

MSBuildLocator.RegisterDefaults();
MSBuildWorkspace workspace = MSBuildWorkspace.Create();
Project project = await workspace.OpenProjectAsync(@"..\AnalyzedProject\AnalyzedProject.csproj");

Document sampleClassDocument = project.Documents.Single(d => d.FilePath?.EndsWith("SampleClass.cs") == true);

SyntaxNode? root = await sampleClassDocument.GetSyntaxRootAsync();
List<MethodDeclarationSyntax> methods = root!.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();

// #1 (non funziona perché rimuove il secondo metodo)
foreach (var method in methods)
{
    bool isContained = root.Contains(method);
    root = root.RemoveNode(method, SyntaxRemoveOptions.KeepNoTrivia)!;
}

// #2: rimuovere più nodi alla volta
// root = root.RemoveNodes(methods, SyntaxRemoveOptions.KeepNoTrivia);

// #3: usare un DocumentEditor
// DocumentEditor editor = await DocumentEditor.CreateAsync(sampleClassDocument);
// foreach (var method in methods)
// {
//     editor.RemoveNode(method, SyntaxRemoveOptions.KeepNoTrivia);
// }
// root = editor.GetChangedRoot();

Console.WriteLine(root.ToFullString());


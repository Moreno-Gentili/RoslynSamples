using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

MSBuildLocator.RegisterDefaults();
MSBuildWorkspace workspace = MSBuildWorkspace.Create();
// Project project = await workspace.OpenProjectAsync(@"..\AnalyzedProject\AnalyzedProject.csproj");
Project project = await workspace.OpenProjectAsync(@"E:\GD\aHMI20\AFI\AFI.csproj");

foreach (Document document in project.Documents)
{
    SyntaxNode? root = await document.GetSyntaxRootAsync();
    if (root is not null)
    {
        List<ClassDeclarationSyntax> classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>().ToList();
        if (classes.Any())
        {
            Console.WriteLine($"Il documento {Path.GetFileName(document.FilePath)} contiene {classes.Count} classi:");
            foreach (ClassDeclarationSyntax @class in classes)
            {
                Console.WriteLine($"\t{@class.Identifier.Text}");
            }
        }
    }
}
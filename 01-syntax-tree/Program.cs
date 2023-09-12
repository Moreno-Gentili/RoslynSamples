using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

string text = File.ReadAllText(@"..\AnalyzedProject\Model\SampleClass.cs");

SyntaxTree tree = CSharpSyntaxTree.ParseText(text);
SyntaxNode root = await tree.GetRootAsync();

PrintNode(root);

Console.ResetColor();

void PrintNode(SyntaxNodeOrToken nodeOrToken, int level = 0)
{
    string padding = new string(' ', level);

    // Tipo di espressione, es. IdentifierName o IdentifierToken
    SyntaxKind kind = nodeOrToken.Kind();
    TextSpan span = nodeOrToken.Span;
    Console.ForegroundColor = nodeOrToken.IsNode ? ConsoleColor.Blue : ConsoleColor.Green;
    Console.WriteLine($"{padding}{kind} {span}");

    PrintTrivia(nodeOrToken, level + 1);

    foreach (SyntaxNodeOrToken childNode in nodeOrToken.ChildNodesAndTokens())
    {
        PrintNode(childNode, level + 1);
    }
}

void PrintTrivia(SyntaxNodeOrToken nodeOrToken, int level)
{
    if (nodeOrToken.IsToken)
    {
        if (nodeOrToken.HasLeadingTrivia)
        {
            PrintTriviaList("Leading", nodeOrToken.GetLeadingTrivia(), level);
        }

        if (nodeOrToken.HasTrailingTrivia)
        {
            PrintTriviaList("Trailing", nodeOrToken.GetTrailingTrivia(), level);
        }
    }
}

void PrintTriviaList(string position, SyntaxTriviaList triviaList, int level)
{
    foreach (SyntaxTrivia trivia in triviaList)
    {
        string padding = new string(' ', level);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{padding}{position}: {trivia.Kind()} {trivia.FullSpan}");
    }
}
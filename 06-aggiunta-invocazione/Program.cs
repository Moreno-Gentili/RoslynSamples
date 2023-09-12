using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

string text = File.ReadAllText(@"..\AnalyzedProject\Model\SampleClass.cs");

SyntaxTree tree = CSharpSyntaxTree.ParseText(text);
SyntaxNode root = await tree.GetRootAsync();

MethodDeclarationSyntax method = root.DescendantNodes()
                                     .OfType<MethodDeclarationSyntax>()
                                     .Single(m => m.Identifier.Text == "ReadLine");

ExpressionStatementSyntax invocation = CreateInvocationExpressionByParsing(method.Identifier.Text);

SyntaxNode newRoot = root.InsertNodesBefore(method.Body!.ChildNodes().First(), new[] { invocation });

// node = Formatter.Format(node, workspace);

Console.WriteLine(newRoot.ToFullString());

ExpressionStatementSyntax CreateInvocationExpression(string methodName)
{
    // Debug.WriteLine($"È stato chiamato il metodo {methodName}");
    var debug = SyntaxFactory.IdentifierName("Debug");
    var writeline = SyntaxFactory.IdentifierName("WriteLine");
    var memberaccess = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, debug, writeline);

    var text = $"Invocato {methodName}";
    var argument = SyntaxFactory.Argument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(text)));
    var argumentList = SyntaxFactory.SeparatedList(new[] { argument });

    var writeLineCall =
        SyntaxFactory.ExpressionStatement(
        SyntaxFactory.InvocationExpression(memberaccess,
        SyntaxFactory.ArgumentList(argumentList)))
        .WithTrailingTrivia(SyntaxTriviaList.Create(SyntaxFactory.CarriageReturnLineFeed));

    return writeLineCall;
}

ExpressionStatementSyntax CreateInvocationExpressionByParsing(string methodName)
{
    SyntaxTree tree = CSharpSyntaxTree.ParseText($@"Debug.WriteLine(""Invocato {methodName}"");");
    SyntaxNode root = tree.GetRoot();
    return SyntaxFactory.ExpressionStatement(
        root.DescendantNodes().OfType<InvocationExpressionSyntax>().Single())
        .WithTrailingTrivia(SyntaxTriviaList.Create(SyntaxFactory.CarriageReturnLineFeed));
}
using LanguageCompiler.Lexer;

namespace LanguageCompiler.Parser
{
    public class Parser
    {
	private readonly IScanner scanner;
	private Token lookAhead;

	public Parser(IScanner scanner)
	{
	    this.scanner = scanner;
	    this.Move();
	}

	private void Code()
	{
	    Block();
	}

	private void Block()
	{
	    Match(TokenType.OpenBrace);
	    Decls();
	    Stmts();
	    Match(TokenType.CloseBrace);
	}

	private void Stmts()
	{
	    //{}
	    if (this.lookAhead.TokenType == TokenType.CloseBrace)
	    {
		//eps
	    }
	    else
	    {
                Stmt();
                Stmts();
            }
	}

	private void Stmt()
	{
	    switch (this.lookAhead.TokenType)
	    {
		case TokenType.Identifier:
		    AssignationStatement();
		    break;
		case TokenType.WhileKeyword:
		    WhileStatement();
		    break;
		case TokenType.PrintKeyword:
		    PrintStatement();
		    break;
		case TokenType.IfKeyword:
		    IfStatement();
		    break;
		default:
		    Block();
		    break;
	    }
	}

	private void IfStatement()
	{
	    throw new NotImplementedException();
	}

	private void PrintStatement()
	{
	    Match(TokenType.PrintKeyword);
	    Match(TokenType.LeftParens);
	    Params();
	    Match(TokenType.RightParens);
	    Match(TokenType.SemiColon);
	}

	private void Params()
	{
	    throw new NotImplementedException();
	}

	private void WhileStatement()
	{
	    Match(TokenType.WhileKeyword);
	    Match(TokenType.LeftParens);
	    LogicalOrExpr();
	    Match(TokenType.RightParens);
	    Stmt();
	}

	private void LogicalOrExpr()
	{
	    throw new NotImplementedException();
	}

	private void AssignationStatement()
	{
	    Match(TokenType.Identifier);
	    Match(TokenType.Assignation);
	    LogicalOrExpr();
	}

	private void Decls()
	{
	    if (this.lookAhead.TokenType == TokenType.Identifier)
	    {
                Decl();
                Decls();
	    }
	}

	private void Decl()
	{
	    Match(TokenType.Identifier);
	    Match(TokenType.Colon);
	    Type();
	    Match(TokenType.SemiColon);
	}

	private void Type()
	{
	    switch (this.lookAhead.TokenType)
	    {
		case TokenType.FloatKeyword:
		    Match(TokenType.FloatKeyword);
		    break;
		case TokenType.StringKeyword:
		    Match(TokenType.StringKeyword);
		    break;
		default:
                    Match(TokenType.IntKeyword);
                    break;
	    }
	}

	private void Move()
	{
	    this.lookAhead = this.scanner.GetNextToken();
	}

	private void Match(TokenType tokenType)
	{
	    if (this.lookAhead.TokenType != tokenType)
	    {
		throw new ApplicationException($"Syntax error! expected {tokenType} but found {this.lookAhead.TokenType}.");
	    }
	    this.Move();
	}
    }
}
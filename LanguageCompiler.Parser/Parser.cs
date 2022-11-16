using ClassLibrary1;
using LanguageCompiler.Core;
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

        public Statement Parse()
        {
            var code = Code();
            return code;
        }
        private Statement Code()
        {
            return Block();
        }

        private Statement Block()
        {
            IdExpression id = null;
            Match(TokenType.OpenBrace);
            if (this.lookAhead.TokenType == TokenType.Identifier)
            {
                id = new IdExpression(this.lookAhead.Lexeme, null);
                this.Match(TokenType.Identifier);
            }
            Decls();
            var statements = Stmts(id);
            Match(TokenType.CloseBrace);
            return statements;
        }

        private Statement Stmts(IdExpression id)
        {
            //{}
            if (this.lookAhead.TokenType == TokenType.CloseBrace)
            {
                //eps
            }
            else
            {
                return new SequenceStatement(Stmt(id), Stmts(id));
            }

            return null;
        }

        private Statement Stmt(IdExpression id)
        {
            switch (this.lookAhead.TokenType)
            {
                case TokenType.Assignation:
                case TokenType.Identifier:
                    return AssignationStatement(id);
                case TokenType.WhileKeyword:
                    return WhileStatement();
                case TokenType.PrintKeyword:
                    return PrintStatement();
                case TokenType.IfKeyword:
                    return IfStatement();
                default:
                    return Block();
            }
        }

        private Statement IfStatement()
        {
            this.Match(TokenType.IfKeyword);
            this.Match(TokenType.LeftParens);
            var expr = LogicalOrExpr();
            this.Match(TokenType.RightParens);
            var statement = Stmt(null);
            if (this.lookAhead.TokenType != TokenType.ElseKeyword)
            {
                return new IfStatement(expr, statement, null);
            }
            this.Match(TokenType.ElseKeyword);
            var elseStatement = Stmt(null);
            return new IfStatement(expr, statement, elseStatement);
        }

        private Statement PrintStatement()
        {
            Match(TokenType.PrintKeyword);
            Match(TokenType.LeftParens);
            var @params = Params();
            Match(TokenType.RightParens);
            Match(TokenType.SemiColon);
            return new PrintStatement(@params);
        }

        private List<Expression> Params()
        {
            var expressions = new List<Expression>();
            expressions.Add(LogicalOrExpr());
            expressions.AddRange(ParamsPrime());
            return expressions;
        }

        private List<Expression> ParamsPrime()
        {
            var expressions = new List<Expression>();
            if (this.lookAhead.TokenType == TokenType.Comma)
            {
                Match(TokenType.Comma);
                expressions.Add(LogicalOrExpr());
                expressions.AddRange(ParamsPrime());
            }

            return expressions;
        }

        private Statement WhileStatement()
        {
            Match(TokenType.WhileKeyword);
            Match(TokenType.LeftParens);
            var expression = LogicalOrExpr();
            Match(TokenType.RightParens);
            return new WhileStatement(expression,Stmt(null));
        }

        private Expression LogicalOrExpr()
        {
            var expr = LogicalAndExpr();
            while (this.lookAhead.TokenType == TokenType.LogicalOr)
            {
                Move();
                expr =  new LogicalOrExpression(expr, LogicalAndExpr());
            }

            return expr;
        }

        private Expression LogicalAndExpr()
        {
            var expr = EqExpr();
            while (this.lookAhead.TokenType == TokenType.LogicalAnd)
            {
                Move();
                expr = new LogicalAndExpression(expr, EqExpr());
            }

            return expr;
        }
	
        private Expression EqExpr()
        {
            var expr = RelExpr();
            while (this.lookAhead.TokenType == TokenType.Equal || this.lookAhead.TokenType == TokenType.NotEqual)
            {
                Move();
                expr = new EqualExpression(expr,  RelExpr());
            }

            return expr;
        }
	
        private Expression RelExpr()
        {
            var expr = Expr();
            var token = this.lookAhead;
            while (this.lookAhead.TokenType == TokenType.LessThan ||
                   this.lookAhead.TokenType == TokenType.LessOrEqualThan ||
                   this.lookAhead.TokenType == TokenType.GreaterThan ||
                   this.lookAhead.TokenType == TokenType.GreaterOrEqualThan)
            {
                Move();
                expr = new RelationalExpression(expr,  Expr(), token);
            }

            return expr;
        }
	
        private Expression Expr()
        {
            var expr = Term();
            var token = this.lookAhead;
            while (this.lookAhead.TokenType == TokenType.Plus ||
                   this.lookAhead.TokenType == TokenType.Minus)
            {
                Move();
                expr = new ArithmeticExpression(expr, Term(), token);
            }

            return expr;
        }
	
        private Expression Term()
        {
            var expr = Factor();
            var token = this.lookAhead;
            while (this.lookAhead.TokenType == TokenType.Asterisk ||
                   this.lookAhead.TokenType == TokenType.Division)
            {
                Move();
                expr = new ArithmeticExpression(expr,  Factor(), token);
            }

            return expr;
        }
	
        private Expression Factor()
        {
            switch (this.lookAhead.TokenType)
            {
                case TokenType.LeftParens:
                    Match(TokenType.LeftParens);
                    var expr = Expr();
                    Match(TokenType.RightParens);
                    return expr;
                case TokenType.IntConstant:
                    var token = this.lookAhead;
                    Match(TokenType.IntConstant);
                    return new ConstantExpression(ExpressionType.Int, token);
                case TokenType.FloatConstant:
                    token = this.lookAhead;
                    Match(TokenType.FloatConstant);
                    return new ConstantExpression(ExpressionType.Float, token);
                case TokenType.StringConstant:
                    token = this.lookAhead;
                    Match(TokenType.StringConstant);
                    return new ConstantExpression(ExpressionType.String, token);
                default:
                    Match(TokenType.Identifier);
                    break;
            }

            return null;
        }
        private Statement AssignationStatement(IdExpression id)
        {
            if (this.lookAhead.TokenType == TokenType.Identifier)
            {
                Match(TokenType.Identifier);
            }
            Match(TokenType.Assignation);
            var expression = LogicalOrExpr();
            this.Match(TokenType.SemiColon);
            return new AssignationStatement(id, expression);
        }

        private void Decls()
        {
            if (this.lookAhead.TokenType == TokenType.Colon)
            {
                Decl();
                Decls();
            }
        }

        private void Decl()
        {
            Match(TokenType.Colon);
            Type();
            Match(TokenType.SemiColon);
            if (this.lookAhead.TokenType == TokenType.Identifier)
            {
                this.Match(TokenType.Identifier);
            }
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
                throw new ApplicationException($"Syntax error! expected {tokenType} but found {this.lookAhead.TokenType}. Line: {this.lookAhead.Line}, Column: {this.lookAhead.Column}");
            }
            this.Move();
        }
    }
}
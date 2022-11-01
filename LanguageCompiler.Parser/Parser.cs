﻿using LanguageCompiler.Lexer;

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

        public void Parse()
        {
            Code();
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
                case TokenType.Assignation:
                    AssignationStatement();
                    break;
                case TokenType.WhileKeyword:
                    WhileStatement();
                    break;
                case TokenType.PrintKeyword:
                    PrintStatement();
                    break;
                case TokenType.IfKeyword:
                    this.Match(TokenType.IfKeyword);
                    this.Match(TokenType.LeftParens);
                    LogicalOrExpr();
                    this.Match(TokenType.RightParens);
                    Stmt();
                    if (this.lookAhead.TokenType != TokenType.ElseKeyword)
                    {
                        break;
                    }
                    this.Match(TokenType.ElseKeyword);
                    Stmt();
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
            LogicalOrExpr();
            ParamsPrime();
        }

        private void ParamsPrime()
        {
            if (this.lookAhead.TokenType == TokenType.Comma)
            {
                Match(TokenType.Comma);
                LogicalOrExpr();
                ParamsPrime();
            }
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
            LogicalAndExpr();
            while (this.lookAhead.TokenType == TokenType.LogicalOr)
            {
                Move();
                LogicalAndExpr();
            }
        }

        private void LogicalAndExpr()
        {
            EqExpr();
            while (this.lookAhead.TokenType == TokenType.LogicalAnd)
            {
                Move();
                EqExpr();
            }
        }
	
        private void EqExpr()
        {
            RelExpr();
            while (this.lookAhead.TokenType == TokenType.Equal || this.lookAhead.TokenType == TokenType.NotEqual)
            {
                Move();
                RelExpr();
            }
        }
	
        private void RelExpr()
        {
            Expr();
            while (this.lookAhead.TokenType == TokenType.LessThan ||
                   this.lookAhead.TokenType == TokenType.LessOrEqualThan ||
                   this.lookAhead.TokenType == TokenType.GreaterThan ||
                   this.lookAhead.TokenType == TokenType.GreaterOrEqualThan)
            {
                var token = this.lookAhead;
                Move();
                Expr();
            }
        }
	
        private void Expr()
        {
            Term();
            while (this.lookAhead.TokenType == TokenType.Plus ||
                   this.lookAhead.TokenType == TokenType.Minus)
            {
                Move();
                Term();
            }
        }
	
        private void Term()
        {
            Factor();
            while (this.lookAhead.TokenType == TokenType.Asterisk ||
                   this.lookAhead.TokenType == TokenType.Division ||
                   this.lookAhead.TokenType == TokenType.GreaterThan)
            {
                Move();
                Factor();
            }
        }
	
        private void Factor()
        {
            switch (this.lookAhead.TokenType)
            {
                case TokenType.LeftParens:
                    Match(TokenType.LeftParens);
                    Expr();
                    Match(TokenType.RightParens);
                    break;
                case TokenType.IntConstant:
                    Match(TokenType.IntConstant);
                    break;
                case TokenType.FloatConstant:
                    Match(TokenType.FloatConstant);
                    break;
                case TokenType.StringConstant:
                    Match(TokenType.StringConstant);
                    break;
                default:
                    Match(TokenType.Identifier);
                    break;
            }
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
                throw new ApplicationException($"Syntax error! expected {tokenType} but found {this.lookAhead.TokenType}. Line: {this.lookAhead.Line}, Column: {this.lookAhead.Column}");
            }
            this.Move();
        }
    }
}
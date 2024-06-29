using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Compiler
{
    internal abstract class NodeAST
    {
        protected abstract TokenType Type { get; }
    }
    internal abstract class Expression:NodeAST
    {
        public abstract object Evaluate();
        public abstract bool CheckSemantic();
    }
    internal class BinaryExpretion:Expression
    {
        Expression leftExpretion;
        Expression rightExpretion;
        TokenType op;
        protected override TokenType Type => TokenType.BINARY_EXPRESSION;

        public BinaryExpretion(Expression leftExpretion, Expression rightExpretion, TokenType op)
        {
            this.leftExpretion = leftExpretion;
            this.rightExpretion = rightExpretion;
            this.op = op;
        }


        public override bool CheckSemantic()
        {
            throw new NotImplementedException();
        }

        public override object Evaluate()
        {
            throw new NotImplementedException();
        }
    }
    internal class UnaryExpretion: Expression
    {
        Expression expretion { get; }

        protected override TokenType Type => TokenType.UNARY_EXPRESSION;

        TokenType op;
        public UnaryExpretion(Expression expretion, TokenType op)
        {
            this.expretion = expretion;
            this.op = op;
        }

        public override bool CheckSemantic()
        {
            throw new NotImplementedException();
        }
        public override object Evaluate()
        {
            throw new NotImplementedException();
        }
    }
    internal class LiteralExpretion:Expression
    {
        object value { get; }

        protected override TokenType Type => TokenType.LITERAL_EXPRESSION;

        TokenType op;

        public LiteralExpretion(object value, TokenType op)
        {
            this.value = value;
            this.op = op;
        }

        public override bool CheckSemantic()
        {
            throw new NotImplementedException();
        }

        public override object Evaluate()
        {
            throw new NotImplementedException();
        }
    }
    internal class ParenthesisExpretion : Expression
    {
        protected override TokenType Type => TokenType.PATENTHESIS_EXPRESSION;

        TokenType leftParenthesis { get; }
        TokenType rightParenthesis { get; }
        Expression expression { get; }
        public ParenthesisExpretion(TokenType leftParenthesis, TokenType rightParenthesis, Expression expretion)
        {
            this.leftParenthesis = leftParenthesis;
            this.rightParenthesis = rightParenthesis;
            this.expression = expretion;
        }



        public override bool CheckSemantic()
        {
            throw new NotImplementedException();
        }

        public override object Evaluate()
        {
            throw new NotImplementedException();
        }
    }
    internal class AssingmentExpretion:Expression
    {
        public AssingmentExpretion(TokenType var, TokenType identifier, Expression expretion)
        {
            this.var = var;
            this.identifier = identifier;
            this.expretion = expretion;
        }

        TokenType var  { get; }
        TokenType identifier { get; }
        Expression expretion { get; }

        protected override TokenType Type => TokenType.ASSINGAMENT_EXPRETION;

        public override object Evaluate()
        {
            throw new NotImplementedException();
        }

        public override bool CheckSemantic()
        {
            throw new NotImplementedException();
        }

    }
    internal class WhileExpression : Expression
    {
        protected override TokenType Type => TokenType.WHILE_EXPRESSION;

        Expression CondicionalExpression { get; }
        Expression? BodyExpression { get; }
        public WhileExpression(Expression condicionalExpression)
        {
            CondicionalExpression = condicionalExpression;
        }

        public override bool CheckSemantic()
        {
            throw new NotImplementedException();
        }

        public override object Evaluate()
        {
            throw new NotImplementedException();
        }
    }
    internal class ForExpression : Expression
    {
        protected override TokenType Type => throw new NotImplementedException();

        public override bool CheckSemantic()
        {
            throw new NotImplementedException();
        }

        public override object Evaluate()
        {
            throw new NotImplementedException();
        }
    }
    internal class CardExpression : Expression
    {
        Expression ?NameExpression { get; }
        Expression ?TypeExpression { get; }
        Expression ?FactionExpression { get; }
        protected override TokenType Type => TokenType.CARD_EXPRESSION;

        public override bool CheckSemantic()
        {
            throw new NotImplementedException();
        }

        public override object Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}

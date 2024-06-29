using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Compiler
{
    internal class Parser
    {
        List<Token> tS;
        Token currentToken;
        int tokenPosition;

        public Parser(List<Token> tS)
        {
            this.tS = tS;
            tokenPosition = 0;
            currentToken = tS[0];
        }
        internal List<Token> TS { get => tS; set => tS = value; }
        internal Token CurrentToken { get => currentToken; set => currentToken = value; }
    }

}

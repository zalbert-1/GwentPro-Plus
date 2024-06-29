
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Compiler
{
    /// <summary>
    /// Realiza el analisis lexico a una cadena de texto
    /// </summary>
    internal class Lexer
    {
        private readonly string codeAppetizer;
        private readonly List<Token> tokensSequency;
        private int codeIndex;
        private char currentChar;
        public Lexer(string codeAppetizer)
        {
            this.codeAppetizer = codeAppetizer;
            this.codeIndex = 0;
            this.currentChar = codeAppetizer[codeIndex];
            this.tokensSequency = GetSequency();
        }
        internal List<Token> TokensSequency { get => tokensSequency; }

        private Token GetToken()
        {
            while(currentChar != '\0')
            {
                if(char.IsWhiteSpace(currentChar))
                {
                    SkipSpaces();
                    continue;
                }
                if(char.IsLetter(currentChar) || currentChar == '_')
                {
                    string word = WordAnalyzer();
                    return OwnWord(word);
                }
                if(char.IsDigit(currentChar))
                {
                    return new Token(TokenType.NUMBER, Int_Analyzer());
                }


                //si el caracter actual es un operador  
                if(currentChar == '+')
                {
                    MoveOn();
                    if(currentChar == '+') 
                    {
                        MoveOn();
                        return new Token(TokenType.OPERATOR,"++");
                    }
                    else if(currentChar == '=')
                    {
                        MoveOn();
                        return new Token(TokenType.OPERATOR,"+=");
                    }
                    else
                    {
                        return new Token(TokenType.OPERATOR, '+');
                    }
                }
                if (currentChar == '-')
                {
                    MoveOn();
                    if(currentChar == '=')
                    {
                        MoveOn();
                        return new Token(TokenType.OPERATOR,"-=");
                    }
                    else
                    {
                        return new Token(TokenType.OPERATOR,'-');
                    }
                }
                if (currentChar == '*')
                {
                    MoveOn();
                    return new Token(TokenType.OPERATOR, '*');
                }
                if(currentChar == '/')
                {
                    MoveOn();
                    return new Token(TokenType.OPERATOR, '/');
                }
                if (currentChar == '^')
                {
                    MoveOn();
                    return new Token(TokenType.OPERATOR, '^');
                }
                if(currentChar =='@')
                {
                    MoveOn();
                    if(currentChar =='@')
                    {
                        MoveOn();
                        return new Token(TokenType.OPERATOR,"@@");
                    }
                    return new Token(TokenType.OPERATOR, '@');
                }
                if(currentChar == '=')
                {
                    MoveOn();
                    if(currentChar == '=')
                    {
                        MoveOn();
                        return new Token(TokenType.OPERATOR, "==");
                    }
                    else if (currentChar == '>')
                    {
                        MoveOn();
                        return new Token (TokenType.DO, "=>");
                    }
                    else
                    {
                        return new Token(TokenType.EQUAL, '=');
                    }

                }
                if (currentChar == '>')
                {
                    MoveOn();
                    if(currentChar =='=')
                    {
                        MoveOn();
                        return new Token(TokenType.OPERATOR,">=");
                    }
                    else
                    {
                        return new Token(TokenType.OPERATOR, '>');
                    }
                }
                if(currentChar == '<')
                {
                    MoveOn();
                    if(currentChar == '=')
                    {
                        MoveOn();
                        return new Token(TokenType.OPERATOR , "<=");
                    }
                    else
                    {
                        return new Token(TokenType.OPERATOR,'<');
                    }

                }
                if(currentChar == '&')
                {
                    MoveOn();
                    if(currentChar == '&')
                    {
                        MoveOn();
                        return new Token(TokenType.OPERATOR,"&&");
                    }
                    return new Token(TokenType.UNKNOWN, '&');
                }
                if (currentChar == '|')
                {
                    MoveOn();
                    if (currentChar == '|')
                    {
                        MoveOn();
                        return new Token(TokenType.OPERATOR, "||");
                    }
                    return new Token(TokenType.UNKNOWN,'|');
                }


                //Puntuadores
                if(currentChar =='(')
                {
                    MoveOn();
                    return new Token(TokenType.L_PHARENTESYS,'(');
                }
                if(currentChar == ')')
                {
                    MoveOn();
                    return new Token(TokenType.R_PHARENTESYS,')');
                }
                if(currentChar == '[')
                {
                    MoveOn();
                    return new Token(TokenType.L_BRACKETS, '[');
                }
                if(currentChar == ']')
                {
                    MoveOn();
                    return new Token(TokenType.R_BRACKETS, ']');
                }
                if(currentChar == '{')
                {
                    MoveOn();
                    return new Token(TokenType.L_KEYS,'{');
                }
                if(currentChar == '}')
                {
                    MoveOn();
                    return new Token(TokenType.R_KEYS,'}');
                }
                if(currentChar == '.')
                {
                    MoveOn();
                    return new Token(TokenType.DOT,'.');
                }
                if(currentChar == ';')
                {
                    MoveOn();
                    return new Token(TokenType.D_COMMA,';');
                }
                if(currentChar == ',')
                {
                    MoveOn();
                    return new Token(TokenType.COMMA,',');
                }
                if(currentChar == '\"')
                {
                    MoveOn();
                    (bool, string, int) s = ReadCommillas(codeIndex,codeAppetizer);
                    if(!s.Item1)
                    {
                        throw new Exception("Expected ' \" ', invalid string");
                    }
                    codeIndex = s.Item3;
                    MoveOn();
                    return new Token(TokenType.STRING,s.Item2);
                }
                if(currentChar == ':')
                {
                    MoveOn();
                    return new Token(TokenType.TWO_POINTS, ":");
                }
                if (currentChar == '!')
                {
                    MoveOn();
                    return new Token(TokenType.OPERATOR, "!");
                }
                throw new Exception("Unknown character "+currentChar);
            }
            return new Token(TokenType.EOF,currentChar);
        }

       
        /// <returns>Returna la secuencia de tokens del 'codeAppetizer'</returns>
        private List<Token> GetSequency()
        {
            List<Token> result = [];
            while (codeIndex < codeAppetizer.Length )
            {
                Token token = GetToken();
                if(token.Type == TokenType.UNKNOWN)
                {
                    throw new Exception("Token desconocido "+"\'"+token.Value+"\'");
                }
                result.Add(token);
            }
            //result.Add(new Token(TokenType.EOF,'\0'));
            return result;
        }

        #region // Metodos Auxiliares
        private void SkipSpaces()
        {
            while(currentChar != '\0' && char.IsWhiteSpace(currentChar))
            {
                MoveOn();
            }

        }
        /// <summary>
        /// Aumenta la posicion y actualiza el currentChar
        /// </summary>
        private  void MoveOn()
        {
            codeIndex++;
            if (codeIndex < codeAppetizer.Length)
            {
                currentChar = codeAppetizer[codeIndex];
            }
            else
            {
                currentChar = '\0';
            }
        }
        /// <summary>
        /// Recibe un string como entrada 
        /// </summary>
        /// <returns>Retorna el token de la palabra reservada en el DLS</returns>
        private string WordAnalyzer()
        {
            string value = "";
            while(codeIndex < codeAppetizer.Length && char.IsLetterOrDigit(currentChar) || currentChar == '_')
            {
                value += currentChar;
                MoveOn();
            }
            return value;
        }
        /// <summary>
        /// Si currentChar representa un digito entonces evalua por cada posicion del 'codeAppetizer' si este seria un numero
        /// </summary>
        /// <returns>retorna el valor representado</returns>
        private double Int_Analyzer()
        {
            string number = "";
            int dotCounter = 0;
            while(currentChar != '\0' && (char.IsDigit(currentChar)) || currentChar == '.')
            {
                if(currentChar == '.') dotCounter++;
                number += currentChar;
                MoveOn();
            }
            if(char.IsLetter(currentChar))
            {
                number += currentChar;
                throw new Exception("Invalid Token"+"\'"+number+"\'");
            }
            if (currentChar == '.') number += '0';
            if(dotCounter > 1) { throw new Exception("Invalid number"); }
            return Convert.ToDouble(number);
        }
        private  Token OwnWord(string word)
        {
            return word switch
            {
                "GraveyardOfPlayer" => new Token(TokenType.GRAVEYARD_OF_PLAYER, word),
                "FielOfPlayer" => new Token(TokenType.FIELD_OF_PLAYER, word),
                "DeckOfPlayer" => new Token(TokenType.DECK_OF_PLAYER, word),
                "HandOfPlayer" => new Token(TokenType.HAND_OF_PLAYER, word),
                "while" => new Token(TokenType.WHILE,word),
                "for" => new Token(TokenType.FOR, word),
                "Pop" => ReadPharentesys(codeIndex+1,codeAppetizer,word),
                "Shuffle" => ReadPharentesys(codeIndex+1,codeAppetizer,word),
                "effect" => new Token(TokenType.EFFECT, word),
                "card" => new Token(TokenType.CARD, word),
                "Effect" => new Token(TokenType.EFFECT_CARD, word),
                "Action" => new Token(TokenType.ACTION, word),
                "Type" => new Token(TokenType.TYPE, word),
                "Faction" => new Token(TokenType.FACTION, word),
                "Power" => new Token(TokenType.POWER, word),
                "Range" => new Token(TokenType.RANGE, word),
                "OnActivation" => new Token(TokenType.ON_ACTIVATION, word),
                "Selector" => new Token(TokenType.SELECTOR, word),
                "Single" => new Token(TokenType.SINGLE, word),
                "PostAction" => new Token(TokenType.POST_ACTION, word),
                "Predicate" => new Token(TokenType.PREDICATE, word),
                "Params" => new Token(TokenType.PARAMS, word),
                "amount" => new Token(TokenType.AMOUNT_EFFECT, word),
                "Amount" => new Token(TokenType.AMOUNT_CARD, word),
                "Source" => new Token(TokenType.SOURCE, word),
                "target" => new Token(TokenType.TARGET, word),
                "targets" => new Token(TokenType.TARGETS, word),
                "Deck" => new Token(TokenType.DECK, word),
                "Owner" => new Token(TokenType.OWNER, word),
                "Board" => new Token(TokenType.BOARD, word),
                "Hand" => new Token(TokenType.HAND, word),
                "Graveyard" => new Token(TokenType.GRAVEYARD, word),
                "Field" => new Token(TokenType.FIELD, word),
                "in" => new Token(TokenType.IN, word),
                _ => new Token(TokenType.VARIABLE, word),
            };
        }
        private static (bool,string,int ) ReadCommillas(int position,string text)
        {
            string s = "";
            while (position < text.Length && text[position] != '\"')
            {
                s+= text[position];
                position++;
            }
            if (position < text.Length && text[position] == '\"')
            {
                return (true, s, position);
            }
            return (false,"",0);
        }
        /// <summary>
        /// Su objetivo es completar o mandar una exepcion si no se completa los token de POP y SHUFFLE
        /// </summary>
        /// <returns>Retorna POP o SHUFFLE en dependencia del parametro 'word'</returns>
        private Token ReadPharentesys(int position, string text,string word)
        {
            bool left = false;
            int countLeft = 0;
            string s = "";
            while(position < text.Length && countLeft < 2)
            {
                s+= text[position];
                if (text[position] == '(' || left)
                {
                    left = true;
                    if (text[position] == '(')
                    {
                        countLeft++;
                    }
                    else if(countLeft == 1 && text[position] ==')')
                    {
                        codeIndex = position;
                        MoveOn();
                        if(word == "Shuffle")
                        {
                            return new Token(TokenType.SHUFFLE,word);
                        }
                        else
                            return new Token(TokenType.POP, word);
                    }
                    position++;
                }
                else if (text[position] == ' ')
                {
                    position++;
                }
            }
            throw new Exception("Invalid Token "+'\"'+s+'\"');
        }
        #endregion
    }
}

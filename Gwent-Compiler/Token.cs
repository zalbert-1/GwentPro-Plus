using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gwent_Compiler
{
    internal enum TokenType
    {
        // tipo de datos 
        NUMBER, BOOLEAN,STRING,TRUE,FALSE,
        // PUNTUADORES
        L_PHARENTESYS,R_PHARENTESYS,L_KEYS,R_KEYS,L_BRACKETS,R_BRACKETS,COMMA,D_COMMA,COMMILLAS,
        // palabras reservadas
        OWNER,BOARD,FIELD,HAND,FIELD_OF_PLAYER,GRAVEYARD_OF_PLAYER,DECK_OF_PLAYER,HAND_OF_PLAYER,WHILE,FOR,TRIGGER_PLAYER,GRAVEYARD,FIND,PUSH,SHUFFLE,SEND_BOTTON,REMOVE,POP,DECK,TARGET,TARGETS,EFFECT,CARD,EFFECT_CARD,ACTION,TYPE,NAME,FACTION,POWER,RANGE,ON_ACTIVATION,SELECTOR,SINGLE,POST_ACTION,PREDICATE,AMOUNT_CARD,AMOUNT_EFFECT,PARAMS,SOURCE,
        //OPERADORES
        OPERATOR,EQUAL,DO,TWO_POINTS,DOT,IN,
        //?
        VARIABLE,UNKNOWN,EOF,

        BINARY_EXPRESSION,
        UNARY_EXPRESSION,
        LITERAL_EXPRESSION,
        PATENTHESIS_EXPRESSION,
        ASSINGAMENT_EXPRETION,
        WHILE_EXPRESSION,
        CARD_EXPRESSION
    }

    internal class Token
    {
        private readonly TokenType type;
        private object value;

        internal Token(TokenType type, object value)
        {
            this.type = type;
            this.value = value;
        }
        internal TokenType Type => type;
        public object Value { get => value; set => this.value = value; }
    }

}

using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace Gwent_Compiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "context.Deck.Shuffle  (                    )"+"; . \" eres ser y estas \" ";

            Lexer lexer = new Lexer(input);

            //Parser parser = new Parser(lexer.TokensSequency);


        }
    }
}

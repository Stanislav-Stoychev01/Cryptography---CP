using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptograchyCourseProject
{
    internal class Program
    {
        const int AlphabetSize = 30;

        static int GetPositiveDecryptionResult(int encryptedLetter)
        {
            if (encryptedLetter < 0)
                return GetPositiveDecryptionResult(encryptedLetter + AlphabetSize);
            else
                return encryptedLetter;
        }

        static Dictionary<string, int> GenerateAlphabetDictionary()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>
            {
                { "а", 1 },
                { "б", 2 },
                { "в", 3 },
                { "г", 4 },
                { "д", 5 },
                { "е", 6 },
                { "ж", 7 },
                { "з", 8 },
                { "и", 9 },
                { "й", 10 },
                { "к", 11 },
                { "л", 12 },
                { "м", 13 },
                { "н", 14 },
                { "о", 15 },
                { "п", 16 },
                { "р", 17 },
                { "с", 18 },
                { "т", 19 },
                { "у", 20 },
                { "ф", 21 },
                { "х", 22 },
                { "ц", 23 },
                { "ч", 24 },
                { "ш", 25 },
                { "щ", 26 },
                { "ъ", 27 },
                { "ь", 28 },
                { "ю", 29 },
                { "я", 30 },
            };

            return dictionary;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.GetEncoding("Cyrillic");
            Console.InputEncoding = Encoding.GetEncoding("Cyrillic");

            Console.WriteLine("Въведете съобщение за крипиране: ");
            string message = Console.ReadLine();

            int[] messageNumberArray = new int[message.Length];
            Dictionary<string, int> alphabetBinding = GenerateAlphabetDictionary();

            for (int i = 0; i < message.Length; i++)
            {
                int currentLetterNumber = alphabetBinding.FirstOrDefault(x => x.Key == message[i].ToString()).Value;
                messageNumberArray[i] = currentLetterNumber;
            }

            Console.WriteLine("Въведете стойност за k1: ");
            int k1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Въведете стойност k2: ");
            int k2 = int.Parse(Console.ReadLine());

            string encryptedMessage = "";
            int[] multipliers = new int[messageNumberArray.Length];

            for (int i = 0; i < messageNumberArray.Length; i++)
            {
                int currentLetterNumber = messageNumberArray[i];
                int encryptFormulaOne = (k1 * currentLetterNumber) + k2;
                int multiplier = (encryptFormulaOne / AlphabetSize) % 3;
                multipliers[i] = multiplier;
                int encryptedLetterNumber = encryptFormulaOne % AlphabetSize;
                string encryptedLetter = alphabetBinding.FirstOrDefault(x => x.Value == encryptedLetterNumber).Key;
                encryptedMessage += encryptedLetter;
            }

            Console.WriteLine("Криптираното съобщение е: {0}", encryptedMessage);
            Console.WriteLine("Започва декриптиране...");

            string decryptedMessage = "";

            for (int i = 0; i < encryptedMessage.Length; i++)
            {
                int encryptedNumber = alphabetBinding.FirstOrDefault(x => x.Key == encryptedMessage[i].ToString()).Value;
                int decryptFormulaK2 = encryptedNumber - k2;
                int decryptFormulaК1 = (decryptFormulaK2 / k1);
                int decryptFormulaResult;

                if (multipliers[i] != 0)
                {
                    decryptFormulaResult = decryptFormulaК1 + (multipliers[i] * 10);
                }
                else
                {
                    decryptFormulaResult = decryptFormulaК1;
                }

                int decryptionNumber = GetPositiveDecryptionResult(decryptFormulaResult);
                var decryptedLetter = alphabetBinding.FirstOrDefault(x => x.Value == decryptionNumber).Key;
                Console.WriteLine("Декриптираната буква е: {0}", decryptedLetter);
                decryptedMessage += decryptedLetter;
                Console.WriteLine();
            }

            Console.WriteLine("Декриптираното съобщение е: {0}", decryptedMessage);
            Console.ReadLine();
        }
    }
}

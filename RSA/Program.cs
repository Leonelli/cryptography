using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using System.IO;

namespace RSA
{
     public class Program
    {
        public static bool Check(string [] args)
        {
            if (File.Exists(args[1]))
            {
                Console.WriteLine(args[1]+ " esiste");
                if (File.Exists(args[2]))
                {
                    Console.WriteLine(args[2] + " esiste");
                    if (!File.Exists(args[3])) //output filename control
                    {
                        Console.WriteLine(args[3] + " non esiste, lo creo");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine(args[3] + " esiste già, non corretto");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine(args[2] + " non esiste");
                    return false;
                }
            }
            else
            {
                Console.WriteLine(args[1] + " non esiste");
                return false;
            }
        }
        public static void Encode(string[] args)
        {
            Console.WriteLine("Encode ... ");

            if (Check(args))
            {
                // verifica parametri e cifrare
                string inputfilename = File.ReadAllText(args[2]);
                string Keyfilename = File.ReadAllText(args[1]);
                string RSA = Crypto_Utils.EncryptRSA(inputfilename,Keyfilename);
                Console.WriteLine(RSA);
                File.WriteAllText(args[3], RSA);
                Console.WriteLine("Encode Completed ");
            }
            else
            {
                Console.WriteLine("Encode failed");           
            }
        }
        public static void Decode(string[] args)
        {
            Console.WriteLine("Decode ... ");
            if (Check(args))
            {
                // verifica parametri e decifrare
                string Keyfilename = File.ReadAllText(args[1]);
                string inputfilename = File.ReadAllText(args[2]);
                string RSA = Crypto_Utils.DecryptRSA(inputfilename, Keyfilename);
                Console.WriteLine(RSA);
                File.WriteAllText(args[3], RSA);
                Console.WriteLine("Decode completed ");
            }
            else
            {
                Console.WriteLine("Decode failed");
            }
        }
        public static void Help()
        {
            Console.WriteLine();
            Console.WriteLine("Syntax: RSA.exe [ENC|DEC] <Keyfilename> <inputfilename> <outputfilename>");
            Console.WriteLine("  es: RSA.exe ENC Chiave.pub.xml plaintext.txt plaintext.rsa");
            Console.WriteLine("  es: RSA.exe DEC Chiave.pri.xml plaintext.rsa plaintext.txt");
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("RSA encode - decode");
/*  
			// DEBUG
            Console.WriteLine(args.Length);
            Console.WriteLine("-------------------------");
            Console.WriteLine(args[0]);
            Console.WriteLine(args[1]);
            Console.WriteLine(args[2]);
            Console.WriteLine(args[3]);
*/
            if (args.Length != 4)
            {
                Console.WriteLine("Syntax ERROR!");
                Console.WriteLine("Numero di argomenti non corretti");
                Help();
            }

            switch (args[0].ToUpper())
            {
                case "ENC":
                    Encode(args);
                    break;
                case "DEC":
                    Decode(args);
                    break;
                default:
                    Console.WriteLine("Controlla i comandi disponibili");
                    Help();
                    break;
            }
            Console.ReadKey();
        }
    }
}

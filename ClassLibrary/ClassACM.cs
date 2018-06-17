using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;

namespace crypto
{
    public class ClassACM
    {
        public List<String> EncodeFileList(List<String> files, string outDir, string key)
        {
            List<String> fileCriptati = new List<string>();
            foreach (string f in files)
            {
                string tmp = EncodeFile(f, outDir, key);
                fileCriptati.Add(tmp);
            }
            return fileCriptati;
            //return null;
        }
        public List<String> DecodeFileList(bool checkMD5, List<String> files, string outDir, string key)
        {
            List<String> fileCriptati = new List<string>();
            foreach (string f in files)
            {
                string tmp = DecodeFile(checkMD5, f, outDir, key);
                fileCriptati.Add(tmp);
            }
            return fileCriptati;
            //return null;
        }

        /// <summary>
        /// Metodo per cifrare un file in formato ACM (Algoritmo Crittografico Marconi)
        /// </summary>
        /// <param name="fileName">Nome del file es: "c:\lavoro-temp\prova.txt"</param>
        /// <param name="outDir">Directory di output es: "c:\lavoro-temp"</param>
        /// <param name="key">Chiave simmetrica di cifratura</param>
        /// <returns>Log dell'azione o di ventuali errori</returns>
        public string EncodeFile(string fileName, string outDir, string key)
        {
            string f_inp = Path.GetFileName(fileName);
            string f_out = Path.ChangeExtension(outDir + "\\" + f_inp, "acm");

            string sFile = File.ReadAllText(fileName);
            string sMD5 = Crypto_Utils.HashMD5(sFile);
            string sACM = Crypto_Utils.EncryptAES(sFile, key);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("ACM");
            sb.AppendLine("File=" + f_inp);
            sb.AppendLine("Hash=" + sMD5);
            sb.AppendLine("Size=" + sACM.Length);
            sb.AppendLine("Type=AES");
            sb.AppendLine("@@@@@");
            sb.Append(sACM);
            //MessageBox.Show(f_out);
            //MessageBox.Show(sb.ToString());
            File.WriteAllText(f_out, sb.ToString());

            return "Percorso: " + f_out + "\n File " + f_inp + " criptato";
        }

        public string DecodeFile(bool checkMD5, string fileName, string outDir, string key)
        {
            string f_inp = Path.GetFileName(fileName);
            string[] text = System.IO.File.ReadAllLines(fileName);
            string f_out = "", name = "", Hash = "", sFile = "", sMD5 = "", plantext = "";
            if (Path.GetExtension(fileName) == ".acm")
            {
                if (text[0].Contains("ACM"))
                {
                    name = text[1].Replace("File=", "");
                    f_out = outDir + "\\" + name;
                    Hash = text[2].Replace("Hash=", "");
                    sFile = text[6];

                    plantext = Crypto_Utils.DecryptAES(sFile, key);
                    sMD5 = Crypto_Utils.HashMD5(plantext); //cambio acm con testo in chiaro

                    if (checkMD5)
                    {
                        if (sMD5 == Hash)
                        {
                            //MessageBox.Show("uguale");
                        }
                        else
                        {
                            return "MD5 non corretto";
                            //MessageBox.Show("diverso");
                        }
                    }

                    if (name == "" || name == null)
                    {
                        return "Il file non è nel formato corretto o non può essere decriptato";
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(plantext);
                        if (File.Exists(f_out))
                        {
                            //MessageBox.Show(f_out + " esiste");
                            File.WriteAllText(outDir+"\\"+ "new_" + name, sb.ToString());
                            return "Percorso: " + f_out + "\n File " + name + " decriptato";
                        }
                        else
                        {
                            File.WriteAllText(f_out, sb.ToString());
                            return "Percorso: " + f_out + "\n File " + name + " decriptato";
                        }

                    }
                }
                return "Contenuto del file non corretto!";
            }
            return "Estensione del file non corretta!";
        }   


        public string Md5FileString(string fileName)
        {
            return Crypto_Utils.HashMD5(File.ReadAllText(fileName));
        }
    }
}
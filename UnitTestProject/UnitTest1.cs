using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using crypto;
using System.IO;
using ClassLibrary;
using RSA;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        ClassACM c = new ClassACM();

        string txtChiaro = Directory.GetCurrentDirectory() + @"\Test\test.txt";
        string fileACM = Directory.GetCurrentDirectory() + @"\Test\test.acm";
        string fileDecriptato = Directory.GetCurrentDirectory() + @"\Test\prova_decrypted.txt";
        string dir = Directory.GetCurrentDirectory() + @"\Test\";

        [TestMethod]
        public void EncodeListFile()
        {
            List<String> file = new List<string>();
            file.Add(dir + "test.txt");
            file.Add(dir + "prova.txt");
            c.EncodeFileList(file, dir, "chiave");

        }

        [TestMethod]
        public void DecodeListFile()
        {

            List<String> file = new List<string>();
            file.Add(dir + "test.acm");
            file.Add(dir + "prova.acm");
            c.DecodeFileList(true, file, dir, "chiave");
        }

        [TestMethod]
        public void TestCheck()
        {
            string[] argsCod = "ENC Chiave.pub.xml plaintext.txt plaintext.rsa".Split(' ');
            if (RSA.Program.Check(argsCod))
            {
                RSA.Program.Encode(argsCod);
            }
            string[] argsDec = "DEC Chiave.pri.xml plaintext.rsa plaintext.txt".Split(' ');
            if (RSA.Program.Check(argsCod))
            {
                RSA.Program.Decode(argsDec);
            }
        }




        [TestMethod]
        public void TestHelp()
        {
            Program.Help();
        }

        [TestMethod]
        public void CifraFileRSA()
        {

            string testoCifrato = "";
            string testoConfronta = "";

            string[] args = "ENC Chiave.pub.xml plaintext.txt plaintext.rsa".Split(' ');
            RSA.Program.Encode(args);
            testoCifrato = File.ReadAllText(dir + "plaintext.rsa");
            testoConfronta = File.ReadAllText(dir + "plaintext_test.rsa");
            Assert.AreEqual(testoCifrato, testoConfronta);


            string[] args1 = "Es Chiave.pub.xml plaintext.txt plaintext.rsa".Split(' ');
            RSA.Program.Encode(args1);
            testoCifrato = File.ReadAllText(dir + "plaintext.rsa");
            testoConfronta = File.ReadAllText(dir + "plaintext_test.rsa");
            Assert.AreEqual(testoCifrato, testoConfronta);

            string[] args2 = "ENC Chiae.pub.xml plaintext.txt plaintext.rsa".Split(' ');
            RSA.Program.Encode(args2);
            testoCifrato = File.ReadAllText(dir + "plaintext.rsa");
            testoConfronta = File.ReadAllText(dir + "plaintext_test.rsa");
            Assert.AreEqual(testoCifrato, testoConfronta);

            string[] args3 = "ENC Chiave.pub.xml plaint.txt plaintext.rsa".Split(' ');
            RSA.Program.Encode(args3);
            testoCifrato = File.ReadAllText(dir + "plaintext.rsa");
            testoConfronta = File.ReadAllText(dir + "plaintext_test.rsa");
            Assert.AreEqual(testoCifrato, testoConfronta);

            string[] args4 = "ENC Chiave.pub.xml plaintext.txt pntext.rsa".Split(' ');
            RSA.Program.Encode(args4);
            testoCifrato = File.ReadAllText(dir + "plaintext.rsa");
            testoConfronta = File.ReadAllText(dir + "plaintext_test.rsa");
            Assert.AreEqual(testoCifrato, testoConfronta);

            //if (RSA.Program.Check(args))
            //{

            //}

        }

        [TestMethod]
        public void RSATestCheck()
        {
            string[] args = " ".Split(' ');
        }

        [TestMethod]
        public void DecifraFileRSA()
        {
            string testoCifrato = "";
            string testoConfronta = "";


            string[] args = "DEC Chiave.pri.xml plaintext.rsa plaintext.txt".Split(' ');
            RSA.Program.Decode(args);
            testoCifrato = File.ReadAllText(dir + "plaintext.txt");
            testoConfronta = File.ReadAllText(dir + "plaintext_test.txt");
            Assert.AreEqual(testoCifrato, testoConfronta);

            string[] args1 = "Dfd Chiave.pri.xml plaintext.rsa plaintext.txt".Split(' ');
            RSA.Program.Decode(args1);
            testoCifrato = File.ReadAllText(dir + "plaintext.txt");
            testoConfronta = File.ReadAllText(dir + "plaintext_test.txt");
            Assert.AreEqual(testoCifrato, testoConfronta);

            string[] args2 = "DEC Chive.ri.xml plaintext.rsa plaintext.txt".Split(' ');
            RSA.Program.Decode(args);
            testoCifrato = File.ReadAllText(dir + "plaintext.txt");
            testoConfronta = File.ReadAllText(dir + "plaintext_test.txt");
            Assert.AreEqual(testoCifrato, testoConfronta);

            string[] args3 = "DEC Chiave.pri.xml plainte.rsa plaintext.txt".Split(' ');
            RSA.Program.Decode(args);
            testoCifrato = File.ReadAllText(dir + "plaintext.txt");
            testoConfronta = File.ReadAllText(dir + "plaintext_test.txt");
            Assert.AreEqual(testoCifrato, testoConfronta);

            string[] args4 = "DEC Chiave.pri.xml plaintext.rsa plaixt.txt".Split(' ');
            RSA.Program.Decode(args);
            testoCifrato = File.ReadAllText(dir + "plaintext.txt");
            testoConfronta = File.ReadAllText(dir + "plaintext_test.txt");
            Assert.AreEqual(testoCifrato, testoConfronta);

        }



        [TestMethod]
        public void EncodeShouldCryptAES()
        {
            string outdir = Directory.GetCurrentDirectory() + @"\Test\";
            c.EncodeFile(txtChiaro, outdir, "chiave");
            outdir = outdir + "test.acm";
            string fileA = File.ReadAllText(fileACM);
            string fileB = File.ReadAllText(outdir);
            Assert.AreEqual(fileA, fileB);

            //File.Delete(outdir);
        }

        [TestMethod]
        public void DecodeShouldDecryptAES()
        {
            string outdir = Directory.GetCurrentDirectory() + @"\Test\";
            //string outdir = @"C:\lavoro-temp\rsa\";
            c.DecodeFile(true, fileACM, outdir, "chiave");
            outdir = outdir + "prova_decrypted.txt";
            string fileA = File.ReadAllText(fileDecriptato);
            string fileB = File.ReadAllText(outdir);
            Assert.AreEqual(fileA, fileB);
        }


        [TestMethod]
        public void decodeTestNoACM()
        {
            string outdir = Directory.GetCurrentDirectory() + @"\Test\";
            c.DecodeFile(true, Directory.GetCurrentDirectory() + @"\Test\provaNoACM.acm", outdir, "chiave");
            outdir = outdir + "prova_ACM.txt";
        }


        [TestMethod]
        public void decodeTestMD5()
        {
            string outdir = Directory.GetCurrentDirectory() + @"\Test\";
            c.DecodeFile(true, Directory.GetCurrentDirectory() + @"\Test\provaMD5.acm", outdir, "chiave");
            outdir = outdir + "prova_MD5.txt";
        }

        [TestMethod]
        public void decodeTestNoMD5()
        {
            string outdir = Directory.GetCurrentDirectory() + @"\Test\";
            c.DecodeFile(false, Directory.GetCurrentDirectory() + @"\Test\provaMD5.acm", outdir, "chiave");
            outdir = outdir + "prova_MD5.txt";
        }

        [TestMethod]
        public void decodeTestNoDotAcm()
        {
            string outdir = Directory.GetCurrentDirectory() + @"\Test\";
            c.DecodeFile(true, Directory.GetCurrentDirectory() + @"\Test\prova.txt", outdir, "chiave");
            outdir = outdir + "prova_dot_acm.txt";
        }



        /*[TestMethod]
        public void TestMD5()
        {
            string key = "chiave";
            string TestoCifrato = File.ReadLines(DirACMoriginal).Skip(6).Take(1).First();
            Assert.AreEqual(true, CLSACM.VerificaMD5(TestoCifrato, key, DirACMoriginal));
        }*/

        [TestMethod]
        public void Test_RSA()
        {
            var pubKey_1 = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            var priKey_1 = "<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=</Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";

            var testo__1 = "testing ...";
            var testo__2 = "Hello World!";

            var s1_enc = Crypto_Utils.EncryptRSA(testo__1, pubKey_1);
            var s1_dec = Crypto_Utils.DecryptRSA(s1_enc, priKey_1);

            Assert.AreEqual(s1_dec, testo__1);

            var s2_enc = Crypto_Utils.EncryptRSA(testo__2, pubKey_1);
            var s2_dec = Crypto_Utils.DecryptRSA(s2_enc, priKey_1);

            Assert.AreEqual(s2_dec, testo__2);
        }

        [TestMethod]
        public void Test_Base64()
        {
            string Text1 = "Man";
            string Code1 = "TWFu";

            Assert.AreEqual(Text1, Crypto_Utils.Base64_Decode(Code1));
            Assert.AreEqual(Code1, Crypto_Utils.Base64_Encode(Text1));

            string Text2 = "I.T.T. Marconi Rovereto";
            string Code2 = "SS5ULlQuIE1hcmNvbmkgUm92ZXJldG8=";

            Assert.AreEqual(Text2, Crypto_Utils.Base64_Decode(Code2));
            Assert.AreEqual(Code2, Crypto_Utils.Base64_Encode(Text2));

            Assert.AreNotEqual(Code2, Crypto_Utils.Base64_Encode("ITT - Marconi Rovereto"));
        }
    }
}



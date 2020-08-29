using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using SevenZip.Compression.LZMA;

namespace ArzoraLifeServerConsole
{
    class Program
    {

        static string modname = "@ArzoraLife";

        static void Main(string[] args)
        {
            Console.Write("Quelle action voulez vous faire ? (UPDATE,XML) > ");

            string action = Console.ReadLine();

            switch (action)
            {
                case "UPDATE":
                    update();
                    break;
                case "XML":
                    xml();
                    break;
                default:
                    Console.WriteLine("Action non reconnue");
                    break;
            }
        }

        static void update()
        {

            Console.Write("INPUT > ");
            string inputFolder = Console.ReadLine();
            Console.Write("OUTPUT > ");
            string outputFolder = Console.ReadLine();

            List<string> tmp = getAllFile(inputFolder);
            List<FileLauncher> files = new List<FileLauncher>();
            int count = 0;

            foreach (string filepath in tmp)
            {
                count++;
                Console.WriteLine("   + Ajout de \"" + filepath + "\" " + count + " sur " + tmp.Count);
                FileLauncher file = new FileLauncher();
                file.path = filepath;
                file.hash = getFileMD5(inputFolder + "\\" + filepath);
                file.size = (new FileInfo(inputFolder + "\\" + filepath)).Length;

                Console.WriteLine("      - Path = " + file.path);
                Console.WriteLine("      - Hash = " + file.hash);
                Console.WriteLine("      - Size = " + file.size);
                files.Add(file);
            }


            string xml = "<dayzrp>\r\n    <patch mode =\"lzma\" launcherVersion=\"1.0.7\">\r\n";

            foreach (FileLauncher file in files)
            {
                string newFile = Path.GetFullPath(outputFolder + "\\" + file.path);
                string newFileDir = Path.GetDirectoryName(newFile);

                if (!Directory.Exists(newFileDir))
                {
                    Directory.CreateDirectory(newFileDir);
                }

                Console.Write("   + Compression de " + file.path + " : ");

                CompressFileLZMA(
                    inputFolder + "\\" + file.path,
                    newFile + ".lzma"
                );

                file.url = "@ArzoraLife/" + file.path;

                xml += "        <file path=\""+file.path+"\" hash=\""+file.hash+"\" url=\""+file.url+"\" size=\""+file.size+"\"/>\r\n";

                Console.WriteLine("OK");
            }

            xml += "    </patch>\r\n</dayzrp>";

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\main.xml", xml);
        }

        static void xml()
        {
            Console.Write("INPUT > ");
            string inputFolder = Console.ReadLine();

            List<string> tmp = getAllFile(inputFolder);
            List<FileLauncher> files = new List<FileLauncher>();
            int count = 0;

            foreach (string filepath in tmp)
            {
                count++;
                Console.WriteLine("   + Ajout de \"" + filepath + "\" " + count + " sur " + tmp.Count);
                FileLauncher file = new FileLauncher();
                file.path = filepath;
                file.hash = getFileMD5(inputFolder + "\\" + filepath);
                file.size = (new FileInfo(inputFolder + "\\" + filepath)).Length;

                Console.WriteLine("      - Path = " + file.path);
                Console.WriteLine("      - Hash = " + file.hash);
                Console.WriteLine("      - Size = " + file.size);
                files.Add(file);
            }

            string xml = "<dayzrp>\r\n    <patch mode =\"lzma\" launcherVersion=\"1.0.7\">\r\n";

            foreach (FileLauncher file in files)
            {
                file.url = "@ArzoraLife/" + file.path;
                xml += "        <file path=\"" + file.path + "\" hash=\"" + file.hash + "\" url=\"" + file.url + "\" size=\"" + file.size + "\"/>\r\n";
            }

            xml += "    </patch>\r\n</dayzrp>";

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\main.xml", xml);
        }
        private static void CompressFileLZMA(string inFile, string outFile)
        {
            SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
            FileStream input = new FileStream(inFile, FileMode.Open);
            FileStream output = new FileStream(outFile, FileMode.Create);

            // Write the encoder properties
            coder.WriteCoderProperties(output);

            // Write the decompressed file size.
            output.Write(BitConverter.GetBytes(input.Length), 0, 8);

            // Encode the file.
            coder.Code(input, output, input.Length, -1, null);
            output.Flush();
            output.Close();
        }

        private static void DecompressFileLZMA(string inFile, string outFile)
        {
            SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
            FileStream input = new FileStream(inFile, FileMode.Open);
            FileStream output = new FileStream(outFile, FileMode.Create);

            // Read the decoder properties
            byte[] properties = new byte[5];
            input.Read(properties, 0, 5);

            // Read in the decompress file size.
            byte[] fileLengthBytes = new byte[8];
            input.Read(fileLengthBytes, 0, 8);
            long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

            coder.SetDecoderProperties(properties);
            coder.Code(input, output, input.Length, fileLength, null);
            output.Flush();
            output.Close();
        }

        static List<string> getAllFile(string folder, string callbackFolder = "")
        {
            List<string> result = new List<string>();

            string[] directories = Directory.GetDirectories(folder);
            foreach (string dir in directories)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dir);
                List<string> tmp = getAllFile(dir, callbackFolder);
                foreach (string t in tmp)
                {
                    result.Add(callbackFolder + directoryInfo.Name + "/" + t);
                }
            }

            string[] files = Directory.GetFiles(folder);
            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                result.Add(info.Name);
            }

            return result;
        }

        static string getFileMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

    }

    class FileLauncher
    {
        public string path;
        public string hash;
        public string url;
        public long size;
    }
}

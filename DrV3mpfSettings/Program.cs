using System;
using System.IO;
using DrV3mpfCursor;
using DrV3mpfSettings;

namespace DrV3mpf
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //args =
            if (args.Length < 3)
            {
                MostrarUso();
                return;
            }

            string option = args[0].ToLower();
            string command = args[1].ToLower();
            string input = args[2];
            string output = args.Length > 3 ? args[3] : "";

            switch (option)
            {
                case "-settings":
                    if (command == "-extract")
                        ExtrairSettings(input, output);
                    else if (command == "-repack")
                        ReempacotarSettings(input, output);
                    else
                        Console.WriteLine("Comando inválido para -settings.");
                    break;

                case "-cursor":
                    if (command == "-extract")
                        ExtrairCursor(input, output);
                    else if (command == "-repack")
                        ReempacotarCursor(input, output);
                    else
                        Console.WriteLine("Comando inválido para -cursor.");
                    break;

                case "-map":
                    Console.WriteLine("Suporte para -map ainda não implementado.");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    MostrarUso();
                    break;
            }
        }

        private static void MostrarUso()
        {
            Console.WriteLine("Uso:");
            Console.WriteLine("  Extração:");
            Console.WriteLine("    DrV3mpf -settings -extract mpf_setting.dat output_folder");
            Console.WriteLine("    DrV3mpf -cursor -extract mpf_cursol.dat output_folder");
            Console.WriteLine("  Reempacotamento:");
            Console.WriteLine("    DrV3mpf -settings -repack input_folder output.dat");
            Console.WriteLine("    DrV3mpf -cursor -repack input_folder output.dat");
        }

        private static void ExtrairSettings(string inputFile, string outputFolder)
        {
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Arquivo não encontrado: {inputFile}");
                return;
            }

            var dat = new mpfSettingsDat(inputFile);
            dat.ExportAllChapters(outputFolder);
            Console.WriteLine($"Extração concluída: {inputFile} -> {outputFolder}");
        }

        private static void ReempacotarSettings(string inputFolder, string outputFile)
        {
            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Pasta não encontrada: {inputFolder}");
                return;
            }

            var dat = new mpfSettingsDat();
            dat.ImportAllChapters(inputFolder);
            dat.SaveToDat(outputFile);
            Console.WriteLine($"Reempacotamento concluído: {inputFolder} -> {outputFile}");
        }

        private static void ExtrairCursor(string inputFile, string outputFolder)
        {
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Arquivo não encontrado: {inputFile}");
                return;
            }

            var dat = new mpfCursorDat(inputFile);
            dat.ExportAllChapters(outputFolder);
            Console.WriteLine($"Extração concluída: {inputFile} -> {outputFolder}");
        }

        private static void ReempacotarCursor(string inputFolder, string outputFile)
        {
            if (!Directory.Exists(inputFolder))
            {
                Console.WriteLine($"Pasta não encontrada: {inputFolder}");
                return;
            }

            var dat = new mpfCursorDat();
            dat.ImportAllChapters(inputFolder);
            dat.SaveToDat(outputFile);
            Console.WriteLine($"Reempacotamento concluído: {inputFolder} -> {outputFile}");
        }
    }
}
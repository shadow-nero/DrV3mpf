using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using DrV3mpfSettings;

namespace DrV3mpfSettings;

public class mpfSettingsDat
{
    private Stream stream;
    public List<mpfSettingsChapter> Chapters = new List<mpfSettingsChapter>();

    public mpfSettingsDat() { }
    public mpfSettingsDat(string path)
    {
        stream = File.OpenRead(path);
        ReadHeader();
    }
    public void ReadHeader()
    {
        Chapters.Clear();
        using BinaryReader reader = new(stream);
        for (int i = 0; i < 7; i++)
            Chapters.Add(new mpfSettingsChapter(reader));
    }
    public void ExportChapter(int chapterIndex, string outputFolder)
    {
        if (chapterIndex < 0 || chapterIndex >= Chapters.Count)
            throw new ArgumentOutOfRangeException(nameof(chapterIndex), "Capítulo inválido.");

        string filePath = Path.Combine(outputFolder, $"Chapter_{chapterIndex}.txt");
        Chapters[chapterIndex].ExportToText(filePath);
        Console.WriteLine($"Capítulo {chapterIndex} exportado para {filePath}");
    }
    public void ExportAllChapters(string outputFolder)
    {
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        for (int i = 0; i < Chapters.Count; i++)
            ExportChapter(i, outputFolder);

        Console.WriteLine("Todos os capítulos foram exportados.");
    }
    public void ImportChapter(int chapterIndex, string filePath)
    {
        if (chapterIndex < 0 || chapterIndex >= Chapters.Count)
            throw new ArgumentOutOfRangeException(nameof(chapterIndex), "Capítulo inválido.");

        Chapters[chapterIndex] = mpfSettingsChapter.ReadFromText(filePath);
        Console.WriteLine($"Capítulo {chapterIndex} importado de {filePath}");
    }
    public void ImportAllChapters(string inputFolder)
    {
        if (!Directory.Exists(inputFolder))
            throw new DirectoryNotFoundException($"A pasta '{inputFolder}' não foi encontrada.");

        string[] files = Directory.GetFiles(inputFolder, "Chapter_*.txt");
        foreach (string file in files)
        {
            mpfSettingsChapter chapter = mpfSettingsChapter.ReadFromText(file);
            int chapterIndex = Chapters.FindIndex(c => c.ID_Chapter == chapter.ID_Chapter);

            if (chapterIndex != -1)
            {
                Chapters[chapterIndex] = chapter;
                Console.WriteLine($"Capítulo {chapter.ID_Chapter} importado de {file}");
            }
            else
            {
                Console.WriteLine($"Aviso: O capítulo {chapter.ID_Chapter} não existe no arquivo original e foi ignorado.");
            }
        }
    }
    public void SaveToDat(string outputPath)
    {
        using FileStream fs = File.Create(outputPath);
        using BinaryWriter writer = new(fs);

        foreach (var chapter in Chapters)
            chapter.WriteToBinary(writer);

        Console.WriteLine($"Arquivo salvo como {outputPath}");
    }
}
using System;
using System.IO;
using System.Collections.Generic;

namespace DrV3mpfSettings
{
    public class mpfSettingsSection
    {
        public List<ushort> Data = new();

        public mpfSettingsSection() { }

        public mpfSettingsSection(BinaryReader reader)
        {
            for (int i = 0; i < 38; i++)
                Data.Add(reader.ReadUInt16());
        }

        public void ExportToString(StreamWriter writer, string prefix = "")
        {
            for (ushort i = 0; i < Data.Count; i++)
                writer.WriteLine($"{prefix}{(ValueNames.ContainsKey(i) ? ValueNames[i] : $"Unk{i}")}: {Data[i]}");
        }

        public static mpfSettingsSection ReadFromString(StreamReader reader)
        {
            mpfSettingsSection section = new();
            for (ushort i = 0; i < 38; i++)
                section.Data.Add(ushort.Parse(reader.ReadLine().Split(": ")[1]));
            return section;
        }

        public Stream ToStream()
        {
            MemoryStream stream = new();
            using (BinaryWriter writer = new(stream))
            {
                foreach (var a in Data)
                    writer.Write(BitConverter.GetBytes(a));
            }
            stream.Position = 0;
            return stream;
        }

        public void WriteToBinary(BinaryWriter writer)
        {
            foreach (ushort value in Data)
                writer.Write(value);
        }

        public static Dictionary<ushort, string> ValueNames = new()
        {
            [0] = "Model_ID",
        };
    }

    public class mpfSettingsChapter
    {
        public ushort ID_Chapter { get; private set; }
        public List<mpfSettingsSection> Sections = new();

        public mpfSettingsChapter() { }
        public mpfSettingsChapter(BinaryReader reader)
        {
            ID_Chapter = reader.ReadUInt16();
            reader.BaseStream.Position -= 2;

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                ushort chapterShort = reader.ReadUInt16();
                if (ID_Chapter != chapterShort)
                {
                    reader.BaseStream.Position -= 2;
                    break;
                }
                Sections.Add(new mpfSettingsSection(reader));
            }
        }

        public void ExportToText(string filePath)
        {
            using StreamWriter writer = new(filePath);
            writer.WriteLine($"Chapter_ID: {ID_Chapter}");
            writer.WriteLine($"Total_Sections: {Sections.Count}");
            writer.WriteLine(new string('-', 30));

            for (int i = 0; i < Sections.Count; i++)
            {
                writer.WriteLine($"[Section_{i}]");
                Sections[i].ExportToString(writer, "  ");
                writer.WriteLine();
            }
        }

        public static mpfSettingsChapter ReadFromText(string filePath)
        {
            using StreamReader reader = new(filePath);
            mpfSettingsChapter chapter = new mpfSettingsChapter();

            chapter.ID_Chapter = ushort.Parse(reader.ReadLine().Split(": ")[1]);
            int totalSections = int.Parse(reader.ReadLine().Split(": ")[1]);
            reader.ReadLine();

            for (int i = 0; i < totalSections; i++)
            {
                reader.ReadLine();
                chapter.Sections.Add(mpfSettingsSection.ReadFromString(reader));
                reader.ReadLine();
            }

            return chapter;
        }
        public void WriteToBinary(BinaryWriter writer)
        {
            //writer.Write(ID_Chapter);
            foreach (var section in Sections)
            {
                writer.Write(ID_Chapter);
                section.WriteToBinary(writer);
            }
        }
    }
}
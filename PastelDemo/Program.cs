namespace PastelDemo
{
    using System;
    using System.Drawing;
    using System.Linq;
    using Pastel;

    class Program
    {
        static void Main(string[] args)
        {
            "ENTER".Pastel(Color.FromArgb(165, 229, 250));
            Console.WriteLine($"Press {"ENTER".Pastel(Color.FromArgb(165, 229, 250))} to continue");

            var spectrum = new (string color, string letter)[]
            {
                ("#124542", "a"),
                ("#185C58", "b"),
                ("#1E736E", "c"),
                ("#248A84", "d"),
                ("#20B2AA", "e"),
                ("#3FBDB6", "f"),
                ("#5EC8C2", "g"),
                ("#7DD3CE", "i"),
                ("#9CDEDA", "j"),
                ("#BBE9E6", "k")
            };
            Console.WriteLine(string.Join("", spectrum.Select(s => s.letter.Pastel(s.color))));

            Console.WriteLine("Colorize me".Pastel(Color.Black).PastelBg("FFD000"));
        }
    }
}

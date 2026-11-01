using System;
using System.Globalization;
using System.IO;

namespace MyMGPixelEngine;
// Classe représentant une carte de pixels
internal class PixelMap
{
    // Tableau bidimensionnel contenant les pixels de l'image
    private int[,] picture;

    public int Width { get; set; }
    public int Height { get; set; }


    public PixelMap()
    {

    }

    // constructeur à partir d'un tableau
    public PixelMap(int width, int height, int[,] picture)
    {
        Width = width;
        Height = height;

        this.picture = picture;
    }

    // Constructeur à partir d'un fichier
    public PixelMap(string path)
    {
        SetPictureFromFile(path);
    }

    public void SetPictureFromFile_old(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            // Lecture de la largeur et de la hauteur de l'image
            Width = int.Parse(reader.ReadLine());
            Height = int.Parse(reader.ReadLine());

            // Initialisation du tableau de pixels avec la hauteur et la largeur lues
            this.picture = new int[Height, Width];
            int j = 0;
            while (reader.Peek() >= 0)
            {
                string line = reader.ReadLine();
                for (int i = 0; i < Width; i++)
                {
                    // Affectation de la valeur du pixel à partir du fichier
                    this.picture[j, i] = int.Parse(line[i].ToString());
                }
                j++;
                // Arrêt de la lecture si on a atteint la hauteur de l'image
                if (j == Height)
                {
                    break;
                }
            }
        }
    }

    public void SetPictureFromFile(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            // Lecture de la largeur et de la hauteur
            string widthLine = reader.ReadLine();
            string heightLine = reader.ReadLine();

            if (widthLine != null && heightLine != null)
            {
                Width = int.Parse(widthLine);
                Height = int.Parse(heightLine);

                // Initialisation du tableau de pixels
                this.picture = new int[Height, Width];

                int j = 0;
                string line;

                // On lit les lignes jusqu'à atteindre la hauteur demandée
                while (j < Height && (line = reader.ReadLine()) != null)
                {
                    // On découpe la ligne par les espaces
                    // StringSplitOptions.RemoveEmptyEntries permet d'ignorer les espaces doubles
                    string[] values = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < Width && i < values.Length; i++)
                    {
                        // On convertit chaque élément du tableau de mots en entier
                        this.picture[j, i] = int.Parse(values[i]);
                    }
                    j++;
                }
            }
        }
    }


    public void save(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            // Ecriture de la largeur et de la hauteur de l'image
            writer.WriteLine(Width);
            writer.WriteLine(Height);

            // Ecriture de chaque pixel de l'image
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    writer.Write(picture[j, i]);
                }
                writer.Write("\r\n");
            }

        }

    }



    // Méthode pour récupérer le tableau de pixels
    public int[,] getPicture()
    {
        return picture;
    }
}
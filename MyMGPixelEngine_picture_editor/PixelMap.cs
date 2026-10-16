using System.Globalization;
using System.IO;

// Classe représentant une carte de pixels
internal class PixelMap
{
    // Tableau bidimensionnel contenant les pixels de l'image
    private int[,] picture;

    public int Width { get; set; }
    public int Height { get; set; }


    // constructeur à partir d'un tableaus
    public PixelMap(int width, int height, int[,] picture)
    {
        Width = width;
        Height = height;

        this.picture = picture;
    }

    // Constructeur à partir d'un fichier
    public PixelMap(string path)
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
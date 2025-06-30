using carrossel.Properties;
using Microsoft.VisualBasic.ApplicationServices;
using System.Drawing;
using System.IO;

public class Banners
{
    public Image[] Fotos { get; private set; }

    public Banners(string caminhoDaPasta)
    {
        CarregarImagens(caminhoDaPasta);
    }

    private void CarregarImagens(string pasta)
    {
        if (!Directory.Exists(pasta))
        {
            Fotos = new Image[0];
            return;
        }

        string[] arquivos = Directory.GetFiles(pasta, "*.jpg"); // ou *.png, *.jpeg, etc.
        Fotos = new Image[arquivos.Length];

        for (int i = 0; i < arquivos.Length; i++)
        {
            Fotos[i] = Image.FromFile(arquivos[i]);
        }
    }
}
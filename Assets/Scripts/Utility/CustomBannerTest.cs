using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


static class CustomBannerTest
{
    //private static Sprite banner;

    static CustomBannerTest()
    {
        Sprites = new Sprite[Config.NumberOfRaces];
        try
        {
            for (int i = 0; i < Config.NumberOfRaces; i++)
            {
                Sprites[i] = LoadPNG($"UserData{Path.DirectorySeparatorChar}Banners{Path.DirectorySeparatorChar}{i + 1}.jpg");
                if (Sprites[i] == null)
                    Sprites[i] = LoadPNG($"UserData{Path.DirectorySeparatorChar}Banners{Path.DirectorySeparatorChar}{i + 1}.png");
            }

        }
        catch
        {
            State.GameManager.CreateMessageBox("Failed to read image, it may not have been in a correct format");
        }

    }

    static internal Sprite[] Sprites;

    static Sprite LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2, TextureFormat.BGRA32, false);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.

        }
        if (tex == null)
            return null;
        Rect rect = new Rect(new Vector2(0, 0), new Vector2(tex.width, tex.height));
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        int higherDimension = Math.Max(tex.width, tex.height);
        Sprite sprite = Sprite.Create(tex, rect, pivot, higherDimension);
        return sprite;
    }
}


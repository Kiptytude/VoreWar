using System;
using System.Collections.Generic;
using UnityEngine;


class ColorSwapPalette
{
    internal Material colorSwapMaterial;

    public ColorSwapPalette(Dictionary<int, Color> swap, bool[] clear = null, int maxClearRange = 256)
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;
        colorSwapTex.wrapMode = TextureWrapMode.Clamp;
        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        List<int> keys = new List<int>();

        foreach (var key in swap)
        {
            keys.Add(key.Key);
        }

        //Round to closest -- prevents mipmap issues or one off textures being white
        if (keys.Count > 0)
        {
            int currentKeyIndex = 0;
            for (int i = 0; i < colorSwapTex.width; i++)
            {
                if (clear != null && clear[i])
                    continue;
                if (maxClearRange != 256 && Math.Abs(keys[currentKeyIndex] - i) > maxClearRange && (currentKeyIndex == keys.Count - 1 ? true : (Math.Abs(keys[currentKeyIndex + 1] - i) > maxClearRange)))
                    continue;

                if (currentKeyIndex == keys.Count - 1 || Math.Abs(keys[currentKeyIndex] - i) < Math.Abs(keys[currentKeyIndex + 1] - i))
                {
                    colorSwapTex.SetPixel(i, 0, swap[keys[currentKeyIndex]]);
                }
                else
                {
                    currentKeyIndex += 1;
                    colorSwapTex.SetPixel(i, 0, swap[keys[currentKeyIndex]]);
                }
            }
        }


        colorSwapTex.Apply();
        colorSwapMaterial = GameObject.Instantiate(State.GameManager.ColorSwapMaterial);
        colorSwapMaterial.SetTexture("_SwapTex", colorSwapTex);

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class SpriteContainer
{

    bool usingSpriteRenderer = false;

    public GameObject GameObject;
    public Animation anim;

    SpriteRenderer spriteRenderer;
    Image image;

    internal bool IsImage => image != null;

    int _sortOrder;

    public int SortOrder
    {
        get
        {
            if (usingSpriteRenderer == false)
                return _sortOrder;
            else
                return spriteRenderer.sortingOrder;
        }
        set
        {            
            if (usingSpriteRenderer == false)
                _sortOrder = value;
            else
            {
                value = value + 20000 - (30 * ((int)GameObject.transform.parent.position.x + (3 * ((int)GameObject.transform.parent.position.y))));
                spriteRenderer.sortingOrder = value;
            }
        }
    }

    public Sprite Sprite
    {
        get
        {
            return usingSpriteRenderer == false ? image.sprite : spriteRenderer.sprite;
        }

        set
        {
            if (usingSpriteRenderer == false)
            {
                image.sprite = value;
                if (value.rect.width != value.rect.height)
                {
                    if (value.rect.width > value.rect.height)
                        GameObject.transform.localScale = new Vector3(1, value.rect.height / value.rect.width, 1);
                    else
                        GameObject.transform.localScale = new Vector3(value.rect.width / value.rect.height, 1, 1);
                }
                else
                    GameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            else
                spriteRenderer.sprite = value;
        }
    }

    public Color Color
    {
        get
        {
            return usingSpriteRenderer == false ? image.color : spriteRenderer.color;
        }

        set
        {
            if (usingSpriteRenderer == false)
            {
                if (image.color != value)
                    image.color = value;
            }
            else
            {
                if (spriteRenderer.color != value)
                    spriteRenderer.color = value;
            }
        }
    }

    public SpriteContainer(GameObject type, Transform folder, string name, float xOffset, float yOffset, ColorSwapPalette palette)
    {
        GameObject = GameObject.Instantiate(type, folder);
        GameObject.name = name;
        anim = GameObject.GetComponent<Animation>();
        spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
        if (type == State.GameManager.SpriteRenderAnimatedPrefab)
        {
            spriteRenderer = GameObject.GetComponentInChildren<SpriteRenderer>();
            GameObject = spriteRenderer.gameObject;
        }
        image = GameObject.GetComponent<Image>();
        if (spriteRenderer != null)
            usingSpriteRenderer = true;
        SetOffSets(xOffset, yOffset);
        if (palette != null)
        {
            if (image == null)
            {
                spriteRenderer.material = palette.colorSwapMaterial;
            }
            else
            {
                image.material = palette.colorSwapMaterial;
            }
        }
    }

    public void UpdatePalette(ColorSwapPalette palette)
    {
        if (palette != null)
        {
            if (usingSpriteRenderer)
            {
                spriteRenderer.material = palette.colorSwapMaterial;
            }
            else
            {
                image.material = palette.colorSwapMaterial;
            }
        }
        else
        {
            if (usingSpriteRenderer)
            {
                spriteRenderer.material = ColorPaletteMap.Default.colorSwapMaterial;
            }
            else
            {
                image.material = ColorPaletteMap.Default.colorSwapMaterial;
            }
        }
    }

    public void SetOffSets(float xOffset, float yOffset)
    {
        if (usingSpriteRenderer == false)
            GameObject.transform.localPosition = new Vector3(xOffset * (160 / image.sprite.rect.width) * (160 / image.sprite.pixelsPerUnit), 30 + yOffset * (160 / image.sprite.rect.height) * (160 / image.sprite.pixelsPerUnit), 0);
        else
            GameObject.transform.localPosition = new Vector3(xOffset / 100, 0.1f + yOffset / 100, 0);
    }


}

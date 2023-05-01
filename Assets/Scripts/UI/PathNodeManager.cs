using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathNodeManager : MonoBehaviour
{
    public GameObject NodePrefab;
    List<GameObject> nodes;
    List<SpriteRenderer> spriteRenders;
    List<Text> texts;
    int nextNum = 0;

    private void Start()
    {
        nodes = new List<GameObject>();
        spriteRenders = new List<SpriteRenderer>();
        texts = new List<Text>();
    }

    public void ClearNodes()
    {
        foreach (GameObject arrow in nodes)
        {
            arrow.SetActive(false);
        }
        nextNum = 0;
    }

    public void PlaceNode(Color color, Vec2i location, string text = "")
    {
        if (nextNum >= nodes.Count)
        {
            nodes.Add(Instantiate(NodePrefab, transform));
            spriteRenders.Add(nodes[nextNum].GetComponent<SpriteRenderer>());
            texts.Add(nodes[nextNum].GetComponentInChildren<Text>());
        }

        nodes[nextNum].SetActive(true);
        nodes[nextNum].transform.position = new Vector2(location.x, location.y);
        spriteRenders[nextNum].color = color;
        texts[nextNum].text = text;
        texts[nextNum].gameObject.SetActive(text != "");
        nextNum++;
    }
}

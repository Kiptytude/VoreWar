using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;

public class SimpleHoverAI : MonoBehaviour
{
    public TextMeshProUGUI InfoText;

    private void Update()
    {
        if (gameObject.activeInHierarchy == false)
            return;

        int wordIndex = TMP_TextUtilities.FindIntersectingWord(InfoText, Input.mousePosition, null);

        if (wordIndex > -1)
        {
            string[] words = new string[5];
            for (int i = 0; i < 5; i++)
            {
                if (wordIndex - 2 + i < 0 || wordIndex - 2 + i >= InfoText.textInfo.wordCount || InfoText.textInfo.wordInfo[wordIndex - 2 + i].characterCount < 1)
                {
                    words[i] = string.Empty;
                    continue;
                }
                words[i] = InfoText.textInfo.wordInfo[wordIndex - 2 + i].GetWord();
            }
            State.GameManager.HoveringTooltip.UpdateInformationAIOnly(words);
        }
        
    }

}

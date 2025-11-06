using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIUnitSprite : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Text Name;

    CompleteSprite CompleteSprite;

    [HideInInspector]
    int index = -1;

    public void SetIndex(int num)
    {
        index = num;
    }

    public void ResetBellyScale(Actor_Unit actor)
    {
        if (CompleteSprite != null)
        {
            CompleteSprite.ResetBellyScale();
        }
    }

    public void UpdateSprites(Actor_Unit actor, bool locked = true)
    {

        if (CompleteSprite == null)
            CompleteSprite = new CompleteSprite(State.GameManager.ImagePrefab, null, transform);

        CompleteSprite.SetActor(actor);
        CompleteSprite.UpdateSprite();
        if (locked)
        {
            if (actor.AnimationController?.frameLists != null)
            {
                for (int i = 0; i < actor.AnimationController.frameLists.Length; i++)
                {
                    actor.AnimationController.frameLists[i].currentFrame = 0;
                }
            }

        }
        CompleteSprite.UpdateSprite();
        //The second one is designed to fix the squishbreasts flag not applying correctly on the first round.  I could do a lot of complicated stuff to fix, or just this
        CompleteSprite.AdjustSpriteScale();

        Name.color = actor.Unit.HasEnoughExpToLevelUp() ? new Color(1, .6f, 0) : new Color(.196f, .196f, .196f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (index >= 0)
        {
            if (State.GameManager.CurrentScene == State.GameManager.Recruit_Mode)
                State.GameManager.Recruit_Mode.Select(index);
            else
                State.GameManager.StrategyMode.ExchangerUI.Select(index);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (index >= 0)
        {
            if (State.GameManager.CurrentScene != State.GameManager.Recruit_Mode)
                State.GameManager.StrategyMode.ExchangerUI.UpdateInfo(index);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (index >= 0)
        {
            if (State.GameManager.CurrentScene != State.GameManager.Recruit_Mode)
                State.GameManager.StrategyMode.ExchangerUI.UpdateInfo(-20);
        }
    }
}


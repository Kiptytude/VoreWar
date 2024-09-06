using UnityEngine;
using UnityEngine.UI;

public class ArmySectionsPanel : MonoBehaviour
{
    public UnitInfoPanel InfoPanel;
    public Button Dismiss;
    public Button Rename;
    public Button AutoLevelUp;
    public Button LevelUp;
    public Button Customizer;
    public Button ConfigAutoLevelUp;
    public Text RecruitSoldier;

    public Button Shop;
    public Text ShopText;
    public Text AlliedArmyText;

    public Text ArmyName;

    public GameObject Selector;

    public RectTransform UnitInfoAreaSize;
    public UIUnitSprite[] UnitInfoArea;

    public Transform UnitFolder;

    public void OpenRenameArmy()
    {
        var box = Instantiate(State.GameManager.InputBoxPrefab).GetComponentInChildren<InputBox>();
        box.SetData(State.GameManager.Recruit_Mode.RenameArmy, "Rename", "Cancel", "Rename this army?", 100);

    }

}

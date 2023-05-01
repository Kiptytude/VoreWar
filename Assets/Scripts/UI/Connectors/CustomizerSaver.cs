using UnityEngine;
using UnityEngine.UI;

public class CustomizerSaver : MonoBehaviour
{
    public InputField Text;
    bool OpenedFromEditor;

    public void Open(bool openedFromEditor)
    {
        OpenedFromEditor = openedFromEditor;
        if (OpenedFromEditor)
            Text.text = State.GameManager.UnitEditor.UnitEditor.Unit.Name;
        else
            Text.text = State.GameManager.Recruit_Mode.Customizer.Unit.Name;
        gameObject.SetActive(true);
    }

    public void Save()
    {
        CustomizerData custom = new CustomizerData();
        if (OpenedFromEditor)
            custom.CopyFromUnit(State.GameManager.UnitEditor.UnitEditor.Unit);
        else
            custom.CopyFromUnit(State.GameManager.Recruit_Mode.Customizer.Unit);
        custom.Name = Text.text;
        CustomizationDataStorer.Add(custom);
    }


}

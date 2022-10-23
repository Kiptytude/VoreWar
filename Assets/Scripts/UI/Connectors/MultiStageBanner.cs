using System.Collections;
using System.Linq;
using UnityEngine;

public class MultiStageBanner : MonoBehaviour
{
    public SpriteRenderer LeaderLayer;
    public SpriteRenderer FullBannerLayer;
    public SpriteRenderer InsigniaLayer;
    public SpriteRenderer SelectedBackLayer;
    public SpriteRenderer SelectedColorLayer;
    ParticleSystem system;

    public void Refresh(Army army, bool selected)
    {
        transform.localScale = new Vector3(Config.BannerScale, Config.BannerScale, 1);
        Empire empire = army.Empire;
        LeaderLayer.enabled = army.LeaderIfInArmy() != null;
        if (empire.UnitySecondaryColor != default)
            FullBannerLayer.color = empire.UnitySecondaryColor;
        else
            FullBannerLayer.color = Color.white;
        InsigniaLayer.color = empire.UnityColor;
        if (army.BannerStyle == 0)
        {
            if (25 + (int)empire.Race < State.GameManager.StrategyMode.Banners.Length)
                InsigniaLayer.sprite = State.GameManager.StrategyMode.Banners[25 + (int)empire.Race];
            else
                InsigniaLayer.sprite = State.GameManager.StrategyMode.Banners[3];
        }
        else
            InsigniaLayer.sprite = State.GameManager.StrategyMode.Banners[2 + army.BannerStyle];

        SelectedBackLayer.enabled = selected;
        SelectedColorLayer.enabled = selected;
        if (selected)
            SelectedColorLayer.color = empire.UnityColor;

        if (empire.StrategicAI == null && State.World.ActingEmpire == empire && army.Units.Where(s => s.HasEnoughExpToLevelUp()).Any())
        {
            if (system == null)
            {
                system = Instantiate(State.GameManager.ParticleSystem, gameObject.transform).GetComponent<ParticleSystem>();
                var main = system.main;
                main.startColor = new ParticleSystem.MinMaxGradient(empire.UnityColor);
            }
        }
        else
        {
            if (system != null)
                system.gameObject.SetActive(false);
        }
    }
}

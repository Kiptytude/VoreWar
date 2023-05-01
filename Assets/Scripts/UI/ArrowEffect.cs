using System;
using UnityEngine;

public class ArrowEffect : MonoBehaviour
{

    public GameObject Arrow;

    internal Vector2 StartLocation;
    internal Vector2 EndLocation;
    internal float totalTime;
    internal float currentTime;

    internal float extraTime;

    Action PlayHitSound;
    Action CreateHitEffect;

    public void Setup(Vec2i startLocation, Vec2i endLocation, Actor_Unit target)
    {
        GeneralSetup(startLocation, endLocation);

        PlayHitSound = () => State.GameManager.SoundManager.PlayArrowHit(target);
        CreateHitEffect = () => State.GameManager.TacticalMode.CreateBloodHitEffect(EndLocation);
    }

    public void Setup(Vec2i startLocation, Vec2i endLocation, Actor_Unit target, Action hitSound, Action hitEffect)
    {
        GeneralSetup(startLocation, endLocation);

        PlayHitSound = hitSound;
        CreateHitEffect = hitEffect;

    }

    private void GeneralSetup(Vec2i startLocation, Vec2i endLocation)
    {
        StartLocation = startLocation;
        EndLocation = endLocation;
        Arrow.transform.position = StartLocation;

        currentTime = 0;
        totalTime = startLocation.GetNumberOfMovesDistance(endLocation) * 0.05f;

        float angle = 90 + (float)(Math.Atan2(startLocation.y - endLocation.y, startLocation.x - endLocation.x) * 180 / Math.PI);
        Arrow.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }



    private void Update()
    {
        if (State.GameManager.TacticalMode.PausedText.activeSelf)
            return;
        if (State.GameManager.CurrentScene != State.GameManager.TacticalMode)
        {
            Destroy(gameObject);
            return;
        }
        currentTime += Time.deltaTime;
        Arrow.transform.position = Vector2.Lerp(StartLocation, EndLocation, currentTime / totalTime);
        if (currentTime > totalTime)
        {
            PlayHitSound?.Invoke();
            CreateHitEffect?.Invoke();
        }
        if (currentTime > totalTime + extraTime)
        {
            Destroy(gameObject);
        }
    }

}


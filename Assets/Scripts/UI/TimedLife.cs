using UnityEngine;

public class TimedLife : MonoBehaviour
{
    public float Life;
    float currentLife;
    private void Update()
    {
        currentLife += Time.deltaTime;
        if (currentLife > Life)
            Destroy(gameObject);
    }
}


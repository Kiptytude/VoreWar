using UnityEngine;

public class HandDisplay : MonoBehaviour
{
    Vector3 velocity = new Vector3(0.0f, 0.02f, 0.0f);
    private SpriteRenderer spriteR;
    double alpha = 1;
    // Use this for initialization
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.fixedDeltaTime;
        spriteR.color = new Color(1f, 0.7688679f, 0.9320133f, (float)alpha);
        alpha -= 0.03;
        if (alpha < 0) Destroy(gameObject);
    }
}

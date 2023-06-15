using UnityEngine;

namespace Assets.Scripts.Entities.Animations
{
    class AnimationBase : MonoBehaviour
    {
        protected SpriteRenderer SpriteRenderer;
        protected Frame[] frames;
        protected int currentFrame = 0;
        protected float currentTime = 0;

        public struct Frame
        {
            public Sprite image;
            public Vector3 shift;
            public float time;

            public Frame(Sprite image, Vector3 shift, float time)
            {
                this.image = image;
                this.shift = shift;
                this.time = time;
            }
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime > frames[currentFrame].time)
            {
                currentFrame += 1;
                if (currentFrame >= frames.Length)
                {
                    Destroy(gameObject);
                    return;
                }
                currentTime = 0;
                SpriteRenderer.sprite = frames[currentFrame].image;
            }
            if (currentFrame > 0)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(frames[currentFrame - 1].shift.y, frames[currentFrame].shift.y, currentTime / frames[currentFrame].time), transform.localPosition.z);
            }
        }
    }
}

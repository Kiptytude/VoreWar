using System.Linq;

public class AnimationController
{
    public FrameList[] frameLists;

    public struct FrameList
    {
        public int currentFrame;
        public float currentTime;
        public bool currentlyActive;

        public FrameList(int frame, float time, bool active)
        {
            currentFrame = frame;
            currentTime = time;
            currentlyActive = active;
        }
    }

    public void UpdateTimes(float time)
    {
        if (frameLists == null) return;
        for (int i = 0; i < frameLists.Count(); i++)
        {
            if (frameLists[i].currentlyActive) frameLists[i].currentTime += time;
        }
    }
}


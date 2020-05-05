using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public string[] clipNames;
    private Animation animation;
    private float curTime;
    private System.Action callback;

    public bool IsPlaying { get; private set; }

    public void Init()
    {
        animation = GetComponent<Animation>();
        IsPlaying = false;
    }

    private void Update()
    {
        if (IsPlaying)
        {
            curTime -= Time.deltaTime;
            if (curTime < 0f)
            {
                IsPlaying = false;
                if (callback != null)
                {
                    callback();
                    callback = null;
                }
            }
        }
    }

    public void Play(int clipIndex, System.Action callback = null, bool forcePlay = false)
    {
        if (clipNames == null || clipIndex >= clipNames.Length)
        {
            return;
        }
        if (!IsPlaying)
        {
            animation.Play(clipNames[clipIndex]);
            this.callback = callback;
            IsPlaying = true;
            curTime = animation[clipNames[clipIndex]].length;
        }
        else if (forcePlay)
        {
            animation.Play(clipNames[clipIndex]);
            if (this.callback != null)
            {
                this.callback();
            }
            this.callback = callback;
            IsPlaying = true;
            curTime = animation[clipNames[clipIndex]].length;
        }
    }

    public void SetFrame(int clipIndex, float rate)
    {
        if (clipNames == null || clipIndex >= clipNames.Length)
        {
            return;
        }
        animation.Play(clipNames[clipIndex]);
        animation[clipNames[clipIndex]].normalizedTime = rate;
    }

    public void Stop()
    {
        animation.Stop();
        callback = null;
    }
}

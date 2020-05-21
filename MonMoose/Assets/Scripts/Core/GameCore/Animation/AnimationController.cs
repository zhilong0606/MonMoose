using UnityEngine;

namespace MonMoose.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AnimationController : MonoBehaviour
    {
        private List<string> clipNameList = new List<string>();
        private Animation anim;
        private float curTime;
        private Action callback;
        public string curAnimName;

        public bool IsPlaying { get; private set; }

        public void Init()
        {
            anim = GetComponent<Animation>();
            foreach (AnimationState s in anim)
            {
                clipNameList.Add(s.name);
            }
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
                        Action temp = callback;
                        callback = null;
                        temp();
                    }
                }
            }
        }
        public void Play(int clipIndex, Action callback = null, bool forcePlay = false)
        {
            Play(clipIndex, -1f, callback, forcePlay);
        }

        public void Play(int clipIndex, float time, Action callback = null, bool forcePlay = false)
        {
            if (clipIndex >= clipNameList.Count)
            {
                return;
            }
            if (!IsPlaying)
            {
                PlayInternal(clipIndex, time);
                this.callback = callback;
            }
            else if (forcePlay)
            {
                PlayInternal(clipIndex, time);
                if (this.callback != null)
                {
                    Action temp = this.callback;
                    this.callback = null;
                    temp();
                }
                this.callback = callback;
            }
        }

        public void SetFrame(int clipIndex, float rate)
        {
            if (clipIndex >= clipNameList.Count)
            {
                return;
            }
            anim.Play(clipNameList[clipIndex]);
            anim[clipNameList[clipIndex]].normalizedTime = rate;
        }

        public void Stop()
        {
            anim.Stop();
            callback = null;
        }

        private void PlayInternal(int clipIndex, float time)
        {
            if (isActiveAndEnabled)
            {
                string clipName = clipNameList[clipIndex];
                AnimationState state = anim[clipName];
                state.speed = time <= 0f ? 1f : state.length / time;
                curTime = state.length / state.speed;
                anim.Play(clipName);
                curAnimName = clipName;
                IsPlaying = true;
            }
            else
            {
                SetFrame(clipIndex, 1f);
            }
        }
    }

}

namespace MonMoose.Logic
{
    public class AnimationComponent : EntityComponent
    {
        //private Animation animation;

        //private List<AnimationPlayContext> contextList = new List<AnimationPlayContext>();
        //private List<AnimationPlayContext> playingList = new List<AnimationPlayContext>();
        //private AnimationPlayContext topContext;

        //public void Init(Animation animation, int[] animIds)
        //{
        //    this.animation = animation;
        //    for (int i = 0; i < animIds.Length; ++i)
        //    {
        //        AnimatorClipInfo info = GameDataManager.instance.animationInfoDic[animIds[i]];
        //        AnimationPlayContext context = new AnimationPlayContext();
        //        context.info = info;
        //        context.isLoop = animation[info.animName].wrapMode == WrapMode.Loop;
        //        context.length = animation[info.animName].length;
        //        contextList.Add(context);
        //    }
        //}

        //public void Play(string animName, float length = float.MaxValue)
        //{
        //    for (int i = 0; i < contextList.Count; ++i)
        //    {
        //        if (contextList[i].info.animName == animName)
        //        {
        //            Play(i, length);
        //            break;
        //        }
        //    }
        //}

        //public void Play(int index, float length = float.MaxValue)
        //{
        //    AnimationPlayContext context = contextList[index];
        //    if (!playingList.Contains(context))
        //    {
        //        playingList.Add(context);
        //    }
        //    context.speed = context.length / length;
        //    context.totalTime = length;
        //    context.curTime = 0f;
        //    if (topContext == null || topContext == context || context.info.priority >= topContext.info.priority)
        //    {
        //        PlayImmediately(context);
        //    }
        //}

        //public void Stop(string animName)
        //{
        //    AnimationPlayContext context = null;
        //    for (int i = 0; i < contextList.Count; ++i)
        //    {
        //        if (contextList[i].info.animName == animName)
        //        {
        //            Stop(i);
        //            break;
        //        }
        //    }
        //}

        //public void Stop(int index)
        //{
        //    AnimationPlayContext context = contextList[index];
        //    playingList.Remove(context);
        //    if (context == topContext)
        //    {
        //        topContext = null;
        //        Rerank();
        //    }
        //}

        //public void UnityUpdate()
        //{

        //}

        //public void FrameUpdate()
        //{
        //    bool needRerank = false;
        //    for (int i = playingList.Count - 1; i >= 0; --i)
        //    {
        //        AnimationPlayContext context = playingList[i];
        //        if (context.length < 0)
        //        {
        //            continue;
        //        }
        //        context.curTime += FrameSyncDefine.TimeInterval;
        //        if (context.curTime >= context.totalTime)
        //        {
        //            if (context == topContext)
        //            {
        //                needRerank = true;
        //            }
        //            playingList.RemoveAt(i);
        //        }
        //    }
        //    if (needRerank)
        //    {
        //        Rerank();
        //    }
        //}

        //public void Rerank()
        //{
        //    AnimationPlayContext topContext = null;
        //    int topPriority = -1;
        //    float minTime = 0f;
        //    for (int i = 0; i < playingList.Count; ++i)
        //    {
        //        AnimationPlayContext context = playingList[i];
        //        if (context.info.priority == topPriority)
        //        {
        //            if (context.curTime < minTime)
        //            {
        //                minTime = context.curTime;
        //                topContext = context;
        //            }
        //        }
        //        else if (context.info.priority > topPriority)
        //        {
        //            topPriority = context.info.priority;
        //            minTime = context.curTime;
        //            topContext = context;
        //        }
        //    }
        //    PlayImmediately(topContext);
        //}

        //private void PlayImmediately(AnimationPlayContext context)
        //{
        //    if (context == topContext)
        //    {
        //        animation.Stop(topContext.info.animName);
        //    }
        //    topContext = context;
        //    if (topContext == null)
        //    {
        //        animation.Stop();
        //        return;
        //    }
        //    if (!topContext.isLoop)
        //    {
        //        animation[topContext.info.animName].speed = topContext.speed;
        //    }
        //    animation.Play(topContext.info.animName);
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using MonMoose.Core;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGraphicDownUpScaleEffect : UIGraphicDownUpEffect
{
    public float scale = 0.9f;
    public float time = 0.1f;

    private Vector3 m_initScale;
    private float m_curTime;
    private EState m_state;

    protected override void Awake()
    {
        base.Awake();
        m_initScale = transform.localScale;
        m_state = EState.Stay;
    }

    protected override void OnDisable()
    {
        m_state = EState.Stay;
        transform.localScale = m_initScale;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        m_state = EState.StartPress;
        m_curTime = 0f;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        m_state = EState.EndPress;
        m_curTime = 0f;
    }

    protected virtual void Update()
    {
        switch (m_state)
        {
            case EState.StartPress:
                {
                    m_curTime += Time.unscaledDeltaTime;
                    float f = m_curTime / time;
                    if (f < 1f)
                    {
                        transform.localScale = m_initScale * Mathf.Lerp(1f, scale, f);
                    }
                    else
                    {
                        transform.localScale = m_initScale * scale;
                        m_state = EState.Press;
                    }
                }
                break;
            case EState.EndPress:
                {
                    m_curTime += Time.unscaledDeltaTime;
                    float f = m_curTime / time;
                    if (f < 1f)
                    {
                        transform.localScale = m_initScale * Mathf.Lerp(scale, 1f, f);
                    }
                    else
                    {
                        transform.localScale = m_initScale;
                        m_state = EState.Stay;
                    }
                }
                break;
        }
    }

    private enum EState
    {
        Stay,
        StartPress,
        Press,
        EndPress,
    }
}

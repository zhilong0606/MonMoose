using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.GameLogic.Battle
{
    public abstract class BattleView : MonoBehaviour
    {
        protected abstract GameObject rotateRoot { get; }

        public void Init()
        {
            OnInit();
        }

        public void UnInit()
        {
            OnUnInit();
        }

        public void Tick(float deltaTime)
        {
            OnTick(deltaTime);
        }

        protected virtual void OnInit()
        {


        }

        protected virtual void OnUnInit()
        {

        }

        protected virtual void OnTick(float deltaTime)
        {

        }
    }
}

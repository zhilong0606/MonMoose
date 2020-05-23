using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonMoose.Core
{
    public class UICameraHolder : MonoBehaviour
    {
        public virtual bool needAutoSetCamera
        {
            get { return false; }
        }

        public new virtual Camera camera { get; set; }
    }
}

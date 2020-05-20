using UnityEngine;

namespace MonMoose.Core
{
    public static class UnityExtension
    {
        public static void SetActiveSafely(this GameObject go, bool isActive)
        {
            if (go != null && go.activeSelf != isActive)
            {
                go.SetActive(isActive);
            }
        }

        public static GameObject FindChild(this GameObject go, string path)
        {
            if (go != null)
            {
                Transform trans = go.transform.Find(path);
                if (trans != null)
                {
                    return trans.gameObject;
                }
                Debug.LogError("Error: Cannot Find Child by Path!");
            }
            else
            {
                Debug.LogError("Error: Left Value is Null!");
            }
            return null;
        }

        public static bool IsParentOf(this GameObject go, GameObject child)
        {
            if (go == null)
            {
                Debug.LogError("Error: Left Value is Null!");
                return false;
            }
            if (child == null)
            {
                Debug.LogError("Error: Right Value is Null!");
                return false;
            }
            return child.transform.parent == go.transform;
        }

        public static void SetParent(this GameObject go, GameObject parent)
        {
            if (go == null)
            {
                Debug.LogError("Error: Left Value is Null!");
                return;
            }
            if (parent == null)
            {
                Debug.LogError("Error: Right Value is Null!");
                return;
            }
            if (go.transform.parent != parent.transform)
            {
                go.transform.SetParent(parent.transform);
            }
        }

        public static Transform SetPositionX(this Transform transform, float x)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(x, pos.y, pos.z);
            return transform;
        }

        public static Transform SetPositionY(this Transform transform, float y)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, y, pos.z);
            return transform;
        }

        public static Transform SetPositionZ(this Transform transform, float z)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(pos.x, pos.y, z);
            return transform;
        }

        public static Transform SetLocalPositionX(this Transform transform, float x)
        {
            Vector3 pos = transform.localPosition;
            transform.localPosition = new Vector3(x, pos.y, pos.z);
            return transform;
        }

        public static Transform SetLocalPositionY(this Transform transform, float y)
        {
            Vector3 pos = transform.localPosition;
            transform.localPosition = new Vector3(pos.x, y, pos.z);
            return transform;
        }

        public static Transform SetLocalPositionZ(this Transform transform, float z)
        {
            Vector3 pos = transform.localPosition;
            transform.localPosition = new Vector3(pos.x, pos.y, z);
            return transform;
        }

        public static Vector2 CenterPosition(this Transform transform)
        {
            RectTransform rectTransform = transform as RectTransform;
            float posX = rectTransform.position.x - rectTransform.rect.width * rectTransform.lossyScale.x * (rectTransform.pivot.x - 0.5f);
            float posY = rectTransform.position.y - rectTransform.rect.height * rectTransform.lossyScale.y * (rectTransform.pivot.y - 0.5f);
            return new Vector2(posX, posY);
        }

        public static Quaternion LookRotation(this Vector3 forward)
        {
            if (forward.sqrMagnitude < float.Epsilon)
            {
                forward = Vector3.forward;
            }
            return Quaternion.LookRotation(forward);
        }
    }
}

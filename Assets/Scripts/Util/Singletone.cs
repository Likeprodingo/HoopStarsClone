using UnityEngine;

namespace Util
{
    public abstract class Singleton<T> : MonoBehaviour
        where T : Component
    {
        static T s_instance = null;
        
        public static T GetInstance()
        {
            if (ReferenceEquals(s_instance,null))
            {
                s_instance = FindObjectOfType<T>();
            }
            return s_instance;
        } 

        public virtual void Init()
        {
           
        }
        
        public virtual void DeInit()
        {
        }
    }
}
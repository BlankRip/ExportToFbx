using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolimorfLearning
{
    public class AblityBase : MonoBehaviour, IAbility
    {
        
        public virtual void UseAbility() {
            Debug.Log("This is from base Class");
        }
    }

    public interface IAbility
    {
        void UseAbility();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolimorfLearning
{
    public class DashAbility : MonoBehaviour, IAbility
    {
        Transform playerMovemntSecite;
        

        private void Start() {
            playerMovemntSecite = transform.parent;
        }

        public virtual void UseAbility()
        {
            Debug.Log("<color=red>This is from Dash Ability</color>");
        }
    }
}
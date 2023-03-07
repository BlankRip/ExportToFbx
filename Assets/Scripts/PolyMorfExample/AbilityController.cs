using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolimorfLearning
{
    public class AbilityController : MonoBehaviour
    {
        [SerializeField]
        bool enoughKillesToActive = false;

        [SerializeField]
        [Tooltip("This object should have a script that is using the IAbility Interface")]
        GameObject abilityObj;
        IAbility myAbility;
        //AblityBase myAbility;

        private void Start() {
            myAbility = abilityObj.GetComponent<IAbility>();
        }

        public void SwapAbility(IAbility newAbility) {
            myAbility = newAbility;
        }

        private void Update() {
            if (enoughKillesToActive && Input.GetKeyDown(KeyCode.Q))
            {
                myAbility.UseAbility();
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolimorfLearning
{
    public class ShockAbilitliy : DashAbility
    {
        [SerializeField]
        ParticleSystem shockParticas;

        public override void UseAbility()
        {
            base.UseAbility();
            Debug.Log("<color=blue>This is from Shock Ability</color>");
        }
    }
}
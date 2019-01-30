using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomEffects
{
    public class Celebrate : MonoBehaviour {

        public void Confetti()
        {
            //GetComponent<ParticleSystem>().enableEmission = true;
            ParticleSystem.EmissionModule emissionModule = GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = true;
        }
    }
}
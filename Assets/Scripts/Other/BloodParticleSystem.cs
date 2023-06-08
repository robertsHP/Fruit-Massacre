using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticleSystem : MonoBehaviour {
    [SerializeField] private ParticleSystem textureParticle;
    [SerializeField] private ParticleSystem standardParticle;

    void Update() {
        if(textureParticle == null && standardParticle == null) {
            Destroy(gameObject);
        }
    }
}

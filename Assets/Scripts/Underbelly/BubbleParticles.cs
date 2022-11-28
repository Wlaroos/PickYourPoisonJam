using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleParticles : MonoBehaviour
{
    Reactor _reactorRef;

    ParticleSystem ps;

    List<ParticleSystem.Particle> _particles = new List<ParticleSystem.Particle>();

    private void Awake()
    {
        ps = transform.GetComponent<ParticleSystem>();

        if (FindObjectOfType<Reactor>() != null)
        {
            _reactorRef = FindObjectOfType<Reactor>();
            ps.trigger.SetCollider(0, _reactorRef.GetComponent<CircleCollider2D>());
        }
    }

    // When particle enters the trigger of the reactor
    private void OnParticleTrigger()
    {
        // Gets how many particles that were touched
        int triggeredParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, _particles);

        if (ps.particleCount <= 0)
        {
            AudioManager.PlaySound("Pop");
            Destroy(gameObject);
        }

        // For each particle that the player touched
        for (int i = 0; i < triggeredParticles; i++)
        {
            // Local reference for a particle in the list
            ParticleSystem.Particle p = _particles[i];
            // Changes lifetime of the particle
            p.remainingLifetime = 0;
            // Add amount to reactor count

            _reactorRef.Increase();

            // Applies local referece changes to the particle in the list
            _particles[i] = p;

            //SOUND
        }

        // Apply changes to the particle system using the new values from the list
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, _particles);

    }

}

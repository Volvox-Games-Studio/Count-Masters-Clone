using Emre;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;
    [SerializeField] private ColorContainer colors;
    
    
    public void PlayDestroyParticle()
    {
        foreach (var particle in particles)
        {
            var main = particle.main;
            main.startColor = colors[ColorWheelButton.SkinColorIndex];
        }

        particles[0].transform.parent = null;
        particles[0].Play();
        Destroy(particles[0].gameObject, 2f);
    }
}

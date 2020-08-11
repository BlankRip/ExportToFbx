using System.Collections.Generic;
using UnityEngine;

public class SPH_Manager : MonoBehaviour
{

#region Variables
    [Header("Drop or single particle data")]
    [SerializeField] GameObject dropPrefab;
    [SerializeField] float radius;
    [SerializeField] float mass;
    [SerializeField] float resetDensity;
    [SerializeField] float viscosity;
    [SerializeField] float drag;

    [Header("Simulation data")]
    [SerializeField] bool wallsUp;
    [SerializeField] int amount;
    [SerializeField] int perRow;
    [SerializeField] List<GameObject> walls;

    [Header("Physic's Constands of the game")]
    [SerializeField] float smoothiningRadius = 1;
    [SerializeField] float gravityY = -9.81f;
    [SerializeField] float gravityMultiplicator = 2000;
    [SerializeField] float gas = 2000;
    [SerializeField] float damping = -0.5f;
    private float deltaTime = 0;
    private Vector3 gravity;

    private SPH_Particle[] particles;
    private SPH_ParticleCollider[] particleColliders;
    private bool clearing;
#endregion

    private void Awake() {
        gravity = new Vector3(0, gravityY, 0) * gravityMultiplicator;
    }

    private void Start() {
        InitilizeSimulation();
        CalculateForce();
    }

    private void InitilizeSimulation() {
        particles = new SPH_Particle[amount];

        for (int i = 0; i < particles.Length; i++) {
            float x = (i % perRow) + Random.Range(-0.1f, 0.1f);
            float y = (2 * radius) + ((i /perRow) / perRow) * 1.1f;
            float z = ((i / perRow) % perRow) + Random.Range(-0.1f, 0.1f);

            GameObject currentGameObject = Instantiate(dropPrefab);
            SPH_Particle currentParticle = currentGameObject.AddComponent<SPH_Particle>();
            particles[i] = currentParticle;

            currentGameObject.transform.localScale = Vector3.one * radius;
            currentGameObject.transform.position = new Vector3(x, y, z);

            currentParticle.particleObj = currentGameObject;
            currentParticle.position = currentGameObject.transform.position;
        }
    }

#region Kinematics (Movement)
    private void CalculateMovment() {
        for (int i = 0; i < particles.Length; i++) {
            if(clearing)
                return;
            
            Vector3 pressureForce = Vector3.zero;
            Vector3 viscosityForce = Vector3.zero;

            for (int j = 0; j < particles.Length; j++) {
                if(i == j)
                    continue;
                
                Vector3 direction = particles[j].position = particles[i].position;
                float distance = direction.magnitude;

                pressureForce += CalculatePressure(particles[i], particles[j], direction, distance);
                viscosityForce += CalculateViscosity(particles[i], particles[j], distance);
            }

            Vector3 gravitationalForce = gravity * particles[i].density;

            particles[i].combinedForce = pressureForce + viscosityForce + gravitationalForce;
            particles[i].velocity += (particles[i].combinedForce / particles[i].density) * deltaTime;
            particles[i].position += particles[i].velocity * deltaTime;
            particles[i].particleObj.transform.position = particles[i].position;
        }
    }
    private float CalculateDensity (SPH_Particle currentParticle, float distance) {
        if(distance < smoothiningRadius)
            currentParticle.density += mass * (315 / (64 * Mathf.PI * Mathf.Pow(smoothiningRadius, 9))) * Mathf.Pow(smoothiningRadius -distance, 3);
        
        return currentParticle.density;
    }

    #region Force Calculation Functions
    private void CalculateForce() {
        for (int i = 0; i < particles.Length; i++) {
            if(clearing)
                return;
            
            for (int j = 0; j < particles.Length; j++) {
                float distance = (particles[j].position - particles[i].position).magnitude;
                particles[i].density = CalculateDensity(particles[i], distance);
                particles[i].pressure = gas * (particles[i].density - resetDensity);
            }
        }
    }
    
    private Vector3 CalculatePressure(SPH_Particle currentParticle, SPH_Particle otherParticle, Vector3 direction, float distance) {
        if(distance < smoothiningRadius) {
            Vector3 pressure = -1 * (direction.normalized) * mass * (currentParticle.pressure + otherParticle.pressure)/ (2 * otherParticle.density) * 
            (-45 / (Mathf.PI * Mathf.Pow(smoothiningRadius, 6))) * Mathf.Pow(smoothiningRadius - distance, 2);

            return pressure;
        }
        
        return Vector3.zero;
    }

    private Vector3 CalculateViscosity(SPH_Particle currentParticle, SPH_Particle otherParticle, float distance) {
        if(distance < smoothiningRadius) {
            Vector3 viscosity = this.viscosity * mass * (otherParticle.velocity - currentParticle.velocity) / otherParticle.density *
                (45 / (Mathf.PI *  Mathf.Pow(smoothiningRadius, 6))) * (smoothiningRadius - distance);
            
            return viscosity;
        }

        return Vector3.zero;
    }
    #endregion
#endregion

#region Collision
    private void CalculateCollisions() {
        for (int i = 0; i < particles.Length; i++) {
            for (int j = 0; j < particleColliders.Length; j++) {
                if(clearing || particleColliders.Length == 0)
                    return;

                Vector3 penetrationNormal;
                Vector3 penetrationPosition;
                float penetrationLength;
                if(Collision(particleColliders[j], particleColliders[i].position, radius, out penetrationNormal,
                    out penetrationPosition, out penetrationLength)) {
                    particles[i].velocity = DampenVelocity(particleColliders[j], particles[i].velocity, penetrationNormal, 1 - drag);
                    particles[i].position = penetrationPosition - (penetrationNormal * Mathf.Abs(penetrationLength));
                }
            }
        }
    }

    private bool Collision(SPH_ParticleCollider collider, Vector3 position, float radius, out Vector3 penetrationNormal,
        out Vector3 penetrationPosition, out float penetrationLength) {
        Vector3 colliderProjection = collider.position - position;

        penetrationNormal = Vector3.Cross(collider.right, collider.up);
        penetrationLength = Mathf.Abs(Vector3.Dot(colliderProjection, penetrationNormal)) - (radius / 2);
        penetrationPosition = collider.position - colliderProjection;

        return penetrationLength < 0 
            && Mathf.Abs(Vector3.Dot(colliderProjection, collider.right)) < collider.scale.x
            && Mathf.Abs(Vector3.Dot(colliderProjection, collider.up)) < collider.scale.y;
    }

    private Vector3 DampenVelocity(SPH_ParticleCollider collider, Vector3 velocity, Vector3 penetrationNormal, float drag) {
        Vector3 newVelocity = Vector3.Dot(velocity, penetrationNormal) * penetrationNormal * damping +  
            Vector3.Dot(velocity, collider.right) * collider.right * drag + Vector3.Dot(velocity, collider.up) * collider.up * drag;
        
        return Vector3.Dot(newVelocity, Vector3.forward) * Vector3.forward + Vector3.Dot(newVelocity, Vector3.right) * Vector3.right +
            Vector3.Dot(newVelocity, Vector3.up) * Vector3.up;
    }
#endregion

}

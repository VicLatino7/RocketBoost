using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    //Parameters - for tuning, typically set in the editor
    //Cache - e.g. references for readability or speed
    //State - private instance (member) variables
    [SerializeField] InputAction thrust;
    [SerializeField] float thrustStrength = 650f;
    [SerializeField] InputAction rotation;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngineSFX;

    [SerializeField] ParticleSystem LeftBoosterParticles;
    [SerializeField] ParticleSystem RightBoosterParticles;
    [SerializeField] ParticleSystem MainBoosterParticles;
    Rigidbody rb;
    AudioSource audioSource;

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        processThrust();
        processRotation();
    }

    private void processThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        MainBoosterParticles.Play();
        if (!MainBoosterParticles.isPlaying)
        {
            MainBoosterParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        MainBoosterParticles.Stop();
    }
    private void processRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(rotationStrength);
        if (!RightBoosterParticles.isPlaying)
        {
            LeftBoosterParticles.Stop();
            RightBoosterParticles.Play();
        }        
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!LeftBoosterParticles.isPlaying)
        {
            RightBoosterParticles.Stop();
            LeftBoosterParticles.Play();
        }        
    }

    private void StopRotating()
    {
        LeftBoosterParticles.Stop();
        RightBoosterParticles.Stop();
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

}

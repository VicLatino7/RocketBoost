using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;
    bool isControllable = true;
    bool isCollidable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }
    
    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable) return;

        switch (other.gameObject.tag)
        {
        case "Friendly":
            Debug.Log("This thing is friendly");
            break;
        case "Finish":
            StartSuccessSequence();
            break;
        default:
            StartCrashSequence();
            break;
        }
    }

    void StartSuccessSequence()
    {
        // to do: add particle effect and sfx
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        // to do: add particle effect and sfx
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("Reloadlevel", levelLoadDelay);
    }
    
    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene >= SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    void Reloadlevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}

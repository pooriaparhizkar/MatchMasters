using UnityEngine;

public class audio : MonoBehaviour
{
    // Start is called before the first frame update
    //AudioSource audioSource;
    public AudioSource audioSource;

    private void Start()
    {
        // audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            audioSource.mute = !audioSource.mute;
    }
}
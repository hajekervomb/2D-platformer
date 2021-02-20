using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    [SerializeField] private float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    [SerializeField] private float pitch = 1f;
    [Range(0f, 0.5f)]
    [SerializeField] private float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    [SerializeField] private float randomPitch = 0.1f;

    private AudioSource source;

    public bool loopSound = false;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loopSound;
    }

    public void Play()
    {
        source.volume = volume + Random.Range(-randomVolume / 2f, randomVolume / 2f);
        source.pitch = pitch + + Random.Range(-randomPitch / 2f, randomPitch / 2f);
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

}
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene");
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    [SerializeField]
    private Sound[] sounds;

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            //sound game object will be a child of Game Manager
            _go.transform.SetParent(transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        PlaySound("Music");
    }
       

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }           
            
        }
        //no sound with _name
 
    }

    public void StopSound (string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
            
        }
        //no sound with _name
        Debug.LogWarning("AudioManager STOP: Sound not found in array [Sounds]" + _name);
    }

    public void RandomSoundness(string[] _name)
    {
        //playt random sound
    }
}

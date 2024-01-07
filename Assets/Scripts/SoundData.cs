using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundData : MonoBehaviour
{
    [HideInInspector] public static SoundData soundData { private set; get; }

    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private AudioSource musicaAudio;

    private void Awake()
    {

        if (soundData == null)
        {
            soundData = this;
        }
        else if (soundData != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        masterMixer.SetFloat("MasterVolumen", GameData.gameData.LoadMasterVolumen());

        musicaAudio = GameObject.Find("EndlessMusica").GetComponent<AudioSource>();
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        StartCoroutine(encontrarEndlessMusica());
    }

    public void ChangeMasterVolumen()
    {
        float a = GameData.gameData.LoadMasterVolumen();

        if (a == -60f)
        {
            
            a = -9f;
            StartCoroutine(SmoothVolumeChange(a));
        }
        else if (a == -9)
        {
            a = -15f;
            StartCoroutine(SmoothVolumeChange(a));
        }
        else if (a == -15)
        {
            a = -60f;
            StartCoroutine(SmoothVolumeChange(a));
        }

    }

    IEnumerator SmoothVolumeChange(float a)
    {
        float b;
        
        masterMixer.GetFloat("MasterVolumen", out b);
        while (b != a)
        {
            if(a < b)
            {
                b += a * Time.deltaTime;
                masterMixer.SetFloat("MasterVolumen", b);
                if (b <= a)
                {
                    b = a;
                    masterMixer.SetFloat("MasterVolumen", a);
                    if (a == -60f) musicaAudio.Pause();
                    GameData.gameData.SaveMasterVolumen(a);
                    yield return null;
                }
            }
            else
            {
                b -= a * Time.deltaTime * 10;
                masterMixer.SetFloat("MasterVolumen", b);
                if(!musicaAudio.isPlaying && musicaAudio != null)
                {
                    musicaAudio.UnPause();
                }
                
                if (b >= a)
                {
                    b = a;
                    masterMixer.SetFloat("MasterVolumen", a);
                    GameData.gameData.SaveMasterVolumen(a);
                   
                    yield return null;
                }
            }
            

            yield return null;
        }

    }

    IEnumerator encontrarEndlessMusica()
    {
        float a = 0;
        while(a < 3f)
        {
            musicaAudio = GameObject.Find("EndlessMusica").GetComponent<AudioSource>();
            a += Time.deltaTime;
            yield return null;
        }
    }

}

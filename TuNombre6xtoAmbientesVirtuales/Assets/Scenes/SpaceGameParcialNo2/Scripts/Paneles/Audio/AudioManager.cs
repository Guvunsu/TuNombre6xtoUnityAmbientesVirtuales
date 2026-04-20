using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    [Header("Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    public Slider sliderMusic;
    public Slider sliderSFX;
    public Slider sliderUI;
    public Slider sliderAmbient;
    public Slider sliderVoice;

    [Header("Sources")]
    [SerializeField] private AudioSource[] musicSources;
    [SerializeField] private AudioSource[] sfxSources;
    [SerializeField] private AudioSource uiSource;
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private AudioSource voiceSource;

    [Header("Sources")]
    [SerializeField] List<AudioClip> clipList;
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // si no es nulo y no es ese destruye el objeto (audio)
        // y regresa a preguntar hasta que encuentre el que si,
        // es ese, y pide no destruirlo (objecto/audio)
    }
    public void Start()
    {
        LoadVolumes();
    }

    private void LoadVolumes()
    {
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 1f);
        float ui = PlayerPrefs.GetFloat("UIVolume", 1f);
        float ambient = PlayerPrefs.GetFloat("AmbientVolume", 1f);
        float voice = PlayerPrefs.GetFloat("VoiceVolume", 1f);

        sliderMusic.value = music;
        sliderSFX.value = sfx;
        sliderUI.value = ui;
        sliderAmbient.value = ambient;
        sliderVoice.value = voice;

        SetMusicVolume(music);
        SetSFXVolume(sfx);
        SetUIVolume(ui);
        SetAmbientVolume(ambient);
        SetVoiceVolume(voice);
    }
    public void PlayMusic(int index, bool loop = true)
    {
        if (index < 0 || index >= musicSources.Length) return;

        var source = musicSources[index];
        if (clipList.Count == 0) return;

        source.clip = clipList[0];
        source.loop = loop;
        source.Play();
    }

    public void PlaySFX(int index, AudioClip clip, float volume = 1f)
    {
        if (index < 0 || index >= sfxSources.Length) return;
        if (clip == null) return;

        sfxSources[index].PlayOneShot(clip, volume);
    }
    public void StopMusic()
    {
        foreach (var source in musicSources)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
    }

    public void StopAmbient()
    {
        if(ambientSource != null && ambientSource.isPlaying)
        {
            ambientSource.Stop();
        }
    }
    //public void PlayMusic(bool loop = true) {
    //    //siempre estara en loop la musica 
    //    //si la lista esta verdaderamente en cero, se regresa a poregutnar a ver si es lo contrario, es para evitar un error 
    //    //la cancion de esa variable es igual el indice de la lista de las canciones vinculadas 
    //    //sera siempre en loop verdadero y se reproduce esa cancion 

    //    if (clipList.Count == 0) return;

    //    musicSource.clip = clipList[0];
    //    musicSource.loop = loop;
    //    musicSource.Play();
    //}
    //public void PlaySFX(AudioClip clip, float volume = 1f) {
    //    //si no hay cancion se regresa, el volumen se establecera como un maximo de 1 flotante puede registar una cancion(clip)
    //    //entonces la variable global quie gaurda la informacion de un audio se accede
    //    //a una escala de 0 a 1 especializado al audioclip, se puede colocar su clip y su volumen del sonido

    //    if (clip == null) return;
    //    sfxSource.PlayOneShot(clip, volume);
    //}

    //public void StopMusic()
    //{
    //    //metodo para parar la musica
    //    musicSource.Stop();
    //}

    public void PlayUI(AudioClip clip, float volume = 1f) {
        //lo mismo al anterior pero para el UI
        if (clip == null) return;
        uiSource.PlayOneShot(clip, volume);
    }

    public void PlayAmbient(AudioClip clip, bool loop = true) {
        //si eñ clip es verdaderamente nulo, se regresa a preguntar haasta que de lo contrario
        //el clip del ambient vinculado , sera el clip igual a la variable temproal del metodo
        //ese clip esatra en loop y ese audio se reproducira 
        if (clip == null) return;

        ambientSource.clip = clip;
        ambientSource.loop = loop;
        ambientSource.Play();
    }

    public void PlayVoice(AudioClip clip, bool loop = false) {
        //lo mismo como el anteiror pero para las voces
        if (clip == null) return;

        voiceSource.clip = clip;
        voiceSource.loop = loop;
        voiceSource.Play();
    }

    //para los volumentes y con el setbool, ocupa 2 parametros el nombre del audio y el value en flotante
    //con un metodo especializado para losvolumenes del audioclip 
    public void SetMusicVolume(float normalizedValue) {
        audioMixer.SetFloat("MusicVolume", ConvertToMixerVolume(normalizedValue));
        Debug.Log("si accedo a MusicVolume " +  normalizedValue);
        PlayerPrefs.SetFloat("MusicVolume", normalizedValue);
    }

    public void SetSFXVolume(float normalizedValue) {
        audioMixer.SetFloat("SFXVolume", ConvertToMixerVolume(normalizedValue));
        PlayerPrefs.SetFloat("SFXVolume", normalizedValue);
    }

    public void SetUIVolume(float normalizedValue) {
        audioMixer.SetFloat("UIVolume", ConvertToMixerVolume(normalizedValue));
        PlayerPrefs.SetFloat("UIVolume", normalizedValue);
    }

    public void SetAmbientVolume(float normalizedValue) {
        audioMixer.SetFloat("AmbientVolume", ConvertToMixerVolume(normalizedValue));
        PlayerPrefs.SetFloat("AmbientVolume", normalizedValue);
    }

    public void SetVoiceVolume(float normalizedValue) {
        audioMixer.SetFloat("VoiceVolume", ConvertToMixerVolume(normalizedValue));
        PlayerPrefs.SetFloat("VoiceVolume", normalizedValue);
    }

    //paea manejar varios sonidos y sus volumenes 
    private float ConvertToMixerVolume(float value) {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        return Mathf.Log10(value) * 20f;
    }
}
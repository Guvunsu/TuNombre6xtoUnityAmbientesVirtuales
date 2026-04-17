using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    [Header("Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
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
    }

    public void PlayMusic(bool loop = true) {
        if (clipList.Count == 0) return;

        musicSource.clip = clipList[0];
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic() {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f) {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayUI(AudioClip clip, float volume = 1f) {
        if (clip == null) return;
        uiSource.PlayOneShot(clip, volume);
    }

    public void PlayAmbient(AudioClip clip, bool loop = true) {
        if (clip == null) return;

        ambientSource.clip = clip;
        ambientSource.loop = loop;
        ambientSource.Play();
    }

    public void PlayVoice(AudioClip clip, bool loop = false) {
        if (clip == null) return;

        voiceSource.clip = clip;
        voiceSource.loop = loop;
        voiceSource.Play();
    }

    public void SetMusicVolume(float normalizedValue) {
        audioMixer.SetFloat("MusicVolume", ConvertToMixerVolume(normalizedValue));
    }

    public void SetSFXVolume(float normalizedValue) {
        audioMixer.SetFloat("SFXVolume", ConvertToMixerVolume(normalizedValue));
    }

    public void SetUIVolume(float normalizedValue) {
        audioMixer.SetFloat("UIVolume", ConvertToMixerVolume(normalizedValue));
    }

    public void SetAmbientVolume(float normalizedValue) {
        audioMixer.SetFloat("AmbientVolume", ConvertToMixerVolume(normalizedValue));
    }

    public void SetVoiceVolume(float normalizedValue) {
        audioMixer.SetFloat("VoiceVolume", ConvertToMixerVolume(normalizedValue));
    }

    private float ConvertToMixerVolume(float value) {
        value = Mathf.Clamp(value, 0.0001f, 1f);
        return Mathf.Log10(value) * 20f;
    }
}
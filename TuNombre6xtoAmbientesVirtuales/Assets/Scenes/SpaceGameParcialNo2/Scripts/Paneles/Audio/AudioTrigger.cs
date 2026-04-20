using System.Collections;
using UnityEngine;

public enum ActivationAudioFSM
{
    ACTIVATE,
    DEACTIVATE,
    WAITING
}

public class AudioTrigger : MonoBehaviour
{
    public ActivationAudioFSM fsm = ActivationAudioFSM.WAITING;
    public AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        if (audio != null)
            audio.volume = 0f;
    }

    void Update()
    {
        VerifyState();
    }

    void VerifyState()
    {
        switch (fsm)
        {
            case ActivationAudioFSM.ACTIVATE:
                if (audio != null && !audio.isPlaying)
                {
                    audio.Play();
                    StartCoroutine(Fade(true, audio, 2f, 1f));
                }
                break;

            case ActivationAudioFSM.DEACTIVATE:
                if (audio != null && audio.isPlaying)
                {
                    StartCoroutine(Fade(false, audio, 2f, 0f));
                }
                break;

            case ActivationAudioFSM.WAITING:
                break;
        }
    }

    public IEnumerator Fade(bool fadeIn, AudioSource audio, float duration, float targetVolume)
    {
        float time = 0f;
        float startVol = audio.volume;
        while (time < duration)
        {
            time += Time.deltaTime;
            audio.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
            yield return null;
        }

        if (!fadeIn)
            audio.Stop();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.StopAmbient();
            AudioManager.Instance.StopMusic();
            fsm = ActivationAudioFSM.ACTIVATE;
            Debug.Log($"AudioTrigger: Player entró en {gameObject.name}, activando clip {audio.clip.name}");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fsm = ActivationAudioFSM.DEACTIVATE;
            Debug.Log($"AudioTrigger: Player salió de {gameObject.name}, desactivando clip {audio.clip.name}");
        }
    }
}

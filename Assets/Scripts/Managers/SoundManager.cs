using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    public List<AudioClip> BGM;//������� ����
    public List<AudioClip> SFX;//ȿ���� ����

    [SerializeField] AudioSource bgmPlayer = new AudioSource();//������� �����
    List<AudioSource> sfxPlayer = new List<AudioSource>();//ȿ���� �����

    public AudioMixer audioMixer;
    string MIXER_MASTER = "MASTER";//��� ����
    string MIXER_BGM = "BGM";//��� ����
    string MIXER_SFX = "SFX";//ȿ���� ����

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        LoadVolume();
    }

    public void PlayBGM(string name)//������� ���
    {
        foreach (AudioClip clip in BGM)
        {
            if(clip.name == name)
            {
                bgmPlayer.clip = clip;
                bgmPlayer.Play();
            }
        }
    }

    public void StopBGM()//������� ����
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string name)//ȿ���� ���
    {
        for(int i = 0; i < SFX.Count; i++)
        {
            if (SFX[i].name == name)
            {
                //for(int j = 0; j < sfxPlayer.Count; j++)
                //{
                //    if (!sfxPlayer[j].isPlaying)
                //    {
                //        sfxPlayer[j].clip = SFX[i];
                //        sfxPlayer[j].Play();
                //        StartCoroutine(DestoryAudiosource(sfxPlayer[j]));
                //        return;
                //    }
                //}

                GameObject sfx = new GameObject(name);
                sfx.transform.SetParent(transform);
                AudioSource newSource = sfx.AddComponent<AudioSource>();
                newSource.clip = SFX[i];
                newSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups(MIXER_SFX)[0];
                newSource.loop = false;
                newSource.Play();
                sfxPlayer.Add(newSource);
                StartCoroutine(DestoryAudiosource(newSource));
                return;
            }
        }
    }

    public void StopAllSFX()//��� ȿ���� ����
    {
        foreach(AudioSource s in sfxPlayer)
        {
            s.Stop();
        }
    }
    IEnumerator DestoryAudiosource(AudioSource source)//ȿ���� ��� ������ ������ҽ� ����
    {
        yield return new WaitWhile(() => source.isPlaying);

        sfxPlayer.Remove(source);
        Destroy(source.gameObject);
    }

    public void StopAllSound()//��� ���� ����
    {
        StopBGM();
        StopAllSFX();
    }

    public float GetMasterVolume()
    {
        audioMixer.GetFloat(MIXER_MASTER, out float volume);
        return Mathf.Pow(10, volume / 20);
    }

    public void SetMasterVolume(float volume)//��ü ���� ����(0~1)
    {
        volume = Mathf.Max(volume, 0.0001f);
        audioMixer.SetFloat(MIXER_MASTER, Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("MasterVolume", GetMasterVolume());
    }

    public float GetBGMVolume()
    {
        audioMixer.GetFloat(MIXER_BGM, out float volume);
        return Mathf.Pow(10, volume / 20);
    }

    public void SetBGMVolume(float volume)//������� ���� ����(0~1)
    {
        volume = Mathf.Max(volume, 0.0001f);
        audioMixer.SetFloat(MIXER_BGM, Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("BGMVolume", GetBGMVolume());
    }

    public float GetSFXVolume()
    {
        audioMixer.GetFloat(MIXER_SFX, out float volume);
        return Mathf.Pow(10, volume / 20);
    }

    public void SetSFXVolume(float volume)//ȿ���� ���� ����(0~1)
    {
        volume = Mathf.Max(volume, 0.0001f);
        audioMixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("SFXVolume", GetSFXVolume());
    }

    public void LoadVolume()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 1f));
        SetBGMVolume(PlayerPrefs.GetFloat("BGMVolume", 1f));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 1f));
    }
}

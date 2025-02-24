using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public List<AudioClip> BGM;//배경음악 모음
    public List<AudioClip> SFX;//효과음 모음

    AudioSource bgmPlayer = new AudioSource();//배경음악 재생기
    List<AudioSource> sfxPlayer = new List<AudioSource>();//효과음 재생기

    private void Start()
    {
        bgmPlayer = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void PlayBGM(string name)//배경음악 재생
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

    public void StopBGM()//배경음악 종료
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string name)//효과음 재생
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
                newSource.loop = false;
                newSource.Play();
                sfxPlayer.Add(newSource);
                StartCoroutine(DestoryAudiosource(newSource));
                return;
            }
        }
    }

    public void StopAllSFX()
    {
        foreach(AudioSource s in sfxPlayer)
        {
            s.Stop();
        }
    }

    IEnumerator DestoryAudiosource(AudioSource source)
    {
        yield return new WaitWhile(() => source.isPlaying);

        sfxPlayer.Remove(source);
        Destroy(source.gameObject);
    }

    public void StopAllSound()
    {
        StopBGM();
        StopAllSFX();
    }
}

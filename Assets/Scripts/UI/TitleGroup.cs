using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGroup : MonoBehaviour
{
    SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlaySFX("Title");
    }

    void OnClick()
    {
        SoundManager.Instance.StopAllSound();
        if (!PlayerPrefs.HasKey("FirstPlay"))
        {
            PlayerPrefs.SetInt("FirstPlay", 1);  // ù ���� ����
            PlayerPrefs.Save();  // ���� (�ʼ�)
            SceneManager.LoadScene("Tutorial");  // Ʃ�丮�� ������ �̵�
        }
        else
        {
            SceneManager.LoadScene("Lobby");  // �κ�ȭ�� ������ �̵�
        }
    }
}

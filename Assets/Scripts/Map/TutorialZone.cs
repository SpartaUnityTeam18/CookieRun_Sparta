using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialZone : MonoBehaviour
{
    // Ʃ�丮�� ui ���� > tutorialzone���� �����ؼ� ���
    public GameObject tutorialUI;
    // ���� �� ���� > ���� ������ ����
    public string nextSceneName = "Lobby";
    // ui ��ȣ
    private int number = 0;
    // ui Active ����
    private bool isActive = false;

    void Start()
    {
        SoundManager.Instance.PlayBGM("Bgm_Map_1");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ��Ű�� �浹�߰�  ui�� ���������� isTutorialScene�� true �϶�
        if (collision != null && collision.CompareTag("Cookie") && !isActive && GameManager.Instance.isTutorialScene)
        {
            // UI Ȱ��ȭ
            ShowTutorial();
        }
    }

    void ShowTutorial()
    {
        // ���� ��� UI�� ��Ȱ��ȭ
        foreach (Transform child in tutorialUI.transform)
        {
            child.gameObject.SetActive(false);
        }

        // ���� number�� �ش��ϴ� UI Ȱ��ȭ
        // transform.childCount > gameobject�� �� ������ �ִ� transform�� �ִ� childCount�� ����ؼ� �ڽ� ��ü�� ���� �� �� ����.
        if (number < tutorialUI.transform.childCount)
        {
            // Ÿ�ӽ����� 0�� �ؼ� ����!
            Time.timeScale = 0f;

            // ���� UI �� number�� �ش�Ǵ� �༮ Ȱ��ȭ ����
            tutorialUI.transform.GetChild(number).gameObject.SetActive(true);

            // number�� �÷��༭ �ٽ� �޼��� ȣ�� �� ������ UI�� Ȱ��ȭ �ϰ� ����
            number++;
        }
        // UI Ȱ��ȭ ���±� ������ true
        isActive = true;
    }

    void HideTutorial()
    {
        // ��� Ʃ�丮�� UI ��Ȱ��ȭ
        foreach (Transform child in tutorialUI.transform)
        {
            child.gameObject.SetActive(false);
        }

        //Ÿ�ӽ����� 1�� ��������
        Time.timeScale = 1f;

        // isActive�� false�� ���༭ ������ �浹���� �� UI�� ���� �� �ְ� ����
        isActive = false;
    }

    void LoadNextScene()
    {
        // �ش� ���ڿ� Null���� üũ > �ƴϸ� false ��ȯ > ! �� true
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // ������ ������ ��ȯ, Ÿ�ӽ����� 1�� ��������
            SceneManager.LoadScene(nextSceneName);
            Time.timeScale = 1f;
        }
    }

    void OnEnter()
    {
        // ����Ű �Է½� �Ѿ
        if (isActive)
        {
            // UI�� �������̰ų� �Ѿ����
            if (number >= tutorialUI.transform.childCount)
            {
                // ������ ���� ������ ��ȯ
                SoundManager.Instance.StopAllSound();
                LoadNextScene();
                return;
            }
            else
            {
                // �ƴϸ� UI ������
                HideTutorial();
            }
        }
    }
}

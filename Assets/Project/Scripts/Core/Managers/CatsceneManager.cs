using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Assets.Project.Scripts.Core.Managers
{
    [Serializable]
    public class DialogModel
    {
        public string Line;
        public string LineEn;
        public UnityEvent OnLineComplete = new();
    }

    public class CatsceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject dialoguePanel; // Панель діалогу
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private List<DialogModel> lines; // Масив реплік
        [SerializeField] private float typingSpeed = 0.02f; // Для ефекту "написання"
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource audioSource;

        private int currentLineIndex = 0;
        private bool isTyping = false;
        private string currentLine = "";
        private CatsceneAction _input;

        private void OnEnable()
        {
            dialoguePanel.SetActive(true);

            _input = new CatsceneAction();
            _input.Catscene.Click.performed += OnClick;
            _input.Enable();
            ShowLine();
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (isTyping)
            {
                // Якщо гравець клікнув під час набору тексту — одразу показати весь рядок
                dialogueText.text = currentLine;
                isTyping = false;
                audioSource.Pause();
                StopAllCoroutines();
            }
            else
            {
                // Якщо текст вже показаний — перейти далі
                NextLine();
            }
        }

        private void ShowLine()
        {
            if (currentLineIndex < lines.Count)
            {
                if(PlayerPrefs.GetString("Language") == "en")
                {
                    currentLine = lines[currentLineIndex].LineEn;
                }
                else
                {
                    currentLine = lines[currentLineIndex].Line;
                }
                StopAllCoroutines();
                StartCoroutine(TypeLine());
            }
        }

        private IEnumerator TypeLine()
        {
            audioSource.Play();
            isTyping = true;
            dialogueText.text = "";
            foreach (char c in currentLine.ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
            isTyping = false;
            audioSource.Pause();
        }

        private void NextLine()
        {
            lines[currentLineIndex].OnLineComplete?.Invoke();
            currentLineIndex++;
            if (currentLineIndex < lines.Count)
            {
                ShowLine();
            }
            else
            {
                dialogueText.text = "";
            }
        }

        public void OnCutsceneEnd()
        {
            SceneManager.LoadScene("SampleScene");
            _input.Catscene.Click.performed -= OnClick;
            _input.Disable();
        }

        public void ChangeSpeed()
        {
            animator.SetFloat("Speed", 1);
            animator.GetComponent<Rigidbody2D>().linearVelocity = -transform.right;
        }
    }
}
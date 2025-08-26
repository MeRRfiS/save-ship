using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Project.Scripts
{
    public class Chooselanguage : MonoBehaviour
    {
        public void LanguageButton(string lang)
        {
            PlayerPrefs.SetString("Language", lang);
            SceneManager.LoadScene("Catscene");
        }
    }
}
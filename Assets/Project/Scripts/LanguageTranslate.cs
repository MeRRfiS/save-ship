using TMPro;
using UnityEngine;

namespace Assets.Project.Scripts
{
    public class LanguageTranslate : MonoBehaviour
    {
        [SerializeField] private string _uaLine;
        [SerializeField] private string _enLine;

        private void Awake()
        {
            if(PlayerPrefs.GetString("Language") == "en")
            {
                GetComponent<TMP_Text>().text = _enLine;
            }
            else
            {
                GetComponent<TMP_Text>().text = _uaLine;
            }
        }
    }
}
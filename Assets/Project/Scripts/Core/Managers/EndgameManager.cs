using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Project.Scripts.Core.Managers
{
    public class EndgameManager : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
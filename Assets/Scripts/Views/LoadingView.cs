using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;

        public Slider ProgressBar => progressBar;
    }
}
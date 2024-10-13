using UnityEngine.EventSystems;

namespace UI.Toolbar
{
    using UnityEngine;
    using UnityEngine.UI;

    public class ColorAnimation : MonoBehaviour
    {
        public Color normalColor;
        public Color hoverColor;
        public Color pressColor;

        private Image image;
        private PointerEventData pointerEventData;
        private EventSystem eventSystem;

        private void Start()
        {
            image = GetComponent<Image>();
            eventSystem = GameObject.FindObjectOfType<EventSystem>();
        }

        private void OnEnable()
        {
            eventSystem.firstSelectedGameObject = gameObject;
        }

        private void Update()
        {
            if (eventSystem.currentSelectedGameObject == gameObject)
            {
                pointerEventData = new PointerEventData(eventSystem);
                HandlePointerEvents(pointerEventData);
            }
        }

        private void HandlePointerEvents(PointerEventData eventData)
        {
            if (eventData.pointerEnter )
            {
                image.color = hoverColor;
            }
            else if (eventData.pointerPress)
            {
                image.color = pressColor;
            }
            else if (!eventData.pointerPress)
            {
                image.color = hoverColor;
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionsMenu : MonoBehaviour
{
    // Referencias a los elementos de la UI
    public GameObject optionsBanner; // Panel del menú de opciones
    public Button musicButton; // Botón para activar/desactivar música
    public Button soundButton; // Botón para activar/desactivar sonido
    public Button backButton; // Botón para cerrar el menú
    public Button playButton; // Botón para reanudar el juego tras pausar

    // Sprites que indican el estado de música y sonido
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    public GameObject otherCanvasObject; // Otro objeto del canvas a desactivar

    // Estados de música y sonido
    private bool isMusicOn;
    private bool isSoundOn;

    private GraphicRaycaster raycaster;
    private bool isPaused = false; // Indica si el juego está pausado

    void Start()
    {
        // Inicializa el raycaster y configura los botones
        raycaster = GetComponent<GraphicRaycaster>();
        backButton.onClick.AddListener(CloseOptionsMenu);
        playButton.onClick.AddListener(ResumeGame);

        // Cargar configuraciones guardadas de música y sonido
        isMusicOn = PlayerPrefs.GetInt("Music", 1) == 1;
        isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;

        UpdateButtonSprites();
        ApplySettings();

        // Ocultar inicialmente el panel de opciones
        optionsBanner.SetActive(false);
        playButton.gameObject.SetActive(false); // Ocultar botón de reanudar inicialmente
    }

    public void ToggleOptionsMenu()
    {
        if (!optionsBanner.activeSelf)
        {
            // Mostrar el menú de opciones con una animación
            optionsBanner.SetActive(true);
            //LeanTween.scale(optionsBanner, Vector3.one, 0.3f).setEaseOutBack().setFrom(Vector3.zero);
            PauseGame(); // Pausar el juego al abrir el menú
        }
        else
        {
            CloseOptionsMenu();
        }
    }

    private void CloseOptionsMenu()
    {
        // Cerrar el menú de opciones con una animación
        //LeanTween.scale(optionsBanner, Vector3.zero, 0.3f).setEaseInBack().setOnComplete(() => {
            optionsBanner.SetActive(false);
        //});

        otherCanvasObject.SetActive(false);
        ResumeGame(); // Reanudar el juego al cerrar el menú
    }

    public void ToggleMusic()
    {
        // Alternar el estado de la música y guardar el cambio
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt("Music", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateButtonSprites();

        MuteAllByTag("Musica", !isMusicOn);
    }

    public void ToggleSound()
    {
        // Alternar el estado del sonido y guardar el cambio
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("Sound", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
        UpdateButtonSprites();

        MuteAllByTag("Sonido", !isSoundOn);
        MuteAllByTag("Bomb", !isSoundOn);
        MuteAllByTag("Drug", !isSoundOn);
    }

    private void UpdateButtonSprites()
    {
        // Actualizar los sprites de los botones según el estado
        musicButton.image.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
        soundButton.image.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
    }

    private void MuteAllByTag(string tag, bool mute)
    {
        // Mutea o desmutea todos los objetos con un tag específico
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            AudioSource audioSource = obj.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.mute = mute;
            }
        }
    }

    private void ApplySettings()
    {
        // Aplicar configuraciones iniciales de música y sonido
        MuteAllByTag("Musica", !isMusicOn);
        MuteAllByTag("Sonido", !isSoundOn);
        MuteAllByTag("Bomb", !isSoundOn);
        MuteAllByTag("Drug", !isSoundOn);
    }

    private void PauseGame()
    {
        // Pausar el juego
        Time.timeScale = 0f;
        isPaused = true;
        playButton.gameObject.SetActive(true); // Mostrar botón de reanudar
    }

    private void ResumeGame()
    {
        // Reanudar el juego
        Time.timeScale = 1f;
        isPaused = false;
        playButton.gameObject.SetActive(false); // Ocultar botón de reanudar
    }

    void Update()
    {
        // Detectar clic fuera del menú de opciones
        if (Input.GetMouseButtonDown(0))
        {
            if (optionsBanner.activeSelf && !IsPointerOverUIObject())
            {
                CloseOptionsMenu();
            }
            else if (otherCanvasObject.activeSelf && IsPointerOverObject(otherCanvasObject))
            {
                otherCanvasObject.SetActive(false);
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        // Detecta si el puntero está sobre un objeto de UI
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var raycastResults = new System.Collections.Generic.List<RaycastResult>();
        raycaster.Raycast(pointerData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject == optionsBanner)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsPointerOverObject(GameObject obj)
    {
        // Detecta si el puntero está sobre un objeto específico
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var raycastResults = new System.Collections.Generic.List<RaycastResult>();
        raycaster.Raycast(pointerData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject == obj)
            {
                return true;
            }
        }

        return false;
    }
}

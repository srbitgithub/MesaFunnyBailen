using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsAutoConfig : MonoBehaviour
{
    public class bodiesController
    {
        public Material[] materials = new Material[10];
        public Transform[] objects = new Transform[10];
        public int Current;
    }

    public class legsController
    {
        public Material[] materials = new Material[10];
        public Transform[] objects = new Transform[10];
        public int Current;
    }

    public bodiesController bodies = new bodiesController();
    public legsController legs = new legsController();
   
    public GameObject furniture;
    public GameObject decoration;
    public GameObject buttonsPanel;
    Animator theAnimator;

    public GameObject decorationButton;
    public GameObject texturesButton;
    public GameObject zoomButton;
    public GameObject rotateButton;

    public GameObject texturesPanel;
    public GameObject zoomPanel;
    public GameObject openCloseTablePanel;
    public Text modeText;

    float lastWidthResolution = Screen.width;
    public GameObject cameraController;
    public String currentPanel = "rotate";

    

    void Start()
    {
        furniture = GameObject.FindWithTag("Furniture");
        theAnimator = furniture.GetComponent<Animator>();
        decoration = GameObject.FindWithTag("Decoration");
        decorationButton = GameObject.FindWithTag("DecorationButtonMain");
        texturesButton = GameObject.FindWithTag("TexturesButtonMain");
        zoomButton = GameObject.FindWithTag("ZoomButtonMain");
        rotateButton = GameObject.FindWithTag("RotateButtonMain");
        GameObject mdText = GameObject.FindWithTag("ModeTextMain");
        modeText = mdText.GetComponent<Text>();

        texturesPanel = GameObject.FindWithTag("TexturesPanel");
        zoomPanel = GameObject.FindWithTag("ZoomPanel");

        cameraController = GameObject.FindWithTag("CameraController");

        redrawView();

        rotateButtonClicked();
    }

    Material[] getObjectsMaterial(Transform[] objectsArray)
    {
        Material[] materialArray = new Material[10];
        Transform item;
        for (int i = 0; i < objectsArray.Length; i++)
        {
            item = objectsArray[i];
            if (item != null)
            {
                if (item.childCount > 0)
                    materialArray[i] = item.GetChild(0).transform.GetComponent<Renderer>().material;
                else
                    materialArray[i] = item.GetComponent<Renderer>().material;
            }
        }
        return materialArray;
    }

    void Update()
    {
        float currentScreenWidth = Screen.width;
        if (lastWidthResolution != currentScreenWidth)
        {
            redrawView();
            lastWidthResolution = currentScreenWidth;
            modeText.transform.position = new Vector2(currentScreenWidth - 170, 20);
        }
    }

    Material[] getMaterials(Transform[] objects, Material[] materials)
    {
        Transform item;
        Material getItemMaterial;
        for (int i = 0; i < objects.Length; i++)
        {
            item = objects[i];
            if (item != null)
            {
                if (item.childCount > 0)
                {
                    getItemMaterial = item.GetChild(0).transform.GetComponent<Renderer>().material;
                }
                else
                {
                    getItemMaterial = item.transform.GetComponent<Renderer>().material;
                }
                materials[i] = getItemMaterial;
            }
        }
        return materials;
    }

    void redrawView()
    {
        float widthResolution = Screen.width;
        int buttonsWidth = 100;
        int spaceBetweenButtons = 10;
        int rightMargin = 5;
        float initPoint = 0 + (buttonsWidth / 2.0f) + rightMargin;
        decorationButton.transform.position = new Vector2(initPoint, decorationButton.transform.position.y);
        texturesButton.transform.position = new Vector2(decorationButton.transform.position.x + buttonsWidth + spaceBetweenButtons, texturesButton.transform.position.y);
        zoomButton.transform.position = new Vector2(texturesButton.transform.position.x + buttonsWidth + spaceBetweenButtons, zoomButton.transform.position.y);
        rotateButton.transform.position = new Vector2(zoomButton.transform.position.x + buttonsWidth + spaceBetweenButtons, rotateButton.transform.position.y);
    }

    void enableDisableButtons(Boolean decorationBtn, Boolean texturesBtn, Boolean zoomBtn, Boolean rotateBtn)
    {
        decorationButton.GetComponent<Button>().interactable = decorationBtn;
        texturesButton.GetComponent<Button>().interactable = texturesBtn;
        zoomButton.GetComponent<Button>().interactable = zoomBtn;
        rotateButton.GetComponent<Button>().interactable = rotateBtn;
    }

    public void textureButtonClicked()
    {
        enableDisableCameraControllers(false);
        currentPanel = "textures";
        closePanels();
        texturesPanel.SetActive(true);
        enableDisableButtons(true, false, true, true);
        modeText.text = "Modo Texturas";
    }

    public void zoomButtonClicked()
    {
        enableDisableCameraControllers(false);
        closePanels();
        zoomPanel.SetActive(true);
        enableDisableButtons(true, true, false, true);
        modeText.text = "Modo Zoom";
    }

    public void rotateButtonClicked()
    {
        enableDisableButtons(true, true, true, false);
        modeText.text = "Modo Rotación";
        closePanels();
        enableDisableCameraControllers(true);
    }

    public void openCloseButtonClicked()
    {
        //enableDisableCameraControllers(false);
        closePanels();
        openCloseTablePanel.SetActive(true);
        enableDisableButtons(false, true, true, true);
        modeText.text = "Abrir/Cerrar Mesa";
    }

    public void closePanels()
    {
        texturesPanel.SetActive(false);
        zoomPanel.SetActive(false);
        openCloseTablePanel.SetActive(false);
    }


    void enableDisableCameraControllers(Boolean enableDisable)
    {
        cameraController.GetComponent<RotateMode>().enabled = enableDisable;
        cameraController.GetComponent<ZoomMode>().enabled = enableDisable;
        cameraController.GetComponent<PanMode>().enabled = enableDisable;
    }

    public void playOpenTable()
    {
        theAnimator.Play("TableOpen");
    }

    public void playCloseTable()
    {
        theAnimator.Play("TableClose");
    }
}
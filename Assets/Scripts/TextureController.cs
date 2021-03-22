using UnityEngine;
using UnityEngine.UI;

public class TextureController : MonoBehaviour
{
    [Header("PANEL BUTTONS")]
    public GameObject[] texturesButtons = new GameObject[20];
    [Header("IMAGE BUTTONS (SPRITES)")]
    public Sprite[] topMaterialsSprites = new Sprite[20];
    public Sprite[] legsMaterialsSprites = new Sprite[13];
    public Sprite spriteNone;
    [Header("MATERIALS")]
    public Material[] topMaterials;
    public Material[] legsMaterials;

    public class textureButtons
    {
        public Vector2[] ButtonPosition = new Vector2[20];
        public int Position = 0;
    }

    public textureButtons btnPosition = new textureButtons();

    Transform currentObject;
    public Transform[] tops;
    public Transform[] legs;
    public GameObject selectedMaterialButton;
    public GameObject overMaterialButton;
    public GameObject furniture;
    public Text textureText;
    string currentPanel = "tops";
    int textureTop = 0;
    int textureLegs = 12;
    int theCount = 0;

    void Start()
    {
        furniture = GameObject.FindWithTag("Furniture");

        getTextureButtonsPosition();
        legClicked();
        topClicked();
    }

    void getTextureButtonsPosition()
    {
        for (int i = 0; i < texturesButtons.Length; i++)
        {
            btnPosition.ButtonPosition[i] = texturesButtons[i].GetComponent<RectTransform>().localPosition;
        }
    }

    public void legClicked()
    {
        for (int i = 0; i < legsMaterialsSprites.Length; i++)
        {
            texturesButtons[i].GetComponent<Image>().sprite = legsMaterialsSprites[i];
        }
        for (int i = legsMaterialsSprites.Length; i < 20; i++)
        {
            texturesButtons[i].GetComponent<Image>().sprite = spriteNone;
        }
        currentPanel = "leg";
        whenClickButtonTexture(textureLegs);
    }

    public void topClicked()
    {
        for (int i = 0; i < topMaterialsSprites.Length; i++)
        {
            texturesButtons[i].GetComponent<Image>().sprite = topMaterialsSprites[i];
        }
        currentPanel = "tops";
        whenClickButtonTexture(textureTop);
    }


    void samePositionhighlight()
    {
        overMaterialButton.GetComponent<RectTransform>().localPosition = selectedMaterialButton.transform.localPosition;
    }

    string changeRobleNames(string textureRoble)
    {
        string forReturn = "";
        if (textureRoble.Contains("Ceniza")) return  "Roble_Teñido_Ceniza";
        if (textureRoble.Contains("Tostado")) forReturn = "Roble_Teñido_Tostado";
        if (textureRoble.Contains("Wengue")) forReturn = "Roble_Teñido_Wengué";
        return forReturn;
    }

    public void whenOverButtonTexture(int item)
    {
        string spriteName = texturesButtons[item].GetComponent<Image>().sprite.name;
        if (spriteName != "None_Button")
        {
            selectedMaterialButton.GetComponent<RectTransform>().localPosition = btnPosition.ButtonPosition[item];
            if (spriteName.Contains("Tenydo")) spriteName = changeRobleNames(spriteName);
            textureText.text = spriteName;
        }
        else
            samePositionhighlight();
    }
    public void whenClickButtonTexture(int item)
    {
        string spriteName = texturesButtons[item].GetComponent<Image>().sprite.name;
        print(spriteName);
        if (spriteName != "None_Button")
        {
            overMaterialButton.GetComponent<RectTransform>().localPosition = btnPosition.ButtonPosition[item];
            selectedMaterialButton.GetComponent<RectTransform>().localPosition = btnPosition.ButtonPosition[item];
            if (currentPanel == "tops")
            {
                for (int i = 0; i < tops.Length; i++)
                {
                    tops[i].GetComponent<Renderer>().material = topMaterials[item];
                }
                if (spriteName.Contains("Tenydo")) spriteName = changeRobleNames(spriteName);
                textureTop = item;
            }
            else
            {
                print("estoy en patas");
                for (int i = 0; i < legs.Length; i++)
                {
                    legs[i].GetComponent<Renderer>().material = legsMaterials[item];
                }
                textureLegs = item;
            }
            textureText.text = spriteName;
            print(textureTop);
            print(textureLegs);
        }
    }
}
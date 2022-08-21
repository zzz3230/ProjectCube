using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenuWidgetScript : MonoBehaviour
{
    [SerializeField] SchematicManagerScript _schematicManager;

    [SerializeField] TMP_InputField _scheamticNameInputField;

    [SerializeField] RectTransform _schematicToLoadParent;

    [SerializeField] MenuSchematicLoaderButtonWidgetScript _schematicLoaderWidgetOriginal;

    public bool isShowing => gameObject.activeSelf;

    void Start()
    {
        _scheamticNameInputField.text = "save_" + System.DateTime.Now.Ticks + ".pcsc.json";
    }

    private void Update()
    {
        if(isShowing &&
            //Input.GetAxis("Back") == 1f &&
            Input.GetKeyDown(KeyCode.Escape) 
            )
            Button_Continue();
    }

    public void Button_Continue()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        UpdateSchematicLoLoad();
    }

    void UpdateSchematicLoLoad()
    {
        foreach (Transform t in _schematicToLoadParent)
            Destroy(t.gameObject);

        var scs = _schematicManager.AvailableSchematics();

        for (int i = 0; i < scs.Count; i++)
        {
            var loader = Instantiate(_schematicLoaderWidgetOriginal);
            loader.transform.SetParent(_schematicToLoadParent);
            loader.Init(scs[i], _schematicManager);
        }

    }

    public void Button_SaveSchematic()
    {
        _schematicManager.Save(_scheamticNameInputField.text);
    }

    public void Button_Exit()
    {
        Utils.LoadScene("DemoMenu0");
    }
}

using System.Net.NetworkInformation;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    // making this script into a singleton pattern class
    // instance is the name we are giving the game object the script is attached to now
    public static MainManager Instance {get; private set; }
    public float timeElapsed;
    public Color unitColour;

    private void Awake()
    // awake function is called before anything else
    {
        // making sure that there is always one of this and destroying any copies
        if(Instance != null)
        // if we already have a definition for instance...
        {
            Destroy(gameObject);
        }

        // setting the current object as Instance
        // this refers to the current class, which is MainManager
        Instance = this;

        // making this object persistent
        DontDestroyOnLoad(gameObject);

        LoadColour();

    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    // tag as serialisable so that it can be converted to Json format by the JsonUtility class
    [System.Serializable]
    class SaveData
    {
        // color will be an array of three values - R, G, B
        public Color _unitColour;

    }

    // serialisation, bridge between unity and JSON file
    public void SaveColour()
    {
        // create new data from SaveData template defined above
        SaveData data = new SaveData();
        data._unitColour = unitColour;

        // convert data to Json format
        string jsonData = JsonUtility.ToJson(data);
        // use a persistentDataPath so that game is always able to save to a path on diffent computers
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", jsonData);
        Debug.Log("New color saved: " + unitColour);
    }

    // reverse of SaveColour()
    public void LoadColour()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(path))
        {
            Debug.Log("json file exists");
            string jsonData = File.ReadAllText(path);
            // deserialisation using SaveData template
            SaveData data = JsonUtility.FromJson<SaveData>(jsonData);

            unitColour = data._unitColour;
        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;

public class ModelTargetDatabaseXmlReader : MonoBehaviour
{
    XmlSerializer serializer = new XmlSerializer(typeof(QCARConfig));

    // Modified method to use callbacks for processing loaded XML data
    public void GetAllModelTargetNames(List<string> databaseNames, System.Action<Dictionary<string, HashSet<string>>> onComplete)
    {
        StartCoroutine(LoadModelTargets(databaseNames, onComplete));
    }

    private IEnumerator LoadModelTargets(List<string> databaseNames, System.Action<Dictionary<string, HashSet<string>>> onComplete)
    {
        Dictionary<string, HashSet<string>> databaseModelTargets = new Dictionary<string, HashSet<string>>();

        foreach (var databaseName in databaseNames)
        {
            string path = Path.Combine(Application.streamingAssetsPath, $"Vuforia/{databaseName}.xml");
            UnityWebRequest www = UnityWebRequest.Get(path);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning($"Failed to load XML file for database: {databaseName}");
            }
            else
            {
                // Successfully loaded the XML, now process it
                using (StringReader reader = new StringReader(www.downloadHandler.text))
                {
                    QCARConfig qcarConfig = (QCARConfig)serializer.Deserialize(reader);
                    if (!databaseModelTargets.ContainsKey(databaseName))
                    {
                        databaseModelTargets[databaseName] = new HashSet<string>();
                    }

                    if (qcarConfig.Tracking != null && qcarConfig.Tracking.ModelTargets != null)
                    {
                        foreach (var modelTarget in qcarConfig.Tracking.ModelTargets)
                        {
                            databaseModelTargets[databaseName].Add(modelTarget.Name);
                        }
                    }
                }
            }
        }

        onComplete?.Invoke(databaseModelTargets);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class ModelTargetDatabaseXmlReader : MonoBehaviour
{
    private List<QCARConfig> GetModelsFromModelTargetDatabaseXml()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Vuforia");

        if (!Directory.Exists(path))
        {
            Debug.LogWarning($"Directory does not exist at path: {path}");
            return null;
        }

        string[] files = Directory.GetFiles(path, "*.xml");
        if (files.Length == 0)
        {
            Debug.LogWarning("No XML files found in the directory.");
            return null;
        }

        List<QCARConfig> qcarConfigs = new List<QCARConfig>();
        XmlSerializer serializer = new XmlSerializer(typeof(QCARConfig));

        foreach (string file in files)
        {
            using (StreamReader reader = new StreamReader(file))
            {
                QCARConfig qcarConfig = (QCARConfig)serializer.Deserialize(reader);
                qcarConfigs.Add(qcarConfig);
            }
        }

        return qcarConfigs;
    }

    public Dictionary<string, HashSet<string>> GetAllModelTargetNames()
    {
        List<QCARConfig> qcarConfigs = GetModelsFromModelTargetDatabaseXml();
        if (qcarConfigs == null)
        {
            Debug.LogWarning("No QCARConfig objects found.");
            return null;
        }

        Dictionary<string, HashSet<string>> databaseModelTargets = new Dictionary<string, HashSet<string>>();

        foreach (var qcarConfig in qcarConfigs)
        {
            if (qcarConfig.Tracking != null && qcarConfig.Tracking.ModelTargets != null)
            {
                foreach (var modelTarget in qcarConfig.Tracking.ModelTargets)
                {
                    // Use the database name as the key if available, otherwise use the model target name
                    string databaseName = qcarConfig.ModelTargetDatabase != null ? qcarConfig.ModelTargetDatabase.Name : modelTarget.Name;

                    if (!databaseModelTargets.ContainsKey(databaseName))
                    {
                        databaseModelTargets[databaseName] = new HashSet<string>();
                    }

                    databaseModelTargets[databaseName].Add(modelTarget.Name);
                }
            }
        }

        return databaseModelTargets;
    }
}

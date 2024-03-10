using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class ModelTargetDatabaseXmlReader : MonoBehaviour
{
    // Adjusted method to load XML files based on database names provided by the manifest
    public Dictionary<string, HashSet<string>> GetAllModelTargetNames(List<string> databaseNames)
    {
        // Dictionary to hold database names and their corresponding model target names
        Dictionary<string, HashSet<string>> databaseModelTargets = new Dictionary<string, HashSet<string>>();
        XmlSerializer serializer = new XmlSerializer(typeof(QCARConfig));

        foreach (string databaseName in databaseNames)
        {
            // Load each specified database XML file
            TextAsset xmlTextAsset = Resources.Load<TextAsset>($"XmlDatabaseFiles/{databaseName}");
            if (xmlTextAsset != null)
            {
                using (StringReader reader = new StringReader(xmlTextAsset.text))
                {
                    // Deserialize the XML file into a QCARConfig object
                    QCARConfig qcarConfig = (QCARConfig)serializer.Deserialize(reader);
                    // Ensure the database name is in the dictionary
                    if (!databaseModelTargets.ContainsKey(databaseName))
                    {
                        databaseModelTargets[databaseName] = new HashSet<string>();
                    }

                    // If the QCARConfig has model targets, add them to the hash set
                    if (qcarConfig.Tracking != null && qcarConfig.Tracking.ModelTargets != null)
                    {
                        foreach (var modelTarget in qcarConfig.Tracking.ModelTargets)
                        {
                            databaseModelTargets[databaseName].Add(modelTarget.Name);
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning($"Failed to load XML file for database: {databaseName}");
            }
        }

        return databaseModelTargets;
    }
}

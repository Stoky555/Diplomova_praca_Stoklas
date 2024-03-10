using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("QCARConfig")]
public class QCARConfig
{
    [XmlElement("Tracking")]
    public Tracking Tracking { get; set; }

    [XmlElement("Assembly")]
    public List<Assembly> Assemblies { get; set; }

    [XmlElement("ModelTargetDatabase")]
    public ModelTargetDatabase ModelTargetDatabase { get; set; }
}

public class Tracking
{
    [XmlElement("ModelTarget")]
    public List<ModelTarget> ModelTargets { get; set; }
}

public class ModelTarget
{
    [XmlAttribute("name")]
    public string Name { get; set; }
}

public class Assembly
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("assemblyId")]
    public string AssemblyId { get; set; }
}

public class ModelTargetDatabase
{
    [XmlAttribute("name")]
    public string Name { get; set; }
}

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

    [XmlAttribute("trackingMode")]
    public string TrackingMode { get; set; }

    [XmlAttribute("motionHint")]
    public string MotionHint { get; set; }

    [XmlAttribute("upVector")]
    public string UpVector { get; set; }

    [XmlAttribute("optimizeTrackingFor")]
    public string OptimizeTrackingFor { get; set; }
}

public class Assembly
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("assemblyId")]
    public string AssemblyId { get; set; }

    [XmlElement("Part")]
    public Part Part { get; set; }

    [XmlElement("EntryPoint")]
    public EntryPoint EntryPoint { get; set; }
}

public class Part
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("translation")]
    public string Translation { get; set; }

    [XmlAttribute("rotation")]
    public string Rotation { get; set; }
}

public class EntryPoint
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("trained")]
    public string Trained { get; set; }
}

public class ModelTargetDatabase
{
    [XmlAttribute("name")]
    public string Name { get; set; }
}

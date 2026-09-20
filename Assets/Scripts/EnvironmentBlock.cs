using UnityEngine;

public class EnvironmentBlock
{
    public GameObject tree;
    public GameObject power_line;
    public GameObject fire;
    public GameObject flood;

    public EnvironmentBlock(GameObject tree, GameObject power_line, GameObject fire, GameObject flood)
    {
        this.tree = tree;
        this.power_line = power_line;
        this.fire = fire;
        this.flood = flood;
    }
}

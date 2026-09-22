using UnityEngine;
using System.Collections.Generic;
public static class DialogueTemplates
{
    //looks up the template for a claim type, fills in the blanks
    public static string Build(ClaimType type, string[] vars) =>
        string.Format(templates[type], vars);

    
    //one template per claim type
    static Dictionary<ClaimType, string> templates = new()
    {
        {ClaimType.RoadBlocked, "The road infront of {0} is blocked, cars can't get through because of {1}."},
        //more to come! flooded, wrong location, people trapped, clear? uncertain.
    };
}

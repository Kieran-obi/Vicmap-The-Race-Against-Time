using UnityEngine;
using System.Collections.Generic;
public static class DialogueTemplates
{
    //looks up the template for a claim type, fills in the blanks
    public static string Build(CallClaimType type, string[] vars) =>
        string.Format(templates[type], vars);

    
    //one template per claim type
    static Dictionary<CallClaimType, string> templates = new()
    {
        {CallClaimType.RoadBlocked, "The road infront of {0} is blocked, cars can't get through because of {1}."},
        {CallClaimType.Flooded, "The area around {0} is flooded, there are {1} people who are stuggling to deal with it!"},
        {CallClaimType.WrongLocation, "I'm standing infront of {0}, there are powerlines down and electricity out."},
        {CallClaimType.PeopleTrapped, "There are people trapped inside {0}, there are {1} blocking the exits!"},
        {CallClaimType.Clear, "Everything looks okay at {0}, nothing to report, just thought you could use the info, it's getting wild out here!"},
        {CallClaimType.Uncertain, "Can't really tell from here but I think {0} is getting {1}. Hope this helps!"}
        //more to come!
    };
}

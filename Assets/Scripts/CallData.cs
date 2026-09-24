using UnityEngine;


//the differnt types of reports from callers
public enum CallClaimType {RoadBlocked, Flooded, WrongLocation, PeopleTrapped, Clear, Uncertain}
//types of data errors in the call
public enum IssueType {None, Missing, Misclassified, Outdated, Mislocated}
//what the camera shows to the player if one exists at the location 
public enum CameraVisual {Confirms, Contradicts, Frozen, NoSignal}


[CreateAssetMenu(fileName = "CallData", menuName = "Vicmap/Call")]
public class CallData : ScriptableObject
{
    public int stormStage; //1, 2 or 3
    public LocationData callerLocation; //where the caller is
    public LocationData actualLocation; //for misloacated calls, the location is different to the callers location
    public CallClaimType claimType; //claim template
    public string[] claimVars; //fills tempolate in order {0}, {1}
    public IssueType issueType; //none if the caller is right
    public bool hasCamera; //does a camera exist
    public CameraVisual cameraVisual; //what the camera shows 
    public int civiliansAtRisk; //used for civilians saved?
}

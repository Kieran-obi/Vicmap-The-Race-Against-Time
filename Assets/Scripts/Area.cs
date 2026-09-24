using UnityEngine;

public class Area
{
    public int x;
    public int y;
    public bool safe;
    public EnvironmentBlock type;

    public Area(int x, int y, bool safe)
    {
        this.x = x; 
        this.y = y; 
        this.safe = safe;
    }
}

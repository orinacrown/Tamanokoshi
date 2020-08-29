/************************************************
 *                                              *
 * Class Name   : Time Flagger                  *
 * Super Class  :                               *
 * Author       : orinacrown                    *
 * Create day   : 2020/05/01                    *
 * Update day   :                               *
 *                                              *
 ************************************************/


/*  Use libraries       */
using UnityEngine;
using System.Collections;

/*  Class               */
public class TimeFlagger
{
    /*  Constant Arguments      */

    /*  Constructor Arguments   */
    private readonly int flagThresholdMS;   /*  Measure Time    */

    /*  Read Only Arguments     */
    private readonly float startTime;       /*  Measure Start   */

    /*  Variable Arguments      */
    private int pastTimeMS;
    
    /*  Constructor             */
    public TimeFlagger(int flagThresholdMS)
    {
        this.flagThresholdMS = flagThresholdMS;
        startTime = Time.time;
        pastTimeMS = 0;
    }

    /*  Methods                 */
    
    
    private void MeasurePastTime()
    {
        float currentTime = Time.time;
        pastTimeMS = (int)((currentTime - startTime) * 1000);
    }

    public bool IsTimeOver()
    {
        return IsTimeOverOffset(0);
    }

    public bool IsTimeOverOffset(int offset)
    {
        int overTime;
        MeasurePastTime();
        overTime = pastTimeMS - flagThresholdMS + offset;
        if (overTime >= 0)
        {
            return true;
        }
        return false;

    }

    public int PastTimeMS()
    {
        float pastTime = flagThresholdMS - pastTimeMS;
        return (int)pastTime;
    }
}

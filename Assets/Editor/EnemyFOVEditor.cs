using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyFOV))]

public class EnemyFOVEditor : Editor
{
    private void OnSceneGUI()
    {
        // FOV doesn't update rotation when in play mode, FOV seems to update in game though.
        EnemyFOV fov = (EnemyFOV)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.position.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.position.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);
        /*
        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            
            foreach (GameObject players in players)
            {

            }
            
            Handles.DrawLine(fov.transform.position, fov.players.transfrom.position); // would need to be a for each loop
        }*/
    }

    private Vector3 DirectionFromAngle(float eulerY,float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

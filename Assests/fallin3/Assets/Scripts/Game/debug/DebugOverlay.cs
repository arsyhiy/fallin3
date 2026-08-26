/* DebugOverlay.cs: логирование на экра

тоесть как DebugUtility но для логирования на экран а не в консоль 
*/
using UnityEngine;

// TODO: добавить что то вроде тумблеров чтобы выводить кокретную нужную информацию и так можно большой список

namespace Unity.Game
{
    public class DebugOverlay : MonoBehaviour
    {
        public Transform player;

        bool show = true;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3))
                show = !show;
        }

        void OnGUI()
        {
            if (!show) return;

            GUILayout.BeginArea(new Rect(10, 10, 300, 200), GUI.skin.box);

            GUILayout.Label($"FPS: {(1f / Time.deltaTime):F1}");

            if (player != null)
            {
                GUILayout.Label($"Position: {player.position}");
                GUILayout.Label($"Rotation: {player.eulerAngles}");
            }

            GUILayout.Label($"Time: {Time.time:F1}");

            GUILayout.EndArea();
        }
    }
}
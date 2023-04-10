using System.Collections.Generic;
using UnityEngine;

namespace HyPack
{
    [CreateAssetMenu(fileName = "CustomDialogRepo", menuName = "HyPack/CustomDialogRepo")]
    public class DialogRepo : ScriptableObject
    {
        [SerializeField] private List<IDialog> m_DialogTemplatePrefabs = new List<IDialog>();

        private Dictionary<string, IDialog> m_DialogTemplateMap = new Dictionary<string, IDialog>();

        public void Initialize()
        {
            m_DialogTemplatePrefabs.ForEach(x =>
            {
                m_DialogTemplateMap.Add(x.GetType().Name, x);
            });
        }

        public T GetTemplate<T>() where T : IDialog
        {
            m_DialogTemplateMap.TryGetValue(typeof(T).Name, out var result);
            if (result is null) Debug.LogError($"[Error] No DialogTemplate({typeof(T).Name}) ref in DialogRepo");
            return result as T;
        }
    }
}

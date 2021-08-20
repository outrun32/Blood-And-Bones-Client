using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Networking
{
    public delegate void GetStartInfo(float maxHealth, float maxMana, float health, float mana);
    public delegate void GetInfo(float health, float mana);

    public delegate void ReturnVoid();
    public class PlayerManager : MonoBehaviour
    {
        private int _id;
        private string _username;
        [SerializeField] private bool _usernameIsvizible;
        [SerializeField] private ClientPlayerController _clientController; 
        [SerializeField] private AnimationController _animationController;
        [SerializeField] private Text _usernameText;
        [SerializeField] private HudController _hud;
        public event GetStartInfo GetStartInfoEvent;
        public event GetInfo GetInfoEvent;
        public event ReturnVoid DeathEvent;
        public void SetHud(HudController hudController)
        {
            _hud = hudController;
        }
        public void SetID(int value)
        {
            _id = value;
        }
    
        public void SetUsername(string value)
        {
            if (_usernameIsvizible) _usernameText.text = value;
            else _usernameText.text = "";
            _username = value;
        }
    
        public void SetPosition(Vector3 position)
        {
            _clientController.SetPosition(position);
        }

        public void SetStartInfo(float maxHealth, float maxMana, float startHealth, float startMana)
        {
            Debug.Log($"Start MH = {maxHealth}, MM = {maxMana}, SH = {startHealth}, SM = {startMana}");
            GetStartInfoEvent?.Invoke(maxHealth, maxMana,startHealth, startMana);
            _hud.Init(maxHealth, maxMana,startHealth, startMana);
        }
        public void SetInfo(float health, float mana)
        {
            Debug.Log($"SetInfo H = {health}, M = {mana}");
            GetInfoEvent?.Invoke(health, mana);
            _hud.UpdateImages(health, mana);
        }
        public void SeAnimation(AnimationModel model)
        {
            _animationController.NUpdate(model);
        }
        public void SetRotation(Quaternion rotation)
        {
            _clientController.SetRotation(rotation);
        }

        public void Death()
        {
            Debug.Log("Death");
            DeathEvent?.Invoke();
            StartCoroutine(DeathCoroutine());
        }
        
        private IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(10);
            Destroy(this.gameObject);
        }
    }
}

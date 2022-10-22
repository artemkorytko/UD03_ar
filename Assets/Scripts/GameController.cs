using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject completePanel;
        [SerializeField] private GameObject fieldPrefab;

        private Player _player;
        private Bot _bot;

        private bool _isEntityMakeStep;
        private Field _field;
        private FieldPositionController _fieldPositionController;
        private FieldSettings _fieldSettings;

        private Coroutine _plaCoroutine;


        private void Awake()
        {
            _player = GetComponent<Player>();
            _bot = GetComponent<Bot>();

            _field = Instantiate(fieldPrefab).GetComponent<Field>();
            _field.Initialize();
            _field.SetState(false);
            _field.Refresh();
            _field.OnCompleted += OnCompleted;

            _fieldSettings = _field.gameObject.GetComponent<FieldSettings>();
            _fieldPositionController = _field.gameObject.GetComponent<FieldPositionController>();
            _fieldPositionController.IsActive = true;
        }

        private void Start()
        {
            mainPanel.SetActive(true);
            gamePanel.SetActive(false);
            completePanel.SetActive(false);
        }

        private void OnDestroy()
        {
            _field.OnCompleted -= OnCompleted;
        }

        public void Play()
        {
            _fieldPositionController.IsActive = false;
            _field.SetState(true);

            _plaCoroutine = StartCoroutine(GameCoroutine());
            
            mainPanel.SetActive(false);
            gamePanel.SetActive(true);
            completePanel.SetActive(false);
        }

        public void Reset()
        {
            _field.Refresh();
            _field.SetState(false);
            _fieldPositionController.IsActive = true;
            
            mainPanel.SetActive(true);
            gamePanel.SetActive(false);
            completePanel.SetActive(false);
        }

        private IEnumerator GameCoroutine()
        {
            Entity currentEntity;

            while (true)
            {
                currentEntity = _player;
                currentEntity.OnStep += OnEntityStep;
                _isEntityMakeStep = false;
                currentEntity.GetStep();

                yield return new WaitWhile(() => _isEntityMakeStep == false);
                currentEntity.OnStep -= OnEntityStep;

                currentEntity = _bot;
                currentEntity.OnStep += OnEntityStep;
                _isEntityMakeStep = false;
                currentEntity.GetStep(_field.FieldToArray());

                yield return new WaitUntil(() => _isEntityMakeStep != true);
                currentEntity.OnStep -= OnEntityStep;
            }
        }

        private void OnEntityStep(int index, CellType cellType)
        {
            _field.SetCell(index,cellType);
            _isEntityMakeStep = true;
        }

        private void OnCompleted(CellType type)
        {
            StopCoroutine(_plaCoroutine);
            _field.SetState(false);
            
            mainPanel.SetActive(false);
            gamePanel.SetActive(false);
            completePanel.SetActive(true);
        }
        public void ChangeFieldScale(float value)
        {
            _fieldSettings.ChangeScale(value);
        }

        public void ChangeFieldRotation(float value)
        {
            _fieldSettings.ChangeRotation(value);
        }

    }
}
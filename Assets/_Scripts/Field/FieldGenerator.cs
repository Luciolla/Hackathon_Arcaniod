using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Field
{
	public class FieldGenerator : MonoBehaviour
	{
		private const float ReleaseDelay = 1f;
		
		[SerializeField] private GameObject _squarePrefab;
		[SerializeField] private int _squareCount;

		private GridLayoutGroup _group;

		public int GetMaxBlock => _squareCount;
		public List<GameObject> GetSquareList {get; private set;} = new List<GameObject>();

		private void Awake() =>
			_group = GetComponent<GridLayoutGroup>();

		private void Start() =>
			GenerateField();

		
		[ContextMenu("RestartField")]
		public void RestartField()
		{
			foreach (var square in GetSquareList)
			{
				square.SetActive(true);
			}

			PaintField();
		}

		private void GenerateField()
		{
			for(var i = 0; i < _squareCount; i++)
			{
				var square = Instantiate(_squarePrefab, this.transform);
				GetSquareList.Add(square);
			}

			PaintField();
			ReleaseField();
		}

		private void PaintField()
		{
			var painter = new FieldPainter();
			
			painter.PaintField(GetSquareList);
		}

		private async void ReleaseField()
		{
			await UniTask.Delay(TimeSpan.FromSeconds(ReleaseDelay));
			_group.enabled = false;
		}
	}
}
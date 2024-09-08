using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace _Scripts.Field
{
	public class FieldPainter
	{
		private const float NewAlpha = 0.5f;
		
		private readonly Random _rand = new Random();

		public void PaintField(List<GameObject> squareList)
		{
			foreach (var square in squareList)
			{
				RandomColor(square);
			}
		}
		
		private void RandomColor(GameObject obj)
		{
			var tempGradient = new float[] {
				0, 0, 0,
			};

			if(!obj.TryGetComponent(out SpriteRenderer render))
				return;

			for(var i = 0; i < 3; i++)
				tempGradient[i] = (float)_rand.Next(0, 100) / 100;

			var newColor = new Color(tempGradient[0], tempGradient[1], tempGradient[2], NewAlpha);
			render.color = newColor;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{


	public int rows = 15;
	public int columns = 20;
	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] blockTiles;
	public GameObject startTile;
	public GameObject exitTile;

	public int[,] levelTwoSetup = new int[,] {{ 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
										 	  { 1,0,0,0,0,0,1,1,0,0,0,0,2,0,1,1,1,1,1,1 },
										 	  { 1,0,2,0,8,0,1,1,0,1,0,0,0,0,1,1,1,1,1,1 },
										 	  { 1,0,0,0,2,2,0,2,0,1,0,0,2,2,1,1,1,1,1,1 },
										 	  { 1,1,1,0,0,2,0,0,2,0,2,0,0,0,0,0,1,1,1,1 },
										 	  { 1,1,1,1,2,1,0,2,0,1,1,0,0,2,2,0,1,1,1,1 },
										 	  { 1,0,2,0,0,0,0,2,0,0,2,2,1,0,0,0,0,0,0,1 },
										 	  { 1,0,1,1,1,1,1,0,1,0,0,0,1,2,0,0,2,2,0,1 },
										 	  { 1,2,0,0,0,0,0,2,0,1,2,2,1,0,0,2,0,0,0,1 },
										 	  { 1,0,0,2,0,0,0,2,0,1,0,1,1,0,2,0,0,0,0,1 },
										 	  { 1,0,2,0,0,0,0,0,0,0,0,2,0,2,0,1,1,1,1,1 },
										 	  { 1,0,0,0,0,2,0,0,0,0,0,0,0,0,0,1,1,1,1,1 },
										 	  { 1,0,0,0,0,0,0,0,2,0,0,2,1,1,2,1,1,1,1,1 },
										 	  { 1,1,1,1,1,1,0,0,2,0,0,0,1,1,0,0,0,0,1,1 },
										 	  { 1,1,1,1,1,1,1,0,0,0,2,0,0,0,0,9,2,0,1,1 } };

	private Transform boardHolder;

	void BoardSetup() {
		boardHolder = new GameObject("Board").transform;
		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {
				GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
				// if (x == -1 || x == columns || y == -1 || y == rows) {
				// 	toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.length)];
				// }

				GameObject instance = Instantiate(toInstantiate, new Vector3 (x,y,0f), Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}
	}

	void LevelTwo() {
		for (int x = 0; x < columns; x++) {
			for (int y = 0; y < rows; y++) {

				GameObject toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
				
				if (levelTwoSetup[y,x] == 2) {
					toInstantiate = blockTiles[Random.Range(0, blockTiles.Length)];

				} else if (levelTwoSetup[y,x] == 8) {
					toInstantiate = startTile;

				} else if (levelTwoSetup[y,x] == 9) {
					toInstantiate = exitTile;
				}

				if (levelTwoSetup[y,x] > 0) {
					GameObject instance = Instantiate(toInstantiate, new Vector3 (x,rows-y-1,0f), Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}

			}
		}
	}

	public void SetupScene (int level) {
		BoardSetup();

		if (level == 2){
			LevelTwo();
		}

	}

}

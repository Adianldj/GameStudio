using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections;
using UnityEngine.UI;


public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
	private float SpawnTime = 10f;
	//private const int MaxEnemyCount = 8; // Only 8 enemies allowed on screen at a time
	public  int WaveNumber = 9;
    public int _enemiesLeft;
	private const int enemyNumber = 3;
	private static Object _enemyPrefab;
	private float _lastspawn;
	private Transform _holder;


	private int normal = 0, fast = 1, strong = 2;
	private Color[] render = new Color[]{Color.black , Color.yellow , Color.red};
	private int[] health = new int[]{100, 50, 150};
	private float[] scale = new float[]{0.9f, 0.4f, 1.5f};
	private float[] startspeed = new float[]{7, 10, 4};
	private float[] enespeed = new float[]{7, 10, 4};


    public List<Enemy> beingShocked;



	private int[][] arr = new int[9][];

	public Text WaveNum;
	public Text FastNum;
	public Text SlowNum;
	public Text NormalNum;
	internal void Start ()
	{
	    instance = this;
		_enemyPrefab = Resources.Load("Enemy");
		_holder = transform;
		Enemy.Manager = this;
	    _enemiesLeft = 99;

		WaveNumber = 9;

		arr [0] = new int[2]{ 5, normal };
		arr [1] = new int[2]{ 10, normal };
		arr [2] = new int[6]{ 5, normal, 5, fast, 5, normal };
		arr [3] = new int[6]{ 5, normal, 2, strong, 5, normal };
		arr [4] = new int[6]{ 10, normal, 4, strong, 10, normal };
		arr [5] = new int[6]{ 10, normal, 4, strong, 10, fast };
		arr [6] = new int[6]{ 1, normal, 1, strong, 1, fast };
		arr [7] = new int[6]{ 1, normal, 1, strong, 1, fast };
		arr [8] = new int[6]{ 1, normal, 1, strong, 1, fast };
	}

	internal void Update () {
		if (WaveNumber < 0) {
			return;
		}

		if (MenuManager.instance.WinLoseShowing) {
			return;
		}
		SpawnTime = 25 - WaveNumber / 3;   
		if ((Time.time - _lastspawn) < SpawnTime ) {
			//FastNum.enabled = true;

			if ((Time.time - _lastspawn) > SpawnTime - 4) {
				WaveNum.text = "Wave #" + (10 - WaveNumber);
				if (WaveNumber == 9) {
					FastNum.text = "Fast Number : 0";
					SlowNum.text = "Slow Number : 0";
					NormalNum.text = "Normal Number : 5";
				} else if (WaveNumber == 8) {
					FastNum.text = "Fast Number : 0";
					SlowNum.text = "Slow Number : 0";
					NormalNum.text = "Normal Number : 10";
				} else if (WaveNumber == 7) {
					FastNum.text = "Fast Number : 5";
					SlowNum.text = "Slow Number : 0";
					NormalNum.text = "Normal Number : 10";
				} else if (WaveNumber == 6) {
					FastNum.text = "Fast Number : 0";
					SlowNum.text = "Slow Number : 2";
					NormalNum.text = "Normal Number : 10";
				} else if (WaveNumber == 5) {
					FastNum.text = "Fast Number : 0";
					SlowNum.text = "Slow Number : 4";
					NormalNum.text = "Normal Number : 20";
				} else if (WaveNumber == 4) {
					FastNum.text = "Fast Number : 10";
					SlowNum.text = "Slow Number : 4";
					NormalNum.text = "Normal Number : 10";
				} else if (WaveNumber == 3) {
					FastNum.text = "Fast Number : 1";
					SlowNum.text = "Slow Number : 1";
					NormalNum.text = "Normal Number : 1";
				} else if (WaveNumber == 2) {
					FastNum.text = "Fast Number : 1";
					SlowNum.text = "Slow Number : 1";
					NormalNum.text = "Normal Number : 1";
				} else if (WaveNumber == 1) {
					FastNum.text = "Fast Number : 1";
					SlowNum.text = "Slow Number : 1";
					NormalNum.text = "Normal Number : 1";
				}
			} else {
				WaveNum.text = "";
				FastNum.text = "";
				SlowNum.text = "";
				NormalNum.text = "";
			}
			return;
		} else {
			WaveNum.text = "";
			FastNum.text = "";
			SlowNum.text = "";
			NormalNum.text = "";

			
			_lastspawn = Time.time;
			StartCoroutine (Spawn (WaveNumber - 1));


		}
	}

	IEnumerator Spawn (int index) {

		int arridx = 8 - index;
		

		for (int i = 0; i < arr[arridx].Length; i += 2) {
			int enenum = arr [arridx] [i];
			int kind = arr [arridx] [i + 1];

			Debug.Log (arridx + "      " + "num    " + enenum + "    " + "kind" + kind);


			for (int j = 0; j < enenum; j++) {
				SpawnEnemy (kind);
				yield return new WaitForSeconds(1f);
			}



		}



        if (WaveNumber > 1)
		    WaveNumber--;
	}

	//index, WaveNum is decreasing
	private void SpawnEnemy(int index){

		Debug.Log ( (10 - WaveNumber) + "    " + index);


		Vector3 pos = new Vector3 (0, 1, 0);
		Quaternion rotation = new Quaternion (-45, 0, 0, 0);
		GameObject enemyObject = (GameObject) Object.Instantiate(_enemyPrefab, pos, rotation, _holder);

		Renderer r = enemyObject.GetComponent<Renderer>();
		r.material.color = render [index];

		enemyObject.transform.localScale = new Vector3 (scale[index], 0.3f, 1);

		Enemy enemy = enemyObject.GetComponent<Enemy> ();
		enemy._health = health[index];


		enemy.startSpeed = startspeed[index];
		enemy._enemySpeed = enespeed[index];



		enemy.tag = "Enemy";
	}
    

}


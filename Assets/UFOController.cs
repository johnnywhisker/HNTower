using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour {
	
	private Vector3 pos;
	private bool isRising;
	private Vector3 desirerLoc;
	private float bottom2;
	private double UFOheight;
	private bool isPickup;
	private bool isPicked;
	private bool isDrop;
	private Vector3 pill1, pill2, pill3, home;

	public double root;
	public GameObject venus;
	public double bottom;
	public double shakeTop;
	public double shakeBottom;
	public double shakeRatio;
	public double ratio;
	public float moveRatio;

	void Start () {
		// Khởi tạo các giá trị mặc định
		pos = this.gameObject.transform.position; // Vị trí ban đầu của UFO
		isRising = false; // Thể hiện dao động lên hay xuống của UFO 
		isPickup = false; // Thể hiện vật đang ở trạng thái bị gắp hay không
		isDrop   = false; // Thể hiện vật đang trong trạng thái bị thả tự do
		UFOheight = 4.29f; // Chiều cao của Sprite UFO 
		bottom2   = -4.41f; // Vị trí đáy của cột 
		pill1 = new Vector3 (-6.57f, 3.56f, -2.46f); // Vị trí đối chiếu của UFO với cột 1
		pill2 = new Vector3 (-2.27f,3.63f,-2.46f); // cột 2
		pill3 = new Vector3 (2.07f,3.7f,-2.46f); // cột 3
		home  = new Vector3 (-10.21f,3.5f,-2.46f); // Vị trí đối chiếu của UFO offscreen 
		desirerLoc = pill1; // set vị trí mong muốn đầu tiên là cột 1 -> UFO sẽ đi từ vị trí offscreen(mặc định) tiến vào cột 1
	}

	void Update () {
		verticalMovement (); // chạy hàm để dao động lên xuống UFO 
		pos = transform.position; // khởi tạo xác định vị trí UFO mỗi frame hình
		if(Input.GetKey(KeyCode.Alpha1)) // Khởi tạo event khi nhấn phím 1 
			desirerLoc = pill1;
		if (Input.GetKey (KeyCode.Alpha2))
			desirerLoc = pill2;
		if (Input.GetKey (KeyCode.Alpha3))
			desirerLoc = pill3;
		if (Input.GetKey (KeyCode.H))
			desirerLoc = home;
		if (Input.GetKey (KeyCode.Space)) {
			if (transform.position.x - venus.transform.position.x < 1) { // set điều kiện pickup. UFO chỉ pickup khi và chỉ khi vị trí UFO và venus k cách quá 1 đơn vị chiều dài
				isPickup = true;
			}
		}
		if (Input.GetKey (KeyCode.D))
			isPickup = false;

		// Code giám sát vị trí UFO từng frame hình 
		
		if (pos.x < desirerLoc.x+2.13) {
			transform.position = new Vector3 (pos.x + moveRatio, pos.y, pos.z);
		}
		if (pos.x > desirerLoc.x + 2.13) {
			transform.position = new Vector3 (pos.x - moveRatio, pos.y, pos.z);
		}

		// Code giám sát trạng thái nhặt hành tinh từng frame hình
		if (isPickup == true && venus.transform.position.y + 1.5 < UFOheight) {
			venus.transform.position = new Vector3 (pos.x, venus.transform.position.y + moveRatio, pos.z);
		}
		if (isPickup == false && venus.transform.position.y + 1.5 > bottom2+1.5f && desirerLoc == pill2) {
			isPicked = false;
			venus.transform.position = new Vector3 (pill2.x+2.13f, venus.transform.position.y - moveRatio, pill2.z);
		}
		// Code giám sát đồng bộ hoá chuyển động của hành tình với UFO khi hành tinh được nhặt lên 
		if (venus.transform.position.y + 1.5 >= UFOheight)
			isPicked = true;
		if (isPicked) {
			venus.transform.position = new Vector3 (pos.x, (float)pos.y - 1f, pos.z);
		}		
	}
	// Code hàm giám sát chuyển động trục dọc của UFO 
	void verticalMovement(){
		pos = transform.position;
		if (isRising) {			
			pos.y += (float)ratio;
			if (this.gameObject.transform.position.y >= root) 
				isRising = false;
			this.gameObject.transform.position = pos;

		} else {
			pos.y = pos.y - (float)ratio;
			if (this.gameObject.transform.position.y <= bottom)
				isRising = true;
			this.gameObject.transform.position = pos;
		} 
	}
}

public class PlanetManager{
	List<GameObject> Planets = new List<GameObject> ();
	public PlanetManager(GameObject[] Planets) {
		this.Planets.AddRange (Planets);
	}
}
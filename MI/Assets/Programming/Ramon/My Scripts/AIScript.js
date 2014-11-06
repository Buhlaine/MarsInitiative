var waypoints : Transform[];

static var currentTarget : Transform;

private var waypointNo : int;
private var moveDirection : Vector3 = Vector3.zero;
private var walkSpeed : int;;
private var gravity : float = 100.0;
private var charController : CharacterController;

function Start() {
	waypointNo = 0;
	charController = GetComponent(CharacterController);
}

function Update() {
	currentTarget = waypoints[waypointNo];
	
	RotateToTarget(currentTarget);
	
	var target : Vector3 = currentTarget.position;
	moveDirection = target - transform.position;
	
	if (moveDirection.magnitude > 20) {
		walkSpeed = 10;
	}
	else {
		waypointNo++;
		
		if (waypointNo >= waypoints.length) {
			waypointNo = 0;
		}
	}	
	
	//print(waypointNo);
	
	moveDirection = Vector3(0,0,walkSpeed * Time.deltaTime);
	moveDirection = transform.TransformDirection(moveDirection);
	moveDirection.y -= gravity * Time.deltaTime;
	charController.Move(moveDirection);
}

function RotateToTarget(targeted : Transform) {
	var rotate = Quaternion.LookRotation(targeted.position - transform.position);
	var damp  = 2.0;
	transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * damp);
}
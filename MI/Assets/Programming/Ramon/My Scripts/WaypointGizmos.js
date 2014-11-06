var wayPointName : String;
var wayPointOn : boolean = true;

function OnDrawGizmos() {
	if (wayPointOn) {
		Gizmos.DrawIcon(transform.position, wayPointName);
	}
}
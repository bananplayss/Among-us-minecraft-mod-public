using Reactor.Utilities.Attributes;
using UnityEngine;

namespace bananplaysshu {
	public static class EscapeManager {

	public static bool HasAllCrewmatesEscaped() {
			
			int numOfCrewmatesEscaped = 0;
			int numOfAliveCrewmates = PlayerControl.AllPlayerControls.Count-1;
			foreach(var pc in PlayerControl.AllPlayerControls) {
				if (pc.Data.RoleType == AmongUs.GameOptions.RoleTypes.Impostor) continue;
				if (pc.Data.IsDead) {
					numOfAliveCrewmates--;
				}
				if (!pc.cosmetics.nameText.gameObject.activeSelf) {
					numOfCrewmatesEscaped++;
				}
			}
			Debug.Log("Alive: " + numOfAliveCrewmates);
			Debug.Log("Escaped: " + numOfCrewmatesEscaped);
			return numOfCrewmatesEscaped >= numOfAliveCrewmates;
		}		

	}
}

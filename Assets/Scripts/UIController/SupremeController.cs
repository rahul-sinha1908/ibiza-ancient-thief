using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;

public interface SupremeController {
	void showTravellingWindow();
	void showCooldownWindow();
	void hideAllWindows();
	void setCooldownTime(int time);
}

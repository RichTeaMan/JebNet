using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JebNet
{
    public class ControllerPart : Part
    {
        bool pilotEngaged = false;

        protected override void onFlightStart()  //Called when vessel is placed on the launchpad
        {
            //at the beginning of the flight, register your fly-by-wire control function that will be called repeatedly during flight:
            vessel.OnFlyByWire += new FlightInputCallback(fly);
            RenderingManager.AddToPostDrawQueue(3, new Callback(drawGUI));//start the GUI
            print("controller part flight started");
        }

        //remove the fly-by-wire function when we get destroyed:
        protected override void onPartDestroy()
        {
            vessel.OnFlyByWire -= new FlightInputCallback(fly);
            RenderingManager.RemoveFromPostDrawQueue(3, new Callback(drawGUI)); //close the GUI
        }

        //remove the fly-by-wire function when we get disconnected from the ship:
        protected override void onDisconnect()
        {
            vessel.OnFlyByWire -= new FlightInputCallback(fly);
        }


        //now this function gets called every frame or something and gives you access to the flight controls
        private void fly(FlightCtrlState s)
        {
            if (pilotEngaged)
            {
                
            }
        }

        protected Rect windowPos;

        private void WindowGUI(int windowID)
        {
            GUIStyle mySty = new GUIStyle(GUI.skin.button);
            mySty.normal.textColor = mySty.focused.textColor = Color.white;
            mySty.hover.textColor = mySty.active.textColor = Color.yellow;
            mySty.onNormal.textColor = mySty.onFocused.textColor = mySty.onHover.textColor = mySty.onActive.textColor = Color.green;
            mySty.padding = new RectOffset(8, 8, 8, 8);

            GUILayout.BeginVertical();
            if (GUILayout.Button("FLY", mySty, GUILayout.ExpandWidth(true)))//GUILayout.Button is "true" when clicked
            {
                beginFlight();
            }
            GUILayout.EndVertical();

            //DragWindow makes the window draggable. The Rect specifies which part of the window it can by dragged by, and is 
            //clipped to the actual boundary of the window. You can also pass no argument at all and then the window can by
            //dragged by any part of it. Make sure the DragWindow command is AFTER all your other GUI input stuff, or else
            //it may "cover up" your controls and make them stop responding to the mouse.
            GUI.DragWindow(new Rect(0, 0, 10000, 20));

        }

        private void beginFlight()
        {
            Staging.ActivateNextStage();
        }

        private void drawGUI()
        {
            GUI.skin = HighLogic.Skin;
            windowPos = GUILayout.Window(1, windowPos, WindowGUI, "Self Destruct", GUILayout.MinWidth(100));
        }

        protected override void onPartStart()
        {
            if ((windowPos.x == 0) && (windowPos.y == 0))//windowPos is used to position the GUI window, lets set it in the center of the screen
            {
                windowPos = new Rect(Screen.width / 2, Screen.height / 2, 10, 10);
            }
        }

    }
}

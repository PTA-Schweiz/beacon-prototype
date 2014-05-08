﻿using System;
using MonoTouch.UIKit;
using BeaconLibrary;
using System.Collections.Generic;
using BeaconLibrary;

namespace BeaconTouch
{
    public partial class InfoViewController : UIViewController
    {
        IndoorLocationController indoorLocationController;

        public InfoViewController(IntPtr handle) : base (handle) 
        {
        }

        public InfoViewController() : base("InfoViewController", null)
        {
        }
            

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            var beaconManager = new BeaconManager();
            indoorLocationController = new IndoorLocationController(beaconManager);
            indoorLocationController.BeaconFound += HandleBeaconFound;
            indoorLocationController.BeaconLost += HandleBeaconLost;

            var beacon1 = DummyBeaconCreator.CreateBeaconUrsin();
//            var beacon2 = DummyBeaconCreator.CreateBeaconMarcel();
            indoorLocationController.AddBeaconsAndStart(new List<Beacon>{beacon1});
        }

        void HandleBeaconFound (Beacon beacon)
        {
            UpdateDisplay(String.Format("Beacon with ID {0} found!",beacon.UniqueId), UIColor.Blue);
        }

        void HandleBeaconLost (Beacon beacon)
        {
            UpdateDisplay(String.Format("Beacon with ID {0} lost!",beacon.UniqueId), UIColor.DarkGray);
        }
        
        void UpdateDisplay(string message, UIColor color)
        {
            InvokeOnMainThread(delegate {

                string formatedMessage = String.Format("{0}: {1}",DateTime.Now.ToString("T"),message) + Environment.NewLine;

                FullView.BackgroundColor = color;

                if(InfoText.Text.Equals("There aren't any iBeacons nearby"))
                {
                    InfoText.Text = string.Empty;
                }

                InfoText.Text += formatedMessage;
            });
        }
    }
}


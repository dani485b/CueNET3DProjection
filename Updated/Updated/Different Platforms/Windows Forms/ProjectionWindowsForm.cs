﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SRP_3D_Projection_on_Keyboard.Basic_Projection;

namespace SRP_3D_Projection_on_Keyboard.Different_Platforms.WindowsForm {
    class Projection {
        public static void Main(Model model) {
            System.Console.Clear();
            var canvas = new WindowsForms(model);
            canvas.ShowDialog();
        }

        //Windows forms skal selv kører det
        public static void ActualStuff(Model model, WindowsForms canvas) {
            model.RotateBy(new PointF3D((float)model.properties.rotateAmount, (float)model.properties.rotateAmount, (float)model.properties.rotateAmount));

            canvas.Clear();
            model.Translation(new PointF(200, 250)); //Flyt den ind i midten af tastaturet
            model.Draw(canvas, Color.Red, Color.Green);
            model.Translation(new PointF(600, 250)); //Flyt den ind i midten af tastaturet
            model.Draw(canvas, Color.RoyalBlue, Color.Purple);

            //Thread.Sleep((int)(0.03 * 1000));
        }
    }
}